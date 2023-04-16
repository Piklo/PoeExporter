// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BetrayalJobs.dat data.
/// </summary>
public sealed partial class BetrayalJobsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets ExtraTerrainFeaturesKey.</summary>
    /// <remarks> references <see cref="ExtraTerrainFeaturesDat"/> on <see cref="Specification.GetExtraTerrainFeaturesDat"/> index.</remarks>
    public required int? ExtraTerrainFeaturesKey { get; init; }

    /// <summary> Gets Art.</summary>
    public required string Art { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets Completion_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Completion_AchievementItemsKey { get; init; }

    /// <summary> Gets OpenChests_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> OpenChests_AchievementItemsKey { get; init; }

    /// <summary> Gets MissionCompletion_AcheivementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MissionCompletion_AcheivementItemsKey { get; init; }

    /// <summary>
    /// Gets BetrayalJobsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of BetrayalJobsDat.</returns>
    internal static BetrayalJobsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/BetrayalJobs.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BetrayalJobsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ExtraTerrainFeaturesKey
            (var extraterrainfeatureskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Art
            (var artLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Completion_AchievementItemsKey
            (var tempcompletion_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var completion_achievementitemskeyLoading = tempcompletion_achievementitemskeyLoading.AsReadOnly();

            // loading OpenChests_AchievementItemsKey
            (var tempopenchests_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var openchests_achievementitemskeyLoading = tempopenchests_achievementitemskeyLoading.AsReadOnly();

            // loading MissionCompletion_AcheivementItemsKey
            (var tempmissioncompletion_acheivementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var missioncompletion_acheivementitemskeyLoading = tempmissioncompletion_acheivementitemskeyLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BetrayalJobsDat()
            {
                Id = idLoading,
                Text = textLoading,
                ExtraTerrainFeaturesKey = extraterrainfeatureskeyLoading,
                Art = artLoading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                WorldAreasKey = worldareaskeyLoading,
                Completion_AchievementItemsKey = completion_achievementitemskeyLoading,
                OpenChests_AchievementItemsKey = openchests_achievementitemskeyLoading,
                MissionCompletion_AcheivementItemsKey = missioncompletion_acheivementitemskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
