using Travel.API.Models;

namespace Travel.API.Services;

public interface ITouristRouteRepository
{
    IEnumerable<TouristRoute> GetTouristRoutes();
    TouristRoute GetTouristRoute(Guid touristRouteId);
}