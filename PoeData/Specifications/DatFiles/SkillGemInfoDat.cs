// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SkillGemInfo.dat data.
/// </summary>
public sealed partial class SkillGemInfoDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets VideoURL1.</summary>
    public required string VideoURL1 { get; init; }

    /// <summary> Gets SkillGemsKey.</summary>
    /// <remarks> references <see cref="SkillGemsDat"/> on <see cref="Specification.LoadSkillGemsDat"/> index.</remarks>
    public required int? SkillGemsKey { get; init; }

    /// <summary> Gets VideoURL2.</summary>
    public required string VideoURL2 { get; init; }

    /// <summary> Gets CharactersKeys.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.LoadCharactersDat"/> index.</remarks>
    public required ReadOnlyCollection<int> CharactersKeys { get; init; }
}
