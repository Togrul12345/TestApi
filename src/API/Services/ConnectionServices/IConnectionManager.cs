namespace API.Services.ConnectionServices
{
    public interface IConnectionManager
    {
        void AddConnection(int userId, string connectionId);
        void RemoveConnection(int userId, string connectionId);
        IEnumerable<string> GetConnections(int userId);
        IEnumerable<string> GetConnectionIds(int userId);
    }
}
