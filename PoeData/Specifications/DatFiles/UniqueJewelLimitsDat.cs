// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing UniqueJewelLimits.dat data.
/// </summary>
public sealed partial class UniqueJewelLimitsDat
{
    /// <summary> Gets JewelName.</summary>
    /// <remarks> references <see cref="WordsDat"/> on <see cref="Specification.LoadWordsDat"/> index.</remarks>
    public required int? JewelName { get; init; }

    /// <summary> Gets Limit.</summary>
    public required int Limit { get; init; }
}
