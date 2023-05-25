// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BetrayalForts.dat data.
/// </summary>
public sealed partial class BetrayalFortsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required int Unknown12 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets ExtraTerrainFeaturesKey.</summary>
    /// <remarks> references <see cref="ExtraTerrainFeaturesDat"/> on <see cref="Specification.LoadExtraTerrainFeaturesDat"/> index.</remarks>
    public required int? ExtraTerrainFeaturesKey { get; init; }
}
