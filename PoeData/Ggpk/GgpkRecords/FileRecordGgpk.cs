using System.Numerics;

namespace PoeData.Ggpk.GgpkRecords;

/// <summary>
/// Class containing file record data.
/// </summary>
internal sealed class FileRecordGgpk : IGgpkTagRecord, IReadGgpkTagRecord<FileRecordGgpk>, INamedGgpkTagRecord
{
    /// <inheritdoc/>
    public required int Length { get; init; }

    /// <inheritdoc/>
    public required long Offset { get; init; }

    /// <inheritdoc/>
    public required string Name { get; init; }

    /// <summary>Gets file hash.</summary>
    public required BigInteger Hash { get; init; }

    /// <inheritdoc/>
    public static FileRecordGgpk Read(BinaryReader ggpkReader, int length, long offset)
    {
        const int CharWidth = 2;

        var nameLength = ggpkReader.ReadInt32();
        var hashBytes = ggpkReader.ReadBytes(32);
        var hash = new BigInteger(hashBytes, isUnsigned: true, isBigEndian: true);

        var nameBytes = ggpkReader.ReadBytes(CharWidth * (nameLength - 1));
        var name = System.Text.Encoding.Unicode.GetString(nameBytes);

        ggpkReader.BaseStream.Seek(CharWidth, SeekOrigin.Current);
        var dataStart = ggpkReader.BaseStream.Position;

        // Length 4B - Tag 4B - STRLen 4B - Hash 32B + STR ?B
        var dataLength = length - 44 - (nameLength * CharWidth);

        ggpkReader.BaseStream.Seek(dataLength, SeekOrigin.Current);

        return new FileRecordGgpk()
        {
            Length = length,
            Offset = offset,
            Hash = hash,
            Name = name,
        };
    }
}
