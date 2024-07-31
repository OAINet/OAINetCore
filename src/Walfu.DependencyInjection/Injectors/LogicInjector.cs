using Microsoft.Extensions.DependencyInjection;
using Walfu.Node.Logics;

namespace OAINet.DependencyInjection.Injectors;

public static class LogicInjector
{
    public static IServiceCollection AddWalfuNode(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<WalfuNode>();
        return serviceCollection;
    }
}