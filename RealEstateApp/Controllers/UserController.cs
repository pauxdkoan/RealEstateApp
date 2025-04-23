
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Middlewares;

namespace RealEstateApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginVm());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginVm vm)
        {
            if (!ModelState.IsValid) 
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>( "user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else 
            { 
                vm.HasError = true;
                vm.Error = userVm.Error;
                return View(vm);
            }
        }

        public async Task<IActionResult> LogOut() 
        { 
            await _userService.SignOutAsync();
            HttpContext.Session.Remove(key: "user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> Register()
        {
            SaveUserVm vm = new();
            vm.RolList= await _userService.GetAllRoles();
            return View(vm);
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserVm vm) 
        {
            if (!ModelState.IsValid) 
            {
                vm.RolList = await _userService.GetAllRoles();
                return View(vm);
            }


            var photoPath = UploadFileHelper.UploadFile(vm.Rol, vm.File, vm.Email, null);
            vm.Photo = photoPath;

            var origin = Request.Headers["origin"]; //Esto es para Obtener la url base...
            RegisterResponse response = await _userService.RegisterAsync(vm, origin);
            
            if (response.HasError)
            {
                UploadFileHelper.DeleteFile(photoPath);//Se borra ft

                vm.RolList = await _userService.GetAllRoles();
                vm.HasError=response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }

            return RedirectToRoute(new {controller="User", Action="Index"});
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token) 
        { 
            string response= await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }

    }
}
