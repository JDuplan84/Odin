using Microsoft.AspNetCore.Mvc;
using Odin.Core.Interfaces;

namespace Odin.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PluginController : ControllerBase
{
    private readonly IPluginService _pluginService;
    private readonly ISkyrimEnvironmentService _environment;

    public PluginController(IPluginService pluginService, ISkyrimEnvironmentService environment)
    {
        _pluginService = pluginService;
        _environment   = environment;
    }

    /// <summary>Returns all plugin files found in the Skyrim Data folder.</summary>
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailablePlugins(CancellationToken ct)
    {
        var plugins = await _environment.GetAvailablePluginsAsync(ct);
        return Ok(plugins);
    }

    /// <summary>Loads a plugin into the active session.</summary>
    [HttpPost("load")]
    public async Task<IActionResult> LoadPlugin([FromBody] LoadPluginRequest request, CancellationToken ct)
    {
        var plugin = await _pluginService.LoadPluginAsync(request.Path, ct);
        return Ok(plugin);
    }

    /// <summary>Creates a new empty plugin.</summary>
    [HttpPost("create")]
    public async Task<IActionResult> CreatePlugin([FromBody] CreatePluginRequest request, CancellationToken ct)
    {
        var plugin = await _pluginService.CreatePluginAsync(request.Name, request.Masters, ct);
        return Ok(plugin);
    }

    /// <summary>Adds a master dependency to the active plugin.</summary>
    [HttpPost("add-master")]
    public async Task<IActionResult> AddMaster([FromBody] AddMasterRequest request, CancellationToken ct)
    {
        await _pluginService.AddMasterAsync(request.MasterPath, ct);
        return NoContent();
    }

    /// <summary>Saves the active plugin to disk.</summary>
    [HttpPost("save")]
    public async Task<IActionResult> Save(CancellationToken ct)
    {
        await _pluginService.SaveActivePluginAsync(ct);
        return NoContent();
    }
}

public record LoadPluginRequest(string Path);
public record CreatePluginRequest(string Name, IEnumerable<string> Masters);
public record AddMasterRequest(string MasterPath);
