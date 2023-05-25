// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PantheonPanelLayout.dat data.
/// </summary>
public sealed partial class PantheonPanelLayoutDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets X.</summary>
    public required int X { get; init; }

    /// <summary> Gets Y.</summary>
    public required int Y { get; init; }

    /// <summary> Gets a value indicating whether IsMajorGod is set.</summary>
    public required bool IsMajorGod { get; init; }

    /// <summary> Gets CoverImage.</summary>
    public required string CoverImage { get; init; }

    /// <summary> Gets GodName2.</summary>
    public required string GodName2 { get; init; }

    /// <summary> Gets SelectionImage.</summary>
    public required string SelectionImage { get; init; }

    /// <summary> Gets Effect1_StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Effect1_StatsKeys { get; init; }

    /// <summary> Gets Effect1_Values.</summary>
    public required ReadOnlyCollection<int> Effect1_Values { get; init; }

    /// <summary> Gets Effect2_StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Effect2_StatsKeys { get; init; }

    /// <summary> Gets GodName3.</summary>
    public required string GodName3 { get; init; }

    /// <summary> Gets Effect3_Values.</summary>
    public required ReadOnlyCollection<int> Effect3_Values { get; init; }

    /// <summary> Gets Effect3_StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Effect3_StatsKeys { get; init; }

    /// <summary> Gets GodName4.</summary>
    public required string GodName4 { get; init; }

    /// <summary> Gets Effect4_StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Effect4_StatsKeys { get; init; }

    /// <summary> Gets Effect4_Values.</summary>
    public required ReadOnlyCollection<int> Effect4_Values { get; init; }

    /// <summary> Gets GodName1.</summary>
    public required string GodName1 { get; init; }

    /// <summary> Gets Effect2_Values.</summary>
    public required ReadOnlyCollection<int> Effect2_Values { get; init; }

    /// <summary> Gets QuestState1.</summary>
    /// <remarks> references <see cref="QuestStatesDat"/> on <see cref="Specification.LoadQuestStatesDat"/> index.</remarks>
    public required int? QuestState1 { get; init; }

    /// <summary> Gets QuestState2.</summary>
    /// <remarks> references <see cref="QuestStatesDat"/> on <see cref="Specification.LoadQuestStatesDat"/> index.</remarks>
    public required int? QuestState2 { get; init; }

    /// <summary> Gets QuestState3.</summary>
    /// <remarks> references <see cref="QuestStatesDat"/> on <see cref="Specification.LoadQuestStatesDat"/> index.</remarks>
    public required int? QuestState3 { get; init; }

    /// <summary> Gets QuestState4.</summary>
    /// <remarks> references <see cref="QuestStatesDat"/> on <see cref="Specification.LoadQuestStatesDat"/> index.</remarks>
    public required int? QuestState4 { get; init; }

    /// <summary> Gets a value indicating whether IsDisabled is set.</summary>
    public required bool IsDisabled { get; init; }

    /// <summary> Gets AchievementItems.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItems { get; init; }
}
