

using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Identity.Entities;

namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultPropertyType
    {
        public static async Task SeedAsync(IPropertyTypeRepository _propertyTypeRepository) 
        {
            PropertyType propertyType1 = new();
            propertyType1.Name = "Apartamento";
            propertyType1.Description = "Construccion de mas de 3 pisos y menos de 7"; //No se muy bien que va en la descripcion realmente

            PropertyType propertyType2 = new();
            propertyType2.Name = "Casa";
            propertyType2.Description = "Maximo 3 pisos";

            var propertyList = await _propertyTypeRepository.GetAllListAsync();

            if(propertyList.All(p=>p.Id!=propertyType1.Id || p.Id != propertyType2.Id)) 
            {
                if(!propertyList.Any(p => p.Name == propertyType1.Name || p.Name == propertyType2.Name)) 
                { 
                    await _propertyTypeRepository.AddAsync(propertyType1);
                    await _propertyTypeRepository.AddAsync(propertyType2);
                }
            }

        }
    }
}
