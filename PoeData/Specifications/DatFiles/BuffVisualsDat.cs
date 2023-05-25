// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BuffVisuals.dat data.
/// </summary>
public sealed partial class BuffVisualsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BuffDDSFile.</summary>
    public required string BuffDDSFile { get; init; }

    /// <summary> Gets EPKFiles1.</summary>
    public required ReadOnlyCollection<string> EPKFiles1 { get; init; }

    /// <summary> Gets EPKFiles2.</summary>
    public required ReadOnlyCollection<string> EPKFiles2 { get; init; }

    /// <summary> Gets PreloadGroups.</summary>
    /// <remarks> references <see cref="PreloadGroupsDat"/> on <see cref="Specification.LoadPreloadGroupsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> PreloadGroups { get; init; }

    /// <summary> Gets a value indicating whether Unknown64 is set.</summary>
    public required bool Unknown64 { get; init; }

    /// <summary> Gets BuffName.</summary>
    public required string BuffName { get; init; }

    /// <summary> Gets MiscAnimated1.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimated1 { get; init; }

    /// <summary> Gets MiscAnimated2.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimated2 { get; init; }

    /// <summary> Gets BuffDescription.</summary>
    public required string BuffDescription { get; init; }

    /// <summary> Gets EPKFile.</summary>
    public required string EPKFile { get; init; }

    /// <summary> Gets a value indicating whether HasExtraArt is set.</summary>
    public required bool HasExtraArt { get; init; }

    /// <summary> Gets ExtraArt.</summary>
    public required string ExtraArt { get; init; }

    /// <summary> Gets Unknown130.</summary>
    public required ReadOnlyCollection<int> Unknown130 { get; init; }

    /// <summary> Gets EPKFiles.</summary>
    public required ReadOnlyCollection<string> EPKFiles { get; init; }

    /// <summary> Gets BuffVisualOrbs.</summary>
    /// <remarks> references <see cref="BuffVisualOrbsDat"/> on <see cref="Specification.LoadBuffVisualOrbsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BuffVisualOrbs { get; init; }

    /// <summary> Gets MiscAnimated3.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimated3 { get; init; }

    /// <summary> Gets Unknown194.</summary>
    public required int? Unknown194 { get; init; }
}
