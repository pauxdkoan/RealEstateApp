

namespace RealEstateApp.Core.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } 
        public DateTime DateTime {  get; set; } //Para organizar los mensajes de acuerdo a su fecha y hora
        
        //Relacion N-1 con Chat
        public int ChatId {  get; set; }
        public Chat Chat {  get; set; }

        //Relacion N-1 con User
        public string SenderId {  get; set; }
        public User Sender {  get; set; }

        
    }
}
