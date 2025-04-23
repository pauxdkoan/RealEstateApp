

using RealEstateApp.Core.Application.ViewModels.Property;

namespace RealEstateApp.Core.Application.ViewModels.User.Admin
{
    public class AdminVm
    {
        public ICollection<PropertyVm> Properties { get; set; }//Listado de propiedades
        public ICollection<UserVm> Agents { get; set; } //Listado de agentes
        public ICollection<UserVm> Clients { get; set; } //Listado de cliente
        public ICollection<UserVm> Developers { get; set; } //Listado de desarrolladores


    }
}
