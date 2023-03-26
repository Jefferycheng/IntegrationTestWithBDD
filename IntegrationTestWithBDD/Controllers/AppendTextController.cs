using IntegrationTestWithBDD.ApplicationService;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationTestWithBDD.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class AppendTextController : ControllerBase
{
    private readonly IAppendTextService _appendTextService;

    public AppendTextController(IAppendTextService appendTextService)
    {
        _appendTextService = appendTextService;
    }

    [HttpGet("append")]
    public async Task<IActionResult> Append([FromQuery] string text)
    {
        return Ok(_appendTextService.AppendText(text));
    }
}