// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing TradeMarketIndexItemAs.dat data.
/// </summary>
public sealed partial class TradeMarketIndexItemAsDat
{
    /// <summary> Gets Item.</summary>
    public required int? Item { get; init; }

    /// <summary> Gets IndexAs.</summary>
    public required int? IndexAs { get; init; }

    /// <summary>
    /// Gets TradeMarketIndexItemAsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of TradeMarketIndexItemAsDat.</returns>
    internal static TradeMarketIndexItemAsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/TradeMarketIndexItemAs.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TradeMarketIndexItemAsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Item
            (var itemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading IndexAs
            (var indexasLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TradeMarketIndexItemAsDat()
            {
                Item = itemLoading,
                IndexAs = indexasLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
