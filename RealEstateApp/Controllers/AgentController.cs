
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User.Client;


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

        public async Task<IActionResult> PropertyDeatails(int id)
        {
            
            var propertyDetails = await _propertyService.PropertyDetails(propertyId: id);
            return View(propertyDetails);

        }
    }
}
