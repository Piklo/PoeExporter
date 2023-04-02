// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing HeistNPCDialogue.dat data.
/// </summary>
public sealed partial class HeistNPCDialogueDat : ISpecificationFile<HeistNPCDialogueDat>
{
    /// <summary> Gets DialogueEventKey.</summary>
    public required int? DialogueEventKey { get; init; }

    /// <summary> Gets HeistNPCsKey.</summary>
    public required int? HeistNPCsKey { get; init; }

    /// <summary> Gets AudioNormal.</summary>
    public required ReadOnlyCollection<int> AudioNormal { get; init; }

    /// <summary> Gets AudioLoud.</summary>
    public required ReadOnlyCollection<int> AudioLoud { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <inheritdoc/>
    public static HeistNPCDialogueDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/HeistNPCDialogue.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistNPCDialogueDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetDialogueEventDat();
            // specification.GetHeistNPCsDat();
            // specification.GetNPCTextAudioDat();

            // loading DialogueEventKey
            (var dialogueeventkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HeistNPCsKey
            (var heistnpcskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading AudioNormal
            (var tempaudionormalLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var audionormalLoading = tempaudionormalLoading.AsReadOnly();

            // loading AudioLoud
            (var tempaudioloudLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var audioloudLoading = tempaudioloudLoading.AsReadOnly();

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistNPCDialogueDat()
            {
                DialogueEventKey = dialogueeventkeyLoading,
                HeistNPCsKey = heistnpcskeyLoading,
                AudioNormal = audionormalLoading,
                AudioLoud = audioloudLoading,
                Unknown64 = unknown64Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
