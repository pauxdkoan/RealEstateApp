using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Improvement;
using AutoMapper;
using RealEstateApp.Core.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace RealEstateApp.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ImprovementController : Controller
    {
        private readonly IImprovementService _improvementService;
        private readonly IMapper _mapper;

        public ImprovementController(IImprovementService improvementService, IMapper mapper)
        {
            _improvementService = improvementService;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            List<ImprovementVm> improvements = await _improvementService.GetAllListViewModel();
            return View(improvements);
        }

        public IActionResult Create()
        {
            return View("Save", new SaveImprovementVm());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveImprovementVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }

            var result = await _improvementService.Add(vm);

            if (result.HasError)
            {
                vm.HasError = result.HasError;
                vm.Error = result.Error;
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var improvement = await _improvementService.GetByIdSaveViewModel(id);
            if (improvement == null)
            {
                return NotFound();
            }
            return View("Save", improvement);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveImprovementVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }

            await _improvementService.Update(vm, (int)vm.Id);


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var improvement = await _improvementService.GetByIdViewModel(id);
            return View(improvement);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _improvementService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}


