

using Microsoft.AspNetCore.Mvc;

namespace Travel.API.Controllers;

[ApiController]
[Route("api/manualapi")]
public class ManualApiController : Controller
{
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }
}