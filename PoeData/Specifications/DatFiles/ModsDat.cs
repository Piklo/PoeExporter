// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Mods.dat data.
/// </summary>
public sealed partial class ModsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets ModTypeKey.</summary>
    /// <remarks> references <see cref="ModTypeDat"/> on <see cref="Specification.LoadModTypeDat"/> index.</remarks>
    public required int? ModTypeKey { get; init; }

    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets StatsKey1.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? StatsKey1 { get; init; }

    /// <summary> Gets StatsKey2.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? StatsKey2 { get; init; }

    /// <summary> Gets StatsKey3.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? StatsKey3 { get; init; }

    /// <summary> Gets StatsKey4.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? StatsKey4 { get; init; }

    /// <summary> Gets Domain.</summary>
    /// <remarks> references <see cref="ModDomainsDat"/> on <see cref="Specification.LoadModDomainsDat"/> index.</remarks>
    public required int Domain { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets GenerationType.</summary>
    /// <remarks> references <see cref="ModGenerationTypeDat"/> on <see cref="Specification.LoadModGenerationTypeDat"/> index.</remarks>
    public required int GenerationType { get; init; }

    /// <summary> Gets Families.</summary>
    /// <remarks> references <see cref="ModFamilyDat"/> on <see cref="Specification.LoadModFamilyDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Families { get; init; }

    /// <summary> Gets Stat1Min.</summary>
    public required int Stat1Min { get; init; }

    /// <summary> Gets Stat1Max.</summary>
    public required int Stat1Max { get; init; }

    /// <summary> Gets Stat2Min.</summary>
    public required int Stat2Min { get; init; }

    /// <summary> Gets Stat2Max.</summary>
    public required int Stat2Max { get; init; }

    /// <summary> Gets Stat3Min.</summary>
    public required int Stat3Min { get; init; }

    /// <summary> Gets Stat3Max.</summary>
    public required int Stat3Max { get; init; }

    /// <summary> Gets Stat4Min.</summary>
    public required int Stat4Min { get; init; }

    /// <summary> Gets Stat4Max.</summary>
    public required int Stat4Max { get; init; }

    /// <summary> Gets SpawnWeight_TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SpawnWeight_TagsKeys { get; init; }

    /// <summary> Gets SpawnWeight_Values.</summary>
    public required ReadOnlyCollection<int> SpawnWeight_Values { get; init; }

    /// <summary> Gets TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TagsKeys { get; init; }

    /// <summary> Gets GrantedEffectsPerLevelKeys.</summary>
    /// <remarks> references <see cref="GrantedEffectsPerLevelDat"/> on <see cref="Specification.LoadGrantedEffectsPerLevelDat"/> index.</remarks>
    public required ReadOnlyCollection<int> GrantedEffectsPerLevelKeys { get; init; }

    /// <summary> Gets Unknown224.</summary>
    public required ReadOnlyCollection<int> Unknown224 { get; init; }

    /// <summary> Gets MonsterMetadata.</summary>
    public required string MonsterMetadata { get; init; }

    /// <summary> Gets MonsterKillAchievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MonsterKillAchievements { get; init; }

    /// <summary> Gets ChestModType.</summary>
    /// <remarks> references <see cref="ModTypeDat"/> on <see cref="Specification.LoadModTypeDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ChestModType { get; init; }

    /// <summary> Gets Stat5Min.</summary>
    public required int Stat5Min { get; init; }

    /// <summary> Gets Stat5Max.</summary>
    public required int Stat5Max { get; init; }

    /// <summary> Gets StatsKey5.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? StatsKey5 { get; init; }

    /// <summary> Gets FullAreaClear_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> FullAreaClear_AchievementItemsKey { get; init; }

    /// <summary> Gets AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKey { get; init; }

    /// <summary> Gets GenerationWeight_TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> GenerationWeight_TagsKeys { get; init; }

    /// <summary> Gets GenerationWeight_Values.</summary>
    public required ReadOnlyCollection<int> GenerationWeight_Values { get; init; }

    /// <summary> Gets ModifyMapsAchievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModifyMapsAchievements { get; init; }

    /// <summary> Gets a value indicating whether IsEssenceOnlyModifier is set.</summary>
    public required bool IsEssenceOnlyModifier { get; init; }

    /// <summary> Gets Stat6Min.</summary>
    public required int Stat6Min { get; init; }

    /// <summary> Gets Stat6Max.</summary>
    public required int Stat6Max { get; init; }

    /// <summary> Gets StatsKey6.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? StatsKey6 { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets a value indicating whether Unknown413 is set.</summary>
    public required bool Unknown413 { get; init; }

    /// <summary> Gets CraftingItemClassRestrictions.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.LoadItemClassesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> CraftingItemClassRestrictions { get; init; }

    /// <summary> Gets MonsterOnDeath.</summary>
    public required string MonsterOnDeath { get; init; }

    /// <summary> Gets Unknown438.</summary>
    public required int Unknown438 { get; init; }

    /// <summary> Gets Unknown442.</summary>
    /// <remarks> references <see cref="GrantedEffectsPerLevelDat"/> on <see cref="Specification.LoadGrantedEffectsPerLevelDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown442 { get; init; }

    /// <summary> Gets Heist_SubStatValue1.</summary>
    public required int Heist_SubStatValue1 { get; init; }

    /// <summary> Gets Heist_SubStatValue2.</summary>
    public required int Heist_SubStatValue2 { get; init; }

    /// <summary> Gets Heist_StatsKey0.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Heist_StatsKey0 { get; init; }

    /// <summary> Gets Heist_StatsKey1.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Heist_StatsKey1 { get; init; }

    /// <summary> Gets Heist_AddStatValue1.</summary>
    public required int Heist_AddStatValue1 { get; init; }

    /// <summary> Gets Heist_AddStatValue2.</summary>
    public required int Heist_AddStatValue2 { get; init; }

    /// <summary> Gets InfluenceTypes.</summary>
    /// <remarks> references <see cref="InfluenceTypesDat"/> on <see cref="Specification.LoadInfluenceTypesDat"/> index.</remarks>
    public required int InfluenceTypes { get; init; }

    /// <summary> Gets ImplicitTagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ImplicitTagsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown526 is set.</summary>
    public required bool Unknown526 { get; init; }

    /// <summary> Gets Unknown527.</summary>
    public required int Unknown527 { get; init; }

    /// <summary> Gets Unknown531.</summary>
    public required int Unknown531 { get; init; }

    /// <summary> Gets Unknown535.</summary>
    public required int Unknown535 { get; init; }

    /// <summary> Gets Unknown539.</summary>
    public required int Unknown539 { get; init; }

    /// <summary> Gets Unknown543.</summary>
    public required int Unknown543 { get; init; }

    /// <summary> Gets Unknown547.</summary>
    public required int Unknown547 { get; init; }

    /// <summary> Gets Unknown551.</summary>
    public required int Unknown551 { get; init; }

    /// <summary> Gets Unknown555.</summary>
    public required int Unknown555 { get; init; }

    /// <summary> Gets Unknown559.</summary>
    public required int Unknown559 { get; init; }

    /// <summary> Gets Unknown563.</summary>
    public required int Unknown563 { get; init; }

    /// <summary> Gets Unknown567.</summary>
    public required int Unknown567 { get; init; }

    /// <summary> Gets Unknown571.</summary>
    public required int Unknown571 { get; init; }

    /// <summary> Gets Unknown575.</summary>
    public required int Unknown575 { get; init; }

    /// <summary> Gets Unknown579.</summary>
    public required int Unknown579 { get; init; }

    /// <summary> Gets Unknown583.</summary>
    public required int Unknown583 { get; init; }

    /// <summary> Gets Unknown587.</summary>
    public required int Unknown587 { get; init; }

    /// <summary> Gets BuffTemplate.</summary>
    /// <remarks> references <see cref="BuffTemplatesDat"/> on <see cref="Specification.LoadBuffTemplatesDat"/> index.</remarks>
    public required int? BuffTemplate { get; init; }

    /// <summary> Gets ArchnemesisMinionMod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? ArchnemesisMinionMod { get; init; }

    /// <summary> Gets HASH32.</summary>
    public required int HASH32 { get; init; }

    /// <summary> Gets Unknown619.</summary>
    public required ReadOnlyCollection<int> Unknown619 { get; init; }

    /// <summary> Gets Unknown635.</summary>
    public required int Unknown635 { get; init; }

    /// <summary> Gets Unknown639.</summary>
    /// <remarks> references <see cref="GrantedEffectsPerLevelDat"/> on <see cref="Specification.LoadGrantedEffectsPerLevelDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown639 { get; init; }
}
