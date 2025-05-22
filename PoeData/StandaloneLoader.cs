using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace PoeData;

public sealed class StandaloneLoader : IDataLoader
{
    private const string FileName = "Content.ggpk";
    private const int LengthLength = sizeof(int);
    private const int TagLength = 4;

    private readonly string _clientPath;
    private readonly Ggpk _ggpk;
    private readonly Dictionary<long, IRecord> _records = [];
    public StandaloneLoader(string clientPath)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(clientPath);
        _clientPath = clientPath;

        var fullPath = Path.Combine(clientPath, FileName);
        using var file = File.OpenRead(fullPath);
        using var reader = new BinaryReader(file);
        var record = ReadRecord(reader);
        if (record is not Ggpk ggpk)
        {
            throw new UnreachableException($"Expected {nameof(Ggpk)} at position {record.Position}.");
        }
        _ggpk = ggpk;
        _records.Add(_ggpk.Position, ggpk);

        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            record = ReadRecord(reader);
        }
    }

    public byte[] ReadIndex()
    {
        throw new NotImplementedException();
    }

    public byte[] GetFileBytes(string path)
    {
        throw new NotImplementedException();
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
            _ => throw new UnreachableException($"Unknown tag = {tag}."),
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

        const int CharWidth = 2;
        var nameLength = CharWidth * nameBytesLength;
        var nameBytes = reader.ReadBytes(nameLength - CharWidth);
        reader.BaseStream.Seek(CharWidth, SeekOrigin.Current);
        var name = Encoding.Unicode.GetString(nameBytes);

        var file = new FileEntry() { Position = position, Length = length, Tag = tag, NameLength = nameBytesLength, Sha256Hash = hash, Name = name };

        const int NameLengthLength = sizeof(int);
        var remainder = LengthLength + TagLength + NameLengthLength + SHA256.HashSizeInBytes + nameLength;
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

        const int CharWidth = 2;
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
}
