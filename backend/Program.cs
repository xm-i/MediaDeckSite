using System.Threading.RateLimiting;
using MediaDeckSite.Data;
using MediaDeckSite.Options;
using MediaDeckSite.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

var wwwrootPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot");
var frontendDistPath = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "frontend", "dist"));
if (!Directory.Exists(wwwrootPath) && Directory.Exists(frontendDistPath))
{
    builder.Environment.WebRootPath = frontendDistPath;
    builder.Environment.WebRootFileProvider = new PhysicalFileProvider(frontendDistPath);
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<SlackOptions>(builder.Configuration.GetSection(SlackOptions.SectionName));
builder.Services.AddHttpClient<ISlackNotifier, SlackNotifier>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(15);
});

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        await context.HttpContext.Response.WriteAsJsonAsync(new { error = "Too Many Requests" }, cancellationToken);
    };

    options.AddPolicy("feedback", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: ClientIpResolver.GetClientIp(httpContext),
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0,
                AutoReplenishment = true,
            }));
});

builder.Services.AddControllers();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

app.UseForwardedHeaders();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRateLimiter();

app.MapControllers();

app.MapGet("/api/health", () => Results.Ok(new
{
    status = "ok",
    timestamp = DateTimeOffset.UtcNow,
}));

app.Map("/api/{**slug}", () => Results.NotFound(new { error = "Not Found" }));

app.MapFallbackToFile("index.html");

app.Run();
