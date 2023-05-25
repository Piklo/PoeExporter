// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HarvestPlantBoosters.dat data.
/// </summary>
public sealed partial class HarvestPlantBoostersDat
{
    /// <summary> Gets HarvestObjectsKey.</summary>
    /// <remarks> references <see cref="HarvestObjectsDat"/> on <see cref="Specification.LoadHarvestObjectsDat"/> index.</remarks>
    public required int? HarvestObjectsKey { get; init; }

    /// <summary> Gets Radius.</summary>
    public required int Radius { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int? Unknown20 { get; init; }

    /// <summary> Gets Lifeforce.</summary>
    public required int Lifeforce { get; init; }

    /// <summary> Gets AdditionalCraftingOptionsChance.</summary>
    public required int AdditionalCraftingOptionsChance { get; init; }

    /// <summary> Gets RareExtraChances.</summary>
    public required int RareExtraChances { get; init; }

    /// <summary> Gets HarvestPlantBoosterFamilies.</summary>
    public required int HarvestPlantBoosterFamilies { get; init; }
}
