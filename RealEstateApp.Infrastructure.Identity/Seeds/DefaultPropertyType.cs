

using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Identity.Entities;

namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultPropertyType
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, IUserRepository userRepository ) 
        {
            
          
        }
    }
}
