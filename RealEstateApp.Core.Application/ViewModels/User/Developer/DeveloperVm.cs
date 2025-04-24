
using RealEstateApp.Core.Application.Enums;

namespace RealEstateApp.Core.Application.ViewModels.User.Developer
{
    public class DeveloperVm
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdentityCard { get; set; }

        public readonly string Rol = Roles.Desarrollador.ToString(); //Readonly para que no pueda ser cambiado

        public bool IsActive { get; set; } = true;
    }
}
