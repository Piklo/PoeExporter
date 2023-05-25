// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MetamorphosisScaling.dat data.
/// </summary>
public sealed partial class MetamorphosisScalingDat
{
    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets StatValueMultiplier.</summary>
    public required float StatValueMultiplier { get; init; }

    /// <summary> Gets ScalingStats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ScalingStats { get; init; }

    /// <summary> Gets ScalingValues.</summary>
    public required ReadOnlyCollection<int> ScalingValues { get; init; }

    /// <summary> Gets ScalingValues2.</summary>
    public required ReadOnlyCollection<int> ScalingValues2 { get; init; }

    /// <summary> Gets ScalingValuesHardmode.</summary>
    public required ReadOnlyCollection<int> ScalingValuesHardmode { get; init; }

    /// <summary> Gets ScalingValuesHardmode2.</summary>
    public required ReadOnlyCollection<int> ScalingValuesHardmode2 { get; init; }
}
