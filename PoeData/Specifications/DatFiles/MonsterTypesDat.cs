// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MonsterTypes.dat data.
/// </summary>
public sealed partial class MonsterTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether IsSummoned is set.</summary>
    public required bool IsSummoned { get; init; }

    /// <summary> Gets Armour.</summary>
    public required int Armour { get; init; }

    /// <summary> Gets Evasion.</summary>
    public required int Evasion { get; init; }

    /// <summary> Gets EnergyShieldFromLife.</summary>
    public required int EnergyShieldFromLife { get; init; }

    /// <summary> Gets DamageSpread.</summary>
    public required int DamageSpread { get; init; }

    /// <summary> Gets MonsterResistancesKey.</summary>
    /// <remarks> references <see cref="MonsterResistancesDat"/> on <see cref="Specification.LoadMonsterResistancesDat"/> index.</remarks>
    public required int? MonsterResistancesKey { get; init; }

    /// <summary> Gets a value indicating whether IsLargeAbyssMonster is set.</summary>
    public required bool IsLargeAbyssMonster { get; init; }

    /// <summary> Gets a value indicating whether IsSmallAbyssMonster is set.</summary>
    public required bool IsSmallAbyssMonster { get; init; }

    /// <summary> Gets a value indicating whether Unknown47 is set.</summary>
    public required bool Unknown47 { get; init; }
}
