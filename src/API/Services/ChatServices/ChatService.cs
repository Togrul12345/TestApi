using Domain.Entities.MessageEntity;
using Infrastructure.Contexts;

namespace API.Services.ChatServices
{
    public class ChatService : IChatService
    {
        private readonly TestDbContext _context;

        public ChatService(TestDbContext context)
        {
            _context = context;
        }

        public async Task MarkAsDelivered(int userId)
        {
            var messages = _context.Messages
                .Where(m => m.ReceiverId == userId && !m.IsDelivered)
                .ToList();
            foreach (var message in messages)
            {
                message.IsDelivered = true;
            }
           await _context.SaveChangesAsync();
        }

        public async Task MarkAsReadAsync(int messageId)
        {
            var messages = _context.Messages
                .Where(m => m.Id == messageId && !m.IsRead)
                .ToList();
            foreach (var message in messages)
            {
                message.IsRead = true;
            }
            await _context.SaveChangesAsync();
        }

        public Task<Message> SendMessageAsync(string senderId, string receiverId, string text)
        {
            throw new NotImplementedException();
        }
    }
}
