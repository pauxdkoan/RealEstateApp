

using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Identity.Entities;

namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultAgents
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, IUserRepository userRepository, IPropertyRepository propertyRepository ) 
        {
            ApplicationUser defaultAgent1 = new();
            defaultAgent1.FirstName = "Paulo";
            defaultAgent1.LastName = "Burgos";
            defaultAgent1.PhoneNumber = "809-402-7589";
            defaultAgent1.UserName = "agente01";
            defaultAgent1.Email = "paulo@email.com";
            defaultAgent1.Photo = "https://thumbs.dreamstime.com/b/alcances-del-agente-secreto-para-el-arma-6848466.jpg";
            defaultAgent1.IsActive = true;
            defaultAgent1.EmailConfirmed = true;

            ApplicationUser defaultAgent2 = new();
            defaultAgent2.FirstName = "Saihan";
            defaultAgent2.LastName = "Castillo";
            defaultAgent2.PhoneNumber = "809-401-7589";
            defaultAgent2.UserName = "agente02";
            defaultAgent2.Email = "saihan@email.com";
            defaultAgent2.Photo = "https://thumbs.dreamstime.com/b/alcances-del-agente-secreto-para-el-arma-6848466.jpg";
            defaultAgent2.IsActive = true;
            defaultAgent2.EmailConfirmed = true;

            ApplicationUser defaultAgent3 = new();
            defaultAgent3.FirstName = "Leonardo";
            defaultAgent3.LastName = "Tavarez";
            defaultAgent3.PhoneNumber = "809-401-7589";
            defaultAgent3.UserName = "agente03";
            defaultAgent3.Email = "lacabra@email.com";
            defaultAgent3.Photo = "https://cdn-images.dzcdn.net/images/cover/380f3bcfb8e88dcf37c6ef66b523bd94/0x1900-000000-80-0-0.jpg";
            defaultAgent3.IsActive = true;
            defaultAgent3.EmailConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultAgent1.Id || u.Id != defaultAgent2.Id || u.Id != defaultAgent3.Id))
            {
                var agent1 = await userManager.FindByEmailAsync(defaultAgent1.Email) ?? await userManager.FindByNameAsync(defaultAgent1.UserName);
                var agent2 = await userManager.FindByEmailAsync(defaultAgent2.Email) ?? await userManager.FindByNameAsync(defaultAgent2.UserName);
                var agent3 = await userManager.FindByEmailAsync(defaultAgent3.Email) ?? await userManager.FindByNameAsync(defaultAgent3.UserName);


                if (agent1 == null)
                {
                    await userManager.CreateAsync(defaultAgent1, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultAgent1, Roles.Agente.ToString());
                    var createdUser = await userManager.FindByEmailAsync(defaultAgent1.Email);
                    var rol = await userManager.GetRolesAsync(createdUser);

                    await userRepository.AddAsync(new User
                    {
                        Id = defaultAgent1.Id,
                        FirstName = defaultAgent1.FirstName,
                        LastName = defaultAgent1.LastName,
                        Phone = defaultAgent1.PhoneNumber,
                        Photo = defaultAgent1.Photo,
                        IsActive = defaultAgent1.IsActive,
                        Email = defaultAgent1.Email,
                        UserName = defaultAgent1.UserName,
                        Rol = rol.FirstOrDefault(),
                    });
                }

                if (agent2 == null)
                {
                    await userManager.CreateAsync(defaultAgent2, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultAgent2, Roles.Agente.ToString());
                    var createdUser = await userManager.FindByEmailAsync(defaultAgent2.Email);
                    var rol = await userManager.GetRolesAsync(createdUser);

                    await userRepository.AddAsync(new User
                    {
                        Id = defaultAgent2.Id,
                        FirstName = defaultAgent2.FirstName,
                        LastName = defaultAgent2.LastName,
                        Phone = defaultAgent2.PhoneNumber,
                        Photo = defaultAgent2.Photo,
                        IsActive = defaultAgent2.IsActive,
                        UserName= defaultAgent2.UserName,
                        Email = defaultAgent2.Email,
                        Rol = rol.FirstOrDefault(),
                    });

                   
                }

                if (agent3 == null)
                {
                    await userManager.CreateAsync(defaultAgent3, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultAgent3, Roles.Agente.ToString());
                    var createdUser = await userManager.FindByEmailAsync(defaultAgent3.Email);
                    var rol = await userManager.GetRolesAsync(createdUser);

                    await userRepository.AddAsync(new User
                    {
                        Id = defaultAgent3.Id,
                        FirstName = defaultAgent3.FirstName,
                        LastName = defaultAgent3.LastName,
                        Phone = defaultAgent3.PhoneNumber,
                        Photo = defaultAgent3.Photo,
                        UserName = defaultAgent3.UserName,
                        IsActive = defaultAgent3.IsActive,
                        Email = defaultAgent3.Email,
                        Rol = rol.FirstOrDefault(),
                    });
                }

                await DefaultProperties.SeedAsync(propertyRepository, new string[] {defaultAgent1.Id, defaultAgent2.Id, defaultAgent3.Id });
            }
        }
    }
}
