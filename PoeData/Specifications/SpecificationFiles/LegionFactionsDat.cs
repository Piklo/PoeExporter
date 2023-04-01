// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing LegionFactions.dat data.
/// </summary>
public sealed partial class LegionFactionsDat : ISpecificationFile<LegionFactionsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets LegionBalancePerLevelKey.</summary>
    public required int? LegionBalancePerLevelKey { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required float Unknown28 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required float Unknown32 { get; init; }

    /// <summary> Gets BuffVisualsKey.</summary>
    public required int? BuffVisualsKey { get; init; }

    /// <summary> Gets MiscAnimatedKey1.</summary>
    public required int? MiscAnimatedKey1 { get; init; }

    /// <summary> Gets MiscAnimatedKey2.</summary>
    public required int? MiscAnimatedKey2 { get; init; }

    /// <summary> Gets MiscAnimatedKey3.</summary>
    public required int? MiscAnimatedKey3 { get; init; }

    /// <summary> Gets AchievementItemsKeys1.</summary>
    public required ReadOnlyCollection<int> AchievementItemsKeys1 { get; init; }

    /// <summary> Gets MiscAnimatedKey4.</summary>
    public required int? MiscAnimatedKey4 { get; init; }

    /// <summary> Gets MiscAnimatedKey5.</summary>
    public required int? MiscAnimatedKey5 { get; init; }

    /// <summary> Gets Unknown148.</summary>
    public required float Unknown148 { get; init; }

    /// <summary> Gets Unknown152.</summary>
    public required float Unknown152 { get; init; }

    /// <summary> Gets AchievementItemsKeys2.</summary>
    public required ReadOnlyCollection<int> AchievementItemsKeys2 { get; init; }

    /// <summary> Gets StatsKey.</summary>
    public required int? StatsKey { get; init; }

    /// <summary> Gets Shard.</summary>
    public required string Shard { get; init; }

    /// <summary> Gets RewardJewelArt.</summary>
    public required string RewardJewelArt { get; init; }

    /// <inheritdoc/>
    public static LegionFactionsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/LegionFactions.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LegionFactionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetLegionBalancePerLevelDat();
            // specification.GetBuffVisualsDat();
            // specification.GetMiscAnimatedDat();
            // specification.GetAchievementItemsDat();
            // specification.GetStatsDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LegionBalancePerLevelKey
            (var legionbalanceperlevelkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading BuffVisualsKey
            (var buffvisualskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MiscAnimatedKey1
            (var miscanimatedkey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MiscAnimatedKey2
            (var miscanimatedkey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MiscAnimatedKey3
            (var miscanimatedkey3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading AchievementItemsKeys1
            (var tempachievementitemskeys1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeys1Loading = tempachievementitemskeys1Loading.AsReadOnly();

            // loading MiscAnimatedKey4
            (var miscanimatedkey4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MiscAnimatedKey5
            (var miscanimatedkey5Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown148
            (var unknown148Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown152
            (var unknown152Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading AchievementItemsKeys2
            (var tempachievementitemskeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeys2Loading = tempachievementitemskeys2Loading.AsReadOnly();

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Shard
            (var shardLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RewardJewelArt
            (var rewardjewelartLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LegionFactionsDat()
            {
                Id = idLoading,
                SpawnWeight = spawnweightLoading,
                LegionBalancePerLevelKey = legionbalanceperlevelkeyLoading,
                Unknown28 = unknown28Loading,
                Unknown32 = unknown32Loading,
                BuffVisualsKey = buffvisualskeyLoading,
                MiscAnimatedKey1 = miscanimatedkey1Loading,
                MiscAnimatedKey2 = miscanimatedkey2Loading,
                MiscAnimatedKey3 = miscanimatedkey3Loading,
                AchievementItemsKeys1 = achievementitemskeys1Loading,
                MiscAnimatedKey4 = miscanimatedkey4Loading,
                MiscAnimatedKey5 = miscanimatedkey5Loading,
                Unknown148 = unknown148Loading,
                Unknown152 = unknown152Loading,
                AchievementItemsKeys2 = achievementitemskeys2Loading,
                StatsKey = statskeyLoading,
                Shard = shardLoading,
                RewardJewelArt = rewardjewelartLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
