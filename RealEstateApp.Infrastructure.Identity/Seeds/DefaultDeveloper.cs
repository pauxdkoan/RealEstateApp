

using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Identity.Entities;

namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultDeveloper
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, IUserRepository userRepository ) 
        {
            ApplicationUser defaultDeveloper = new();
            defaultDeveloper.FirstName = "Angelito";
            defaultDeveloper.LastName = "Bozo";
            defaultDeveloper.UserName = "devop1";
            defaultDeveloper.Email = "devop1@email.com";
            defaultDeveloper.IdentityCard = "402-2998511-7";
            defaultDeveloper.IsActive = true;
            defaultDeveloper.EmailConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultDeveloper.Id)) 
            { 
                var user = await userManager.FindByEmailAsync(defaultDeveloper.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultDeveloper, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultDeveloper, Roles.Desarrollador.ToString());
                    var createdUser=await userManager.FindByEmailAsync(defaultDeveloper.Email);
                    var rol = await userManager.GetRolesAsync(createdUser);

                    await userRepository.AddAsync(new User
                    {
                        Id = defaultDeveloper.Id,
                        FirstName = defaultDeveloper.FirstName,
                        LastName = defaultDeveloper.LastName,
                        IsActive = defaultDeveloper.IsActive,
                        Email = defaultDeveloper.Email,
                        Rol = rol.FirstOrDefault(),
                        UserName = defaultDeveloper.UserName,
                        IdentityCard= defaultDeveloper.IdentityCard,
                        



                    });
                }
            }
        }
    }
}
