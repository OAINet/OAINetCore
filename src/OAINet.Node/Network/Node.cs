using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging;
using OAINet.Node.RequestHandler;

namespace OAINet.Node.Network;

public record Peer(TcpClient TcpClient, TcpListener TcpListener);

public record ExternalPeer(string Id, TcpClient Client);

public class Node
{
    private readonly ILogger<Node> _logger;
    private Peer _peer;
    private List<ExternalPeer> _connectedPeers;
    private Dictionary<string, (Type, MethodInfo)> handlers = new Dictionary<string, (Type, MethodInfo)>();
    public Node(ILogger<Node> logger)
    {
        _logger = logger;
        RegisterHandlers();
    }
    private void RegisterHandlers()
    {
        var handlerTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .Any(m => m.GetCustomAttributes(typeof(OAINetHandlerAttribute), false).Length > 0));

        foreach (var type in handlerTypes)
        {
            var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.GetCustomAttributes(typeof(OAINetHandlerAttribute), false).Length > 0);

            foreach (var method in methods)
            {
                var attr = (OAINetHandlerAttribute)method.GetCustomAttributes(typeof(OAINetHandlerAttribute), false).First();
                handlers.Add(attr.Route.ToLower(), (type, method));
            }
        }
    }
    public async Task RunNode()
    {
        _logger.LogInformation("server is preparing to run");
        _peer = new Peer(new TcpClient(),
            new TcpListener(IPAddress.Any, 3024));
        _peer.TcpListener.Start();
        _connectedPeers = new List<ExternalPeer>();
        _logger.LogInformation("server is waiting external connexion. . .");

        while (true)
        {
            try
            {
                var client = await _peer.TcpListener.AcceptTcpClientAsync();
                var externalPeer = new ExternalPeer(Guid.NewGuid().ToString(), client);
                _connectedPeers.Add(externalPeer);
                _ = HandleClientAsync(externalPeer);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                _logger.LogInformation("server restarting.");
                _peer.TcpListener.Stop();
                _peer.TcpListener.Dispose();
                _peer.TcpClient.Dispose();
                RunNode();
                throw;
            }
        }
        Console.ReadKey();
    }

    private async Task HandleClientAsync(ExternalPeer externalPeer)
    {
        var stream = externalPeer.Client.GetStream();
        var buffer = new byte[1024];
        
        int bytesRead;
        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
        {
            var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            _logger.LogInformation("Received message: " + message);
            var response = await HandleUriAsync(message);
            await SendStringToAClientAsync(externalPeer, response);
        }

        await SendStringToAClientAsync(externalPeer, "bye");
        _connectedPeers.Remove(externalPeer);
        _logger.LogWarning($"{externalPeer} has left.");
        
        externalPeer.Client.Close();
    }

    private async Task CreateConnectionWithPeer()
    {
        throw new NotImplementedException();
    }
    
    private async Task<string> HandleUriAsync(string uri)
    {
        try
        {
            var uriHandler = new UriHandler(uri);
            Console.WriteLine(uriHandler.ToString());

            if (handlers.TryGetValue(uriHandler.Command.ToLower(), out var handlerInfo))
            {
                var (type, method) = handlerInfo;
                var instance = Activator.CreateInstance(type);
                var response = (string)method.Invoke(instance, new object[] { uriHandler.PeerAddress });
                return response;
            }
            else
            {
                return "Unknown command";
            }
        }
        catch (Exception ex)
        {
            return "Error handling URI: " + ex.Message;
        }
    }
    private async Task SendStringToAClientAsync(ExternalPeer peer, string message)
    {
        var stream = peer.Client.GetStream();
        var buffer = Encoding.UTF8.GetBytes(message);
        await stream.WriteAsync(buffer, 0, buffer.Length);
    }
}