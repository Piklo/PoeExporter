// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AtlasExiles.dat data.
/// </summary>
public sealed partial class AtlasExilesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets InfluencedItemIncrStat.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? InfluencedItemIncrStat { get; init; }

    /// <summary> Gets MapIcon.</summary>
    public required string MapIcon { get; init; }

    /// <summary> Gets MapIcon2.</summary>
    public required string MapIcon2 { get; init; }
}
