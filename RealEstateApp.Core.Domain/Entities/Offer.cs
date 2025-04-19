

namespace RealEstateApp.Core.Domain.Entities
{
    public class Offer
    {
        public int Id {  get; set; }
        public string State {  get; set; } //pendiente, rechazada, aceptada
        public DateTime Date {  get; set; }
        public decimal Amount {  get; set; }

        //Relacion N-1 con Property
        public int PropertyId {  get; set; }
        public Property Property { get; set; }

        //Relacion N-1 con Cliente(User[Rol='Cliente'])
        public string ClientId {  get; set; }
        public User Cliente { get; set; } 



    }
}
