using Odin.Core.Extensions;
using Odin.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// ─── Services ────────────────────────────────────────────────────────────────

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Odin API", Version = "v1" });
});

// Odin.Core services (plugin, dialogue, papyrus, environment detection)
builder.Services.AddOdinCore();

// Bind Papyrus toolchain options from appsettings.json
builder.Services.Configure<PapyrusOptions>(
    builder.Configuration.GetSection(PapyrusOptions.Section));

// CORS — allows the Odin.Web frontend (dev server) to call the API
builder.Services.AddCors(options =>
{
    options.AddPolicy("OdinFrontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",  // Vite dev server
                "http://localhost:5174",
                "https://localhost:5001") // Odin.Web production
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// ─── Pipeline ────────────────────────────────────────────────────────────────

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("OdinFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
