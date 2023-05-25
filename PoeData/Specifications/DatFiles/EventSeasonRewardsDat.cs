// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing EventSeasonRewards.dat data.
/// </summary>
public sealed partial class EventSeasonRewardsDat
{
    /// <summary> Gets EventSeasonKey.</summary>
    /// <remarks> references <see cref="EventSeasonDat"/> on <see cref="Specification.LoadEventSeasonDat"/> index.</remarks>
    public required int? EventSeasonKey { get; init; }

    /// <summary> Gets Point.</summary>
    public required int Point { get; init; }

    /// <summary> Gets RewardCommand.</summary>
    public required string RewardCommand { get; init; }
}
