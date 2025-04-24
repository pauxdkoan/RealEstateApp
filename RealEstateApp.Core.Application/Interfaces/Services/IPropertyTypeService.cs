

using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyType;
using RealEstateApp.Core.Application.ViewModels.SalesType;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyTypeService : IGenericService<SavePropertyTypeVm,PropertyTypeVm, PropertyType, int>
    {
       Task<PropertyTypeVm> GetByIdViewModel(int id);
    }
}
