// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing PantheonPanelLayout.dat data.
/// </summary>
public sealed partial class PantheonPanelLayoutDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets X.</summary>
    public required int X { get; init; }

    /// <summary> Gets Y.</summary>
    public required int Y { get; init; }

    /// <summary> Gets a value indicating whether IsMajorGod is set.</summary>
    public required bool IsMajorGod { get; init; }

    /// <summary> Gets CoverImage.</summary>
    public required string CoverImage { get; init; }

    /// <summary> Gets GodName2.</summary>
    public required string GodName2 { get; init; }

    /// <summary> Gets SelectionImage.</summary>
    public required string SelectionImage { get; init; }

    /// <summary> Gets Effect1_StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Effect1_StatsKeys { get; init; }

    /// <summary> Gets Effect1_Values.</summary>
    public required ReadOnlyCollection<int> Effect1_Values { get; init; }

    /// <summary> Gets Effect2_StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Effect2_StatsKeys { get; init; }

    /// <summary> Gets GodName3.</summary>
    public required string GodName3 { get; init; }

    /// <summary> Gets Effect3_Values.</summary>
    public required ReadOnlyCollection<int> Effect3_Values { get; init; }

    /// <summary> Gets Effect3_StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Effect3_StatsKeys { get; init; }

    /// <summary> Gets GodName4.</summary>
    public required string GodName4 { get; init; }

    /// <summary> Gets Effect4_StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Effect4_StatsKeys { get; init; }

    /// <summary> Gets Effect4_Values.</summary>
    public required ReadOnlyCollection<int> Effect4_Values { get; init; }

    /// <summary> Gets GodName1.</summary>
    public required string GodName1 { get; init; }

    /// <summary> Gets Effect2_Values.</summary>
    public required ReadOnlyCollection<int> Effect2_Values { get; init; }

    /// <summary> Gets QuestState1.</summary>
    /// <remarks> references <see cref="QuestStatesDat"/> on <see cref="Specification.GetQuestStatesDat"/> index.</remarks>
    public required int? QuestState1 { get; init; }

    /// <summary> Gets QuestState2.</summary>
    /// <remarks> references <see cref="QuestStatesDat"/> on <see cref="Specification.GetQuestStatesDat"/> index.</remarks>
    public required int? QuestState2 { get; init; }

    /// <summary> Gets QuestState3.</summary>
    /// <remarks> references <see cref="QuestStatesDat"/> on <see cref="Specification.GetQuestStatesDat"/> index.</remarks>
    public required int? QuestState3 { get; init; }

    /// <summary> Gets QuestState4.</summary>
    /// <remarks> references <see cref="QuestStatesDat"/> on <see cref="Specification.GetQuestStatesDat"/> index.</remarks>
    public required int? QuestState4 { get; init; }

    /// <summary> Gets a value indicating whether IsDisabled is set.</summary>
    public required bool IsDisabled { get; init; }

    /// <summary> Gets AchievementItems.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItems { get; init; }

    /// <summary>
    /// Gets PantheonPanelLayoutDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of PantheonPanelLayoutDat.</returns>
    internal static PantheonPanelLayoutDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/PantheonPanelLayout.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PantheonPanelLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading X
            (var xLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Y
            (var yLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsMajorGod
            (var ismajorgodLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CoverImage
            (var coverimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GodName2
            (var godname2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SelectionImage
            (var selectionimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Effect1_StatsKeys
            (var tempeffect1_statskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var effect1_statskeysLoading = tempeffect1_statskeysLoading.AsReadOnly();

            // loading Effect1_Values
            (var tempeffect1_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var effect1_valuesLoading = tempeffect1_valuesLoading.AsReadOnly();

            // loading Effect2_StatsKeys
            (var tempeffect2_statskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var effect2_statskeysLoading = tempeffect2_statskeysLoading.AsReadOnly();

            // loading GodName3
            (var godname3Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Effect3_Values
            (var tempeffect3_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var effect3_valuesLoading = tempeffect3_valuesLoading.AsReadOnly();

            // loading Effect3_StatsKeys
            (var tempeffect3_statskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var effect3_statskeysLoading = tempeffect3_statskeysLoading.AsReadOnly();

            // loading GodName4
            (var godname4Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Effect4_StatsKeys
            (var tempeffect4_statskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var effect4_statskeysLoading = tempeffect4_statskeysLoading.AsReadOnly();

            // loading Effect4_Values
            (var tempeffect4_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var effect4_valuesLoading = tempeffect4_valuesLoading.AsReadOnly();

            // loading GodName1
            (var godname1Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Effect2_Values
            (var tempeffect2_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var effect2_valuesLoading = tempeffect2_valuesLoading.AsReadOnly();

            // loading QuestState1
            (var queststate1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading QuestState2
            (var queststate2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading QuestState3
            (var queststate3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading QuestState4
            (var queststate4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading IsDisabled
            (var isdisabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AchievementItems
            (var tempachievementitemsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemsLoading = tempachievementitemsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PantheonPanelLayoutDat()
            {
                Id = idLoading,
                X = xLoading,
                Y = yLoading,
                IsMajorGod = ismajorgodLoading,
                CoverImage = coverimageLoading,
                GodName2 = godname2Loading,
                SelectionImage = selectionimageLoading,
                Effect1_StatsKeys = effect1_statskeysLoading,
                Effect1_Values = effect1_valuesLoading,
                Effect2_StatsKeys = effect2_statskeysLoading,
                GodName3 = godname3Loading,
                Effect3_Values = effect3_valuesLoading,
                Effect3_StatsKeys = effect3_statskeysLoading,
                GodName4 = godname4Loading,
                Effect4_StatsKeys = effect4_statskeysLoading,
                Effect4_Values = effect4_valuesLoading,
                GodName1 = godname1Loading,
                Effect2_Values = effect2_valuesLoading,
                QuestState1 = queststate1Loading,
                QuestState2 = queststate2Loading,
                QuestState3 = queststate3Loading,
                QuestState4 = queststate4Loading,
                IsDisabled = isdisabledLoading,
                AchievementItems = achievementitemsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
