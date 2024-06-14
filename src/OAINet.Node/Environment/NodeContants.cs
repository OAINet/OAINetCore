namespace OAINet.Node.Environment;

public class NodeContants
{
    public string NodePort { get; private set; } = System.Environment.GetEnvironmentVariable("OAINET_NODE_PORT");
    public string OAINetProtocolVersion { get; private set; } = System.Environment.GetEnvironmentVariable("OAINET_PROTOCOL_VERSION");
    public string BlockchainInjectorTimeInMinute { get; private set; } = System.Environment.GetEnvironmentVariable("OAINET_BLOCKCHAIN_INJECTOR_TIME");
}