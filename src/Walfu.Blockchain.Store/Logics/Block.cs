using System.Security.Cryptography;
using System.Text;

namespace OAINet.Blockchain.Store.Logics;

public class Block
{
    public string Hash { get; set; }
    public Header BlockHeader { get; set; }
    public List<Transaction> Transactions { get; set; }

    public Block()
    {
        BlockHeader = new Header();
        Transactions = new List<Transaction>();
        Hash = CalculateHash();
    }
    
    private string CalculateHash()
    {
        using (var sha256 = SHA256.Create())
        {
            var rawData = $"{BlockHeader.Version}{BlockHeader.PreviousHash}{BlockHeader.CreatedAt}{BlockHeader.DifficultyTarget}";
            foreach (var transaction in Transactions)
            {
                rawData += $"{transaction.TransactionId}{transaction.FromAddress}{transaction.ToAddress}{transaction.Amount}";
            }
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
    
    public void RecalculateHash()
    {
        Hash = CalculateHash();
    }
}