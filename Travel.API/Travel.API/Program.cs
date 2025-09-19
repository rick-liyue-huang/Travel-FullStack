using Travel.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ITouristRouteRepository, MockTouristRepository>();
// builder.Services.AddSingleton<>(); // log or app config
// builder.Services.AddTransient<>(); // light weight service

var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

app.MapControllers(); // match AddControllers - Middleware

app.Run();