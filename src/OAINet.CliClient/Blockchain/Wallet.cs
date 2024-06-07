namespace OAINet.CliClient.Blockchain;
using System.Security.Cryptography;
using System.Text;

public class Wallet
{
    public string PublicKey { get; set; }
    public string PrivateKey { get; set; } 
    
    public static Wallet GenerateWallet()
    {
        using (var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256))
        {
            var privateKey = ecdsa.ExportECPrivateKey();
            var publicKey = ecdsa.ExportSubjectPublicKeyInfo();

            return new Wallet
            {
                PrivateKey = Convert.ToBase64String(privateKey),
                PublicKey = Convert.ToBase64String(publicKey)
            };
        }
    }
    public bool VerifySignature(string data, string signature)
    {
        using (var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256))
        {
            ecdsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(PublicKey), out _);
            var hash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(data));
            return ecdsa.VerifyHash(hash, Convert.FromBase64String(signature));
        }
    }
    
    public string SignData(string data)
    {
        using (var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256))
        {
            ecdsa.ImportECPrivateKey(Convert.FromBase64String(PrivateKey), out _);
            var hash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(ecdsa.SignHash(hash)); 
        }
    }
    
    public Transaction CreateTransaction(string recipientPublicKey, decimal amount)
    {
        var transaction = new Transaction
        {
            SenderPublicKey = this.PublicKey,
            RecipientPublicKey = recipientPublicKey,
            Amount = amount
        };

        transaction.Signature = SignTransaction(transaction);
        return transaction;
    }

    private string SignTransaction(Transaction transaction)
    {
        var transactionData = $"{transaction.SenderPublicKey}:{transaction.RecipientPublicKey}:{transaction.Amount}";
        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(this.PrivateKey), out _);
            var signedBytes = rsa.SignData(Encoding.UTF8.GetBytes(transactionData), CryptoConfig.MapNameToOID("SHA256"));
            return Convert.ToBase64String(signedBytes);
        }
    }
}