
using API.Hubs;
using Domain.Enums;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.SignalR;
using System;

namespace API.Services.MessageServices
{
    public class MessageExpirationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<ChatHub> _hub;

        public MessageExpirationService(
            IServiceScopeFactory scopeFactory,
            IHubContext<ChatHub> hub)
        {
            _scopeFactory = scopeFactory;
            _hub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<TestDbContext>();

                var expiredMessages = db.Messages
                    .Where(m =>
                        m.ExpiredAt != null &&
                        m.ExpiredAt <= DateTime.UtcNow &&
                        m.IsExpired==false)
                    .ToList();

                foreach (var msg in expiredMessages)
                {
                    msg.IsExpired = true;

                    if (msg.Type == MessageType.File &&
                        File.Exists(msg.FilePath))
                    {
                        File.Delete(msg.FilePath);
                    }

                    // 🔔 UI-ni xəbərdar et
                    await _hub.Clients.Group($"chat-{msg.ChatId}")
                        .SendAsync("MessageExpired", msg.Id);
                }

                await db.SaveChangesAsync();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }


}
