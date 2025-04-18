

using AutoMapper;

using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.Property.FavoriteProperty;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyImage;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyType;
using RealEstateApp.Core.Application.ViewModels.SalesType;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class PropertyService : GenericService<SavePropertyVm, PropertyVm, Property, int>,IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public PropertyService(IPropertyRepository propertyRepository, IMapper mapper) :base(propertyRepository,mapper) 
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async override Task<List<PropertyVm>> GetAllListViewModel()
        {
            var propertyList=  await _propertyRepository.GetAllListWithIncludeAsync(new List<string> { "PropertyType", "PropertyImages", "SalesType", "FavoriteProperties" });
            var propertyListVm = propertyList.Select(p => new PropertyVm()
            {
                Id = p.Id,
                Code = p.Code,
                Price = p.Price,
                AmountOfBathroom = p.AmountOfBathroom,
                AmountOfRoom = p.AmountOfRoom,
                PropertySize = p.PropertySize,
                PropertyTypeId = p.PropertyTypeId,
                SalesTypeId = p.SalesTypeId,
                PropertyImageVms = _mapper.Map<List<PropertyImageVm>>(p.PropertyImages),
                PropertyTypeVm = _mapper.Map<PropertyTypeVm>(p.PropertyType),
                SalesTypeVm = _mapper.Map<SalesTypeVm>(p.SalesType),
                FavoritePropertyVms = _mapper.Map<List<FavoritePropertyVm>>(p.FavoriteProperties)
            }).ToList();
            
            return propertyListVm;

        }
        
    }
}
