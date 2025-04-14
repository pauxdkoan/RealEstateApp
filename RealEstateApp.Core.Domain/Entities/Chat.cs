

namespace RealEstateApp.Core.Domain.Entities
{
    public class Chat
    {
        public int Id { get; set; }

        //Relacion N-1 con Property
        public int PropertyId {  get; set; }
        public Property Property { get; set; }

        //Relacion N-1 con Cliente[User]
        public string ClientId {  get; set; }
        public User Client { get; set; }

        //Relacion N-1 con Agente[User]
        public string AgentId {  get; set; }
        public User Agent { get; set; }

        //Relacion 1-N con Message
        public ICollection<Message> Messages { get; set; }

    }
}
