// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SupporterPackSets.dat data.
/// </summary>
public sealed partial class SupporterPackSetsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets FormatTitle.</summary>
    public required string FormatTitle { get; init; }

    /// <summary> Gets Background.</summary>
    public required string Background { get; init; }

    /// <summary> Gets Time0.</summary>
    public required string Time0 { get; init; }

    /// <summary> Gets Time1.</summary>
    public required string Time1 { get; init; }

    /// <summary> Gets ShopPackagePlatform.</summary>
    /// <remarks> references <see cref="ShopPackagePlatformDat"/> on <see cref="Specification.LoadShopPackagePlatformDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ShopPackagePlatform { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required string Unknown56 { get; init; }
}
