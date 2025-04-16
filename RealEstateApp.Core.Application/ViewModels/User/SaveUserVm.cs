

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModels.User
{
    public class SaveUserVm
    {

        public List<string>? RolList { get; set; } //Cliente - Agente
        public bool HasError {  get; set; }
        public string? Error { get; set; }
        public string? Photo {  get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }

        [Range(1, int.MaxValue, ErrorMessage ="Debe indicar el rol del usuario")]
        public int Rol { get; set; } //Cliente=1 | Agente=2

        [Required(ErrorMessage ="Debe indicar el nombre del usuario")]
        public string FirstName {  get; set; }

        [Required(ErrorMessage = "Debe indicar el apellido del usuario")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe indicar el nombre de usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Debe indicar el correo de usuario")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe indicar el telefono del usuario")]
        public string Phone {  get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [DataType(DataType.Password)]

        /*Falta ponerle q la contrasena debe tener un tamanio minimo
         pa q no explote identity ya q por default identity el tamano 
         minimo es de 6, con masyuscula y un caracter especial.
         [OJO se le puede quitar eso a identity]*/
        public string Password {  get; set; }

        [Required(ErrorMessage = "Debe confirmar su contraseña")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        public string ConfirmPassword {  get; set; }
        
    }
}
