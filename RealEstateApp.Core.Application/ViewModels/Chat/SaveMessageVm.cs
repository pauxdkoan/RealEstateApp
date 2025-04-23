

using RealEstateApp.Core.Application.ViewModels.User;

namespace RealEstateApp.Core.Application.ViewModels.Chat
{
    public class SaveMessageVm
    {
        public string Content { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int ChatId { get; set; }
        public string SenderId { get; set; }
    }
}
