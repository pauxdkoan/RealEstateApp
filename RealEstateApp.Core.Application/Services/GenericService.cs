
using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;

namespace RealEstateApp.Core.Application.Services
{
    public class GenericService<SaveVm,Vm, Model,TId>:IGenericService<SaveVm,Vm,Model,TId>
        where SaveVm : class
        where Vm : class
        where Model : class
    {
        private readonly IGenericRepository<Model, TId> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Model,TId> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public virtual async Task Update(SaveVm vm, TId id)
        {
            Model entity = _mapper.Map<Model>(vm);
            await _repository.UpdateAsync(entity, id);
        }

        public virtual async Task<SaveVm> Add(SaveVm vm)
        {
            Model entity = _mapper.Map<Model>(vm);

            entity = await _repository.AddAsync(entity);

            SaveVm entityVm = _mapper.Map<SaveVm>(entity);

            return entityVm;
        }

        public virtual async Task<List<SaveVm>> AddRange(List<SaveVm> vm)
        {
            List<Model> entities = _mapper.Map<List<Model>>(vm);

            entities = await _repository.AddRangeAsync(entities);

            List<SaveVm> entitiesVm = _mapper.Map<List<SaveVm>>(entities);

            return entitiesVm;
        }

        public virtual async Task Delete(TId id)
        {
            var product = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(product);
        }

        public virtual async Task<SaveVm> GetByIdSaveViewModel(TId id)
        {
            var entity = await _repository.GetByIdAsync(id);

            SaveVm vm = _mapper.Map<SaveVm>(entity);
            return vm;
        }

        public virtual async Task<List<Vm>> GetAllListViewModel()
        {
            var entityList = await _repository.GetAllListAsync();

            return _mapper.Map<List<Vm>>(entityList);
        }

        public virtual IQueryable<Vm> GetAllQueryViewModel()
        {
            var entityList = _repository.GetAllQuery();

            return _mapper.Map<IQueryable<Vm>>(entityList);
        }

    }
}
