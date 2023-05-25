// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistAreas.dat data.
/// </summary>
public sealed partial class HeistAreasDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets WorldAreasKeys.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> WorldAreasKeys { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets EnvironmentsKey1.</summary>
    /// <remarks> references <see cref="EnvironmentsDat"/> on <see cref="Specification.LoadEnvironmentsDat"/> index.</remarks>
    public required int? EnvironmentsKey1 { get; init; }

    /// <summary> Gets EnvironmentsKey2.</summary>
    /// <remarks> references <see cref="EnvironmentsDat"/> on <see cref="Specification.LoadEnvironmentsDat"/> index.</remarks>
    public required int? EnvironmentsKey2 { get; init; }

    /// <summary> Gets HeistJobsKeys.</summary>
    /// <remarks> references <see cref="HeistJobsDat"/> on <see cref="Specification.LoadHeistJobsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> HeistJobsKeys { get; init; }

    /// <summary> Gets Contract_BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? Contract_BaseItemTypesKey { get; init; }

    /// <summary> Gets Blueprint_BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? Blueprint_BaseItemTypesKey { get; init; }

    /// <summary> Gets DGRFile.</summary>
    public required string DGRFile { get; init; }

    /// <summary> Gets Unknown116.</summary>
    public required int Unknown116 { get; init; }

    /// <summary> Gets Unknown120.</summary>
    public required int Unknown120 { get; init; }

    /// <summary> Gets a value indicating whether Unknown124 is set.</summary>
    public required bool Unknown124 { get; init; }

    /// <summary> Gets a value indicating whether Unknown125 is set.</summary>
    public required bool Unknown125 { get; init; }

    /// <summary> Gets Blueprint_DDSFile.</summary>
    public required string Blueprint_DDSFile { get; init; }

    /// <summary> Gets Achievements1.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievements1 { get; init; }

    /// <summary> Gets Achievements2.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievements2 { get; init; }

    /// <summary> Gets Reward.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required int? Reward { get; init; }

    /// <summary> Gets Achievements3.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievements3 { get; init; }

    /// <summary> Gets RewardHardmode.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required int? RewardHardmode { get; init; }
}
