

using RealEstateApp.Core.Application.ViewModels.User;

namespace RealEstateApp.Core.Application.ViewModels.Chat
{
    public class MessageVm
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; } //Para organizar los mensajes de acuerdo a su fecha y hora

        //Relacion N-1 con Chat
        public int ChatId { get; set; }
        public ChatVm Chat { get; set; }

        //Relacion N-1 con User
        public string SenderId { get; set; }
        public UserVm Sender { get; set; }
    }
}
