// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AtlasRegions.dat data.
/// </summary>
public sealed partial class AtlasRegionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int? Unknown20 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required float Unknown36 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required float Unknown40 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int? Unknown44 { get; init; }

    /// <summary> Gets CitadelName.</summary>
    public required string CitadelName { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int Unknown68 { get; init; }

    /// <summary> Gets BK2File.</summary>
    public required string BK2File { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required int Unknown80 { get; init; }

    /// <summary> Gets AdviceAudio.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? AdviceAudio { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required int? Unknown100 { get; init; }

    /// <summary> Gets Quest.</summary>
    /// <remarks> references <see cref="QuestDat"/> on <see cref="Specification.LoadQuestDat"/> index.</remarks>
    public required int? Quest { get; init; }
}
