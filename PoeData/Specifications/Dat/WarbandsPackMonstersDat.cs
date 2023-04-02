// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing WarbandsPackMonsters.dat data.
/// </summary>
public sealed partial class WarbandsPackMonstersDat : ISpecificationFile<WarbandsPackMonstersDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether Unknown12 is set.</summary>
    public required bool Unknown12 { get; init; }

    /// <summary> Gets a value indicating whether Unknown13 is set.</summary>
    public required bool Unknown13 { get; init; }

    /// <summary> Gets a value indicating whether Unknown14 is set.</summary>
    public required bool Unknown14 { get; init; }

    /// <summary> Gets a value indicating whether Unknown15 is set.</summary>
    public required bool Unknown15 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Tier4_MonsterVarietiesKeys.</summary>
    public required ReadOnlyCollection<int> Tier4_MonsterVarietiesKeys { get; init; }

    /// <summary> Gets Tier3_MonsterVarietiesKeys.</summary>
    public required ReadOnlyCollection<int> Tier3_MonsterVarietiesKeys { get; init; }

    /// <summary> Gets Tier2_MonsterVarietiesKeys.</summary>
    public required ReadOnlyCollection<int> Tier2_MonsterVarietiesKeys { get; init; }

    /// <summary> Gets Tier1_MonsterVarietiesKeys.</summary>
    public required ReadOnlyCollection<int> Tier1_MonsterVarietiesKeys { get; init; }

    /// <summary> Gets Tier1Name.</summary>
    public required string Tier1Name { get; init; }

    /// <summary> Gets Tier2Name.</summary>
    public required string Tier2Name { get; init; }

    /// <summary> Gets Tier3Name.</summary>
    public required string Tier3Name { get; init; }

    /// <summary> Gets Tier4Name.</summary>
    public required string Tier4Name { get; init; }

    /// <summary> Gets Tier1Art.</summary>
    public required string Tier1Art { get; init; }

    /// <summary> Gets Tier2Art.</summary>
    public required string Tier2Art { get; init; }

    /// <summary> Gets Tier3Art.</summary>
    public required string Tier3Art { get; init; }

    /// <summary> Gets Tier4Art.</summary>
    public required string Tier4Art { get; init; }

    /// <inheritdoc/>
    public static WarbandsPackMonstersDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/WarbandsPackMonsters.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WarbandsPackMonstersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetMonsterVarietiesDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown13
            (var unknown13Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown14
            (var unknown14Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown15
            (var unknown15Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier4_MonsterVarietiesKeys
            (var temptier4_monstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tier4_monstervarietieskeysLoading = temptier4_monstervarietieskeysLoading.AsReadOnly();

            // loading Tier3_MonsterVarietiesKeys
            (var temptier3_monstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tier3_monstervarietieskeysLoading = temptier3_monstervarietieskeysLoading.AsReadOnly();

            // loading Tier2_MonsterVarietiesKeys
            (var temptier2_monstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tier2_monstervarietieskeysLoading = temptier2_monstervarietieskeysLoading.AsReadOnly();

            // loading Tier1_MonsterVarietiesKeys
            (var temptier1_monstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tier1_monstervarietieskeysLoading = temptier1_monstervarietieskeysLoading.AsReadOnly();

            // loading Tier1Name
            (var tier1nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier2Name
            (var tier2nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier3Name
            (var tier3nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier4Name
            (var tier4nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier1Art
            (var tier1artLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier2Art
            (var tier2artLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier3Art
            (var tier3artLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier4Art
            (var tier4artLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WarbandsPackMonstersDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Unknown13 = unknown13Loading,
                Unknown14 = unknown14Loading,
                Unknown15 = unknown15Loading,
                Unknown16 = unknown16Loading,
                Tier4_MonsterVarietiesKeys = tier4_monstervarietieskeysLoading,
                Tier3_MonsterVarietiesKeys = tier3_monstervarietieskeysLoading,
                Tier2_MonsterVarietiesKeys = tier2_monstervarietieskeysLoading,
                Tier1_MonsterVarietiesKeys = tier1_monstervarietieskeysLoading,
                Tier1Name = tier1nameLoading,
                Tier2Name = tier2nameLoading,
                Tier3Name = tier3nameLoading,
                Tier4Name = tier4nameLoading,
                Tier1Art = tier1artLoading,
                Tier2Art = tier2artLoading,
                Tier3Art = tier3artLoading,
                Tier4Art = tier4artLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
