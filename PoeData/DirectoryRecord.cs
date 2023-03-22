namespace PoeData;

/// <summary>
/// Class contaning directory record data.
/// </summary>
public class DirectoryRecord
{
    /// <summary>Gets hash.</summary>
    public required ulong Hash { get; init; }

    /// <summary>Gets offset.</summary>
    public required uint Offset { get; init; }

    /// <summary>Gets size.</summary>
    public required uint Size { get; init; }

    /// <summary>Gets unknown.</summary>
    public required uint Unknown { get; init; }

    /// <summary>
    /// Creates an instance of <see cref="DirectoryRecord"/> and moves the offset.
    /// </summary>
    /// <param name="data">An array of bytes used to create <see cref="DirectoryRecord"/>.</param>
    /// <param name="offset">Starting index.</param>
    /// <returns>instance of <see cref="DirectoryRecord"/> and moved offset.</returns>
    public static (DirectoryRecord directoryRecord, int offset) Create(byte[] data, int offset)
    {
        (var hash, offset) = BitConverterExtended.ToUInt64(data, offset);
        (var offsetValue, offset) = BitConverterExtended.ToUInt32(data, offset);
        (var size, offset) = BitConverterExtended.ToUInt32(data, offset);
        (var unknown, offset) = BitConverterExtended.ToUInt32(data, offset);

        var directoryRecord = new DirectoryRecord() { Hash = hash, Offset = offsetValue, Size = size, Unknown = unknown };

        return (directoryRecord, offset);
    }
}