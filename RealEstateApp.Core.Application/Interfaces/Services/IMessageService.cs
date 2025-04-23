using RealEstateApp.Core.Application.ViewModels.Chat;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IMessageService : IGenericService<SaveMessageVm, MessageVm, Message, int> 
    {
    }
}
