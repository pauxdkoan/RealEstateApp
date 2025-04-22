

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;


namespace RealEstateApp.Core.Application.Services
{
    public class AgentService : IAgentService
    {
        
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IOfferRepository _offerRepository;

        public AgentService( IMapper mapper, IUserRepository userRepository, 
            IPropertyRepository propertyRepository, IOfferRepository offerRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _propertyRepository = propertyRepository;
            _offerRepository = offerRepository;
        }

       
        public async Task UpdateOfferStatus(int offerId, string newStatus) 
        {
            var offer= await _offerRepository.GetByIdAsync(offerId);
            offer.State= newStatus;
            await _offerRepository.UpdateAsync(offer, offerId);
            if (newStatus == OfferState.Aceptada.ToString()) 
            {
                var property= await _propertyRepository.GetByIdAsync(offer.PropertyId);
                property.State = false;//vendida
                await _propertyRepository.UpdateAsync(property, property.Id);
            }

        }

       




    }
}
