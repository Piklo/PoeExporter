// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MonsterProjectileSpell.dat data.
/// </summary>
public sealed partial class MonsterProjectileSpellDat
{
    /// <summary> Gets Id.</summary>
    public required int Id { get; init; }

    /// <summary> Gets Projectile.</summary>
    /// <remarks> references <see cref="ProjectilesDat"/> on <see cref="Specification.LoadProjectilesDat"/> index.</remarks>
    public required int? Projectile { get; init; }

    /// <summary> Gets Animation.</summary>
    /// <remarks> references <see cref="AnimationDat"/> on <see cref="Specification.LoadAnimationDat"/> index.</remarks>
    public required int? Animation { get; init; }

    /// <summary> Gets a value indicating whether Unknown36 is set.</summary>
    public required bool Unknown36 { get; init; }

    /// <summary> Gets a value indicating whether Unknown37 is set.</summary>
    public required bool Unknown37 { get; init; }

    /// <summary> Gets Unknown38.</summary>
    public required int Unknown38 { get; init; }

    /// <summary> Gets a value indicating whether Unknown42 is set.</summary>
    public required bool Unknown42 { get; init; }
}
