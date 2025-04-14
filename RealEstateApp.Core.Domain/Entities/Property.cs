

namespace RealEstateApp.Core.Domain.Entities
{
    public class Property
    {
        
        public int Id { get; set; }
        public string Code {  get; set; }//Id de 6 digitos unico
        public decimal Price {  get; set; } 

        public int AmountOfRoom {  get; set; }
        public int AmountOfBathroom { get; set; }

        public double PropertySize {  get; set; } //En metros

        public bool State {  get; set; } //Disponible/Vendida

        public string Description {  get; set; }

        //Relacion N-1 con Agentes(User)
        public string AgentId {  get; set; }
        public User Agent { get; set; }


        //Relacion N-1 con Tipo de propiedad
        public int PropertyTypeId {  get; set; }
        public PropertyType PropertyType { get; set; }

        //Relacion N-1 con Tipo de venta
        public int SalesTypeId {  get; set; }
        public SalesType SalesType { get; set; }


        //Relacion 1-N con PropertImage
        public ICollection<PropertyImage> PropertyImages { get; set; }

        //Relacion 1-N con PropertyImprovement
        public ICollection<PropertyImprovement> PropertyImprovements { get; set; }

        //Relacion 1-N con FavoriteProperty
        public ICollection<FavoriteProperty> FavoriteProperties {  get; set; }

        //Relacion 1-N con Offer
        public ICollection<Offer> Offers { get; set; }

        //Relacion 1-N con Chat
        public ICollection<Chat> Chats { get; set; }



    }
}
