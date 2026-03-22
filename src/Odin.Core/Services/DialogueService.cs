using Microsoft.Extensions.Logging;
using Odin.Core.Interfaces;
using Odin.Core.Models;

namespace Odin.Core.Services;

public sealed class DialogueService : IDialogueService
{
    private readonly IPluginService _pluginService;
    private readonly ILogger<DialogueService> _logger;

    public DialogueService(IPluginService pluginService, ILogger<DialogueService> logger)
    {
        _pluginService = pluginService;
        _logger = logger;
    }

    public async Task<DialogueTopic> CreateTopicAsync(
        string editorId,
        DialogueType type,
        string? topicText = null,
        CancellationToken ct = default)
    {
        uint formId = await _pluginService.GetNextFormIdAsync(ct);

        var topic = new DialogueTopic
        {
            FormId  = $"{formId:X8}",
            EditorId = editorId,
            Type     = type,
            TopicText = topicText
        };

        _logger.LogInformation("Created DIAL topic {EditorId} [{FormId}]", editorId, topic.FormId);
        // Mutagen mod.DialogTopics.AddNew() goes here.
        return topic;
    }

    public async Task<DialogueResponse> AddResponseAsync(
        string topicFormId,
        string responseText,
        int responseNumber,
        CancellationToken ct = default)
    {
        uint formId = await _pluginService.GetNextFormIdAsync(ct);

        var response = new DialogueResponse
        {
            FormId         = $"{formId:X8}",
            ResponseText   = responseText,
            ResponseNumber = responseNumber
        };

        _logger.LogInformation("Added INFO response {FormId} to topic {TopicFormId}", response.FormId, topicFormId);
        return response;
    }

    public Task<DialogueCondition> AddConditionAsync(
        string responseFormId,
        DialogueCondition condition,
        CancellationToken ct = default)
    {
        _logger.LogInformation("Adding condition {Function} to response {FormId}", condition.Function, responseFormId);
        return Task.FromResult(condition);
    }

    public Task<DialogueTopic> GetTopicAsync(string formId, CancellationToken ct = default)
    {
        // Stub — Mutagen lookup wired in next milestone.
        throw new KeyNotFoundException($"Topic {formId} not found (Mutagen integration pending).");
    }
}
