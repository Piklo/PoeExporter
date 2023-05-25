// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ShopCountry.dat data.
/// </summary>
public sealed partial class ShopCountryDat
{
    /// <summary> Gets CountryTwoLetterCode.</summary>
    public required string CountryTwoLetterCode { get; init; }

    /// <summary> Gets Country.</summary>
    public required string Country { get; init; }

    /// <summary> Gets ShopCurrencyKey.</summary>
    /// <remarks> references <see cref="ShopCurrencyDat"/> on <see cref="Specification.LoadShopCurrencyDat"/> index.</remarks>
    public required int? ShopCurrencyKey { get; init; }
}
