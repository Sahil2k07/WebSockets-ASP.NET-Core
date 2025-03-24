using Microsoft.EntityFrameworkCore;
using websocket_server.core.interfaces;

namespace websocket_server.core.contexts;

public class WSServerDBContext(IConfigService configService) : DbContext
{
    private readonly IConfigService _configService = configService;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configService.GetDBConnectionString();

        optionsBuilder.UseNpgsql(connectionString);
    }
}
