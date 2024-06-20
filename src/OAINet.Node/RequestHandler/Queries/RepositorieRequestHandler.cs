using OAINet.Node.Network;

namespace OAINet.Node.RequestHandler.Queries;

public class RepositoriesRequestHandler : RequestHandler
{
    private readonly Blockchain.Blockchain _blockchain;
    
    public RepositoriesRequestHandler()
    {
        
    }

    [OAINetHandler("node/repo/stats")]
    public OAINetResponse GetBlockchainStats(
        Request request)
    {
        if (!request.Parameters.ContainsKey("min") 
            && !request.Parameters.ContainsKey("max"))
        {
            return this.OAINetInvalidFormat();
        }

        request.Parameters.TryGetValue("min", out string min);
        request.Parameters.TryGetValue("max", out string max);
        int.TryParse(min, out int minInt);
        int.TryParse(max, out int maxInt);
        var sample = _blockchain.GetStaticBlockchain(minInt, maxInt);

        return this.OAINetSucces(sample);
    }
}