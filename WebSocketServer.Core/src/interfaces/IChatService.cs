using System.Net.WebSockets;

namespace websocket_server.core.interfaces;

public interface IChatService
{
    Task HandleWebSocketConnection(WebSocket webSocket, int userID, int receiverID);
}
