// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing UITalkText.dat data.
/// </summary>
public sealed partial class UITalkTextDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets UITalkCategoriesKey.</summary>
    /// <remarks> references <see cref="UITalkCategoriesDat"/> on <see cref="Specification.LoadUITalkCategoriesDat"/> index.</remarks>
    public required int UITalkCategoriesKey { get; init; }

    /// <summary> Gets OGGFile.</summary>
    public required string OGGFile { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets a value indicating whether Unknown28 is set.</summary>
    public required bool Unknown28 { get; init; }

    /// <summary> Gets NPCTextAudio.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? NPCTextAudio { get; init; }

    /// <summary>
    /// Gets UITalkTextDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of UITalkTextDat.</returns>
    internal static UITalkTextDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/UITalkText.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

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
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
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
