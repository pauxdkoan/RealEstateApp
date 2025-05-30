﻿
using RealEstateApp.Core.Application.ViewModels.Property;

namespace RealEstateApp.Core.Application.ViewModels.SalesType
{
    public class SalesTypeVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Relacion 1-N con Property 
        public ICollection<PropertyVm> Properties { get; set; }
    }
}
