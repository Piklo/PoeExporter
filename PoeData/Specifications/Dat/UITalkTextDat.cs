// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing UITalkText.dat data.
/// </summary>
public sealed partial class UITalkTextDat : ISpecificationFile<UITalkTextDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets UITalkCategoriesKey.</summary>
    /// <remarks> references <see cref="UITalkCategoriesDat"/> on <see cref="Specification.GetUITalkCategoriesDat"/> index.</remarks>
    public required int UITalkCategoriesKey { get; init; }

    /// <summary> Gets OGGFile.</summary>
    public required string OGGFile { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets a value indicating whether Unknown28 is set.</summary>
    public required bool Unknown28 { get; init; }

    /// <summary> Gets NPCTextAudio.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.GetNPCTextAudioDat"/> index.</remarks>
    public required int? NPCTextAudio { get; init; }

    /// <inheritdoc/>
    public static UITalkTextDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/UITalkText.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UITalkTextDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading UITalkCategoriesKey
            (var uitalkcategorieskeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading OGGFile
            (var oggfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading NPCTextAudio
            (var npctextaudioLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UITalkTextDat()
            {
                Id = idLoading,
                UITalkCategoriesKey = uitalkcategorieskeyLoading,
                OGGFile = oggfileLoading,
                Text = textLoading,
                Unknown28 = unknown28Loading,
                NPCTextAudio = npctextaudioLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
