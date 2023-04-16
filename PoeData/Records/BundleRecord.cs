namespace PoeData.Records;

/// <summary>
/// A class containing bundle record data.
/// </summary>
public sealed class BundleRecord
{
    /// <summary>Gets name.</summary>
    public required string Name { get; init; }

    /// <summary>Gets size.</summary>
    public required uint Size { get; init; }

    /// <summary>Gets file name.</summary>
    public string FileName { get => Name + ".bundle.bin"; }

    /// <summary>Gets ggpk path.</summary>
    public string GgpkPath { get => "Bundles2/" + FileName; }

    /// <summary>
    /// Creates an instance of <see cref="BundleRecord"/> and moves an offset.
    /// </summary>
    /// <param name="data">An array of bytes used to create <see cref="BundleRecord"/>.</param>
    /// <param name="offset">Starting index.</param>
    /// <returns>instance of <see cref="BundleRecord"/> and moved offset.</returns>
    public static (BundleRecord bundleRecord, int offset) Create(byte[] data, int offset)
    {
        (var nameLength, offset) = BitConverterExtended.ToUInt32(data, offset);

        var name = System.Text.Encoding.UTF8.GetString(data, offset, (int)nameLength);
        offset += (int)nameLength;

        (var size, offset) = BitConverterExtended.ToUInt32(data, offset);

        var bundleRecord = new BundleRecord() { Name = name, Size = size };

        return (bundleRecord, offset);
    }
}