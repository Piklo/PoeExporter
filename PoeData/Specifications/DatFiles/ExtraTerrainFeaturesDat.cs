// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ExtraTerrainFeatures.dat data.
/// </summary>
public sealed partial class ExtraTerrainFeaturesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ArmFiles.</summary>
    public required ReadOnlyCollection<string> ArmFiles { get; init; }

    /// <summary> Gets TdtFiles.</summary>
    public required ReadOnlyCollection<string> TdtFiles { get; init; }

    /// <summary> Gets a value indicating whether Unknown40 is set.</summary>
    public required bool Unknown40 { get; init; }

    /// <summary> Gets Unknown41.</summary>
    public required ReadOnlyCollection<string> Unknown41 { get; init; }

    /// <summary> Gets Unknown57.</summary>
    public required ReadOnlyCollection<int> Unknown57 { get; init; }

    /// <summary> Gets Unknown73.</summary>
    /// <remarks> references <see cref="ExtraTerrainFeaturesDat"/> on <see cref="Specification.LoadExtraTerrainFeaturesDat"/> index.</remarks>
    public required int? Unknown73 { get; init; }

    /// <summary> Gets WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown97 is set.</summary>
    public required bool Unknown97 { get; init; }

    /// <summary> Gets Unknown98.</summary>
    public required int Unknown98 { get; init; }
}
