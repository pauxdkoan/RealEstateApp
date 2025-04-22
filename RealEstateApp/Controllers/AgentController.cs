

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.Property.PropertyImage;



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
        private readonly IMapper _mapper;

        public AgentController(IAgentService agentService, IPropertyService propertyService,
            ISaleTypeService saleTypeService, IPropertyTypeService propertyTypeService, 
            IImprovementService improvementService, IPropertyImageService propertyImageService,
            IMapper mapper
            )
        {
            _agentService = agentService;
            _propertyService = propertyService;
            _saleTypeService = saleTypeService;
            _propertyTypeService = propertyTypeService;
            _improvementService = improvementService;
            _propertyImageService = propertyImageService;
            _mapper = mapper;
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

        [HttpPost]
        public  async Task<IActionResult> UpdateOfferStatus(int offerId, string newStatus, int propertyId) 
        { 
           await _agentService.UpdateOfferStatus(offerId, newStatus);
           return RedirectToAction("PropertyDeatails", new { id = propertyId });
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
            propertyVm.PropertyTypes =  await _propertyTypeService.GetAllListViewModel();
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
            if( vm.Files!=null && property.PropertyImageVms.Count+vm.Files.Count>4) 
            {
                vm.PropertyImageVms = property.PropertyImageVms;
                vm.SalesTypes = await _saleTypeService.GetAllListViewModel();
                vm.PropertyTypes = await _propertyTypeService.GetAllListViewModel();
                vm.Improvements = await _improvementService.GetAllListViewModel();
                ModelState.AddModelError("imageValidation", "Debe borar una o mas imagenes antes de agregar otras");
                return View(vm);
            }

            var svm=_mapper.Map<SavePropertyVm>(vm);
            await _propertyService.Update(svm, vm.Id);
            return RedirectToAction("PropertyMaintenance");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int imageId, int propertyId)
        {
            await _propertyImageService.Delete(imageId);
            return RedirectToAction("EditProperty", new { id = propertyId });
        }

    }
}
