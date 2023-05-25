// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MicrotransactionSocialFrameVariations.dat data.
/// </summary>
public sealed partial class MicrotransactionSocialFrameVariationsDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets Id.</summary>
    public required int Id { get; init; }

    /// <summary> Gets BK2File.</summary>
    public required string BK2File { get; init; }
}
