namespace PoeExporter;

/// <summary>
/// Class containing config data.
/// </summary>
public class Config : IConfig
{
    /// <inheritdoc/>
    public required string PoePath { get; init; }

    /// <inheritdoc/>
    public required string Output { get; init; }

    /// <inheritdoc/>
    public int MinimumLoggerLevel { get; init; } = (int)Serilog.Events.LogEventLevel.Information;

    /// <inheritdoc/>
    public bool CacheDataLoader { get; init; } = true;
}
