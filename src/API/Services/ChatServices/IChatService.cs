using Domain.Entities.MessageEntity;

namespace API.Services.ChatServices
{
    public interface IChatService
    {
        Task<Message> SendMessageAsync(string senderId, string receiverId, string text);
        Task MarkAsReadAsync(int messageId);
        Task MarkAsDelivered(int userId);
    }
}
