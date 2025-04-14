
namespace RealEstateApp.Core.Domain.Entities
{
    public class PropertyImprovement
    {
        //Esto es una tabla intermedia de la relacion de: N-N de Property e Improvement
        public int Id {  get; set; }
        
        //Relacion N-1 con Property 
        public int PropertyId {  get; set; }
        public Property Property { get; set; }

        //Relacion N-1 con Improvement
        public int ImprovementId {  get; set; }
        public Improvement Improvement { get; set; }
         
    }
}
