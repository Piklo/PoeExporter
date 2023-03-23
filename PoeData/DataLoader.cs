using Serilog;
using System.Diagnostics;
using System.Text;

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

        (var directoryRecords, offset) = CreateDirectoryRecords(data, offset);

        var remainingData = data[offset..];
        var decompressedRemainingData = compressor.Decompress(remainingData);

        AddPathsToDirectoryRecords(directoryRecords, decompressedRemainingData);

        var fileToFind = Encoding.ASCII.GetBytes("Data/AdditionalLifeScaling.dat64"); // debug
        var file = GetFileRecord(fileRecords, fileToFind);

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

    private (Dictionary<ulong, DirectoryRecord> directoryRecords, int offset) CreateDirectoryRecords(byte[] data, int offset)
    {
        var startTimestamp = Stopwatch.GetTimestamp();

        (var directoryRecordsCount, offset) = BitConverterExtended.ToUInt32(data, offset);

        logger.Verbose("creating {directoryRecordsCount} directory records", directoryRecordsCount);

        var directoryRecords = new Dictionary<ulong, DirectoryRecord>();
        for (var i = 0; i < directoryRecordsCount; i++)
        {
            (var directoryRecord, offset) = DirectoryRecord.Create(data, offset);

            var success = directoryRecords.TryAdd(directoryRecord.Hash, directoryRecord);
            if (!success)
            {
                logger.Error("An item with the same key has already been added. Key: {Hash}", directoryRecord.Hash);
                throw new ArgumentException($"An item with the same key has already been added. Key: {directoryRecord.Hash}");
            }
        }

        logger.Verbose("created directory records in {elapsed}", Stopwatch.GetElapsedTime(startTimestamp));

        return (directoryRecords, offset);
    }

    private void AddPathsToDirectoryRecords(Dictionary<ulong, DirectoryRecord> directoryRecords, DecompressedData decompressedRemainingData)
    {
        var startTimestamp = Stopwatch.GetTimestamp();
        foreach (var directoryRecord in directoryRecords.Values)
        {
            var startingIndex = (int)directoryRecord.Offset;
            var endingIndex = (int)directoryRecord.Offset + (int)directoryRecord.Size;
            var relevantData = decompressedRemainingData.Data[startingIndex..endingIndex];

            var paths = MakePaths(relevantData);
            var newDirectoryRecord = new DirectoryRecord()
            {
                Hash = directoryRecord.Hash,
                Offset = directoryRecord.Offset,
                Size = directoryRecord.Size,
                Unknown = directoryRecord.Unknown,
                Paths = paths,
            };

            directoryRecords[directoryRecord.Hash] = newDirectoryRecord;
        }

        logger.Verbose("replaced directory records with records which include paths in {elapsed}", Stopwatch.GetElapsedTime(startTimestamp));
    }

    private static byte[][] MakePaths(byte[] data)
    {
        var temp = new List<byte[]>();
        var paths = new List<byte[]>();
        var isBase = false;
        var offset = 0;
        var rawlen = data.Length - 4; // why -4?

        while (offset <= rawlen)
        {
            (var index, offset) = BitConverterExtended.ToInt32(data, offset);

            if (index == 0)
            {
                isBase = !isBase;

                if (isBase)
                {
                    temp.Clear();
                }

                continue;
            }
            else
            {
                index--;
            }

            var endOffset = Array.FindIndex(data, offset, x => x == '\x00');
            var path = data[offset..endOffset];
            offset = endOffset + 1;

            if (temp.Count != 0)
            {
                var tempPath = temp[index];
                var mergedPath = new byte[tempPath.Length + path.Length];
                tempPath.CopyTo(mergedPath, 0);
                path.CopyTo(mergedPath, tempPath.Length);

                path = mergedPath;
            }

            if (isBase)
            {
                temp.Add(path);
            }
            else
            {
                paths.Add(path);
            }
        }

        return paths.ToArray();
    }

    private static ulong GetHash(byte[] path, PathTypes pathType)
    {
        var pathCopy = new byte[path.Length];
        path.CopyTo(pathCopy, 0);

        if (pathCopy[^1] == '/')
        {
            pathCopy = pathCopy[..^1];
        }

        if (pathType == PathTypes.File)
        {
            // to lower
            for (var i = 0; i < pathCopy.Length; i++)
            {
                var item = pathCopy[i];

                if (item >= 'A' && item <= 'Z')
                {
                    item += (byte)'a' - (byte)'A';
                    pathCopy[i] = item;
                }
            }
        }

        Array.Resize(ref pathCopy, pathCopy.Length + 2);
        pathCopy[^1] = (byte)'+';
        pathCopy[^2] = (byte)'+';

        var hash = Fnv.Fnv1a_64(pathCopy);
        return hash;
    }

    private static FileRecord GetFileRecord(Dictionary<ulong, FileRecord> fileRecords, byte[] path)
    {
        var hash = GetHash(path, PathTypes.File);

        var fileRecord = fileRecords[hash];

        return fileRecord;
    }
}
