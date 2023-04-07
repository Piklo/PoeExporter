// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MoveDaemon.dat data.
/// </summary>
public sealed partial class MoveDaemonDat
{
    /// <summary> Gets Unknown0.</summary>
    public required int Unknown0 { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required int Unknown4 { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required int Unknown12 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int Unknown20 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int Unknown52 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int Unknown60 { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int Unknown68 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }

    /// <summary> Gets a value indicating whether Unknown76 is set.</summary>
    public required bool Unknown76 { get; init; }

    /// <summary> Gets a value indicating whether Unknown77 is set.</summary>
    public required bool Unknown77 { get; init; }

    /// <summary> Gets a value indicating whether Unknown78 is set.</summary>
    public required bool Unknown78 { get; init; }

    /// <summary> Gets Unknown79.</summary>
    public required int Unknown79 { get; init; }

    /// <summary> Gets Unknown83.</summary>
    public required int Unknown83 { get; init; }

    /// <summary> Gets Unknown87.</summary>
    public required int Unknown87 { get; init; }

    /// <summary> Gets Unknown91.</summary>
    public required int Unknown91 { get; init; }

    /// <summary> Gets Unknown95.</summary>
    public required int Unknown95 { get; init; }

    /// <summary> Gets Unknown99.</summary>
    public required int Unknown99 { get; init; }

    /// <summary> Gets a value indicating whether Unknown103 is set.</summary>
    public required bool Unknown103 { get; init; }

    /// <summary> Gets Unknown104.</summary>
    public required string Unknown104 { get; init; }

    /// <summary> Gets Unknown112.</summary>
    public required int Unknown112 { get; init; }

    /// <summary> Gets a value indicating whether Unknown116 is set.</summary>
    public required bool Unknown116 { get; init; }

    /// <summary> Gets a value indicating whether Unknown117 is set.</summary>
    public required bool Unknown117 { get; init; }

    /// <summary> Gets Unknown118.</summary>
    public required int Unknown118 { get; init; }

    /// <inheritdoc/>
    public static MoveDaemonDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/MoveDaemon.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MoveDaemonDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown77
            (var unknown77Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown78
            (var unknown78Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown79
            (var unknown79Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown83
            (var unknown83Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown87
            (var unknown87Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown91
            (var unknown91Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown95
            (var unknown95Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown99
            (var unknown99Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown103
            (var unknown103Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown117
            (var unknown117Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown118
            (var unknown118Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MoveDaemonDat()
            {
                Unknown0 = unknown0Loading,
                Unknown4 = unknown4Loading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
                Unknown60 = unknown60Loading,
                Unknown64 = unknown64Loading,
                Unknown68 = unknown68Loading,
                Unknown72 = unknown72Loading,
                Unknown76 = unknown76Loading,
                Unknown77 = unknown77Loading,
                Unknown78 = unknown78Loading,
                Unknown79 = unknown79Loading,
                Unknown83 = unknown83Loading,
                Unknown87 = unknown87Loading,
                Unknown91 = unknown91Loading,
                Unknown95 = unknown95Loading,
                Unknown99 = unknown99Loading,
                Unknown103 = unknown103Loading,
                Unknown104 = unknown104Loading,
                Unknown112 = unknown112Loading,
                Unknown116 = unknown116Loading,
                Unknown117 = unknown117Loading,
                Unknown118 = unknown118Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
