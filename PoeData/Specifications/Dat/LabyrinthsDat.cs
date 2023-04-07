// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Labyrinths.dat data.
/// </summary>
public sealed partial class LabyrinthsDat
{
    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets OfferingItem.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? OfferingItem { get; init; }

    /// <summary> Gets QuestFlag.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.GetQuestFlagsDat"/> index.</remarks>
    public required int? QuestFlag { get; init; }

    /// <summary> Gets RequiredTrials.</summary>
    /// <remarks> references <see cref="LabyrinthTrialsDat"/> on <see cref="Specification.GetLabyrinthTrialsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> RequiredTrials { get; init; }

    /// <summary> Gets AreaLevel.</summary>
    public required int AreaLevel { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets JewelReward.</summary>
    /// <remarks> references <see cref="WordsDat"/> on <see cref="Specification.GetWordsDat"/> index.</remarks>
    public required int? JewelReward { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required ReadOnlyCollection<int> Unknown84 { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required ReadOnlyCollection<int> Unknown100 { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets Unknown120.</summary>
    public required int Unknown120 { get; init; }

    /// <summary> Gets Unknown124.</summary>
    public required int Unknown124 { get; init; }

    /// <summary> Gets CraftingFontDescription.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.GetClientStringsDat"/> index.</remarks>
    public required int? CraftingFontDescription { get; init; }

    /// <inheritdoc/>
    public static LabyrinthsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/Labyrinths.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading OfferingItem
            (var offeringitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading QuestFlag
            (var questflagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading RequiredTrials
            (var temprequiredtrialsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var requiredtrialsLoading = temprequiredtrialsLoading.AsReadOnly();

            // loading AreaLevel
            (var arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading JewelReward
            (var jewelrewardLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown84
            (var tempunknown84Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown84Loading = tempunknown84Loading.AsReadOnly();

            // loading Unknown100
            (var tempunknown100Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown100Loading = tempunknown100Loading.AsReadOnly();

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown120
            (var unknown120Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown124
            (var unknown124Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CraftingFontDescription
            (var craftingfontdescriptionLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthsDat()
            {
                Tier = tierLoading,
                Name = nameLoading,
                OfferingItem = offeringitemLoading,
                QuestFlag = questflagLoading,
                RequiredTrials = requiredtrialsLoading,
                AreaLevel = arealevelLoading,
                Unknown64 = unknown64Loading,
                JewelReward = jewelrewardLoading,
                Unknown84 = unknown84Loading,
                Unknown100 = unknown100Loading,
                MinLevel = minlevelLoading,
                Unknown120 = unknown120Loading,
                Unknown124 = unknown124Loading,
                CraftingFontDescription = craftingfontdescriptionLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
