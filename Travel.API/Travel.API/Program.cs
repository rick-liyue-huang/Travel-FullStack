using System.Reflection;
using Travel.API.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Travel.API.Database;
using Travel.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ITouristRouteRepository, TouristRouteRepository>();
// builder.Services.AddSingleton<>(); // log or app config
// builder.Services.AddTransient<>(); // light weight service

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

app.MapControllers(); // match AddControllers - Middleware

// 数据初始化
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    try
    {
        // 确保数据库存在
        context.Database.EnsureCreated();
        
        // 打印数据库路径
        var connectionString = context.Database.GetConnectionString();
        Console.WriteLine($"Database connection: {connectionString}");
        
        // 种子化 TouristRoutes
        if (!context.TouristRoutes.Any())
        {
            Console.WriteLine("No tourist routes found, seeding data...");
            
            // 尝试多种路径
            var possiblePaths = new[]
            {
                Path.Combine(Directory.GetCurrentDirectory(), "Database", "FakeRoutes.json"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "FakeRoutes.json"),
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Database", "FakeRoutes.json")
            };
            
            string jsonPath = null;
            foreach (var path in possiblePaths)
            {
                Console.WriteLine($"Checking path: {path}");
                if (File.Exists(path))
                {
                    jsonPath = path;
                    Console.WriteLine($"Found FakeRoutes.json at: {jsonPath}");
                    break;
                }
            }
            
            if (jsonPath == null)
            {
                Console.WriteLine("ERROR: Could not find FakeRoutes.json in any expected location");
                return;
            }
            
            var touristRouteJsonData = File.ReadAllText(jsonPath);
            Console.WriteLine($"Read JSON data, length: {touristRouteJsonData.Length}");
            
            var touristRoutes = JsonConvert.DeserializeObject<IList<TouristRoute>>(touristRouteJsonData);
            Console.WriteLine($"Deserialized {touristRoutes?.Count ?? 0} routes");
            
            if (touristRoutes != null && touristRoutes.Any())
            {
                context.TouristRoutes.AddRange(touristRoutes);
                var savedCount = await context.SaveChangesAsync();
                Console.WriteLine($"Successfully saved {savedCount} tourist routes");
            }
            else
            {
                Console.WriteLine("WARNING: No routes found in JSON file or deserialization failed");
            }
        }
        else
        {
            Console.WriteLine($"Database already contains {context.TouristRoutes.Count()} routes");
        }
        
        // 种子化 TouristRoutePictures
        if (!context.TouristRoutePictures.Any())
        {
            Console.WriteLine("No tourist route pictures found, seeding data...");
            
            // 尝试多种路径
            var possiblePaths = new[]
            {
                Path.Combine(Directory.GetCurrentDirectory(), "Database", "FakeRoutePics.json"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "FakeRoutePics.json"),
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Database", "FakeRoutePics.json")
            };
            
            string jsonPath = null;
            foreach (var path in possiblePaths)
            {
                Console.WriteLine($"Checking path: {path}");
                if (File.Exists(path))
                {
                    jsonPath = path;
                    Console.WriteLine($"Found FakeRoutePics.json at: {jsonPath}");
                    break;
                }
            }
            
            if (jsonPath == null)
            {
                Console.WriteLine("ERROR: Could not find FakeRoutePics.json in any expected location");
                // 不要 return，因为即使没有图片数据，应用程序也应该继续运行
            }
            else
            {
                var touristRoutePictureJsonData = File.ReadAllText(jsonPath);
                Console.WriteLine($"Read picture JSON data, length: {touristRoutePictureJsonData.Length}");
                
                var touristRoutePictures = JsonConvert.DeserializeObject<IList<TouristRoutePicture>>(touristRoutePictureJsonData);
                Console.WriteLine($"Deserialized {touristRoutePictures?.Count ?? 0} pictures");
                
                if (touristRoutePictures != null && touristRoutePictures.Any())
                {
                    // 验证外键关系
                    var validPictures = new List<TouristRoutePicture>();
                    var existingRouteIds = context.TouristRoutes.Select(r => r.Id).ToList();
                    
                    foreach (var picture in touristRoutePictures)
                    {
                        if (existingRouteIds.Contains(picture.TouristRouteId))
                        {
                            validPictures.Add(picture);
                        }
                        else
                        {
                            Console.WriteLine($"WARNING: Picture with TouristRouteId {picture.TouristRouteId} has no matching route");
                        }
                    }
                    
                    if (validPictures.Any())
                    {
                        context.TouristRoutePictures.AddRange(validPictures);
                        var savedCount = await context.SaveChangesAsync();
                        Console.WriteLine($"Successfully saved {savedCount} tourist route pictures");
                    }
                    else
                    {
                        Console.WriteLine("WARNING: No valid pictures to save (all have invalid TouristRouteId)");
                    }
                }
                else
                {
                    Console.WriteLine("WARNING: No pictures found in JSON file or deserialization failed");
                }
            }
        }
        else
        {
            Console.WriteLine($"Database already contains {context.TouristRoutePictures.Count()} pictures");
        }
        
        // 打印最终统计
        Console.WriteLine("\n=== Database Seeding Complete ===");
        Console.WriteLine($"Total Routes: {context.TouristRoutes.Count()}");
        Console.WriteLine($"Total Pictures: {context.TouristRoutePictures.Count()}");
        
        // 打印一些示例数据
        var sampleRoute = context.TouristRoutes.Include(r => r.TouristRoutePictures).FirstOrDefault();
        if (sampleRoute != null)
        {
            Console.WriteLine($"\nSample Route: {sampleRoute.Title}");
            Console.WriteLine($"Pictures Count: {sampleRoute.TouristRoutePictures?.Count ?? 0}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n=== ERROR during seeding ===");
        Console.WriteLine($"Error: {ex.Message}");
        Console.WriteLine($"Type: {ex.GetType().Name}");
        
        if (ex.InnerException != null)
        {
            Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
            Console.WriteLine($"Inner type: {ex.InnerException.GetType().Name}");
        }
        
        Console.WriteLine($"\nStack trace:\n{ex.StackTrace}");
    }
}

app.Run();

// dotnet-ef from nuget.org : dotnet tool install --global dotnet-ef
// dotnet ef migrations add InitialCreate --output-dir Data/Migrations
// dotnet ef migrations add InitialCreate
// dotnet ef database update


// docker pull postgres
// docker run --name my-postgres -e POSTGRES_PASSWORD=mysecretpassword -p 5432:5432 -d postgres
// docker ps -a 
// docker logs <containerId>