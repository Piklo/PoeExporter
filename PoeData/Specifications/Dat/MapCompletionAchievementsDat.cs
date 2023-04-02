// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MapCompletionAchievements.dat data.
/// </summary>
public sealed partial class MapCompletionAchievementsDat : ISpecificationFile<MapCompletionAchievementsDat>
{
    /// <summary> Gets Unknown0.</summary>
    public required string Unknown0 { get; init; }

    /// <summary> Gets MapStatConditionsKeys.</summary>
    public required ReadOnlyCollection<int> MapStatConditionsKeys { get; init; }

    /// <summary> Gets StatsKeys.</summary>
    public required ReadOnlyCollection<int> StatsKeys { get; init; }

    /// <summary> Gets AchievementItemsKeys.</summary>
    public required ReadOnlyCollection<int> AchievementItemsKeys { get; init; }

    /// <summary> Gets MapTierAchievementsKeys.</summary>
    public required ReadOnlyCollection<int> MapTierAchievementsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown72 is set.</summary>
    public required bool Unknown72 { get; init; }

    /// <summary> Gets WorldAreasKeys.</summary>
    public required ReadOnlyCollection<int> WorldAreasKeys { get; init; }

    /// <inheritdoc/>
    public static MapCompletionAchievementsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MapCompletionAchievements.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapCompletionAchievementsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetMapStatConditionsDat();
            // specification.GetStatsDat();
            // specification.GetAchievementItemsDat();
            // specification.GetMapTierAchievementsDat();
            // specification.GetWorldAreasDat();

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MapStatConditionsKeys
            (var tempmapstatconditionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var mapstatconditionskeysLoading = tempmapstatconditionskeysLoading.AsReadOnly();

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading MapTierAchievementsKeys
            (var tempmaptierachievementskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var maptierachievementskeysLoading = tempmaptierachievementskeysLoading.AsReadOnly();

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading WorldAreasKeys
            (var tempworldareaskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var worldareaskeysLoading = tempworldareaskeysLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapCompletionAchievementsDat()
            {
                Unknown0 = unknown0Loading,
                MapStatConditionsKeys = mapstatconditionskeysLoading,
                StatsKeys = statskeysLoading,
                AchievementItemsKeys = achievementitemskeysLoading,
                MapTierAchievementsKeys = maptierachievementskeysLoading,
                Unknown72 = unknown72Loading,
                WorldAreasKeys = worldareaskeysLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
