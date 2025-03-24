using websocket_server.core.interfaces;

namespace websocket_server.core.configs;

public class ProdConfigService : IConfigService
{
    public string? GetDBConnectionString()
    {
        string? connectionString = Environment.GetEnvironmentVariable("DB_URL");

        return connectionString;
    }
}
