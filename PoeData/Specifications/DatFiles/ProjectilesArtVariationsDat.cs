// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ProjectilesArtVariations.dat data.
/// </summary>
public sealed partial class ProjectilesArtVariationsDat
{
    /// <summary> Gets Projectile.</summary>
    public required string Projectile { get; init; }

    /// <summary> Gets Variant.</summary>
    public required int Variant { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required ReadOnlyCollection<int> Unknown12 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int? Unknown32 { get; init; }
}
