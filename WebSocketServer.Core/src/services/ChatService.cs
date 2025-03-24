using System.Net.WebSockets;
using System.Text;
using websocket_server.core.interfaces;

namespace websocket_server.core.services;

public class ChatService(IWebSocketService webSocketService) : IChatService
{
    private readonly IWebSocketService _webSocketService = webSocketService;

    private async Task SendReceiverMessage(int receiverID, string message)
    {
        var receiverConnection = _webSocketService.GetReceiverConnection(receiverID);

        if (receiverConnection != null)
        {
            var response = Encoding.UTF8.GetBytes(message);

            await receiverConnection.SendAsync(
                response,
                WebSocketMessageType.Text,
                true,
                CancellationToken.None
            );
        }

        // Save to the DB
    }

    public async Task HandleWebSocketConnection(WebSocket webSocket, int userID, int receiverID)
    {
        _webSocketService.RegisterConnection(userID, webSocket);

        var buffer = new byte[1024 * 4];
        var arrSeg = new ArraySegment<byte>(buffer);

        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(arrSeg, CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Text)
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                await SendReceiverMessage(receiverID, message);
            }
            else if (result.MessageType == WebSocketMessageType.Close)
            {
                await webSocket.CloseAsync(
                    WebSocketCloseStatus.NormalClosure,
                    "Closing",
                    CancellationToken.None
                );

                _webSocketService.TerminateConnection(userID);
            }
        }
    }
}
