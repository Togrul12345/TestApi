using Domain.Common.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class MessageRepository : IMessageRepository
    {
        private readonly TestDbContext _context;

        public MessageRepository(TestDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<DateTime>> GetMessagesHistory()
        {
            var messagesHistories =  _context.Messages
                .Select(m => m.CreatedDate).AsQueryable();
                ;
            return messagesHistories;
        }
    }
}
