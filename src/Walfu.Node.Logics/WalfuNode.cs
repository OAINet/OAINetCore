using Microsoft.Extensions.DependencyInjection;
using OAINet.Common.Interfaces;

namespace Walfu.Node.Logics;

public class WalfuNode
{
    private readonly IEnumerable<IProtocolSupport> _protocols;

    public WalfuNode(IEnumerable<IProtocolSupport> protocols)
    {
        _protocols = protocols;
    }
    public void RunNode()
    {
        RunProtocols();
    }

    private void RunProtocols()
    {
        foreach (var protocol in _protocols)
        {
            protocol.Start();
        }
    }
}