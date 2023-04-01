// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing HellscapeMonsterPacks.dat data.
/// </summary>
public sealed partial class HellscapeMonsterPacksDat : ISpecificationFile<HellscapeMonsterPacksDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MonsterPack.</summary>
    public required int? MonsterPack { get; init; }

    /// <summary> Gets Faction.</summary>
    public required int? Faction { get; init; }

    /// <summary> Gets a value indicating whether IsDonutPack is set.</summary>
    public required bool IsDonutPack { get; init; }

    /// <summary> Gets a value indicating whether IsElite is set.</summary>
    public required bool IsElite { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <inheritdoc/>
    public static HellscapeMonsterPacksDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/HellscapeMonsterPacks.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HellscapeMonsterPacksDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetMonsterPacksDat();
            // specification.GetHellscapeFactionsDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterPack
            (var monsterpackLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Faction
            (var factionLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading IsDonutPack
            (var isdonutpackLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsElite
            (var iseliteLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HellscapeMonsterPacksDat()
            {
                Id = idLoading,
                MonsterPack = monsterpackLoading,
                Faction = factionLoading,
                IsDonutPack = isdonutpackLoading,
                IsElite = iseliteLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
