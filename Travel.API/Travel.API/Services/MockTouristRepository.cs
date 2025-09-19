using Travel.API.Models;

namespace Travel.API.Services;

public class MockTouristRepository : ITouristRouteRepository
{
    private List<TouristRoute> _touristRoutes;

    public MockTouristRepository()
    {
        if (_touristRoutes == null)
        {
            InitializeTouristRoutes();
        }
    }

    private IEnumerable<TouristRoute> InitializeTouristRoutes()
    {
        _touristRoutes = new List<TouristRoute>
        {
            new TouristRoute
            {
                Id = Guid.NewGuid(),
                Title = "Test",
                Description = "Test",
                OriginalPrice = 1000,
                DiscountPresent = 0.1,
                Features = "Test",
                Fees = "Test",
            },
            new TouristRoute
            {
                Id = Guid.NewGuid(),
                Title = "Test2",
                Description = "Test2",
                OriginalPrice = 1200,
                DiscountPresent = 0.2,
                Features = "Test2",
                Fees = "Test2",
            }
        };
        return _touristRoutes;
    }
    
    public IEnumerable<TouristRoute> GetTouristRoutes()
    {
        return _touristRoutes;
    }

    public TouristRoute GetTouristRoute(Guid touristRouteId)
    {
        // LINQ
        return _touristRoutes.FirstOrDefault(x => x.Id == touristRouteId);
    }
}