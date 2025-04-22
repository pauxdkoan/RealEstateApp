

using RealStateApp.Core.Application.ViewModels.Agent;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAgentService
    {
        Task UpdateOfferStatus(int offerId, string newStatus);
    }

}
