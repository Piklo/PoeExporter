// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MonsterTypes.dat data.
/// </summary>
public sealed partial class MonsterTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether IsSummoned is set.</summary>
    public required bool IsSummoned { get; init; }

    /// <summary> Gets Armour.</summary>
    public required int Armour { get; init; }

    /// <summary> Gets Evasion.</summary>
    public required int Evasion { get; init; }

    /// <summary> Gets EnergyShieldFromLife.</summary>
    public required int EnergyShieldFromLife { get; init; }

    /// <summary> Gets DamageSpread.</summary>
    public required int DamageSpread { get; init; }

    /// <summary> Gets MonsterResistancesKey.</summary>
    /// <remarks> references <see cref="MonsterResistancesDat"/> on <see cref="Specification.GetMonsterResistancesDat"/> index.</remarks>
    public required int? MonsterResistancesKey { get; init; }

    /// <summary> Gets a value indicating whether IsLargeAbyssMonster is set.</summary>
    public required bool IsLargeAbyssMonster { get; init; }

    /// <summary> Gets a value indicating whether IsSmallAbyssMonster is set.</summary>
    public required bool IsSmallAbyssMonster { get; init; }

    /// <summary> Gets a value indicating whether Unknown47 is set.</summary>
    public required bool Unknown47 { get; init; }

    /// <summary>
    /// Gets MonsterTypesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MonsterTypesDat.</returns>
    internal static MonsterTypesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MonsterTypes.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsSummoned
            (var issummonedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Armour
            (var armourLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Evasion
            (var evasionLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EnergyShieldFromLife
            (var energyshieldfromlifeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DamageSpread
            (var damagespreadLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterResistancesKey
            (var monsterresistanceskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading IsLargeAbyssMonster
            (var islargeabyssmonsterLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsSmallAbyssMonster
            (var issmallabyssmonsterLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown47
            (var unknown47Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterTypesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                IsSummoned = issummonedLoading,
                Armour = armourLoading,
                Evasion = evasionLoading,
                EnergyShieldFromLife = energyshieldfromlifeLoading,
                DamageSpread = damagespreadLoading,
                MonsterResistancesKey = monsterresistanceskeyLoading,
                IsLargeAbyssMonster = islargeabyssmonsterLoading,
                IsSmallAbyssMonster = issmallabyssmonsterLoading,
                Unknown47 = unknown47Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
