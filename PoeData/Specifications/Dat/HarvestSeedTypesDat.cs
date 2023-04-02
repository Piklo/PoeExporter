// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing HarvestSeedTypes.dat data.
/// </summary>
public sealed partial class HarvestSeedTypesDat : ISpecificationFile<HarvestSeedTypesDat>
{
    /// <summary> Gets HarvestObjectsKey.</summary>
    public required int? HarvestObjectsKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int? Unknown16 { get; init; }

    /// <summary> Gets GrowthCycles.</summary>
    public required int GrowthCycles { get; init; }

    /// <summary> Gets AOFiles.</summary>
    public required ReadOnlyCollection<string> AOFiles { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required ReadOnlyCollection<int> Unknown52 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int Unknown68 { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets RequiredNearbySeed_Tier.</summary>
    public required int RequiredNearbySeed_Tier { get; init; }

    /// <summary> Gets RequiredNearbySeed_Amount.</summary>
    public required int RequiredNearbySeed_Amount { get; init; }

    /// <summary> Gets WildLifeforceConsumedPercentage.</summary>
    public required int WildLifeforceConsumedPercentage { get; init; }

    /// <summary> Gets VividLifeforceConsumedPercentage.</summary>
    public required int VividLifeforceConsumedPercentage { get; init; }

    /// <summary> Gets PrimalLifeforceConsumedPercentage.</summary>
    public required int PrimalLifeforceConsumedPercentage { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets HarvestCraftOptionsKeys.</summary>
    public required ReadOnlyCollection<int> HarvestCraftOptionsKeys { get; init; }

    /// <summary> Gets Unknown120.</summary>
    public required int Unknown120 { get; init; }

    /// <summary> Gets Unknown124.</summary>
    public required ReadOnlyCollection<int> Unknown124 { get; init; }

    /// <summary> Gets AchievementItemsKeys.</summary>
    public required ReadOnlyCollection<int> AchievementItemsKeys { get; init; }

    /// <summary> Gets OutcomeType.</summary>
    public required int OutcomeType { get; init; }

    /// <inheritdoc/>
    public static HarvestSeedTypesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/HarvestSeedTypes.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestSeedTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetHarvestObjectsDat();
            // specification.GetHarvestCraftOptionsDat();
            // specification.GetAchievementItemsDat();

            // loading HarvestObjectsKey
            (var harvestobjectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading GrowthCycles
            (var growthcyclesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AOFiles
            (var tempaofilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var aofilesLoading = tempaofilesLoading.AsReadOnly();

            // loading Unknown52
            (var tempunknown52Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown52Loading = tempunknown52Loading.AsReadOnly();

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RequiredNearbySeed_Tier
            (var requirednearbyseed_tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RequiredNearbySeed_Amount
            (var requirednearbyseed_amountLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading WildLifeforceConsumedPercentage
            (var wildlifeforceconsumedpercentageLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading VividLifeforceConsumedPercentage
            (var vividlifeforceconsumedpercentageLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PrimalLifeforceConsumedPercentage
            (var primallifeforceconsumedpercentageLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HarvestCraftOptionsKeys
            (var tempharvestcraftoptionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var harvestcraftoptionskeysLoading = tempharvestcraftoptionskeysLoading.AsReadOnly();

            // loading Unknown120
            (var unknown120Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown124
            (var tempunknown124Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown124Loading = tempunknown124Loading.AsReadOnly();

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading OutcomeType
            (var outcometypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestSeedTypesDat()
            {
                HarvestObjectsKey = harvestobjectskeyLoading,
                Unknown16 = unknown16Loading,
                GrowthCycles = growthcyclesLoading,
                AOFiles = aofilesLoading,
                Unknown52 = unknown52Loading,
                Unknown68 = unknown68Loading,
                Tier = tierLoading,
                RequiredNearbySeed_Tier = requirednearbyseed_tierLoading,
                RequiredNearbySeed_Amount = requirednearbyseed_amountLoading,
                WildLifeforceConsumedPercentage = wildlifeforceconsumedpercentageLoading,
                VividLifeforceConsumedPercentage = vividlifeforceconsumedpercentageLoading,
                PrimalLifeforceConsumedPercentage = primallifeforceconsumedpercentageLoading,
                Text = textLoading,
                HarvestCraftOptionsKeys = harvestcraftoptionskeysLoading,
                Unknown120 = unknown120Loading,
                Unknown124 = unknown124Loading,
                AchievementItemsKeys = achievementitemskeysLoading,
                OutcomeType = outcometypeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
