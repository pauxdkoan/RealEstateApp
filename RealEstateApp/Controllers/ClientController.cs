
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Middlewares;

namespace RealEstateApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IPropertyService _propertyService;
        
        public ClientController(IClientService clientService, IPropertyService propertyService)
        {
            _clientService = clientService;
            _propertyService = propertyService;
        }

        public async Task<ActionResult> Index() 
        {
            var propertyList=await _propertyService.GetAllListViewModel();
            return View(propertyList);
        }

        public async Task<ActionResult> ToggleFavoriteProperty(int id, string clientId) 
        {
            await _clientService.ToggleFavoriteProperty(id, clientId);
            return RedirectToRoute(new {controller="Client",  action="Index"});
        }




    }
}
