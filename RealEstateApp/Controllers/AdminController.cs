
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Application.ViewModels.User.Admin;


namespace RealEstateApp.Controllers
{
    [Authorize(Roles= "Administrador")]
    public class AdminController : Controller
    {

        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AdminController(IAdminService adminService, IUserService userService, IMapper mapper)
        {
            _adminService = adminService;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var vm= await _adminService.GetDataForDashBoard();
            return View(vm);
        }

        public async Task<IActionResult> AdminMaintenance() 
        {
            var users= await _userService.GetAllListViewModel();
            return View(users);   
        }

        public IActionResult CreateAdmin()
        {
           return View("SaveAdmin", new SaveAdminVm());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin(SaveAdminVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveAdmin", vm);
            }

            var origin = Request.Headers["origin"];
            var admin = _mapper.Map<SaveUserVm>(vm);
            admin.Rol= (int) Roles.Administrador;
            var response=await _userService.RegisterAsync(admin, origin);

            if (response.HasError) 
            { 
                vm.HasError=response.HasError;
                vm.Error = response.Error;
                return View("SaveAdmin", vm);
            }

            return RedirectToAction("AdminMaintenance");
            
        }


        public async Task<IActionResult> TogleState(string id)
        {
            await _userService.UpdateStatusAsync(id);
            return RedirectToAction("AdminMaintenance");
        }

        public async Task<IActionResult> EditAdmin(string id)
        {
            var users = await _userService.GetAllListViewModel();
            var user = users.Find(u => u.Id == id);
            var admin=_mapper.Map<EditAdminVm>(user);
          
            return View("EditAdmin", admin);
            
        }

        [HttpPost]
        public async Task<IActionResult> EditAdmin(EditAdminVm vm)
        {
            if (!ModelState.IsValid) 
            { 
                return View("EditAdmin", vm);
            }
            var users = await _userService.GetAllListViewModel();
            var user = users.Find(u => u.Id == vm.Id);
            var admin = _mapper.Map<SaveUserVm>(vm);
            admin.Rol = (int) Roles.Administrador;

            var response= await _userService.UpdateUserAsync(admin);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }

            return RedirectToAction("AdminMaintenance");

        }



    }
}
