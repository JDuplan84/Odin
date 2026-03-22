using Odin.Core.Models;

namespace Odin.Core.Interfaces;

/// <summary>
/// Creates and edits DIAL/INFO records in the active plugin.
/// </summary>
public interface IDialogueService
{
    Task<DialogueTopic> CreateTopicAsync(string editorId, DialogueType type, string? topicText = null, CancellationToken ct = default);
    Task<DialogueResponse> AddResponseAsync(string topicFormId, string responseText, int responseNumber, CancellationToken ct = default);
    Task<DialogueCondition> AddConditionAsync(string responseFormId, DialogueCondition condition, CancellationToken ct = default);
    Task<DialogueTopic> GetTopicAsync(string formId, CancellationToken ct = default);
}
