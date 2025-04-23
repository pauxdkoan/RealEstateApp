
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public AdminController(IAdminService adminService, IUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
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
           return View("SaveAdmin", new SaveUserVm());
        }

        [HttpPost]
        public IActionResult CreateAdmin(SaveAdminVm vm)
        {
            return null;
        }



    }
}
