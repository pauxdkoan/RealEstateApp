


using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class FavoritePropertyRepository : GenericRepository<FavoriteProperty,int>, IFavoritePropertyRepository
    {
        private readonly ApplicationContext _context;
        public FavoritePropertyRepository(ApplicationContext context) : base(context) { }

        
    }
}
