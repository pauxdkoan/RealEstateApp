namespace RealEstateApp.Core.Application.ViewModels.Property.PropertyImage
{
    public class PropertyImageVm
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int Order { get; set; } //Esto es para saber donde colocar la imagen en el frontend

        //Relacion 1-N con Property
        public int PropertyId { get; set; }
        public PropertyVm Property { get; set; }
    }
}
