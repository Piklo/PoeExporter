// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing IndexableSupportGems.dat data.
/// </summary>
public sealed partial class IndexableSupportGemsDat
{
    /// <summary> Gets Index.</summary>
    public required int Index { get; init; }

    /// <summary> Gets SupportGem.</summary>
    /// <remarks> references <see cref="SkillGemsDat"/> on <see cref="Specification.LoadSkillGemsDat"/> index.</remarks>
    public required int? SupportGem { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }
}
