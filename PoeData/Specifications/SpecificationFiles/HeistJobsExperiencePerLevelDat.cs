// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing HeistJobsExperiencePerLevel.dat data.
/// </summary>
public sealed partial class HeistJobsExperiencePerLevelDat : ISpecificationFile<HeistJobsExperiencePerLevelDat>
{
    /// <summary> Gets HeistJobsKey.</summary>
    public required int? HeistJobsKey { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets Experience.</summary>
    public required int Experience { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets AchievementItemsKey.</summary>
    public required ReadOnlyCollection<int> AchievementItemsKey { get; init; }

    /// <inheritdoc/>
    public static HeistJobsExperiencePerLevelDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/HeistJobsExperiencePerLevel.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

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

            // loading referenced tables if any
            // specification.GetHeistJobsDat();
            // specification.GetAchievementItemsDat();

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
