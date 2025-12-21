using API.Dtos;
using API.Models;
using API.Services.AuditService;
using API.Services.ChatServices;
using API.Services.ConnectionServices;
using API.Services.SenderReceiverServices;
using API.Services.UserServices;
using Application.Dtos.ChatAdminDtos;
using Application.Dtos.ChatDtos;
using Application.Dtos.ChatUserDtos;
using Domain.Common.Entities;
using Domain.Common.Results;
using Domain.Entities.ChatEntity;
using Domain.Entities.MessageEntity;
using Domain.Entities.UserEntity;
using Domain.Enums;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using XAct.Users;

namespace API.Hubs
{

    public class ChatHub : Hub
    {
        private readonly IHttpClientFactory _factory;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        private readonly ISenderReceiverService _senderReceiverService;
        private readonly IConnectionManager _connectionManager;
        private readonly TestDbContext _context;
        private readonly IAuditService _auditService;

        public ChatHub(IHttpClientFactory factory, IChatService chatService, IUserService userService, ISenderReceiverService senderReceiverService, IConnectionManager connectionManager, TestDbContext context, IAuditService auditService)
        {
            _factory = factory;
            _chatService = chatService;
            _userService = userService;
            _senderReceiverService = senderReceiverService;
            _connectionManager = connectionManager;
            _context = context;
            _auditService = auditService;
        }
        //Huba qoşulma zamanı işləyən metod
        public async override Task OnConnectedAsync()
        {
            var isAdmin = Context.User.IsInRole("Admin");
            var client = _factory.CreateClient();
            var userId = Context.UserIdentifier;
            var removeduser = _context.RemovedUserFromChats.FirstOrDefault(r => r.UserId == int.Parse(userId!) && r.Until > DateTime.UtcNow);
            if (removeduser != null)
            {
                Context.Abort();
                return;
            }
            var banndedUsers = _context.Bans.Where(b => b.UserId == int.Parse(userId!) && b.Until > DateTime.UtcNow).ToList();
            if (banndedUsers.Count > 0)
            {
                Context.Abort();
                return;
            }
            if (!isAdmin)
            {
                await Clients.All.SendAsync("UserOnline", userId);
                await _userService.AssignStatus(true, int.Parse(userId!));
                await _auditService.LogAsync(int.Parse(userId!), "AssignStatus", "UserStatus", true);
            }
            await base.OnConnectedAsync();

        }
        // // Admin ghost mode üçün online users alma
        public async Task<List<int>> GetOnlineUsers()
        {
            var userId = int.Parse(Context.UserIdentifier!);


            var users = _context.AppUsers
              .Where(u => u.IsAdmin == false && u.Id != userId)
              .Select(u => u.Id)
              .ToList();

            return await Task.FromResult(users);





        }
        //Chat yaratmaq üçün yazılmış metod
        public async Task CreateChat(string foneImg, string avatar)
        {
            var dto = new CreateChatDto
            {
                Avatar = avatar,
                FoneImg = foneImg,
            };
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var client = _factory.CreateClient();
            var result = await client.PostAsync($"https://localhost:7294/api/Chats/Create", content);
            if (result.IsSuccessStatusCode)
            {
                var userId = Context.UserIdentifier;
                await _auditService.LogAsync(int.Parse(userId!), "Create", "Chat", dto);
            }
        }
        //Useri chata əlavə etmək üçün yazılmış metod
        public async Task AddUserToChat(int chatId, int userId)
        {
            var entity = new ChatUser
            {
                ChatId = chatId,
                ParticipantId = userId
            };
            _context.ChatUsers.Add(entity);
            await _context.SaveChangesAsync();

            await _auditService.LogAsync(userId, "AddUser", "Chat", entity);

        }
        //15 dəqiqə ərzində update etmək üçün yazilmış metod
        public async Task EditMessage(int messageId, string content)
        {
            var userId = Context.UserIdentifier;
            await _chatService.UpdateMessage(messageId, content);
            await _auditService.LogAsync(int.Parse(userId!), "Update", "Message", content);
        }
        //Ban etmək üçün yazılmış metod
        public async Task TemporaryBan(int userId, int hours, string reason, int roomId)
        {
            var ban = new Ban
            {
                UserId = userId,
                RoomId = roomId,
                Until = DateTime.UtcNow.AddHours(hours),
                Reason = reason

            };
            await _chatService.CreateBan(ban);
            await _auditService.LogAsync(userId, "Update", "Ban", ban);
            await Clients.User(userId.ToString())
       .SendAsync("Banned", $"You are banned until {ban.Until}. Reason: {reason}");
            var connectionIds = _connectionManager.GetConnectionIds(userId);
            if (connectionIds.First() != null)
            {
                await Clients.Client(connectionIds.First()).SendAsync("ForceDisconnect");
            }
            await Clients.User(userId.ToString())
        .SendAsync("Banned", $"You are permanently banned. Reason: {reason}");

        }
        //Useri connectiondan remove etmək üçün yazılmış metod
        public async Task RemoveFromChat(int userId, int hours)
        {
            _context.RemovedUserFromChats.Add(new RemovedUserFromChat
            {
                UserId = userId,
                Until = DateTime.UtcNow.AddHours(hours)
            });
            await _context.SaveChangesAsync();
            await _auditService.LogAsync(userId, "Add", "RemovedUserFromChat", new { UserId = userId, Until = DateTime.UtcNow.AddHours(hours) });
            var connectionIds = _connectionManager.GetConnectionIds(userId);
            foreach (var connectionId in connectionIds)
            {
                await Clients.Client(connectionId).SendAsync("RemovedFromChat", "You have been removed from the chat by an admin.");
            }
        }
        //Pollun baglanmasi
        public async Task ClosePoll(int pollId)
        {
            var userId = int.Parse(Context.UserIdentifier!);

            var poll = await _context.Polls.FindAsync(pollId);
            if (poll == null || poll.IsClosed)
                return;

            // yalnız admin / owner
            if (!Context.User.IsInRole("Admin"))
                throw new HubException("Forbidden");

            poll.IsClosed = true;
            poll.DecisionLocked = true;

            // 🔒 chat-i read-only et
            var chat = await _context.Chats.FindAsync(poll.ChatId);
            chat!.IsReadOnly = true;

            await _context.SaveChangesAsync();

            // 🔔 realtime xəbər
            await Clients.Group($"chat-{poll.ChatId}")
                .SendAsync("PollClosed", new
                {
                    PollId = poll.Id,
                    ChatId = poll.ChatId
                });
        }
        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
            await _userService.AssignStatus(false, int.Parse(userId!));
            await _auditService.LogAsync(int.Parse(userId!), "AssignStatus", "UserStatus", false);
            await base.OnDisconnectedAsync(exception);
        }


        //mesaj göndərmək üçün yazılmış metod
        public async Task SendMessage(string message, int receiverId, int chatId, DateTime? unLockAt, MessageType type)
        {
            var userId = Context.UserIdentifier;
            var userName = Context.User.Identity?.Name;
            var chat = _context.Chats.FirstOrDefault(c => c.Id == chatId);
            if (chat.IsReadOnly == true)
            {
                throw new HubException("Chat is read-only. You cannot send messages.");
            }
            DateTime? ExpiredAt = null;
            if (type == MessageType.File)
            {
                ExpiredAt = DateTime.UtcNow.AddHours(24);
            }
            var entity = new Message()
            {
                ChatId = chatId,
                Content = message,
                ExpiredAt = ExpiredAt,

                SentDate = DateTime.UtcNow,
                UnLockAt = unLockAt
            };
            if (unLockAt != null && unLockAt > DateTime.UtcNow)
            {
                entity.Status = "Locked";
                await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", message);
                entity.IsDelivered = true;
                await _context.SaveChangesAsync();
            }
            else
            {
                entity.Status = "Sent";
            }
            var dto = new
            {
                chatId = chatId,
                message = message
            };
            var UserReceivers = await _senderReceiverService.GetAllBy(sr => sr.ChatId == chatId && sr.UserId != int.Parse(userId!));
            foreach (var receiver in UserReceivers)
            {
                receiver.UnReadCount += 1;
            }
            await _senderReceiverService.SaveChanges();
            await _auditService.LogAsync(int.Parse(userId!), "SendMessage", "UnreadCount", UserReceivers);
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var client = _factory.CreateClient();
            await client.PostAsync($"https://localhost:7294/api/Chats{chatId}/messages?content={message}", content);
            await _auditService.LogAsync(int.Parse(userId!), "{chatId}/messages?content={message}", "ChatId,Content", dto);
            await Groups.AddToGroupAsync(
        Context.ConnectionId,
        $"user-{userId}"
    );
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
        //mesaj çatdırılmasını işarələmək üçün yazılmış metod
        public async Task MarkMessageAsDelivered()
        {
            var userId = Context.UserIdentifier;
            await _chatService.MarkAsDelivered(int.Parse(userId!));
            await Clients.All.SendAsync("MessageDelivered", $"Messages delivered to user {userId}");
        }
        //mesaj oxundu kimi işarələmək üçün yazılmış metod
        public async Task MarkAsRead(int messageId)
        {
            var userReceivers = await _senderReceiverService.GetAllBy(sr => sr.UserId == int.Parse(Context.UserIdentifier!));
            foreach (var receiver in userReceivers)
            {
                receiver.UnReadCount = 0;
            }
            await _senderReceiverService.SaveChanges();
            await _chatService.MarkAsReadAsync(messageId);
        }
        //public async Task<List<OnlineUserDto>> GetOnlineUsers()
        //{
        //    //var isGhost = Context.User.IsInRole("Admin");
        //if(isGhost)
        //{
        //    throw new HubException("Forbidden");
        //}
        //var currentUserId = int.Parse(Context.UserIdentifier!);
        //var users = await 
        // }
    }
}
