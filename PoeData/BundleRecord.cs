namespace PoeData;

/// <summary>
/// A class containing bundle record data.
/// </summary>
public sealed class BundleRecord
{
    /// <summary>Gets name.</summary>
    public required string Name { get; init; }

    /// <summary>Gets size.</summary>
    public required uint Size { get; init; }

    /// <summary>
    /// Creates an instance of <see cref="BundleRecord"/> and moves an offset.
    /// </summary>
    /// <param name="data">data used to create the <see cref="BundleRecord"/>.</param>
    /// <param name="offset">offset with starting index.</param>
    /// <returns>instance of <see cref="BundleRecord"/> and bytes read to create it.</returns>
    public static (BundleRecord bundleRecord, int bytesRead) Create(byte[] data, int offset)
    {
        var startingOffset = offset;
        var nameLength = BitConverter.ToUInt32(data, offset);
        offset += sizeof(uint);

        // var name = BitConverter.ToString(data, offset, nameLength);
        var name = System.Text.Encoding.UTF8.GetString(data, offset, (int)nameLength);
        offset += (int)nameLength;

        var size = BitConverter.ToUInt32(data, offset);
        offset += sizeof(uint);

        var bundle = new BundleRecord() { Name = name, Size = size };

        return (bundle, offset - startingOffset);
    }
}