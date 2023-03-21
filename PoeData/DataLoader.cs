using Serilog;
using System.Diagnostics;

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
        var timestampStart = Stopwatch.GetTimestamp();
        logger.Debug("loading data");

        var decompressedData = compressor.LoadAndDecompress();
        var offset = 0;

        var data = decompressedData.Data;

        (var bundleRecords, offset) = CreateBundleRecords(data, offset);

        logger.Verbose("loaded data in {elapsed}", Stopwatch.GetElapsedTime(timestampStart));
    }

    private (BundleRecord[] bundleRecords, int movedOffset) CreateBundleRecords(byte[] data, int offset)
    {
        var startTimestamp = Stopwatch.GetTimestamp();

        var bundleCount = BitConverter.ToInt32(data, offset);
        offset += sizeof(int);

        logger.Verbose("creating {bundleCount} bundle records", bundleCount);

        var bundleRecords = new BundleRecord[bundleCount];
        for (var i = 0; i < bundleCount; i++)
        {
            var (bundleRecord, bytesRead) = BundleRecord.Create(data, offset);
            offset += bytesRead;

            bundleRecords[i] = bundleRecord;
        }

        logger.Verbose("created bundle records in {elapsed}", Stopwatch.GetElapsedTime(startTimestamp));

        return (bundleRecords, offset);
    }
}
