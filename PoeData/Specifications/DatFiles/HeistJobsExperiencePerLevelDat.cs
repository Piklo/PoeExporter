// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistJobsExperiencePerLevel.dat data.
/// </summary>
public sealed partial class HeistJobsExperiencePerLevelDat
{
    /// <summary> Gets HeistJobsKey.</summary>
    /// <remarks> references <see cref="HeistJobsDat"/> on <see cref="Specification.GetHeistJobsDat"/> index.</remarks>
    public required int? HeistJobsKey { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets Experience.</summary>
    public required int Experience { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKey { get; init; }

    /// <summary>
    /// Gets HeistJobsExperiencePerLevelDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HeistJobsExperiencePerLevelDat.</returns>
    internal static HeistJobsExperiencePerLevelDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HeistJobsExperiencePerLevel.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistJobsExperiencePerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading HeistJobsKey
            (var heistjobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Experience
            (var experienceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AchievementItemsKey
            (var tempachievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeyLoading = tempachievementitemskeyLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistJobsExperiencePerLevelDat()
            {
                HeistJobsKey = heistjobskeyLoading,
                Tier = tierLoading,
                Experience = experienceLoading,
                MinLevel = minlevelLoading,
                AchievementItemsKey = achievementitemskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
