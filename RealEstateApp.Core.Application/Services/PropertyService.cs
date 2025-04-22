

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Chat;
using RealEstateApp.Core.Application.ViewModels.Improvement.PropertyImprovement;
using RealEstateApp.Core.Application.ViewModels.Offer;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.Property.FavoriteProperty;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyImage;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyType;
using RealEstateApp.Core.Application.ViewModels.SalesType;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Domain.Entities;



namespace RealEstateApp.Core.Application.Services
{
    public class PropertyService : GenericService<SavePropertyVm, PropertyVm, Property, int>,IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        private readonly IFavoritePropertyRepository _favoritePropertyRepository;
        public PropertyService(IPropertyRepository propertyRepository, IMapper mapper, IFavoritePropertyRepository favoritePropertyRepository) : base(propertyRepository, mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
            _favoritePropertyRepository = favoritePropertyRepository;
        }
        

        public async override Task<List<PropertyVm>> GetAllListViewModel()
        {
            var propertyList=  await _propertyRepository.GetAllListWithIncludeAsync(new List<string> 
            { "PropertyType", "PropertyImages", "SalesType", "FavoriteProperties", 
              "PropertyImprovements", "PropertyImprovements.Improvement", "Agent",
              "Offers","Offers.Cliente", "Chats", "Chats.Messages"
            });
            var propertyListVm = propertyList.Select(p => new PropertyVm()
            {
                Id = p.Id,
                Code = p.Code,
                Price = p.Price,
                State = p.State,
                AmountOfBathroom = p.AmountOfBathroom,
                AmountOfRoom = p.AmountOfRoom,
                PropertySize = p.PropertySize,
                PropertyTypeId = p.PropertyTypeId,
                SalesTypeId = p.SalesTypeId,
                PropertyImageVms = _mapper.Map<List<PropertyImageVm>>(p.PropertyImages),
                PropertyTypeVm = _mapper.Map<PropertyTypeVm>(p.PropertyType),
                SalesTypeVm = _mapper.Map<SalesTypeVm>(p.SalesType),
                FavoritePropertyVms = _mapper.Map<List<FavoritePropertyVm>>(p.FavoriteProperties),

                //Propiedades agregadas para el detalle de propiedades
                PropertyImprovements= _mapper.Map<List<PropertyImprovementVm>>(p.PropertyImprovements),
                AgentId = p.AgentId,
                Agent=_mapper.Map<UserVm>(p.Agent),
                Description = p.Description,

                //Propiedades agregadas para ofertas y el chat de cliente
                Offers= _mapper.Map<List<OfferVm>>(p.Offers),
                Chats = _mapper.Map<List<ChatVm>>(p.Chats),


            }).ToList();
            
            return propertyListVm;

        }

        public async override Task<SavePropertyVm> GetByIdSaveViewModel(int id)
        {
            var properties = await GetAllListViewModel();
            var property= properties.Find(p=>p.Id==id);

            return _mapper.Map<SavePropertyVm>(property);

        }

        public async override Task<SavePropertyVm> Add(SavePropertyVm vm)
        {

            vm.Code= await GenerateUniqueCode();
            int order=1;
            
            foreach (var images in vm.Files) 
            {
                vm.PropertyImageVms.Add(new PropertyImageVm 
                { 
                    ImageUrl=UploadFileHelper.UploadFile(null, images,vm.Code,"Propiedad"),
                    PropertyId=vm.Id,
                    Order= order++
                });
                
            }


            var property =_mapper.Map<Property>(vm);
            property.State = true;
            property.PropertyImprovements = new List<PropertyImprovement>();

            foreach (var improvementId in vm.SelectedImprovements)
            {

                property.PropertyImprovements.Add(new PropertyImprovement
                {

                    ImprovementId = improvementId,
                  


                });

            }

            property = await _propertyRepository.AddAsync(property);

            
            return _mapper.Map<SavePropertyVm>(property);
        }

        public async override Task Update(SavePropertyVm vm, int id)
        {
            var properties= await _propertyRepository.GetAllListWithIncludeAsync(new List<string> {"PropertyImprovements",
            "PropertyImages"  });
            var property = properties.Find(p => p.Id == id);

            // Actualizar campos básicos
            property.Price = vm.Price;
            property.Description = vm.Description;
            property.PropertySize = vm.PropertySize;
            property.AmountOfRoom = vm.AmountOfRoom;
            property.AmountOfBathroom = vm.AmountOfBathroom;
            property.PropertyTypeId = vm.PropertyTypeId;
            property.SalesTypeId = vm.SalesTypeId;

            // Manejar mejoras
            var existingImprovements = property.PropertyImprovements.ToList();
            var newImprovements = vm.SelectedImprovements?.Except(existingImprovements.Select(i => i.ImprovementId)) ?? new List<int>();
            var removedImprovements = existingImprovements.Where(i => !vm.SelectedImprovements?.Contains(i.ImprovementId) ?? true);

            foreach (var imp in removedImprovements)
            {
                property.PropertyImprovements.Remove(imp);
            }

            foreach (var improvementId in newImprovements)
            {
                property.PropertyImprovements.Add(new PropertyImprovement
                {
                    ImprovementId = improvementId,
                    PropertyId = property.Id
                });
            }

            // Manejar imágenes nuevas
            if (vm.Files != null && vm.Files.Any())
            {
                var existingImagesCount = property.PropertyImages.Count;
                var remainingSlots = 4 - existingImagesCount;
                var filesToUpload = vm.Files.Take(remainingSlots);

                foreach (var file in filesToUpload)
                {
                    var imageUrl = UploadFileHelper.UploadFile(null, file, property.Code, "Propiedad");
                    property.PropertyImages.Add(new PropertyImage
                    {
                        ImageUrl = imageUrl,
                        PropertyId = property.Id,
                        Order = existingImagesCount + 1
                    });
                    existingImagesCount++;
                }
            }

            await _propertyRepository.UpdateAsync(property, id);
        }

        private async Task<string> GenerateUniqueCode() 
        {
            string code;
            bool codeExists;
            Random random = new Random();

            do
            {
                int number = random.Next(0, 1000000);
                code=number.ToString("D6");
                codeExists= await _propertyRepository.GetAllQuery().AnyAsync(p=>p.Code==code);
            }
            while (codeExists);

            return code;

        }

        //Detalles de una propiedad
        public async Task<PropertyVm> PropertyDetails(int propertyId) 
        {
            var propertyList = await GetAllListViewModel();
            var propertyDetails = propertyList.Find(p=>p.Id==propertyId);
            return propertyDetails;
        }

        //Propiedades favoritas de un usuario (cliente)
        public async Task<List<PropertyVm>> MyFavoritesProperties(string clientId)
        {
           
            //Obtengo las propiedades favoritas del cliente
            var favoriteProperties = await _favoritePropertyRepository.GetAllListAsync(); 
            var myProperties= favoriteProperties.Where(fp=>fp.ClientId==clientId).ToList();
            

            //Almaceno los id de las propiedades favoritas del cliente
            List<int> propertiesId =new();
            myProperties.ForEach(p=>propertiesId.Add(p.PropertyId));

            //Luego obtengo las propiedades con sus includes, luego filtro las propiedades q tengan los id almacenados 
            var propertyList = await GetAllListViewModel();
            var MyPropertiesFavorite = propertyList.Where(p=>propertiesId.Contains(p.Id)).ToList();

            return MyPropertiesFavorite;
        }

        //Propiedades de un usuario (agente)
        public async Task<List<PropertyVm>> MyProperties(string agentId) 
        {
            var properties = await GetAllListViewModel();
            properties=properties.Where(p=>p.AgentId==agentId).ToList();

            return properties;

        
        }


       


    }
}
