namespace Odin.Core.Models;

/// <summary>
/// Represents a loaded Bethesda plugin file (.esm, .esp, .esl).
/// </summary>
public record PluginFile
{
    public required string Name { get; init; }
    public required string FullPath { get; init; }
    public PluginType Type { get; init; }
    public IReadOnlyList<string> Masters { get; init; } = [];
}

public enum PluginType
{
    Esm,
    Esp,
    Esl
}
