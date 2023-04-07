// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PassiveTreeExpansionJewelSizes.dat data.
/// </summary>
public sealed partial class PassiveTreeExpansionJewelSizesDat
{
    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets PassiveTreeExpansionJewelSizesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of PassiveTreeExpansionJewelSizesDat.</returns>
    internal static PassiveTreeExpansionJewelSizesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/PassiveTreeExpansionJewelSizes.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveTreeExpansionJewelSizesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveTreeExpansionJewelSizesDat()
            {
                Name = nameLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
