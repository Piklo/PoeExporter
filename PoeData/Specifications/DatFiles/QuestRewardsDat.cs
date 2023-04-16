// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing QuestRewards.dat data.
/// </summary>
public sealed partial class QuestRewardsDat
{
    /// <summary> Gets RewardOffer.</summary>
    /// <remarks> references <see cref="QuestRewardOffersDat"/> on <see cref="Specification.GetQuestRewardOffersDat"/> index.</remarks>
    public required int? RewardOffer { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Characters.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.GetCharactersDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Characters { get; init; }

    /// <summary> Gets Reward.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? Reward { get; init; }

    /// <summary> Gets RewardLevel.</summary>
    public required int RewardLevel { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int? Unknown56 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required string Unknown76 { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required ReadOnlyCollection<int> Unknown84 { get; init; }

    /// <summary> Gets RewardStack.</summary>
    public required int RewardStack { get; init; }

    /// <summary> Gets a value indicating whether Unknown104 is set.</summary>
    public required bool Unknown104 { get; init; }

    /// <summary> Gets a value indicating whether Unknown105 is set.</summary>
    public required bool Unknown105 { get; init; }

    /// <summary> Gets Unknown106.</summary>
    public required ReadOnlyCollection<int> Unknown106 { get; init; }

    /// <summary> Gets Unknown122.</summary>
    public required int Unknown122 { get; init; }

    /// <summary> Gets Unknown126.</summary>
    public required int Unknown126 { get; init; }

    /// <summary> Gets Unknown130.</summary>
    public required ReadOnlyCollection<int> Unknown130 { get; init; }

    /// <summary> Gets Unknown146.</summary>
    public required int Unknown146 { get; init; }

    /// <summary> Gets Unknown150.</summary>
    public required int? Unknown150 { get; init; }

    /// <summary> Gets Unknown166.</summary>
    public required int Unknown166 { get; init; }

    /// <summary>
    /// Gets QuestRewardsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of QuestRewardsDat.</returns>
    internal static QuestRewardsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/QuestRewards.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new QuestRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading RewardOffer
            (var rewardofferLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Characters
            (var tempcharactersLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var charactersLoading = tempcharactersLoading.AsReadOnly();

            // loading Reward
            (var rewardLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading RewardLevel
            (var rewardlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown84
            (var tempunknown84Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown84Loading = tempunknown84Loading.AsReadOnly();

            // loading RewardStack
            (var rewardstackLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown105
            (var unknown105Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown106
            (var tempunknown106Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown106Loading = tempunknown106Loading.AsReadOnly();

            // loading Unknown122
            (var unknown122Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown126
            (var unknown126Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown130
            (var tempunknown130Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown130Loading = tempunknown130Loading.AsReadOnly();

            // loading Unknown146
            (var unknown146Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown150
            (var unknown150Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown166
            (var unknown166Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new QuestRewardsDat()
            {
                RewardOffer = rewardofferLoading,
                Unknown16 = unknown16Loading,
                Characters = charactersLoading,
                Reward = rewardLoading,
                RewardLevel = rewardlevelLoading,
                Unknown56 = unknown56Loading,
                Unknown72 = unknown72Loading,
                Unknown76 = unknown76Loading,
                Unknown84 = unknown84Loading,
                RewardStack = rewardstackLoading,
                Unknown104 = unknown104Loading,
                Unknown105 = unknown105Loading,
                Unknown106 = unknown106Loading,
                Unknown122 = unknown122Loading,
                Unknown126 = unknown126Loading,
                Unknown130 = unknown130Loading,
                Unknown146 = unknown146Loading,
                Unknown150 = unknown150Loading,
                Unknown166 = unknown166Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
