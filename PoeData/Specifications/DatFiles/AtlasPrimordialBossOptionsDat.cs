// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AtlasPrimordialBossOptions.dat data.
/// </summary>
public sealed partial class AtlasPrimordialBossOptionsDat
{
    /// <summary> Gets Unknown0.</summary>
    public required int Unknown0 { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required int Unknown4 { get; init; }

    /// <summary> Gets DefaultIcon.</summary>
    public required string DefaultIcon { get; init; }

    /// <summary> Gets HoverIcon.</summary>
    public required string HoverIcon { get; init; }

    /// <summary> Gets HighlightIcon.</summary>
    public required string HighlightIcon { get; init; }

    /// <summary> Gets EmptyIcon.</summary>
    public required string EmptyIcon { get; init; }

    /// <summary> Gets Description.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required int? Description { get; init; }

    /// <summary> Gets DescriptionActive.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required int? DescriptionActive { get; init; }

    /// <summary> Gets ProgressTracker.</summary>
    public required string ProgressTracker { get; init; }

    /// <summary> Gets ProgressTrackerFill.</summary>
    public required string ProgressTrackerFill { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets MapDeviceTrackerFill.</summary>
    public required string MapDeviceTrackerFill { get; init; }
}
