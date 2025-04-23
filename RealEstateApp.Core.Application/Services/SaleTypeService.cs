

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.SalesType;
using RealEstateApp.Core.Domain.Entities;


namespace RealEstateApp.Core.Application.Services
{
    public class SaleTypeService : GenericService<SaveSalesTypeVm, SalesTypeVm, SalesType, int>,ISaleTypeService
    {
        
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IOfferRepository _offerRepository;
        private readonly ISalesTypeRepository _salesTypeRepository;

        public SaleTypeService( IMapper mapper, IUserRepository userRepository, 
            IPropertyRepository propertyRepository, IOfferRepository offerRepository, ISalesTypeRepository salesTypeRepository) :base(salesTypeRepository,mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _propertyRepository = propertyRepository;
            _offerRepository = offerRepository;
            _salesTypeRepository = salesTypeRepository;
        }

       
     
       
     

    }
}
