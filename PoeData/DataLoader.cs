using PoeData.Ggpk;
using PoeData.Steam;
using Serilog;

namespace PoeData;

/// <summary>
/// Class used to load poe data.
/// </summary>
internal sealed class DataLoader : IDataLoader
{
    private readonly ILogger logger;
    private readonly IConfig config;
    private readonly IDataLoader dataLoader;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataLoader"/> class.
    /// </summary>
    /// <param name="config">Contains config data.</param>
    /// <param name="logger">Contains logger used through the application.</param>
    public DataLoader(IConfig config, ILogger logger)
    {
        this.logger = logger;
        this.config = config;

        if (IsStandaloneClient())
        {
            this.logger.Debug("detected standalone client");
            dataLoader = new GgpkLoader(config, logger);
        }
        else
        {
            this.logger.Debug("detected steam client");
            dataLoader = new SteamDataLoader(config, logger);
        }
    }

    /// <inheritdoc/>
    public byte[] GetFileBytes(string filePath)
    {
        return dataLoader.GetFileBytes(filePath);
    }

    private bool IsStandaloneClient()
    {
        var ggpkPath = Path.Combine(config.PoePath, "Content.ggpk");
        return Path.Exists(ggpkPath);
    }
}
