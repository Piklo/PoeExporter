// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HellscapeMonsterPacks.dat data.
/// </summary>
public sealed partial class HellscapeMonsterPacksDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MonsterPack.</summary>
    /// <remarks> references <see cref="MonsterPacksDat"/> on <see cref="Specification.LoadMonsterPacksDat"/> index.</remarks>
    public required int? MonsterPack { get; init; }

    /// <summary> Gets Faction.</summary>
    /// <remarks> references <see cref="HellscapeFactionsDat"/> on <see cref="Specification.LoadHellscapeFactionsDat"/> index.</remarks>
    public required int? Faction { get; init; }

    /// <summary> Gets a value indicating whether IsDonutPack is set.</summary>
    public required bool IsDonutPack { get; init; }

    /// <summary> Gets a value indicating whether IsElite is set.</summary>
    public required bool IsElite { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary>
    /// Gets HellscapeMonsterPacksDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HellscapeMonsterPacksDat.</returns>
    internal static HellscapeMonsterPacksDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HellscapeMonsterPacks.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

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

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterPack
            (var monsterpackLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Faction
            (var factionLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

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
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
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
