// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BetrayalTraitorRewards.dat data.
/// </summary>
public sealed partial class BetrayalTraitorRewardsDat
{
    /// <summary> Gets BetrayalJobsKey.</summary>
    /// <remarks> references <see cref="BetrayalJobsDat"/> on <see cref="Specification.LoadBetrayalJobsDat"/> index.</remarks>
    public required int? BetrayalJobsKey { get; init; }

    /// <summary> Gets BetrayalTargetsKey.</summary>
    /// <remarks> references <see cref="BetrayalTargetsDat"/> on <see cref="Specification.LoadBetrayalTargetsDat"/> index.</remarks>
    public required int? BetrayalTargetsKey { get; init; }

    /// <summary> Gets BetrayalRanksKey.</summary>
    /// <remarks> references <see cref="BetrayalRanksDat"/> on <see cref="Specification.LoadBetrayalRanksDat"/> index.</remarks>
    public required int? BetrayalRanksKey { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }
}
