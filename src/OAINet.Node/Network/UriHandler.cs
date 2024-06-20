namespace OAINet.Node.Network;

using System;

public class UriHandler
{
    public string? Protocol { get; }
    public string Command { get; }

    public UriHandler(string uri)
    {
        if (!uri.StartsWith("oainet://"))
        {
            throw new ArgumentException("Invalid URI protocol");
        }

        var parts = uri.Substring("oainet://".Length).Split('/', 1);
        if (parts.Length != 1)
        {
            throw new ArgumentException("Invalid URI format");
        }
        
        Command = parts[0];
    }
    
    public override string ToString()
    {
        return $"Protocol: oainet, Command: {Command}";
    }
}