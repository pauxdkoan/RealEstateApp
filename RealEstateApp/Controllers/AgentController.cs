using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyImage;
using RealEstateApp.Core.Application.ViewModels.Agent;
using RealStateApp.Core.Application.ViewModels.Agent;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Application.Services;
using RealEstateApp.Core.Application.ViewModels.Chat;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RealEstateApp.Controllers
{
    public class AgentController : Controller
    {
        private readonly IAgentService _agentService;
        private readonly IPropertyService _propertyService;
        private readonly ISaleTypeService _saleTypeService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IImprovementService _improvementService;
        private readonly IPropertyImageService _propertyImageService;
        private readonly IUserService _userService;
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AgentController(IAgentService agentService, IPropertyService propertyService,
            ISaleTypeService saleTypeService, IPropertyTypeService propertyTypeService,
            IImprovementService improvementService, IPropertyImageService propertyImageService,
            IUserService userService, IChatService chatService, IMessageService messageService,
            IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _agentService = agentService;
            _propertyService = propertyService;
            _saleTypeService = saleTypeService;
            _propertyTypeService = propertyTypeService;
            _improvementService = improvementService;
            _propertyImageService = propertyImageService;
            _userService = userService;
            _chatService = chatService;
            _messageService = messageService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index(PropertyFilters? filters)
        {
            // Cargar los tipos de propiedad para el ViewBag
            var propertyTypes = await _propertyTypeService.GetAllListViewModel();
            ViewBag.PropertyTypes = propertyTypes
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();

            var agentInSession = HttpContext.Session.Get<AuthenticationResponse>("user");
            var propertyList = await _propertyService.MyProperties(agentInSession.Id, filters);
            return View("Home", propertyList);
        }

        public async Task<IActionResult> PropertyDeatails(int id, string? clientId)
        {
            var propertyDetails = await _propertyService.PropertyDetails(propertyId: id);
            ViewBag.SelectedClientId = clientId;
            return View(propertyDetails);
        }

        public async Task<IActionResult> PropertyMaintenance()
        {
            var agentInSession = HttpContext.Session.Get<AuthenticationResponse>("user");
            var propertyList = await _propertyService.MyProperties(agentInSession.Id);
            return View(propertyList);
        }

        public async Task<IActionResult> CreateProperty()
        {
            SavePropertyVm propertyVm = new();
            propertyVm.SalesTypes = await _saleTypeService.GetAllListViewModel();
            propertyVm.PropertyTypes = await _propertyTypeService.GetAllListViewModel();
            propertyVm.Improvements = await _improvementService.GetAllListViewModel();
            return View(propertyVm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty(SavePropertyVm vm)
        {
            if (!ModelState.IsValid)
            {
                vm.SalesTypes = await _saleTypeService.GetAllListViewModel();
                vm.PropertyTypes = await _propertyTypeService.GetAllListViewModel();
                vm.Improvements = await _improvementService.GetAllListViewModel();
                return View(vm);
            }

            await _propertyService.Add(vm);
            return RedirectToAction("PropertyMaintenance");
        }

        public async Task<IActionResult> EditProperty(int id)
        {
            var property = await _propertyService.GetByIdSaveViewModel(id);
            property.SalesTypes = await _saleTypeService.GetAllListViewModel();
            property.PropertyTypes = await _propertyTypeService.GetAllListViewModel();
            property.Improvements = await _improvementService.GetAllListViewModel();
            property.SelectedImprovements = property.PropertyImprovements?.Select(pi => pi.ImprovementId).ToList();

            var editVm = _mapper.Map<EditPropertyVm>(property);
            return View(editVm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProperty(int propertyId)
        {
            await _propertyService.Delete(propertyId);
            return RedirectToAction("PropertyMaintenance");
        }

        [HttpPost]
        public async Task<IActionResult> EditProperty(EditPropertyVm vm)
        {
            var property = await _propertyService.GetByIdSaveViewModel(vm.Id);
            if (!ModelState.IsValid)
            {
                vm.PropertyImageVms = property.PropertyImageVms;
                vm.SalesTypes = await _saleTypeService.GetAllListViewModel();
                vm.PropertyTypes = await _propertyTypeService.GetAllListViewModel();
                vm.Improvements = await _improvementService.GetAllListViewModel();
                return View(vm);
            }
            if (vm.Files != null && property.PropertyImageVms.Count + vm.Files.Count > 4)
            {
                vm.PropertyImageVms = property.PropertyImageVms;
                vm.SalesTypes = await _saleTypeService.GetAllListViewModel();
                vm.PropertyTypes = await _propertyTypeService.GetAllListViewModel();
                vm.Improvements = await _improvementService.GetAllListViewModel();
                ModelState.AddModelError("imageValidation", "Debe borar una o mas imagenes antes de agregar otras");
                return View(vm);
            }

            var svm = _mapper.Map<SavePropertyVm>(vm);
            await _propertyService.Update(svm, vm.Id);
            return RedirectToAction("PropertyMaintenance");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int imageId, int propertyId)
        {
            await _propertyImageService.Delete(imageId);
            return RedirectToAction("EditProperty", new { id = propertyId });
        }

        public async Task<IActionResult> AgentsList()
        {
            List<AgentViewModel> agents = await _userService.GetAllAgents();
            foreach (var agent in agents)
            {
                List<PropertyVm> properties = await _propertyService.GetByAgent(agent.Id);
                agent.properties = properties;
            }
            return View(agents);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleAgentStatus(string agentId)
        {
            await _userService.UpdateStatusAsync(agentId);
            return RedirectToAction("AgentsList");
        }

        public async Task<IActionResult> Delete(string Id)
        {
            UserVm vm = await _userService.GetByIdViewModel(Id);
            AgentViewModel agent = _mapper.Map<AgentViewModel>(vm);
            return View("DeleteAgent", agent);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteAgent(string Id)
        {
            await _userService.Delete(Id);
            return RedirectToAction("AgentsList");
        }

        public async Task<IActionResult> EditAgent()
        {
            var user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            if (user == null)
                return RedirectToAction("Login", "User");

            var agent = await _userService.GetByIdSaveViewModel(user.Id);

            if (agent == null)
                return NotFound();

            var vm = new EditAgentViewModel
            {
                Nombre = agent.FirstName,
                Apellido = agent.LastName,
                Telefono = agent.Phone,
                FotoUrl = agent.Photo
            };

            return View("MyProfile", vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditAgent(EditAgentViewModel vm)
        {
            var user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            if (user == null)
                return RedirectToAction("Login", "User");

            if (!ModelState.IsValid)
            {
                return View("MyProfile", vm);
            }

            await _userService.UpdateUserAsync(new()
            {
                Id = user.Id,
                FirstName = vm.Nombre,
                LastName = vm.Apellido,
                Phone = vm.Telefono,
                Photo = vm.FotoUrl,
            });

            return RedirectToAction("Index", "Agent");
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message, int chatId, int propertyId, string clientId)
        {
            if (chatId == 0)
            {
                PropertyVm property = await _propertyService.GetByIdViewModel(propertyId);

                SaveChatViewModel chat = new SaveChatViewModel
                {
                    PropertyId = propertyId,
                    ClientId = clientId,
                    AgentId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
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

        [HttpPost]
        public async Task<IActionResult> UpdateOfferStatus(int offerId, string newStatus, int propertyId)
        {
            await _agentService.UpdateOfferStatus(offerId, newStatus);
            return RedirectToAction("PropertyDeatails", new { id = propertyId });
        }

        public async Task<IActionResult> ViewAgents()
        {
            List<AgentViewModel> agents = await _userService.GetAllAgents();
            foreach (var agent in agents)
            {
                List<PropertyVm> properties = await _propertyService.GetByAgent(agent.Id);
                agent.properties = properties;
            }
            return View(agents);
        }
    }
}
