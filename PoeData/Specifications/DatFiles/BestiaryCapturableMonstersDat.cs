// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BestiaryCapturableMonsters.dat data.
/// </summary>
public sealed partial class BestiaryCapturableMonstersDat
{
    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets BestiaryGroupsKey.</summary>
    /// <remarks> references <see cref="BestiaryGroupsDat"/> on <see cref="Specification.LoadBestiaryGroupsDat"/> index.</remarks>
    public required int? BestiaryGroupsKey { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets BestiaryEncountersKey.</summary>
    /// <remarks> references <see cref="BestiaryEncountersDat"/> on <see cref="Specification.LoadBestiaryEncountersDat"/> index.</remarks>
    public required int? BestiaryEncountersKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown56 is set.</summary>
    public required bool Unknown56 { get; init; }

    /// <summary> Gets IconSmall.</summary>
    public required string IconSmall { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary> Gets Boss_MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? Boss_MonsterVarietiesKey { get; init; }

    /// <summary> Gets BestiaryGenusKey.</summary>
    /// <remarks> references <see cref="BestiaryGenusDat"/> on <see cref="Specification.LoadBestiaryGenusDat"/> index.</remarks>
    public required int? BestiaryGenusKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown105 is set.</summary>
    public required bool Unknown105 { get; init; }

    /// <summary> Gets BestiaryCapturableMonstersKey.</summary>
    /// <remarks> references <see cref="BestiaryCapturableMonstersDat"/> on <see cref="Specification.LoadBestiaryCapturableMonstersDat"/> index.</remarks>
    public required int? BestiaryCapturableMonstersKey { get; init; }

    /// <summary> Gets a value indicating whether IsDisabled is set.</summary>
    public required bool IsDisabled { get; init; }

    /// <summary> Gets Unknown115.</summary>
    public required int Unknown115 { get; init; }

    /// <summary> Gets a value indicating whether Unknown119 is set.</summary>
    public required bool Unknown119 { get; init; }
}
