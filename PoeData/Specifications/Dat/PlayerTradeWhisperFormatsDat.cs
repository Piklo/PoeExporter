// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing PlayerTradeWhisperFormats.dat data.
/// </summary>
public sealed partial class PlayerTradeWhisperFormatsDat : ISpecificationFile<PlayerTradeWhisperFormatsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Whisper.</summary>
    public required string Whisper { get; init; }

    /// <summary> Gets a value indicating whether InStash is set.</summary>
    public required bool InStash { get; init; }

    /// <summary> Gets a value indicating whether IsPriced is set.</summary>
    public required bool IsPriced { get; init; }

    /// <inheritdoc/>
    public static PlayerTradeWhisperFormatsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/PlayerTradeWhisperFormats.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PlayerTradeWhisperFormatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Whisper
            (var whisperLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading InStash
            (var instashLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsPriced
            (var ispricedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PlayerTradeWhisperFormatsDat()
            {
                Id = idLoading,
                Whisper = whisperLoading,
                InStash = instashLoading,
                IsPriced = ispricedLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
