
namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveVm,Vm, Model>
        where SaveVm : class
        where Vm : class
        where Model : class
    {
        Task Update(SaveVm vm, int id);

        Task<SaveVm> Add(SaveVm vm);

        Task<List<SaveVm>> AddRange(List<SaveVm> vm);

        Task Delete(int id);

        Task<SaveVm> GetByIdSaveViewModel(int id);

        Task<List<Vm>> GetAllListViewModel();

        IQueryable<Vm> GetAllQueryViewModel();
    }
}
