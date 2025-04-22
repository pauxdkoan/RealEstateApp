
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Helpers;


namespace RealEstateApp.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            var user= HttpContext.Session.Get<AuthenticationResponse>("user");

          
            if (user != null)
            {
                //Nota: lo puse sin enums por q me daba problema 
                switch (user.Rol) 
                {
                    case "Cliente":
                        return RedirectToRoute(new {controller="Client", action="Index" });
   
                    case "Agente":
                        return RedirectToRoute(new { controller = "Agent", action = "Index" });
                }



            }

            return View();
        }


    }
}
