using System.Collections.Generic;
using RealStateApp.Core.Application.ViewModels.Filters;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyType;
namespace RealStateApp.Core.Application.ViewModels.View
{
    public class HomeViewModel
    {
        public HomeFilterViewModel Filters { get; set; } 
        public ICollection<PropertyVm> Properties { get; set; } 
        public List<PropertyTypeVm> PropertyTypes { get; set; } 
        public string UserRole { get; set; } 

        public HomeViewModel()
        {
            
            Filters = new HomeFilterViewModel();
            Properties = new List<PropertyVm>();
            PropertyTypes = new List<PropertyTypeVm>();
        }
    }
}
