

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
            property1.PropertyTypeId = 1; //Apartamento
            property1.SalesTypeId = 1;
            property1.AgentId = AgentsId[0];


            Property property2 = new();
            property2.Code = "123457";
            property2.Price = 45000;
            property2.AmountOfRoom = 2;
            property2.AmountOfBathroom = 2;
            property2.PropertySize = 250;
            property2.State = true; //Vendidad, Disponible
            property2.Description = "Propiedad Pequenia"; //No se q poner en descripcion
            property2.PropertyTypeId = 2; //Casa
            property2.SalesTypeId = 2;
            property2.AgentId = AgentsId[1];


            Property property3 = new();
            property3.Code = "123458";
            property3.Price = 400000;
            property3.AmountOfRoom = 4;
            property3.AmountOfBathroom = 5;
            property3.PropertySize = 400;
            property3.State = true; //Vendidad, Disponible
            property3.Description = "Propiedad Muy Grande"; //No se q poner en descripcion
            property3.PropertyTypeId = 1; //Apartamento
            property3.SalesTypeId = 1;
            property3.AgentId = AgentsId[2];


            Property property4 = new();
            property4.Code = "123459";
            property4.Price = 47000;
            property4.AmountOfRoom = 2;
            property4.AmountOfBathroom = 1;
            property4.PropertySize = 200;
            property4.State = true; //Vendidad, Disponible
            property4.Description = "Propiedad Pequenia"; //No se q poner en descripcion
            property4.PropertyTypeId = 1; //Apartamento
            property4.SalesTypeId = 2;
            property4.AgentId = AgentsId[0];

            Property property5 = new();
            property5.Code = "1234610";
            property5.Price = 600000;
            property5.AmountOfRoom = 5;
            property5.AmountOfBathroom = 4;
            property5.PropertySize = 500;
            property5.State = true; //Vendidad, Disponible
            property5.Description = "Propiedad de Lujo"; //No se q poner en descripcion
            property5.PropertyTypeId = 2; //Casa
            property5.SalesTypeId = 1;
            property5.AgentId = AgentsId[1];


            Property property6 = new();
            property6.Code = "123461";
            property6.Price = 550000;
            property6.AmountOfRoom = 3;
            property6.AmountOfBathroom = 2;
            property6.PropertySize = 350;
            property6.State = true; //Vendidad, Disponible
            property6.Description = "Propiedad Mediana"; //No se q poner en descripcion
            property6.PropertyTypeId = 1; //Apartamento
            property6.SalesTypeId = 2;
            property6.AgentId = AgentsId[2];


            Property property7 = new();
            property7.Code = "123462";
            property7.Price = 300000;
            property7.AmountOfRoom = 2;
            property7.AmountOfBathroom = 3;
            property7.PropertySize = 600;
            property7.State = true; //Vendidad, Disponible
            property7.Description = "Propiedad Muy Grande"; //No se q poner en descripcion
            property7.PropertyTypeId = 1; //Apartamento
            property7.SalesTypeId = 1;
            property7.AgentId = AgentsId[0];


            Property property8 = new();
            property8.Code = "123463";
            property8.Price = 50000;
            property8.AmountOfRoom = 2;
            property8.AmountOfBathroom = 1;
            property8.PropertySize = 250;
            property8.State = true; //Vendidad, Disponible
            property8.Description = "Propiedad Pequenia"; //No se q poner en descripcion
            property8.PropertyTypeId = 2; //Casa
            property8.SalesTypeId = 2;
            property8.AgentId = AgentsId[1];


            Property property9 = new();
            property9.Code = "123464";
            property9.Price = 40000;
            property9.AmountOfRoom = 2;
            property9.AmountOfBathroom = 1;
            property9.PropertySize = 220;
            property9.State = true; //Vendidad, Disponible
            property9.Description = "Propiedad Pequenia"; //No se q poner en descripcion
            property9.PropertyTypeId = 1; //Apartamento
            property9.SalesTypeId = 1;
            property9.AgentId = AgentsId[2];



            var propertyList = await _propertyRepository.GetAllListAsync();

            if (propertyList.All(p => p.Id != property1.Id || p.Id != property2.Id || p.Id != property3.Id || p.Id != property4.Id || p.Id != property5.Id ||
            p.Id != property6.Id || p.Id != property7.Id || p.Id != property8.Id || p.Id != property9.Id))
            {
                if (!propertyList.Any(p => p.Code == property1.Code || p.Code == property2.Code || p.Code == property3.Code || p.Code == property4.Code || p.Code == property5.Code ||
                p.Code == property6.Code || p.Code == property7.Code || p.Code == property8.Code || p.Code == property9.Code))
                {
                    await _propertyRepository.AddAsync(property1);
                    await _propertyRepository.AddAsync(property2);
                    await _propertyRepository.AddAsync(property3);
                    await _propertyRepository.AddAsync(property4);
                    await _propertyRepository.AddAsync(property5);
                    await _propertyRepository.AddAsync(property6);
                    await _propertyRepository.AddAsync(property7);
                    await _propertyRepository.AddAsync(property8);
                    await _propertyRepository.AddAsync(property9);

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

                    var propertyAdded3 = properties.FirstOrDefault(p => p.Code == property3.Code);
                    propertyAdded3.PropertyImages.Add(new PropertyImage
                    {
                        ImageUrl = "https://www.sgl.do/upload/olimare/properties/53b354612d26628e73986a80e254864e/properties-6332-126137.jpg",
                        Order = 1,
                        PropertyId = propertyAdded3.Id,
                    });
                    await _propertyRepository.UpdateAsync(propertyAdded3, propertyAdded3.Id);

                    var propertyAdded4 = properties.FirstOrDefault(p => p.Code == property4.Code);
                    propertyAdded4.PropertyImages.Add(new PropertyImage
                    {
                        ImageUrl = "https://presidencia.gob.do/sites/default/files/inline-images/WhatsApp%20Image%202023-10-21%20at%204.32.57%20PM%20%283%29.jpeg",
                        Order = 1,
                        PropertyId = propertyAdded4.Id,
                    });
                    await _propertyRepository.UpdateAsync(propertyAdded4, propertyAdded4.Id);

                    var propertyAdded5 = properties.FirstOrDefault(p => p.Code == property5.Code);
                    propertyAdded5.PropertyImages.Add(new PropertyImage
                    {
                        ImageUrl = "https://www.casadecampo.com.do/wp-content/uploads/slider417/6BDRM_OCF_Casa_Alba_Pool_view.jpeg",
                        Order = 1,
                        PropertyId = propertyAdded5.Id,
                    });
                    await _propertyRepository.UpdateAsync(propertyAdded5, propertyAdded5.Id);

                    var propertyAdded6 = properties.FirstOrDefault(p => p.Code == property6.Code);
                    propertyAdded6.PropertyImages.Add(new PropertyImage
                    {
                        ImageUrl = "https://www.inmobiliaria.com.do/wp-content/uploads/2023/03/SECTOR-LOS-TRES-OJOS-1.jpg",
                        Order = 1,
                        PropertyId = propertyAdded6.Id,
                    });
                    await _propertyRepository.UpdateAsync(propertyAdded6, propertyAdded6.Id);

                    var propertyAdded7 = properties.FirstOrDefault(p => p.Code == property7.Code);
                    propertyAdded7.PropertyImages.Add(new PropertyImage
                    {
                        ImageUrl = "https://assets.easybroker.com/property_images/1233821/18351319/EB-DS3821.jpg?version=1567664550",
                        Order = 1,
                        PropertyId = propertyAdded7.Id,
                    });
                    await _propertyRepository.UpdateAsync(propertyAdded7, propertyAdded7.Id);

                    var propertyAdded8 = properties.FirstOrDefault(p => p.Code == property8.Code);
                    propertyAdded8.PropertyImages.Add(new PropertyImage
                    {
                        ImageUrl = "https://www.shutterstock.com/image-photo/modern-singlestory-house-minimalist-garden-600nw-2563059783.jpg",
                        Order = 1,
                        PropertyId = propertyAdded8.Id,
                    });
                    await _propertyRepository.UpdateAsync(propertyAdded8, propertyAdded8.Id);

                    var propertyAdded9 = properties.FirstOrDefault(p => p.Code == property9.Code);
                    propertyAdded9.PropertyImages.Add(new PropertyImage
                    {
                        ImageUrl = "https://d2p0bx8wfdkjkb.cloudfront.net/static/properties/SD1VYND9UU/79P1G35WJP/4WD8UxJZp9/vistas_Exterior_02.jpg",
                        Order = 1,
                        PropertyId = propertyAdded9.Id,
                    });
                    await _propertyRepository.UpdateAsync(propertyAdded9, propertyAdded9.Id);
                }
            }

        }
    }
}
