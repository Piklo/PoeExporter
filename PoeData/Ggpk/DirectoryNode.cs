using OneOf;
using PoeData.Ggpk.GgpkRecords;

namespace PoeData.Ggpk;

/// <summary>
/// Class containing directory node data.
/// </summary>
internal sealed class DirectoryNode
{
    /// <summary>Gets parent.</summary>
    public required DirectoryNode? Parent { get; init; }

    /// <summary>Gets parent.</summary>
    public List<DirectoryNode> Children { get; } = new();

    /// <summary>Gets record.</summary>
    public required OneOf<DirectoryRecordGgpk, FileRecordGgpk> Record { get; init; }

    /// <summary>Gets offset.</summary>
    public long Offset
    {
        get
        {
            return Record.Match(
                directoryRecordGgpk => directoryRecordGgpk.Offset,
                fileRecord => fileRecord.Offset);
        }
    }

    /// <summary>Gets offset.</summary>
    public string Name
    {
        get
        {
            return Record.Match(
                directoryRecordGgpk => directoryRecordGgpk.Name,
                fileRecord => fileRecord.Name);
        }
    }
}