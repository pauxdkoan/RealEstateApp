using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Filters
{
    public class FilterByValuesViewModel
    {
        public string PropertyType { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
    }
}
