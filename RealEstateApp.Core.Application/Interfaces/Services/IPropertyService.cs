

using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IPropertyService : IGenericService<SavePropertyVm,PropertyVm, Property, int>
    {
        Task<PropertyVm> PropertyDetails(int propertyId);
        Task<List<PropertyVm>> MyFavoritesProperties(string clientId);
        Task<List<PropertyVm>> MyFavoritesProperties(string clientId, PropertyFilters? filters);
        Task<List<PropertyVm>> MyProperties(string agentId);
        Task<List<PropertyVm>> MyProperties(string agentId, PropertyFilters? filters);
        Task<PropertyVm> GetByIdViewModel(int id);
        Task<List<PropertyVm>> GetByAgent(string id);
        Task<List<PropertyVm>> GetWithFilters(PropertyFilters? filters);
        Task<List<PropertyVm>> GetByAgentWithFilters(string Id, PropertyFilters? filters);

    }
}
