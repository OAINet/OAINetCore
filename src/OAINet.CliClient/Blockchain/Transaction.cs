namespace OAINet.CliClient.Blockchain;

public class Transaction
{
    public string SenderPublicKey { get; set; }
    public string RecipientPublicKey { get; set; }
    public decimal Amount { get; set; }
    public string Signature { get; set; }
}