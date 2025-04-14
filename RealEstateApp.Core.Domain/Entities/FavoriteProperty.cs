

namespace RealEstateApp.Core.Domain.Entities
{
    public class FavoriteProperty
    {
        //Esto es una tabla intermedia de la relacion: N-M Property y Cliente(User)
        public int Id { get; set; }
        
        //Relacion N-1 con User
        public string ClientId {  get; set; }
        public User Client { get; set; }

        //Relacion N-1 con Property
        public int PropertyId {  get; set; }
        public Property Property {  get; set; }

    }
}
