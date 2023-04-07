// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing UltimatumItemisedRewards.dat data.
/// </summary>
public sealed partial class UltimatumItemisedRewardsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets RewardText.</summary>
    public required string RewardText { get; init; }

    /// <summary> Gets ItemVisualIdentityKey.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.GetItemVisualIdentityDat"/> index.</remarks>
    public required int? ItemVisualIdentityKey { get; init; }

    /// <summary> Gets RewardType.</summary>
    public required int RewardType { get; init; }

    /// <summary> Gets SacrificeItem.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? SacrificeItem { get; init; }

    /// <summary> Gets SacrificeAmount.</summary>
    public required int SacrificeAmount { get; init; }

    /// <summary> Gets SacrificeText.</summary>
    public required string SacrificeText { get; init; }

    /// <summary> Gets a value indicating whether Unknown68 is set.</summary>
    public required bool Unknown68 { get; init; }

    /// <summary> Gets TrialMods.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TrialMods { get; init; }

    /// <inheritdoc/>
    public static UltimatumItemisedRewardsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/UltimatumItemisedRewards.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UltimatumItemisedRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RewardText
            (var rewardtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ItemVisualIdentityKey
            (var itemvisualidentitykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading RewardType
            (var rewardtypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SacrificeItem
            (var sacrificeitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading SacrificeAmount
            (var sacrificeamountLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SacrificeText
            (var sacrificetextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading TrialMods
            (var temptrialmodsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var trialmodsLoading = temptrialmodsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UltimatumItemisedRewardsDat()
            {
                Id = idLoading,
                HASH16 = hash16Loading,
                RewardText = rewardtextLoading,
                ItemVisualIdentityKey = itemvisualidentitykeyLoading,
                RewardType = rewardtypeLoading,
                SacrificeItem = sacrificeitemLoading,
                SacrificeAmount = sacrificeamountLoading,
                SacrificeText = sacrificetextLoading,
                Unknown68 = unknown68Loading,
                TrialMods = trialmodsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
