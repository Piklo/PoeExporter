// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing GrantedEffectStatSets.dat data.
/// </summary>
public sealed partial class GrantedEffectStatSetsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ImplicitStats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ImplicitStats { get; init; }

    /// <summary> Gets ConstantStats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ConstantStats { get; init; }

    /// <summary> Gets ConstantStatsValues.</summary>
    public required ReadOnlyCollection<int> ConstantStatsValues { get; init; }

    /// <summary> Gets BaseEffectiveness.</summary>
    public required float BaseEffectiveness { get; init; }

    /// <summary> Gets IncrementalEffectiveness.</summary>
    public required float IncrementalEffectiveness { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }
}
