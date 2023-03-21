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

    /// <summary>
    /// Initializes a new instance of the <see cref="DataDecompressor"/> class.
    /// </summary>
    /// <param name="logger">logger used by this class.</param>
    public DataDecompressor(ILogger logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Loads and decompresses Bundles2\_.index.bin data.
    /// </summary>
    /// <returns>decompressed data.</returns>
    public DecompressedData LoadAndDecompress()
    {
        var compressed = ReadIndex();
        return Decompress(compressed);
    }

    /// <summary>
    /// Decompresses data using ooz.
    /// </summary>
    /// <param name="compressedData">data to decompress.</param>
    /// <returns>decompressed data.</returns>
    public DecompressedData Decompress(byte[] compressedData)
    {
        if (compressedData is null)
        {
            throw new ArgumentNullException(nameof(compressedData));
        }

        logger.Verbose("decompressing data");
        var startTimestamp = Stopwatch.GetTimestamp();
        var offset = 0;

        // base data
        var uncompressedSize = BitConverter.ToInt32(compressedData, offset);
        offset += sizeof(int);

        var dataSize = BitConverter.ToInt32(compressedData, offset);
        offset += sizeof(int);

        var headSize = BitConverter.ToInt32(compressedData, offset);
        offset += sizeof(int);

        // other data
        var encoderType = (EncodeTypes)BitConverter.ToInt32(compressedData, offset);
        offset += sizeof(int);

        var unknown = BitConverter.ToInt32(compressedData, offset);
        offset += sizeof(int);

        var sizeDecompressed = BitConverter.ToInt64(compressedData, offset);
        offset += sizeof(long);

        var sizeCompressed = BitConverter.ToInt64(compressedData, offset);
        offset += sizeof(long);

        var entryCount = BitConverter.ToInt32(compressedData, offset);
        offset += sizeof(int);

        var chunkSize = BitConverter.ToInt32(compressedData, offset);
        offset += sizeof(int);

        var unknown3 = BitConverter.ToInt32(compressedData, offset);
        offset += sizeof(int);

        var unknown4 = BitConverter.ToInt32(compressedData, offset);
        offset += sizeof(int);

        var unknown5 = BitConverter.ToInt32(compressedData, offset);
        offset += sizeof(int);

        var unknown6 = BitConverter.ToInt32(compressedData, offset);
        offset += sizeof(int);

        var chunks = new int[entryCount];
        for (var i = 0; i < entryCount; i++)
        {
            var chunk = BitConverter.ToInt32(compressedData, offset);
            offset += sizeof(int);

            chunks[i] = chunk;
        }

        var data = new byte[entryCount][];
        for (var i = 0; i < entryCount; i++)
        {
            var offset2 = offset + chunks[i];

            data[i] = compressedData[offset..offset2];

            offset = offset2;
        }

        var decompressed = new List<byte>();
        var last = entryCount - 1;
        for (var i = 0; i < entryCount; i++)
        {
            var size = i != last ? chunkSize : (int)(sizeDecompressed % chunkSize);

            var chunkDecompressed = new byte[size];
            Ooz.Decompress(data[i], data[i].Length, chunkDecompressed, size);
            decompressed.AddRange(chunkDecompressed);
        }

        logger.Verbose("decompressed data in {elapsed}", Stopwatch.GetElapsedTime(startTimestamp));

        var decompressedData = new DecompressedData()
        {
            UncompressedSize = uncompressedSize,
            DataSize = dataSize,
            HeadSize = headSize,
            EncoderType = encoderType,
            Unknown = unknown,
            SizeDecompressed = sizeDecompressed,
            SizeCompressed = sizeCompressed,
            EntryCount = entryCount,
            ChunkSize = chunkSize,
            Unknown3 = unknown3,
            Unknown4 = unknown4,
            Unknown5 = unknown5,
            Unknown6 = unknown6,
            Data = decompressed.ToArray(),
        };

        return decompressedData;
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
