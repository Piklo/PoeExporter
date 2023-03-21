using ooz;
using Serilog;
using System.Diagnostics;

namespace PoeData;

/// <summary>
/// Class used to decompress Bundles2\_.index.bin data.
/// </summary>
internal sealed class DataDecompressor
{
    private const string IndexPath = "Bundles2\\_.index.bin";
    private readonly ILogger logger;

    /// <summary>Gets PoePath.</summary>
    public required string PoePath { get; init; }

    /// <summary>Gets UncompressedSize.</summary>
    public int UncompressedSize { get; private set; }

    /// <summary>Gets DataSize.</summary>
    public int DataSize { get; private set; }

    /// <summary>Gets HeadSize.</summary>
    public int HeadSize { get; private set; }

    /// <summary>Gets EncoderType.</summary>
    public EncodeTypes EncoderType { get; private set; }

    /// <summary>Gets Unknown.</summary>
    public int Unknown { get; private set; }

    /// <summary>Gets SizeDecompressed.</summary>
    public long SizeDecompressed { get; private set; }

    /// <summary>Gets SizeCompressed.</summary>
    public long SizeCompressed { get; private set; }

    /// <summary>Gets EntryCount.</summary>
    public int EntryCount { get; private set; }

    /// <summary>Gets ChunkSize.</summary>
    public int ChunkSize { get; private set; }

    /// <summary>Gets Unknown3.</summary>
    public int Unknown3 { get; private set; }

    /// <summary>Gets Unknown4.</summary>
    public int Unknown4 { get; private set; }

    /// <summary>Gets Unknown5.</summary>
    public int Unknown5 { get; private set; }

    /// <summary>Gets Unknown6.</summary>
    public int Unknown6 { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataDecompressor"/> class.
    /// </summary>
    /// <param name="logger">logger used by this class.</param>
    public DataDecompressor(ILogger logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Decompresses Bundles2\_.index.bin data.
    /// </summary>
    /// <returns>bytes array with decompressed data.</returns>
    public byte[] Decompress()
    {
        logger.Verbose("decompressing data");
        var startTimestamp = Stopwatch.GetTimestamp();

        var compressed = ReadIndex();
        var offset = 0;

        // base data
        UncompressedSize = BitConverter.ToInt32(compressed, offset);
        offset += sizeof(int);

        DataSize = BitConverter.ToInt32(compressed, offset);
        offset += sizeof(int);

        HeadSize = BitConverter.ToInt32(compressed, offset);
        offset += sizeof(int);

        // other data
        EncoderType = (EncodeTypes)BitConverter.ToInt32(compressed, offset);
        offset += sizeof(int);

        Unknown = BitConverter.ToInt32(compressed, offset);
        offset += sizeof(int);

        SizeDecompressed = BitConverter.ToInt64(compressed, offset);
        offset += sizeof(long);

        SizeCompressed = BitConverter.ToInt64(compressed, offset);
        offset += sizeof(long);

        EntryCount = BitConverter.ToInt32(compressed, offset);
        offset += sizeof(int);

        ChunkSize = BitConverter.ToInt32(compressed, offset);
        offset += sizeof(int);

        Unknown3 = BitConverter.ToInt32(compressed, offset);
        offset += sizeof(int);

        Unknown4 = BitConverter.ToInt32(compressed, offset);
        offset += sizeof(int);

        Unknown5 = BitConverter.ToInt32(compressed, offset);
        offset += sizeof(int);

        Unknown6 = BitConverter.ToInt32(compressed, offset);
        offset += sizeof(int);

        var chunks = new int[EntryCount];
        for (var i = 0; i < EntryCount; i++)
        {
            var chunk = BitConverter.ToInt32(compressed, offset);
            offset += sizeof(int);

            chunks[i] = chunk;
        }

        var data = new byte[EntryCount][];
        for (var i = 0; i < EntryCount; i++)
        {
            var offset2 = offset + chunks[i];

            data[i] = compressed[offset..offset2];

            offset = offset2;
        }

        var decompressed = new List<byte>();

        var last = EntryCount - 1;
        for (var i = 0; i < EntryCount; i++)
        {
            var size = i != last ? ChunkSize : (int)(SizeDecompressed % ChunkSize);

            var chunkDecompressed = new byte[size];
            Ooz.Decompress(data[i], data[i].Length, chunkDecompressed, size);
            decompressed.AddRange(chunkDecompressed);
        }

        logger.Verbose("decompressed data in {elapsed}", Stopwatch.GetElapsedTime(startTimestamp));
        return decompressed.ToArray();
    }

    private byte[] ReadIndex()
    {
        var combinedPath = Path.Combine(PoePath, IndexPath);

        logger.Verbose("reading bytes from {path}", combinedPath);
        var bytes = File.ReadAllBytes(combinedPath);
        logger.Verbose("read {count} bytes", bytes.Length);

        return bytes;
    }
}
