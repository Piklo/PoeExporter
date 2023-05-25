// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ProjectileVariations.dat data.
/// </summary>
public sealed partial class ProjectileVariationsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ProjectileKey.</summary>
    /// <remarks> references <see cref="ProjectilesDat"/> on <see cref="Specification.LoadProjectilesDat"/> index.</remarks>
    public required int? ProjectileKey { get; init; }
}
