namespace OAINet.Node.Network;

using System;

public class UriHandler
{
    public string Protocol { get; }
    public string PeerAddress { get; }
    public string Command { get; }

    public UriHandler(string uri)
    {
        if (!uri.StartsWith("oainet://"))
        {
            throw new ArgumentException("Invalid URI protocol");
        }

        var parts = uri.Substring("oainet://".Length).Split('/', 2);
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid URI format");
        }

        PeerAddress = parts[0];
        Command = parts[1];
    }
    
    public override string ToString()
    {
        return $"Protocol: oainet, PeerAddress: {PeerAddress}, Command: {Command}";
    }
}