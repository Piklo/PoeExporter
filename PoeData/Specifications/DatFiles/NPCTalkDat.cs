// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing NPCTalk.dat data.
/// </summary>
public sealed partial class NPCTalkDat
{
    /// <summary> Gets NPCKey.</summary>
    /// <remarks> references <see cref="NPCsDat"/> on <see cref="Specification.GetNPCsDat"/> index.</remarks>
    public required int? NPCKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets DialogueOption.</summary>
    public required string DialogueOption { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required ReadOnlyCollection<int> Unknown28 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required ReadOnlyCollection<int> Unknown44 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required ReadOnlyCollection<int> Unknown60 { get; init; }

    /// <summary> Gets Script.</summary>
    public required string Script { get; init; }

    /// <summary> Gets TextAudio.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.GetNPCTextAudioDat"/> index.</remarks>
    public required int? TextAudio { get; init; }

    /// <summary> Gets Category.</summary>
    /// <remarks> references <see cref="NPCTalkCategoryDat"/> on <see cref="Specification.GetNPCTalkCategoryDat"/> index.</remarks>
    public required int? Category { get; init; }

    /// <summary> Gets QuestRewardOffersKey.</summary>
    /// <remarks> references <see cref="QuestRewardOffersDat"/> on <see cref="Specification.GetQuestRewardOffersDat"/> index.</remarks>
    public required int? QuestRewardOffersKey { get; init; }

    /// <summary> Gets QuestFlag.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.GetQuestFlagsDat"/> index.</remarks>
    public required int? QuestFlag { get; init; }

    /// <summary> Gets NPCTextAudioKeys.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.GetNPCTextAudioDat"/> index.</remarks>
    public required ReadOnlyCollection<int> NPCTextAudioKeys { get; init; }

    /// <summary> Gets Script2.</summary>
    public required string Script2 { get; init; }

    /// <summary> Gets a value indicating whether Unknown172 is set.</summary>
    public required bool Unknown172 { get; init; }

    /// <summary> Gets a value indicating whether Unknown173 is set.</summary>
    public required bool Unknown173 { get; init; }

    /// <summary> Gets Unknown174.</summary>
    public required ReadOnlyCollection<int> Unknown174 { get; init; }

    /// <summary> Gets Unknown190.</summary>
    public required ReadOnlyCollection<int> Unknown190 { get; init; }

    /// <summary> Gets Unknown206.</summary>
    public required int Unknown206 { get; init; }

    /// <summary> Gets Unknown210.</summary>
    public required ReadOnlyCollection<int> Unknown210 { get; init; }

    /// <summary> Gets Unknown226.</summary>
    public required int Unknown226 { get; init; }

    /// <summary> Gets a value indicating whether Unknown230 is set.</summary>
    public required bool Unknown230 { get; init; }

    /// <summary> Gets Unknown231.</summary>
    public required int? Unknown231 { get; init; }

    /// <summary> Gets a value indicating whether Unknown247 is set.</summary>
    public required bool Unknown247 { get; init; }

    /// <summary> Gets a value indicating whether Unknown248 is set.</summary>
    public required bool Unknown248 { get; init; }

    /// <summary> Gets DialogueOption2.</summary>
    public required string DialogueOption2 { get; init; }

    /// <summary> Gets Unknown257.</summary>
    public required int? Unknown257 { get; init; }

    /// <summary> Gets Unknown273.</summary>
    public required int? Unknown273 { get; init; }

    /// <summary> Gets Unknown289.</summary>
    public required int Unknown289 { get; init; }

    /// <summary> Gets Unknown293.</summary>
    public required ReadOnlyCollection<int> Unknown293 { get; init; }

    /// <summary> Gets Unknown309.</summary>
    public required int Unknown309 { get; init; }

    /// <summary>
    /// Gets NPCTalkDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of NPCTalkDat.</returns>
    internal static NPCTalkDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/NPCTalk.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCTalkDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading NPCKey
            (var npckeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DialogueOption
            (var dialogueoptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown28
            (var tempunknown28Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown28Loading = tempunknown28Loading.AsReadOnly();

            // loading Unknown44
            (var tempunknown44Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown44Loading = tempunknown44Loading.AsReadOnly();

            // loading Unknown60
            (var tempunknown60Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown60Loading = tempunknown60Loading.AsReadOnly();

            // loading Script
            (var scriptLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TextAudio
            (var textaudioLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Category
            (var categoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading QuestRewardOffersKey
            (var questrewardofferskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading QuestFlag
            (var questflagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading NPCTextAudioKeys
            (var tempnpctextaudiokeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var npctextaudiokeysLoading = tempnpctextaudiokeysLoading.AsReadOnly();

            // loading Script2
            (var script2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown172
            (var unknown172Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown173
            (var unknown173Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown174
            (var tempunknown174Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown174Loading = tempunknown174Loading.AsReadOnly();

            // loading Unknown190
            (var tempunknown190Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown190Loading = tempunknown190Loading.AsReadOnly();

            // loading Unknown206
            (var unknown206Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown210
            (var tempunknown210Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown210Loading = tempunknown210Loading.AsReadOnly();

            // loading Unknown226
            (var unknown226Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown230
            (var unknown230Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown231
            (var unknown231Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown247
            (var unknown247Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown248
            (var unknown248Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading DialogueOption2
            (var dialogueoption2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown257
            (var unknown257Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown273
            (var unknown273Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown289
            (var unknown289Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown293
            (var tempunknown293Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown293Loading = tempunknown293Loading.AsReadOnly();

            // loading Unknown309
            (var unknown309Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCTalkDat()
            {
                NPCKey = npckeyLoading,
                Unknown16 = unknown16Loading,
                DialogueOption = dialogueoptionLoading,
                Unknown28 = unknown28Loading,
                Unknown44 = unknown44Loading,
                Unknown60 = unknown60Loading,
                Script = scriptLoading,
                TextAudio = textaudioLoading,
                Category = categoryLoading,
                QuestRewardOffersKey = questrewardofferskeyLoading,
                QuestFlag = questflagLoading,
                NPCTextAudioKeys = npctextaudiokeysLoading,
                Script2 = script2Loading,
                Unknown172 = unknown172Loading,
                Unknown173 = unknown173Loading,
                Unknown174 = unknown174Loading,
                Unknown190 = unknown190Loading,
                Unknown206 = unknown206Loading,
                Unknown210 = unknown210Loading,
                Unknown226 = unknown226Loading,
                Unknown230 = unknown230Loading,
                Unknown231 = unknown231Loading,
                Unknown247 = unknown247Loading,
                Unknown248 = unknown248Loading,
                DialogueOption2 = dialogueoption2Loading,
                Unknown257 = unknown257Loading,
                Unknown273 = unknown273Loading,
                Unknown289 = unknown289Loading,
                Unknown293 = unknown293Loading,
                Unknown309 = unknown309Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
