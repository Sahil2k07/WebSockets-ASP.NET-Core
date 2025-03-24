using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using websocket_server.core.interfaces;

namespace websocket_server.api.controllers;

[ApiController]
[Route("/api/chat/")]
public class ChatController(IChatService chatService) : Controller
{
    private readonly IChatService _chatService = chatService;

    [HttpGet("user")]
    public async Task OneToOneMessage([FromQuery] int userID, int receiverID)
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            await _chatService.HandleWebSocketConnection(webSocket, userID, receiverID);
        }
        else
        {
            HttpContext.Response.StatusCode = 400;
        }
    }
}
