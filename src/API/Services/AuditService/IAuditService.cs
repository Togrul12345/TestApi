namespace API.Services.AuditService
{
    public interface IAuditService
    {
        Task LogAsync(int userId, string action, string target, object details = null);
    }
}
