using Microsoft.Extensions.DependencyInjection;
using OAINet.Node.Environment;
using OAINet.Node.RequestHandler.Queries;

namespace OAINet.Node.Services;

public static class OAINetServices
{
    public static void AddOAINetLogicalServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<Node.Blockchain.Blockchain>();
        serviceCollection.AddSingleton<NodeContants>();
        serviceCollection.AddTransient<Blockchain.WalletService>();
        serviceCollection.AddTransient<Blockchain.TransactionService>();
    }
    public static void AddOAINetRequestHandlers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ConfigurationRequestHandler>();
    }
}