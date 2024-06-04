using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OAINet.Node.Blockchain;

[Serializable]
public class Block
{
    public string Hash { get; set; }
    public string PreviousHash { get; set; }
    public BaseContentType Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CalculateHash()
    {
        using (var sha256 = SHA256.Create())
        {
            var rawData = $"{PreviousHash}{CreatedAt}{JsonSerializer.Serialize(Content)}";
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return Convert.ToBase64String(bytes);
        }
    }
}

public abstract class BaseContentType
{
    public string OwnerPK { get; set; }
}

public class SimpleContentType : BaseContentType;

public class WalletContentType : BaseContentType
{
    public decimal CoinSold { get; set; }
}

public class TransactionContentType : BaseContentType
{
    public string AudiencePK { get; set; }
    public decimal TransactionSold { get; set; }
}