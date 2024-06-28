using Microsoft.AspNetCore.Mvc;

namespace Walfu.Protocol.HttpsProtocolSupport.Controllers;

[Route("api/walfu")]
public class WalfuController : Controller
{
    [HttpGet("stats")]
    public async Task<IActionResult> GetWalfuProtocolStats()
    {
        return Ok(new
        {
            /*
            WalfuVersion = 0.2,
            WebSocketPort = 3025,
            HttpsPort = 3024,
            BlockNumber = 0,
            */
        });
    }
    
    
}