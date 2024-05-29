namespace OAINet.Node.RequestHandler.Queries;

public class Stocker
{
    [OAINetHandler("hello")]
    public string HandleWorld(string peerAdress)
    {
        return "world, " + peerAdress;
    }
}