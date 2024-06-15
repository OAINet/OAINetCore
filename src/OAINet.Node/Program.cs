using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OAINet.Node.Blockchain;
using OAINet.Node.Network;
using OAINet.Node.Services;
using OAINet.Node.Services.Blockchain;

public static class Program
{
    public static async Task Main(string[] args)
    {
        ServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<Node>();
        serviceCollection.AddOAINetLogicalServices();
        serviceCollection.AddOAINetRequestHandlers();
        serviceCollection.AddLogging(configure => configure.AddConsole())
            .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);
        ServiceProvider builder = serviceCollection.BuildServiceProvider();

        Node? node = builder.GetService<Node>();
        if (node is null) throw new NullReferenceException(nameof(node));
        await node.RunNode();
    }
}