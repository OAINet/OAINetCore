using Microsoft.Extensions.Logging;

namespace OAINet.Node.Network;

public class Node
{
    private readonly ILogger<Node> _logger;

    public Node(ILogger<Node> logger)
    {
        _logger = logger;
    }

    public void RunServer()
    {
        _logger.LogInformation("server is running");
        // TODO : server setup.
    }
}