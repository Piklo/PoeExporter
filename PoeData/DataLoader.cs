using System.Text;
using PoeData.Hash;

namespace PoeData;

public sealed class DataLoader
{
    private readonly DirectoryRecord[] directoryRecords;
    private readonly Dictionary<ulong, FileRecord> _fileRecords;
    private readonly IDataLoader _dataLoader;
    private readonly Dictionary<ulong, PathedDirectoryRecord> _pathedDirectoryRecords;

    public DataLoader(string clientPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(clientPath);

        _dataLoader = IsStandaloneClient(clientPath) ? new StandaloneLoader(clientPath) : new SteamLoader(clientPath);

        var indexData = _dataLoader.ReadIndex();
        var decompressed = Decompressor.Decompress(indexData);
        using var stream = new MemoryStream(decompressed.Data);
        using var reader = new BinaryReader(stream);
        var bundleRecords = ReadBundleRecords(reader);
        _fileRecords = ReadFileRecords(reader, bundleRecords);
        directoryRecords = ReadDirectoryRecords(reader);

        var remainingDataLength = stream.Length - stream.Position;
        var remainingData = new byte[remainingDataLength];
        stream.ReadExactly(remainingData);

        var decompressedRemaining = Decompressor.Decompress(remainingData);
        _pathedDirectoryRecords = GetPathedDirectoryRecords(directoryRecords, decompressedRemaining);

        foreach (var (_, fileRecord) in _fileRecords)
        {
            if (fileRecord.BundleRecord.Name.Contains("acts.dat64", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine();
            }
        }
    }

    public byte[] GetFileBytes(string filePath)
    {
        var fileRecord = GetFileRecord(filePath);
        var bytes = _dataLoader.GetFileBytes(fileRecord.BundleRecord.GgpkPath);
        var decompressedData = Decompressor.Decompress(bytes);

        var start = fileRecord.FileOffset;
        var end = start + fileRecord.FileSize;
        var fileBytes = decompressedData.Data[start..end];

        return fileBytes;
    }

    private static bool IsStandaloneClient(string clientPath)
    {
        var fullPath = Path.Join(clientPath, StandaloneLoader.GgpkFileName);
        return Path.Exists(fullPath);
    }

    private static BundleRecord[] ReadBundleRecords(BinaryReader reader)
    {
        var bundleCount = reader.ReadUInt32();

        var records = new BundleRecord[bundleCount];
        for (var i = 0; i < records.Length; i++)
        {
            var nameLength = reader.ReadInt32();
            var nameBytes = reader.ReadBytes(nameLength);
            var name = Encoding.ASCII.GetString(nameBytes);
            var size = reader.ReadUInt32();
            var record = new BundleRecord() { Name = name, Size = size };
            records[i] = record;
        }

        return records;
    }

    private sealed class BundleRecord
    {
        public required string Name { get; init; }
        public required uint Size { get; init; }
        public string FileName => $"{Name}.bundle.bin";
        public string GgpkPath => $"Bundles2/{FileName}";
    }

    private static Dictionary<ulong, FileRecord> ReadFileRecords(BinaryReader reader, BundleRecord[] bundleRecords)
    {
        var fileCount = reader.ReadInt32();

        var fileRecords = new Dictionary<ulong, FileRecord>(fileCount);
        for (var i = 0; i < fileCount; i++)
        {
            var hash = reader.ReadUInt64();
            var bundleRecordsIndex = reader.ReadInt32();
            var bundleRecord = bundleRecords[bundleRecordsIndex];
            var fileOffset = reader.ReadInt32();
            var fileSize = reader.ReadInt32();
            var record = new FileRecord() { Hash = hash, BundleRecord = bundleRecord, FileOffset = fileOffset, FileSize = fileSize };
            fileRecords.Add(record.Hash, record);
        }

        return fileRecords;
    }

    private sealed class FileRecord
    {
        public required ulong Hash { get; init; }
        public required BundleRecord BundleRecord { get; init; }
        public required int FileOffset { get; init; }
        public required int FileSize { get; init; }
    }

    private static DirectoryRecord[] ReadDirectoryRecords(BinaryReader reader)
    {
        var directoriesCount = reader.ReadInt32();

        var directories = new DirectoryRecord[directoriesCount];
        for (var i = 0; i < directories.Length; i++)
        {
            var hash = reader.ReadUInt64();
            var offset = reader.ReadInt32();
            var size = reader.ReadInt32();
            var unknown = reader.ReadInt32();

            var directory = new DirectoryRecord() { Hash = hash, Offset = offset, Size = size, Unknown = unknown };
            directories[i] = directory;
        }

        return directories;
    }

    private sealed class DirectoryRecord
    {
        public required ulong Hash { get; init; }
        public required int Offset { get; init; }
        public required int Size { get; init; }
        public required int Unknown { get; init; }
    }

    private static Dictionary<ulong, PathedDirectoryRecord> GetPathedDirectoryRecords(DirectoryRecord[] directoryRecords, DecompressedData decompressedData)
    {
        var dict = new Dictionary<ulong, PathedDirectoryRecord>();

        foreach (var directoryRecord in directoryRecords)
        {
            var data = new ReadOnlySpan<byte>(decompressedData.Data, directoryRecord.Offset, directoryRecord.Size);
            var paths = MakePaths(data);

            var pathed = new PathedDirectoryRecord() { DirectoryRecord = directoryRecord, Paths = paths };
            dict.Add(directoryRecord.Hash, pathed);
        }

        return dict;
    }

    private static List<string> MakePaths(ReadOnlySpan<byte> data)
    {
        var temp = new List<string>();
        var paths = new List<string>();
        var isBase = false;
        var offset = 0;
        var rawLength = data.Length - 4;

        while (offset <= rawLength)
        {
            var index = BitConverter.ToInt32(data[offset..]);
            offset += sizeof(int);

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

            var endOffset = offset + data[offset..].IndexOf((byte)0);
            var pathBytes = data[offset..endOffset];
            var path = Encoding.UTF8.GetString(pathBytes);
            offset = endOffset + 1;

            if (temp.Count != 0)
            {
                var tempPath = temp[index];
                path = tempPath + path;
            }

            if (isBase)
            {
                temp.Add(path);
            }
            {
                paths.Add(path);
            }
        }

        return paths;
    }

    private sealed class PathedDirectoryRecord
    {
        public required List<string> Paths { get; init; }
        public required DirectoryRecord DirectoryRecord { get; init; }
    }

    private FileRecord GetFileRecord(string filePath)
    {
        var hash = GetHash(filePath);
        if (_fileRecords.TryGetValue(hash, out var fileRecord))
        {
            return fileRecord;
        }
        else
        {
            throw new InvalidOperationException($"""Failed to find file record for path "{filePath}" with hash = {hash}.""");
        }
    }

    private ulong GetHash(string path)
    {
        var rootEntry = directoryRecords[0];

        if (rootEntry.Hash == 0x07e47507b4a92e53)
        {
            return GetHashFnv(path);
        }

        var hash = GetMurmurHash(path, rootEntry.Hash);
        return hash;
    }

    private static ulong GetHashFnv(string path)
    {
        if (path.EndsWith('/'))
        {
            path = path[..^1];
        }

#pragma warning disable CA1308
        path = path.ToLowerInvariant();
#pragma warning restore CA1308
        path += "++";

        var hash = FNV.HashFNV1a(path);
        return hash;
    }

    private static ulong GetMurmurHash(string path, ulong seed)
    {
        seed ^= seed >> 47;
        seed *= 0x5F7A0EA7E59B19BD;
        seed ^= seed >> 47;

        if (path.EndsWith('/'))
        {
            path = path[..^1];
        }

#pragma warning disable CA1308
        path = path.ToLowerInvariant();
#pragma warning restore CA1308

        var hash = MurmurHash2.MurmurHash64A(path, seed);
        return hash;
    }
}
