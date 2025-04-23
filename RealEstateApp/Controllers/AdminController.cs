
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Application.ViewModels.User.Admin;


namespace RealEstateApp.Controllers
{
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


        public IActionResult TogleState(int id) 
        {
            return RedirectToAction("AdminMaintenance");
        }



    }
}
