// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SpawnAdditionalChestsOrClusters.dat data.
/// </summary>
public sealed partial class SpawnAdditionalChestsOrClustersDat
{
    /// <summary> Gets StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? StatsKey { get; init; }

    /// <summary> Gets ChestsKey.</summary>
    /// <remarks> references <see cref="ChestsDat"/> on <see cref="Specification.LoadChestsDat"/> index.</remarks>
    public required int? ChestsKey { get; init; }

    /// <summary> Gets ChestClustersKey.</summary>
    /// <remarks> references <see cref="ChestClustersDat"/> on <see cref="Specification.LoadChestClustersDat"/> index.</remarks>
    public required int? ChestClustersKey { get; init; }
}
