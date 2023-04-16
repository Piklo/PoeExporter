// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ShopCurrency.dat data.
/// </summary>
public sealed partial class ShopCurrencyDat
{
    /// <summary> Gets CurrencyCode.</summary>
    public required string CurrencyCode { get; init; }

    /// <summary> Gets CurrencySign.</summary>
    public required string CurrencySign { get; init; }

    /// <summary>
    /// Gets ShopCurrencyDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ShopCurrencyDat.</returns>
    internal static ShopCurrencyDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ShopCurrency.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShopCurrencyDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading CurrencyCode
            (var currencycodeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CurrencySign
            (var currencysignLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShopCurrencyDat()
            {
                CurrencyCode = currencycodeLoading,
                CurrencySign = currencysignLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
