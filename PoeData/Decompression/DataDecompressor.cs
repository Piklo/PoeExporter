﻿using ooz;
using Serilog;
using System.Diagnostics;

namespace PoeData.Decompression;

/// <summary>
/// Class used to decompress Bundles2\_.index.bin data.
/// </summary>
internal sealed class DataDecompressor
{
    private readonly ILogger logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataDecompressor"/> class.
    /// </summary>
    /// <param name="logger">logger used by this class.</param>
    internal DataDecompressor(ILogger logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Decompresses data using ooz.
    /// </summary>
    /// <param name="compressedData">data to decompress.</param>
    /// <returns>decompressed data.</returns>
    internal DecompressedData Decompress(byte[] compressedData)
    {
        if (compressedData is null)
        {
            throw new ArgumentNullException(nameof(compressedData));
        }

        logger.Verbose("decompressing {length} bytes", compressedData.Length);
        var startTimestamp = Stopwatch.GetTimestamp();
        var offset = 0;

        // base data
        (var uncompressedSize, offset) = BitConverterExtended.ToUInt32(compressedData, offset);

        (var dataSize, offset) = BitConverterExtended.ToUInt32(compressedData, offset);

        (var headSize, offset) = BitConverterExtended.ToUInt32(compressedData, offset);

        // other data
        (var encoderTypeValue, offset) = BitConverterExtended.ToUInt32(compressedData, offset);
        var encoderType = (EncodeTypes)encoderTypeValue;

        (var unknown, offset) = BitConverterExtended.ToUInt32(compressedData, offset);

        (var sizeDecompressed, offset) = BitConverterExtended.ToUInt64(compressedData, offset);

        (var sizeCompressed, offset) = BitConverterExtended.ToUInt64(compressedData, offset);

        (var entryCount, offset) = BitConverterExtended.ToUInt32(compressedData, offset);

        (var chunkSize, offset) = BitConverterExtended.ToUInt32(compressedData, offset);

        (var unknown3, offset) = BitConverterExtended.ToUInt32(compressedData, offset);

        (var unknown4, offset) = BitConverterExtended.ToUInt32(compressedData, offset);

        (var unknown5, offset) = BitConverterExtended.ToUInt32(compressedData, offset);

        (var unknown6, offset) = BitConverterExtended.ToUInt32(compressedData, offset);

        var chunks = new uint[entryCount];
        for (var i = 0; i < entryCount; i++)
        {
            (var chunk, offset) = BitConverterExtended.ToUInt32(compressedData, offset);

            chunks[i] = chunk;
        }

        var data = new byte[entryCount][];
        for (var i = 0; i < entryCount; i++)
        {
            var offset2 = offset + (int)chunks[i];

            data[i] = compressedData[offset..offset2];

            offset = offset2;
        }

        var decompressed = new List<byte>();
        var last = (int)entryCount - 1;
        for (var i = 0; i < entryCount; i++)
        {
            var size = i != last ? (int)chunkSize : (int)(sizeDecompressed % chunkSize);

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
}
