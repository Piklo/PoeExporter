﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing QuestRewardOffers.dat data.
/// </summary>
public sealed partial class QuestRewardOffersDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets QuestKey.</summary>
    /// <remarks> references <see cref="QuestDat"/> on <see cref="Specification.LoadQuestDat"/> index.</remarks>
    public required int? QuestKey { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets RewardWindowTake.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required int? RewardWindowTake { get; init; }

    /// <summary> Gets a value indicating whether Unknown60 is set.</summary>
    public required bool Unknown60 { get; init; }

    /// <summary> Gets a value indicating whether Unknown61 is set.</summary>
    public required bool Unknown61 { get; init; }

    /// <summary> Gets RewardWindowTitle.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required int? RewardWindowTitle { get; init; }

    /// <summary>
    /// Gets QuestRewardOffersDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of QuestRewardOffersDat.</returns>
    internal static QuestRewardOffersDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/QuestRewardOffers.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new QuestRewardOffersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading QuestKey
            (var questkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RewardWindowTake
            (var rewardwindowtakeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading RewardWindowTitle
            (var rewardwindowtitleLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new QuestRewardOffersDat()
            {
                Id = idLoading,
                QuestKey = questkeyLoading,
                Unknown24 = unknown24Loading,
                Unknown40 = unknown40Loading,
                RewardWindowTake = rewardwindowtakeLoading,
                Unknown60 = unknown60Loading,
                Unknown61 = unknown61Loading,
                RewardWindowTitle = rewardwindowtitleLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
