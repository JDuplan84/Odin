using Microsoft.AspNetCore.Mvc;
using Odin.Core.Interfaces;

namespace Odin.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ScriptController : ControllerBase
{
    private readonly IPapyrusService _papyrus;

    public ScriptController(IPapyrusService papyrus)
    {
        _papyrus = papyrus;
    }

    /// <summary>Decompiles a compiled Papyrus .pex binary to a .psc source file.</summary>
    [HttpPost("decompile")]
    public async Task<IActionResult> Decompile([FromBody] DecompileRequest request, CancellationToken ct)
    {
        string pscPath = await _papyrus.DecompileAsync(request.PexPath, ct);
        string source  = await System.IO.File.ReadAllTextAsync(pscPath, ct);
        return Ok(new { pscPath, source });
    }

    /// <summary>Compiles a .psc Papyrus source file to a .pex binary.</summary>
    [HttpPost("compile")]
    public async Task<IActionResult> Compile([FromBody] CompileRequest request, CancellationToken ct)
    {
        string pexPath = await _papyrus.CompileAsync(request.PscPath, ct);
        return Ok(new { pexPath });
    }
}

public record DecompileRequest(string PexPath);
public record CompileRequest(string PscPath);
