namespace API.Services.UserConnectionServices
{
    public interface IUserConnectionManager
    {
        void AddConnection(string userId, string connectionId);
        void RemoveConnection(string userId, string connectionId);
        IReadOnlyList<string> GetConnections(string userId);
        bool IsOnline(string userId);
    }
}
