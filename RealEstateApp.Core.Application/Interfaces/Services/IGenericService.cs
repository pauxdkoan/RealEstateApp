
namespace RealEstateApp.Core.Application.Interfaces.Services
{
    //Hice la interfaz mas generica para q acepte id de tipo string para [Usuario]
    public interface IGenericService<SaveVm,Vm, Model, TId>
        where SaveVm : class
        where Vm : class
        where Model : class
    {
        Task Update(SaveVm vm, TId id);

        Task<SaveVm> Add(SaveVm vm);

        Task<List<SaveVm>> AddRange(List<SaveVm> vm);

        Task Delete(TId id);

        Task<SaveVm> GetByIdSaveViewModel(TId id);

        Task<List<Vm>> GetAllListViewModel();

        IQueryable<Vm> GetAllQueryViewModel();
    }
}
