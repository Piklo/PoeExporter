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
    private readonly string filePath;
    private readonly Dictionary<long, IGgpkTagRecord> records = new();
    private readonly FileStream ggpkFileStream;

    /// <summary>
    /// Initializes a new instance of the <see cref="GgpkLoader"/> class.
    /// </summary>
    /// <param name="config">config.</param>
    /// <param name="logger">logger.</param>
    public GgpkLoader(IConfig config, ILogger logger)
    {
        this.config = config;
        this.logger = logger;
        filePath = Path.Combine(this.config.PoePath, FileName);

        ggpkFileStream = File.OpenRead(filePath);

        var ggpkRecord = ReadGgpkRecord();
        records.Add(ggpkRecord.Offset, ggpkRecord);

        while (ggpkFileStream.Position < ggpkFileStream.Length)
        {
            var record = ReadRecord();
            records.Add(record.Offset, record);
        }

        this.logger.Verbose("created {count} records", records.Count);

        ggpkFileStream.Dispose();
    }

    private int GetLength()
    {
        var buffer = new byte[sizeof(int)];
        ggpkFileStream.ReadExactly(buffer);
        var length = BitConverter.ToInt32(buffer);

        return length;
    }

    private byte[] GetTag()
    {
        const int tagLength = 4; // 4 characters long string in byte[] form.

        var tag = new byte[tagLength];
        ggpkFileStream.ReadExactly(tag);

        return tag;
    }

    private GgpkRecord ReadGgpkRecord()
    {
        var recordOffset = ggpkFileStream.Position;
        var length = GetLength();
        _ = GetTag(); // GGPK tag, we need to keep it to move the position

        var record = GgpkRecord.Read(ggpkFileStream, length, recordOffset);

        return record;
    }

    private IGgpkTagRecord ReadRecord()
    {
        var recordOffset = ggpkFileStream.Position;
        var length = GetLength();
        var tag = GetTag();

        var tagStr = System.Text.Encoding.Default.GetString(tag);
        if (tagStr == "FILE")
        {
            throw new NotImplementedException($"unknown tag = {tagStr} at offset = {recordOffset} of length {length}");
        }
        else if (tagStr == "FREE")
        {
            return FreeRecord.Read(ggpkFileStream, length, recordOffset);
        }
        else if (tagStr == "PDIR")
        {
            throw new NotImplementedException($"unknown tag = {tagStr} at offset = {recordOffset} of length {length}");
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
