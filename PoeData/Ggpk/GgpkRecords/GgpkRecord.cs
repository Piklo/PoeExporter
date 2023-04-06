namespace PoeData.Ggpk.GgpkRecords;

/// <summary>
/// Class containing ggpk tag record data.
/// </summary>
internal sealed class GgpkRecord : IGgpkTagRecord
{
    /// <inheritdoc/>
    public required int Length { get; init; }

    /// <inheritdoc/>
    public required long Offset { get; init; }

    /// <summary>Gets offsets.</summary>
    public required IReadOnlyCollection<long> Offsets { get; init; }

    /// <summary>Gets ggpk version.</summary>
    public required int Version { get; init; }

    /// <summary>
    /// Reads ggpk record data.
    /// </summary>
    /// <param name="ggpkFile">stream to read from.</param>
    /// <param name="length">records length.</param>
    /// <param name="offset">records offset.</param>
    /// <returns>parsed ggpk record data.</returns>
    public static GgpkRecord Read(FileStream ggpkFile, int length, long offset)
    {
        var versionBytes = new byte[sizeof(int)];
        ggpkFile.ReadExactly(versionBytes);
        var version = BitConverter.ToInt32(versionBytes);

        const int OffsetsCount = 2;

        var offsets = new long[OffsetsCount];
        for (var i = 0; i < OffsetsCount; i++)
        {
            var offsetBytes = new byte[sizeof(long)];
            ggpkFile.ReadExactly(offsetBytes);

            var newOffset = BitConverter.ToInt64(offsetBytes);

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
