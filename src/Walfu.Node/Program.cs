using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OAINet.DependencyInjection.Injectors;
using Walfu.Node.Logics;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddWalfuNode()
            .AddHttpsSupport();
        var builder = serviceCollection.BuildServiceProvider();

        var node = builder.GetService<WalfuNode>();
        node.RunNode();
    }
}