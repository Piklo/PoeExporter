// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing NearbyMonsterConditions.dat data.
/// </summary>
public sealed partial class NearbyMonsterConditionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MonsterVarietiesKeys.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MonsterVarietiesKeys { get; init; }

    /// <summary> Gets MonsterAmount.</summary>
    public required int MonsterAmount { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets a value indicating whether IsNegated is set.</summary>
    public required bool IsNegated { get; init; }

    /// <summary> Gets Unknown33.</summary>
    public required int Unknown33 { get; init; }

    /// <summary> Gets Unknown37.</summary>
    public required ReadOnlyCollection<int> Unknown37 { get; init; }

    /// <summary> Gets a value indicating whether IsLessThen is set.</summary>
    public required bool IsLessThen { get; init; }

    /// <summary> Gets MinimumHealthPercentage.</summary>
    public required int MinimumHealthPercentage { get; init; }
}
