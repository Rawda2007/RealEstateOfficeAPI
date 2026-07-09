using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ErrorController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    [HttpGet("/error")]
    public IActionResult Error()
    {
        var exception =
            HttpContext.Features
            .Get<IExceptionHandlerFeature>();

        return StatusCode(500, new
        {
            Message = "Something went wrong"
        });
    }
}