

namespace RealEstateApp.Core.Domain.Entities
{
    public class PropertyImage
    {
        public int Id {  get; set; }
        public string ImageUrl {  get; set; }
        public int? Order {  get; set; } //Esto es para saber donde colocar la imagen en el frontend

        //Relacion 1-N con Property
        public int PropertyId {  get; set; }
        public Property Property { get; set; }
    }

}
