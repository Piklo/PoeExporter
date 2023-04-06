using System.Numerics;

namespace PoeData.Ggpk.GgpkRecords;

/// <summary>
/// Class containing directory record data.
/// </summary>
internal sealed class DirectoryRecordGgpk : IGgpkTagRecord, IReadGgpkTagRecord<DirectoryRecordGgpk>
{
    /// <inheritdoc/>
    public required int Length { get; init; }

    /// <inheritdoc/>
    public required long Offset { get; init; }

    /// <summary>Gets directory name.</summary>
    public required string Name { get; init; }

    /// <summary>Gets directory hash.</summary>
    public required BigInteger Hash { get; init; }

    /// <summary>Gets directory entries.</summary>
    public required IReadOnlyCollection<DirectoryRecordEntry> Entries { get; init; }

    /// <inheritdoc/>
    public static DirectoryRecordGgpk Read(BinaryReader ggpkReader, int length, long offset)
    {
        const int CharWidth = 2;

        var nameLength = ggpkReader.ReadInt32();
        var entriesLength = ggpkReader.ReadInt32();
        var hashBytes = ggpkReader.ReadBytes(32);
        var hash = new BigInteger(hashBytes);

        var nameBytes = ggpkReader.ReadBytes(CharWidth * (nameLength - 1));
        var name = System.Text.Encoding.Unicode.GetString(nameBytes);

        ggpkReader.BaseStream.Seek(CharWidth, SeekOrigin.Current);

        var entries = new DirectoryRecordEntry[entriesLength];
        for (var i = 0; i < entriesLength; i++)
        {
            var entryHash = ggpkReader.ReadUInt32();
            var entryOffset = ggpkReader.ReadInt64();
            var entry = new DirectoryRecordEntry() { Hash = entryHash, Offset = entryOffset };
            entries[i] = entry;
        }

        return new DirectoryRecordGgpk()
        {
            Length = length,
            Offset = offset,
            Hash = hash,
            Name = name,
            Entries = entries,
        };
    }
}
