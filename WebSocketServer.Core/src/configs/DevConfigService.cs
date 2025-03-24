using Microsoft.Extensions.Configuration;
using websocket_server.core.interfaces;

namespace websocket_server.core.configs;

public class DevConfigService(IConfiguration configuration) : IConfigService
{
    private readonly IConfiguration _configuration = configuration;

    public string? GetDBConnectionString()
    {
        string? connectionString = _configuration.GetConnectionString("DBConnectionString");

        return connectionString;
    }
}
