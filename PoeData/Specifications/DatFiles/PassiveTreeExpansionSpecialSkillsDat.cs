// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PassiveTreeExpansionSpecialSkills.dat data.
/// </summary>
public sealed partial class PassiveTreeExpansionSpecialSkillsDat
{
    /// <summary> Gets PassiveSkillsKey.</summary>
    /// <remarks> references <see cref="PassiveSkillsDat"/> on <see cref="Specification.LoadPassiveSkillsDat"/> index.</remarks>
    public required int? PassiveSkillsKey { get; init; }

    /// <summary> Gets StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? StatsKey { get; init; }
}
