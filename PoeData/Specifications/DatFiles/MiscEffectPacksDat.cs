// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MiscEffectPacks.dat data.
/// </summary>
public sealed partial class MiscEffectPacksDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets EPKFile.</summary>
    public required string EPKFile { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int Unknown20 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets PreloadGroups.</summary>
    /// <remarks> references <see cref="PreloadGroupsDat"/> on <see cref="Specification.LoadPreloadGroupsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> PreloadGroups { get; init; }

    /// <summary> Gets a value indicating whether Unknown44 is set.</summary>
    public required bool Unknown44 { get; init; }

    /// <summary> Gets PlayerOnly_EPKFile.</summary>
    public required string PlayerOnly_EPKFile { get; init; }

    /// <summary> Gets a value indicating whether Unknown53 is set.</summary>
    public required bool Unknown53 { get; init; }
}
