// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Achievements.dat data.
/// </summary>
public sealed partial class AchievementsDat : IDat<AchievementsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets SetId.</summary>
    /// <remarks> references <see cref="AchievementSetsDisplayDat"/> on <see cref="AchievementSetsDisplayDat.Id"/>.</remarks>
    public required int SetId { get; init; }

    /// <summary> Gets Objective.</summary>
    public required string Objective { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets a value indicating whether Unknown32 is set.</summary>
    public required bool Unknown32 { get; init; }

    /// <summary> Gets a value indicating whether HideAchievementItems is set.</summary>
    public required bool HideAchievementItems { get; init; }

    /// <summary> Gets a value indicating whether Unknown34 is set.</summary>
    public required bool Unknown34 { get; init; }

    /// <summary> Gets MinCompletedItems.</summary>
    public required int MinCompletedItems { get; init; }

    /// <summary> Gets a value indicating whether TwoColumnLayout is set.</summary>
    public required bool TwoColumnLayout { get; init; }

    /// <summary> Gets a value indicating whether ShowItemCompletionsAsOne is set.</summary>
    public required bool ShowItemCompletionsAsOne { get; init; }

    /// <summary> Gets Unknown41.</summary>
    public required string Unknown41 { get; init; }

    /// <summary> Gets a value indicating whether SoftcoreOnly is set.</summary>
    public required bool SoftcoreOnly { get; init; }

    /// <summary> Gets a value indicating whether HardcoreOnly is set.</summary>
    public required bool HardcoreOnly { get; init; }

    /// <summary> Gets a value indicating whether Unknown51 is set.</summary>
    public required bool Unknown51 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required string Unknown52 { get; init; }

    /// <inheritdoc/>
    public static AchievementsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/Achievements.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AchievementsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SetId
            (var setidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Objective
            (var objectiveLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HideAchievementItems
            (var hideachievementitemsLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown34
            (var unknown34Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MinCompletedItems
            (var mincompleteditemsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TwoColumnLayout
            (var twocolumnlayoutLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ShowItemCompletionsAsOne
            (var showitemcompletionsasoneLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown41
            (var unknown41Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SoftcoreOnly
            (var softcoreonlyLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HardcoreOnly
            (var hardcoreonlyLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown51
            (var unknown51Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AchievementsDat()
            {
                Id = idLoading,
                Description = descriptionLoading,
                SetId = setidLoading,
                Objective = objectiveLoading,
                Unknown28 = unknown28Loading,
                Unknown32 = unknown32Loading,
                HideAchievementItems = hideachievementitemsLoading,
                Unknown34 = unknown34Loading,
                MinCompletedItems = mincompleteditemsLoading,
                TwoColumnLayout = twocolumnlayoutLoading,
                ShowItemCompletionsAsOne = showitemcompletionsasoneLoading,
                Unknown41 = unknown41Loading,
                SoftcoreOnly = softcoreonlyLoading,
                HardcoreOnly = hardcoreonlyLoading,
                Unknown51 = unknown51Loading,
                Unknown52 = unknown52Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
