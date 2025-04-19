

using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultProperties
    {
        public static async Task SeedAsync(IPropertyRepository _propertyRepository, string[] AgentsId)
        {
            Property property1 = new();
            property1.Code = "123456";
            property1.Price = 40000;
            property1.AmountOfRoom = 2;
            property1.AmountOfBathroom = 3;
            property1.PropertySize = 300;
            property1.State = true; //Vendidad, Disponible
            property1.Description = "Propiedad Grande"; //No se q poner en descripcion
            property1.PropertyTypeId = 1;
            property1.SalesTypeId=1;
            property1.AgentId = AgentsId[0];
       

            Property property2 = new();
            property2.Code = "123457";
            property2.Price = 45000;
            property2.AmountOfRoom = 2;
            property2.AmountOfBathroom = 2;
            property2.PropertySize = 250;
            property2.State = true; //Vendidad, Disponible
            property2.Description = "Propiedad Pequenia"; //No se q poner en descripcion
            property2.PropertyTypeId = 2;
            property2.SalesTypeId = 2;
            property2.AgentId = AgentsId[1];
       


            var propertyList = await _propertyRepository.GetAllListAsync();

            if (propertyList.All(p => p.Id != property1.Id || p.Id != property2.Id))
            {
                if (!propertyList.Any(p => p.Code == property1.Code || p.Code == property2.Code))
                {
                    await _propertyRepository.AddAsync(property1);
                    await _propertyRepository.AddAsync(property2);

                    var properties = await _propertyRepository.GetAllListWithIncludeAsync(new List<string> { "PropertyImages" });


                    var propertyAdded1= properties.FirstOrDefault(p=>p.Code==property1.Code);
                    propertyAdded1.PropertyImages.Add(new PropertyImage
                    { 
                        ImageUrl= "https://sanmarrealestate.com/wp-content/uploads/2022/07/Apartamento-en-el-Paseo-del-Arroyo-2023.jpg",
                        Order = 1,
                        PropertyId = propertyAdded1.Id,
                        
                    });
                    await _propertyRepository.UpdateAsync(propertyAdded1, propertyAdded1.Id );


                    var propertyAdded2 = properties.FirstOrDefault(p => p.Code == property2.Code);
                    propertyAdded2.PropertyImages.Add(new PropertyImage
                    {
                        ImageUrl = "https://resources.diariolibre.com/images/binrepository/etudb9qxcayryzz_15747636_20210330214357-focus-0-0-375-240.jpg",
                        Order = 1,
                        PropertyId = propertyAdded2.Id,

                    });
                    await _propertyRepository.UpdateAsync(propertyAdded2, propertyAdded2.Id);



                }
            }

        }
    }
}
