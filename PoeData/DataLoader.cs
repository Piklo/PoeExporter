using Serilog;
using System.Diagnostics;
using System.Text;

namespace PoeData;

/// <summary>
/// Class used to load poe data.
/// </summary>
internal sealed class DataLoader : IDataLoader
{
    private readonly DataDecompressor decompressor;
    private readonly ILogger logger;
    private readonly IConfig config;
    private readonly Dictionary<string, DecompressedData> decompressedFilesCache = new();
    private readonly BundleRecord[] bundleRecords;
    private readonly Dictionary<ulong, FileRecord> fileRecords;
    private readonly DirectoryRecord[] directoryRecords;
    private readonly Dictionary<ulong, DirectoryRecordWithPaths> directoryRecordsWithPaths;

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
        decompressor = new(this.logger) { PoePath = this.config.PoePath };

        // data loading
        var timestampStart = Stopwatch.GetTimestamp();
        logger.Debug("loading data");

        var indexBytes = decompressor.ReadIndex();
        var decompressedData = decompressor.Decompress(indexBytes);
        var data = decompressedData.Data;

        var offset = 0;

        (bundleRecords, offset) = CreateBundleRecords(data, offset);

        (fileRecords, offset) = CreateFileRecords(data, offset, bundleRecords);

        (directoryRecords, offset) = CreateDirectoryRecords(data, offset);

        var remainingData = data[offset..];
        var decompressedRemainingData = decompressor.Decompress(remainingData);

        directoryRecordsWithPaths = AddPathsToDirectoryRecords(directoryRecords, decompressedRemainingData);

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

    private (DirectoryRecord[] directoryRecords, int offset) CreateDirectoryRecords(byte[] data, int offset)
    {
        var startTimestamp = Stopwatch.GetTimestamp();

        (var directoryRecordsCount, offset) = BitConverterExtended.ToUInt32(data, offset);

        logger.Verbose("creating {directoryRecordsCount} directory records", directoryRecordsCount);

        var directoryRecords = new DirectoryRecord[directoryRecordsCount];
        for (var i = 0; i < directoryRecordsCount; i++)
        {
            (var directoryRecord, offset) = DirectoryRecord.Create(data, offset);
            directoryRecords[i] = directoryRecord;
        }

        logger.Verbose("created directory records in {elapsed}", Stopwatch.GetElapsedTime(startTimestamp));

        return (directoryRecords, offset);
    }

    private Dictionary<ulong, DirectoryRecordWithPaths> AddPathsToDirectoryRecords(DirectoryRecord[] directoryRecords, DecompressedData decompressedRemainingData)
    {
        var startTimestamp = Stopwatch.GetTimestamp();
        logger.Verbose("creating paths for {count} directory records", directoryRecords.Length);

        var dict = new Dictionary<ulong, DirectoryRecordWithPaths>();
        foreach (var directoryRecord in directoryRecords)
        {
            var startingIndex = (int)directoryRecord.Offset;
            var endingIndex = (int)directoryRecord.Offset + (int)directoryRecord.Size;
            var relevantData = decompressedRemainingData.Data[startingIndex..endingIndex];

            var paths = MakePaths(relevantData);
            var newDirectoryRecordWithPaths = new DirectoryRecordWithPaths(directoryRecord, paths);

            dict.Add(directoryRecord.Hash, newDirectoryRecordWithPaths);
        }

        logger.Verbose("created paths for directory records in {elapsed}", Stopwatch.GetElapsedTime(startTimestamp));

        return dict;
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

    /// <summary>
    /// Gets file record for a given path.
    /// </summary>
    /// <param name="path">path of a file record to get.</param>
    /// <returns><see cref="FileRecord"/>.</returns>
    internal FileRecord GetFileRecord(byte[] path)
    {
        var hash = GetHash(path, PathTypes.File);

        var fileRecord = fileRecords[hash];

        return fileRecord;
    }

    /// <inheritdoc/>
    public byte[] GetFileBytes(string filePath)
    {
        var pathBytes = Encoding.ASCII.GetBytes(filePath);
        var fileRecord = GetFileRecord(pathBytes);

        var decompressedData = GetDecompressedData(fileRecord.BundleRecord);

        var start = (int)fileRecord.FileOffset;
        var end = start + (int)fileRecord.FileSize;
        var fileBytes = decompressedData[start..end];

        return fileBytes;
    }

    private byte[] GetDecompressedData(BundleRecord bundleRecord)
    {
        if (decompressedFilesCache.TryGetValue(bundleRecord.Name, out var value))
        {
            return value.Data;
        }
        else
        {
            var combinedPath = Path.Combine(config.PoePath, bundleRecord.GgpkPath);
            var bytes = decompressor.ReadFile(combinedPath);
            var decompressed = decompressor.Decompress(bytes);
            decompressedFilesCache.Add(bundleRecord.Name, decompressed);

            return decompressed.Data;
        }
    }
}
