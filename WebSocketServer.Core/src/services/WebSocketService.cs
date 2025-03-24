using System.Collections.Concurrent;
using System.Net.WebSockets;
using websocket_server.core.interfaces;

namespace websocket_server.core.services;

public class WebSocketService : IWebSocketService
{
    private readonly ConcurrentDictionary<int, WebSocket> _connections = new();

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
}
