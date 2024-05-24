using Nethermind.Libp2p.Core;

namespace OAINet.Node.Network;

public class Protocol : IProtocol, IDisposable
{
    public Task DialAsync(IChannel downChannel, IChannelFactory? upChannelFactory, IPeerContext context)
    {
        throw new NotImplementedException();
    }

    public Task ListenAsync(IChannel downChannel, IChannelFactory? upChannelFactory, IPeerContext context)
    {
        throw new NotImplementedException();
    }

    public string Id { get; }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}