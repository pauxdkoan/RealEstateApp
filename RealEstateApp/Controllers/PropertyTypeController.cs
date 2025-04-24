using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyType;

namespace RealEstateApp.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class PropertyTypeController : Controller
    {
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IMapper _mapper;

        public PropertyTypeController(IPropertyTypeService propertyTypeService, IMapper mapper)
        {
            _propertyTypeService = propertyTypeService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var propertyTypes = await _propertyTypeService.GetAllListViewModel();
            return View(propertyTypes);
        }

        public IActionResult Create()
        {
            return View("SavePropertyType", new SavePropertyTypeVm());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePropertyTypeVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SavePropertyType", vm);
            }

            await _propertyTypeService.Add(vm);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var propertyType = await _propertyTypeService.GetByIdSaveViewModel(id);
            return View("SavePropertyType", propertyType);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePropertyTypeVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SavePropertyType", vm);
            }

            await _propertyTypeService.Update(vm, (int)vm.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var propertyType = await _propertyTypeService.GetByIdViewModel(id);
            return View(propertyType);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _propertyTypeService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

