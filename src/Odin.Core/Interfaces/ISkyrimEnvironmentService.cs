namespace Odin.Core.Interfaces;

/// <summary>
/// Detects Skyrim installation paths (Steam, registry, MO2) and enumerates available plugins.
/// </summary>
public interface ISkyrimEnvironmentService
{
    /// <summary>Returns the absolute path to the Skyrim SE Data folder.</summary>
    Task<string?> FindDataFolderAsync(CancellationToken ct = default);

    /// <summary>Enumerates all plugin files (.esm, .esp, .esl) in the Data folder.</summary>
    Task<IReadOnlyList<string>> GetAvailablePluginsAsync(CancellationToken ct = default);
}
