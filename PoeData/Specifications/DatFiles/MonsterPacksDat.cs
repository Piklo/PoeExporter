// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MonsterPacks.dat data.
/// </summary>
public sealed partial class MonsterPacksDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets WorldAreasKeys.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> WorldAreasKeys { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets BossMonsterSpawnChance.</summary>
    public required int BossMonsterSpawnChance { get; init; }

    /// <summary> Gets BossMonsterCount.</summary>
    public required int BossMonsterCount { get; init; }

    /// <summary> Gets BossMonster_MonsterVarietiesKeys.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BossMonster_MonsterVarietiesKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown56 is set.</summary>
    public required bool Unknown56 { get; init; }

    /// <summary> Gets Unknown57.</summary>
    public required int Unknown57 { get; init; }

    /// <summary> Gets Unknown61.</summary>
    public required ReadOnlyCollection<string> Unknown61 { get; init; }

    /// <summary> Gets TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TagsKeys { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets WorldAreas2.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> WorldAreas2 { get; init; }

    /// <summary> Gets Unknown117.</summary>
    public required int Unknown117 { get; init; }

    /// <summary> Gets PackFormation.</summary>
    /// <remarks> references <see cref="PackFormationDat"/> on <see cref="Specification.LoadPackFormationDat"/> index.</remarks>
    public required int? PackFormation { get; init; }

    /// <summary> Gets Unknown137.</summary>
    public required int Unknown137 { get; init; }

    /// <summary> Gets a value indicating whether Unknown141 is set.</summary>
    public required bool Unknown141 { get; init; }
}
