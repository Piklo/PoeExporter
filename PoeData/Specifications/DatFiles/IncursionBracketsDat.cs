// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing IncursionBrackets.dat data.
/// </summary>
public sealed partial class IncursionBracketsDat
{
    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets Incursion_WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? Incursion_WorldAreasKey { get; init; }

    /// <summary> Gets Template_WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? Template_WorldAreasKey { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required ReadOnlyCollection<float> Unknown36 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required float Unknown52 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }
}
