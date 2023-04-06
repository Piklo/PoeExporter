namespace PoeData.Ggpk.GgpkRecords;

/// <summary>
/// Class containing free record data.
/// </summary>
internal sealed class FreeRecord : IGgpkTagRecord
{
    /// <inheritdoc/>
    public required int Length { get; init; }

    /// <inheritdoc/>
    public required long Offset { get; init; }

    /// <summary>Gets offset of next <see cref="FreeRecord"/>.</summary>
    public required long NextFree { get; init; }

    /// <summary>
    /// Reads free record data.
    /// </summary>
    /// <param name="ggpkFile">stream to read from.</param>
    /// <param name="length">records length.</param>
    /// <param name="offset">records offset.</param>
    /// <returns>parsed free record data.</returns>
    public static FreeRecord Read(FileStream ggpkFile, int length, long offset)
    {
        var nextFreeBytes = new byte[sizeof(long)];
        ggpkFile.ReadExactly(nextFreeBytes);
        var nextFree = BitConverter.ToInt64(nextFreeBytes);

        var record = new FreeRecord()
        {
            Length = length,
            Offset = offset,
            NextFree = nextFree,
        };

        ggpkFile.Seek(length - 16, SeekOrigin.Current);

        return record;
    }
}
