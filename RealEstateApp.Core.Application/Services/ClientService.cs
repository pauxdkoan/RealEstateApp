

using AutoMapper;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.Property.FavoriteProperty;
using RealEstateApp.Core.Domain.Entities;
using System.Reflection.Metadata.Ecma335;

namespace RealEstateApp.Core.Application.Services
{
    public class ClientService:IClientService
    {
        
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IFavoritePropertyRepository _favoritePropertyRepository;
        private readonly IPropertyRepository _propertyRepository;

        public ClientService( IMapper mapper, IUserRepository userRepository, 
            IFavoritePropertyRepository favoritePropertyRepository, IPropertyRepository propertyRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _favoritePropertyRepository = favoritePropertyRepository;
            _propertyRepository = propertyRepository;
        }

        public async Task ToggleFavoriteProperty(int propertyId, string clientId)
        {
            var clientList = await _userRepository.GetAllListWithIncludeAsync(new List<string> { "FavoriteProperties" });
            var client= clientList.FirstOrDefault(c=>c.Id==clientId);

            var IsFavorite = client.FavoriteProperties.Any(fp => fp.PropertyId == propertyId);

            if (IsFavorite) 
            { 
                var favoriteProperty = client.FavoriteProperties.FirstOrDefault(fp => fp.Id==propertyId);
                await _favoritePropertyRepository.DeleteAsync(favoriteProperty);
              
            }
            else 
            {
                //Se añade la propiedad a traves del navigation property
                client.FavoriteProperties.Add(new FavoriteProperty { PropertyId = propertyId });
                await _userRepository.UpdateAsync(client, client.Id);
            }

            
            
        }

        public async Task CreateOffer(int propertyId, string clientId, decimal amount)
        {
            var propertyList =  await _propertyRepository.GetAllListWithIncludeAsync(new List<string> { "Offers" });
            var property= propertyList.FirstOrDefault(p=>p.Id==propertyId);

            //Se agrega la oferta a traves del navigation property
            property.Offers.Add(new Offer 
            {
                ClientId=clientId, 
                Amount=amount,
                Date=DateTime.Now,
                PropertyId=property.Id,
                State=OfferState.Pendiente.ToString(),
            });

            //Se atualiza la propiedad

            await _propertyRepository.UpdateAsync(property,propertyId);

        } 
      
    }
}
