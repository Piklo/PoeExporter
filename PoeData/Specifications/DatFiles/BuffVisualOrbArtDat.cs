// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BuffVisualOrbArt.dat data.
/// </summary>
public sealed partial class BuffVisualOrbArtDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MiscAnimated.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimated { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required ReadOnlyCollection<int> Unknown24 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required ReadOnlyCollection<string> Unknown40 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required string Unknown56 { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required string Unknown64 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required string Unknown72 { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required string Unknown80 { get; init; }
}
