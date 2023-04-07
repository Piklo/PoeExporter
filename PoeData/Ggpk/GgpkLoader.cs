using PoeData.Ggpk.GgpkRecords;
using Serilog;

namespace PoeData.Ggpk;

/// <summary>
/// Class containing .ggpk data.
/// </summary>
internal sealed class GgpkLoader
{
    private const string FileName = "Content.ggpk";
    private readonly IConfig config;
    private readonly ILogger logger;
    private readonly string ggpkPath;
    private readonly GgpkRecord ggpkRecord;
    private readonly Dictionary<long, IGgpkTagRecord> records = new();
    private readonly Dictionary<long, FileRecordGgpk> fileRecords = new();
    private readonly Dictionary<long, FreeRecord> freeRecords = new();
    private readonly Dictionary<long, DirectoryRecordGgpk> directoryRecords = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="GgpkLoader"/> class.
    /// </summary>
    /// <param name="config">config.</param>
    /// <param name="logger">logger.</param>
    public GgpkLoader(IConfig config, ILogger logger)
    {
        this.config = config;
        this.logger = logger;
        ggpkPath = Path.Combine(this.config.PoePath, FileName);

        var ggpkFileStream = File.OpenRead(ggpkPath);
        using var ggpkReader = new BinaryReader(ggpkFileStream);

        ggpkRecord = ReadGgpkRecord(ggpkReader);
        records.Add(ggpkRecord.Offset, ggpkRecord);

        while (ggpkFileStream.Position < ggpkFileStream.Length)
        {
            var record = ReadRecord(ggpkReader);
            records.Add(record.Offset, record);
        }

        this.logger.Verbose("created {count} records", records.Count);
    }

    private static int GetLength(BinaryReader ggpkReader)
    {
        var length = ggpkReader.ReadInt32();

        return length;
    }

    private static byte[] GetTag(BinaryReader ggpkReader)
    {
        const int tagLength = 4; // 4 characters long string in byte[] form.

        var tagBytes = ggpkReader.ReadBytes(tagLength);

        return tagBytes;
    }

    private static GgpkRecord ReadGgpkRecord(BinaryReader ggpkReader)
    {
        var recordOffset = ggpkReader.BaseStream.Position;
        var length = GetLength(ggpkReader);
        _ = GetTag(ggpkReader); // GGPK tag, we need to keep it to move the position

        var record = GgpkRecord.Read(ggpkReader, length, recordOffset);

        return record;
    }

    private IGgpkTagRecord ReadRecord(BinaryReader ggpkReader)
    {
        var recordOffset = ggpkReader.BaseStream.Position;
        var length = GetLength(ggpkReader);
        var tag = GetTag(ggpkReader);

        var tagStr = System.Text.Encoding.Default.GetString(tag);
        if (tagStr == "FILE")
        {
            var fileRecord = FileRecordGgpk.Read(ggpkReader, length, recordOffset);
            fileRecords.Add(fileRecord.Offset, fileRecord);
            return fileRecord;
        }
        else if (tagStr == "FREE")
        {
            var freeRecord = FreeRecord.Read(ggpkReader, length, recordOffset);
            freeRecords.Add(freeRecord.Offset, freeRecord);
            return freeRecord;
        }
        else if (tagStr == "PDIR")
        {
            var directoryRecord = DirectoryRecordGgpk.Read(ggpkReader, length, recordOffset);
            directoryRecords.Add(directoryRecord.Offset, directoryRecord);
            return directoryRecord;
        }
        else if (tagStr == "GGPK")
        {
            // return GgpkRecord.Read(ggpkFile, length, ggpkFile.Position);
            throw new FoundAnotherGgpkTagRecordException();
        }
        else
        {
            throw new NotImplementedException($"unknown tag = {tagStr} at offset = {recordOffset} of length {length}");
        }
    }
}
