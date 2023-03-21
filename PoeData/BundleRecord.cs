namespace PoeData;

/// <summary>
/// A class containing bundle record data.
/// </summary>
public sealed class BundleRecord
{
    /// <summary>Gets name.</summary>
    public required string Name { get; init; }

    /// <summary>Gets size.</summary>
    public required int Size { get; init; }

    private BundleRecord()
    {
    }

    /// <summary>
    /// Creates an instance of <see cref="BundleRecord"/> and moves an offset.
    /// </summary>
    /// <param name="data">data used to create the <see cref="BundleRecord"/>.</param>
    /// <param name="offset">offset with starting index.</param>
    /// <returns>instance of <see cref="BundleRecord"/> and bytes read to create it.</returns>
    public static (BundleRecord bundle, int bytesRead) Create(byte[] data, int offset)
    {
        var startingOffset = offset;
        var nameLength = BitConverter.ToInt32(data, offset);
        offset += sizeof(int);

        // var name = BitConverter.ToString(data, offset, nameLength);
        var name = System.Text.Encoding.UTF8.GetString(data, offset, nameLength);
        offset += nameLength;

        var size = BitConverter.ToInt32(data, offset);
        offset += sizeof(int);

        var bundle = new BundleRecord() { Name = name, Size = size };

        return (bundle, offset - startingOffset);
    }
}