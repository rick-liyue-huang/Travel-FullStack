using Travel.API.Database;
using Travel.API.Models;

namespace Travel.API.Services;

public class TouristRouteRepository(AppDbContext appDbContext) : ITouristRouteRepository
{
    AppDbContext _appDbContext = appDbContext;
    public IEnumerable<TouristRoute> GetTouristRoutes()
    {
        return _appDbContext.TouristRoutes;
    }

    public TouristRoute GetTouristRoute(Guid touristRouteId)
    {
        return _appDbContext.TouristRoutes.FirstOrDefault(r => r.Id == touristRouteId);
    }
}