

using RealEstateApp.Core.Application.ViewModels.Improvement;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IImprovementService : IGenericService<SaveImprovementVm,ImprovementVm, Improvement, int>
    {
       Task<ImprovementVm> GetByIdViewModel(int id);
    }
}
