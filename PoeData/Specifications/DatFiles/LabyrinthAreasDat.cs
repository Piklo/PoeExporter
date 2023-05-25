// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LabyrinthAreas.dat data.
/// </summary>
public sealed partial class LabyrinthAreasDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Normal_WorldAreasKeys.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Normal_WorldAreasKeys { get; init; }

    /// <summary> Gets Cruel_WorldAreasKeys.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Cruel_WorldAreasKeys { get; init; }

    /// <summary> Gets Merciless_WorldAreasKeys.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Merciless_WorldAreasKeys { get; init; }

    /// <summary> Gets Endgame_WorldAreasKeys.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Endgame_WorldAreasKeys { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }
}
