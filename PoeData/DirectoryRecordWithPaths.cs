namespace PoeData;

/// <summary>
/// A class containing paths for <see cref="DirectoryRecord"/>.
/// </summary>
public class DirectoryRecordWithPaths
{
    /// <summary>Gets directory record..</summary>
    public required DirectoryRecord Record { get; init; }

    /// <summary>Gets packed paths.</summary>
#pragma warning disable CA1819 // Properties should not return arrays
    public required byte[][] Paths { get; init; }
#pragma warning restore CA1819 // Properties should not return arrays
}