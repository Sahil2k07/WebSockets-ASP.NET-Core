using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;
using websocket_server.core.interfaces;

namespace websocket_server.api.controllers;

[ApiController]
[Route("/api/group")]
public class GroupChatController(IGroupChatService groupChatService) : Controller
{
    private readonly IGroupChatService _groupChatService = groupChatService;

    [HttpGet]
    public async Task ChatMessage([FromQuery] int groupID)
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            await _groupChatService.HandleWebSocketConnection(webSocket, groupID);
        }
        else
        {
            HttpContext.Response.StatusCode = 400;
        }
    }
}
