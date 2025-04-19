


using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class SalesTypeRepository : GenericRepository<SalesType, int>, ISalesTypeRepository
    {
        private readonly ApplicationContext _dbContext;

        public SalesTypeRepository(ApplicationContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }



    }
}
