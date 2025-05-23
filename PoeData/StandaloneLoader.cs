using System.Security.Cryptography;
using System.Text;

namespace PoeData;

internal sealed class StandaloneLoader : IDataLoader
{
    public const string GgpkFileName = "Content.ggpk";
    private const string IndexPath = "Bundles2/_.index.bin";
    private const int LengthLength = sizeof(int);
    private const int TagLength = 4;

    const int CharWidth = 2;
    const int FileNameLengthLength = sizeof(int);
    private const int FileHeaderLength = LengthLength + TagLength + FileNameLengthLength + SHA256.HashSizeInBytes; // + nameLength

    private readonly string _ggpkPath;
    private readonly DirectoryNode _rootNode;
    public StandaloneLoader(string clientPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(clientPath);

        _ggpkPath = Path.Combine(clientPath, GgpkFileName);

        using var file = File.OpenRead(_ggpkPath);
        using var reader = new BinaryReader(file);

        var record = ReadRecord(reader);
        if (record is not Ggpk ggpk)
        {
            throw new InvalidOperationException($"Expected {nameof(Ggpk)} at position {record.Position}.");
        }

        var records = new Dictionary<long, IRecord> { { ggpk.Position, ggpk } };

        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            record = ReadRecord(reader);
            records.Add(record.Position, record);
        }

        _rootNode = BuildDirectoryStructure(ggpk, records);
    }

    public byte[] ReadIndex()
    {
        return GetFileBytes(IndexPath);
    }

    public byte[] GetFileBytes(string path)
    {
        var split = path.Replace("\\", "/", StringComparison.Ordinal).Split("/");

        var currentNode = _rootNode;
        foreach (var currentPath in split)
        {
            var foundCurrent = false;
            foreach (var node in currentNode.Children)
            {
                if (currentPath != node.Record.Name())
                {
                    continue;
                }
                currentNode = node;
                foundCurrent = true;
                break;
            }

            if (!foundCurrent)
            {
                throw new InvalidOperationException($"Failed to find {currentPath}.");
            }
        }

        if (split[^1] != currentNode.Record.Name())
        {
            throw new InvalidOperationException($"Traversed to the end and failed to find {path}.");
        }

        var file = currentNode.Record.File;
        if (file is null)
        {
            throw new InvalidOperationException($"{path} is not a file.");
        }

        var totalHeaderLength = FileHeaderLength + file.NameLength * CharWidth;
        var dataStart = file.Position + totalHeaderLength;
        var dataLength = file.Length - totalHeaderLength;

        using var stream = File.OpenRead(_ggpkPath);
        stream.Seek(dataStart, SeekOrigin.Begin);

        var bytes = new byte[dataLength];
        stream.ReadExactly(bytes);

        return bytes;
    }


    private static GgpkEntry ReadGgpkEntry(BinaryReader reader)
    {
        var offset = reader.ReadInt64();
        var entry = new GgpkEntry { Offset = offset };
        return entry;
    }

    private static IRecord ReadRecord(BinaryReader reader)
    {
        var position = reader.BaseStream.Position;
        var length = reader.ReadInt32();

        var tagBytes = reader.ReadBytes(TagLength);
        var tag = Encoding.ASCII.GetString(tagBytes);

        return tag switch
        {
            "GGPK" => ReadGgpkRecord(reader, position, length, tag),
            "FILE" => ReadFile(reader, position, length, tag),
            "FREE" => ReadFree(reader, position, length, tag),
            "PDIR" => ReadPDir(reader, position, length, tag),
            _ => throw new InvalidOperationException($"Unknown tag = {tag}."),
        };
    }

    private static Ggpk ReadGgpkRecord(BinaryReader reader, long position, int length, string tag)
    {
        var version = reader.ReadInt32();

        const int EntriesCount = 2;
        var entries = new GgpkEntry[EntriesCount];
        for (var i = 0; i < EntriesCount; i++)
        {
            var entry = ReadGgpkEntry(reader);
            entries[i] = entry;
        }

        var ggpk = new Ggpk { Position = position, Length = length, Tag = tag, Version = version, Entries = entries };

        return ggpk;
    }

    private static FileEntry ReadFile(BinaryReader reader, long position, int length, string tag)
    {
        var nameBytesLength = reader.ReadInt32();
        var hash = reader.ReadBytes(SHA256.HashSizeInBytes);

        var nameLength = CharWidth * nameBytesLength;
        var nameBytes = reader.ReadBytes(nameLength - CharWidth);
        reader.BaseStream.Seek(CharWidth, SeekOrigin.Current);
        var name = Encoding.Unicode.GetString(nameBytes);

        var file = new FileEntry() { Position = position, Length = length, Tag = tag, NameLength = nameBytesLength, Sha256Hash = hash, Name = name };

        var remainder = FileHeaderLength + nameLength;
        var skip = length - remainder;
        reader.BaseStream.Seek(skip, SeekOrigin.Current);
        return file;
    }

    private static Free ReadFree(BinaryReader reader, long position, int length, string tag)
    {
        var free = new Free() { Position = position, Length = length, Tag = tag };

        const int Remainder = LengthLength + TagLength;
        var skip = length - Remainder;
        reader.BaseStream.Seek(skip, SeekOrigin.Current);

        return free;
    }

    private static Pdir ReadPDir(BinaryReader reader, long position, int length, string tag)
    {
        var nameBytesLength = reader.ReadInt32();
        var totalEntries = reader.ReadInt32();
        var hash = reader.ReadBytes(SHA256.HashSizeInBytes);

        var nameLength = CharWidth * nameBytesLength;
        var nameBytes = reader.ReadBytes(nameLength - CharWidth);
        reader.BaseStream.Seek(CharWidth, SeekOrigin.Current);
        var name = Encoding.Unicode.GetString(nameBytes);

        var entries = new DirectoryEntry[totalEntries];
        for (var i = 0; i < totalEntries; i++)
        {
            var entry = ReadDirectoryEntry(reader);
            entries[i] = entry;
        }

        var pdir = new Pdir()
            { Position = position, Length = length, Tag = tag, NameLength = nameBytesLength, TotalEntries = totalEntries, Sha256Hash = hash, Name = name, Entries = entries };

        return pdir;
    }

    private static DirectoryEntry ReadDirectoryEntry(BinaryReader reader)
    {
        var entryNameHash = reader.ReadInt32();
        var offset = reader.ReadInt64();

        var entry = new DirectoryEntry() { EntryNameHash = entryNameHash, Offset = offset };

        return entry;
    }

    private static DirectoryNode BuildDirectoryStructure(Ggpk ggpk, IReadOnlyDictionary<long, IRecord> records)
    {
        var rootNode = GetRootDirectoryNode(ggpk, records);
        TraverseDirectories(rootNode, records);
        return rootNode;
    }

    private static DirectoryNode GetRootDirectoryNode(Ggpk ggpk, IReadOnlyDictionary<long, IRecord> records)
    {
        Pdir? pdir = null;
        var pdirsCount = 0;
        foreach (var entry in ggpk.Entries)
        {
            if (!records.TryGetValue(entry.Offset, out var record))
            {
                throw new InvalidOperationException($"Failed to find record with offset: {entry.Offset}.");
            }

            if (record is not Pdir pdir2)
            {
                continue;
            }

            pdir = pdir2;
            pdirsCount++;
        }

        if (pdirsCount != 1)
        {
            throw new InvalidOperationException($"Expected 1 pdir, found: {pdirsCount}.");
        }

        if (pdir is null)
        {
            throw new InvalidOperationException($"{nameof(pdir)} is null.");
        }

        var rootNode = new DirectoryNode() { Parent = null, Record = new(pdir) };
        return rootNode;
    }

    private static void TraverseDirectories(DirectoryNode rootNode, IReadOnlyDictionary<long, IRecord> records)
    {
        var queue = new Queue<(DirectoryNode parent, DirectoryEntry child)>();
        foreach (var entry in rootNode.Record.Directory?.Entries ?? [])
        {
            queue.Enqueue((rootNode, entry));
        }


        while (queue.Count != 0)
        {
            var (parent, child) = queue.Dequeue();

            if (!records.TryGetValue(child.Offset, out var record))
            {
                throw new InvalidOperationException($"Expected to find a record at offset: {child.Offset}.");
            }

            var node = record switch
            {
                FileEntry file => new DirectoryNode() { Parent = parent, Record = new(file) },
                Pdir dir => new DirectoryNode() { Parent = parent, Record = new(dir) },
                _ => throw new InvalidOperationException($"Unexpected type."),
            };

            if (record is Pdir pdir2)
            {
                foreach (var entry in pdir2.Entries)
                {
                    queue.Enqueue((node, entry));
                }
            }

            parent.Children.Add(node);
        }
    }

    private interface IRecord
    {
        int Length { get; }
        string Tag { get; }
        long Position { get; }
    }

    private sealed class Ggpk : IRecord
    {
        public required long Position { get; init; }
        public required int Length { get; init; }
        public required string Tag { get; init; }
        public required int Version { get; init; }
        public required GgpkEntry[] Entries { get; init; }

    }

    private sealed class GgpkEntry
    {
        public required long Offset { get; init; }
    }

    private sealed class Free : IRecord
    {
        public required long Position { get; init; }
        public required int Length { get; init; }

        public required string Tag { get; init; }

        // public required byte[] Data { get; init; }
    }

    private sealed class Pdir : IRecord
    {
        public required long Position { get; init; }
        public required int Length { get; init; }
        public required string Tag { get; init; }
        public required int NameLength { get; init; }
        public required int TotalEntries { get; init; }
        public required byte[] Sha256Hash { get; init; }
        public required string Name { get; init; }
        public required DirectoryEntry[] Entries { get; init; }
    }

    private sealed class DirectoryEntry
    {
        public required int EntryNameHash { get; init; }
        public required long Offset { get; init; }
    }

    private sealed class FileEntry : IRecord
    {
        public required long Position { get; init; }
        public required int Length { get; init; }
        public required string Tag { get; init; }
        public required int NameLength { get; init; }
        public required byte[] Sha256Hash { get; init; }

        public required string Name { get; init; }
        // public required byte[] Data { get; init; }
    }

    private sealed class DirectoryNode
    {
        public required DirectoryNode? Parent { get; init; }
        public List<DirectoryNode> Children { get; } = [];
        public required Value Record { get; init; }

        public sealed class Value
        {
            public Pdir? Directory { get; }
            public FileEntry? File { get; }

            public Value(Pdir directory)
            {
                Directory = directory;
            }

            public Value(FileEntry file)
            {
                File = file;
            }

            public string Name()
            {
                if (Directory is not null)
                {
                    return Directory.Name;
                }
                else if (File is not null)
                {
                    return File.Name;
                }
                throw new InvalidOperationException($"Both {nameof(Directory)} and ${nameof(File)} are null.");
            }
        }
    }
}
