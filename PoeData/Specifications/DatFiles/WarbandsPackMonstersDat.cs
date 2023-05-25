// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing WarbandsPackMonsters.dat data.
/// </summary>
public sealed partial class WarbandsPackMonstersDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether Unknown12 is set.</summary>
    public required bool Unknown12 { get; init; }

    /// <summary> Gets a value indicating whether Unknown13 is set.</summary>
    public required bool Unknown13 { get; init; }

    /// <summary> Gets a value indicating whether Unknown14 is set.</summary>
    public required bool Unknown14 { get; init; }

    /// <summary> Gets a value indicating whether Unknown15 is set.</summary>
    public required bool Unknown15 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Tier4_MonsterVarietiesKeys.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Tier4_MonsterVarietiesKeys { get; init; }

    /// <summary> Gets Tier3_MonsterVarietiesKeys.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Tier3_MonsterVarietiesKeys { get; init; }

    /// <summary> Gets Tier2_MonsterVarietiesKeys.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Tier2_MonsterVarietiesKeys { get; init; }

    /// <summary> Gets Tier1_MonsterVarietiesKeys.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Tier1_MonsterVarietiesKeys { get; init; }

    /// <summary> Gets Tier1Name.</summary>
    public required string Tier1Name { get; init; }

    /// <summary> Gets Tier2Name.</summary>
    public required string Tier2Name { get; init; }

    /// <summary> Gets Tier3Name.</summary>
    public required string Tier3Name { get; init; }

    /// <summary> Gets Tier4Name.</summary>
    public required string Tier4Name { get; init; }

    /// <summary> Gets Tier1Art.</summary>
    public required string Tier1Art { get; init; }

    /// <summary> Gets Tier2Art.</summary>
    public required string Tier2Art { get; init; }

    /// <summary> Gets Tier3Art.</summary>
    public required string Tier3Art { get; init; }

    /// <summary> Gets Tier4Art.</summary>
    public required string Tier4Art { get; init; }
}
