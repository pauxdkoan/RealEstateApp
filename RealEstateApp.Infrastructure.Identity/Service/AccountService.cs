

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using RealEstateApp.Core.Application.Dtos.Email;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Infrastructure.Identity.Entities;
using System.Text;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using AutoMapper;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Infrastructure.Identity.Service
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AccountService(UserManager<ApplicationUser> userManager, IEmailService emailService, IUserRepository userRepository, IMapper mapper) 
        {  
            _userManager = userManager;
            _userRepository = userRepository;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin) 
        {
            RegisterResponse response = new() 
            { 
                HasError = false,
            };

            var userWithSameUsername = _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUsername != null) 
            { 
                response.HasError = true;
                response.Error = $"El nombre de usuario: '{request.UserName}' ya esta registrado.";
                return response;
            }

            var userWithSameEmail = _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"El correo: '{request.UserName}' ya esta registrado.";
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
                Photo = request.PhotoUrl,
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
                    await _userManager.UpdateAsync(user);

                    //Se registra  el usuario del esquema dbo:
                    var userReference = new User() 
                    { 
                        FirstName=user.FirstName,
                        LastName=user.LastName,
                        IdentityCard=user.IdentityCard,
                        IsActive=user.IsActive,
                        Photo=user.Photo,
                        Email=user.Email,
                        Rol = Roles.Agente.ToString(),
                    };
                    
                    await _userRepository.AddAsync(userReference);
                }
            }
            else
            {
                response.HasError = true;
                response.Error = $"Ocurrio un error inesperado al intentar registrar el usuario.";
                return response;
            }

            return response;
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
