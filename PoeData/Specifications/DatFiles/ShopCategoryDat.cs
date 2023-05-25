// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ShopCategory.dat data.
/// </summary>
public sealed partial class ShopCategoryDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ClientText.</summary>
    public required string ClientText { get; init; }

    /// <summary> Gets ClientJPGFile.</summary>
    public required string ClientJPGFile { get; init; }

    /// <summary> Gets WebsiteText.</summary>
    public required string WebsiteText { get; init; }

    /// <summary> Gets WebsiteJPGFile.</summary>
    public required string WebsiteJPGFile { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets AppliedTo_BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? AppliedTo_BaseItemTypesKey { get; init; }
}
