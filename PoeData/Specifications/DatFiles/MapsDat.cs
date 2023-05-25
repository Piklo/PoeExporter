// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Maps.dat data.
/// </summary>
public sealed partial class MapsDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets Regular_WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? Regular_WorldAreasKey { get; init; }

    /// <summary> Gets Unique_WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? Unique_WorldAreasKey { get; init; }

    /// <summary> Gets MapUpgrade_BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? MapUpgrade_BaseItemTypesKey { get; init; }

    /// <summary> Gets MonsterPacksKeys.</summary>
    /// <remarks> references <see cref="MonsterPacksDat"/> on <see cref="Specification.LoadMonsterPacksDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MonsterPacksKeys { get; init; }

    /// <summary> Gets AchievementItem.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? AchievementItem { get; init; }

    /// <summary> Gets Regular_GuildCharacter.</summary>
    public required string Regular_GuildCharacter { get; init; }

    /// <summary> Gets Unique_GuildCharacter.</summary>
    public required string Unique_GuildCharacter { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets Shaped_Base_MapsKey.</summary>
    /// <remarks> references <see cref="MapsDat"/> on <see cref="Specification.LoadMapsDat"/> index.</remarks>
    public required int? Shaped_Base_MapsKey { get; init; }

    /// <summary> Gets Shaped_AreaLevel.</summary>
    public required int Shaped_AreaLevel { get; init; }

    /// <summary> Gets UpgradedFrom_MapsKey.</summary>
    /// <remarks> references <see cref="MapsDat"/> on <see cref="Specification.LoadMapsDat"/> index.</remarks>
    public required int? UpgradedFrom_MapsKey { get; init; }

    /// <summary> Gets MapsKey2.</summary>
    /// <remarks> references <see cref="MapsDat"/> on <see cref="Specification.LoadMapsDat"/> index.</remarks>
    public required int? MapsKey2 { get; init; }

    /// <summary> Gets MapsKey3.</summary>
    /// <remarks> references <see cref="MapsDat"/> on <see cref="Specification.LoadMapsDat"/> index.</remarks>
    public required int? MapsKey3 { get; init; }

    /// <summary> Gets MapSeriesKey.</summary>
    public required int MapSeriesKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown156 is set.</summary>
    public required bool Unknown156 { get; init; }

    /// <summary> Gets a value indicating whether Unknown157 is set.</summary>
    public required bool Unknown157 { get; init; }

    /// <summary> Gets a value indicating whether Unknown158 is set.</summary>
    public required bool Unknown158 { get; init; }
}
