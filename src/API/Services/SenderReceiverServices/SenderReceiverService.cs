using Domain.Entities.UserEntity;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Services.SenderReceiverServices
{
    public class SenderReceiverService : ISenderReceiverService
    {private readonly TestDbContext _context;

        public SenderReceiverService(TestDbContext context)
        {
            _context = context;
        }

        public async Task Create(SenderReceiver senderReceiver)
        {
            _context.SenderReceivers.Add(senderReceiver);
             await _context.SaveChangesAsync();
        }

        public async Task<List<SenderReceiver>> GetAllBy(Expression<Func<SenderReceiver, bool>> exp)
        {
            return await _context.SenderReceivers.Where(exp).ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
