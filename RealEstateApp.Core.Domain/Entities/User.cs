
namespace RealEstateApp.Core.Domain.Entities
{
    public class User
    {

        /* NOTA: Esto es practicamente una tabla referenciar de la tabla de ApplicationUser(identity) 
           para poder utilizar los navigation y no joder tanto.... identity maneja el registro del 
           usuario y las : (contrasenia, roles, etc).
         */
        public string Id {  get; set; } 
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName {  get; set; } //Agregue esto
        public string? Phone {  get; set; } //Agregue esto
        public string? Photo { get; set; } //No todos los usuarios se le solicita foto, agregue el nulleable
        public string Rol {  get; set; }
        public bool IsActive { get; set; }

        /*Esto lo puse nulleable por q 
         *solo los desarrolladores y los 
         *admin lo usan*/
        public string? IdentityCard {  get; set; } //Cedula

        //Relacion 1-N [Agentes] con Property 
        public ICollection<Property> Properties { get; set; }

        //Relacion 1-N con Offers
        public ICollection<Offer> Offers { get; set; }

        //Relacion 1-N con FavoriteProperty
        public ICollection<FavoriteProperty> FavoriteProperties {  get; set; }

        //Relacion 1-N [Cliente] con Chat
        public ICollection<Chat> ClientChats { get; set; }

        //Relacion 1-N [Agent] con Chat
        public ICollection<Chat> AgentChats { get; set; }

        //Relacion 1-N [Cliente/Agente] con Message
        public ICollection<Message> Messages { get; set; }





    }
}
