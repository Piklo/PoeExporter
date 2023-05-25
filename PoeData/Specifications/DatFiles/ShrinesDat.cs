// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Shrines.dat data.
/// </summary>
public sealed partial class ShrinesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets TimeoutInSeconds.</summary>
    public required int TimeoutInSeconds { get; init; }

    /// <summary> Gets a value indicating whether ChargesShared is set.</summary>
    public required bool ChargesShared { get; init; }

    /// <summary> Gets Player_ShrineBuffsKey.</summary>
    /// <remarks> references <see cref="ShrineBuffsDat"/> on <see cref="Specification.LoadShrineBuffsDat"/> index.</remarks>
    public required int? Player_ShrineBuffsKey { get; init; }

    /// <summary> Gets Unknown29.</summary>
    public required int Unknown29 { get; init; }

    /// <summary> Gets Unknown33.</summary>
    public required int Unknown33 { get; init; }

    /// <summary> Gets Monster_ShrineBuffsKey.</summary>
    /// <remarks> references <see cref="ShrineBuffsDat"/> on <see cref="Specification.LoadShrineBuffsDat"/> index.</remarks>
    public required int? Monster_ShrineBuffsKey { get; init; }

    /// <summary> Gets SummonMonster_MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? SummonMonster_MonsterVarietiesKey { get; init; }

    /// <summary> Gets SummonPlayer_MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? SummonPlayer_MonsterVarietiesKey { get; init; }

    /// <summary> Gets Unknown85.</summary>
    public required int Unknown85 { get; init; }

    /// <summary> Gets Unknown89.</summary>
    public required int Unknown89 { get; init; }

    /// <summary> Gets ShrineSoundsKey.</summary>
    /// <remarks> references <see cref="ShrineSoundsDat"/> on <see cref="Specification.LoadShrineSoundsDat"/> index.</remarks>
    public required int? ShrineSoundsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown109 is set.</summary>
    public required bool Unknown109 { get; init; }

    /// <summary> Gets AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKeys { get; init; }

    /// <summary> Gets a value indicating whether IsPVPOnly is set.</summary>
    public required bool IsPVPOnly { get; init; }

    /// <summary> Gets a value indicating whether Unknown127 is set.</summary>
    public required bool Unknown127 { get; init; }

    /// <summary> Gets a value indicating whether IsLesserShrine is set.</summary>
    public required bool IsLesserShrine { get; init; }

    /// <summary> Gets Description.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required int? Description { get; init; }

    /// <summary> Gets Name.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required int? Name { get; init; }

    /// <summary> Gets a value indicating whether Unknown161 is set.</summary>
    public required bool Unknown161 { get; init; }
}
