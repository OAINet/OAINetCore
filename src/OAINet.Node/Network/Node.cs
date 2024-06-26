using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using OAINet.Node.Blockchain;
using OAINet.Node.RequestHandler;
using OAINet.Node.Services.Blockchain;

namespace OAINet.Node.Network;

public record Peer(TcpClient TcpClient, TcpListener TcpListener);

public record ExternalPeer(string Id, TcpClient Client);

public class Node
{
    private readonly ILogger<Node> _logger;
    private Peer? _peer;
    private List<ExternalPeer?>? _connectedPeers;
    private Dictionary<string, (Type, MethodInfo)> handlers = new Dictionary<string, (Type, MethodInfo)>();
    private readonly WalletService _walletService;
    private readonly Blockchain.Blockchain _blockchain;
    public Node(ILogger<Node> logger,
        WalletService walletService,
        Blockchain.Blockchain blockchain)
    {
        _blockchain = blockchain;
        _logger = logger;
        _walletService = walletService;
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
        _peer = InitializeNode(3024);
        await AcceptRequest();
        Console.ReadKey();
    }
    private Peer InitializeNode(int port)
    {
        _logger.LogInformation("server is preparing to run");
        var peer = new Peer(new TcpClient(),
            new TcpListener(IPAddress.Any, port));
        peer.TcpListener.Start();
        _connectedPeers = new List<ExternalPeer?>();
        var host = Dns.GetHostEntry(Dns.GetHostName());
        var ipAddress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        _logger.LogInformation("server is waiting external connexion in " + ipAddress?.ToString());
        return peer;
    }
    private async Task AcceptRequest()
    {
        if (_peer is null) throw new NullReferenceException(nameof(_peer));
        
        while (true)
        {
            try
            {
                TcpClient? client = await _peer.TcpListener.AcceptTcpClientAsync();
                var externalPeer = new ExternalPeer(Guid.NewGuid().ToString(), client);
                _connectedPeers?.Add(externalPeer);
                _ = HandleClientAsync(externalPeer);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                _logger.LogInformation("server restarting.");
                _peer.TcpListener.Stop();
                _peer.TcpListener.Dispose();
                _peer.TcpClient.Dispose();
                await RunNode();
                throw;
            }
        }
    }
    private async Task HandleClientAsync(ExternalPeer externalPeer)
    {
        var stream = externalPeer.Client.GetStream();
        var buffer = new byte[1024];
        var remoteEndPoint = externalPeer.Client.Client.RemoteEndPoint as IPEndPoint;
        _logger.LogInformation($"client from {remoteEndPoint?.Address} is connected");
        int bytesRead;
        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
        {
            string? message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            _logger.LogInformation("Received message: " + message);
            string? response = await HandleRequestAsync(message);
            if (response is null)
            {
                throw new NullReferenceException(nameof(response));
            }
            await SendStringToAClientAsync(externalPeer, response);
        }

        await SendStringToAClientAsync(externalPeer, "bye");
        _connectedPeers?.Remove(externalPeer);
        _logger.LogWarning($"{externalPeer} has left.");
        externalPeer.Client.Close();
    }
    private async Task<string?> HandleRequestAsync(string requestData)
    {
        // Why async ? It's only computation, isn't it ?
        try
        {
            var request = RequestParser.Parse(requestData);
            string? uri = request.Uri;
            if (uri is null) throw new NullReferenceException(nameof(uri));
            
            var uriHandler = new UriHandler(uri);
            Console.WriteLine(uriHandler.ToString());

            if (handlers.TryGetValue(uriHandler.Command.ToLower(), out var handlerInfo))
            {
                var (type, method) = handlerInfo;
                var instance = Activator.CreateInstance(type);
                var response = (string?)method.Invoke(instance, new object[] { request });
                return response;
            }
            else
            {
                return "Unknown command";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error handling request: {ex.Message}");
            return "Error handling request: " + ex.Message;
        }
    }
    private async Task<string?> HandleUriAsync(string uri)
    {
        // Why async ? It's only computation, isn't it ?
        try
        {
            var uriHandler = new UriHandler(uri);
            Console.WriteLine(uriHandler.ToString());

            if (handlers.TryGetValue(uriHandler.Command.ToLower(), out var handlerInfo))
            {
                var (type, method) = handlerInfo;
                var instance = Activator.CreateInstance(type);
                var response = (string?)method.Invoke(instance, new object[] { uriHandler.PeerAddress });
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