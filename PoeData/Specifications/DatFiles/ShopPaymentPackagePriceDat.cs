// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ShopPaymentPackagePrice.dat data.
/// </summary>
public sealed partial class ShopPaymentPackagePriceDat
{
    /// <summary> Gets ShopPaymentPackageKey.</summary>
    /// <remarks> references <see cref="ShopPaymentPackageDat"/> on <see cref="Specification.LoadShopPaymentPackageDat"/> index.</remarks>
    public required int? ShopPaymentPackageKey { get; init; }

    /// <summary> Gets ShopCountryKey.</summary>
    /// <remarks> references <see cref="ShopCountryDat"/> on <see cref="Specification.LoadShopCountryDat"/> index.</remarks>
    public required int? ShopCountryKey { get; init; }

    /// <summary> Gets Price.</summary>
    public required int Price { get; init; }
}
