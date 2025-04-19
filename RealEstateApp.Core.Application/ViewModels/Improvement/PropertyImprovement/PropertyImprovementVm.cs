using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.ViewModels.Improvement.PropertyImprovement
{
    public class PropertyImprovementVm
    {

        //Esto es una tabla intermedia de la relacion de: N-N de Property e Improvement

        public int Id { get; set; }

        //Relacion N-1 con Property 
        public int PropertyId { get; set; }
        public PropertyVm Property { get; set; }

        //Relacion N-1 con Improvement
        public int ImprovementId { get; set; }
        public ImprovementVm Improvement { get; set; }


    }
}
