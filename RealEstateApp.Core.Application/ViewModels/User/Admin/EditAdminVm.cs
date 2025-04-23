

using RealEstateApp.Core.Application.Enums;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModels.User.Admin
{
    public class EditAdminVm
    {
        public string? Id { get; set; }
        [Required(ErrorMessage ="Debe indicar el nombre del administrador")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe indicar el apellido del administrador")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe indicar el correo del administrador")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe indicar la cedula del administrador")]
        public string IdentityCard { get; set; } //Cedula

        [Required(ErrorMessage = "Debe indicar el usuario del administrador")]
        public string UserName { get; set; } //Agregue esto

     
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).+$",
         ErrorMessage = "La contraseña debe incluir al menos una letra mayúscula, una minúscula y un dígito")]
        public string? Password { get; set; }

      
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [DataType(DataType.Password)]
        public string? ConfrimPassword { get; set; }


        public string Rol { get; set; }=Roles.Administrador.ToString();
        public bool IsActive { get; set; } = true;

        public string? Error {  get; set; }
        public bool HasError {  get; set; }
    }
}
