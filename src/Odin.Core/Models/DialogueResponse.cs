namespace Odin.Core.Models;

/// <summary>
/// Represents a single dialogue response (INFO record) within a DIAL topic.
/// </summary>
public record DialogueResponse
{
    public required string FormId { get; init; }
    public string? ResponseText { get; init; }
    public string? VoiceType { get; init; }
    public string? ScriptNotes { get; init; }
    public int ResponseNumber { get; init; }
    public IReadOnlyList<DialogueCondition> Conditions { get; init; } = [];
}
