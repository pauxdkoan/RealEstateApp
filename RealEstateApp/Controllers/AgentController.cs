
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User.Client;
using RealStateApp.Core.Application.ViewModels.Agent;


namespace RealEstateApp.Controllers
{
    public class AgentController : Controller
    {
        private readonly IAgentService _agentService;
        private readonly IPropertyService _propertyService;
        
        public AgentController(IAgentService agentService, IPropertyService propertyService)
        {
            _agentService = agentService;
            _propertyService = propertyService;
        }

        public async Task<IActionResult> Index() 
        {
            var agentInSession = HttpContext.Session.Get<AuthenticationResponse>("user");
            var propertyList=await _propertyService.MyProperties(agentInSession.Id);
            return View(propertyList);
        }

        //[HttpGet]
        //public async Task<IActionResult> MyProfile()
        //{
        //    var userId = User.FindFirst("UserId")?.Value;
        //    var model = await _agentService.GetAgentProfileAsync(userId);
        //    return View(model);
        //}
        //[Authorize(Roles = "Agente")]

        //[HttpPost]
        //public async Task<IActionResult> MyProfile(EditAgentViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    var userId = User.FindFirst("UserId")?.Value;
        //    await _agentService.UpdateAgentProfileAsync(userId, model);

        //    return RedirectToAction("Index", "Home");
        //}
        //[Authorize(Roles = "Agente")]

        public async Task<IActionResult> PropertyDeatails(int id)
        {
            
            var propertyDetails = await _propertyService.PropertyDetails(propertyId: id);
            return View(propertyDetails);

        }

        [HttpPost]
        public  async Task<IActionResult> UpdateOfferStatus(int offerId, string newStatus, int propertyId) 
        { 
           await _agentService.UpdateOfferStatus(offerId, newStatus);
           return RedirectToAction("PropertyDeatails", new { id = propertyId });
        }
    }
}
