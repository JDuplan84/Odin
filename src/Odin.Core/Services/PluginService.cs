using Microsoft.Extensions.Logging;
using Odin.Core.Interfaces;
using Odin.Core.Models;

namespace Odin.Core.Services;

/// <summary>
/// Loads and manages Bethesda plugin files.
/// Mutagen API calls will be introduced in the Mutagen integration milestone.
/// </summary>
public sealed class PluginService : IPluginService
{
    private readonly ILogger<PluginService> _logger;

    // In-memory state for the currently active plugin session.
    // Will be replaced by a proper Mutagen overlay mod once integrated.
    private PluginFile? _activePlugin;
    private uint _nextFormId = 0x800;

    public PluginService(ILogger<PluginService> logger)
    {
        _logger = logger;
    }

    public Task<PluginFile> LoadPluginAsync(string path, CancellationToken ct = default)
    {
        _logger.LogInformation("Loading plugin: {Path}", path);

        if (!File.Exists(path))
            throw new FileNotFoundException($"Plugin not found: {path}", path);

        string name = Path.GetFileName(path);
        string ext = Path.GetExtension(path).ToLowerInvariant();

        var plugin = new PluginFile
        {
            Name = name,
            FullPath = path,
            Type = ext switch
            {
                ".esm" => PluginType.Esm,
                ".esl" => PluginType.Esl,
                _      => PluginType.Esp
            }
        };

        _activePlugin = plugin;
        _logger.LogInformation("Plugin loaded: {Name} ({Type})", plugin.Name, plugin.Type);
        return Task.FromResult(plugin);
    }

    public Task<IReadOnlyList<DialogueTopic>> GetDialogueTopicsAsync(CancellationToken ct = default)
    {
        // Stub — Mutagen enumeration wired in the next milestone.
        _logger.LogDebug("GetDialogueTopicsAsync called (stub)");
        return Task.FromResult<IReadOnlyList<DialogueTopic>>([]);
    }

    public Task<PluginFile> CreatePluginAsync(string name, IEnumerable<string> masterPaths, CancellationToken ct = default)
    {
        _logger.LogInformation("Creating new plugin: {Name}", name);

        var plugin = new PluginFile
        {
            Name = name,
            FullPath = Path.GetFullPath(name),
            Type = PluginType.Esp,
            Masters = masterPaths.Select(Path.GetFileName).Where(m => m is not null).Cast<string>().ToList()
        };

        _activePlugin = plugin;
        _nextFormId = 0x800;
        return Task.FromResult(plugin);
    }

    public Task AddMasterAsync(string masterPath, CancellationToken ct = default)
    {
        if (_activePlugin is null)
            throw new InvalidOperationException("No active plugin. Load or create a plugin first.");

        _logger.LogInformation("Adding master: {Master}", masterPath);
        // Mutagen overlay master list mutation goes here.
        return Task.CompletedTask;
    }

    public Task<uint> GetNextFormIdAsync(CancellationToken ct = default)
    {
        uint id = _nextFormId++;
        _logger.LogDebug("Allocated FormID: {FormId:X8}", id);
        return Task.FromResult(id);
    }

    public Task SaveActivePluginAsync(CancellationToken ct = default)
    {
        if (_activePlugin is null)
            throw new InvalidOperationException("No active plugin to save.");

        _logger.LogInformation("Saving plugin: {Path}", _activePlugin.FullPath);
        // Mutagen WriteToBinary goes here.
        return Task.CompletedTask;
    }
}
