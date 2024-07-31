using Microsoft.Extensions.DependencyInjection;
using Walfu.Blockchain.Store.Logics;

namespace OAINet.DependencyInjection.Injectors;

public static class BlockchainInjector
{
     public static IServiceCollection AddBlockchainStore(this IServiceCollection serviceCollection)
     {
          serviceCollection.AddSingleton<Blockchain>();
          return serviceCollection;
     }
}