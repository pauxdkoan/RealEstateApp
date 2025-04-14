
namespace RealEstateApp.Core.Domain.Entities
{
    public class Improvement
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Relacion 1-N con PropertyImprovements
        public ICollection<PropertyImprovement> PropertyImprovements { get; set; }
    }
}
