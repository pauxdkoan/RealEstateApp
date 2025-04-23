

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using RealEstateApp.Core.Application.Dtos.Email;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Infrastructure.Identity.Entities;
using System.Text;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace RealEstateApp.Infrastructure.Identity.Service
{
    public class AccountService :IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        

        public AccountService(UserManager<ApplicationUser> userManager, IEmailService emailService, 
            IUserRepository userRepository, RoleManager<IdentityRole> roleManager) 
        {  
            _userManager = userManager;
            _userRepository = userRepository;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        public async Task<List<string>> GetAllRoles() 
        {
            var roles= await _roleManager.Roles.Select(r=>r.Name).ToListAsync();
            return roles;
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin) 
        {
            RegisterResponse response = new() 
            { 
                HasError = false,
            };

            var userWithSameUsername = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUsername != null) 
            { 
                response.HasError = true;
                response.Error = $"El nombre de usuario: '{request.UserName}' ya esta registrado, intente con uno nuevo.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"El correo: '{request.UserName}' ya esta registrado, intente con uno nuevo.";
                return response;
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
                EmailConfirmed =false,

                IsActive=false,
            };
            
            var result= await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded) 
            {
                await _userManager.AddToRoleAsync(user, request.Rol);
                
                if (request.Rol == Roles.Cliente.ToString()) 
                {
                    //Se registra  el usuario del esquema dbo:
                    var client= await _userManager.FindByEmailAsync(user.Email);  
                    var userReference = new User() 
                    { 
                        Id= client.Id,
                        FirstName = client.FirstName,
                        LastName= client.LastName,
                        UserName=client.UserName,
                        Phone= client.PhoneNumber,
                        IdentityCard= client.IdentityCard,
                        IsActive= client.IsActive,
                        Photo= client.Photo,
                        Email= client.Email,
                        Rol = Roles.Cliente.ToString(),
                    };

                    await _userRepository.AddAsync(userReference);

                    //Luego se hace la verificacion de correo
                    var verificationUri = await SendVerificationEmailUri(user, origin);
                    await _emailService.SendAsync(new EmailRequest()
                    {
                        //OJO AQUI
                        To = user.Email,
                        Body = $"Gracias por registrate en [NOSE], ahora confirme su usuario visitando esta url:{verificationUri}",
                        Subject= "Verificar Registro"

                    });
                }

                if (request.Rol == Roles.Agente.ToString())
                {
                    var agent=await _userManager.FindByEmailAsync(user.Email);
                    agent.EmailConfirmed = true;
                    await _userManager.UpdateAsync(agent);

                    //Se registra  el usuario del esquema dbo:
                    var userReference = new User() 
                    {
                        Id= agent.Id,
                        FirstName=user.FirstName,
                        LastName=user.LastName,
                        UserName=user.UserName,
                        IdentityCard=user.IdentityCard,
                        Phone=user.PhoneNumber,
                        IsActive=user.IsActive,
                        Photo=user.Photo,
                        Email=user.Email,
                        Rol = Roles.Agente.ToString(),
                    };
                    
                    await _userRepository.AddAsync(userReference);
                }

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
                        IdentityCard = user.IdentityCard,
                        IsActive = user.IsActive,
                        Email = user.Email,
                        Rol = Roles.Agente.ToString(),
                    };

                    await _userRepository.AddAsync(userReference);
                }

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
                        IdentityCard = user.IdentityCard,
                        IsActive = user.IsActive,
                        Email = user.Email,
                        Rol = Roles.Agente.ToString(),
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
                return response;
            }

            return response;
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null) 
            {
                return $"No hay cuenta registrada con este usuario";
            }

            token= Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                user.IsActive = true;
                await _userManager.UpdateAsync(user);
                var userDbo= await _userRepository.GetByIdAsync(user.Id);
                userDbo.IsActive = true;
                await _userRepository.UpdateAsync(userDbo, userDbo.Id);

                return $"Correo confirmado de {user.Email}. Ahora puedes utilizar la app";
            }
            else 
            {
                return $"Ocurrio un error mientras se confirmaba el {user.Email}";
            }

        }


        #region Protected
        protected async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin) 
        { 
            var code= await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code)); //Se cifra
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);//se le pasa el id del usuario
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);//y el token

            return verificationUri;
        }
        #endregion


    }
}
