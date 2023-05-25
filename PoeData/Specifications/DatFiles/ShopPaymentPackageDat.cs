// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ShopPaymentPackage.dat data.
/// </summary>
public sealed partial class ShopPaymentPackageDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets Coins.</summary>
    public required int Coins { get; init; }

    /// <summary> Gets a value indicating whether AvailableFlag is set.</summary>
    public required bool AvailableFlag { get; init; }

    /// <summary> Gets Unknown21.</summary>
    public required int Unknown21 { get; init; }

    /// <summary> Gets Unknown25.</summary>
    public required int Unknown25 { get; init; }

    /// <summary> Gets a value indicating whether Unknown29 is set.</summary>
    public required bool Unknown29 { get; init; }

    /// <summary> Gets a value indicating whether ContainsBetaKey is set.</summary>
    public required bool ContainsBetaKey { get; init; }

    /// <summary> Gets Unknown31.</summary>
    public required ReadOnlyCollection<int> Unknown31 { get; init; }

    /// <summary> Gets Unknown47.</summary>
    public required int? Unknown47 { get; init; }

    /// <summary> Gets BackgroundImage.</summary>
    public required string BackgroundImage { get; init; }

    /// <summary> Gets Unknown71.</summary>
    public required string Unknown71 { get; init; }

    /// <summary> Gets a value indicating whether Unknown79 is set.</summary>
    public required bool Unknown79 { get; init; }

    /// <summary> Gets Upgrade_ShopPaymentPackageKey.</summary>
    /// <remarks> references <see cref="ShopPaymentPackageDat"/> on <see cref="Specification.LoadShopPaymentPackageDat"/> index.</remarks>
    public required int? Upgrade_ShopPaymentPackageKey { get; init; }

    /// <summary> Gets PhysicalItemPoints.</summary>
    public required int PhysicalItemPoints { get; init; }

    /// <summary> Gets Unknown92.</summary>
    public required int Unknown92 { get; init; }

    /// <summary> Gets ShopPackagePlatform.</summary>
    /// <remarks> references <see cref="ShopPackagePlatformDat"/> on <see cref="Specification.LoadShopPackagePlatformDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ShopPackagePlatform { get; init; }

    /// <summary> Gets Unknown112.</summary>
    public required string Unknown112 { get; init; }
}
