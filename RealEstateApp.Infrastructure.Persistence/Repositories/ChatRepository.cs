
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class ChatRepository : GenericRepository<Chat, int>, IChatRepository
    {
        public ChatRepository(ApplicationContext context) : base(context) {}
    }
}

