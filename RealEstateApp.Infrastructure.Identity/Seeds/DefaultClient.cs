

using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Identity.Entities;

namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultClient
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, IUserRepository userRepository ) 
        {
            ApplicationUser defaultClient = new();
            defaultClient.FirstName = "Justin";
            defaultClient.LastName = "Baez";
            defaultClient.UserName = "client1";
            defaultClient.PhoneNumber = "809-403-7589";
            defaultClient.Email = "justin@email.com";
            defaultClient.Photo = "https://www.cognodata.com/wp-content/uploads/2019/01/perfil-de-cliente-e1549901099803-1.jpg";
            defaultClient.IsActive = true;
            defaultClient.EmailConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultClient.Id)) 
            { 
                var user = await userManager.FindByEmailAsync(defaultClient.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultClient, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultClient, Roles.Cliente.ToString());
                    var createdUser=await userManager.FindByEmailAsync(defaultClient.Email);
                    var rol = await userManager.GetRolesAsync(createdUser);

                    await userRepository.AddAsync(new User
                    {
                        Id = defaultClient.Id,
                        FirstName = defaultClient.FirstName,
                        LastName = defaultClient.LastName,
                        Phone = defaultClient.PhoneNumber,
                        Photo = defaultClient.Photo,
                        IsActive = defaultClient.IsActive,
                        Email = defaultClient.Email,
                        Rol = rol.FirstOrDefault(),
                        UserName = defaultClient.UserName,
                    });
                }
            }
        }
    }
}
