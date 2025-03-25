using System.Net.WebSockets;
using System.Text;
using websocket_server.core.interfaces;

namespace websocket_server.core.services;

public class GroupChatService(IWebSocketService webSocketService) : IGroupChatService
{
    private readonly IWebSocketService _webSocketService = webSocketService;

    private async Task SendMessageInGroup(int groupID, string message, WebSocket userConnection)
    {
        var connections = _webSocketService.GetGroupSessions(groupID);

        foreach (var conn in connections)
        {
            if (conn == userConnection)
            {
                continue;
            }

            var response = Encoding.UTF8.GetBytes(message);

            await conn.SendAsync(response, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        // Save in DB
    }

    public async Task HandleWebSocketConnection(WebSocket webSocket, int groupID)
    {
        var buffer = new byte[1024 * 4];
        var arrSeg = new ArraySegment<byte>(buffer);

        _webSocketService.RegisterGroupSession(groupID, webSocket);

        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(arrSeg, CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Text)
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                await SendMessageInGroup(groupID, message, webSocket);
            }
            else if (result.MessageType == WebSocketMessageType.Close)
            {
                await webSocket.CloseAsync(
                    WebSocketCloseStatus.NormalClosure,
                    "Closing",
                    CancellationToken.None
                );

                _webSocketService.TerminateGroupSession(groupID, webSocket);
            }
        }
    }
}
