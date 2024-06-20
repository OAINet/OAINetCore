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
    
    public static OAINetResponse OAINetInvalidFormat(
        this RequestHandler.RequestHandler requestHandler)
    {
        return new OAINetResponse()
        {
            ResponseAt = DateTime.Now,
            ResponseContent = new
            {
                Message = "not good format."
            },
            Stat = RequestStatus.NetInvalidFormat
        };
    }
    
    public static OAINetResponse OAINetInternalError(
        this RequestHandler.RequestHandler requestHandler,
        Exception exception)
    {
        return new OAINetResponse()
        {
            ResponseAt = DateTime.Now,
            ResponseContent = new
            {
                Message = exception.Message,
            },
            Stat = RequestStatus.NetInternalError
        };
    }
    
    public static OAINetResponse OAINetInternalError(
        this RequestHandler.RequestHandler requestHandler,
        string error)
    {
        return new OAINetResponse()
        {
            ResponseAt = DateTime.Now,
            ResponseContent = new
            {
                Message = error
            },
            Stat = RequestStatus.NetInternalError
        };
    }
    
    public static OAINetResponse OAINetNotFound(
        this RequestHandler.RequestHandler requestHandler,
        string endpoint)
    {
        return new OAINetResponse()
        {
            ResponseAt = DateTime.Now,
            ResponseContent = new
            {
                Message = endpoint + "is not support by the oainet protocol"
            },
            Stat = RequestStatus.NetNotFound
        };
    }
}