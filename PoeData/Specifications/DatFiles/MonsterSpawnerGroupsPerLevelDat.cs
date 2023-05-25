// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MonsterSpawnerGroupsPerLevel.dat data.
/// </summary>
public sealed partial class MonsterSpawnerGroupsPerLevelDat
{
    /// <summary> Gets MonsterSpawnerGroupsKey.</summary>
    /// <remarks> references <see cref="MonsterSpawnerGroupsDat"/> on <see cref="Specification.LoadMonsterSpawnerGroupsDat"/> index.</remarks>
    public required int? MonsterSpawnerGroupsKey { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int Unknown20 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }
}
