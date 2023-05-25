// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AtlasExileInfluence.dat data.
/// </summary>
public sealed partial class AtlasExileInfluenceDat
{
    /// <summary> Gets Conqueror.</summary>
    /// <remarks> references <see cref="AtlasExilesDat"/> on <see cref="Specification.LoadAtlasExilesDat"/> index.</remarks>
    public required int? Conqueror { get; init; }

    /// <summary> Gets Sets.</summary>
    /// <remarks> references <see cref="AtlasInfluenceSetsDat"/> on <see cref="Specification.LoadAtlasInfluenceSetsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Sets { get; init; }
}
