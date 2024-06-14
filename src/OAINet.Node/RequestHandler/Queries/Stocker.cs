namespace OAINet.Node.RequestHandler.Queries;

public class Stocker
{
    [OAINetHandler("hello")]
    public string HandleWorld(string peerAddress)
    {
        return "world, " + peerAddress;
    }
}