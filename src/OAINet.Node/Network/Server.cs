using Microsoft.Extensions.Logging;

namespace OAINet.Node.Network;

public class Server
{
    private readonly ILogger<Server> _logger;

    public Server(ILogger<Server> logger)
    {
        _logger = logger;
    }

    public void RunServer()
    {
        _logger.LogInformation("server is running");
        // TODO : server setup.
    }
}