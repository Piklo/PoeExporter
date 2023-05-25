// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing WorldAreas.dat data.
/// </summary>
public sealed partial class WorldAreasDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Act.</summary>
    public required int Act { get; init; }

    /// <summary> Gets a value indicating whether IsTown is set.</summary>
    public required bool IsTown { get; init; }

    /// <summary> Gets a value indicating whether HasWaypoint is set.</summary>
    public required bool HasWaypoint { get; init; }

    /// <summary> Gets Connections_WorldAreasKeys.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Connections_WorldAreasKeys { get; init; }

    /// <summary> Gets AreaLevel.</summary>
    public required int AreaLevel { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets Unknown46.</summary>
    public required int Unknown46 { get; init; }

    /// <summary> Gets Unknown50.</summary>
    public required int Unknown50 { get; init; }

    /// <summary> Gets LoadingScreen_DDSFile.</summary>
    public required string LoadingScreen_DDSFile { get; init; }

    /// <summary> Gets Unknown62.</summary>
    public required int? Unknown62 { get; init; }

    /// <summary> Gets Unknown78.</summary>
    public required ReadOnlyCollection<int> Unknown78 { get; init; }

    /// <summary> Gets Unknown94.</summary>
    public required int Unknown94 { get; init; }

    /// <summary> Gets TopologiesKeys.</summary>
    /// <remarks> references <see cref="TopologiesDat"/> on <see cref="Specification.LoadTopologiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TopologiesKeys { get; init; }

    /// <summary> Gets ParentTown_WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? ParentTown_WorldAreasKey { get; init; }

    /// <summary> Gets Unknown122.</summary>
    public required int Unknown122 { get; init; }

    /// <summary> Gets Unknown126.</summary>
    public required int? Unknown126 { get; init; }

    /// <summary> Gets Unknown142.</summary>
    public required int? Unknown142 { get; init; }

    /// <summary> Gets Bosses_MonsterVarietiesKeys.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Bosses_MonsterVarietiesKeys { get; init; }

    /// <summary> Gets Monsters_MonsterVarietiesKeys.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Monsters_MonsterVarietiesKeys { get; init; }

    /// <summary> Gets SpawnWeight_TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SpawnWeight_TagsKeys { get; init; }

    /// <summary> Gets SpawnWeight_Values.</summary>
    public required ReadOnlyCollection<int> SpawnWeight_Values { get; init; }

    /// <summary> Gets a value indicating whether IsMapArea is set.</summary>
    public required bool IsMapArea { get; init; }

    /// <summary> Gets FullClear_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> FullClear_AchievementItemsKeys { get; init; }

    /// <summary> Gets Unknown239.</summary>
    public required int? Unknown239 { get; init; }

    /// <summary> Gets AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? AchievementItemsKey { get; init; }

    /// <summary> Gets ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKeys { get; init; }

    /// <summary> Gets Unknown287.</summary>
    public required int Unknown287 { get; init; }

    /// <summary> Gets VaalArea.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> VaalArea { get; init; }

    /// <summary> Gets a value indicating whether Unknown307 is set.</summary>
    public required bool Unknown307 { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets AreaTypeTags.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AreaTypeTags { get; init; }

    /// <summary> Gets Unknown328.</summary>
    public required int Unknown328 { get; init; }

    /// <summary> Gets a value indicating whether IsHideout is set.</summary>
    public required bool IsHideout { get; init; }

    /// <summary> Gets Inflection.</summary>
    public required string Inflection { get; init; }

    /// <summary> Gets Unknown341.</summary>
    public required int Unknown341 { get; init; }

    /// <summary> Gets Unknown345.</summary>
    public required int Unknown345 { get; init; }

    /// <summary> Gets Tags.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Tags { get; init; }

    /// <summary> Gets a value indicating whether IsVaalArea is set.</summary>
    public required bool IsVaalArea { get; init; }

    /// <summary> Gets a value indicating whether IsLabyrinthAirlock is set.</summary>
    public required bool IsLabyrinthAirlock { get; init; }

    /// <summary> Gets a value indicating whether IsLabyrinthArea is set.</summary>
    public required bool IsLabyrinthArea { get; init; }

    /// <summary> Gets TwinnedFullClear_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? TwinnedFullClear_AchievementItemsKey { get; init; }

    /// <summary> Gets Enter_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? Enter_AchievementItemsKey { get; init; }

    /// <summary> Gets TSIFile.</summary>
    public required string TSIFile { get; init; }

    /// <summary> Gets Unknown408.</summary>
    public required int? Unknown408 { get; init; }

    /// <summary> Gets WaypointActivation_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> WaypointActivation_AchievementItemsKeys { get; init; }

    /// <summary> Gets a value indicating whether IsUniqueMapArea is set.</summary>
    public required bool IsUniqueMapArea { get; init; }

    /// <summary> Gets a value indicating whether IsLabyrinthBossArea is set.</summary>
    public required bool IsLabyrinthBossArea { get; init; }

    /// <summary> Gets FirstEntry_NPCTextAudioKey.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? FirstEntry_NPCTextAudioKey { get; init; }

    /// <summary> Gets FirstEntry_SoundEffectsKey.</summary>
    /// <remarks> references <see cref="SoundEffectsDat"/> on <see cref="Specification.LoadSoundEffectsDat"/> index.</remarks>
    public required int? FirstEntry_SoundEffectsKey { get; init; }

    /// <summary> Gets FirstEntry_NPCsKey.</summary>
    /// <remarks> references <see cref="NPCsDat"/> on <see cref="NPCsDat.Id"/>.</remarks>
    public required string FirstEntry_NPCsKey { get; init; }

    /// <summary> Gets Unknown482.</summary>
    public required int Unknown482 { get; init; }

    /// <summary> Gets EnvironmentsKey.</summary>
    /// <remarks> references <see cref="EnvironmentsDat"/> on <see cref="Specification.LoadEnvironmentsDat"/> index.</remarks>
    public required int? EnvironmentsKey { get; init; }

    /// <summary> Gets Unknown502.</summary>
    public required int Unknown502 { get; init; }

    /// <summary> Gets Unknown506.</summary>
    public required int? Unknown506 { get; init; }

    /// <summary> Gets Unknown522.</summary>
    public required int Unknown522 { get; init; }

    /// <summary> Gets Unknown526.</summary>
    public required int Unknown526 { get; init; }

    /// <summary> Gets Unknown530.</summary>
    public required int Unknown530 { get; init; }

    /// <summary> Gets Unknown534.</summary>
    public required int Unknown534 { get; init; }

    /// <summary> Gets Unknown538.</summary>
    public required int Unknown538 { get; init; }

    /// <summary> Gets a value indicating whether Unknown542 is set.</summary>
    public required bool Unknown542 { get; init; }

    /// <summary> Gets a value indicating whether Unknown543 is set.</summary>
    public required bool Unknown543 { get; init; }

    /// <summary> Gets Unknown544.</summary>
    public required int Unknown544 { get; init; }

    /// <summary> Gets Unknown548.</summary>
    public required int Unknown548 { get; init; }

    /// <summary> Gets Unknown552.</summary>
    public required int Unknown552 { get; init; }

    /// <summary> Gets Unknown556.</summary>
    public required ReadOnlyCollection<int> Unknown556 { get; init; }

    /// <summary> Gets Unknown572.</summary>
    public required int? Unknown572 { get; init; }

    /// <summary> Gets Unknown588.</summary>
    public required int Unknown588 { get; init; }

    /// <summary> Gets Unknown592.</summary>
    public required int? Unknown592 { get; init; }

    /// <summary> Gets Unknown608.</summary>
    public required int? Unknown608 { get; init; }

    /// <summary> Gets Unknown624.</summary>
    /// <remarks> references <see cref="RulesetsDat"/> on <see cref="Specification.LoadRulesetsDat"/> index.</remarks>
    public required int? Unknown624 { get; init; }
}
