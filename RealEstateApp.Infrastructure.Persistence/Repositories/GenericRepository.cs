﻿

using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Infrastructure.Persistence.Contexts;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity, TId> : IGenericRepository<Entity, TId>
        where Entity : class
    {

        private readonly ApplicationContext _dbContext;
        public GenericRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async virtual Task<Entity> AddAsync(Entity entity)
        {
            await _dbContext.Set<Entity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;

        }

        public virtual async Task<List<Entity>> AddRangeAsync(List<Entity> entities)
        {
            await _dbContext.Set<Entity>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public virtual async Task UpdateAsync(Entity entity, TId id)
        {
            var entry = await _dbContext.Set<Entity>().FindAsync(id);
            _dbContext.Entry(entry).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            _dbContext.Set<Entity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<Entity>> GetAllListAsync()
        {
            return await _dbContext.Set<Entity>().ToListAsync();
        }

        public virtual IQueryable<Entity> GetAllQuery()
        {
            return _dbContext.Set<Entity>().AsQueryable();
        }

        public virtual async Task<List<Entity>> GetAllListWithIncludeAsync(List<string> properties)
        {
            var query = _dbContext.Set<Entity>().AsQueryable();

            foreach (string property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }

        public virtual IQueryable<Entity> GetAllQueryWithInclude(List<string> properties)
        {
            var query = _dbContext.Set<Entity>().AsQueryable();

            foreach (string property in properties)
            {
                query = query.Include(property);
            }

            return query.AsQueryable();
        }

        public virtual async Task<Entity> GetByIdAsync(TId id)
        {
            return await _dbContext.Set<Entity>().FindAsync(id);
        }

       
    }
}
