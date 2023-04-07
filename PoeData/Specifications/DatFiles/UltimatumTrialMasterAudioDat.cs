// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing UltimatumTrialMasterAudio.dat data.
/// </summary>
public sealed partial class UltimatumTrialMasterAudioDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Variant.</summary>
    public required int Variant { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required int Unknown12 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets TextAudio.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.GetNPCTextAudioDat"/> index.</remarks>
    public required int? TextAudio { get; init; }

    /// <summary> Gets RoundsMin.</summary>
    public required int RoundsMin { get; init; }

    /// <summary> Gets RoundsMax.</summary>
    public required int RoundsMax { get; init; }

    /// <summary>
    /// Gets UltimatumTrialMasterAudioDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of UltimatumTrialMasterAudioDat.</returns>
    internal static UltimatumTrialMasterAudioDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/UltimatumTrialMasterAudio.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UltimatumTrialMasterAudioDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Variant
            (var variantLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TextAudio
            (var textaudioLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading RoundsMin
            (var roundsminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RoundsMax
            (var roundsmaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UltimatumTrialMasterAudioDat()
            {
                Id = idLoading,
                Variant = variantLoading,
                Unknown12 = unknown12Loading,
                Unknown16 = unknown16Loading,
                TextAudio = textaudioLoading,
                RoundsMin = roundsminLoading,
                RoundsMax = roundsmaxLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
