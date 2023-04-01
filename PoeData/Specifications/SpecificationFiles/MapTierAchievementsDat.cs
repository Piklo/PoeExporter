// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing MapTierAchievements.dat data.
/// </summary>
public sealed partial class MapTierAchievementsDat : ISpecificationFile<MapTierAchievementsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets AchievementItemsKey.</summary>
    public required ReadOnlyCollection<int> AchievementItemsKey { get; init; }

    /// <summary> Gets MapTiers.</summary>
    public required ReadOnlyCollection<int> MapTiers { get; init; }

    /// <inheritdoc/>
    public static MapTierAchievementsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MapTierAchievements.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapTierAchievementsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetAchievementItemsDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AchievementItemsKey
            (var tempachievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeyLoading = tempachievementitemskeyLoading.AsReadOnly();

            // loading MapTiers
            (var tempmaptiersLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var maptiersLoading = tempmaptiersLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapTierAchievementsDat()
            {
                Id = idLoading,
                AchievementItemsKey = achievementitemskeyLoading,
                MapTiers = maptiersLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
