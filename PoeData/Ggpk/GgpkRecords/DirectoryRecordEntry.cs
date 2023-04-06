namespace PoeData.Ggpk.GgpkRecords;

/// <summary>
/// Class contianing directory record entry data.
/// </summary>
internal sealed class DirectoryRecordEntry
{
    /// <summary>Gets hash.</summary>
    public required uint Hash { get; init; }

    /// <summary>Gets offset.</summary>
    public required long Offset { get; init; }
}