using Microsoft.AspNetCore.Mvc;
using Odin.Core.Interfaces;
using Odin.Core.Models;

namespace Odin.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class DialogueController : ControllerBase
{
    private readonly IPluginService  _pluginService;
    private readonly IDialogueService _dialogueService;

    public DialogueController(IPluginService pluginService, IDialogueService dialogueService)
    {
        _pluginService   = pluginService;
        _dialogueService = dialogueService;
    }

    /// <summary>Returns all DIAL topics from the active plugin.</summary>
    [HttpGet("topics")]
    public async Task<IActionResult> GetTopics(CancellationToken ct)
    {
        var topics = await _pluginService.GetDialogueTopicsAsync(ct);
        return Ok(topics);
    }

    /// <summary>Creates a new DIAL topic in the active plugin.</summary>
    [HttpPost("topics")]
    public async Task<IActionResult> CreateTopic([FromBody] CreateTopicRequest request, CancellationToken ct)
    {
        var topic = await _dialogueService.CreateTopicAsync(
            request.EditorId,
            request.Type,
            request.TopicText,
            ct);

        return CreatedAtAction(nameof(GetTopics), new { formId = topic.FormId }, topic);
    }

    /// <summary>Adds an INFO response to an existing DIAL topic.</summary>
    [HttpPost("topics/{topicFormId}/responses")]
    public async Task<IActionResult> AddResponse(
        string topicFormId,
        [FromBody] AddResponseRequest request,
        CancellationToken ct)
    {
        var response = await _dialogueService.AddResponseAsync(
            topicFormId,
            request.ResponseText,
            request.ResponseNumber,
            ct);

        return Ok(response);
    }

    /// <summary>Adds a condition to an INFO response.</summary>
    [HttpPost("responses/{responseFormId}/conditions")]
    public async Task<IActionResult> AddCondition(
        string responseFormId,
        [FromBody] DialogueCondition condition,
        CancellationToken ct)
    {
        var result = await _dialogueService.AddConditionAsync(responseFormId, condition, ct);
        return Ok(result);
    }
}

public record CreateTopicRequest(string EditorId, DialogueType Type, string? TopicText);
public record AddResponseRequest(string ResponseText, int ResponseNumber);
