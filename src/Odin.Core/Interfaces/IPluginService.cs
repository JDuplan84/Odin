using Odin.Core.Models;

namespace Odin.Core.Interfaces;

/// <summary>
/// Manages loading, querying, and saving Bethesda plugin files via Mutagen.
/// </summary>
public interface IPluginService
{
    /// <summary>Loads a plugin from disk and returns its metadata.</summary>
    Task<PluginFile> LoadPluginAsync(string path, CancellationToken ct = default);

    /// <summary>Returns all DIAL records from the currently loaded plugin.</summary>
    Task<IReadOnlyList<DialogueTopic>> GetDialogueTopicsAsync(CancellationToken ct = default);

    /// <summary>Creates a new empty plugin and sets it as the active target.</summary>
    Task<PluginFile> CreatePluginAsync(string name, IEnumerable<string> masterPaths, CancellationToken ct = default);

    /// <summary>Adds a master file dependency to the active plugin.</summary>
    Task AddMasterAsync(string masterPath, CancellationToken ct = default);

    /// <summary>Reserves and returns the next available FormID in the active plugin.</summary>
    Task<uint> GetNextFormIdAsync(CancellationToken ct = default);

    /// <summary>Persists the active plugin to disk.</summary>
    Task SaveActivePluginAsync(CancellationToken ct = default);
}
