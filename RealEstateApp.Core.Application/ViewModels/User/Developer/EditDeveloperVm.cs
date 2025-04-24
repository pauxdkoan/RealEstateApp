
using RealEstateApp.Core.Application.Enums;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModels.User.Developer
{
    public class EditDeveloperVm
    {
        public string? Id { get; set; }

        [Required(ErrorMessage ="Debe indicar el nombre del desarrollador")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Debe indicar el apellido del desarrollador")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Debe indicar el nombre de usuario del desarrollador")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe indicar el correo electronico del desarrollador")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [DataType(DataType.Text)]
        public string IdentityCard { get; set; }

        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).+$",
         ErrorMessage = "La contraseña debe incluir al menos una letra mayúscula, una minúscula y un dígito")]
        public string? Password { get; set; }


        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [DataType(DataType.Password)]
        public string? ConfrimPassword { get; set; }

        public readonly string Rol = Roles.Desarrollador.ToString();

        public bool IsActive { get; set; } = true;
        public string? Error { get; set; }
        public bool HasError { get; set; }
    }
}
