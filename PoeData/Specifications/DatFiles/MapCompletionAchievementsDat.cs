// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MapCompletionAchievements.dat data.
/// </summary>
public sealed partial class MapCompletionAchievementsDat
{
    /// <summary> Gets Unknown0.</summary>
    public required string Unknown0 { get; init; }

    /// <summary> Gets MapStatConditionsKeys.</summary>
    /// <remarks> references <see cref="MapStatConditionsDat"/> on <see cref="Specification.LoadMapStatConditionsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MapStatConditionsKeys { get; init; }

    /// <summary> Gets StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys { get; init; }

    /// <summary> Gets AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKeys { get; init; }

    /// <summary> Gets MapTierAchievementsKeys.</summary>
    /// <remarks> references <see cref="MapTierAchievementsDat"/> on <see cref="Specification.LoadMapTierAchievementsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MapTierAchievementsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown72 is set.</summary>
    public required bool Unknown72 { get; init; }

    /// <summary> Gets WorldAreasKeys.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> WorldAreasKeys { get; init; }
}
