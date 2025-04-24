using RealEstateApp.Core.Application.ViewModels.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.User.Client
{
    public class ClientVm
    {
        public string Id {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Email {  get; set; }
        public string Phone {  get; set; }
        public string Photo {  get; set; }
        public string Rol {  get; set; }
        public bool IsActive {  get; set; }


        //Propiedades Actuales del sistema -> Home
        public List<PropertyVm>? CurrentProperties { get; set; }

        //Propiedades favoritas:
        public List<PropertyVm>? FavoriteProperties { get; set; }

    }
}
