// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing CharacterStartStates.dat data.
/// </summary>
public sealed partial class CharacterStartStatesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets CharactersKey.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.LoadCharactersDat"/> index.</remarks>
    public required int? CharactersKey { get; init; }

    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets PassiveSkillsKeys.</summary>
    /// <remarks> references <see cref="PassiveSkillsDat"/> on <see cref="Specification.LoadPassiveSkillsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> PassiveSkillsKeys { get; init; }

    /// <summary> Gets CharacterStartStateSetKey.</summary>
    /// <remarks> references <see cref="CharacterStartStateSetDat"/> on <see cref="Specification.LoadCharacterStartStateSetDat"/> index.</remarks>
    public required int? CharacterStartStateSetKey { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int? Unknown68 { get; init; }

    /// <summary> Gets CharacterStartQuestStateKeys.</summary>
    /// <remarks> references <see cref="CharacterStartQuestStateDat"/> on <see cref="Specification.LoadCharacterStartQuestStateDat"/> index.</remarks>
    public required ReadOnlyCollection<int> CharacterStartQuestStateKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown100 is set.</summary>
    public required bool Unknown100 { get; init; }

    /// <summary> Gets InfoText.</summary>
    public required string InfoText { get; init; }

    /// <summary> Gets Unknown109.</summary>
    public required int? Unknown109 { get; init; }
}
