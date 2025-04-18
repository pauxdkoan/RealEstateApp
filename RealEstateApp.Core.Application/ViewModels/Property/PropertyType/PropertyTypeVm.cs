namespace RealEstateApp.Core.Application.ViewModels.Property.PropertyType
{
    public class PropertyTypeVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Relacion 1-N con Property 
        public ICollection<PropertyVm> Properties { get; set; }
    }
}
