using Microsoft.Extensions.Logging;
using Odin.Core.Interfaces;

namespace Odin.Core.Services;

/// <summary>
/// Detects Skyrim SE installation via Windows registry and Steam library folders.
/// Full Mutagen integration will be wired here during the Mutagen milestone.
/// </summary>
public sealed class SkyrimEnvironmentService : ISkyrimEnvironmentService
{
    private readonly ILogger<SkyrimEnvironmentService> _logger;

    public SkyrimEnvironmentService(ILogger<SkyrimEnvironmentService> logger)
    {
        _logger = logger;
    }

    public Task<string?> FindDataFolderAsync(CancellationToken ct = default)
    {
        _logger.LogInformation("Searching for Skyrim SE Data folder...");

        // Windows registry lookup (HKLM\SOFTWARE\WOW6432Node\Bethesda Softworks\Skyrim Special Edition)
        string? path = TryRegistryPath();

        if (path is null)
            _logger.LogWarning("Skyrim SE installation not found via registry. Steam library scan pending.");

        return Task.FromResult(path);
    }

    public async Task<IReadOnlyList<string>> GetAvailablePluginsAsync(CancellationToken ct = default)
    {
        string? dataFolder = await FindDataFolderAsync(ct);

        if (dataFolder is null || !Directory.Exists(dataFolder))
            return [];

        var plugins = Directory.EnumerateFiles(dataFolder, "*.*", SearchOption.TopDirectoryOnly)
            .Where(f =>
            {
                string ext = Path.GetExtension(f).ToLowerInvariant();
                return ext is ".esm" or ".esp" or ".esl";
            })
            .OrderBy(f => f)
            .ToList();

        _logger.LogInformation("Found {Count} plugins in {DataFolder}", plugins.Count, dataFolder);
        return plugins;
    }

    private static string? TryRegistryPath()
    {
        if (!OperatingSystem.IsWindows())
            return null;

        try
        {
            using var key = Microsoft.Win32.Registry.LocalMachine
                .OpenSubKey(@"SOFTWARE\WOW6432Node\Bethesda Softworks\Skyrim Special Edition");

            return key?.GetValue("Installed Path") is string installPath
                ? Path.Combine(installPath, "Data")
                : null;
        }
        catch
        {
            return null;
        }
    }
}
