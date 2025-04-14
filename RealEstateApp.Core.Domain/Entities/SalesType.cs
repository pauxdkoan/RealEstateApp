

namespace RealEstateApp.Core.Domain.Entities
{
    public class SalesType
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Relacion 1-N con Property 
        public ICollection<Property> Properties { get; set; }
    }
}
