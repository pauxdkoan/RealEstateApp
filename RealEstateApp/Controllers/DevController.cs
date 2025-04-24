using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User.Developer;
using RealEstateApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;

namespace RealEstateApp.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class DevController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public DevController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<DeveloperVm> developers = await _userService.GetAllDevelopers();
            return View(developers);
        }

        public IActionResult CreateDeveloper()
        {
            return View("CreateDeveloper", new SaveDeveloperVm());
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeveloper(SaveDeveloperVm vm)
        {
            if (!ModelState.IsValid)
                return View("CreateDeveloper", vm);

            var origin = Request.Headers["origin"];
            var user = _mapper.Map<SaveUserVm>(vm);
            user.Rol = (int)Roles.Desarrollador;

            var response = await _userService.RegisterAsync(user, origin);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View("CreateDeveloper", vm);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditDeveloper(string id)
        {
            var users = await _userService.GetAllListViewModel();
            var user = users.Find(u => u.Id == id);
            var vm = _mapper.Map<EditDeveloperVm>(user);
            return View("EditDeveloper", vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditDeveloper(EditDeveloperVm vm)
        {
            if (!ModelState.IsValid)
                return View("EditDeveloper", vm);

            var user = _mapper.Map<SaveUserVm>(vm);
            user.Rol = (int)Roles.Desarrollador;

            var response = await _userService.UpdateUserAsync(user);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View("EditDeveloper", vm);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> TogleState(string id)
        {
            await _userService.UpdateStatusAsync(id);
            return RedirectToAction("Index");
        }
    }
}

