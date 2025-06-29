namespace PoeData;

internal static class Decompressor
{
    public static DecompressedData Decompress(byte[] compressedData)
    {
        using var stream = new MemoryStream(compressedData);
        using var reader = new BinaryReader(stream);

        var uncompressedSize = reader.ReadUInt32();
        var dataSize = reader.ReadUInt32();
        var headSize = reader.ReadUInt32();

        var encoderType = (EncodeTypes)reader.ReadInt32();
        var unknown = reader.ReadUInt32();
        var sizeDecompressed = reader.ReadUInt64();
        var sizeCompressed = reader.ReadUInt64();
        var entryCount = reader.ReadUInt32();
        var chunkSize = reader.ReadUInt32();
        var unknown3 = reader.ReadUInt32();
        var unknown4 = reader.ReadUInt32();
        var unknown5 = reader.ReadUInt32();
        var unknown6 = reader.ReadUInt32();

        var chunks = new int[entryCount];
        for (var i = 0; i < chunks.Length; i++)
        {
            var chunk = reader.ReadInt32();
            chunks[i] = chunk;
        }

        var data = new byte[chunks.Length][];
        for (var i = 0; i < chunks.Length; i++)
        {
            var chunk = chunks[i];
            var bytes = reader.ReadBytes(chunk);
            data[i] = bytes;
        }

        var decompressedTotal = new List<byte>((int)uncompressedSize);
        for (var i = 0; i < entryCount; i++)
        {
            var size = (int)(i != entryCount - 1 ? chunkSize : sizeDecompressed % chunkSize);

            var chunkDecompressed = new byte[size];
            Ooz.Ooz.Decompress(data[i], data[i].Length, chunkDecompressed, size);
            decompressedTotal.AddRange(chunkDecompressed);
        }

        byte[] decompressed = [.. decompressedTotal];

        var result = new DecompressedData()
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
            Data = decompressed,
        };

        return result;
    }
}

internal enum EncodeTypes
{
    LZH = 0,
    LZHLW = 1,
    LZNIB = 2,
    NONE = 3,
    LZB16 = 4,
    LZBLW = 5,
    LZA = 6,
    LZNA = 7,
    KRAKEN = 8,
    MERMAID = 9,
    BITKNIT = 10,
    SELKIE = 11,
    HYDRA = 12,
    LEVIATHAN = 13,
}

internal sealed class DecompressedData
{
    public required uint UncompressedSize { get; init; }

    public required uint DataSize { get; init; }

    public required uint HeadSize { get; init; }

    public required EncodeTypes EncoderType { get; init; }

    public required uint Unknown { get; init; }

    public required ulong SizeDecompressed { get; init; }

    public required ulong SizeCompressed { get; init; }

    public required uint EntryCount { get; init; }

    public required uint ChunkSize { get; init; }

    public required uint Unknown3 { get; init; }

    public required uint Unknown4 { get; init; }

    public required uint Unknown5 { get; init; }

    public required uint Unknown6 { get; init; }

    public required byte[] Data { get; init; }
}
