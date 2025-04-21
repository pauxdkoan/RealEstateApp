

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
    public class AgentService : IAgentService
    {
        
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPropertyRepository _propertyRepository;

        public AgentService( IMapper mapper, IUserRepository userRepository, 
            IPropertyRepository propertyRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _propertyRepository = propertyRepository;
        }

       

    

    }
}
