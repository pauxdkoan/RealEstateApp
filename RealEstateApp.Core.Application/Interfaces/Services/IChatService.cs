
using RealEstateApp.Core.Application.ViewModels.Chat;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IChatService : IGenericService<SaveChatViewModel,ChatVm, Chat, int>
    {
        Task<ChatVm> AddVm(SaveChatViewModel vm);//Para obtener un VM del chat creado
    }
}

