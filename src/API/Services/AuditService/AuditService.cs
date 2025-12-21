
using Domain.Entities.AuditLogEntity;
using Infrastructure.Contexts;

namespace API.Services.AuditService
{
    public class AuditService : IAuditService
    {
        private readonly TestDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditService(TestDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LogAsync(int userId, string action, string target, object details = null)
        {
            var log = new AuditLog
            {
                CreatedDate = DateTime.UtcNow,
                Detail = details,
                Action = action,
                Target = target,
                UserId = userId,
                IpAdress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString()
            };
            _context.AuditLogs.Add(log);
          await  _context.SaveChangesAsync();
        }
    }
}
