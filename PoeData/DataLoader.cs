using System.Text;

namespace PoeData;

public sealed class DataLoader
{

    public DataLoader(string clientPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(clientPath);

        IDataLoader dataLoader = IsStandaloneClient(clientPath) ? new StandaloneLoader(clientPath) : new SteamLoader(clientPath);

        var indexData = dataLoader.ReadIndex();
        var decompressed = Decompressor.Decompress(indexData);
        using var stream = new MemoryStream(decompressed.Data);
        using var reader = new BinaryReader(stream);
        var bundleRecords = ReadBundleRecords(reader);
        var fileRecords = ReadFileRecords(reader, bundleRecords);
        var directoryRecords = ReadDirectoryRecords(reader);

        var remainingDataLength = stream.Length - stream.Position;
        var remainingData = new byte[remainingDataLength];
        stream.ReadExactly(remainingData);

        var decompressedRemaining = Decompressor.Decompress(remainingData);
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
            var fileOffset = reader.ReadUInt32();
            var fileSize = reader.ReadUInt32();
            var record = new FileRecord() { Hash = hash, BundleRecord = bundleRecord, FileOffset = fileOffset, FileSize = fileSize };
            fileRecords.Add(record.Hash, record);
        }

        return fileRecords;
    }

    private sealed class FileRecord
    {
        public required ulong Hash { get; init; }
        public required BundleRecord BundleRecord { get; init; }
        public required uint FileOffset { get; init; }
        public required uint FileSize { get; init; }
    }

    private static DirectoryRecord[] ReadDirectoryRecords(BinaryReader reader)
    {
        var directoriesCount = reader.ReadInt32();

        var directories = new DirectoryRecord[directoriesCount];
        for (var i = 0; i < directories.Length; i++)
        {
            var hash = reader.ReadUInt64();
            var offset = reader.ReadUInt32();
            var size = reader.ReadUInt32();
            var unknown = reader.ReadUInt32();

            var directory = new DirectoryRecord() { Hash = hash, Offset = offset, Size = size, Unknown = unknown };
            directories[i] = directory;
        }

        return directories;
    }

    private sealed class DirectoryRecord
    {
        public required ulong Hash { get; init; }
        public required uint Offset { get; init; }
        public required uint Size { get; init; }
        public required uint Unknown { get; init; }
    }

    private sealed class PathedDirectoryRecord
    {
        public required byte[][] Paths { get; init; }
        public required DirectoryRecord DirectoryRecord { get; init; }
    }
}
