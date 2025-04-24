using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.SalesType;

namespace RealEstateApp.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class SalesTypeController : Controller
    {
        private readonly ISaleTypeService _salesTypeService;
        private readonly IMapper _mapper;

        public SalesTypeController(ISaleTypeService salesTypeService, IMapper mapper)
        {
            _salesTypeService = salesTypeService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var salesTypes = await _salesTypeService.GetAllListViewModel();

            var viewModel = salesTypes.Select(x => new SalesTypeVm
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Properties = x.Properties ?? new List<PropertyVm>()
            }).ToList();

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View("Save", new SaveSalesTypeVm());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveSalesTypeVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }

            await _salesTypeService.Add(vm);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var salesType = await _salesTypeService.GetByIdSaveViewModel(id);
            return View("Save", salesType);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveSalesTypeVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }

            await _salesTypeService.Update(vm, (int)vm.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var salesType = await _salesTypeService.GetByIdViewModel(id);
            return View(salesType);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _salesTypeService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

