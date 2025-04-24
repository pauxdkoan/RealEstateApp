
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Services;
using RealEstateApp.Core.Application.ViewModels.Chat;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.User.Client;


namespace RealEstateApp.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IPropertyService _propertyService;
        private readonly IChatService _chatService;
        private readonly IAgentService _agentService;
        private readonly IMessageService _messageService;
        private readonly IPropertyTypeService _propertyTypeService;

        public ClientController(IClientService clientService, IPropertyService propertyService, IChatService chatService, 
            IAgentService agentService, IMessageService messageService, IPropertyTypeService propertyTypeService)
        {
            _clientService = clientService;
            _propertyService = propertyService;
            _chatService = chatService;
            _agentService = agentService;
            _messageService = messageService;
            _propertyTypeService = propertyTypeService;
        }

        public async Task<IActionResult> Index(PropertyFilters filters) 
        {
            // Cargar los tipos de propiedad para el ViewBag
            var propertyTypes = await _propertyTypeService.GetAllListViewModel();
            ViewBag.PropertyTypes = propertyTypes
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

            var propertyList=await _propertyService.GetWithFilters(filters);
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

        public async Task<IActionResult> MyFavoritesProperties(string clientId, PropertyFilters? filters)
        {
            // Cargar los tipos de propiedad para el ViewBag
            var propertyTypes = await _propertyTypeService.GetAllListViewModel();
            ViewBag.PropertyTypes = propertyTypes
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();
            var myPropertyList = await _propertyService.MyFavoritesProperties(clientId, filters);
            return View(myPropertyList);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOffer(int propertyId, string clientId, decimal amount )
        {
            await _clientService.CreateOffer(propertyId, clientId, amount);
            return RedirectToAction("PropertyDeatails", new {id= propertyId });
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message, int chatId, int propertyId)
        {
            if (chatId == 0)
            {
                PropertyVm property = await _propertyService.GetByIdViewModel(propertyId);

                SaveChatViewModel chat = new SaveChatViewModel
                {
                    PropertyId = propertyId,
                    ClientId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
                    AgentId = property.AgentId
                };

                ChatVm chatVm = await _chatService.AddVm(chat);
                chatId = chatVm.Id;

            }

            SaveMessageVm messageVm = new SaveMessageVm
            {
                ChatId = chatId,
                Content = message,
                SenderId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
            };

            await _messageService.Add(messageVm);
            return RedirectToAction("PropertyDeatails", new { id = propertyId });

        }
    }
}
