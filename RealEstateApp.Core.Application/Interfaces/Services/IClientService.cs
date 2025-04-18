

using RealEstateApp.Core.Application.ViewModels.Property.FavoriteProperty;


namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IClientService 
    {
        Task ToggleFavoriteProperty(int propertyId, string clientId);
    }
}
