using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.ViewModels.Filters
{
    public class HomeFilterViewModel
    {
        public FilterCodeViewModel FilterCode { get; set; }
        public FilterByValuesViewModel FilterValues { get; set; }
        public HomeFilterViewModel()
        {
            FilterValues = new FilterByValuesViewModel(); 
        }
    }
}
