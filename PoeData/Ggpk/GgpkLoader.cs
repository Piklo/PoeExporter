using PoeData.Ggpk.GgpkRecords;
using Serilog;

namespace PoeData.Ggpk;

/// <summary>
/// Class containing .ggpk data.
/// </summary>
internal sealed class GgpkLoader : IDataLoader
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
    private readonly DirectoryNode rootDirectoryNode;

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

        rootDirectoryNode = BuildRootDirectory();

        var nodes = new Queue<(DirectoryNode parent, DirectoryRecordEntry child)>();

        if (rootDirectoryNode.Record.TryPickT0(out var directoryRecordGgpk, out _))
        {
            foreach (var entry in directoryRecordGgpk.Entries)
            {
                nodes.Enqueue((rootDirectoryNode, entry));
            }
        }

        while (nodes.Count != 0)
        {
            var entry = nodes.Dequeue();
            var offset = entry.child.Offset;

            if (directoryRecords.TryGetValue(offset, out var directoryRecord))
            {
                var node = new DirectoryNode() { Parent = entry.parent, Record = directoryRecord };

                entry.parent.Children.Add(node);

                foreach (var recordEntry in directoryRecord.Entries)
                {
                    nodes.Enqueue((node, recordEntry));
                }
            }
            else if (fileRecords.TryGetValue(offset, out var fileRecord))
            {
                var node = new DirectoryNode() { Parent = entry.parent, Record = fileRecord };

                entry.parent.Children.Add(node);
            }
            else
            {
                logger.Error("unknown record type");
                throw new NotImplementedException();
            }
        }

        var indexBytes = ReadIndex();
        this.logger.Verbose("created {count} records", records.Count);
    }

    /// <inheritdoc/>
    public byte[] GetFileBytes(string filePath)
    {
        var splitPath = filePath.Replace("\\", " ").Replace("/", " ").Split(" ");

        var currentDirectoryNode = rootDirectoryNode;
        foreach (var currentPath in splitPath)
        {
            var foundCurrent = false;
            foreach (var child in currentDirectoryNode.Children)
            {
                if (child.Name == currentPath)
                {
                    currentDirectoryNode = child;
                    foundCurrent = true;
                    break;
                }
            }

            if (!foundCurrent)
            {
                throw new PathNotFoundException($"failed to find {currentPath}");
            }
        }

        currentDirectoryNode.Record.TryPickT1(out var fileRecord, out _);

        using var ggpkFileStream = File.OpenRead(ggpkPath);
        ggpkFileStream.Seek(fileRecord.DataStart, SeekOrigin.Begin);

        var bytes = new byte[fileRecord.DataLength];
        ggpkFileStream.Read(bytes);

        return bytes;
    }

    private byte[] ReadIndex()
    {
        var bytes = GetFileBytes(DataLoader.IndexPath);
        return bytes;
    }

    private DirectoryNode BuildRootDirectory()
    {
        foreach (var offset in ggpkRecord.Offsets)
        {
            if (records.TryGetValue(offset, out var record) && record is DirectoryRecordGgpk directoryRecord)
            {
                var root = new DirectoryNode() { Parent = null, Record = directoryRecord };
                return root;
            }
        }

        logger.Error("{ggpkRecord} doesnt contain a directory record", nameof(ggpkRecord));
        throw new NotImplementedException();
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
