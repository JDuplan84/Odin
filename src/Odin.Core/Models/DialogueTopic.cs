namespace Odin.Core.Models;

/// <summary>
/// Represents a Skyrim dialogue topic (DIAL record).
/// </summary>
public record DialogueTopic
{
    public required string FormId { get; init; }
    public required string EditorId { get; init; }
    public string? TopicText { get; init; }
    public DialogueType Type { get; init; }
    public IReadOnlyList<DialogueResponse> Responses { get; init; } = [];
}

public enum DialogueType
{
    Topic,
    Greeting,
    Persuasion,
    Detection,
    Service,
    Miscellaneous,
    Combat,
    Favors,
    Scene,
    Quest,
    Player
}
