// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ArchnemesisMetaRewards.dat data.
/// </summary>
public sealed partial class ArchnemesisMetaRewardsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets RewardText.</summary>
    public required string RewardText { get; init; }

    /// <summary> Gets RewardGroup.</summary>
    public required int RewardGroup { get; init; }

    /// <summary> Gets ScriptArgument.</summary>
    public required string ScriptArgument { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }
}
