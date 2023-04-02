// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing ShopPaymentPackagePrice.dat data.
/// </summary>
public sealed partial class ShopPaymentPackagePriceDat : ISpecificationFile<ShopPaymentPackagePriceDat>
{
    /// <summary> Gets ShopPaymentPackageKey.</summary>
    public required int? ShopPaymentPackageKey { get; init; }

    /// <summary> Gets ShopCountryKey.</summary>
    public required int? ShopCountryKey { get; init; }

    /// <summary> Gets Price.</summary>
    public required int Price { get; init; }

    /// <inheritdoc/>
    public static ShopPaymentPackagePriceDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/ShopPaymentPackagePrice.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShopPaymentPackagePriceDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetShopPaymentPackageDat();
            // specification.GetShopCountryDat();

            // loading ShopPaymentPackageKey
            (var shoppaymentpackagekeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ShopCountryKey
            (var shopcountrykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Price
            (var priceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShopPaymentPackagePriceDat()
            {
                ShopPaymentPackageKey = shoppaymentpackagekeyLoading,
                ShopCountryKey = shopcountrykeyLoading,
                Price = priceLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
