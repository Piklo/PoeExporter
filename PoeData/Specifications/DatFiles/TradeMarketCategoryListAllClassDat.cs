// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing TradeMarketCategoryListAllClass.dat data.
/// </summary>
public sealed partial class TradeMarketCategoryListAllClassDat
{
    /// <summary> Gets TradeCategory.</summary>
    /// <remarks> references <see cref="TradeMarketCategoryDat"/> on <see cref="Specification.GetTradeMarketCategoryDat"/> index.</remarks>
    public required int? TradeCategory { get; init; }

    /// <summary> Gets ItemClass.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.GetItemClassesDat"/> index.</remarks>
    public required int? ItemClass { get; init; }

    /// <summary>
    /// Gets TradeMarketCategoryListAllClassDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of TradeMarketCategoryListAllClassDat.</returns>
    internal static TradeMarketCategoryListAllClassDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/TradeMarketCategoryListAllClass.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TradeMarketCategoryListAllClassDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading TradeCategory
            (var tradecategoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ItemClass
            (var itemclassLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TradeMarketCategoryListAllClassDat()
            {
                TradeCategory = tradecategoryLoading,
                ItemClass = itemclassLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
