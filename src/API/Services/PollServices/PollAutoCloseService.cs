using API.Hubs;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.SignalR;
using System;

namespace API.Services.PollServices
{
    public class PollAutoCloseService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<ChatHub> _hub;

        public PollAutoCloseService(
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

                var expiredPolls = db.Polls
                    .Where(p =>
                        !p.IsClosed &&
                        p.EndsAt <= DateTime.UtcNow)
                    .ToList();

                foreach (var poll in expiredPolls)
                {
                    poll.IsClosed = true;
                    poll.DecisionLocked = true;

                    var chat = db.Chats.First(c => c.Id == poll.ChatId);
                    chat.IsReadOnly = true;

                    await _hub.Clients.Group($"chat-{poll.ChatId}")
                        .SendAsync("PollClosed", new
                        {
                            PollId = poll.Id,
                            ChatId = poll.ChatId
                        });
                }

                await db.SaveChangesAsync();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }

}
