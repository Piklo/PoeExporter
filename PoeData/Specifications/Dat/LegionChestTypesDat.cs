// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing LegionChestTypes.dat data.
/// </summary>
public sealed partial class LegionChestTypesDat : IDat<LegionChestTypesDat>
{
    /// <summary> Gets Unknown0.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? Unknown0 { get; init; }

    /// <summary> Gets Chest.</summary>
    /// <remarks> references <see cref="ChestsDat"/> on <see cref="Specification.GetChestsDat"/> index.</remarks>
    public required int? Chest { get; init; }

    /// <summary> Gets HardmodeChest.</summary>
    /// <remarks> references <see cref="ChestsDat"/> on <see cref="Specification.GetChestsDat"/> index.</remarks>
    public required int? HardmodeChest { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets Faction.</summary>
    /// <remarks> references <see cref="LegionFactionsDat"/> on <see cref="Specification.GetLegionFactionsDat"/> index.</remarks>
    public required int? Faction { get; init; }

    /// <inheritdoc/>
    public static LegionChestTypesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/LegionChestTypes.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LegionChestTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Chest
            (var chestLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HardmodeChest
            (var hardmodechestLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Faction
            (var factionLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LegionChestTypesDat()
            {
                Unknown0 = unknown0Loading,
                Chest = chestLoading,
                HardmodeChest = hardmodechestLoading,
                Unknown48 = unknown48Loading,
                Faction = factionLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
