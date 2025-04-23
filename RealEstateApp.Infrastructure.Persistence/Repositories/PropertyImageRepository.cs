


using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class PropertyImageRepository : GenericRepository<PropertyImage, int>, IPropertyImageRepository
    {
        private readonly ApplicationContext _dbContext;

        public PropertyImageRepository(ApplicationContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }



    }
}
