// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing MonsterMortar.dat data.
/// </summary>
public sealed partial class MonsterMortarDat : ISpecificationFile<MonsterMortarDat>
{
    /// <summary> Gets Id.</summary>
    public required int Id { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required int? Unknown4 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int? Unknown20 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int? Unknown36 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int Unknown52 { get; init; }

    /// <summary> Gets a value indicating whether Unknown56 is set.</summary>
    public required bool Unknown56 { get; init; }

    /// <summary> Gets a value indicating whether Unknown57 is set.</summary>
    public required bool Unknown57 { get; init; }

    /// <summary> Gets a value indicating whether Unknown58 is set.</summary>
    public required bool Unknown58 { get; init; }

    /// <summary> Gets a value indicating whether Unknown59 is set.</summary>
    public required bool Unknown59 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int Unknown60 { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int Unknown68 { get; init; }

    /// <summary> Gets a value indicating whether Unknown72 is set.</summary>
    public required bool Unknown72 { get; init; }

    /// <summary> Gets Unknown73.</summary>
    public required int Unknown73 { get; init; }

    /// <summary> Gets a value indicating whether Unknown77 is set.</summary>
    public required bool Unknown77 { get; init; }

    /// <summary> Gets a value indicating whether Unknown78 is set.</summary>
    public required bool Unknown78 { get; init; }

    /// <summary> Gets Unknown79.</summary>
    public required int? Unknown79 { get; init; }

    /// <summary> Gets Unknown95.</summary>
    public required int Unknown95 { get; init; }

    /// <summary> Gets Unknown99.</summary>
    public required float Unknown99 { get; init; }

    /// <summary> Gets Unknown103.</summary>
    public required float Unknown103 { get; init; }

    /// <summary> Gets Unknown107.</summary>
    public required float Unknown107 { get; init; }

    /// <summary> Gets a value indicating whether Unknown111 is set.</summary>
    public required bool Unknown111 { get; init; }

    /// <summary> Gets Unknown112.</summary>
    public required int? Unknown112 { get; init; }

    /// <summary> Gets Unknown128.</summary>
    public required string Unknown128 { get; init; }

    /// <inheritdoc/>
    public static MonsterMortarDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MonsterMortar.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterMortarDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown57
            (var unknown57Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown58
            (var unknown58Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown59
            (var unknown59Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown73
            (var unknown73Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown77
            (var unknown77Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown78
            (var unknown78Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown79
            (var unknown79Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown95
            (var unknown95Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown99
            (var unknown99Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown103
            (var unknown103Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown107
            (var unknown107Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown111
            (var unknown111Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown112
            (var unknown112Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown128
            (var unknown128Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterMortarDat()
            {
                Id = idLoading,
                Unknown4 = unknown4Loading,
                Unknown20 = unknown20Loading,
                Unknown36 = unknown36Loading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
                Unknown57 = unknown57Loading,
                Unknown58 = unknown58Loading,
                Unknown59 = unknown59Loading,
                Unknown60 = unknown60Loading,
                Unknown64 = unknown64Loading,
                Unknown68 = unknown68Loading,
                Unknown72 = unknown72Loading,
                Unknown73 = unknown73Loading,
                Unknown77 = unknown77Loading,
                Unknown78 = unknown78Loading,
                Unknown79 = unknown79Loading,
                Unknown95 = unknown95Loading,
                Unknown99 = unknown99Loading,
                Unknown103 = unknown103Loading,
                Unknown107 = unknown107Loading,
                Unknown111 = unknown111Loading,
                Unknown112 = unknown112Loading,
                Unknown128 = unknown128Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
