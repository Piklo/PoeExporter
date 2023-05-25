// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing OnKillAchievements.dat data.
/// </summary>
public sealed partial class OnKillAchievementsDat
{
    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets GameStat.</summary>
    /// <remarks> references <see cref="GameStatsDat"/> on <see cref="Specification.LoadGameStatsDat"/> index.</remarks>
    public required int? GameStat { get; init; }
}
