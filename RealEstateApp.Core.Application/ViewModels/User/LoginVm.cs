

using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModels.User
{
    public class LoginVm
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }

        [Required(ErrorMessage ="Debe ingresar su correo o nombre de usuario ")]

        public string EmailOrUserName {  get; set; }

        [Required(ErrorMessage = "Debe ingresar su correo o nombre de usuario ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
