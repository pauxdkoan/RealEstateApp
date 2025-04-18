

using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
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

        public ClientService( IMapper mapper, IUserRepository userRepository, 
            IFavoritePropertyRepository favoritePropertyRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _favoritePropertyRepository = favoritePropertyRepository;
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
    }
}
