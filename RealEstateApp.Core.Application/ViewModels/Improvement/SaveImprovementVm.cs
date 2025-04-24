

using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModels.Improvement
{
    public class SaveImprovementVm
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe ingresar la descripción")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        public bool HasError { get; set; } = false;
        public string Error { get; set; } = "";
    }
}
