using System.Security.Cryptography;

namespace OAINet.Node.Blockchain;

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
}