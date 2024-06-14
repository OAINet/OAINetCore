namespace OAINet.Node.Network;

public class ObjectParameter
{
    public string? Name { get; set; }
    public Dictionary<string, string> Properties { get; set; } = new();
    public List<ObjectParameter> NestedObjects { get; set; } = new();
}
