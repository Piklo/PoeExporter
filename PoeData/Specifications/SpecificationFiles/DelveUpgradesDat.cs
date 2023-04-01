// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing DelveUpgrades.dat data.
/// </summary>
public sealed partial class DelveUpgradesDat : ISpecificationFile<DelveUpgradesDat>
{
    /// <summary> Gets DelveUpgradeTypeKey.</summary>
    public required int DelveUpgradeTypeKey { get; init; }

    /// <summary> Gets UpgradeLevel.</summary>
    public required int UpgradeLevel { get; init; }

    /// <summary> Gets StatsKeys.</summary>
    public required ReadOnlyCollection<int> StatsKeys { get; init; }

    /// <summary> Gets StatValues.</summary>
    public required ReadOnlyCollection<int> StatValues { get; init; }

    /// <summary> Gets Cost.</summary>
    public required int Cost { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets AchievementItemsKey.</summary>
    public required int? AchievementItemsKey { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <inheritdoc/>
    public static DelveUpgradesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/DelveUpgrades.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveUpgradesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetStatsDat();
            // specification.GetAchievementItemsDat();

            // loading DelveUpgradeTypeKey
            (var delveupgradetypekeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading UpgradeLevel
            (var upgradelevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading StatValues
            (var tempstatvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statvaluesLoading = tempstatvaluesLoading.AsReadOnly();

            // loading Cost
            (var costLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AchievementItemsKey
            (var achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveUpgradesDat()
            {
                DelveUpgradeTypeKey = delveupgradetypekeyLoading,
                UpgradeLevel = upgradelevelLoading,
                StatsKeys = statskeysLoading,
                StatValues = statvaluesLoading,
                Cost = costLoading,
                Unknown44 = unknown44Loading,
                AchievementItemsKey = achievementitemskeyLoading,
                Unknown64 = unknown64Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
