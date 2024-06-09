using Microsoft.Extensions.DependencyInjection;

namespace OAINet.Node.Services;

public static class OAINetServices
{
    public static void AddOAINetLogicalServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<Node.Blockchain.Blockchain>();
        serviceCollection.AddTransient<Blockchain.WalletService>();
        serviceCollection.AddTransient<Blockchain.TransactionService>();
    }
}