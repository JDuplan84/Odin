using Microsoft.Extensions.DependencyInjection;
using Odin.Core.Interfaces;
using Odin.Core.Services;

namespace Odin.Core.Extensions;

/// <summary>
/// Registers all Odin.Core services into the DI container.
/// Call <c>services.AddOdinCore()</c> from the API or Web host startup.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOdinCore(this IServiceCollection services)
    {
        // Environment detection — singleton: stateless after first resolve.
        services.AddSingleton<ISkyrimEnvironmentService, SkyrimEnvironmentService>();

        // Plugin session — singleton: holds in-memory active plugin state.
        // Replace with Scoped once multi-user / multi-session support is needed.
        services.AddSingleton<IPluginService, PluginService>();

        // Dialogue editing — transient: stateless facade over IPluginService.
        services.AddTransient<IDialogueService, DialogueService>();

        // Papyrus toolchain — transient: wraps external process calls.
        services.AddTransient<IPapyrusService, PapyrusService>();

        return services;
    }
}
