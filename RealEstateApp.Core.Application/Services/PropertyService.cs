

using AutoMapper;

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
using System.Security.Cryptography.X509Certificates;

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
              "Offers", "Chats", "Chats.Messages"
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
