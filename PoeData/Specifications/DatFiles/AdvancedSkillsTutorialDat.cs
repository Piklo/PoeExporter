// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AdvancedSkillsTutorial.dat data.
/// </summary>
public sealed partial class AdvancedSkillsTutorialDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets SkillGemInfoKey1.</summary>
    /// <remarks> references <see cref="SkillGemInfoDat"/> on <see cref="Specification.LoadSkillGemInfoDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SkillGemInfoKey1 { get; init; }

    /// <summary> Gets SkillGemInfoKey2.</summary>
    /// <remarks> references <see cref="SkillGemInfoDat"/> on <see cref="Specification.LoadSkillGemInfoDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SkillGemInfoKey2 { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets International_BK2File.</summary>
    public required string International_BK2File { get; init; }

    /// <summary> Gets SkillGemsKey.</summary>
    /// <remarks> references <see cref="SkillGemsDat"/> on <see cref="Specification.LoadSkillGemsDat"/> index.</remarks>
    public required int? SkillGemsKey { get; init; }

    /// <summary> Gets China_BK2File.</summary>
    public required string China_BK2File { get; init; }

    /// <summary> Gets CharactersKey.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.LoadCharactersDat"/> index.</remarks>
    public required ReadOnlyCollection<int> CharactersKey { get; init; }
}
