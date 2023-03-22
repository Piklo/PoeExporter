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

        (var fileRecords, offset) = CreateFileRecords(data, offset, bundleRecords);

        logger.Verbose("loaded data in {elapsed}", Stopwatch.GetElapsedTime(timestampStart));
    }

    private (BundleRecord[] bundleRecords, int movedOffset) CreateBundleRecords(byte[] data, int offset)
    {
        var startTimestamp = Stopwatch.GetTimestamp();

        (var bundleCount, offset) = BitConverterExtended.ToUInt32(data, offset);

        logger.Verbose("creating {bundleCount} bundle records", bundleCount);

        var bundleRecords = new BundleRecord[bundleCount];
        for (var i = 0; i < bundleCount; i++)
        {
            (var bundleRecord, offset) = BundleRecord.Create(data, offset);

            bundleRecords[i] = bundleRecord;
        }

        logger.Verbose("created bundle records in {elapsed}", Stopwatch.GetElapsedTime(startTimestamp));

        return (bundleRecords, offset);
    }

    private (Dictionary<ulong, FileRecord> fileRecords, int movedOffset) CreateFileRecords(byte[] data, int offset, BundleRecord[] bundleRecords)
    {
        var startTimestamp = Stopwatch.GetTimestamp();

        (var fileCount, offset) = BitConverterExtended.ToUInt32(data, offset);

        logger.Verbose("creating {fileRecordsCount} file records", fileCount);

        var fileRecords = new Dictionary<ulong, FileRecord>();
        for (var i = 0; i < fileCount; i++)
        {
            (var fileRecord, offset) = FileRecord.Create(data, offset, bundleRecords);

            var success = fileRecords.TryAdd(fileRecord.Hash, fileRecord);
            if (!success)
            {
                logger.Error("An item with the same key has already been added. Key: {Hash}", fileRecord.Hash);
                throw new ArgumentException($"An item with the same key has already been added. Key: {fileRecord.Hash}");
            }
        }

        logger.Verbose("created file records in {elapsed}", Stopwatch.GetElapsedTime(startTimestamp));

        return (fileRecords, offset);
    }
}
