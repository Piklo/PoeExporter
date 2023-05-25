// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AchievementSetRewards.dat data.
/// </summary>
public sealed partial class AchievementSetRewardsDat
{
    /// <summary> Gets SetId.</summary>
    /// <remarks> references <see cref="AchievementSetsDisplayDat"/> on <see cref="AchievementSetsDisplayDat.Id"/>.</remarks>
    public required int SetId { get; init; }

    /// <summary> Gets AchievementsRequired.</summary>
    public required int AchievementsRequired { get; init; }

    /// <summary> Gets Rewards.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Rewards { get; init; }

    /// <summary> Gets TotemPieceEveryNAchievements.</summary>
    public required int TotemPieceEveryNAchievements { get; init; }

    /// <summary> Gets Message.</summary>
    public required string Message { get; init; }

    /// <summary> Gets NotificationIcon.</summary>
    public required string NotificationIcon { get; init; }

    /// <summary> Gets HideoutName.</summary>
    public required string HideoutName { get; init; }

    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }
}
