using System.Net.WebSockets;
using Microsoft.Extensions.DependencyInjection;
using Walfu.Common.Interfaces;
using OAINet.DependencyInjection.Managers;
using Walfu.Protocol.HttpsProtocolSupport;
using Walfu.Protocol.RPCSupport;
using Walfu.Protocol.WebSocketSupport;

namespace OAINet.DependencyInjection.Injectors;

public static class ProtocolInjector
{
    public static IServiceCollection AddRpcSupport(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IProtocolSupport, RPCProtocolSupport>();
        return serviceCollection;
    }
    
    public static IServiceCollection AddHttpsSupport(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IProtocolSupport, HttpsProtocolSupport>();
        return serviceCollection;
    }
    
    public static IServiceCollection AddWebSocketSupport(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IProtocolSupport, WebSocketProtocolSupport>();
        return serviceCollection;
    }
    
    public static IServiceCollection AddProtocolManager(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ProtocolManager>();
        return serviceCollection;
    }
}