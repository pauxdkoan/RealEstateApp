

using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Identity.Entities;

namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, IUserRepository userRepository ) 
        {
            ApplicationUser defaultAdmin = new();
            defaultAdmin.FirstName = "Leornado";
            defaultAdmin.LastName = "Cabra";
            defaultAdmin.UserName = "admin1";
            defaultAdmin.PhoneNumber = "809-403-7589";
            defaultAdmin.Email = "admin1@email.com";
            defaultAdmin.IdentityCard = "402-2998511-7";
            defaultAdmin.Photo = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRQYE42H29VlT1l7Y9HdJd-jRjW0z0_AlWQ4g&s";
            defaultAdmin.IsActive = true;
            defaultAdmin.EmailConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultAdmin.Id)) 
            { 
                var user = await userManager.FindByEmailAsync(defaultAdmin.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultAdmin, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultAdmin, Roles.Administrador.ToString());
                    var createdUser=await userManager.FindByEmailAsync(defaultAdmin.Email);
                    var rol = await userManager.GetRolesAsync(createdUser);

                    await userRepository.AddAsync(new User
                    {
                        Id = defaultAdmin.Id,
                        FirstName = defaultAdmin.FirstName,
                        LastName = defaultAdmin.LastName,
                        Phone = defaultAdmin.PhoneNumber,
                        Photo = defaultAdmin.Photo,
                        IsActive = defaultAdmin.IsActive,
                        Email = defaultAdmin.Email,
                        Rol = rol.FirstOrDefault(),
                        UserName = defaultAdmin.UserName,
                        IdentityCard= defaultAdmin.IdentityCard,
                        



                    });
                }
            }
        }
    }
}
