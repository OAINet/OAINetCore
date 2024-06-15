using System.Net;

namespace OAINet.Node.Network;

public static class ResponseType
{
    public static OAINetResponse OAINetSucces(
        this RequestHandler.RequestHandler requestHandler,
        object response)
    {
        return new OAINetResponse()
        {
            ResponseAt = DateTime.Now,
            ResponseContent = response,
            Stat = RequestStatus.NetSuccess
        };
    }
}