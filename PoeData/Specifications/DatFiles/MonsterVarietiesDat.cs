// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MonsterVarieties.dat data.
/// </summary>
public sealed partial class MonsterVarietiesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MonsterTypesKey.</summary>
    /// <remarks> references <see cref="MonsterTypesDat"/> on <see cref="Specification.LoadMonsterTypesDat"/> index.</remarks>
    public required int? MonsterTypesKey { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets ObjectSize.</summary>
    public required int ObjectSize { get; init; }

    /// <summary> Gets MinimumAttackDistance.</summary>
    public required int MinimumAttackDistance { get; init; }

    /// <summary> Gets MaximumAttackDistance.</summary>
    public required int MaximumAttackDistance { get; init; }

    /// <summary> Gets ACTFiles.</summary>
    public required ReadOnlyCollection<string> ACTFiles { get; init; }

    /// <summary> Gets AOFiles.</summary>
    public required ReadOnlyCollection<string> AOFiles { get; init; }

    /// <summary> Gets BaseMonsterTypeIndex.</summary>
    public required string BaseMonsterTypeIndex { get; init; }

    /// <summary> Gets ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKeys { get; init; }

    /// <summary> Gets Unknown96.</summary>
    public required int Unknown96 { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required string Unknown100 { get; init; }

    /// <summary> Gets Unknown108.</summary>
    public required string Unknown108 { get; init; }

    /// <summary> Gets ModelSizeMultiplier.</summary>
    public required int ModelSizeMultiplier { get; init; }

    /// <summary> Gets Unknown120.</summary>
    public required int Unknown120 { get; init; }

    /// <summary> Gets Unknown124.</summary>
    public required int Unknown124 { get; init; }

    /// <summary> Gets Unknown128.</summary>
    public required int Unknown128 { get; init; }

    /// <summary> Gets Unknown132.</summary>
    public required int Unknown132 { get; init; }

    /// <summary> Gets Unknown136.</summary>
    public required int Unknown136 { get; init; }

    /// <summary> Gets TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TagsKeys { get; init; }

    /// <summary> Gets ExperienceMultiplier.</summary>
    public required int ExperienceMultiplier { get; init; }

    /// <summary> Gets Unknown160.</summary>
    public required ReadOnlyCollection<int> Unknown160 { get; init; }

    /// <summary> Gets Unknown176.</summary>
    public required int Unknown176 { get; init; }

    /// <summary> Gets Unknown180.</summary>
    public required int Unknown180 { get; init; }

    /// <summary> Gets Unknown184.</summary>
    public required int Unknown184 { get; init; }

    /// <summary> Gets CriticalStrikeChance.</summary>
    public required int CriticalStrikeChance { get; init; }

    /// <summary> Gets Unknown192.</summary>
    public required int Unknown192 { get; init; }

    /// <summary> Gets GrantedEffectsKeys.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.LoadGrantedEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> GrantedEffectsKeys { get; init; }

    /// <summary> Gets AISFile.</summary>
    public required string AISFile { get; init; }

    /// <summary> Gets ModsKeys2.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKeys2 { get; init; }

    /// <summary> Gets Stance.</summary>
    public required string Stance { get; init; }

    /// <summary> Gets Unknown244.</summary>
    public required int? Unknown244 { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets DamageMultiplier.</summary>
    public required int DamageMultiplier { get; init; }

    /// <summary> Gets LifeMultiplier.</summary>
    public required int LifeMultiplier { get; init; }

    /// <summary> Gets AttackSpeed.</summary>
    public required int AttackSpeed { get; init; }

    /// <summary> Gets Weapon1_ItemVisualIdentityKeys.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Weapon1_ItemVisualIdentityKeys { get; init; }

    /// <summary> Gets Weapon2_ItemVisualIdentityKeys.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Weapon2_ItemVisualIdentityKeys { get; init; }

    /// <summary> Gets Back_ItemVisualIdentityKey.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required int? Back_ItemVisualIdentityKey { get; init; }

    /// <summary> Gets MainHand_ItemClassesKey.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.LoadItemClassesDat"/> index.</remarks>
    public required int? MainHand_ItemClassesKey { get; init; }

    /// <summary> Gets OffHand_ItemClassesKey.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.LoadItemClassesDat"/> index.</remarks>
    public required int? OffHand_ItemClassesKey { get; init; }

    /// <summary> Gets Helmet_ItemVisualIdentityKey.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required int? Helmet_ItemVisualIdentityKey { get; init; }

    /// <summary> Gets Unknown376.</summary>
    public required int Unknown376 { get; init; }

    /// <summary> Gets KillSpecificMonsterCount_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> KillSpecificMonsterCount_AchievementItemsKeys { get; init; }

    /// <summary> Gets Special_ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Special_ModsKeys { get; init; }

    /// <summary> Gets KillRare_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> KillRare_AchievementItemsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown428 is set.</summary>
    public required bool Unknown428 { get; init; }

    /// <summary> Gets Unknown429.</summary>
    public required int Unknown429 { get; init; }

    /// <summary> Gets Unknown433.</summary>
    public required int Unknown433 { get; init; }

    /// <summary> Gets Unknown437.</summary>
    public required int Unknown437 { get; init; }

    /// <summary> Gets Unknown441.</summary>
    public required int Unknown441 { get; init; }

    /// <summary> Gets Unknown445.</summary>
    public required int Unknown445 { get; init; }

    /// <summary> Gets Unknown449.</summary>
    public required int Unknown449 { get; init; }

    /// <summary> Gets Unknown453.</summary>
    public required int Unknown453 { get; init; }

    /// <summary> Gets a value indicating whether Unknown457 is set.</summary>
    public required bool Unknown457 { get; init; }

    /// <summary> Gets Unknown458.</summary>
    public required string Unknown458 { get; init; }

    /// <summary> Gets KillWhileOnslaughtIsActive_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? KillWhileOnslaughtIsActive_AchievementItemsKey { get; init; }

    /// <summary> Gets MonsterSegmentsKey.</summary>
    /// <remarks> references <see cref="MonsterSegmentsDat"/> on <see cref="Specification.LoadMonsterSegmentsDat"/> index.</remarks>
    public required int? MonsterSegmentsKey { get; init; }

    /// <summary> Gets MonsterArmoursKey.</summary>
    /// <remarks> references <see cref="MonsterArmoursDat"/> on <see cref="Specification.LoadMonsterArmoursDat"/> index.</remarks>
    public required int? MonsterArmoursKey { get; init; }

    /// <summary> Gets KillWhileTalismanIsActive_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? KillWhileTalismanIsActive_AchievementItemsKey { get; init; }

    /// <summary> Gets Part1_ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Part1_ModsKeys { get; init; }

    /// <summary> Gets Part2_ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Part2_ModsKeys { get; init; }

    /// <summary> Gets Endgame_ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Endgame_ModsKeys { get; init; }

    /// <summary> Gets Unknown578.</summary>
    public required int? Unknown578 { get; init; }

    /// <summary> Gets Unknown594.</summary>
    public required int Unknown594 { get; init; }

    /// <summary> Gets Unknown598.</summary>
    public required int Unknown598 { get; init; }

    /// <summary> Gets Unknown602.</summary>
    public required ReadOnlyCollection<int> Unknown602 { get; init; }

    /// <summary> Gets Unknown618.</summary>
    public required ReadOnlyCollection<int> Unknown618 { get; init; }

    /// <summary> Gets Unknown634.</summary>
    public required int Unknown634 { get; init; }

    /// <summary> Gets SinkAnimation_AOFile.</summary>
    public required string SinkAnimation_AOFile { get; init; }

    /// <summary> Gets a value indicating whether Unknown646 is set.</summary>
    public required bool Unknown646 { get; init; }

    /// <summary> Gets Unknown647.</summary>
    public required ReadOnlyCollection<int> Unknown647 { get; init; }

    /// <summary> Gets a value indicating whether Unknown663 is set.</summary>
    public required bool Unknown663 { get; init; }

    /// <summary> Gets a value indicating whether Unknown664 is set.</summary>
    public required bool Unknown664 { get; init; }

    /// <summary> Gets a value indicating whether Unknown665 is set.</summary>
    public required bool Unknown665 { get; init; }

    /// <summary> Gets Unknown666.</summary>
    public required int Unknown666 { get; init; }

    /// <summary> Gets Unknown670.</summary>
    public required int Unknown670 { get; init; }

    /// <summary> Gets Unknown674.</summary>
    public required float Unknown674 { get; init; }

    /// <summary> Gets Unknown678.</summary>
    public required int Unknown678 { get; init; }

    /// <summary> Gets EPKFile.</summary>
    public required string EPKFile { get; init; }

    /// <summary> Gets Unknown690.</summary>
    public required int Unknown690 { get; init; }

    /// <summary> Gets MonsterConditionalEffectPacksKey.</summary>
    /// <remarks> references <see cref="MonsterConditionalEffectPacksDat"/> on <see cref="Specification.LoadMonsterConditionalEffectPacksDat"/> index.</remarks>
    public required int? MonsterConditionalEffectPacksKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown710 is set.</summary>
    public required bool Unknown710 { get; init; }

    /// <summary> Gets a value indicating whether Unknown711 is set.</summary>
    public required bool Unknown711 { get; init; }

    /// <summary> Gets Unknown712.</summary>
    public required int Unknown712 { get; init; }

    /// <summary> Gets a value indicating whether Unknown716 is set.</summary>
    public required bool Unknown716 { get; init; }

    /// <summary> Gets Unknown717.</summary>
    public required int Unknown717 { get; init; }

    /// <summary> Gets Unknown721.</summary>
    public required int Unknown721 { get; init; }

    /// <summary> Gets Unknown725.</summary>
    public required int Unknown725 { get; init; }

    /// <summary> Gets Unknown729.</summary>
    public required int Unknown729 { get; init; }

    /// <summary> Gets Unknown733.</summary>
    public required int Unknown733 { get; init; }

    /// <summary> Gets Unknown737.</summary>
    public required int Unknown737 { get; init; }

    /// <summary> Gets Unknown741.</summary>
    public required ReadOnlyCollection<int> Unknown741 { get; init; }

    /// <summary> Gets Unknown757.</summary>
    public required int Unknown757 { get; init; }

    /// <summary> Gets Unknown761.</summary>
    public required int Unknown761 { get; init; }

    /// <summary> Gets Unknown765.</summary>
    public required int Unknown765 { get; init; }

    /// <summary> Gets Unknown769.</summary>
    public required int Unknown769 { get; init; }

    /// <summary> Gets Unknown773.</summary>
    public required int Unknown773 { get; init; }

    /// <summary> Gets a value indicating whether Unknown777 is set.</summary>
    public required bool Unknown777 { get; init; }

    /// <summary>
    /// Gets MonsterVarietiesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MonsterVarietiesDat.</returns>
    internal static MonsterVarietiesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MonsterVarieties.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterVarietiesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterTypesKey
            (var monstertypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ObjectSize
            (var objectsizeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinimumAttackDistance
            (var minimumattackdistanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaximumAttackDistance
            (var maximumattackdistanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ACTFiles
            (var tempactfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var actfilesLoading = tempactfilesLoading.AsReadOnly();

            // loading AOFiles
            (var tempaofilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var aofilesLoading = tempaofilesLoading.AsReadOnly();

            // loading BaseMonsterTypeIndex
            (var basemonstertypeindexLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ModsKeys
            (var tempmodskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeysLoading = tempmodskeysLoading.AsReadOnly();

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ModelSizeMultiplier
            (var modelsizemultiplierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown120
            (var unknown120Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown124
            (var unknown124Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown128
            (var unknown128Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown132
            (var unknown132Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown136
            (var unknown136Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TagsKeys
            (var temptagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tagskeysLoading = temptagskeysLoading.AsReadOnly();

            // loading ExperienceMultiplier
            (var experiencemultiplierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown160
            (var tempunknown160Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown160Loading = tempunknown160Loading.AsReadOnly();

            // loading Unknown176
            (var unknown176Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown180
            (var unknown180Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown184
            (var unknown184Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CriticalStrikeChance
            (var criticalstrikechanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown192
            (var unknown192Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading GrantedEffectsKeys
            (var tempgrantedeffectskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var grantedeffectskeysLoading = tempgrantedeffectskeysLoading.AsReadOnly();

            // loading AISFile
            (var aisfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ModsKeys2
            (var tempmodskeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeys2Loading = tempmodskeys2Loading.AsReadOnly();

            // loading Stance
            (var stanceLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown244
            (var unknown244Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DamageMultiplier
            (var damagemultiplierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LifeMultiplier
            (var lifemultiplierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AttackSpeed
            (var attackspeedLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Weapon1_ItemVisualIdentityKeys
            (var tempweapon1_itemvisualidentitykeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var weapon1_itemvisualidentitykeysLoading = tempweapon1_itemvisualidentitykeysLoading.AsReadOnly();

            // loading Weapon2_ItemVisualIdentityKeys
            (var tempweapon2_itemvisualidentitykeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var weapon2_itemvisualidentitykeysLoading = tempweapon2_itemvisualidentitykeysLoading.AsReadOnly();

            // loading Back_ItemVisualIdentityKey
            (var back_itemvisualidentitykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MainHand_ItemClassesKey
            (var mainhand_itemclasseskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading OffHand_ItemClassesKey
            (var offhand_itemclasseskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Helmet_ItemVisualIdentityKey
            (var helmet_itemvisualidentitykeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown376
            (var unknown376Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading KillSpecificMonsterCount_AchievementItemsKeys
            (var tempkillspecificmonstercount_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var killspecificmonstercount_achievementitemskeysLoading = tempkillspecificmonstercount_achievementitemskeysLoading.AsReadOnly();

            // loading Special_ModsKeys
            (var tempspecial_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var special_modskeysLoading = tempspecial_modskeysLoading.AsReadOnly();

            // loading KillRare_AchievementItemsKeys
            (var tempkillrare_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var killrare_achievementitemskeysLoading = tempkillrare_achievementitemskeysLoading.AsReadOnly();

            // loading Unknown428
            (var unknown428Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown429
            (var unknown429Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown433
            (var unknown433Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown437
            (var unknown437Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown441
            (var unknown441Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown445
            (var unknown445Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown449
            (var unknown449Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown453
            (var unknown453Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown457
            (var unknown457Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown458
            (var unknown458Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading KillWhileOnslaughtIsActive_AchievementItemsKey
            (var killwhileonslaughtisactive_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MonsterSegmentsKey
            (var monstersegmentskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MonsterArmoursKey
            (var monsterarmourskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading KillWhileTalismanIsActive_AchievementItemsKey
            (var killwhiletalismanisactive_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Part1_ModsKeys
            (var temppart1_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var part1_modskeysLoading = temppart1_modskeysLoading.AsReadOnly();

            // loading Part2_ModsKeys
            (var temppart2_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var part2_modskeysLoading = temppart2_modskeysLoading.AsReadOnly();

            // loading Endgame_ModsKeys
            (var tempendgame_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var endgame_modskeysLoading = tempendgame_modskeysLoading.AsReadOnly();

            // loading Unknown578
            (var unknown578Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown594
            (var unknown594Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown598
            (var unknown598Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown602
            (var tempunknown602Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown602Loading = tempunknown602Loading.AsReadOnly();

            // loading Unknown618
            (var tempunknown618Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown618Loading = tempunknown618Loading.AsReadOnly();

            // loading Unknown634
            (var unknown634Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SinkAnimation_AOFile
            (var sinkanimation_aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown646
            (var unknown646Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown647
            (var tempunknown647Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown647Loading = tempunknown647Loading.AsReadOnly();

            // loading Unknown663
            (var unknown663Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown664
            (var unknown664Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown665
            (var unknown665Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown666
            (var unknown666Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown670
            (var unknown670Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown674
            (var unknown674Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown678
            (var unknown678Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EPKFile
            (var epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown690
            (var unknown690Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterConditionalEffectPacksKey
            (var monsterconditionaleffectpackskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown710
            (var unknown710Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown711
            (var unknown711Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown712
            (var unknown712Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown716
            (var unknown716Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown717
            (var unknown717Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown721
            (var unknown721Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown725
            (var unknown725Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown729
            (var unknown729Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown733
            (var unknown733Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown737
            (var unknown737Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown741
            (var tempunknown741Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown741Loading = tempunknown741Loading.AsReadOnly();

            // loading Unknown757
            (var unknown757Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown761
            (var unknown761Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown765
            (var unknown765Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown769
            (var unknown769Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown773
            (var unknown773Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown777
            (var unknown777Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterVarietiesDat()
            {
                Id = idLoading,
                MonsterTypesKey = monstertypeskeyLoading,
                Unknown24 = unknown24Loading,
                ObjectSize = objectsizeLoading,
                MinimumAttackDistance = minimumattackdistanceLoading,
                MaximumAttackDistance = maximumattackdistanceLoading,
                ACTFiles = actfilesLoading,
                AOFiles = aofilesLoading,
                BaseMonsterTypeIndex = basemonstertypeindexLoading,
                ModsKeys = modskeysLoading,
                Unknown96 = unknown96Loading,
                Unknown100 = unknown100Loading,
                Unknown108 = unknown108Loading,
                ModelSizeMultiplier = modelsizemultiplierLoading,
                Unknown120 = unknown120Loading,
                Unknown124 = unknown124Loading,
                Unknown128 = unknown128Loading,
                Unknown132 = unknown132Loading,
                Unknown136 = unknown136Loading,
                TagsKeys = tagskeysLoading,
                ExperienceMultiplier = experiencemultiplierLoading,
                Unknown160 = unknown160Loading,
                Unknown176 = unknown176Loading,
                Unknown180 = unknown180Loading,
                Unknown184 = unknown184Loading,
                CriticalStrikeChance = criticalstrikechanceLoading,
                Unknown192 = unknown192Loading,
                GrantedEffectsKeys = grantedeffectskeysLoading,
                AISFile = aisfileLoading,
                ModsKeys2 = modskeys2Loading,
                Stance = stanceLoading,
                Unknown244 = unknown244Loading,
                Name = nameLoading,
                DamageMultiplier = damagemultiplierLoading,
                LifeMultiplier = lifemultiplierLoading,
                AttackSpeed = attackspeedLoading,
                Weapon1_ItemVisualIdentityKeys = weapon1_itemvisualidentitykeysLoading,
                Weapon2_ItemVisualIdentityKeys = weapon2_itemvisualidentitykeysLoading,
                Back_ItemVisualIdentityKey = back_itemvisualidentitykeyLoading,
                MainHand_ItemClassesKey = mainhand_itemclasseskeyLoading,
                OffHand_ItemClassesKey = offhand_itemclasseskeyLoading,
                Helmet_ItemVisualIdentityKey = helmet_itemvisualidentitykeyLoading,
                Unknown376 = unknown376Loading,
                KillSpecificMonsterCount_AchievementItemsKeys = killspecificmonstercount_achievementitemskeysLoading,
                Special_ModsKeys = special_modskeysLoading,
                KillRare_AchievementItemsKeys = killrare_achievementitemskeysLoading,
                Unknown428 = unknown428Loading,
                Unknown429 = unknown429Loading,
                Unknown433 = unknown433Loading,
                Unknown437 = unknown437Loading,
                Unknown441 = unknown441Loading,
                Unknown445 = unknown445Loading,
                Unknown449 = unknown449Loading,
                Unknown453 = unknown453Loading,
                Unknown457 = unknown457Loading,
                Unknown458 = unknown458Loading,
                KillWhileOnslaughtIsActive_AchievementItemsKey = killwhileonslaughtisactive_achievementitemskeyLoading,
                MonsterSegmentsKey = monstersegmentskeyLoading,
                MonsterArmoursKey = monsterarmourskeyLoading,
                KillWhileTalismanIsActive_AchievementItemsKey = killwhiletalismanisactive_achievementitemskeyLoading,
                Part1_ModsKeys = part1_modskeysLoading,
                Part2_ModsKeys = part2_modskeysLoading,
                Endgame_ModsKeys = endgame_modskeysLoading,
                Unknown578 = unknown578Loading,
                Unknown594 = unknown594Loading,
                Unknown598 = unknown598Loading,
                Unknown602 = unknown602Loading,
                Unknown618 = unknown618Loading,
                Unknown634 = unknown634Loading,
                SinkAnimation_AOFile = sinkanimation_aofileLoading,
                Unknown646 = unknown646Loading,
                Unknown647 = unknown647Loading,
                Unknown663 = unknown663Loading,
                Unknown664 = unknown664Loading,
                Unknown665 = unknown665Loading,
                Unknown666 = unknown666Loading,
                Unknown670 = unknown670Loading,
                Unknown674 = unknown674Loading,
                Unknown678 = unknown678Loading,
                EPKFile = epkfileLoading,
                Unknown690 = unknown690Loading,
                MonsterConditionalEffectPacksKey = monsterconditionaleffectpackskeyLoading,
                Unknown710 = unknown710Loading,
                Unknown711 = unknown711Loading,
                Unknown712 = unknown712Loading,
                Unknown716 = unknown716Loading,
                Unknown717 = unknown717Loading,
                Unknown721 = unknown721Loading,
                Unknown725 = unknown725Loading,
                Unknown729 = unknown729Loading,
                Unknown733 = unknown733Loading,
                Unknown737 = unknown737Loading,
                Unknown741 = unknown741Loading,
                Unknown757 = unknown757Loading,
                Unknown761 = unknown761Loading,
                Unknown765 = unknown765Loading,
                Unknown769 = unknown769Loading,
                Unknown773 = unknown773Loading,
                Unknown777 = unknown777Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
