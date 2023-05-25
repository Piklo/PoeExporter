// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BetrayalTargets.dat data.
/// </summary>
public sealed partial class BetrayalTargetsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BetrayalRanksKey.</summary>
    /// <remarks> references <see cref="BetrayalRanksDat"/> on <see cref="Specification.LoadBetrayalRanksDat"/> index.</remarks>
    public required int? BetrayalRanksKey { get; init; }

    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets BetrayalJobsKey.</summary>
    /// <remarks> references <see cref="BetrayalJobsDat"/> on <see cref="Specification.LoadBetrayalJobsDat"/> index.</remarks>
    public required int? BetrayalJobsKey { get; init; }

    /// <summary> Gets Art.</summary>
    public required string Art { get; init; }

    /// <summary> Gets a value indicating whether Unknown64 is set.</summary>
    public required bool Unknown64 { get; init; }

    /// <summary> Gets ItemClasses.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.LoadItemClassesDat"/> index.</remarks>
    public required int? ItemClasses { get; init; }

    /// <summary> Gets FullName.</summary>
    public required string FullName { get; init; }

    /// <summary> Gets Safehouse_ARMFile.</summary>
    public required string Safehouse_ARMFile { get; init; }

    /// <summary> Gets ShortName.</summary>
    public required string ShortName { get; init; }

    /// <summary> Gets Unknown105.</summary>
    public required int Unknown105 { get; init; }

    /// <summary> Gets SafehouseLeader_AcheivementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? SafehouseLeader_AcheivementItemsKey { get; init; }

    /// <summary> Gets Level3_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? Level3_AchievementItemsKey { get; init; }

    /// <summary> Gets Unknown141.</summary>
    public required int Unknown141 { get; init; }

    /// <summary> Gets Unknown145.</summary>
    public required int Unknown145 { get; init; }

    /// <summary> Gets Unknown149.</summary>
    public required int Unknown149 { get; init; }

    /// <summary> Gets Unknown153.</summary>
    public required int? Unknown153 { get; init; }

    /// <summary> Gets ScriptArgument.</summary>
    public required string ScriptArgument { get; init; }
}
