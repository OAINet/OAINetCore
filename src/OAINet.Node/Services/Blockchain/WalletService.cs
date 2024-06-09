using System.Security.Cryptography;
using System.Text;
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
    
    public bool VerifySignature(WalletCommunicationInformation walletCommunicationInformation)
    {
        using (var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256))
        {
            ecdsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(walletCommunicationInformation.PK), out _);
            var hash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(walletCommunicationInformation.Data));
            return ecdsa.VerifyHash(hash, Convert.FromBase64String(walletCommunicationInformation.Signature));
        }
    }
}