namespace Walfu.Blockchain.Store.Logics;

public class Transaction
{
    public string TransactionId { get; set; }
    public string ToAddress { get; set; }
    public string FromAddress { get; set; }
    public decimal Amount { get; set; }

    public Transaction(string toAddress,
        string fromAddress,
        decimal amount)
    {
        TransactionId = GenerateTransactionId();
        ToAddress = toAddress;
        FromAddress = fromAddress;
        Amount = amount;
    }
    
    private string GenerateTransactionId()
    {
        var guid = Guid.NewGuid();
        return BitConverter.ToString(guid.ToByteArray()).Replace("-", "").ToLower();
    }
}