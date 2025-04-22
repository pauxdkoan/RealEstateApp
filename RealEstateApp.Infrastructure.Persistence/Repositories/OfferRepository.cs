


using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class OfferRepository : GenericRepository<Offer, int>, IOfferRepository
    {
        private readonly ApplicationContext _dbContext;

        public OfferRepository(ApplicationContext dbContext) :base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
