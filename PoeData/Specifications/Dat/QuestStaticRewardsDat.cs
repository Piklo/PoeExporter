// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing QuestStaticRewards.dat data.
/// </summary>
public sealed partial class QuestStaticRewardsDat
{
    /// <summary> Gets QuestFlag.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.GetQuestFlagsDat"/> index.</remarks>
    public required int? QuestFlag { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys { get; init; }

    /// <summary> Gets StatValues.</summary>
    public required ReadOnlyCollection<int> StatValues { get; init; }

    /// <summary> Gets QuestKey.</summary>
    /// <remarks> references <see cref="QuestDat"/> on <see cref="Specification.GetQuestDat"/> index.</remarks>
    public required int? QuestKey { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int Unknown68 { get; init; }

    /// <summary> Gets ClientStringsKey.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.GetClientStringsDat"/> index.</remarks>
    public required int? ClientStringsKey { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int Unknown88 { get; init; }

    /// <summary> Gets StatValuesHardmode.</summary>
    public required ReadOnlyCollection<int> StatValuesHardmode { get; init; }

    /// <summary> Gets ClientStringHardmode.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.GetClientStringsDat"/> index.</remarks>
    public required int? ClientStringHardmode { get; init; }

    /// <summary> Gets Unknown124.</summary>
    public required int Unknown124 { get; init; }

    /// <inheritdoc/>
    public static QuestStaticRewardsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/QuestStaticRewards.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new QuestStaticRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading QuestFlag
            (var questflagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading StatValues
            (var tempstatvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statvaluesLoading = tempstatvaluesLoading.AsReadOnly();

            // loading QuestKey
            (var questkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ClientStringsKey
            (var clientstringskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatValuesHardmode
            (var tempstatvalueshardmodeLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statvalueshardmodeLoading = tempstatvalueshardmodeLoading.AsReadOnly();

            // loading ClientStringHardmode
            (var clientstringhardmodeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown124
            (var unknown124Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new QuestStaticRewardsDat()
            {
                QuestFlag = questflagLoading,
                Unknown16 = unknown16Loading,
                StatsKeys = statskeysLoading,
                StatValues = statvaluesLoading,
                QuestKey = questkeyLoading,
                Unknown68 = unknown68Loading,
                ClientStringsKey = clientstringskeyLoading,
                Unknown88 = unknown88Loading,
                StatValuesHardmode = statvalueshardmodeLoading,
                ClientStringHardmode = clientstringhardmodeLoading,
                Unknown124 = unknown124Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
