using System.Collections.ObjectModel;

namespace PoeData;

/// <summary>
/// Class containing Path of Exile decompressed data.
/// </summary>
public sealed class DecompressedData
{
    /// <summary>Gets UncompressedSize.</summary>
    public required int UncompressedSize { get; init; }

    /// <summary>Gets DataSize.</summary>
    public required int DataSize { get; init; }

    /// <summary>Gets HeadSize.</summary>
    public required int HeadSize { get; init; }

    /// <summary>Gets EncoderType.</summary>
    public required EncodeTypes EncoderType { get; init; }

    /// <summary>Gets Unknown.</summary>
    public required int Unknown { get; init; }

    /// <summary>Gets SizeDecompressed.</summary>
    public required long SizeDecompressed { get; init; }

    /// <summary>Gets SizeCompressed.</summary>
    public required long SizeCompressed { get; init; }

    /// <summary>Gets EntryCount.</summary>
    public required int EntryCount { get; init; }

    /// <summary>Gets ChunkSize.</summary>
    public required int ChunkSize { get; init; }

    /// <summary>Gets Unknown3.</summary>
    public required int Unknown3 { get; init; }

    /// <summary>Gets Unknown4.</summary>
    public required int Unknown4 { get; init; }

    /// <summary>Gets Unknown5.</summary>
    public required int Unknown5 { get; init; }

    /// <summary>Gets Unknown6.</summary>
    public required int Unknown6 { get; init; }

    /// <summary>Gets decompressed data.</summary>
    public required ReadOnlyCollection<byte> Data { get; init; }
}