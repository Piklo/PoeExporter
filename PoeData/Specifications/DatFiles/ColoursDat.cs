// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Colours.dat data.
/// </summary>
public sealed partial class ColoursDat
{
    /// <summary> Gets Item.</summary>
    public required string Item { get; init; }

    /// <summary> Gets Red.</summary>
    public required int Red { get; init; }

    /// <summary> Gets Green.</summary>
    public required int Green { get; init; }

    /// <summary> Gets Blue.</summary>
    public required int Blue { get; init; }

    /// <summary> Gets RgbCode.</summary>
    public required string RgbCode { get; init; }

    /// <summary>
    /// Gets ColoursDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ColoursDat.</returns>
    internal static ColoursDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Colours.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ColoursDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Item
            (var itemLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Red
            (var redLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Green
            (var greenLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Blue
            (var blueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RgbCode
            (var rgbcodeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ColoursDat()
            {
                Item = itemLoading,
                Red = redLoading,
                Green = greenLoading,
                Blue = blueLoading,
                RgbCode = rgbcodeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
