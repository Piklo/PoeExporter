// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MapTierAchievements.dat data.
/// </summary>
public sealed partial class MapTierAchievementsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKey { get; init; }

    /// <summary> Gets MapTiers.</summary>
    public required ReadOnlyCollection<int> MapTiers { get; init; }

    /// <summary>
    /// Gets MapTierAchievementsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MapTierAchievementsDat.</returns>
    internal static MapTierAchievementsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MapTierAchievements.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

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
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
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
