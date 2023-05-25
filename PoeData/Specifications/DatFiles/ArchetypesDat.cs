// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Archetypes.dat data.
/// </summary>
public sealed partial class ArchetypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets CharactersKey.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.LoadCharactersDat"/> index.</remarks>
    public required int? CharactersKey { get; init; }

    /// <summary> Gets PassiveSkillTreeURL.</summary>
    public required string PassiveSkillTreeURL { get; init; }

    /// <summary> Gets AscendancyClassName.</summary>
    public required string AscendancyClassName { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets UIImageFile.</summary>
    public required string UIImageFile { get; init; }

    /// <summary> Gets TutorialVideo_BKFile.</summary>
    public required string TutorialVideo_BKFile { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required float Unknown68 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required float Unknown72 { get; init; }

    /// <summary> Gets BackgroundImageFile.</summary>
    public required string BackgroundImageFile { get; init; }

    /// <summary> Gets a value indicating whether IsTemporary is set.</summary>
    public required bool IsTemporary { get; init; }

    /// <summary> Gets a value indicating whether Unknown85 is set.</summary>
    public required bool Unknown85 { get; init; }

    /// <summary> Gets ArchetypeImage.</summary>
    public required string ArchetypeImage { get; init; }

    /// <summary> Gets a value indicating whether Unknown94 is set.</summary>
    public required bool Unknown94 { get; init; }

    /// <summary> Gets a value indicating whether Unknown95 is set.</summary>
    public required bool Unknown95 { get; init; }
}
