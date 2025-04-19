
using RealEstateApp.Core.Application.ViewModels.Improvement.PropertyImprovement;

namespace RealEstateApp.Core.Application.ViewModels.Improvement
{
    public class ImprovementVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Relacion 1-N con PropertyImprovements
        public ICollection<PropertyImprovementVm> PropertyImprovements { get; set; }
    }
}
