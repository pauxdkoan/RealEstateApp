
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.ViewModels.Chat
{
    public class SaveChatViewModel
    {
            
        public int PropertyId { get; set; }

        public string ClientId { get; set; }

        public string AgentId { get; set; }
    }
}
