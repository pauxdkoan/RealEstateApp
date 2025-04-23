using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class MessageRepository : GenericRepository<Message, int>, IMessageRepository
    {
        private readonly ApplicationContext _dbContext;
        public MessageRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        } 
    }
}

