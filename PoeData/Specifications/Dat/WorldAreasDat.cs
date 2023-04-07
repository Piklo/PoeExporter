// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

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
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
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
    /// <remarks> references <see cref="TopologiesDat"/> on <see cref="Specification.GetTopologiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TopologiesKeys { get; init; }

    /// <summary> Gets ParentTown_WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? ParentTown_WorldAreasKey { get; init; }

    /// <summary> Gets Unknown122.</summary>
    public required int Unknown122 { get; init; }

    /// <summary> Gets Unknown126.</summary>
    public required int? Unknown126 { get; init; }

    /// <summary> Gets Unknown142.</summary>
    public required int? Unknown142 { get; init; }

    /// <summary> Gets Bosses_MonsterVarietiesKeys.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Bosses_MonsterVarietiesKeys { get; init; }

    /// <summary> Gets Monsters_MonsterVarietiesKeys.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Monsters_MonsterVarietiesKeys { get; init; }

    /// <summary> Gets SpawnWeight_TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SpawnWeight_TagsKeys { get; init; }

    /// <summary> Gets SpawnWeight_Values.</summary>
    public required ReadOnlyCollection<int> SpawnWeight_Values { get; init; }

    /// <summary> Gets a value indicating whether IsMapArea is set.</summary>
    public required bool IsMapArea { get; init; }

    /// <summary> Gets FullClear_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> FullClear_AchievementItemsKeys { get; init; }

    /// <summary> Gets Unknown239.</summary>
    public required int? Unknown239 { get; init; }

    /// <summary> Gets AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? AchievementItemsKey { get; init; }

    /// <summary> Gets ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKeys { get; init; }

    /// <summary> Gets Unknown287.</summary>
    public required int Unknown287 { get; init; }

    /// <summary> Gets VaalArea.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> VaalArea { get; init; }

    /// <summary> Gets a value indicating whether Unknown307 is set.</summary>
    public required bool Unknown307 { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets AreaTypeTags.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
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
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Tags { get; init; }

    /// <summary> Gets a value indicating whether IsVaalArea is set.</summary>
    public required bool IsVaalArea { get; init; }

    /// <summary> Gets a value indicating whether IsLabyrinthAirlock is set.</summary>
    public required bool IsLabyrinthAirlock { get; init; }

    /// <summary> Gets a value indicating whether IsLabyrinthArea is set.</summary>
    public required bool IsLabyrinthArea { get; init; }

    /// <summary> Gets TwinnedFullClear_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? TwinnedFullClear_AchievementItemsKey { get; init; }

    /// <summary> Gets Enter_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? Enter_AchievementItemsKey { get; init; }

    /// <summary> Gets TSIFile.</summary>
    public required string TSIFile { get; init; }

    /// <summary> Gets Unknown408.</summary>
    public required int? Unknown408 { get; init; }

    /// <summary> Gets WaypointActivation_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> WaypointActivation_AchievementItemsKeys { get; init; }

    /// <summary> Gets a value indicating whether IsUniqueMapArea is set.</summary>
    public required bool IsUniqueMapArea { get; init; }

    /// <summary> Gets a value indicating whether IsLabyrinthBossArea is set.</summary>
    public required bool IsLabyrinthBossArea { get; init; }

    /// <summary> Gets FirstEntry_NPCTextAudioKey.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.GetNPCTextAudioDat"/> index.</remarks>
    public required int? FirstEntry_NPCTextAudioKey { get; init; }

    /// <summary> Gets FirstEntry_SoundEffectsKey.</summary>
    /// <remarks> references <see cref="SoundEffectsDat"/> on <see cref="Specification.GetSoundEffectsDat"/> index.</remarks>
    public required int? FirstEntry_SoundEffectsKey { get; init; }

    /// <summary> Gets FirstEntry_NPCsKey.</summary>
    /// <remarks> references <see cref="NPCsDat"/> on <see cref="NPCsDat.Id"/>.</remarks>
    public required string FirstEntry_NPCsKey { get; init; }

    /// <summary> Gets Unknown482.</summary>
    public required int Unknown482 { get; init; }

    /// <summary> Gets EnvironmentsKey.</summary>
    /// <remarks> references <see cref="EnvironmentsDat"/> on <see cref="Specification.GetEnvironmentsDat"/> index.</remarks>
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

    /// <summary>
    /// Gets WorldAreasDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of WorldAreasDat.</returns>
    internal static WorldAreasDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/WorldAreas.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WorldAreasDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Act
            (var actLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsTown
            (var istownLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HasWaypoint
            (var haswaypointLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Connections_WorldAreasKeys
            (var tempconnections_worldareaskeysLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var connections_worldareaskeysLoading = tempconnections_worldareaskeysLoading.AsReadOnly();

            // loading AreaLevel
            (var arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown46
            (var unknown46Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown50
            (var unknown50Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LoadingScreen_DDSFile
            (var loadingscreen_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown62
            (var unknown62Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown78
            (var tempunknown78Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown78Loading = tempunknown78Loading.AsReadOnly();

            // loading Unknown94
            (var unknown94Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TopologiesKeys
            (var temptopologieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var topologieskeysLoading = temptopologieskeysLoading.AsReadOnly();

            // loading ParentTown_WorldAreasKey
            (var parenttown_worldareaskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Unknown122
            (var unknown122Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown126
            (var unknown126Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown142
            (var unknown142Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Bosses_MonsterVarietiesKeys
            (var tempbosses_monstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var bosses_monstervarietieskeysLoading = tempbosses_monstervarietieskeysLoading.AsReadOnly();

            // loading Monsters_MonsterVarietiesKeys
            (var tempmonsters_monstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monsters_monstervarietieskeysLoading = tempmonsters_monstervarietieskeysLoading.AsReadOnly();

            // loading SpawnWeight_TagsKeys
            (var tempspawnweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnweight_tagskeysLoading = tempspawnweight_tagskeysLoading.AsReadOnly();

            // loading SpawnWeight_Values
            (var tempspawnweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweight_valuesLoading = tempspawnweight_valuesLoading.AsReadOnly();

            // loading IsMapArea
            (var ismapareaLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading FullClear_AchievementItemsKeys
            (var tempfullclear_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var fullclear_achievementitemskeysLoading = tempfullclear_achievementitemskeysLoading.AsReadOnly();

            // loading Unknown239
            (var unknown239Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading AchievementItemsKey
            (var achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ModsKeys
            (var tempmodskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeysLoading = tempmodskeysLoading.AsReadOnly();

            // loading Unknown287
            (var unknown287Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading VaalArea
            (var tempvaalareaLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var vaalareaLoading = tempvaalareaLoading.AsReadOnly();

            // loading Unknown307
            (var unknown307Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AreaTypeTags
            (var tempareatypetagsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var areatypetagsLoading = tempareatypetagsLoading.AsReadOnly();

            // loading Unknown328
            (var unknown328Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsHideout
            (var ishideoutLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Inflection
            (var inflectionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown341
            (var unknown341Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown345
            (var unknown345Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tags
            (var temptagsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tagsLoading = temptagsLoading.AsReadOnly();

            // loading IsVaalArea
            (var isvaalareaLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsLabyrinthAirlock
            (var islabyrinthairlockLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsLabyrinthArea
            (var islabyrinthareaLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading TwinnedFullClear_AchievementItemsKey
            (var twinnedfullclear_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Enter_AchievementItemsKey
            (var enter_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading TSIFile
            (var tsifileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown408
            (var unknown408Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading WaypointActivation_AchievementItemsKeys
            (var tempwaypointactivation_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var waypointactivation_achievementitemskeysLoading = tempwaypointactivation_achievementitemskeysLoading.AsReadOnly();

            // loading IsUniqueMapArea
            (var isuniquemapareaLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsLabyrinthBossArea
            (var islabyrinthbossareaLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading FirstEntry_NPCTextAudioKey
            (var firstentry_npctextaudiokeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading FirstEntry_SoundEffectsKey
            (var firstentry_soundeffectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading FirstEntry_NPCsKey
            (var firstentry_npcskeyLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown482
            (var unknown482Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EnvironmentsKey
            (var environmentskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown502
            (var unknown502Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown506
            (var unknown506Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown522
            (var unknown522Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown526
            (var unknown526Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown530
            (var unknown530Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown534
            (var unknown534Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown538
            (var unknown538Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown542
            (var unknown542Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown543
            (var unknown543Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown544
            (var unknown544Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown548
            (var unknown548Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown552
            (var unknown552Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown556
            (var tempunknown556Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown556Loading = tempunknown556Loading.AsReadOnly();

            // loading Unknown572
            (var unknown572Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown588
            (var unknown588Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown592
            (var unknown592Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown608
            (var unknown608Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WorldAreasDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Act = actLoading,
                IsTown = istownLoading,
                HasWaypoint = haswaypointLoading,
                Connections_WorldAreasKeys = connections_worldareaskeysLoading,
                AreaLevel = arealevelLoading,
                HASH16 = hash16Loading,
                Unknown46 = unknown46Loading,
                Unknown50 = unknown50Loading,
                LoadingScreen_DDSFile = loadingscreen_ddsfileLoading,
                Unknown62 = unknown62Loading,
                Unknown78 = unknown78Loading,
                Unknown94 = unknown94Loading,
                TopologiesKeys = topologieskeysLoading,
                ParentTown_WorldAreasKey = parenttown_worldareaskeyLoading,
                Unknown122 = unknown122Loading,
                Unknown126 = unknown126Loading,
                Unknown142 = unknown142Loading,
                Bosses_MonsterVarietiesKeys = bosses_monstervarietieskeysLoading,
                Monsters_MonsterVarietiesKeys = monsters_monstervarietieskeysLoading,
                SpawnWeight_TagsKeys = spawnweight_tagskeysLoading,
                SpawnWeight_Values = spawnweight_valuesLoading,
                IsMapArea = ismapareaLoading,
                FullClear_AchievementItemsKeys = fullclear_achievementitemskeysLoading,
                Unknown239 = unknown239Loading,
                AchievementItemsKey = achievementitemskeyLoading,
                ModsKeys = modskeysLoading,
                Unknown287 = unknown287Loading,
                VaalArea = vaalareaLoading,
                Unknown307 = unknown307Loading,
                MaxLevel = maxlevelLoading,
                AreaTypeTags = areatypetagsLoading,
                Unknown328 = unknown328Loading,
                IsHideout = ishideoutLoading,
                Inflection = inflectionLoading,
                Unknown341 = unknown341Loading,
                Unknown345 = unknown345Loading,
                Tags = tagsLoading,
                IsVaalArea = isvaalareaLoading,
                IsLabyrinthAirlock = islabyrinthairlockLoading,
                IsLabyrinthArea = islabyrinthareaLoading,
                TwinnedFullClear_AchievementItemsKey = twinnedfullclear_achievementitemskeyLoading,
                Enter_AchievementItemsKey = enter_achievementitemskeyLoading,
                TSIFile = tsifileLoading,
                Unknown408 = unknown408Loading,
                WaypointActivation_AchievementItemsKeys = waypointactivation_achievementitemskeysLoading,
                IsUniqueMapArea = isuniquemapareaLoading,
                IsLabyrinthBossArea = islabyrinthbossareaLoading,
                FirstEntry_NPCTextAudioKey = firstentry_npctextaudiokeyLoading,
                FirstEntry_SoundEffectsKey = firstentry_soundeffectskeyLoading,
                FirstEntry_NPCsKey = firstentry_npcskeyLoading,
                Unknown482 = unknown482Loading,
                EnvironmentsKey = environmentskeyLoading,
                Unknown502 = unknown502Loading,
                Unknown506 = unknown506Loading,
                Unknown522 = unknown522Loading,
                Unknown526 = unknown526Loading,
                Unknown530 = unknown530Loading,
                Unknown534 = unknown534Loading,
                Unknown538 = unknown538Loading,
                Unknown542 = unknown542Loading,
                Unknown543 = unknown543Loading,
                Unknown544 = unknown544Loading,
                Unknown548 = unknown548Loading,
                Unknown552 = unknown552Loading,
                Unknown556 = unknown556Loading,
                Unknown572 = unknown572Loading,
                Unknown588 = unknown588Loading,
                Unknown592 = unknown592Loading,
                Unknown608 = unknown608Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
