using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Odin.Core.Interfaces;

namespace Odin.Core.Services;

/// <summary>
/// Wraps Champollion.exe (decompilation) and the Papyrus compiler (compilation).
/// Paths are injected via PapyrusOptions from appsettings.
/// </summary>
public sealed class PapyrusService : IPapyrusService
{
    private readonly PapyrusOptions _options;
    private readonly ILogger<PapyrusService> _logger;

    public PapyrusService(IOptions<PapyrusOptions> options, ILogger<PapyrusService> logger)
    {
        _options = options.Value;
        _logger  = logger;
    }

    public async Task<string> DecompileAsync(string pexPath, CancellationToken ct = default)
    {
        if (!File.Exists(pexPath))
            throw new FileNotFoundException("PEX file not found.", pexPath);

        string outputDir = Path.GetDirectoryName(pexPath)!;
        _logger.LogInformation("Decompiling {Pex} via Champollion", pexPath);

        var result = await RunProcessAsync(
            _options.ChampollionPath,
            $"\"{pexPath}\" -o \"{outputDir}\"",
            ct);

        string pscPath = Path.ChangeExtension(pexPath, ".psc");
        _logger.LogInformation("Decompilation output: {Psc}", pscPath);
        return pscPath;
    }

    public async Task<string> CompileAsync(string pscPath, CancellationToken ct = default)
    {
        if (!File.Exists(pscPath))
            throw new FileNotFoundException("PSC source file not found.", pscPath);

        _logger.LogInformation("Compiling {Psc}", pscPath);

        await RunProcessAsync(
            _options.CompilerPath,
            $"\"{pscPath}\" -i=\"{_options.ScriptSourcePath}\" -o=\"{_options.ScriptOutputPath}\" -f=\"{_options.FlagsFile}\"",
            ct);

        string pexPath = Path.ChangeExtension(pscPath, ".pex");
        return pexPath;
    }

    private async Task<string> RunProcessAsync(string executable, string arguments, CancellationToken ct)
    {
        using var process = new System.Diagnostics.Process
        {
            StartInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName               = executable,
                Arguments              = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError  = true,
                UseShellExecute        = false,
                CreateNoWindow         = true
            }
        };

        process.Start();
        string stdout = await process.StandardOutput.ReadToEndAsync(ct);
        string stderr = await process.StandardError.ReadToEndAsync(ct);
        await process.WaitForExitAsync(ct);

        if (process.ExitCode != 0)
        {
            _logger.LogError("Process exited with code {Code}: {Stderr}", process.ExitCode, stderr);
            throw new InvalidOperationException($"Process failed (exit {process.ExitCode}): {stderr}");
        }

        return stdout;
    }
}

/// <summary>Configuration block bound from appsettings.json "Papyrus" section.</summary>
public sealed class PapyrusOptions
{
    public const string Section = "Papyrus";

    public string ChampollionPath  { get; set; } = "Champollion.exe";
    public string CompilerPath     { get; set; } = "PapyrusCompiler.exe";
    public string ScriptSourcePath { get; set; } = "";
    public string ScriptOutputPath { get; set; } = "";
    public string FlagsFile        { get; set; } = "TESV_Papyrus_Flags.flg";
}
