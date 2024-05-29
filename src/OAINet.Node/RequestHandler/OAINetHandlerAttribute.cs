namespace OAINet.Node.RequestHandler;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class OAINetHandlerAttribute : Attribute
{
    public string Route { get; }

    public OAINetHandlerAttribute(string route)
    {
        Route = route;
    }
}