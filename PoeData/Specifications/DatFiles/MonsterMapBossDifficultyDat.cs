// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MonsterMapBossDifficulty.dat data.
/// </summary>
public sealed partial class MonsterMapBossDifficultyDat
{
    /// <summary> Gets MapLevel.</summary>
    public required int MapLevel { get; init; }

    /// <summary> Gets Stat1Value.</summary>
    public required int Stat1Value { get; init; }

    /// <summary> Gets Stat2Value.</summary>
    public required int Stat2Value { get; init; }

    /// <summary> Gets StatsKey1.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? StatsKey1 { get; init; }

    /// <summary> Gets StatsKey2.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? StatsKey2 { get; init; }

    /// <summary> Gets StatsKey3.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? StatsKey3 { get; init; }

    /// <summary> Gets Stat3Value.</summary>
    public required int Stat3Value { get; init; }

    /// <summary> Gets StatsKey4.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? StatsKey4 { get; init; }

    /// <summary> Gets Stat4Value.</summary>
    public required int Stat4Value { get; init; }

    /// <summary> Gets StatsKey5.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? StatsKey5 { get; init; }

    /// <summary> Gets Stat5Value.</summary>
    public required int Stat5Value { get; init; }
}
