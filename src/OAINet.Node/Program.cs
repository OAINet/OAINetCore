using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OAINet.Node.Network;

public static class Program
{
    public static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<Node>();
        serviceCollection.AddLogging(configure => configure.AddConsole())
            .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);
        var builder = serviceCollection.BuildServiceProvider();
        var app = builder.GetService<Node>();
        app.RunServer();
    }
}