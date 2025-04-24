
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModels.Property.PropertyType
{
    public class SavePropertyTypeVm
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el Nombre")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe ingresar la Descripcion")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
