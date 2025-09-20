using Microsoft.EntityFrameworkCore;
using Travel.API.Models;

namespace Travel.API.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TouristRoute> TouristRoutes { get; set; }
    public DbSet<TouristRoutePicture> TouristRoutePictures { get; set; }
}