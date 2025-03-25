using System.Net.WebSockets;

namespace websocket_server.core.interfaces;

public interface IWebSocketService
{
    void RegisterConnection(int userID, WebSocket webSocket);

    void TerminateConnection(int userID);

    WebSocket? GetReceiverConnection(int userID);

    void RegisterGroupSession(int groupID, WebSocket webSocket);

    void TerminateGroupSession(int groupID, WebSocket webSocket);

    HashSet<WebSocket> GetGroupSessions(int groupID);
}
