// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Maps.dat data.
/// </summary>
public sealed partial class MapsDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets Regular_WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? Regular_WorldAreasKey { get; init; }

    /// <summary> Gets Unique_WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? Unique_WorldAreasKey { get; init; }

    /// <summary> Gets MapUpgrade_BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? MapUpgrade_BaseItemTypesKey { get; init; }

    /// <summary> Gets MonsterPacksKeys.</summary>
    /// <remarks> references <see cref="MonsterPacksDat"/> on <see cref="Specification.GetMonsterPacksDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MonsterPacksKeys { get; init; }

    /// <summary> Gets AchievementItem.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? AchievementItem { get; init; }

    /// <summary> Gets Regular_GuildCharacter.</summary>
    public required string Regular_GuildCharacter { get; init; }

    /// <summary> Gets Unique_GuildCharacter.</summary>
    public required string Unique_GuildCharacter { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets Shaped_Base_MapsKey.</summary>
    /// <remarks> references <see cref="MapsDat"/> on <see cref="Specification.GetMapsDat"/> index.</remarks>
    public required int? Shaped_Base_MapsKey { get; init; }

    /// <summary> Gets Shaped_AreaLevel.</summary>
    public required int Shaped_AreaLevel { get; init; }

    /// <summary> Gets UpgradedFrom_MapsKey.</summary>
    /// <remarks> references <see cref="MapsDat"/> on <see cref="Specification.GetMapsDat"/> index.</remarks>
    public required int? UpgradedFrom_MapsKey { get; init; }

    /// <summary> Gets MapsKey2.</summary>
    /// <remarks> references <see cref="MapsDat"/> on <see cref="Specification.GetMapsDat"/> index.</remarks>
    public required int? MapsKey2 { get; init; }

    /// <summary> Gets MapsKey3.</summary>
    /// <remarks> references <see cref="MapsDat"/> on <see cref="Specification.GetMapsDat"/> index.</remarks>
    public required int? MapsKey3 { get; init; }

    /// <summary> Gets MapSeriesKey.</summary>
    public required int MapSeriesKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown156 is set.</summary>
    public required bool Unknown156 { get; init; }

    /// <summary> Gets a value indicating whether Unknown157 is set.</summary>
    public required bool Unknown157 { get; init; }

    /// <summary> Gets a value indicating whether Unknown158 is set.</summary>
    public required bool Unknown158 { get; init; }

    /// <summary>
    /// Gets MapsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MapsDat.</returns>
    internal static MapsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Maps.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Regular_WorldAreasKey
            (var regular_worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unique_WorldAreasKey
            (var unique_worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MapUpgrade_BaseItemTypesKey
            (var mapupgrade_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MonsterPacksKeys
            (var tempmonsterpackskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monsterpackskeysLoading = tempmonsterpackskeysLoading.AsReadOnly();

            // loading AchievementItem
            (var achievementitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Regular_GuildCharacter
            (var regular_guildcharacterLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unique_GuildCharacter
            (var unique_guildcharacterLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Shaped_Base_MapsKey
            (var shaped_base_mapskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Shaped_AreaLevel
            (var shaped_arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading UpgradedFrom_MapsKey
            (var upgradedfrom_mapskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading MapsKey2
            (var mapskey2Loading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading MapsKey3
            (var mapskey3Loading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading MapSeriesKey
            (var mapserieskeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown156
            (var unknown156Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown157
            (var unknown157Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown158
            (var unknown158Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Regular_WorldAreasKey = regular_worldareaskeyLoading,
                Unique_WorldAreasKey = unique_worldareaskeyLoading,
                MapUpgrade_BaseItemTypesKey = mapupgrade_baseitemtypeskeyLoading,
                MonsterPacksKeys = monsterpackskeysLoading,
                AchievementItem = achievementitemLoading,
                Regular_GuildCharacter = regular_guildcharacterLoading,
                Unique_GuildCharacter = unique_guildcharacterLoading,
                Tier = tierLoading,
                Shaped_Base_MapsKey = shaped_base_mapskeyLoading,
                Shaped_AreaLevel = shaped_arealevelLoading,
                UpgradedFrom_MapsKey = upgradedfrom_mapskeyLoading,
                MapsKey2 = mapskey2Loading,
                MapsKey3 = mapskey3Loading,
                MapSeriesKey = mapserieskeyLoading,
                Unknown156 = unknown156Loading,
                Unknown157 = unknown157Loading,
                Unknown158 = unknown158Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
