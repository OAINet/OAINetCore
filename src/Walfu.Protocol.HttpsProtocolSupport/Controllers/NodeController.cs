using Microsoft.AspNetCore.Mvc;

namespace Walfu.Protocol.HttpsProtocolSupport.Controllers;

[Controller]
[Route("api/node")]
public class NodeController : Controller
{
    [HttpGet("hi")]
    public async Task<IActionResult> HiFromNode()
    {
        return Ok(new
        {
            Content = "hi from node !"
        });
    }
}