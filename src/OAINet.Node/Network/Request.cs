namespace OAINet.Node.Network;

public class Request
{
    public string? Uri { get; set; }
    public Dictionary<string, string> Parameters { get; set; } = new();
    public List<ObjectParameter> Objects { get; set; } = new();
}
