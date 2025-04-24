

using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyType;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class PropertyTypeService : GenericService<SavePropertyTypeVm, PropertyTypeVm, PropertyType, int>, IPropertyTypeService
    {
        
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IOfferRepository _offerRepository;
        private readonly IPropertyTypeRepository _propertyTypeRepository;

        public PropertyTypeService( IMapper mapper, IUserRepository userRepository, 
            IPropertyRepository propertyRepository, IOfferRepository offerRepository, IPropertyTypeRepository propertyTypeRepository) :base(propertyTypeRepository, mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _propertyRepository = propertyRepository;
            _offerRepository = offerRepository;
            _propertyTypeRepository = propertyTypeRepository;
        }

        public async Task<PropertyTypeVm> GetByIdViewModel(int id)
        {
            PropertyType proptype = await _propertyTypeRepository.GetByIdAsync(id);
            PropertyTypeVm vm = _mapper.Map<PropertyTypeVm>(proptype);
            return vm;
        }
    }
}
