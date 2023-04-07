using Serilog;

namespace PoeData.Steam;

/// <summary>
/// Class used to load poe data from steam client.
/// </summary>
internal sealed class SteamDataLoader : IDataLoader
{
    private readonly ILogger logger;
    private readonly IConfig config;

    /// <summary>
    /// Initializes a new instance of the <see cref="SteamDataLoader"/> class.
    /// </summary>
    /// <param name="config">Contains config data.</param>
    /// <param name="logger">Contains logger used through the application.</param>
    public SteamDataLoader(IConfig config, ILogger logger)
    {
        if (config is null)
        {
            throw new ArgumentNullException(nameof(config));
        }

        if (logger is null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        this.config = config;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public byte[] ReadIndex()
    {
        var combinedPath = Path.Combine(config.PoePath, DataLoader.IndexPath);

        var bytes = File.ReadAllBytes(combinedPath);

        return bytes;
    }

    /// <inheritdoc/>
    public byte[] GetFileBytes(string filePath)
    {
        var combinedPath = Path.Combine(config.PoePath, filePath);
        var bytes = File.ReadAllBytes(combinedPath);
        return bytes;
    }
}
