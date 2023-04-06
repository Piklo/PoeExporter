namespace PoeData.Ggpk.GgpkRecords;

/// <summary>
/// Class containing ggpk tag record data.
/// </summary>
internal sealed class GgpkRecord : IGgpkTagRecord, IReadGgpkTagRecord<GgpkRecord>
{
    /// <inheritdoc/>
    public required int Length { get; init; }

    /// <inheritdoc/>
    public required long Offset { get; init; }

    /// <summary>Gets offsets.</summary>
    public required IReadOnlyCollection<long> Offsets { get; init; }

    /// <summary>Gets ggpk version.</summary>
    public required int Version { get; init; }

    /// <inheritdoc/>
    public static GgpkRecord Read(BinaryReader ggpkReader, int length, long offset)
    {
        var version = ggpkReader.ReadInt32();

        const int OffsetsCount = 2;

        var offsets = new long[OffsetsCount];
        for (var i = 0; i < OffsetsCount; i++)
        {
            var newOffset = ggpkReader.ReadInt64();

            offsets[i] = newOffset;
        }

        var read = new GgpkRecord()
        {
            Length = length,
            Offset = offset,
            Offsets = offsets,
            Version = version,
        };

        return read;
    }
}
