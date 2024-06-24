using System.Net.WebSockets;
using Microsoft.Extensions.DependencyInjection;
using OAINet.Common.Interfaces;
using OAINet.DependencyInjection.Managers;
using OAINet.Protocol.HttpsProtocolSupport;
using OAINet.Protocol.RPCSupport;
using OAINet.Protocol.WebSocketSupport;

namespace OAINet.DependencyInjection.Injectors;

public static class ProtocolInjector
{
    public static void AddRpcSupport(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IProtocolSupport, RPCProtocolSupport>();
    }
    
    public static void AddHttpsSupport(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IProtocolSupport, HttpsProtocolSupport>();
    }
    
    public static void AddWebSocketSupport(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IProtocolSupport, WebSocketProtocolSupport>();
    }
    
    public static void AddProtocolManager(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ProtocolManager>();
    }
}