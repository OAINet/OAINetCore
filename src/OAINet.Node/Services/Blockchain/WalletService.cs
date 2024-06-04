using Microsoft.Extensions.Logging;
using OAINet.Node.Blockchain;

namespace OAINet.Node.Services.Blockchain;

public class WalletService
{
    private readonly ILogger<WalletService> _logger;
    private readonly Node.Blockchain.Blockchain _blockchain;

    public WalletService(
        ILogger<WalletService> logger,
        Node.Blockchain.Blockchain blockchain)
    {
        _logger = logger;
        _blockchain = blockchain;
    }
    
    
}