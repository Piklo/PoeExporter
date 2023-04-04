// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing SynthesisFragmentDialogue.dat data.
/// </summary>
public sealed partial class SynthesisFragmentDialogueDat : ISpecificationFile<SynthesisFragmentDialogueDat>
{
    /// <summary> Gets Unknown0.</summary>
    public required int? Unknown0 { get; init; }

    /// <summary> Gets NPCTextAudioKey1.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.GetNPCTextAudioDat"/> index.</remarks>
    public required int? NPCTextAudioKey1 { get; init; }

    /// <summary> Gets NPCTextAudioKey2.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.GetNPCTextAudioDat"/> index.</remarks>
    public required int? NPCTextAudioKey2 { get; init; }

    /// <summary> Gets NPCTextAudioKey3.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.GetNPCTextAudioDat"/> index.</remarks>
    public required int? NPCTextAudioKey3 { get; init; }

    /// <summary> Gets NPCTextAudioKey4.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.GetNPCTextAudioDat"/> index.</remarks>
    public required int? NPCTextAudioKey4 { get; init; }

    /// <summary> Gets NPCTextAudioKey5.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.GetNPCTextAudioDat"/> index.</remarks>
    public required int? NPCTextAudioKey5 { get; init; }

    /// <summary> Gets NPCTextAudioKey6.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.GetNPCTextAudioDat"/> index.</remarks>
    public required int? NPCTextAudioKey6 { get; init; }

    /// <inheritdoc/>
    public static SynthesisFragmentDialogueDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/SynthesisFragmentDialogue.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SynthesisFragmentDialogueDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading NPCTextAudioKey1
            (var npctextaudiokey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading NPCTextAudioKey2
            (var npctextaudiokey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading NPCTextAudioKey3
            (var npctextaudiokey3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading NPCTextAudioKey4
            (var npctextaudiokey4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading NPCTextAudioKey5
            (var npctextaudiokey5Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading NPCTextAudioKey6
            (var npctextaudiokey6Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SynthesisFragmentDialogueDat()
            {
                Unknown0 = unknown0Loading,
                NPCTextAudioKey1 = npctextaudiokey1Loading,
                NPCTextAudioKey2 = npctextaudiokey2Loading,
                NPCTextAudioKey3 = npctextaudiokey3Loading,
                NPCTextAudioKey4 = npctextaudiokey4Loading,
                NPCTextAudioKey5 = npctextaudiokey5Loading,
                NPCTextAudioKey6 = npctextaudiokey6Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
