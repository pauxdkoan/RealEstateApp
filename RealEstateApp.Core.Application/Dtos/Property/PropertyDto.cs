
using RealEstateApp.Core.Application.Dtos.Improvements;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Dtos.Property
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Code {  get; set; }
        
        public string PropertyType {  get; set; }
        public decimal Price { get; set; }
        public double PropertySize { get; set; } //En metros
        public int AmountOfRoom { get; set; }
        public int AmountOfBathroom { get; set; }
        public string Description {  get; set; }
        public List<string> ImprovementList { get; set; }
        public string AgentName {  get; set; }
        public string AgentId {  get; set; }

        public string State {  get; set; }//Disponible o vendidad
    }
}
