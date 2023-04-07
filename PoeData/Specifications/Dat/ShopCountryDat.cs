// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing ShopCountry.dat data.
/// </summary>
public sealed partial class ShopCountryDat : IDat<ShopCountryDat>
{
    /// <summary> Gets CountryTwoLetterCode.</summary>
    public required string CountryTwoLetterCode { get; init; }

    /// <summary> Gets Country.</summary>
    public required string Country { get; init; }

    /// <summary> Gets ShopCurrencyKey.</summary>
    /// <remarks> references <see cref="ShopCurrencyDat"/> on <see cref="Specification.GetShopCurrencyDat"/> index.</remarks>
    public required int? ShopCurrencyKey { get; init; }

    /// <inheritdoc/>
    public static ShopCountryDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/ShopCountry.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShopCountryDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading CountryTwoLetterCode
            (var countrytwolettercodeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Country
            (var countryLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ShopCurrencyKey
            (var shopcurrencykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShopCountryDat()
            {
                CountryTwoLetterCode = countrytwolettercodeLoading,
                Country = countryLoading,
                ShopCurrencyKey = shopcurrencykeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
