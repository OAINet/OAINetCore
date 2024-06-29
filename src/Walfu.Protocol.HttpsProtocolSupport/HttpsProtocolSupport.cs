using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Walfu.Common.Interfaces;

namespace Walfu.Protocol.HttpsProtocolSupport;

public class HttpsProtocolSupport : IProtocolSupport
{
    private IWebHost _webHost;
    public void Start()
    {
        CreateHostBuilder(new []{""}).Build().Run(); ;
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseUrls("https://localhost:3024");
            });
    
    public void Stop()
    {
        throw new NotImplementedException();
    }
}