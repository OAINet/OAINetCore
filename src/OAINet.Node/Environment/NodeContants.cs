namespace OAINet.Node.Environment;

public static class NodeContants
{
    public static readonly string NodePort = System.Environment.GetEnvironmentVariable("OAINET_NODE_PORT");
    public static readonly string OAINetProtocolVersion = System.Environment.GetEnvironmentVariable("OAINET_PROTOCOL_VERSION");
    public static readonly string BlockchainInjectorTimeInMinute = System.Environment.GetEnvironmentVariable("OAINET_BLOCKCHAIN_INJECTOR_TIME");
}