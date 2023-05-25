// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BlightTowersPerLevel.dat data.
/// </summary>
public sealed partial class BlightTowersPerLevelDat
{
    /// <summary> Gets BlightTowersKey.</summary>
    /// <remarks> references <see cref="BlightTowersDat"/> on <see cref="Specification.LoadBlightTowersDat"/> index.</remarks>
    public required int? BlightTowersKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets Cost.</summary>
    public required int Cost { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }
}
