// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistJobs.dat data.
/// </summary>
public sealed partial class HeistJobsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets RequiredSkillIcon.</summary>
    public required string RequiredSkillIcon { get; init; }

    /// <summary> Gets SkillIcon.</summary>
    public required string SkillIcon { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required float Unknown32 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets MapIcon.</summary>
    public required string MapIcon { get; init; }

    /// <summary> Gets Level_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Level_StatsKey { get; init; }

    /// <summary> Gets Alert_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Alert_StatsKey { get; init; }

    /// <summary> Gets Alarm_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Alarm_StatsKey { get; init; }

    /// <summary> Gets Cost_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Cost_StatsKey { get; init; }

    /// <summary> Gets ExperienceGain_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? ExperienceGain_StatsKey { get; init; }

    /// <summary> Gets ConsoleBlueprintLegend.</summary>
    public required string ConsoleBlueprintLegend { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }
}
