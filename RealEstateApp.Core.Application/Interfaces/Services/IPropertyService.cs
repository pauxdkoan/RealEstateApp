

using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyService : IGenericService<SavePropertyVm,PropertyVm, Property, int>
    {
        Task<PropertyVm> PropertyDetails(int propertyId);
        Task<List<PropertyVm>> MyFavoritesProperties(string clientId);
        Task<List<PropertyVm>> MyProperties(string agentId);
    }
}
