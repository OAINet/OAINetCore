using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OAINet.Node.Blockchain;

[Serializable]
public class Block
{
    public string? Hash { get; set; }
    public string? PreviousHash { get; set; }
    public BaseContentType? Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CalculateHash()
    {
        // FIXME: Use HMAC instead of concatenation
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
}

public class SimpleContentType : BaseContentType;

public class TransactionContentType : BaseContentType
{
    public string? FromPk { get; set; }
    public string? TransactionHash { get; set; }
    public string? AudiencePK { get; set; }
    public decimal TransactionSold { get; set; }
    [NonSerialized]
    private readonly DateTime CreatedAt;

    public TransactionContentType()
    {
        CreatedAt = DateTime.Now;
    }
    
    public string CalculateHash()
    {
        using (var sha256 = SHA256.Create())
        {
            var rawData = $"{TransactionSold}{FromPk}{AudiencePK}{CreatedAt}";
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return Convert.ToBase64String(bytes);
        }
    }
}