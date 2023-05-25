// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing NPCTextAudio.dat data.
/// </summary>
public sealed partial class NPCTextAudioDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets CharactersKey.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.LoadCharactersDat"/> index.</remarks>
    public required int? CharactersKey { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets Mono_AudioFile.</summary>
    public required string Mono_AudioFile { get; init; }

    /// <summary> Gets Stereo_AudioFile.</summary>
    public required string Stereo_AudioFile { get; init; }

    /// <summary> Gets a value indicating whether HasStereo is set.</summary>
    public required bool HasStereo { get; init; }

    /// <summary> Gets a value indicating whether Unknown49 is set.</summary>
    public required bool Unknown49 { get; init; }

    /// <summary> Gets Video.</summary>
    public required string Video { get; init; }

    /// <summary> Gets Unknown58.</summary>
    public required int Unknown58 { get; init; }

    /// <summary> Gets Unknown62.</summary>
    public required int Unknown62 { get; init; }

    /// <summary> Gets Unknown66.</summary>
    public required int Unknown66 { get; init; }

    /// <summary> Gets Unknown70.</summary>
    public required ReadOnlyCollection<int> Unknown70 { get; init; }

    /// <summary> Gets Unknown86.</summary>
    public required int? Unknown86 { get; init; }
}
