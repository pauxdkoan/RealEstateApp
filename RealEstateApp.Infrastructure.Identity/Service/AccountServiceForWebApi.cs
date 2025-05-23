﻿

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Dtos.Email;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Core.Domain.Settings;
using RealEstateApp.Infrastructure.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace RealEstateApp.Infrastructure.Identity.Service
{
    public class AccountServiceForWebApi : AccountService, IWebApiAccountService
    {
      
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserRepository _userRepository;

        public AccountServiceForWebApi(IEmailService emailService, IUserRepository userRepository,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            , RoleManager<IdentityRole> roleManager, IOptions<JWTSettings> jwtSettings)
            : base(userManager, emailService, userRepository,roleManager, jwtSettings)
        { 
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
           
        }

        public async Task<AuthenticationResponseForApi> AuthenticateAsync(AuthenticationRequest request) 
        {
            AuthenticationResponseForApi response = new();
            var user = await _userManager.FindByEmailAsync(request.EmailOrUsername)??
                await _userManager.FindByNameAsync(request.EmailOrUsername);



            if (user == null) 
            { 
                response.HasError = true;
                response.Error = $"No hay usuario registrada con esta credencial: {request.EmailOrUsername}";
                throw new ApiException($"No hay usuario registrada con esta credencial: {request.EmailOrUsername}", (int)HttpStatusCode.BadRequest);
            }


            var rolesOfUser = await _userManager.GetRolesAsync(user);
            if (rolesOfUser.Contains(Roles.Agente.ToString()) || rolesOfUser.Contains(Roles.Cliente.ToString()))
            {
                throw new ApiException($"No tienes permisos para usar esta API", (int)HttpStatusCode.BadRequest);
            }


            var isPasswordValid = await _userManager.CheckPasswordAsync (user, request.Password );

            if (!isPasswordValid)
            {
                response.HasError = true;
                response.Error = $"Credenciales invalidas para el usuario:{user.Email}";
                throw new ApiException($"Credenciales invalidas para el usuario:{user.Email}", (int)HttpStatusCode.BadRequest);

                return response ;
            }



            if (!isPasswordValid || !user.EmailConfirmed) 
            { 
                response.HasError=true;
                response.Error = $"Correo no confirmado: {user.Email}, favor confirmar su correo";
                throw new ApiException($"Correo no confirmado: {user.Email}, favor confirmar su correo", (int)HttpStatusCode.BadRequest);
                return response;
            }
            if (!isPasswordValid || !user.IsActive)
            {
                response.HasError = true;
                response.Error = $"Cuenta desactivada {user.Email}, favor comunicarse con un administrador";
                throw new ApiException($"Cuenta desactivada {user.Email}, favor comunicarse con un administrador", (int)HttpStatusCode.BadRequest);
                return response;
            }

            JwtSecurityToken jwtSecurityToken= await GenerateJWToken(user);

            response.Id=user.Id;
            response.Email=user.Email;
            response.UserName = user.UserName;
            response.FirstName=user.FirstName;
            response.LastName=user.LastName;
            response.IsVerified = true;
            response.IdentityCard = user.IdentityCard;
            var roles = await _userManager.GetRolesAsync(user);
            response.Roles = roles.ToList();
            response.JWToken= new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken= refreshToken.Token;

            return response;
        }

        public async Task<RegisterResponse> RegisterDeveloperUserAsync(RegisterRequest request)
        {
            RegisterResponse response = new()
            {
                HasError = false,
            };

            var pwd = request.Password;
            if (pwd.Length < 6 || !pwd.Any(char.IsUpper) || !pwd.Any(char.IsDigit))
            {
               
                throw new ApiException( "La contraseña debe tener al menos 6 caracteres, " +"una letra mayúscula y un dígito.",(int)HttpStatusCode.BadRequest);
            }

            var userWithSameUsername = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUsername != null)
            {
                response.HasError = true;
                response.Error = $"El nombre de usuario: '{request.UserName}' ya esta registrado, intente con uno nuevo.";

                throw new ApiException($"El nombre de usuario: '{request.UserName}' ya esta registrado, intente con uno nuevo.", (int)HttpStatusCode.BadRequest);
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"El correo: '{request.Email}' ya esta registrado, intente con uno nuevo.";
                throw new ApiException($"El correo de usuario: '{request.UserName}' ya esta registrado, intente con uno nuevo.", (int)HttpStatusCode.BadRequest);

                
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                IdentityCard = request.IdentityCard,
                Photo = request.Photo,
                EmailConfirmed = true,

                IsActive = true,
            };

            request.Rol=Roles.Desarrollador.ToString();

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, request.Rol);

                if (request.Rol == Roles.Desarrollador.ToString())
                {
                    var developer = await _userManager.FindByEmailAsync(user.Email);
                    developer.IsActive = true;
                    developer.EmailConfirmed = true;
                    await _userManager.UpdateAsync(developer);

                    //Se registra  el usuario del esquema dbo:
                    var userReference = new User()
                    {
                        Id = developer.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        IdentityCard = developer.IdentityCard,
                        IsActive = user.IsActive,
                        Email = user.Email,
                        Rol = Roles.Desarrollador.ToString(),
                    };

                    await _userRepository.AddAsync(userReference);
                }
            }
            else
            {
                response.HasError = true;
                response.Error = $"Ocurrio un error inesperado al intentar registrar su usuario.";
                throw new ApiException($"Ocurrio un error inesperado al intentar registrar su usuario.", (int)HttpStatusCode.InternalServerError);
                return response;
            }

            return response;
        }

        public async Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request)
        {
            RegisterResponse response = new()
            {
                HasError = false,
            };

            var pwd = request.Password;
            if (pwd.Length < 6 || !pwd.Any(char.IsUpper) || !pwd.Any(char.IsDigit))
            {

                throw new ApiException("La contraseña debe tener al menos 6 caracteres, " + "una letra mayúscula y un dígito.", (int)HttpStatusCode.BadRequest);
            }

            var userWithSameUsername = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUsername != null)
            {
                response.HasError = true;
                response.Error = $"El nombre de usuario: '{request.UserName}' ya esta registrado, intente con uno nuevo.";

                throw new ApiException($"El nombre de usuario: '{request.UserName}' ya esta registrado, intente con uno nuevo.", (int)HttpStatusCode.BadRequest);
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"El correo: '{request.Email}' ya esta registrado, intente con uno nuevo.";
                throw new ApiException($"El correo de usuario: '{request.UserName}' ya esta registrado, intente con uno nuevo.", (int)HttpStatusCode.BadRequest);


            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                IdentityCard = request.IdentityCard,
                Photo = request.Photo,
                EmailConfirmed = true,

                IsActive = true,
            };
            request.Rol=Roles.Administrador.ToString();

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, request.Rol);


                if (request.Rol == Roles.Administrador.ToString())
                {
                    var admin = await _userManager.FindByEmailAsync(user.Email);
                    admin.IsActive = true;
                    admin.EmailConfirmed = true;
                    await _userManager.UpdateAsync(admin);

                    //Se registra  el usuario del esquema dbo:
                    var userReference = new User()
                    {
                        Id = admin.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        IdentityCard = admin.IdentityCard,
                        IsActive = user.IsActive,
                        Email = user.Email,
                        Rol = Roles.Administrador.ToString(),
                    };

                    await _userRepository.AddAsync(userReference);
                }

                /*Falta el registro de desarrolladores y admin 
                  Se agrega despues por q no se si vienen activo por default...
                 */
            }
            else
            {
                response.HasError = true;
                response.Error = $"Ocurrio un error inesperado al intentar registrar su usuario.";
                throw new ApiException($"Ocurrio un error inesperado al intentar registrar su usuario.", (int)HttpStatusCode.InternalServerError);
                return response;
            }

            return response;
        }

    }
}
