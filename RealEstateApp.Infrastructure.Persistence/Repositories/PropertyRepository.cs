


using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class PropertyRepository:GenericRepository<Property, int>, IPropertyRepository
    {
        private readonly ApplicationContext _dbContext;

        public PropertyRepository(ApplicationContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }



    }
}
