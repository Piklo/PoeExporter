// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LegionFactions.dat data.
/// </summary>
public sealed partial class LegionFactionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets LegionBalancePerLevelKey.</summary>
    /// <remarks> references <see cref="LegionBalancePerLevelDat"/> on <see cref="Specification.LoadLegionBalancePerLevelDat"/> index.</remarks>
    public required int? LegionBalancePerLevelKey { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required float Unknown28 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required float Unknown32 { get; init; }

    /// <summary> Gets BuffVisualsKey.</summary>
    /// <remarks> references <see cref="BuffVisualsDat"/> on <see cref="Specification.LoadBuffVisualsDat"/> index.</remarks>
    public required int? BuffVisualsKey { get; init; }

    /// <summary> Gets MiscAnimatedKey1.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimatedKey1 { get; init; }

    /// <summary> Gets MiscAnimatedKey2.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimatedKey2 { get; init; }

    /// <summary> Gets MiscAnimatedKey3.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimatedKey3 { get; init; }

    /// <summary> Gets AchievementItemsKeys1.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKeys1 { get; init; }

    /// <summary> Gets MiscAnimatedKey4.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimatedKey4 { get; init; }

    /// <summary> Gets MiscAnimatedKey5.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimatedKey5 { get; init; }

    /// <summary> Gets Unknown148.</summary>
    public required float Unknown148 { get; init; }

    /// <summary> Gets Unknown152.</summary>
    public required float Unknown152 { get; init; }

    /// <summary> Gets AchievementItemsKeys2.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKeys2 { get; init; }

    /// <summary> Gets StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? StatsKey { get; init; }

    /// <summary> Gets Shard.</summary>
    public required string Shard { get; init; }

    /// <summary> Gets RewardJewelArt.</summary>
    public required string RewardJewelArt { get; init; }
}
