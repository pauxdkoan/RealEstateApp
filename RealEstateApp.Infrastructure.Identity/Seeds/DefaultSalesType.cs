

using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Identity.Entities;

namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultSalesType
    {
        public static async Task SeedAsync( ISalesTypeRepository _salesTypeRepository ) 
        {
            SalesType salesType1 = new();
            salesType1.Name = "Venta Amueblado";
            salesType1.Description = "Muebles bonitos"; //No se muy bien que va en la descripcion realmente


            SalesType salesType2 = new();
            salesType1.Name = "Alquiler Amueblado";
            salesType1.Description = "Muebles feos"; //No se muy bien que va en la descripcion realmente

            var saleTypeList = await _salesTypeRepository.GetAllListAsync();

            if (saleTypeList.All(p => p.Id != salesType1.Id || p.Id != salesType2.Id))
            {
                if (!saleTypeList.Any(p => p.Name == salesType1.Name || p.Name == salesType2.Name))
                {
                    await _salesTypeRepository.AddAsync(salesType1);
                    await _salesTypeRepository.AddAsync(salesType2);
                }
            }

        }
    }
}
