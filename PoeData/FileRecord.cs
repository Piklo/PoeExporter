namespace PoeData;

/// <summary>
/// Class containing file record data.
/// </summary>
public sealed class FileRecord
{
    /// <summary>Gets hash.</summary>
    public required ulong Hash { get; init; }

    /// <summary>Gets related bundle record.</summary>
    public required BundleRecord BundleRecord { get; init; }

    /// <summary>Gets file offset.</summary>
    public required uint FileOffset { get; init; }

    /// <summary>Gets file size.</summary>
    public required uint FileSize { get; init; }

    /// <summary>
    /// Creates an instance of <see cref="FileRecord"/> and moves the offset.
    /// </summary>
    /// <param name="data">data used to create <see cref="FileRecord"/>.</param>
    /// <param name="offset">offset with starting index.</param>
    /// <param name="bundleRecords">contains all BundleRecords.</param>
    /// <returns>instance of <see cref="FileRecord"/> and bytes read used to create it.</returns>
    /// <exception cref="ArgumentNullException">Thrown when bundleRecords is null.</exception>
    public static (FileRecord fileRecord, int bytesRead) Create(byte[] data, int offset, BundleRecord[] bundleRecords)
    {
        if (bundleRecords is null)
        {
            throw new ArgumentNullException(nameof(bundleRecords));
        }

        var startingOffset = offset;

        (var hash, offset) = BitConverterExtended.ToUInt64(data, offset);

        (var bundleRecordsIndex, offset) = BitConverterExtended.ToUInt32(data, offset);
        var bundleRecord = bundleRecords[bundleRecordsIndex];

        (var fileOffset, offset) = BitConverterExtended.ToUInt32(data, offset);

        (var fileSize, offset) = BitConverterExtended.ToUInt32(data, offset);

        var fileRecord = new FileRecord() { Hash = hash, BundleRecord = bundleRecord, FileOffset = fileOffset, FileSize = fileSize };

        return (fileRecord, offset - startingOffset);
    }
}