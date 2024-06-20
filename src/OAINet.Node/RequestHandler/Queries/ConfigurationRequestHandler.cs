using OAINet.Node.Environment;
using OAINet.Node.Network;
using OAINet.Node.Network;

namespace OAINet.Node.RequestHandler.Queries;

public class ConfigurationRequestHandler : RequestHandler
{
    private readonly Blockchain.Blockchain _blockchain;
    private readonly NodeContants _nodeContants;

    public ConfigurationRequestHandler(NodeContants nodeContants, 
        Blockchain.Blockchain blockchain)
    {
        _blockchain = blockchain;
        _nodeContants = nodeContants;
        
    }

    [OAINetHandler("node/config/inf")]
    public OAINetResponse NodeConfiguration(Request request)
    {
        return this.OAINetSucces(new
        {
            NodeVersion = _nodeContants.OAINetProtocolVersion,
            NodePort = _nodeContants.NodePort,
            BlockchainInjectorTime = $"each {_nodeContants.BlockchainInjectorTimeInMinute} minute(s)."
        });
    }
    
    
}

