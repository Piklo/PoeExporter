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
