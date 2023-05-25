// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BuffVisualOrbs.dat data.
/// </summary>
public sealed partial class BuffVisualOrbsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BuffVisualOrbTypesKey.</summary>
    /// <remarks> references <see cref="BuffVisualOrbTypesDat"/> on <see cref="Specification.LoadBuffVisualOrbTypesDat"/> index.</remarks>
    public required int? BuffVisualOrbTypesKey { get; init; }

    /// <summary> Gets BuffVisualOrbArtKeys.</summary>
    /// <remarks> references <see cref="BuffVisualOrbArtDat"/> on <see cref="Specification.LoadBuffVisualOrbArtDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BuffVisualOrbArtKeys { get; init; }

    /// <summary> Gets Player_BuffVisualOrbArtKeys.</summary>
    /// <remarks> references <see cref="BuffVisualOrbArtDat"/> on <see cref="Specification.LoadBuffVisualOrbArtDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Player_BuffVisualOrbArtKeys { get; init; }

    /// <summary> Gets BuffVisualOrbArtKeys2.</summary>
    /// <remarks> references <see cref="BuffVisualOrbArtDat"/> on <see cref="Specification.LoadBuffVisualOrbArtDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BuffVisualOrbArtKeys2 { get; init; }
}
