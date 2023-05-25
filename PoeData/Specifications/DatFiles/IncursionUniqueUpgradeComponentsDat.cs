// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing IncursionUniqueUpgradeComponents.dat data.
/// </summary>
public sealed partial class IncursionUniqueUpgradeComponentsDat
{
    /// <summary> Gets BaseUnique.</summary>
    /// <remarks> references <see cref="WordsDat"/> on <see cref="Specification.LoadWordsDat"/> index.</remarks>
    public required int? BaseUnique { get; init; }

    /// <summary> Gets UpgradeCurrency.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? UpgradeCurrency { get; init; }
}
