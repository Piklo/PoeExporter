// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LegionChestCounts.dat data.
/// </summary>
public sealed partial class LegionChestCountsDat
{
    /// <summary> Gets LegionFactionsKey.</summary>
    /// <remarks> references <see cref="LegionFactionsDat"/> on <see cref="Specification.LoadLegionFactionsDat"/> index.</remarks>
    public required int? LegionFactionsKey { get; init; }

    /// <summary> Gets LegionRanksKey.</summary>
    /// <remarks> references <see cref="LegionRanksDat"/> on <see cref="Specification.LoadLegionRanksDat"/> index.</remarks>
    public required int? LegionRanksKey { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int Unknown52 { get; init; }
}
