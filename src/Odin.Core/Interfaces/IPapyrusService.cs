namespace Odin.Core.Interfaces;

/// <summary>
/// Handles Papyrus script compilation and decompilation (via Champollion / Caprica).
/// </summary>
public interface IPapyrusService
{
    /// <summary>Decompiles a .pex binary script to a .psc source file.</summary>
    Task<string> DecompileAsync(string pexPath, CancellationToken ct = default);

    /// <summary>Compiles a .psc source file to a .pex binary.</summary>
    Task<string> CompileAsync(string pscPath, CancellationToken ct = default);
}
