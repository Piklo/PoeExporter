using Serilog;

namespace PoeData;

/// <summary>
/// Class used to load poe data.
/// </summary>
public sealed class DataLoader
{
    private readonly DataDecompressor compressor;
    private readonly ILogger logger;
    private readonly IConfig config;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataLoader"/> class.
    /// </summary>
    /// <param name="config">Contains config data.</param>
    /// <param name="logger">Contains logger used through the application.</param>
    public DataLoader(IConfig config, ILogger logger)
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
        compressor = new(this.logger) { PoePath = this.config.PoePath };
    }

    /// <summary>
    /// Loads data.
    /// </summary>
    public void LoadData()
    {
        var decompressedData = compressor.Decompress();
    }
}
