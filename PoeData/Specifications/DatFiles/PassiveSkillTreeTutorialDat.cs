// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PassiveSkillTreeTutorial.dat data.
/// </summary>
public sealed partial class PassiveSkillTreeTutorialDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets CharactersKey.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.LoadCharactersDat"/> index.</remarks>
    public required int? CharactersKey { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets ChoiceA_Description.</summary>
    public required string ChoiceA_Description { get; init; }

    /// <summary> Gets ChoiceB_Description.</summary>
    public required string ChoiceB_Description { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int? Unknown56 { get; init; }

    /// <summary> Gets ChoiceA_PassiveTreeURL.</summary>
    public required string ChoiceA_PassiveTreeURL { get; init; }

    /// <summary> Gets ChoiceB_PassiveTreeURL.</summary>
    public required string ChoiceB_PassiveTreeURL { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int? Unknown88 { get; init; }

    /// <summary> Gets Unknown104.</summary>
    public required int? Unknown104 { get; init; }
}
