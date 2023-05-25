// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AtlasExileBossArenas.dat data.
/// </summary>
public sealed partial class AtlasExileBossArenasDat
{
    /// <summary> Gets Conqueror.</summary>
    /// <remarks> references <see cref="AtlasExilesDat"/> on <see cref="Specification.LoadAtlasExilesDat"/> index.</remarks>
    public required int? Conqueror { get; init; }

    /// <summary> Gets WorldArea.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? WorldArea { get; init; }
}
