var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Serve static files from wwwroot (index.html, css/, js/, components/)
app.UseDefaultFiles();
app.UseStaticFiles();

// SPA fallback: any unknown route returns index.html so client-side routing works
app.MapFallbackToFile("index.html");

app.Run();
