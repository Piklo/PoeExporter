// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HellscapeMods.dat data.
/// </summary>
public sealed partial class HellscapeModsDat
{
    /// <summary> Gets Mod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? Mod { get; init; }

    /// <summary> Gets TiersWhitelist.</summary>
    public required ReadOnlyCollection<int> TiersWhitelist { get; init; }

    /// <summary> Gets TransformAchievement.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? TransformAchievement { get; init; }

    /// <summary> Gets ModFamilies.</summary>
    /// <remarks> references <see cref="ModFamilyDat"/> on <see cref="Specification.LoadModFamilyDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModFamilies { get; init; }
}
