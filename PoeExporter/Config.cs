using PoeData;

namespace PoeExporter;

/// <summary>
/// Class containing config data.
/// </summary>
public class Config : IConfig
{
    /// <inheritdoc/>
    public required string PoePath { get; init; }
}
