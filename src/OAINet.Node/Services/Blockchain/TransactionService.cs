using Microsoft.Extensions.Logging;
using OAINet.Node.Blockchain;

namespace OAINet.Node.Services.Blockchain;

public class TransactionService
{
    private readonly ILogger<TransactionService> _logger;
    private readonly Node.Blockchain.Blockchain _blockchain;
    private readonly WalletService _walletService;

    public TransactionService(
        ILogger<TransactionService> logger,
        Node.Blockchain.Blockchain blockchain,
        WalletService walletService)
    {
        _logger = logger;
        _blockchain = blockchain;
        _walletService = walletService;
    }

    public async Task CreateTransaction(WalletCommunicationInformation walletCommunicationInformation,
        string To,
        decimal value)
    {
        // Why async ? It's only computation, isn't it ?
        var result = _walletService.VerifySignature(walletCommunicationInformation);
        if (result != true)
        {
            _logger.LogError("a transaction was cancelled because it's not good signature.");
            return;
        }
        var block = new Block()
        {
            Content = new TransactionContentType()
            {
                FromPk = walletCommunicationInformation.PK,
                AudiencePK = To,
                TransactionSold = value
            }
        };
        block = _blockchain.AddBlock(block);
        var contentType = (TransactionContentType?)block.Content;
        _logger.LogInformation($"{block.Hash} was pushed in the blockchain, he contains the {contentType?.TransactionHash} transaction.");
    }
}

public record WalletCommunicationInformation(string Data, string PK, string Signature);