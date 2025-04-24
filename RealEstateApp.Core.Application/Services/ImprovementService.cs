

using AutoMapper;

using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Improvement;
using RealEstateApp.Core.Application.ViewModels.SalesType;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class ImprovementService : GenericService<SaveImprovementVm, ImprovementVm, Improvement, int>, IImprovementService
    {
        
        private readonly IMapper _mapper;
        private readonly IImprovementRepository _improvementRepository;
     

        public ImprovementService( IMapper mapper, IImprovementRepository improvementRepository) :base(improvementRepository, mapper)
        {
            _mapper = mapper;
            _improvementRepository = improvementRepository;
        }

        public async Task<ImprovementVm> GetByIdViewModel(int id)
        {
            Improvement improvement = await _improvementRepository.GetByIdAsync(id);
            ImprovementVm vm = _mapper.Map<ImprovementVm>(improvement);
            return vm;
        }
    }
}
