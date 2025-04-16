using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Models;

namespace RealEstateApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
