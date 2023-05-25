// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Stats.dat data.
/// </summary>
public sealed partial class StatsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets a value indicating whether Unknown8 is set.</summary>
    public required bool Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether IsLocal is set.</summary>
    public required bool IsLocal { get; init; }

    /// <summary> Gets a value indicating whether IsWeaponLocal is set.</summary>
    public required bool IsWeaponLocal { get; init; }

    /// <summary> Gets Semantics.</summary>
    /// <remarks> references <see cref="StatSemanticsDat"/> on <see cref="Specification.LoadStatSemanticsDat"/> index.</remarks>
    public required int Semantics { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets a value indicating whether Unknown23 is set.</summary>
    public required bool Unknown23 { get; init; }

    /// <summary> Gets a value indicating whether IsVirtual is set.</summary>
    public required bool IsVirtual { get; init; }

    /// <summary> Gets MainHandAlias_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? MainHandAlias_StatsKey { get; init; }

    /// <summary> Gets OffHandAlias_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? OffHandAlias_StatsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown41 is set.</summary>
    public required bool Unknown41 { get; init; }

    /// <summary> Gets HASH32.</summary>
    public required int HASH32 { get; init; }

    /// <summary> Gets BelongsActiveSkillsKey.</summary>
    /// <remarks> references <see cref="ActiveSkillsDat"/> on <see cref="ActiveSkillsDat.Id"/>.</remarks>
    public required ReadOnlyCollection<string> BelongsActiveSkillsKey { get; init; }

    /// <summary> Gets Category.</summary>
    /// <remarks> references <see cref="PassiveSkillStatCategoriesDat"/> on <see cref="Specification.LoadPassiveSkillStatCategoriesDat"/> index.</remarks>
    public required int? Category { get; init; }

    /// <summary> Gets a value indicating whether Unknown78 is set.</summary>
    public required bool Unknown78 { get; init; }

    /// <summary> Gets a value indicating whether Unknown79 is set.</summary>
    public required bool Unknown79 { get; init; }

    /// <summary> Gets a value indicating whether IsScalable is set.</summary>
    public required bool IsScalable { get; init; }

    /// <summary> Gets ContextFlags.</summary>
    /// <remarks> references <see cref="VirtualStatContextFlagsDat"/> on <see cref="Specification.LoadVirtualStatContextFlagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ContextFlags { get; init; }

    /// <summary> Gets Unknown97.</summary>
    public required ReadOnlyCollection<int> Unknown97 { get; init; }
}
