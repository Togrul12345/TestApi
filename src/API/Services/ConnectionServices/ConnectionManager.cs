using API.Dtos;
using System.Collections.Concurrent;

namespace API.Services.ConnectionServices
{
    public class ConnectionManager:IConnectionManager
    {// Key = userId, Value = List of connectionIds
        private readonly ConcurrentDictionary<int, HashSet<string>> _connections = new();

        public void AddConnection(int userId, string connectionId)
        {
            var userConnections = _connections.GetOrAdd(userId, _ => new HashSet<string>());
            lock (userConnections)
            {
                userConnections.Add(connectionId);
            }
        }
        public IEnumerable<string> GetConnectionIds(int userId)
        {
            if (_connections.TryGetValue(userId, out var userConnections))
            {
                lock (userConnections)
                {
                    return userConnections.ToList(); // copy qaytarırıq
                }
            }

            return Enumerable.Empty<string>();
        }
        public void RemoveConnection(int userId, string connectionId)
        {
            if (_connections.TryGetValue(userId, out var userConnections))
            {
                lock (userConnections)
                {
                    userConnections.Remove(connectionId);
                    if (userConnections.Count == 0)
                    {
                        _connections.TryRemove(userId, out _);
                    }
                }
            }
        }

        public IEnumerable<string> GetConnections(int userId)
        {
            if (_connections.TryGetValue(userId, out var userConnections))
            {
                lock (userConnections)
                {
                    return userConnections.ToList();
                }
            }
            return Enumerable.Empty<string>();
        }
        
    }
}
