// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing CharacterEventTextAudio.dat data.
/// </summary>
public sealed partial class CharacterEventTextAudioDat
{
    /// <summary> Gets Event.</summary>
    /// <remarks> references <see cref="CharacterAudioEventsDat"/> on <see cref="Specification.LoadCharacterAudioEventsDat"/> index.</remarks>
    public required int? Event { get; init; }

    /// <summary> Gets Character.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.LoadCharactersDat"/> index.</remarks>
    public required int? Character { get; init; }

    /// <summary> Gets TextAudio.</summary>
    /// <remarks> references <see cref="CharacterTextAudioDat"/> on <see cref="Specification.LoadCharacterTextAudioDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TextAudio { get; init; }
}
