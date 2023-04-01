// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing StrDexIntMissions.dat data.
/// </summary>
public sealed partial class StrDexIntMissionsDat : ISpecificationFile<StrDexIntMissionsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required int Unknown12 { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int? Unknown20 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int? Unknown36 { get; init; }

    /// <summary> Gets Extra_ModsKeys.</summary>
    public required ReadOnlyCollection<int> Extra_ModsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown68 is set.</summary>
    public required bool Unknown68 { get; init; }

    /// <summary> Gets a value indicating whether Unknown69 is set.</summary>
    public required bool Unknown69 { get; init; }

    /// <summary> Gets a value indicating whether Unknown70 is set.</summary>
    public required bool Unknown70 { get; init; }

    /// <summary> Gets Unknown71.</summary>
    public required int? Unknown71 { get; init; }

    /// <summary> Gets Unknown87.</summary>
    public required int Unknown87 { get; init; }

    /// <summary> Gets Unknown91.</summary>
    public required int Unknown91 { get; init; }

    /// <summary> Gets Unknown95.</summary>
    public required int Unknown95 { get; init; }

    /// <summary> Gets Unknown99.</summary>
    public required int Unknown99 { get; init; }

    /// <summary> Gets Unknown103.</summary>
    public required int? Unknown103 { get; init; }

    /// <summary> Gets Unknown119.</summary>
    public required int? Unknown119 { get; init; }

    /// <summary> Gets Unknown135.</summary>
    public required int? Unknown135 { get; init; }

    /// <summary> Gets a value indicating whether Unknown151 is set.</summary>
    public required bool Unknown151 { get; init; }

    /// <summary> Gets Unknown152.</summary>
    public required int? Unknown152 { get; init; }

    /// <summary> Gets a value indicating whether Unknown168 is set.</summary>
    public required bool Unknown168 { get; init; }

    /// <summary> Gets Unknown169.</summary>
    public required int? Unknown169 { get; init; }

    /// <inheritdoc/>
    public static StrDexIntMissionsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/StrDexIntMissions.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new StrDexIntMissionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetModsDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Extra_ModsKeys
            (var tempextra_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var extra_modskeysLoading = tempextra_modskeysLoading.AsReadOnly();

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown69
            (var unknown69Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown70
            (var unknown70Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown71
            (var unknown71Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown87
            (var unknown87Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown91
            (var unknown91Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown95
            (var unknown95Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown99
            (var unknown99Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown103
            (var unknown103Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown119
            (var unknown119Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown135
            (var unknown135Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown151
            (var unknown151Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown152
            (var unknown152Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown168
            (var unknown168Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown169
            (var unknown169Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new StrDexIntMissionsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                SpawnWeight = spawnweightLoading,
                Unknown20 = unknown20Loading,
                Unknown36 = unknown36Loading,
                Extra_ModsKeys = extra_modskeysLoading,
                Unknown68 = unknown68Loading,
                Unknown69 = unknown69Loading,
                Unknown70 = unknown70Loading,
                Unknown71 = unknown71Loading,
                Unknown87 = unknown87Loading,
                Unknown91 = unknown91Loading,
                Unknown95 = unknown95Loading,
                Unknown99 = unknown99Loading,
                Unknown103 = unknown103Loading,
                Unknown119 = unknown119Loading,
                Unknown135 = unknown135Loading,
                Unknown151 = unknown151Loading,
                Unknown152 = unknown152Loading,
                Unknown168 = unknown168Loading,
                Unknown169 = unknown169Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
