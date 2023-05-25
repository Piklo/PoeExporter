// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AtlasInfluenceSets.dat data.
/// </summary>
public sealed partial class AtlasInfluenceSetsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets InfluencePacks.</summary>
    /// <remarks> references <see cref="AtlasInfluenceOutcomesDat"/> on <see cref="Specification.LoadAtlasInfluenceOutcomesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> InfluencePacks { get; init; }
}
