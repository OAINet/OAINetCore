namespace OAINet.Node.Blockchain;

[Serializable]
public class Block
{
    public string Hash { get; set; }
    public string PreviousHash { get; set; }
    public BaseContentType Content { get; set; }
    public DateTime CreatedAt { get; set; }
}

public abstract class BaseContentType
{
    public string OwnerPK { get; set; }
}

public class SimpleContentType : BaseContentType;