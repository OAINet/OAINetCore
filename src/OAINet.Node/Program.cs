﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OAINet.Node.Blockchain;
using OAINet.Node.Network;
using OAINet.Node.Services;
using OAINet.Node.Services.Blockchain;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<Node>();
        serviceCollection.AddOAINetLogicalServices();
        serviceCollection.AddLogging(configure => configure.AddConsole())
            .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);
        var builder = serviceCollection.BuildServiceProvider();
        var node = builder.GetService<Node>();
        await node.RunNode();
    }
}