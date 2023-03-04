using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var corsOrigins = Environment.GetEnvironmentVariable("ALLOWED_ORIGINS");
Console.WriteLine($"ASPNETCORE_ENVIRONMENT: '{env}'");
Console.WriteLine($"Allowed CORS Hosts: '{corsOrigins ?? "*"}'");

// Configure services
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(_ =>_.AddFixedWindowLimiter("koiosRateLimiterPolicy", opt =>
{
    // configured according to https://api.koios.rest/#overview--limits
    opt.PermitLimit = 100;
    opt.Window = TimeSpan.FromSeconds(10);
    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    opt.QueueLimit = 0;
}));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "proxyCorsPolicy", policy =>
    {
        policy.WithOrigins(corsOrigins?.Split(',') ?? new[] { "*" });
    });
});

// Configure
var app = builder.Build();

app.UseCors();
app.UseRateLimiter();

app.MapReverseProxy();
app.MapGet("/", () => "Koios Reverse Proxy");

app.Run();
