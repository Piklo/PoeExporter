using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing AbyssObjects.dat data.
/// </summary>
public sealed class AbyssObjects : ISpecificationFile<AbyssObjects>
{
    // private readonly Specification specification;

    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets min level.</summary>
    public int MinLevel { get; init; }

    /// <summary> Gets max level.</summary>
    public int MaxLevel { get; init; }

    /// <summary> Gets spawn weight.</summary>
    public int SpawnWeight { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown0 { get; init; }

    /// <summary> Gets metadata file.</summary>
    public required string MetadataFile { get; init; } // files .ot .otc

    /// <summary> Gets unknown.</summary>
    public int Unknown1 { get; init; }

    /// <summary> Gets daemon spawners.</summary>
    public required ReadOnlyCollection<MonsterVarieties> DaemonSpawners { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown2 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown3 { get; init; }

    /// <summary> Gets abyssal depths.</summary>
    public required WorldAreas AbyssalDepths { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown4 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown5 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown6 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown7 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown8 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown9 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown10 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown11 { get; init; }

    /// <summary> Gets unknown.</summary>
    public int Unknown12 { get; init; }

    /// <summary>Gets a value indicating whether unknown is set.</summary>
    public bool Unknown13 { get; init; }

    ///// <summary>
    ///// Initializes a new instance of the <see cref="AbyssObjects"/> class.
    ///// </summary>
    ///// <param name="specification">Instance of <see cref="Specification"/> containing specification files.</param>
    // public AbyssObjects(Specification specification)
    // {
    //     this.specification = specification;
    // }

    /// <inheritdoc/>
    public static AbyssObjects[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/AbyssObjects.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var magicNumber = new byte[] { (byte)'\xBB', (byte)'\xbb', (byte)'\xBB', (byte)'\xbb', (byte)'\xBB', (byte)'\xbb', (byte)'\xBB', (byte)'\xbb' };
        var dataOffset = decompressedFile.IndexOfSubArray(magicNumber); // thats where the table ends?
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // var offset = 4 + (rowId * tableRecordLength);
            var end = offset + tableRecordLength;
            var dataRaw = decompressedFile[offset..end];

            if (dataRaw.Length == 0)
            {
                break;

                // continue;
            }

            var rowOffset = 0;
            (var test0, rowOffset) = BitConverterExtended.ToInt64(dataRaw, rowOffset);
            var str = FindString(decompressedFile, dataOffset, rowOffset);
            (var test1, rowOffset) = BitConverterExtended.ToInt32(dataRaw, rowOffset);
            (var test2, rowOffset) = BitConverterExtended.ToInt32(dataRaw, rowOffset);
            (var test3, rowOffset) = BitConverterExtended.ToInt32(dataRaw, rowOffset);
            (var test4, rowOffset) = BitConverterExtended.ToInt32(dataRaw, rowOffset);
            (var test5, rowOffset) = BitConverterExtended.ToInt64(dataRaw, rowOffset);
            (var test6, rowOffset) = BitConverterExtended.ToInt32(dataRaw, rowOffset);
            (var test7, rowOffset) = BitConverterExtended.ToInt64(dataRaw, rowOffset);
            (var test8, rowOffset) = BitConverterExtended.ToInt64(dataRaw, rowOffset);
            (var test9, rowOffset) = BitConverterExtended.ToInt32(dataRaw, rowOffset);
            (var test10, rowOffset) = BitConverterExtended.ToInt32(dataRaw, rowOffset);
            (var test11, rowOffset) = BitConverterExtended.ToInt64(dataRaw, rowOffset);
            (var test12, rowOffset) = BitConverterExtended.ToInt64(dataRaw, rowOffset);
            (var test13, rowOffset) = BitConverterExtended.ToInt32(dataRaw, rowOffset);
            (var test14, rowOffset) = BitConverterExtended.ToInt32(dataRaw, rowOffset);
            (var test15, rowOffset) = BitConverterExtended.ToInt32(dataRaw, rowOffset);
            (var test16, rowOffset) = BitConverterExtended.ToInt32(dataRaw, rowOffset);
            (var test17, rowOffset) = BitConverterExtended.ToInt32(dataRaw, rowOffset);
            (var test18, rowOffset) = BitConverterExtended.ToInt32(dataRaw, rowOffset);
            (var test19, rowOffset) = BitConverterExtended.ToInt32(dataRaw, rowOffset);
            (var test20, rowOffset) = BitConverterExtended.ToInt32(dataRaw, rowOffset);
            (var test21, rowOffset) = BitConverterExtended.ToInt32(dataRaw, rowOffset);
            (var test22, rowOffset) = BitConverterExtended.ToBoolean(dataRaw, rowOffset);
            offset += rowOffset;

            // (var test0, offset) = BitConverterExtended.ToInt64(dataRaw, offset);
            // (var test1, offset) = BitConverterExtended.ToInt32(dataRaw, offset);
            // (var test2, offset) = BitConverterExtended.ToInt32(dataRaw, offset);
            // (var test3, offset) = BitConverterExtended.ToInt32(dataRaw, offset);
            // (var test4, offset) = BitConverterExtended.ToInt32(dataRaw, offset);
            // (var test5, offset) = BitConverterExtended.ToInt64(dataRaw, offset);
            // (var test6, offset) = BitConverterExtended.ToInt32(dataRaw, offset);
            // (var test7, offset) = BitConverterExtended.ToInt64(dataRaw, offset);
            // (var test8, offset) = BitConverterExtended.ToInt64(dataRaw, offset);
            // (var test9, offset) = BitConverterExtended.ToInt32(dataRaw, offset);
            // (var test10, offset) = BitConverterExtended.ToInt32(dataRaw, offset);
            // (var test11, offset) = BitConverterExtended.ToInt64(dataRaw, offset);
            // (var test12, offset) = BitConverterExtended.ToInt64(dataRaw, offset);
            // (var test13, offset) = BitConverterExtended.ToInt32(dataRaw, offset);
            // (var test14, offset) = BitConverterExtended.ToInt32(dataRaw, offset);
            // (var test15, offset) = BitConverterExtended.ToInt32(dataRaw, offset);
            // (var test16, offset) = BitConverterExtended.ToInt32(dataRaw, offset);
            // (var test17, offset) = BitConverterExtended.ToInt32(dataRaw, offset);
            // (var test18, offset) = BitConverterExtended.ToInt32(dataRaw, offset);
            // (var test19, offset) = BitConverterExtended.ToInt32(dataRaw, offset);
            // (var test20, offset) = BitConverterExtended.ToInt32(dataRaw, offset);
            // (var test21, offset) = BitConverterExtended.ToInt32(dataRaw, offset);
            // (var test22, offset) = BitConverterExtended.ToBoolean(dataRaw, offset);
        }

        return Array.Empty<AbyssObjects>();
    }

    private static string FindString(byte[] data, int dataOffset, int rowOffset)
    {
        var beginningOfTheSequence = new byte[] { (byte)'\x00', (byte)'\x00', (byte)'\x00', (byte)'\x00' };
        var start = dataOffset + rowOffset;

        var offsetNew = data.IndexOfSubArray(beginningOfTheSequence, start);

        var str = string.Empty;
        if (start == offsetNew)
        {
            return str;
        }

        while ((offsetNew - start) % 2 == 1)
        {
            offsetNew = data.IndexOfSubArray(beginningOfTheSequence, offsetNew + 1);
        }

        str = Encoding.Unicode.GetString(data, start, offsetNew - start);

        return str;
    }
}
