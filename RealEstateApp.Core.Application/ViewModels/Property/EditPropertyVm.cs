

using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.ViewModels.Improvement;
using RealEstateApp.Core.Application.ViewModels.Improvement.PropertyImprovement;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyImage;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyType;
using RealEstateApp.Core.Application.ViewModels.SalesType;
using System.ComponentModel.DataAnnotations;


namespace RealEstateApp.Core.Application.ViewModels.Property
{
    public class EditPropertyVm
    {

        [Required(ErrorMessage ="Debe indincar el precio de la propiedad")]
        [Range(1, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Price {  get; set; } //En peso dominicano

        [Required(ErrorMessage = "Debe indincar la descripción de la propiedad")]
        public string Description {  get; set; }

        [Required(ErrorMessage = "Debe indincar el tamaño de la propiedad")]
        [Range(1, double.MaxValue, ErrorMessage = "El tamaño debe ser mayor a 0")]
        public double PropertySize {  get; set; } //En metros

        [Required(ErrorMessage = "Debe indincar la cantidad de habitaciones de la propiedad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe tener al menos 1 habitación")]
        public int AmountOfRoom {  get; set; }

        [Required(ErrorMessage = "Debe indincar la cantidad de baños de la propiedad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe tener al menos 1 baño")]
        public int AmountOfBathroom { get; set; }

        [Range(0, int.MaxValue, ErrorMessage ="Debe seleccionar un tipo de propiedad")]
        public int PropertyTypeId {  get; set; }
        
        public ICollection<PropertyTypeVm>? PropertyTypes { get; set; } //Select debe venir lleno


        [Range(0, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de venta")]
        public int SalesTypeId { get; set; }

        public ICollection<SalesTypeVm>? SalesTypes { get; set; }//Select debe venir lleno

        [MinLength(1, ErrorMessage = "Debe seleccionar almenos una mejora")]
        public List<int> SelectedImprovements { get; set; }
        public ICollection<ImprovementVm>? Improvements { get; set; } //Debe venir llevo, selec multiple

        public ICollection<PropertyImprovementVm>? PropertyImprovements { get; set; } //debe
        public ICollection<PropertyImageVm>? PropertyImageVms { get; set; } = new List<PropertyImageVm>(); //Lo inicializo por default pa q no explote en la logica

 
        [MaxLength(4, ErrorMessage = "Máximo 4 imágenes permitidas")]
        public List<IFormFile>? Files { get; set; }//1-4 Campos q permita seleccionar de 1 a 4 imagenes

        public string? AgentId {  get; set; }
        public int Id {  get; set; }

        public string? Code {  get; set; }







    }
}
