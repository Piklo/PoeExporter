// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MapPurchaseCosts.dat data.
/// </summary>
public sealed partial class MapPurchaseCostsDat
{
    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets Cost.</summary>
    /// <remarks> references <see cref="ItemCostsDat"/> on <see cref="Specification.LoadItemCostsDat"/> index.</remarks>
    public required int? Cost { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int? Unknown20 { get; init; }
}
