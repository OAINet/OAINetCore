namespace OAINet.Node.Network;

public class OAINetResponse
{
    public RequestStatus Stat { get; set; }
    public DateTime ResponseAt { get; set; }
    public object ResponseContent { get; set; }
}