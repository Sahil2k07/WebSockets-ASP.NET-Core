using System.Collections.Concurrent;
using System.Net.WebSockets;
using websocket_server.core.interfaces;

namespace websocket_server.core.services;

public class WebSocketService : IWebSocketService
{
    private readonly ConcurrentDictionary<int, WebSocket> _connections = new();

    private readonly ConcurrentDictionary<int, HashSet<WebSocket>> _groupConnections = new();

    public void RegisterConnection(int userID, WebSocket webSocket)
    {
        _connections.AddOrUpdate(userID, webSocket, (_, _) => webSocket);
    }

    public void TerminateConnection(int userID)
    {
        if (_connections.TryGetValue(userID, out _))
        {
            _connections.Remove(userID, out _);
        }
    }

    public WebSocket? GetReceiverConnection(int userID)
    {
        if (_connections.TryGetValue(userID, out var webSocket))
        {
            return webSocket;
        }

        return null;
    }

    public void RegisterGroupSession(int groupID, WebSocket webSocket)
    {
        if (!_groupConnections.ContainsKey(groupID))
        {
            _groupConnections[groupID] = [];
        }

        _groupConnections[groupID].Add(webSocket);
    }

    public void TerminateGroupSession(int groupID, WebSocket webSocket)
    {
        if (_groupConnections.TryGetValue(groupID, out var connections))
        {
            connections.Remove(webSocket);
        }

        if (connections?.Count == 0)
        {
            _groupConnections.Remove(groupID, out _);
        }
    }

    public HashSet<WebSocket> GetGroupSessions(int groupID)
    {
        if (_groupConnections.TryGetValue(groupID, out var connections))
        {
            return connections;
        }

        return [];
    }
}
