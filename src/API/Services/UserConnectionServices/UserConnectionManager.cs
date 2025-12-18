
namespace API.Services.UserConnectionServices
{
    public class UserConnectionManager : IUserConnectionManager
    {
        private readonly Dictionary<string, HashSet<string>> _connections
        = new();
        public void AddConnection(string userId, string connectionId)
        {
            lock (_connections)
            {
                if (!_connections.ContainsKey(userId))
                    _connections[userId] = new HashSet<string>();

                _connections[userId].Add(connectionId);
            }
        }

        public IReadOnlyList<string> GetConnections(string userId)
        {
            return _connections.ContainsKey(userId)
           ? _connections[userId].ToList()
           : Array.Empty<string>();
        }

        public bool IsOnline(string userId)
        {
            return _connections.ContainsKey(userId);
        }

        public void RemoveConnection(string userId, string connectionId)
        {
            lock (_connections)
            {
                if (!_connections.ContainsKey(userId))
                    return;

                _connections[userId].Remove(connectionId);

                if (_connections[userId].Count == 0)
                    _connections.Remove(userId);
            }
        }
    }
}
