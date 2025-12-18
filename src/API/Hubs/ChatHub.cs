using API.Models;
using API.Services.ChatServices;
using Application.Dtos.ChatAdminDtos;
using Application.Dtos.ChatDtos;
using Application.Dtos.ChatUserDtos;
using Domain.Common.Entities;
using Domain.Common.Results;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IHttpClientFactory _factory;
        private readonly IChatService _chatService;

        public ChatHub(IHttpClientFactory factory, IChatService chatService)
        {
            _factory = factory;
            _chatService = chatService;
        }

        public override async Task OnConnectedAsync()
        {


            var client = _factory.CreateClient();
            var userId = Context.UserIdentifier;
      
            CreateChatDto dto = new CreateChatDto
            {
                Avatar = "defaultAvatar.png",
                FoneImg = "defaultFone.png",

                CreateAt = DateTime.UtcNow

            };
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"https://localhost:7294/api/Chats/Create", content);
            var result2=await client.GetAsync($"https://localhost:7294/api/Chats/GetAllChats");
            var content2 = await result2.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<ApiResponse<List<ResultChatDto>>>(content2, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            var lastChatId=data.Success.ResponseValue.Last().Id;
            var dto2 = new CreateChatUserDto
            {
                ChatId = lastChatId,
                ParticipantId = int.Parse(userId!)
            };
            var json3 = JsonSerializer.Serialize(dto2);
            var content3 = new StringContent(json3, System.Text.Encoding.UTF8, "application/json");
            var result3 = await client.PostAsync($"https://localhost:7294/api/Chats/{dto2.ChatId}/members?participantId={dto2.ParticipantId}", content3);

            if (result.IsSuccessStatusCode && result2.IsSuccessStatusCode && result3.IsSuccessStatusCode)
            {
                Console.WriteLine("Chat Created and user added");
            }







            await base.OnConnectedAsync();

        }
        public async Task SendMessage(string message,int receiverId,int chatId)
        {
            var userId = Context.UserIdentifier;
            var userName = Context.User.Identity?.Name;
            var dto = new
            {
                chatId = chatId,
                message = message
            };
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var client=_factory.CreateClient();
            await client.PostAsync($"https://localhost:7294/api/Chats{chatId}/messages?content={message}",content);

            await Groups.AddToGroupAsync(
        Context.ConnectionId,
        $"user-{userId}"
    );
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
        public async Task MarkMessageAsDelivered()
        {
            var userId = Context.UserIdentifier;
          await  _chatService.MarkAsDelivered(int.Parse(userId!));
            await Clients.All.SendAsync("MessageDelivered", $"Messages delivered to user {userId}");
        }
        public async Task MarkAsRead(int messageId)
        {
            await _chatService.MarkAsReadAsync(messageId);
        }
    }
}
