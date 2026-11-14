using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

var wwwrootPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot");
var frontendDistPath = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "frontend", "dist"));
if (!Directory.Exists(wwwrootPath) && Directory.Exists(frontendDistPath))
{
    builder.Environment.WebRootPath = frontendDistPath;
    builder.Environment.WebRootFileProvider = new PhysicalFileProvider(frontendDistPath);
}

builder.Services.AddControllers();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

app.UseForwardedHeaders();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.MapGet("/api/health", () => Results.Ok(new
{
    status = "ok",
    timestamp = DateTimeOffset.UtcNow,
}));

app.Map("/api/{**slug}", () => Results.NotFound(new { error = "Not Found" }));

app.MapFallbackToFile("index.html");

app.Run();
