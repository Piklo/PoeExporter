// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing QuestStates.dat data.
/// </summary>
public sealed partial class QuestStatesDat
{
    /// <summary> Gets QuestKey.</summary>
    /// <remarks> references <see cref="QuestDat"/> on <see cref="Specification.LoadQuestDat"/> index.</remarks>
    public required int? QuestKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets QuestStates.</summary>
    public required ReadOnlyCollection<int> QuestStates { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required ReadOnlyCollection<int> Unknown36 { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets a value indicating whether Unknown60 is set.</summary>
    public required bool Unknown60 { get; init; }

    /// <summary> Gets Message.</summary>
    public required string Message { get; init; }

    /// <summary> Gets MapPinsKeys1.</summary>
    /// <remarks> references <see cref="MapPinsDat"/> on <see cref="Specification.LoadMapPinsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MapPinsKeys1 { get; init; }

    /// <summary> Gets Unknown85.</summary>
    public required int Unknown85 { get; init; }

    /// <summary> Gets MapPinsTexts.</summary>
    public required ReadOnlyCollection<string> MapPinsTexts { get; init; }

    /// <summary> Gets MapPinsKeys2.</summary>
    /// <remarks> references <see cref="MapPinsDat"/> on <see cref="Specification.LoadMapPinsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MapPinsKeys2 { get; init; }

    /// <summary> Gets Unknown121.</summary>
    public required ReadOnlyCollection<int> Unknown121 { get; init; }

    /// <summary> Gets a value indicating whether Unknown137 is set.</summary>
    public required bool Unknown137 { get; init; }

    /// <summary> Gets Unknown138.</summary>
    public required ReadOnlyCollection<int> Unknown138 { get; init; }

    /// <summary> Gets Unknown154.</summary>
    public required ReadOnlyCollection<int> Unknown154 { get; init; }

    /// <summary> Gets Unknown170.</summary>
    public required int Unknown170 { get; init; }

    /// <summary> Gets SoundEffect.</summary>
    /// <remarks> references <see cref="SoundEffectsDat"/> on <see cref="Specification.LoadSoundEffectsDat"/> index.</remarks>
    public required int? SoundEffect { get; init; }
}
