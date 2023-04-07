// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing ClientStrings.dat data.
/// </summary>
public sealed partial class ClientStringsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets XBoxText.</summary>
    public required string XBoxText { get; init; }

    /// <summary> Gets XBoxText2.</summary>
    public required string XBoxText2 { get; init; }

    /// <summary> Gets HASH32.</summary>
    public required int HASH32 { get; init; }

    /// <summary> Gets PlaystationText.</summary>
    public required string PlaystationText { get; init; }

    /// <inheritdoc/>
    public static ClientStringsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/ClientStrings.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ClientStringsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading XBoxText
            (var xboxtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading XBoxText2
            (var xboxtext2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HASH32
            (var hash32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PlaystationText
            (var playstationtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ClientStringsDat()
            {
                Id = idLoading,
                Text = textLoading,
                XBoxText = xboxtextLoading,
                XBoxText2 = xboxtext2Loading,
                HASH32 = hash32Loading,
                PlaystationText = playstationtextLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
