using System.Text.Json;
using OAINet.Node.Environment;
using OAINet.Node.Network;

namespace OAINet.Node.RequestHandler.Queries;

public class Stocker
{
    
    [OAINetHandler("node/information")]
    public string QueriesNodeInformation(Request request)
    {
        return JsonSerializer.Serialize(new
        {
            NodeContants.NodePort,
            NodeContants.BlockchainInjectorTimeInMinute,
            NodeContants.OAINetProtocolVersion,
        } );
    }
}