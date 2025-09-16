using Microsoft.AspNetCore.Mvc;

namespace Travel.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestAPIController : Controller
{
    [HttpGet]
    public IEnumerable<string> Get() => new string[] { "value1", "value2" };
    
    [HttpGet("{id}")]
    public string Get(int id) => "value";
    
    [HttpPost]
    public void Post([FromBody] string value) { }
    
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value) { }
    
    [HttpDelete("{id}")]
    public void Delete(int id) { }
}