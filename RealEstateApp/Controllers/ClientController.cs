
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;


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

        public async Task<IActionResult> Index() 
        {
            var propertyList=await _propertyService.GetAllListViewModel();
            return View(propertyList);
        }

        public async Task<IActionResult> ToggleFavoriteProperty(int id, string clientId) 
        {
            await _clientService.ToggleFavoriteProperty(id, clientId);
            return RedirectToRoute(new {controller="Client",  action="Index"});
        }

        public async Task<IActionResult> PropertyDeatails(int id) 
        { 
            var propertyDetails= await _propertyService.PropertyDetails(propertyId:id);
            return View(propertyDetails);
        }



    }
}
