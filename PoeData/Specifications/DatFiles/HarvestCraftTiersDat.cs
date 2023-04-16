// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HarvestCraftTiers.dat data.
/// </summary>
public sealed partial class HarvestCraftTiersDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets FrameImage.</summary>
    public required string FrameImage { get; init; }

    /// <summary> Gets FrameHighlight.</summary>
    public required string FrameHighlight { get; init; }

    /// <summary>
    /// Gets HarvestCraftTiersDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HarvestCraftTiersDat.</returns>
    internal static HarvestCraftTiersDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HarvestCraftTiers.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestCraftTiersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FrameImage
            (var frameimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FrameHighlight
            (var framehighlightLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestCraftTiersDat()
            {
                Id = idLoading,
                FrameImage = frameimageLoading,
                FrameHighlight = framehighlightLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
