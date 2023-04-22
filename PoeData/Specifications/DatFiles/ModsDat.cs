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

    /// <summary>
    /// Gets ModsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ModsDat.</returns>
    internal static ModsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Mods.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ModsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ModTypeKey
            (var modtypekeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKey1
            (var statskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatsKey2
            (var statskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatsKey3
            (var statskey3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading StatsKey4
            (var statskey4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Domain
            (var domainLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GenerationType
            (var generationtypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Families
            (var tempfamiliesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var familiesLoading = tempfamiliesLoading.AsReadOnly();

            // loading Stat1Min
            (var stat1minLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat1Max
            (var stat1maxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat2Min
            (var stat2minLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat2Max
            (var stat2maxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat3Min
            (var stat3minLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat3Max
            (var stat3maxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat4Min
            (var stat4minLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat4Max
            (var stat4maxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpawnWeight_TagsKeys
            (var tempspawnweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnweight_tagskeysLoading = tempspawnweight_tagskeysLoading.AsReadOnly();

            // loading SpawnWeight_Values
            (var tempspawnweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweight_valuesLoading = tempspawnweight_valuesLoading.AsReadOnly();

            // loading TagsKeys
            (var temptagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tagskeysLoading = temptagskeysLoading.AsReadOnly();

            // loading GrantedEffectsPerLevelKeys
            (var tempgrantedeffectsperlevelkeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var grantedeffectsperlevelkeysLoading = tempgrantedeffectsperlevelkeysLoading.AsReadOnly();

            // loading Unknown224
            (var tempunknown224Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown224Loading = tempunknown224Loading.AsReadOnly();

            // loading MonsterMetadata
            (var monstermetadataLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterKillAchievements
            (var tempmonsterkillachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monsterkillachievementsLoading = tempmonsterkillachievementsLoading.AsReadOnly();

            // loading ChestModType
            (var tempchestmodtypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var chestmodtypeLoading = tempchestmodtypeLoading.AsReadOnly();

            // loading Stat5Min
            (var stat5minLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat5Max
            (var stat5maxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKey5
            (var statskey5Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading FullAreaClear_AchievementItemsKey
            (var tempfullareaclear_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var fullareaclear_achievementitemskeyLoading = tempfullareaclear_achievementitemskeyLoading.AsReadOnly();

            // loading AchievementItemsKey
            (var tempachievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeyLoading = tempachievementitemskeyLoading.AsReadOnly();

            // loading GenerationWeight_TagsKeys
            (var tempgenerationweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var generationweight_tagskeysLoading = tempgenerationweight_tagskeysLoading.AsReadOnly();

            // loading GenerationWeight_Values
            (var tempgenerationweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var generationweight_valuesLoading = tempgenerationweight_valuesLoading.AsReadOnly();

            // loading ModifyMapsAchievements
            (var tempmodifymapsachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modifymapsachievementsLoading = tempmodifymapsachievementsLoading.AsReadOnly();

            // loading IsEssenceOnlyModifier
            (var isessenceonlymodifierLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Stat6Min
            (var stat6minLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat6Max
            (var stat6maxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKey6
            (var statskey6Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown413
            (var unknown413Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CraftingItemClassRestrictions
            (var tempcraftingitemclassrestrictionsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var craftingitemclassrestrictionsLoading = tempcraftingitemclassrestrictionsLoading.AsReadOnly();

            // loading MonsterOnDeath
            (var monsterondeathLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown438
            (var unknown438Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown442
            (var tempunknown442Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown442Loading = tempunknown442Loading.AsReadOnly();

            // loading Heist_SubStatValue1
            (var heist_substatvalue1Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Heist_SubStatValue2
            (var heist_substatvalue2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Heist_StatsKey0
            (var heist_statskey0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Heist_StatsKey1
            (var heist_statskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Heist_AddStatValue1
            (var heist_addstatvalue1Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Heist_AddStatValue2
            (var heist_addstatvalue2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading InfluenceTypes
            (var influencetypesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ImplicitTagsKeys
            (var tempimplicittagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var implicittagskeysLoading = tempimplicittagskeysLoading.AsReadOnly();

            // loading Unknown526
            (var unknown526Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown527
            (var unknown527Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown531
            (var unknown531Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown535
            (var unknown535Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown539
            (var unknown539Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown543
            (var unknown543Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown547
            (var unknown547Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown551
            (var unknown551Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown555
            (var unknown555Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown559
            (var unknown559Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown563
            (var unknown563Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown567
            (var unknown567Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown571
            (var unknown571Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown575
            (var unknown575Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown579
            (var unknown579Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown583
            (var unknown583Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown587
            (var unknown587Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffTemplate
            (var bufftemplateLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ArchnemesisMinionMod
            (var archnemesisminionmodLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading HASH32
            (var hash32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown619
            (var tempunknown619Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown619Loading = tempunknown619Loading.AsReadOnly();

            // loading Unknown635
            (var unknown635Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown639
            (var tempunknown639Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown639Loading = tempunknown639Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ModsDat()
            {
                Id = idLoading,
                HASH16 = hash16Loading,
                ModTypeKey = modtypekeyLoading,
                Level = levelLoading,
                StatsKey1 = statskey1Loading,
                StatsKey2 = statskey2Loading,
                StatsKey3 = statskey3Loading,
                StatsKey4 = statskey4Loading,
                Domain = domainLoading,
                Name = nameLoading,
                GenerationType = generationtypeLoading,
                Families = familiesLoading,
                Stat1Min = stat1minLoading,
                Stat1Max = stat1maxLoading,
                Stat2Min = stat2minLoading,
                Stat2Max = stat2maxLoading,
                Stat3Min = stat3minLoading,
                Stat3Max = stat3maxLoading,
                Stat4Min = stat4minLoading,
                Stat4Max = stat4maxLoading,
                SpawnWeight_TagsKeys = spawnweight_tagskeysLoading,
                SpawnWeight_Values = spawnweight_valuesLoading,
                TagsKeys = tagskeysLoading,
                GrantedEffectsPerLevelKeys = grantedeffectsperlevelkeysLoading,
                Unknown224 = unknown224Loading,
                MonsterMetadata = monstermetadataLoading,
                MonsterKillAchievements = monsterkillachievementsLoading,
                ChestModType = chestmodtypeLoading,
                Stat5Min = stat5minLoading,
                Stat5Max = stat5maxLoading,
                StatsKey5 = statskey5Loading,
                FullAreaClear_AchievementItemsKey = fullareaclear_achievementitemskeyLoading,
                AchievementItemsKey = achievementitemskeyLoading,
                GenerationWeight_TagsKeys = generationweight_tagskeysLoading,
                GenerationWeight_Values = generationweight_valuesLoading,
                ModifyMapsAchievements = modifymapsachievementsLoading,
                IsEssenceOnlyModifier = isessenceonlymodifierLoading,
                Stat6Min = stat6minLoading,
                Stat6Max = stat6maxLoading,
                StatsKey6 = statskey6Loading,
                MaxLevel = maxlevelLoading,
                Unknown413 = unknown413Loading,
                CraftingItemClassRestrictions = craftingitemclassrestrictionsLoading,
                MonsterOnDeath = monsterondeathLoading,
                Unknown438 = unknown438Loading,
                Unknown442 = unknown442Loading,
                Heist_SubStatValue1 = heist_substatvalue1Loading,
                Heist_SubStatValue2 = heist_substatvalue2Loading,
                Heist_StatsKey0 = heist_statskey0Loading,
                Heist_StatsKey1 = heist_statskey1Loading,
                Heist_AddStatValue1 = heist_addstatvalue1Loading,
                Heist_AddStatValue2 = heist_addstatvalue2Loading,
                InfluenceTypes = influencetypesLoading,
                ImplicitTagsKeys = implicittagskeysLoading,
                Unknown526 = unknown526Loading,
                Unknown527 = unknown527Loading,
                Unknown531 = unknown531Loading,
                Unknown535 = unknown535Loading,
                Unknown539 = unknown539Loading,
                Unknown543 = unknown543Loading,
                Unknown547 = unknown547Loading,
                Unknown551 = unknown551Loading,
                Unknown555 = unknown555Loading,
                Unknown559 = unknown559Loading,
                Unknown563 = unknown563Loading,
                Unknown567 = unknown567Loading,
                Unknown571 = unknown571Loading,
                Unknown575 = unknown575Loading,
                Unknown579 = unknown579Loading,
                Unknown583 = unknown583Loading,
                Unknown587 = unknown587Loading,
                BuffTemplate = bufftemplateLoading,
                ArchnemesisMinionMod = archnemesisminionmodLoading,
                HASH32 = hash32Loading,
                Unknown619 = unknown619Loading,
                Unknown635 = unknown635Loading,
                Unknown639 = unknown639Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
