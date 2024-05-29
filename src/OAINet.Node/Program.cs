using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OAINet.Node.Network;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<Node>();
        serviceCollection.AddLogging(configure => configure.AddConsole())
            .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);
        var builder = serviceCollection.BuildServiceProvider();
        var node = builder.GetService<Node>();
        await node.RunNode();
    }
}