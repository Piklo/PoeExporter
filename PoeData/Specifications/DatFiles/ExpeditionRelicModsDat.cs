// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ExpeditionRelicMods.dat data.
/// </summary>
public sealed partial class ExpeditionRelicModsDat
{
    /// <summary> Gets Mod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? Mod { get; init; }

    /// <summary> Gets Categories.</summary>
    /// <remarks> references <see cref="ExpeditionRelicModCategoriesDat"/> on <see cref="Specification.LoadExpeditionRelicModCategoriesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Categories { get; init; }

    /// <summary> Gets DestroyAchievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> DestroyAchievements { get; init; }
}
