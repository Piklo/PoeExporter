// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SkillTotemVariations.dat data.
/// </summary>
public sealed partial class SkillTotemVariationsDat
{
    /// <summary> Gets SkillTotemsKey.</summary>
    /// <remarks> references <see cref="SkillTotemsDat"/> on <see cref="Specification.LoadSkillTotemsDat"/> index.</remarks>
    public required int SkillTotemsKey { get; init; }

    /// <summary> Gets TotemSkinId.</summary>
    public required int TotemSkinId { get; init; }

    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }
}
