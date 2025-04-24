

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Domain.Settings;
using RealEstateApp.Infrastructure.Identity.Entities;

namespace RealEstateApp.Infrastructure.Identity.Service
{
    public class AccountServiceForWebApp :AccountService, IWebAppAccountService
    {
      
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountServiceForWebApp(IEmailService emailService, IUserRepository userRepository,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            , RoleManager<IdentityRole> roleManager, IOptions<JWTSettings> jwtSettings)
            : base(userManager, emailService, userRepository,roleManager, jwtSettings)
        { 
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request) 
        {
            AuthenticationResponse response = new();
            var user = await _userManager.FindByEmailAsync(request.EmailOrUsername)??
                await _userManager.FindByNameAsync(request.EmailOrUsername);

            if (user == null) 
            { 
                response.HasError = true;
                response.Error = $"No hay usuario registrada con esta credencial: {request.EmailOrUsername}";
                return response;
            }
            
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            
            var roles=  await _userManager.GetRolesAsync(user);
            if (roles.Contains(Roles.Desarrollador.ToString())) 
            { 
                response.HasError= true;
                response.Error = $"Los desarrolladores no tienen permisos para utilizar la App";
                return response ;
            }

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Credenciales invalidas para el usuario:{user.Email}";
                return response ;
            }
            if (!user.EmailConfirmed) 
            { 
                response.HasError=true;
                response.Error = $"Correo no confirmado: {user.Email}, favor confirmar su correo";
                return response;
            }
            if (!user.IsActive)
            {
                response.HasError = true;
                response.Error = $"Cuenta desactivada {user.Email}, favor comunicarse con un administrador";
                return response;
            }

            response.Id=user.Id;
            response.Email=user.Email;
            response.UserName = user.UserName;
            response.FirstName=user.FirstName;
            response.LastName=user.LastName;
            var rol = await _userManager.GetRolesAsync(user);
            response.Rol = rol.FirstOrDefault();
            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
