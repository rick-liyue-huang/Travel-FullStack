using Microsoft.AspNetCore.Mvc;
using Travel.API.Services;

namespace Travel.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TouristRoutesController(ITouristRouteRepository touristRouteRepository) : ControllerBase
{
    private readonly ITouristRouteRepository _touristRouteRepository = touristRouteRepository;

    [HttpGet]   
    public IActionResult GetTouristRoutes()
    {
        return Ok(_touristRouteRepository.GetTouristRoutes());
    }
}