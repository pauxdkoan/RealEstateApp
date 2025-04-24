
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Helpers;

using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstateApp.Core.Application.Services;

namespace RealEstateApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ISaleTypeService _saleTypeService;
        private readonly IUserService _userService;
        private readonly IPropertyImageService _propertyImageService;

        public HomeController(IPropertyService propertyService, IPropertyTypeService propertyTypeService,
            ISaleTypeService saleTypeService, IUserService userService, IPropertyImageService propertyImageService)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _saleTypeService = saleTypeService;
            _userService = userService;
            _propertyImageService = propertyImageService;
        }

        public async Task<IActionResult> Index(PropertyFilters filters)
        {
            // Cargar los tipos de propiedad para el ViewBag
            var propertyTypes = await _propertyTypeService.GetAllListViewModel();
            ViewBag.PropertyTypes = propertyTypes
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

            // Obtener propiedades filtradas
            var properties = await _propertyService.GetWithFilters(filters);

            return View(properties);
        }

        public async Task<IActionResult> Details(int id)
        {
            var properties = await _propertyService.GetAllListViewModel();

            var property = properties.FirstOrDefault(p => p.Id == id);
            if (property == null)
            {
                return NotFound();
            }

            return View("PropertyDetails", property);
        }

        public async Task<IActionResult> ViewAgents(string? search)
        {
            var agents = await _userService.GetAllAgents();

            if (!string.IsNullOrEmpty(search))
            {
                agents = agents
                    .Where(a => a.FullName.ToLower().Contains(search.ToLower()))
                    .ToList();
            }

            return View(agents.OrderBy(a => a.FullName).ToList());
        }

        public async Task<IActionResult> AgentsProperties(string Id, PropertyFilters? filters)
        {
            List<PropertyVm> properties = await _propertyService.GetByAgentWithFilters(Id, filters);

            return View(properties);
        }
    }
}

