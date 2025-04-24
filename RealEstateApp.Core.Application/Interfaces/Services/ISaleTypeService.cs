

using RealEstateApp.Core.Application.ViewModels.Property;
using RealEstateApp.Core.Application.ViewModels.SalesType;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface ISaleTypeService : IGenericService<SaveSalesTypeVm,SalesTypeVm, SalesType, int>
    {
        Task<SalesTypeVm> GetByIdViewModel(int id);
    }
}
