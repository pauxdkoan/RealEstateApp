using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.Offer
{
    public class OfferVm
    {
        public int Id { get; set; }
        public string State { get; set; } //pendiente, rechazada, aceptada
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        //Relacion N-1 con Property
        public int PropertyId { get; set; }
        public PropertyVm Property { get; set; }

        //Relacion N-1 con Cliente(User[Rol='Cliente'])
        public string ClientId { get; set; }
        public UserVm Cliente { get; set; } 
    }
}
