// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing NPCConversations.dat data.
/// </summary>
public sealed partial class NPCConversationsDat : IDat<NPCConversationsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets DialogueEvent.</summary>
    /// <remarks> references <see cref="DialogueEventDat"/> on <see cref="Specification.GetDialogueEventDat"/> index.</remarks>
    public required int? DialogueEvent { get; init; }

    /// <summary> Gets NPCTextAudioKeys.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.GetNPCTextAudioDat"/> index.</remarks>
    public required ReadOnlyCollection<int> NPCTextAudioKeys { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required ReadOnlyCollection<int> Unknown40 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <inheritdoc/>
    public static NPCConversationsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/NPCConversations.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCConversationsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DialogueEvent
            (var dialogueeventLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading NPCTextAudioKeys
            (var tempnpctextaudiokeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var npctextaudiokeysLoading = tempnpctextaudiokeysLoading.AsReadOnly();

            // loading Unknown40
            (var tempunknown40Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown40Loading = tempunknown40Loading.AsReadOnly();

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCConversationsDat()
            {
                Id = idLoading,
                DialogueEvent = dialogueeventLoading,
                NPCTextAudioKeys = npctextaudiokeysLoading,
                Unknown40 = unknown40Loading,
                Unknown56 = unknown56Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
