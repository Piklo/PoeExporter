namespace PoeData.Ggpk.GgpkRecords;

/// <summary>
/// Class containing free record data.
/// </summary>
internal sealed class FreeRecord : IGgpkTagRecord, IReadGgpkTagRecord<FreeRecord>
{
    /// <inheritdoc/>
    public required int Length { get; init; }

    /// <inheritdoc/>
    public required long Offset { get; init; }

    /// <summary>Gets offset of next <see cref="FreeRecord"/>.</summary>
    public required long NextFree { get; init; }

    /// <inheritdoc cref="IReadGgpkTagRecord{T}.Read(BinaryReader, int, long)"/>
    public static FreeRecord Read(BinaryReader ggpkReader, int length, long offset)
    {
        var nextFree = ggpkReader.ReadInt64();

        var record = new FreeRecord()
        {
            Length = length,
            Offset = offset,
            NextFree = nextFree,
        };

        ggpkReader.BaseStream.Seek(length - 16, SeekOrigin.Current);

        return record;
    }
}
