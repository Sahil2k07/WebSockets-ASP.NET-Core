using Microsoft.AspNetCore.Mvc;
using websocket_server.core.interfaces;

namespace websocket_server.api.controllers;

[ApiController]
[Route("/api/group")]
public class GroupChatController(IGroupChatService groupChatService) : Controller
{
    private readonly IGroupChatService _groupChatService = groupChatService;
}
