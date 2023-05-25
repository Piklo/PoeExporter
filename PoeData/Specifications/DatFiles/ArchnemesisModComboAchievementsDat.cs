// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ArchnemesisModComboAchievements.dat data.
/// </summary>
public sealed partial class ArchnemesisModComboAchievementsDat
{
    /// <summary> Gets Achievement.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? Achievement { get; init; }

    /// <summary> Gets Mods.</summary>
    /// <remarks> references <see cref="ArchnemesisModsDat"/> on <see cref="Specification.LoadArchnemesisModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Mods { get; init; }
}
