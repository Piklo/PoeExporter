namespace PoeData;

/// <summary>
/// A class containing paths for <see cref="DirectoryRecord"/>.
/// </summary>
public class DirectoryRecordWithPaths
{
    /// <summary>Gets packed paths.</summary>
    private readonly byte[][] paths;

    /// <summary>Gets directory record.</summary>
    public DirectoryRecord Record { get; }

    /// <summary>Gets paths count.</summary>
    public int PathsCount { get => paths.Length; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DirectoryRecordWithPaths"/> class.
    /// </summary>
    /// <param name="record">the inner directory record.</param>
    /// <param name="paths">array containing paths.</param>
    public DirectoryRecordWithPaths(DirectoryRecord record, byte[][] paths)
    {
        Record = record;
        this.paths = paths;
    }

    /// <summary>
    /// a method to get the path.
    /// </summary>
    /// <param name="index">index of the path.</param>
    /// <returns>readonly path at the given index.</returns>
    public ReadOnlySpan<byte> GetPath(int index)
    {
        var path = paths[index].AsSpan();

        return path;
    }
}