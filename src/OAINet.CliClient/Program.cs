using OAINet.CliClient;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<Client>();
        var builder = serviceCollection.BuildServiceProvider();
        var client = builder.GetService<Client>();
        // ...
    }
}