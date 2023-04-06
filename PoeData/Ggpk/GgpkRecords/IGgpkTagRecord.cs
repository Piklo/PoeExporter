namespace PoeData.Ggpk.GgpkRecords;

/// <summary>
/// Interface containing properties required by read ggpk tag records.
/// </summary>
internal interface IGgpkTagRecord
{
    /// <summary>Gets length.</summary>
    public int Length { get; init; }

    /// <summary>Gets offset.</summary>
    public long Offset { get; init; }
}

/// <summary>
/// Interface containing Read method for ggpk tag records.
/// </summary>
internal interface IReadGgpkTagRecord
{
    /// <summary>
    /// Reads the file stream based on tag data.
    /// </summary>
    /// <param name="ggpkFile">ggpk file stream.</param>
    /// <param name="length">length.</param>
    /// <param name="offset">offset.</param>
    /// <returns>ggpk tag record with read values.</returns>
    public static abstract IGgpkTagRecord Read(FileStream ggpkFile, int length, long offset);
}