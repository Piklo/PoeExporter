// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AtlasBaseTypeDrops.dat data.
/// </summary>
public sealed partial class AtlasBaseTypeDropsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets AtlasRegionsKey.</summary>
    /// <remarks> references <see cref="AtlasRegionsDat"/> on <see cref="Specification.LoadAtlasRegionsDat"/> index.</remarks>
    public required int? AtlasRegionsKey { get; init; }

    /// <summary> Gets MinTier.</summary>
    public required int MinTier { get; init; }

    /// <summary> Gets MaxTier.</summary>
    public required int MaxTier { get; init; }

    /// <summary> Gets SpawnWeight_TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SpawnWeight_TagsKeys { get; init; }

    /// <summary> Gets SpawnWeight_Values.</summary>
    public required ReadOnlyCollection<int> SpawnWeight_Values { get; init; }
}
