

using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.User;


namespace RealEstateApp.Core.Application.ViewModels.Chat
{
    public class ChatVm
    {
        public int Id { get; set; }

        //Relacion N-1 con Property
        public int PropertyId { get; set; }
        public PropertyVm Property { get; set; }

        //Relacion N-1 con Cliente[User]
        public string ClientId { get; set; }
        public UserVm Client { get; set; }

        //Relacion N-1 con Agente[User]
        public string AgentId { get; set; }
        public UserVm Agent { get; set; }

        //Relacion 1-N con Message
        public ICollection<MessageVm> Messages { get; set; }
    }
}
