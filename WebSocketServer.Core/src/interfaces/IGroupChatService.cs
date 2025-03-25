using System.Net.WebSockets;

namespace websocket_server.core.interfaces;

public interface IGroupChatService
{
    Task HandleWebSocketConnection(WebSocket webSocket, int groupID);
}
