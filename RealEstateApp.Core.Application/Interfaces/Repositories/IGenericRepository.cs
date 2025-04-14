
namespace RealEstateApp.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity, TId> 
        where Entity : class
       
    {
        //Hice la interfaz mas generica para q acepte id de tipo string
        Task<Entity> AddAsync(Entity entity);
        Task UpdateAsync(Entity entity, TId id);
        Task DeleteAsync(Entity entity);
        Task<List<Entity>> GetAllListAsync();
        Task<Entity> GetByIdAsync(TId id);
        Task<List<Entity>> GetAllListWithIncludeAsync(List<string> properties);
        Task<List<Entity>> AddRangeAsync(List<Entity> entities);
        IQueryable<Entity> GetAllQuery();
        IQueryable<Entity> GetAllQueryWithInclude(List<string> properties);
    }
}
