
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;


namespace RealEstateApp.Controllers
{
    public class AdminController : Controller
    {

        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var vm= await _adminService.GetDataForDashBoard();
            return View(vm);
        }


    }
}
