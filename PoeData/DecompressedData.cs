namespace PoeData;

/// <summary>
/// Class containing Path of Exile decompressed data.
/// </summary>
public sealed class DecompressedData
{
    /// <summary>Gets UncompressedSize.</summary>
    public required uint UncompressedSize { get; init; }

    /// <summary>Gets DataSize.</summary>
    public required uint DataSize { get; init; }

    /// <summary>Gets HeadSize.</summary>
    public required uint HeadSize { get; init; }

    /// <summary>Gets EncoderType.</summary>
    public required EncodeTypes EncoderType { get; init; }

    /// <summary>Gets Unknown.</summary>
    public required uint Unknown { get; init; }

    /// <summary>Gets SizeDecompressed.</summary>
    public required ulong SizeDecompressed { get; init; }

    /// <summary>Gets SizeCompressed.</summary>
    public required ulong SizeCompressed { get; init; }

    /// <summary>Gets EntryCount.</summary>
    public required uint EntryCount { get; init; }

    /// <summary>Gets ChunkSize.</summary>
    public required uint ChunkSize { get; init; }

    /// <summary>Gets Unknown3.</summary>
    public required uint Unknown3 { get; init; }

    /// <summary>Gets Unknown4.</summary>
    public required uint Unknown4 { get; init; }

    /// <summary>Gets Unknown5.</summary>
    public required uint Unknown5 { get; init; }

    /// <summary>Gets Unknown6.</summary>
    public required uint Unknown6 { get; init; }

    /// <summary>Gets decompressed data.</summary>
#pragma warning disable CA1819 // Properties should not return arrays
    public required byte[] Data { get; init; }
#pragma warning restore CA1819 // Properties should not return arrays
}