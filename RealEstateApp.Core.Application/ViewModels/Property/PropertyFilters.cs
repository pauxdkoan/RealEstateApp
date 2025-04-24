
using Microsoft.AspNetCore.Routing.Constraints;

namespace RealEstateApp.Core.Application.ViewModels.Property
{
    public class PropertyFilters
    {
        public string? Code { get; set; }
        public List<int>? PropertyTypes { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
    }
}
