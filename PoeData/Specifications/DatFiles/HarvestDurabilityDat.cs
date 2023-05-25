// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HarvestDurability.dat data.
/// </summary>
public sealed partial class HarvestDurabilityDat
{
    /// <summary> Gets HarvestObjectsKey.</summary>
    /// <remarks> references <see cref="HarvestObjectsDat"/> on <see cref="Specification.LoadHarvestObjectsDat"/> index.</remarks>
    public required int? HarvestObjectsKey { get; init; }

    /// <summary> Gets Durability.</summary>
    public required int Durability { get; init; }
}
