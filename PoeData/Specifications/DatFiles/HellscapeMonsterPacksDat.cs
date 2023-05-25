// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HellscapeMonsterPacks.dat data.
/// </summary>
public sealed partial class HellscapeMonsterPacksDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MonsterPack.</summary>
    /// <remarks> references <see cref="MonsterPacksDat"/> on <see cref="Specification.LoadMonsterPacksDat"/> index.</remarks>
    public required int? MonsterPack { get; init; }

    /// <summary> Gets Faction.</summary>
    /// <remarks> references <see cref="HellscapeFactionsDat"/> on <see cref="Specification.LoadHellscapeFactionsDat"/> index.</remarks>
    public required int? Faction { get; init; }

    /// <summary> Gets a value indicating whether IsDonutPack is set.</summary>
    public required bool IsDonutPack { get; init; }

    /// <summary> Gets a value indicating whether IsElite is set.</summary>
    public required bool IsElite { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }
}
