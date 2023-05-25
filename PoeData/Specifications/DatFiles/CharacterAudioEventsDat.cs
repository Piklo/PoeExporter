// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing CharacterAudioEvents.dat data.
/// </summary>
public sealed partial class CharacterAudioEventsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Event.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.LoadQuestFlagsDat"/> index.</remarks>
    public required int? Event { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets Goddess_CharacterTextAudioKeys.</summary>
    /// <remarks> references <see cref="CharacterTextAudioDat"/> on <see cref="Specification.LoadCharacterTextAudioDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Goddess_CharacterTextAudioKeys { get; init; }

    /// <summary> Gets JackTheAxe_CharacterTextAudioKeys.</summary>
    /// <remarks> references <see cref="CharacterTextAudioDat"/> on <see cref="Specification.LoadCharacterTextAudioDat"/> index.</remarks>
    public required ReadOnlyCollection<int> JackTheAxe_CharacterTextAudioKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown64 is set.</summary>
    public required bool Unknown64 { get; init; }

    /// <summary> Gets a value indicating whether Unknown65 is set.</summary>
    public required bool Unknown65 { get; init; }
}
