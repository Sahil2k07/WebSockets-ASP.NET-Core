using websocket_server.core.configs;
using websocket_server.core.contexts;
using websocket_server.core.interfaces;
using websocket_server.core.services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<WSServerDBContext>();

builder.Services.AddSingleton<IWebSocketService, WebSocketService>();

builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IGroupChatService, GroupChatService>();

if (builder.Environment.IsProduction())
{
    builder.Services.AddScoped<IConfigService, ProdConfigService>();
}
else
{
    builder.Services.AddScoped<IConfigService, DevConfigService>();
}

var app = builder.Build();

app.UseWebSockets();
app.UseRouting();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
