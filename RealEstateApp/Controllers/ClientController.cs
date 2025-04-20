
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User.Client;


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

        public async Task<IActionResult> ToggleFavoriteProperty(int id, string clientId, bool fromMyProperties=false) 
        {
            await _clientService.ToggleFavoriteProperty(id, clientId);
            if (fromMyProperties) 
            {
                return RedirectToRoute(new { controller = "Client", action = "MyProperties" });
            }
            return RedirectToRoute(new {controller="Client",  action="Index"});
        }

        public async Task<IActionResult> PropertyDeatails(int id) 
        { 
            var propertyDetails= await _propertyService.PropertyDetails(propertyId:id);
            return View(propertyDetails);
        }

        public async Task<IActionResult> MyProperties(string clientId)
        {
            var myPropertyList = await _propertyService.MyProperties(clientId);
            return View(myPropertyList);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOffer(int propertyId, string clientId, decimal amount )
        {
            await _clientService.CreateOffer(propertyId, clientId, amount);
            return RedirectToAction("PropertyDeatails", new {id= propertyId });
        }



    }
}
