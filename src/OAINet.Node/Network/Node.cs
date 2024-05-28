using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Extensions.Logging;
using Nethermind.Libp2p.Core;
using Nethermind.Libp2p.Stack;

namespace OAINet.Node.Network;

public record Peer(TcpClient TcpClient, TcpListener TcpListener);

public class Node
{
    private readonly ILogger<Node> _logger;
    private Peer _peer;
    private List<TcpClient> _connectedPeers;

    public Node(ILogger<Node> logger)
    {
        _logger = logger;
        
    }

    public async Task RunNode()
    {
        _logger.LogInformation("server is preparing to run");
        _peer = new Peer(new TcpClient(),
            new TcpListener(IPAddress.Any, 3024));
        _peer.TcpListener.Start();
        _connectedPeers = new List<TcpClient>();
        _logger.LogInformation("server is waiting external connexion. . .");

        while (true)
        {
            try
            {
                var client = await _peer.TcpListener.AcceptTcpClientAsync();
                _connectedPeers.Add(client);
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
}