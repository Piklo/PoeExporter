// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Specifications.Dat;
using Serilog;
using System.Collections.ObjectModel;

namespace PoeData.Specifications;

/// <summary>
/// Class containing all Path of Exile data.
/// </summary>
public sealed partial class Specification
{
    /// <summary>Gets dat file magic number.</summary>
    /// thats where the table ends?
    internal static byte[] DatFileMagicNumber { get; } = new byte[] { (byte)'\xBB', (byte)'\xbb', (byte)'\xBB', (byte)'\xbb', (byte)'\xBB', (byte)'\xbb', (byte)'\xBB', (byte)'\xbb' };

    /// <summary>Gets data loader.</summary>
    internal DataLoader DataLoader { get; }

    private ReadOnlyCollection<RogueExilesDat>? rogueexilesdat;
    private ReadOnlyCollection<RogueExileLifeScalingPerLevelDat>? rogueexilelifescalingperleveldat;
    private ReadOnlyCollection<ShrineBuffsDat>? shrinebuffsdat;
    private ReadOnlyCollection<ShrinesDat>? shrinesdat;
    private ReadOnlyCollection<ShrineSoundsDat>? shrinesoundsdat;
    private ReadOnlyCollection<StrongboxesDat>? strongboxesdat;
    private ReadOnlyCollection<InvasionMonsterRestrictionsDat>? invasionmonsterrestrictionsdat;
    private ReadOnlyCollection<InvasionMonstersPerAreaDat>? invasionmonstersperareadat;
    private ReadOnlyCollection<BeyondDemonsDat>? beyonddemonsdat;
    private ReadOnlyCollection<BeyondFactionsDat>? beyondfactionsdat;
    private ReadOnlyCollection<BloodlinesDat>? bloodlinesdat;
    private ReadOnlyCollection<TormentSpiritsDat>? tormentspiritsdat;
    private ReadOnlyCollection<DivinationCardArtDat>? divinationcardartdat;
    private ReadOnlyCollection<WarbandsGraphDat>? warbandsgraphdat;
    private ReadOnlyCollection<WarbandsMapGraphDat>? warbandsmapgraphdat;
    private ReadOnlyCollection<WarbandsPackMonstersDat>? warbandspackmonstersdat;
    private ReadOnlyCollection<WarbandsPackNumbersDat>? warbandspacknumbersdat;
    private ReadOnlyCollection<TalismanMonsterModsDat>? talismanmonstermodsdat;
    private ReadOnlyCollection<TalismanPacksDat>? talismanpacksdat;
    private ReadOnlyCollection<TalismansDat>? talismansdat;
    private ReadOnlyCollection<LabyrinthAreasDat>? labyrinthareasdat;
    private ReadOnlyCollection<LabyrinthBonusItemsDat>? labyrinthbonusitemsdat;
    private ReadOnlyCollection<LabyrinthExclusionGroupsDat>? labyrinthexclusiongroupsdat;
    private ReadOnlyCollection<LabyrinthIzaroChestsDat>? labyrinthizarochestsdat;
    private ReadOnlyCollection<LabyrinthNodeOverridesDat>? labyrinthnodeoverridesdat;
    private ReadOnlyCollection<LabyrinthRewardTypesDat>? labyrinthrewardtypesdat;
    private ReadOnlyCollection<LabyrinthsDat>? labyrinthsdat;
    private ReadOnlyCollection<LabyrinthSecretEffectsDat>? labyrinthsecreteffectsdat;
    private ReadOnlyCollection<LabyrinthSecretsDat>? labyrinthsecretsdat;
    private ReadOnlyCollection<LabyrinthSectionDat>? labyrinthsectiondat;
    private ReadOnlyCollection<LabyrinthSectionLayoutDat>? labyrinthsectionlayoutdat;
    private ReadOnlyCollection<LabyrinthTrialsDat>? labyrinthtrialsdat;
    private ReadOnlyCollection<LabyrinthTrinketsDat>? labyrinthtrinketsdat;
    private ReadOnlyCollection<PerandusBossesDat>? perandusbossesdat;
    private ReadOnlyCollection<PerandusChestsDat>? peranduschestsdat;
    private ReadOnlyCollection<PerandusDaemonsDat>? perandusdaemonsdat;
    private ReadOnlyCollection<PerandusGuardsDat>? perandusguardsdat;
    private ReadOnlyCollection<PropheciesDat>? propheciesdat;
    private ReadOnlyCollection<ProphecyChainDat>? prophecychaindat;
    private ReadOnlyCollection<ProphecyTypeDat>? prophecytypedat;
    private ReadOnlyCollection<ShaperGuardiansDat>? shaperguardiansdat;
    private ReadOnlyCollection<EssencesDat>? essencesdat;
    private ReadOnlyCollection<EssenceTypeDat>? essencetypedat;
    private ReadOnlyCollection<BreachBossLifeScalingPerLevelDat>? breachbosslifescalingperleveldat;
    private ReadOnlyCollection<BreachElementDat>? breachelementdat;
    private ReadOnlyCollection<BreachstoneUpgradesDat>? breachstoneupgradesdat;
    private ReadOnlyCollection<HarbingersDat>? harbingersdat;
    private ReadOnlyCollection<PantheonPanelLayoutDat>? pantheonpanellayoutdat;
    private ReadOnlyCollection<PantheonSoulsDat>? pantheonsoulsdat;
    private ReadOnlyCollection<AbyssObjectsDat>? abyssobjectsdat;
    private ReadOnlyCollection<ElderBossArenasDat>? elderbossarenasdat;
    private ReadOnlyCollection<ElderMapBossOverrideDat>? eldermapbossoverridedat;
    private ReadOnlyCollection<ElderGuardiansDat>? elderguardiansdat;
    private ReadOnlyCollection<BestiaryCapturableMonstersDat>? bestiarycapturablemonstersdat;
    private ReadOnlyCollection<BestiaryEncountersDat>? bestiaryencountersdat;
    private ReadOnlyCollection<BestiaryFamiliesDat>? bestiaryfamiliesdat;
    private ReadOnlyCollection<BestiaryGenusDat>? bestiarygenusdat;
    private ReadOnlyCollection<BestiaryGroupsDat>? bestiarygroupsdat;
    private ReadOnlyCollection<BestiaryNetsDat>? bestiarynetsdat;
    private ReadOnlyCollection<BestiaryRecipeComponentDat>? bestiaryrecipecomponentdat;
    private ReadOnlyCollection<BestiaryRecipeCategoriesDat>? bestiaryrecipecategoriesdat;
    private ReadOnlyCollection<BestiaryRecipesDat>? bestiaryrecipesdat;
    private ReadOnlyCollection<ArchitectLifeScalingPerLevelDat>? architectlifescalingperleveldat;
    private ReadOnlyCollection<IncursionArchitectDat>? incursionarchitectdat;
    private ReadOnlyCollection<IncursionBracketsDat>? incursionbracketsdat;
    private ReadOnlyCollection<IncursionChestRewardsDat>? incursionchestrewardsdat;
    private ReadOnlyCollection<IncursionChestsDat>? incursionchestsdat;
    private ReadOnlyCollection<IncursionRoomBossFightEventsDat>? incursionroombossfighteventsdat;
    private ReadOnlyCollection<IncursionRoomsDat>? incursionroomsdat;
    private ReadOnlyCollection<IncursionUniqueUpgradeComponentsDat>? incursionuniqueupgradecomponentsdat;
    private ReadOnlyCollection<DelveAzuriteShopDat>? delveazuriteshopdat;
    private ReadOnlyCollection<DelveBiomesDat>? delvebiomesdat;
    private ReadOnlyCollection<DelveCatchupDepthsDat>? delvecatchupdepthsdat;
    private ReadOnlyCollection<DelveCraftingModifierDescriptionsDat>? delvecraftingmodifierdescriptionsdat;
    private ReadOnlyCollection<DelveCraftingModifiersDat>? delvecraftingmodifiersdat;
    private ReadOnlyCollection<DelveCraftingTagsDat>? delvecraftingtagsdat;
    private ReadOnlyCollection<DelveDynamiteDat>? delvedynamitedat;
    private ReadOnlyCollection<DelveFeaturesDat>? delvefeaturesdat;
    private ReadOnlyCollection<DelveFlaresDat>? delveflaresdat;
    private ReadOnlyCollection<DelveLevelScalingDat>? delvelevelscalingdat;
    private ReadOnlyCollection<DelveMonsterSpawnersDat>? delvemonsterspawnersdat;
    private ReadOnlyCollection<DelveResourcePerLevelDat>? delveresourceperleveldat;
    private ReadOnlyCollection<DelveRewardTierConstantsDat>? delverewardtierconstantsdat;
    private ReadOnlyCollection<DelveRoomsDat>? delveroomsdat;
    private ReadOnlyCollection<DelveUpgradesDat>? delveupgradesdat;
    private ReadOnlyCollection<BetrayalChoiceActionsDat>? betrayalchoiceactionsdat;
    private ReadOnlyCollection<BetrayalChoicesDat>? betrayalchoicesdat;
    private ReadOnlyCollection<BetrayalDialogueDat>? betrayaldialoguedat;
    private ReadOnlyCollection<BetrayalFortsDat>? betrayalfortsdat;
    private ReadOnlyCollection<BetrayalJobsDat>? betrayaljobsdat;
    private ReadOnlyCollection<BetrayalRanksDat>? betrayalranksdat;
    private ReadOnlyCollection<BetrayalRelationshipStateDat>? betrayalrelationshipstatedat;
    private ReadOnlyCollection<BetrayalTargetJobAchievementsDat>? betrayaltargetjobachievementsdat;
    private ReadOnlyCollection<BetrayalTargetLifeScalingPerLevelDat>? betrayaltargetlifescalingperleveldat;
    private ReadOnlyCollection<BetrayalTargetsDat>? betrayaltargetsdat;
    private ReadOnlyCollection<BetrayalTraitorRewardsDat>? betrayaltraitorrewardsdat;
    private ReadOnlyCollection<BetrayalUpgradesDat>? betrayalupgradesdat;
    private ReadOnlyCollection<BetrayalWallLifeScalingPerLevelDat>? betrayalwalllifescalingperleveldat;
    private ReadOnlyCollection<SafehouseBYOCraftingDat>? safehousebyocraftingdat;
    private ReadOnlyCollection<SafehouseCraftingSpreeTypeDat>? safehousecraftingspreetypedat;
    private ReadOnlyCollection<SafehouseCraftingSpreeCurrenciesDat>? safehousecraftingspreecurrenciesdat;
    private ReadOnlyCollection<ScarabsDat>? scarabsdat;
    private ReadOnlyCollection<SynthesisAreasDat>? synthesisareasdat;
    private ReadOnlyCollection<SynthesisAreaSizeDat>? synthesisareasizedat;
    private ReadOnlyCollection<SynthesisBonusesDat>? synthesisbonusesdat;
    private ReadOnlyCollection<SynthesisBracketsDat>? synthesisbracketsdat;
    private ReadOnlyCollection<SynthesisFragmentDialogueDat>? synthesisfragmentdialoguedat;
    private ReadOnlyCollection<SynthesisGlobalModsDat>? synthesisglobalmodsdat;
    private ReadOnlyCollection<SynthesisMonsterExperiencePerLevelDat>? synthesismonsterexperienceperleveldat;
    private ReadOnlyCollection<SynthesisRewardCategoriesDat>? synthesisrewardcategoriesdat;
    private ReadOnlyCollection<SynthesisRewardTypesDat>? synthesisrewardtypesdat;
    private ReadOnlyCollection<IncubatorsDat>? incubatorsdat;
    private ReadOnlyCollection<LegionBalancePerLevelDat>? legionbalanceperleveldat;
    private ReadOnlyCollection<LegionChestTypesDat>? legionchesttypesdat;
    private ReadOnlyCollection<LegionChestCountsDat>? legionchestcountsdat;
    private ReadOnlyCollection<LegionChestsDat>? legionchestsdat;
    private ReadOnlyCollection<LegionFactionsDat>? legionfactionsdat;
    private ReadOnlyCollection<LegionMonsterCountsDat>? legionmonstercountsdat;
    private ReadOnlyCollection<LegionMonsterVarietiesDat>? legionmonstervarietiesdat;
    private ReadOnlyCollection<LegionRanksDat>? legionranksdat;
    private ReadOnlyCollection<LegionRewardTypeVisualsDat>? legionrewardtypevisualsdat;
    private ReadOnlyCollection<BlightBalancePerLevelDat>? blightbalanceperleveldat;
    private ReadOnlyCollection<BlightBossLifeScalingPerLevelDat>? blightbosslifescalingperleveldat;
    private ReadOnlyCollection<BlightChestTypesDat>? blightchesttypesdat;
    private ReadOnlyCollection<BlightCraftingItemsDat>? blightcraftingitemsdat;
    private ReadOnlyCollection<BlightCraftingRecipesDat>? blightcraftingrecipesdat;
    private ReadOnlyCollection<BlightCraftingResultsDat>? blightcraftingresultsdat;
    private ReadOnlyCollection<BlightCraftingTypesDat>? blightcraftingtypesdat;
    private ReadOnlyCollection<BlightCraftingUniquesDat>? blightcraftinguniquesdat;
    private ReadOnlyCollection<BlightedSporeAurasDat>? blightedsporeaurasdat;
    private ReadOnlyCollection<BlightEncounterTypesDat>? blightencountertypesdat;
    private ReadOnlyCollection<BlightEncounterWavesDat>? blightencounterwavesdat;
    private ReadOnlyCollection<BlightRewardTypesDat>? blightrewardtypesdat;
    private ReadOnlyCollection<BlightTopologiesDat>? blighttopologiesdat;
    private ReadOnlyCollection<BlightTopologyNodesDat>? blighttopologynodesdat;
    private ReadOnlyCollection<BlightTowerAurasDat>? blighttoweraurasdat;
    private ReadOnlyCollection<BlightTowersDat>? blighttowersdat;
    private ReadOnlyCollection<BlightTowersPerLevelDat>? blighttowersperleveldat;
    private ReadOnlyCollection<AtlasExileBossArenasDat>? atlasexilebossarenasdat;
    private ReadOnlyCollection<AtlasExileInfluenceDat>? atlasexileinfluencedat;
    private ReadOnlyCollection<AtlasExilesDat>? atlasexilesdat;
    private ReadOnlyCollection<AlternateQualityCurrencyDecayFactorsDat>? alternatequalitycurrencydecayfactorsdat;
    private ReadOnlyCollection<AlternateQualityTypesDat>? alternatequalitytypesdat;
    private ReadOnlyCollection<MetamorphLifeScalingPerLevelDat>? metamorphlifescalingperleveldat;
    private ReadOnlyCollection<MetamorphosisMetaMonstersDat>? metamorphosismetamonstersdat;
    private ReadOnlyCollection<MetamorphosisMetaSkillsDat>? metamorphosismetaskillsdat;
    private ReadOnlyCollection<MetamorphosisMetaSkillTypesDat>? metamorphosismetaskilltypesdat;
    private ReadOnlyCollection<MetamorphosisRewardTypeItemsClientDat>? metamorphosisrewardtypeitemsclientdat;
    private ReadOnlyCollection<MetamorphosisRewardTypesDat>? metamorphosisrewardtypesdat;
    private ReadOnlyCollection<MetamorphosisScalingDat>? metamorphosisscalingdat;
    private ReadOnlyCollection<AfflictionBalancePerLevelDat>? afflictionbalanceperleveldat;
    private ReadOnlyCollection<AfflictionEndgameWaveModsDat>? afflictionendgamewavemodsdat;
    private ReadOnlyCollection<AfflictionFixedModsDat>? afflictionfixedmodsdat;
    private ReadOnlyCollection<AfflictionRandomModCategoriesDat>? afflictionrandommodcategoriesdat;
    private ReadOnlyCollection<AfflictionRewardMapModsDat>? afflictionrewardmapmodsdat;
    private ReadOnlyCollection<AfflictionRewardTypeVisualsDat>? afflictionrewardtypevisualsdat;
    private ReadOnlyCollection<AfflictionSplitDemonsDat>? afflictionsplitdemonsdat;
    private ReadOnlyCollection<AfflictionStartDialogueDat>? afflictionstartdialoguedat;
    private ReadOnlyCollection<HarvestCraftOptionIconsDat>? harvestcraftoptioniconsdat;
    private ReadOnlyCollection<HarvestCraftOptionsDat>? harvestcraftoptionsdat;
    private ReadOnlyCollection<HarvestCraftTiersDat>? harvestcrafttiersdat;
    private ReadOnlyCollection<HarvestCraftFiltersDat>? harvestcraftfiltersdat;
    private ReadOnlyCollection<HarvestDurabilityDat>? harvestdurabilitydat;
    private ReadOnlyCollection<HarvestEncounterScalingDat>? harvestencounterscalingdat;
    private ReadOnlyCollection<HarvestInfrastructureDat>? harvestinfrastructuredat;
    private ReadOnlyCollection<HarvestObjectsDat>? harvestobjectsdat;
    private ReadOnlyCollection<HarvestPerLevelValuesDat>? harvestperlevelvaluesdat;
    private ReadOnlyCollection<HarvestPlantBoostersDat>? harvestplantboostersdat;
    private ReadOnlyCollection<HarvestLifeScalingPerLevelDat>? harvestlifescalingperleveldat;
    private ReadOnlyCollection<HarvestSeedsDat>? harvestseedsdat;
    private ReadOnlyCollection<HarvestSeedItemsDat>? harvestseeditemsdat;
    private ReadOnlyCollection<HarvestSeedTypesDat>? harvestseedtypesdat;
    private ReadOnlyCollection<HarvestSpecialCraftCostsDat>? harvestspecialcraftcostsdat;
    private ReadOnlyCollection<HarvestSpecialCraftOptionsDat>? harvestspecialcraftoptionsdat;
    private ReadOnlyCollection<HeistAreaFormationLayoutDat>? heistareaformationlayoutdat;
    private ReadOnlyCollection<HeistAreasDat>? heistareasdat;
    private ReadOnlyCollection<HeistBalancePerLevelDat>? heistbalanceperleveldat;
    private ReadOnlyCollection<HeistChestRewardTypesDat>? heistchestrewardtypesdat;
    private ReadOnlyCollection<HeistChestsDat>? heistchestsdat;
    private ReadOnlyCollection<HeistChokepointFormationDat>? heistchokepointformationdat;
    private ReadOnlyCollection<HeistConstantsDat>? heistconstantsdat;
    private ReadOnlyCollection<HeistContractsDat>? heistcontractsdat;
    private ReadOnlyCollection<HeistDoodadNPCsDat>? heistdoodadnpcsdat;
    private ReadOnlyCollection<HeistDoorsDat>? heistdoorsdat;
    private ReadOnlyCollection<HeistEquipmentDat>? heistequipmentdat;
    private ReadOnlyCollection<HeistGenerationDat>? heistgenerationdat;
    private ReadOnlyCollection<HeistIntroAreasDat>? heistintroareasdat;
    private ReadOnlyCollection<HeistJobsDat>? heistjobsdat;
    private ReadOnlyCollection<HeistJobsExperiencePerLevelDat>? heistjobsexperienceperleveldat;
    private ReadOnlyCollection<HeistLockTypeDat>? heistlocktypedat;
    private ReadOnlyCollection<HeistNPCAurasDat>? heistnpcaurasdat;
    private ReadOnlyCollection<HeistNPCBlueprintTypesDat>? heistnpcblueprinttypesdat;
    private ReadOnlyCollection<HeistNPCDialogueDat>? heistnpcdialoguedat;
    private ReadOnlyCollection<HeistNPCsDat>? heistnpcsdat;
    private ReadOnlyCollection<HeistNPCStatsDat>? heistnpcstatsdat;
    private ReadOnlyCollection<HeistObjectivesDat>? heistobjectivesdat;
    private ReadOnlyCollection<HeistObjectiveValueDescriptionsDat>? heistobjectivevaluedescriptionsdat;
    private ReadOnlyCollection<HeistPatrolPacksDat>? heistpatrolpacksdat;
    private ReadOnlyCollection<HeistQuestContractsDat>? heistquestcontractsdat;
    private ReadOnlyCollection<HeistRevealingNPCsDat>? heistrevealingnpcsdat;
    private ReadOnlyCollection<HeistRoomsDat>? heistroomsdat;
    private ReadOnlyCollection<HeistValueScalingDat>? heistvaluescalingdat;
    private ReadOnlyCollection<InfluenceModUpgradesDat>? influencemodupgradesdat;
    private ReadOnlyCollection<MavenDialogDat>? mavendialogdat;
    private ReadOnlyCollection<AtlasSkillGraphsDat>? atlasskillgraphsdat;
    private ReadOnlyCollection<MavenFightsDat>? mavenfightsdat;
    private ReadOnlyCollection<MavenJewelRadiusKeystonesDat>? mavenjewelradiuskeystonesdat;
    private ReadOnlyCollection<RitualBalancePerLevelDat>? ritualbalanceperleveldat;
    private ReadOnlyCollection<RitualConstantsDat>? ritualconstantsdat;
    private ReadOnlyCollection<RitualRuneTypesDat>? ritualrunetypesdat;
    private ReadOnlyCollection<RitualSetKillAchievementsDat>? ritualsetkillachievementsdat;
    private ReadOnlyCollection<RitualSpawnPatternsDat>? ritualspawnpatternsdat;
    private ReadOnlyCollection<UltimatumEncountersDat>? ultimatumencountersdat;
    private ReadOnlyCollection<UltimatumEncounterTypesDat>? ultimatumencountertypesdat;
    private ReadOnlyCollection<UltimatumItemisedRewardsDat>? ultimatumitemisedrewardsdat;
    private ReadOnlyCollection<UltimatumMapModifiersDat>? ultimatummapmodifiersdat;
    private ReadOnlyCollection<UltimatumModifiersDat>? ultimatummodifiersdat;
    private ReadOnlyCollection<UltimatumModifierTypesDat>? ultimatummodifiertypesdat;
    private ReadOnlyCollection<UltimatumTrialMasterAudioDat>? ultimatumtrialmasteraudiodat;
    private ReadOnlyCollection<ExpeditionAreasDat>? expeditionareasdat;
    private ReadOnlyCollection<ExpeditionBalancePerLevelDat>? expeditionbalanceperleveldat;
    private ReadOnlyCollection<ExpeditionCurrencyDat>? expeditioncurrencydat;
    private ReadOnlyCollection<ExpeditionDealsDat>? expeditiondealsdat;
    private ReadOnlyCollection<ExpeditionFactionsDat>? expeditionfactionsdat;
    private ReadOnlyCollection<ExpeditionMarkersCommonDat>? expeditionmarkerscommondat;
    private ReadOnlyCollection<ExpeditionNPCsDat>? expeditionnpcsdat;
    private ReadOnlyCollection<ExpeditionRelicModsDat>? expeditionrelicmodsdat;
    private ReadOnlyCollection<ExpeditionRelicsDat>? expeditionrelicsdat;
    private ReadOnlyCollection<ExpeditionStorageLayoutDat>? expeditionstoragelayoutdat;
    private ReadOnlyCollection<ExpeditionTerrainFeaturesDat>? expeditionterrainfeaturesdat;
    private ReadOnlyCollection<HellscapeAOReplacementsDat>? hellscapeaoreplacementsdat;
    private ReadOnlyCollection<HellscapeAreaPacksDat>? hellscapeareapacksdat;
    private ReadOnlyCollection<HellscapeExperienceLevelsDat>? hellscapeexperiencelevelsdat;
    private ReadOnlyCollection<HellscapeFactionsDat>? hellscapefactionsdat;
    private ReadOnlyCollection<HellscapeImmuneMonstersDat>? hellscapeimmunemonstersdat;
    private ReadOnlyCollection<HellscapeItemModificationTiersDat>? hellscapeitemmodificationtiersdat;
    private ReadOnlyCollection<HellscapeLifeScalingPerLevelDat>? hellscapelifescalingperleveldat;
    private ReadOnlyCollection<HellscapeModificationInventoryLayoutDat>? hellscapemodificationinventorylayoutdat;
    private ReadOnlyCollection<HellscapeModsDat>? hellscapemodsdat;
    private ReadOnlyCollection<HellscapeMonsterPacksDat>? hellscapemonsterpacksdat;
    private ReadOnlyCollection<HellscapePassivesDat>? hellscapepassivesdat;
    private ReadOnlyCollection<HellscapePassiveTreeDat>? hellscapepassivetreedat;
    private ReadOnlyCollection<ArchnemesisMetaRewardsDat>? archnemesismetarewardsdat;
    private ReadOnlyCollection<ArchnemesisModComboAchievementsDat>? archnemesismodcomboachievementsdat;
    private ReadOnlyCollection<ArchnemesisModsDat>? archnemesismodsdat;
    private ReadOnlyCollection<ArchnemesisModVisualsDat>? archnemesismodvisualsdat;
    private ReadOnlyCollection<ArchnemesisRecipesDat>? archnemesisrecipesdat;
    private ReadOnlyCollection<AtlasPrimordialAltarChoicesDat>? atlasprimordialaltarchoicesdat;
    private ReadOnlyCollection<AtlasPrimordialAltarChoiceTypesDat>? atlasprimordialaltarchoicetypesdat;
    private ReadOnlyCollection<AtlasPrimordialBossesDat>? atlasprimordialbossesdat;
    private ReadOnlyCollection<AtlasPrimordialBossInfluenceDat>? atlasprimordialbossinfluencedat;
    private ReadOnlyCollection<AtlasPrimordialBossOptionsDat>? atlasprimordialbossoptionsdat;
    private ReadOnlyCollection<PrimordialBossLifeScalingPerLevelDat>? primordialbosslifescalingperleveldat;
    private ReadOnlyCollection<AtlasUpgradesInventoryLayoutDat>? atlasupgradesinventorylayoutdat;
    private ReadOnlyCollection<AtlasPassiveSkillTreeGroupTypeDat>? atlaspassiveskilltreegrouptypedat;
    private ReadOnlyCollection<KiracLevelsDat>? kiraclevelsdat;
    private ReadOnlyCollection<ScoutingReportsDat>? scoutingreportsdat;
    private ReadOnlyCollection<DroneBaseTypesDat>? dronebasetypesdat;
    private ReadOnlyCollection<DroneTypesDat>? dronetypesdat;
    private ReadOnlyCollection<SentinelCraftingCurrencyDat>? sentinelcraftingcurrencydat;
    private ReadOnlyCollection<SentinelDroneInventoryLayoutDat>? sentineldroneinventorylayoutdat;
    private ReadOnlyCollection<SentinelPassivesDat>? sentinelpassivesdat;
    private ReadOnlyCollection<SentinelPassiveStatsDat>? sentinelpassivestatsdat;
    private ReadOnlyCollection<SentinelPassiveTypesDat>? sentinelpassivetypesdat;
    private ReadOnlyCollection<SentinelPowerExpLevelsDat>? sentinelpowerexplevelsdat;
    private ReadOnlyCollection<SentinelStorageLayoutDat>? sentinelstoragelayoutdat;
    private ReadOnlyCollection<SentinelTaggedMonsterStatsDat>? sentineltaggedmonsterstatsdat;
    private ReadOnlyCollection<ClientLakeDifficultyDat>? clientlakedifficultydat;
    private ReadOnlyCollection<LakeBossLifeScalingPerLevelDat>? lakebosslifescalingperleveldat;
    private ReadOnlyCollection<LakeMetaOptionsDat>? lakemetaoptionsdat;
    private ReadOnlyCollection<LakeMetaOptionsUnlockTextDat>? lakemetaoptionsunlocktextdat;
    private ReadOnlyCollection<LakeRoomCompletionDat>? lakeroomcompletiondat;
    private ReadOnlyCollection<LakeRoomsDat>? lakeroomsdat;
    private ReadOnlyCollection<AchievementItemRewardsDat>? achievementitemrewardsdat;
    private ReadOnlyCollection<AchievementItemsDat>? achievementitemsdat;
    private ReadOnlyCollection<AchievementsDat>? achievementsdat;
    private ReadOnlyCollection<AchievementSetRewardsDat>? achievementsetrewardsdat;
    private ReadOnlyCollection<AchievementSetsDisplayDat>? achievementsetsdisplaydat;
    private ReadOnlyCollection<ActiveSkillsDat>? activeskillsdat;
    private ReadOnlyCollection<ActiveSkillTypeDat>? activeskilltypedat;
    private ReadOnlyCollection<ActsDat>? actsdat;
    private ReadOnlyCollection<AddBuffToTargetVarietiesDat>? addbufftotargetvarietiesdat;
    private ReadOnlyCollection<AdditionalLifeScalingDat>? additionallifescalingdat;
    private ReadOnlyCollection<AdditionalMonsterPacksFromStatsDat>? additionalmonsterpacksfromstatsdat;
    private ReadOnlyCollection<AdvancedSkillsTutorialDat>? advancedskillstutorialdat;
    private ReadOnlyCollection<AegisVariationsDat>? aegisvariationsdat;
    private ReadOnlyCollection<AlternatePassiveAdditionsDat>? alternatepassiveadditionsdat;
    private ReadOnlyCollection<AlternatePassiveSkillsDat>? alternatepassiveskillsdat;
    private ReadOnlyCollection<AlternateSkillTargetingBehavioursDat>? alternateskilltargetingbehavioursdat;
    private ReadOnlyCollection<AlternateTreeVersionsDat>? alternatetreeversionsdat;
    private ReadOnlyCollection<AnimatedObjectFlagsDat>? animatedobjectflagsdat;
    private ReadOnlyCollection<AnimationDat>? animationdat;
    private ReadOnlyCollection<ApplyDamageFunctionsDat>? applydamagefunctionsdat;
    private ReadOnlyCollection<ArchetypeRewardsDat>? archetyperewardsdat;
    private ReadOnlyCollection<ArchetypesDat>? archetypesdat;
    private ReadOnlyCollection<AreaInfluenceDoodadsDat>? areainfluencedoodadsdat;
    private ReadOnlyCollection<AreaTransitionAnimationsDat>? areatransitionanimationsdat;
    private ReadOnlyCollection<AreaTransitionAnimationTypesDat>? areatransitionanimationtypesdat;
    private ReadOnlyCollection<AreaTransitionInfoDat>? areatransitioninfodat;
    private ReadOnlyCollection<ArmourTypesDat>? armourtypesdat;
    private ReadOnlyCollection<AscendancyDat>? ascendancydat;
    private ReadOnlyCollection<AtlasAwakeningStatsDat>? atlasawakeningstatsdat;
    private ReadOnlyCollection<AtlasBaseTypeDropsDat>? atlasbasetypedropsdat;
    private ReadOnlyCollection<AtlasFogDat>? atlasfogdat;
    private ReadOnlyCollection<AtlasInfluenceDataDat>? atlasinfluencedatadat;
    private ReadOnlyCollection<AtlasInfluenceOutcomesDat>? atlasinfluenceoutcomesdat;
    private ReadOnlyCollection<AtlasInfluenceSetsDat>? atlasinfluencesetsdat;
    private ReadOnlyCollection<AtlasModsDat>? atlasmodsdat;
    private ReadOnlyCollection<AtlasFavouredMapSlotsDat>? atlasfavouredmapslotsdat;
    private ReadOnlyCollection<AtlasNodeDat>? atlasnodedat;
    private ReadOnlyCollection<AtlasNodeDefinitionDat>? atlasnodedefinitiondat;
    private ReadOnlyCollection<AtlasPositionsDat>? atlaspositionsdat;
    private ReadOnlyCollection<AtlasRegionsDat>? atlasregionsdat;
    private ReadOnlyCollection<AtlasRegionUpgradesInventoryLayoutDat>? atlasregionupgradesinventorylayoutdat;
    private ReadOnlyCollection<AtlasRegionUpgradeRegionsDat>? atlasregionupgraderegionsdat;
    private ReadOnlyCollection<AtlasSectorDat>? atlassectordat;
    private ReadOnlyCollection<AwardDisplayDat>? awarddisplaydat;
    private ReadOnlyCollection<BackendErrorsDat>? backenderrorsdat;
    private ReadOnlyCollection<BaseItemTypesDat>? baseitemtypesdat;
    private ReadOnlyCollection<BindableVirtualKeysDat>? bindablevirtualkeysdat;
    private ReadOnlyCollection<BlightStashTabLayoutDat>? blightstashtablayoutdat;
    private ReadOnlyCollection<BloodTypesDat>? bloodtypesdat;
    private ReadOnlyCollection<BuffDefinitionsDat>? buffdefinitionsdat;
    private ReadOnlyCollection<BuffTemplatesDat>? bufftemplatesdat;
    private ReadOnlyCollection<BuffVisualOrbArtDat>? buffvisualorbartdat;
    private ReadOnlyCollection<BuffVisualOrbsDat>? buffvisualorbsdat;
    private ReadOnlyCollection<BuffVisualOrbTypesDat>? buffvisualorbtypesdat;
    private ReadOnlyCollection<BuffVisualsDat>? buffvisualsdat;
    private ReadOnlyCollection<BuffVisualsArtVariationsDat>? buffvisualsartvariationsdat;
    private ReadOnlyCollection<BuffVisualSetEntriesDat>? buffvisualsetentriesdat;
    private ReadOnlyCollection<CharacterAudioEventsDat>? characteraudioeventsdat;
    private ReadOnlyCollection<CharacterEventTextAudioDat>? charactereventtextaudiodat;
    private ReadOnlyCollection<CharacterPanelDescriptionModesDat>? characterpaneldescriptionmodesdat;
    private ReadOnlyCollection<CharacterPanelStatsDat>? characterpanelstatsdat;
    private ReadOnlyCollection<CharacterPanelTabsDat>? characterpaneltabsdat;
    private ReadOnlyCollection<CharactersDat>? charactersdat;
    private ReadOnlyCollection<CharacterStartQuestStateDat>? characterstartqueststatedat;
    private ReadOnlyCollection<CharacterStartStatesDat>? characterstartstatesdat;
    private ReadOnlyCollection<CharacterStartStateSetDat>? characterstartstatesetdat;
    private ReadOnlyCollection<CharacterTextAudioDat>? charactertextaudiodat;
    private ReadOnlyCollection<ChatIconsDat>? chaticonsdat;
    private ReadOnlyCollection<ChestClustersDat>? chestclustersdat;
    private ReadOnlyCollection<ChestEffectsDat>? chesteffectsdat;
    private ReadOnlyCollection<ChestsDat>? chestsdat;
    private ReadOnlyCollection<ClientStringsDat>? clientstringsdat;
    private ReadOnlyCollection<ClientLeagueActionDat>? clientleagueactiondat;
    private ReadOnlyCollection<CloneShotDat>? cloneshotdat;
    private ReadOnlyCollection<ColoursDat>? coloursdat;
    private ReadOnlyCollection<CommandsDat>? commandsdat;
    private ReadOnlyCollection<ComponentAttributeRequirementsDat>? componentattributerequirementsdat;
    private ReadOnlyCollection<ComponentChargesDat>? componentchargesdat;
    private ReadOnlyCollection<CoreLeaguesDat>? coreleaguesdat;
    private ReadOnlyCollection<CostTypesDat>? costtypesdat;
    private ReadOnlyCollection<CraftingBenchOptionsDat>? craftingbenchoptionsdat;
    private ReadOnlyCollection<CraftingBenchSortCategoriesDat>? craftingbenchsortcategoriesdat;
    private ReadOnlyCollection<CraftingBenchUnlockCategoriesDat>? craftingbenchunlockcategoriesdat;
    private ReadOnlyCollection<CraftingItemClassCategoriesDat>? craftingitemclasscategoriesdat;
    private ReadOnlyCollection<CurrencyItemsDat>? currencyitemsdat;
    private ReadOnlyCollection<CurrencyStashTabLayoutDat>? currencystashtablayoutdat;
    private ReadOnlyCollection<CustomLeagueModsDat>? customleaguemodsdat;
    private ReadOnlyCollection<DaemonSpawningDataDat>? daemonspawningdatadat;
    private ReadOnlyCollection<DamageHitEffectsDat>? damagehiteffectsdat;
    private ReadOnlyCollection<DamageParticleEffectsDat>? damageparticleeffectsdat;
    private ReadOnlyCollection<DancesDat>? dancesdat;
    private ReadOnlyCollection<DaressoPitFightsDat>? daressopitfightsdat;
    private ReadOnlyCollection<DefaultMonsterStatsDat>? defaultmonsterstatsdat;
    private ReadOnlyCollection<DeliriumStashTabLayoutDat>? deliriumstashtablayoutdat;
    private ReadOnlyCollection<DelveStashTabLayoutDat>? delvestashtablayoutdat;
    private ReadOnlyCollection<DescentExilesDat>? descentexilesdat;
    private ReadOnlyCollection<DescentRewardChestsDat>? descentrewardchestsdat;
    private ReadOnlyCollection<DescentStarterChestDat>? descentstarterchestdat;
    private ReadOnlyCollection<DialogueEventDat>? dialogueeventdat;
    private ReadOnlyCollection<DisplayMinionMonsterTypeDat>? displayminionmonstertypedat;
    private ReadOnlyCollection<DivinationCardStashTabLayoutDat>? divinationcardstashtablayoutdat;
    private ReadOnlyCollection<DoorsDat>? doorsdat;
    private ReadOnlyCollection<DropEffectsDat>? dropeffectsdat;
    private ReadOnlyCollection<DropPoolDat>? droppooldat;
    private ReadOnlyCollection<EclipseModsDat>? eclipsemodsdat;
    private ReadOnlyCollection<EffectDrivenSkillDat>? effectdrivenskilldat;
    private ReadOnlyCollection<EffectivenessCostConstantsDat>? effectivenesscostconstantsdat;
    private ReadOnlyCollection<EinharMissionsDat>? einharmissionsdat;
    private ReadOnlyCollection<EinharPackFallbackDat>? einharpackfallbackdat;
    private ReadOnlyCollection<EndlessLedgeChestsDat>? endlessledgechestsdat;
    private ReadOnlyCollection<EnvironmentsDat>? environmentsdat;
    private ReadOnlyCollection<EnvironmentTransitionsDat>? environmenttransitionsdat;
    private ReadOnlyCollection<EssenceStashTabLayoutDat>? essencestashtablayoutdat;
    private ReadOnlyCollection<EventSeasonDat>? eventseasondat;
    private ReadOnlyCollection<EventSeasonRewardsDat>? eventseasonrewardsdat;
    private ReadOnlyCollection<EvergreenAchievementsDat>? evergreenachievementsdat;
    private ReadOnlyCollection<ExecuteGEALDat>? executegealdat;
    private ReadOnlyCollection<ExpandingPulseDat>? expandingpulsedat;
    private ReadOnlyCollection<ExperienceLevelsDat>? experiencelevelsdat;
    private ReadOnlyCollection<ExplodingStormBuffsDat>? explodingstormbuffsdat;
    private ReadOnlyCollection<ExtraTerrainFeaturesDat>? extraterrainfeaturesdat;
    private ReadOnlyCollection<FixedHideoutDoodadTypesDat>? fixedhideoutdoodadtypesdat;
    private ReadOnlyCollection<FixedMissionsDat>? fixedmissionsdat;
    private ReadOnlyCollection<FlasksDat>? flasksdat;
    private ReadOnlyCollection<FlavourTextDat>? flavourtextdat;
    private ReadOnlyCollection<FootprintsDat>? footprintsdat;
    private ReadOnlyCollection<FootstepAudioDat>? footstepaudiodat;
    private ReadOnlyCollection<FragmentStashTabLayoutDat>? fragmentstashtablayoutdat;
    private ReadOnlyCollection<GameConstantsDat>? gameconstantsdat;
    private ReadOnlyCollection<GameObjectTasksDat>? gameobjecttasksdat;
    private ReadOnlyCollection<GamepadButtonDat>? gamepadbuttondat;
    private ReadOnlyCollection<GamepadTypeDat>? gamepadtypedat;
    private ReadOnlyCollection<GameStatsDat>? gamestatsdat;
    private ReadOnlyCollection<GemTagsDat>? gemtagsdat;
    private ReadOnlyCollection<GenericBuffAurasDat>? genericbuffaurasdat;
    private ReadOnlyCollection<GenericLeagueRewardTypesDat>? genericleaguerewardtypesdat;
    private ReadOnlyCollection<GenericLeagueRewardTypeVisualsDat>? genericleaguerewardtypevisualsdat;
    private ReadOnlyCollection<GeometryAttackDat>? geometryattackdat;
    private ReadOnlyCollection<GeometryChannelDat>? geometrychanneldat;
    private ReadOnlyCollection<GeometryProjectilesDat>? geometryprojectilesdat;
    private ReadOnlyCollection<GeometryTriggerDat>? geometrytriggerdat;
    private ReadOnlyCollection<GiftWrapArtVariationsDat>? giftwrapartvariationsdat;
    private ReadOnlyCollection<GlobalAudioConfigDat>? globalaudioconfigdat;
    private ReadOnlyCollection<GrandmastersDat>? grandmastersdat;
    private ReadOnlyCollection<GrantedEffectQualityStatsDat>? grantedeffectqualitystatsdat;
    private ReadOnlyCollection<GrantedEffectQualityTypesDat>? grantedeffectqualitytypesdat;
    private ReadOnlyCollection<GrantedEffectsDat>? grantedeffectsdat;
    private ReadOnlyCollection<GrantedEffectsPerLevelDat>? grantedeffectsperleveldat;
    private ReadOnlyCollection<GrantedEffectStatSetsDat>? grantedeffectstatsetsdat;
    private ReadOnlyCollection<GrantedEffectStatSetsPerLevelDat>? grantedeffectstatsetsperleveldat;
    private ReadOnlyCollection<GroundEffectsDat>? groundeffectsdat;
    private ReadOnlyCollection<GroundEffectTypesDat>? groundeffecttypesdat;
    private ReadOnlyCollection<HarvestStorageLayoutDat>? harveststoragelayoutdat;
    private ReadOnlyCollection<HeistStorageLayoutDat>? heiststoragelayoutdat;
    private ReadOnlyCollection<HideoutDoodadsDat>? hideoutdoodadsdat;
    private ReadOnlyCollection<HideoutDoodadCategoryDat>? hideoutdoodadcategorydat;
    private ReadOnlyCollection<HideoutDoodadTagsDat>? hideoutdoodadtagsdat;
    private ReadOnlyCollection<HideoutNPCsDat>? hideoutnpcsdat;
    private ReadOnlyCollection<HideoutRarityDat>? hideoutraritydat;
    private ReadOnlyCollection<HideoutsDat>? hideoutsdat;
    private ReadOnlyCollection<ImpactSoundDataDat>? impactsounddatadat;
    private ReadOnlyCollection<IndexableSupportGemsDat>? indexablesupportgemsdat;
    private ReadOnlyCollection<InfluenceExaltsDat>? influenceexaltsdat;
    private ReadOnlyCollection<InfluenceTagsDat>? influencetagsdat;
    private ReadOnlyCollection<InventoriesDat>? inventoriesdat;
    private ReadOnlyCollection<ItemClassCategoriesDat>? itemclasscategoriesdat;
    private ReadOnlyCollection<ItemClassesDat>? itemclassesdat;
    private ReadOnlyCollection<ItemCostPerLevelDat>? itemcostperleveldat;
    private ReadOnlyCollection<ItemCostsDat>? itemcostsdat;
    private ReadOnlyCollection<ItemFrameTypeDat>? itemframetypedat;
    private ReadOnlyCollection<ItemExperiencePerLevelDat>? itemexperienceperleveldat;
    private ReadOnlyCollection<ItemisedVisualEffectDat>? itemisedvisualeffectdat;
    private ReadOnlyCollection<ItemNoteCodeDat>? itemnotecodedat;
    private ReadOnlyCollection<ItemShopTypeDat>? itemshoptypedat;
    private ReadOnlyCollection<ItemStancesDat>? itemstancesdat;
    private ReadOnlyCollection<ItemThemesDat>? itemthemesdat;
    private ReadOnlyCollection<ItemVisualEffectDat>? itemvisualeffectdat;
    private ReadOnlyCollection<ItemVisualHeldBodyModelDat>? itemvisualheldbodymodeldat;
    private ReadOnlyCollection<ItemVisualIdentityDat>? itemvisualidentitydat;
    private ReadOnlyCollection<JobAssassinationSpawnerGroupsDat>? jobassassinationspawnergroupsdat;
    private ReadOnlyCollection<JobRaidBracketsDat>? jobraidbracketsdat;
    private ReadOnlyCollection<KillstreakThresholdsDat>? killstreakthresholdsdat;
    private ReadOnlyCollection<LeagueFlagDat>? leagueflagdat;
    private ReadOnlyCollection<LeagueInfoDat>? leagueinfodat;
    private ReadOnlyCollection<LeagueProgressQuestFlagsDat>? leagueprogressquestflagsdat;
    private ReadOnlyCollection<LeagueStaticRewardsDat>? leaguestaticrewardsdat;
    private ReadOnlyCollection<LevelRelativePlayerScalingDat>? levelrelativeplayerscalingdat;
    private ReadOnlyCollection<MagicMonsterLifeScalingPerLevelDat>? magicmonsterlifescalingperleveldat;
    private ReadOnlyCollection<MapCompletionAchievementsDat>? mapcompletionachievementsdat;
    private ReadOnlyCollection<MapConnectionsDat>? mapconnectionsdat;
    private ReadOnlyCollection<MapCreationInformationDat>? mapcreationinformationdat;
    private ReadOnlyCollection<MapDeviceRecipesDat>? mapdevicerecipesdat;
    private ReadOnlyCollection<MapDevicesDat>? mapdevicesdat;
    private ReadOnlyCollection<MapFragmentModsDat>? mapfragmentmodsdat;
    private ReadOnlyCollection<MapInhabitantsDat>? mapinhabitantsdat;
    private ReadOnlyCollection<MapPinsDat>? mappinsdat;
    private ReadOnlyCollection<MapPurchaseCostsDat>? mappurchasecostsdat;
    private ReadOnlyCollection<MapsDat>? mapsdat;
    private ReadOnlyCollection<MapSeriesDat>? mapseriesdat;
    private ReadOnlyCollection<MapSeriesTiersDat>? mapseriestiersdat;
    private ReadOnlyCollection<MapStashSpecialTypeEntriesDat>? mapstashspecialtypeentriesdat;
    private ReadOnlyCollection<MapStashUniqueMapInfoDat>? mapstashuniquemapinfodat;
    private ReadOnlyCollection<MapStatConditionsDat>? mapstatconditionsdat;
    private ReadOnlyCollection<MapTierAchievementsDat>? maptierachievementsdat;
    private ReadOnlyCollection<MapTiersDat>? maptiersdat;
    private ReadOnlyCollection<MasterHideoutLevelsDat>? masterhideoutlevelsdat;
    private ReadOnlyCollection<MeleeDat>? meleedat;
    private ReadOnlyCollection<MeleeTrailsDat>? meleetrailsdat;
    private ReadOnlyCollection<MetamorphosisStashTabLayoutDat>? metamorphosisstashtablayoutdat;
    private ReadOnlyCollection<MicroMigrationDataDat>? micromigrationdatadat;
    private ReadOnlyCollection<MicrotransactionCategoryDat>? microtransactioncategorydat;
    private ReadOnlyCollection<MicrotransactionCharacterPortraitVariationsDat>? microtransactioncharacterportraitvariationsdat;
    private ReadOnlyCollection<MicrotransactionCombineFormulaDat>? microtransactioncombineformuladat;
    private ReadOnlyCollection<MicrotransactionCursorVariationsDat>? microtransactioncursorvariationsdat;
    private ReadOnlyCollection<MicrotransactionFireworksVariationsDat>? microtransactionfireworksvariationsdat;
    private ReadOnlyCollection<MicrotransactionGemCategoryDat>? microtransactiongemcategorydat;
    private ReadOnlyCollection<MicrotransactionPeriodicCharacterEffectVariationsDat>? microtransactionperiodiccharactereffectvariationsdat;
    private ReadOnlyCollection<MicrotransactionPortalVariationsDat>? microtransactionportalvariationsdat;
    private ReadOnlyCollection<MicrotransactionRarityDisplayDat>? microtransactionraritydisplaydat;
    private ReadOnlyCollection<MicrotransactionRecycleOutcomesDat>? microtransactionrecycleoutcomesdat;
    private ReadOnlyCollection<MicrotransactionRecycleSalvageValuesDat>? microtransactionrecyclesalvagevaluesdat;
    private ReadOnlyCollection<MicrotransactionSlotDat>? microtransactionslotdat;
    private ReadOnlyCollection<MicrotransactionSocialFrameVariationsDat>? microtransactionsocialframevariationsdat;
    private ReadOnlyCollection<MinimapIconsDat>? minimapiconsdat;
    private ReadOnlyCollection<MiniQuestStatesDat>? miniqueststatesdat;
    private ReadOnlyCollection<MiscAnimatedDat>? miscanimateddat;
    private ReadOnlyCollection<MiscAnimatedArtVariationsDat>? miscanimatedartvariationsdat;
    private ReadOnlyCollection<MiscBeamsDat>? miscbeamsdat;
    private ReadOnlyCollection<MiscBeamsArtVariationsDat>? miscbeamsartvariationsdat;
    private ReadOnlyCollection<MiscEffectPacksDat>? misceffectpacksdat;
    private ReadOnlyCollection<MiscEffectPacksArtVariationsDat>? misceffectpacksartvariationsdat;
    private ReadOnlyCollection<MiscObjectsDat>? miscobjectsdat;
    private ReadOnlyCollection<MiscObjectsArtVariationsDat>? miscobjectsartvariationsdat;
    private ReadOnlyCollection<MissionFavourPerLevelDat>? missionfavourperleveldat;
    private ReadOnlyCollection<MissionTimerTypesDat>? missiontimertypesdat;
    private ReadOnlyCollection<MissionTransitionTilesDat>? missiontransitiontilesdat;
    private ReadOnlyCollection<ModEffectStatsDat>? modeffectstatsdat;
    private ReadOnlyCollection<ModEquivalenciesDat>? modequivalenciesdat;
    private ReadOnlyCollection<ModFamilyDat>? modfamilydat;
    private ReadOnlyCollection<ModsDat>? modsdat;
    private ReadOnlyCollection<ModSellPriceTypesDat>? modsellpricetypesdat;
    private ReadOnlyCollection<ModTypeDat>? modtypedat;
    private ReadOnlyCollection<MonsterArmoursDat>? monsterarmoursdat;
    private ReadOnlyCollection<MonsterBonusesDat>? monsterbonusesdat;
    private ReadOnlyCollection<MonsterConditionalEffectPacksDat>? monsterconditionaleffectpacksdat;
    private ReadOnlyCollection<MonsterConditionsDat>? monsterconditionsdat;
    private ReadOnlyCollection<MonsterDeathAchievementsDat>? monsterdeathachievementsdat;
    private ReadOnlyCollection<MonsterDeathConditionsDat>? monsterdeathconditionsdat;
    private ReadOnlyCollection<MonsterGroupEntriesDat>? monstergroupentriesdat;
    private ReadOnlyCollection<MonsterHeightBracketsDat>? monsterheightbracketsdat;
    private ReadOnlyCollection<MonsterHeightsDat>? monsterheightsdat;
    private ReadOnlyCollection<MonsterMapBossDifficultyDat>? monstermapbossdifficultydat;
    private ReadOnlyCollection<MonsterMapDifficultyDat>? monstermapdifficultydat;
    private ReadOnlyCollection<MonsterMortarDat>? monstermortardat;
    private ReadOnlyCollection<MonsterPackCountsDat>? monsterpackcountsdat;
    private ReadOnlyCollection<MonsterPackEntriesDat>? monsterpackentriesdat;
    private ReadOnlyCollection<MonsterPacksDat>? monsterpacksdat;
    private ReadOnlyCollection<MonsterProjectileAttackDat>? monsterprojectileattackdat;
    private ReadOnlyCollection<MonsterProjectileSpellDat>? monsterprojectilespelldat;
    private ReadOnlyCollection<MonsterResistancesDat>? monsterresistancesdat;
    private ReadOnlyCollection<MonsterSegmentsDat>? monstersegmentsdat;
    private ReadOnlyCollection<MonsterSpawnerGroupsDat>? monsterspawnergroupsdat;
    private ReadOnlyCollection<MonsterSpawnerGroupsPerLevelDat>? monsterspawnergroupsperleveldat;
    private ReadOnlyCollection<MonsterSpawnerOverridesDat>? monsterspawneroverridesdat;
    private ReadOnlyCollection<MonsterTypesDat>? monstertypesdat;
    private ReadOnlyCollection<MonsterVarietiesDat>? monstervarietiesdat;
    private ReadOnlyCollection<MonsterVarietiesArtVariationsDat>? monstervarietiesartvariationsdat;
    private ReadOnlyCollection<MouseCursorSizeSettingsDat>? mousecursorsizesettingsdat;
    private ReadOnlyCollection<MoveDaemonDat>? movedaemondat;
    private ReadOnlyCollection<MTXSetBonusDat>? mtxsetbonusdat;
    private ReadOnlyCollection<MultiPartAchievementAreasDat>? multipartachievementareasdat;
    private ReadOnlyCollection<MultiPartAchievementConditionsDat>? multipartachievementconditionsdat;
    private ReadOnlyCollection<MultiPartAchievementsDat>? multipartachievementsdat;
    private ReadOnlyCollection<MusicDat>? musicdat;
    private ReadOnlyCollection<MusicCategoriesDat>? musiccategoriesdat;
    private ReadOnlyCollection<MysteryBoxesDat>? mysteryboxesdat;
    private ReadOnlyCollection<NearbyMonsterConditionsDat>? nearbymonsterconditionsdat;
    private ReadOnlyCollection<NetTiersDat>? nettiersdat;
    private ReadOnlyCollection<NotificationsDat>? notificationsdat;
    private ReadOnlyCollection<NPCAudioDat>? npcaudiodat;
    private ReadOnlyCollection<NPCConversationsDat>? npcconversationsdat;
    private ReadOnlyCollection<NPCDialogueStylesDat>? npcdialoguestylesdat;
    private ReadOnlyCollection<NPCFollowerVariationsDat>? npcfollowervariationsdat;
    private ReadOnlyCollection<NPCMasterDat>? npcmasterdat;
    private ReadOnlyCollection<NPCPortraitsDat>? npcportraitsdat;
    private ReadOnlyCollection<NPCsDat>? npcsdat;
    private ReadOnlyCollection<NPCShopDat>? npcshopdat;
    private ReadOnlyCollection<NPCShopsDat>? npcshopsdat;
    private ReadOnlyCollection<NPCTalkDat>? npctalkdat;
    private ReadOnlyCollection<NPCTalkCategoryDat>? npctalkcategorydat;
    private ReadOnlyCollection<NPCTalkConsoleQuickActionsDat>? npctalkconsolequickactionsdat;
    private ReadOnlyCollection<NPCTextAudioDat>? npctextaudiodat;
    private ReadOnlyCollection<OnKillAchievementsDat>? onkillachievementsdat;
    private ReadOnlyCollection<PackFormationDat>? packformationdat;
    private ReadOnlyCollection<PassiveJewelRadiiDat>? passivejewelradiidat;
    private ReadOnlyCollection<PassiveJewelSlotsDat>? passivejewelslotsdat;
    private ReadOnlyCollection<PassiveSkillFilterCatagoriesDat>? passiveskillfiltercatagoriesdat;
    private ReadOnlyCollection<PassiveSkillFilterOptionsDat>? passiveskillfilteroptionsdat;
    private ReadOnlyCollection<PassiveSkillMasteryGroupsDat>? passiveskillmasterygroupsdat;
    private ReadOnlyCollection<PassiveSkillMasteryEffectsDat>? passiveskillmasteryeffectsdat;
    private ReadOnlyCollection<PassiveSkillsDat>? passiveskillsdat;
    private ReadOnlyCollection<PassiveSkillStatCategoriesDat>? passiveskillstatcategoriesdat;
    private ReadOnlyCollection<PassiveSkillTreesDat>? passiveskilltreesdat;
    private ReadOnlyCollection<PassiveSkillTreeTutorialDat>? passiveskilltreetutorialdat;
    private ReadOnlyCollection<PassiveSkillTreeUIArtDat>? passiveskilltreeuiartdat;
    private ReadOnlyCollection<PassiveTreeExpansionJewelsDat>? passivetreeexpansionjewelsdat;
    private ReadOnlyCollection<PassiveTreeExpansionJewelSizesDat>? passivetreeexpansionjewelsizesdat;
    private ReadOnlyCollection<PassiveTreeExpansionSkillsDat>? passivetreeexpansionskillsdat;
    private ReadOnlyCollection<PassiveTreeExpansionSpecialSkillsDat>? passivetreeexpansionspecialskillsdat;
    private ReadOnlyCollection<PCBangRewardMicrosDat>? pcbangrewardmicrosdat;
    private ReadOnlyCollection<PetDat>? petdat;
    private ReadOnlyCollection<PlayerConditionsDat>? playerconditionsdat;
    private ReadOnlyCollection<PlayerTradeWhisperFormatsDat>? playertradewhisperformatsdat;
    private ReadOnlyCollection<PreloadGroupsDat>? preloadgroupsdat;
    private ReadOnlyCollection<ProjectilesDat>? projectilesdat;
    private ReadOnlyCollection<ProjectilesArtVariationsDat>? projectilesartvariationsdat;
    private ReadOnlyCollection<ProjectileVariationsDat>? projectilevariationsdat;
    private ReadOnlyCollection<PVPTypesDat>? pvptypesdat;
    private ReadOnlyCollection<QuestDat>? questdat;
    private ReadOnlyCollection<QuestAchievementsDat>? questachievementsdat;
    private ReadOnlyCollection<QuestFlagsDat>? questflagsdat;
    private ReadOnlyCollection<QuestItemsDat>? questitemsdat;
    private ReadOnlyCollection<QuestRewardOffersDat>? questrewardoffersdat;
    private ReadOnlyCollection<QuestRewardsDat>? questrewardsdat;
    private ReadOnlyCollection<QuestStatesDat>? queststatesdat;
    private ReadOnlyCollection<QuestStaticRewardsDat>? queststaticrewardsdat;
    private ReadOnlyCollection<QuestTrackerGroupDat>? questtrackergroupdat;
    private ReadOnlyCollection<QuestTypeDat>? questtypedat;
    private ReadOnlyCollection<RacesDat>? racesdat;
    private ReadOnlyCollection<RaceTimesDat>? racetimesdat;
    private ReadOnlyCollection<RareMonsterLifeScalingPerLevelDat>? raremonsterlifescalingperleveldat;
    private ReadOnlyCollection<RarityDat>? raritydat;
    private ReadOnlyCollection<RealmsDat>? realmsdat;
    private ReadOnlyCollection<RecipeUnlockDisplayDat>? recipeunlockdisplaydat;
    private ReadOnlyCollection<RecipeUnlockObjectsDat>? recipeunlockobjectsdat;
    private ReadOnlyCollection<ReminderTextDat>? remindertextdat;
    private ReadOnlyCollection<RulesetsDat>? rulesetsdat;
    private ReadOnlyCollection<RunicCirclesDat>? runiccirclesdat;
    private ReadOnlyCollection<SalvageBoxesDat>? salvageboxesdat;
    private ReadOnlyCollection<SessionQuestFlagsDat>? sessionquestflagsdat;
    private ReadOnlyCollection<ShieldTypesDat>? shieldtypesdat;
    private ReadOnlyCollection<ShopCategoryDat>? shopcategorydat;
    private ReadOnlyCollection<ShopCountryDat>? shopcountrydat;
    private ReadOnlyCollection<ShopCurrencyDat>? shopcurrencydat;
    private ReadOnlyCollection<ShopPaymentPackageDat>? shoppaymentpackagedat;
    private ReadOnlyCollection<ShopPaymentPackagePriceDat>? shoppaymentpackagepricedat;
    private ReadOnlyCollection<ShopRegionDat>? shopregiondat;
    private ReadOnlyCollection<ShopTagDat>? shoptagdat;
    private ReadOnlyCollection<ShopTokenDat>? shoptokendat;
    private ReadOnlyCollection<SigilDisplayDat>? sigildisplaydat;
    private ReadOnlyCollection<SingleGroundLaserDat>? singlegroundlaserdat;
    private ReadOnlyCollection<SkillArtVariationsDat>? skillartvariationsdat;
    private ReadOnlyCollection<SkillGemInfoDat>? skillgeminfodat;
    private ReadOnlyCollection<SkillGemsDat>? skillgemsdat;
    private ReadOnlyCollection<SkillMineVariationsDat>? skillminevariationsdat;
    private ReadOnlyCollection<SkillMorphDisplayDat>? skillmorphdisplaydat;
    private ReadOnlyCollection<SkillSurgeEffectsDat>? skillsurgeeffectsdat;
    private ReadOnlyCollection<SkillTotemVariationsDat>? skilltotemvariationsdat;
    private ReadOnlyCollection<SkillTrapVariationsDat>? skilltrapvariationsdat;
    private ReadOnlyCollection<SocketNotchesDat>? socketnotchesdat;
    private ReadOnlyCollection<SoundEffectsDat>? soundeffectsdat;
    private ReadOnlyCollection<SpawnAdditionalChestsOrClustersDat>? spawnadditionalchestsorclustersdat;
    private ReadOnlyCollection<SpawnObjectDat>? spawnobjectdat;
    private ReadOnlyCollection<SpecialRoomsDat>? specialroomsdat;
    private ReadOnlyCollection<SpecialTilesDat>? specialtilesdat;
    private ReadOnlyCollection<SpectreOverridesDat>? spectreoverridesdat;
    private ReadOnlyCollection<StartingPassiveSkillsDat>? startingpassiveskillsdat;
    private ReadOnlyCollection<StashTabAffinitiesDat>? stashtabaffinitiesdat;
    private ReadOnlyCollection<StashTypeDat>? stashtypedat;
    private ReadOnlyCollection<StatDescriptionFunctionsDat>? statdescriptionfunctionsdat;
    private ReadOnlyCollection<StatsAffectingGenerationDat>? statsaffectinggenerationdat;
    private ReadOnlyCollection<StatsDat>? statsdat;
    private ReadOnlyCollection<StrDexIntMissionExtraRequirementDat>? strdexintmissionextrarequirementdat;
    private ReadOnlyCollection<StrDexIntMissionsDat>? strdexintmissionsdat;
    private ReadOnlyCollection<SuicideExplosionDat>? suicideexplosiondat;
    private ReadOnlyCollection<SummonedSpecificBarrelsDat>? summonedspecificbarrelsdat;
    private ReadOnlyCollection<SummonedSpecificMonstersDat>? summonedspecificmonstersdat;
    private ReadOnlyCollection<SummonedSpecificMonstersOnDeathDat>? summonedspecificmonstersondeathdat;
    private ReadOnlyCollection<SupporterPackSetsDat>? supporterpacksetsdat;
    private ReadOnlyCollection<SurgeEffectsDat>? surgeeffectsdat;
    private ReadOnlyCollection<SurgeTypesDat>? surgetypesdat;
    private ReadOnlyCollection<TableChargeDat>? tablechargedat;
    private ReadOnlyCollection<TableMonsterSpawnersDat>? tablemonsterspawnersdat;
    private ReadOnlyCollection<TagsDat>? tagsdat;
    private ReadOnlyCollection<TalkingPetAudioEventsDat>? talkingpetaudioeventsdat;
    private ReadOnlyCollection<TalkingPetNPCAudioDat>? talkingpetnpcaudiodat;
    private ReadOnlyCollection<TalkingPetsDat>? talkingpetsdat;
    private ReadOnlyCollection<TencentAutoLootPetCurrenciesDat>? tencentautolootpetcurrenciesdat;
    private ReadOnlyCollection<TencentAutoLootPetCurrenciesExcludableDat>? tencentautolootpetcurrenciesexcludabledat;
    private ReadOnlyCollection<TerrainPluginsDat>? terrainpluginsdat;
    private ReadOnlyCollection<TipsDat>? tipsdat;
    private ReadOnlyCollection<TopologiesDat>? topologiesdat;
    private ReadOnlyCollection<TradeMarketCategoryDat>? trademarketcategorydat;
    private ReadOnlyCollection<TradeMarketCategoryGroupsDat>? trademarketcategorygroupsdat;
    private ReadOnlyCollection<TradeMarketCategoryListAllClassDat>? trademarketcategorylistallclassdat;
    private ReadOnlyCollection<TradeMarketIndexItemAsDat>? trademarketindexitemasdat;
    private ReadOnlyCollection<TreasureHunterMissionsDat>? treasurehuntermissionsdat;
    private ReadOnlyCollection<TriggerBeamDat>? triggerbeamdat;
    private ReadOnlyCollection<TriggerSpawnersDat>? triggerspawnersdat;
    private ReadOnlyCollection<TutorialDat>? tutorialdat;
    private ReadOnlyCollection<UITalkTextDat>? uitalktextdat;
    private ReadOnlyCollection<UniqueChestsDat>? uniquechestsdat;
    private ReadOnlyCollection<UniqueJewelLimitsDat>? uniquejewellimitsdat;
    private ReadOnlyCollection<UniqueMapInfoDat>? uniquemapinfodat;
    private ReadOnlyCollection<UniqueMapsDat>? uniquemapsdat;
    private ReadOnlyCollection<UniqueStashLayoutDat>? uniquestashlayoutdat;
    private ReadOnlyCollection<UniqueStashTypesDat>? uniquestashtypesdat;
    private ReadOnlyCollection<VirtualStatContextFlagsDat>? virtualstatcontextflagsdat;
    private ReadOnlyCollection<VoteStateDat>? votestatedat;
    private ReadOnlyCollection<VoteTypeDat>? votetypedat;
    private ReadOnlyCollection<WeaponClassesDat>? weaponclassesdat;
    private ReadOnlyCollection<WeaponImpactSoundDataDat>? weaponimpactsounddatadat;
    private ReadOnlyCollection<WeaponTypesDat>? weapontypesdat;
    private ReadOnlyCollection<WindowCursorsDat>? windowcursorsdat;
    private ReadOnlyCollection<WordsDat>? wordsdat;
    private ReadOnlyCollection<WorldAreasDat>? worldareasdat;
    private ReadOnlyCollection<WorldAreaLeagueChancesDat>? worldarealeaguechancesdat;
    private ReadOnlyCollection<WorldPopupIconTypesDat>? worldpopupicontypesdat;
    private ReadOnlyCollection<ZanaLevelsDat>? zanalevelsdat;

    /// <summary>
    /// Initializes a new instance of the <see cref="Specification"/> class.
    /// </summary>
    /// <param name="config">Contains config data.</param>
    /// <param name="logger">Contains logger used through the application.</param>
    public Specification(IConfig config, ILogger logger)
    {
        DataLoader = new DataLoader(config, logger);
    }

    /// <summary>
    /// Gets RogueExilesDat data.
    /// </summary>
    /// <returns>readonly collection of RogueExilesDat.</returns>
    public ReadOnlyCollection<RogueExilesDat> GetRogueExilesDat()
    {
        rogueexilesdat ??= RogueExilesDat.Load(this).AsReadOnly();

        return rogueexilesdat;
    }

    /// <summary>
    /// Gets RogueExileLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of RogueExileLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<RogueExileLifeScalingPerLevelDat> GetRogueExileLifeScalingPerLevelDat()
    {
        rogueexilelifescalingperleveldat ??= RogueExileLifeScalingPerLevelDat.Load(this).AsReadOnly();

        return rogueexilelifescalingperleveldat;
    }

    /// <summary>
    /// Gets ShrineBuffsDat data.
    /// </summary>
    /// <returns>readonly collection of ShrineBuffsDat.</returns>
    public ReadOnlyCollection<ShrineBuffsDat> GetShrineBuffsDat()
    {
        shrinebuffsdat ??= ShrineBuffsDat.Load(this).AsReadOnly();

        return shrinebuffsdat;
    }

    /// <summary>
    /// Gets ShrinesDat data.
    /// </summary>
    /// <returns>readonly collection of ShrinesDat.</returns>
    public ReadOnlyCollection<ShrinesDat> GetShrinesDat()
    {
        shrinesdat ??= ShrinesDat.Load(this).AsReadOnly();

        return shrinesdat;
    }

    /// <summary>
    /// Gets ShrineSoundsDat data.
    /// </summary>
    /// <returns>readonly collection of ShrineSoundsDat.</returns>
    public ReadOnlyCollection<ShrineSoundsDat> GetShrineSoundsDat()
    {
        shrinesoundsdat ??= ShrineSoundsDat.Load(this).AsReadOnly();

        return shrinesoundsdat;
    }

    /// <summary>
    /// Gets StrongboxesDat data.
    /// </summary>
    /// <returns>readonly collection of StrongboxesDat.</returns>
    public ReadOnlyCollection<StrongboxesDat> GetStrongboxesDat()
    {
        strongboxesdat ??= StrongboxesDat.Load(this).AsReadOnly();

        return strongboxesdat;
    }

    /// <summary>
    /// Gets InvasionMonsterRestrictionsDat data.
    /// </summary>
    /// <returns>readonly collection of InvasionMonsterRestrictionsDat.</returns>
    public ReadOnlyCollection<InvasionMonsterRestrictionsDat> GetInvasionMonsterRestrictionsDat()
    {
        invasionmonsterrestrictionsdat ??= InvasionMonsterRestrictionsDat.Load(this).AsReadOnly();

        return invasionmonsterrestrictionsdat;
    }

    /// <summary>
    /// Gets InvasionMonstersPerAreaDat data.
    /// </summary>
    /// <returns>readonly collection of InvasionMonstersPerAreaDat.</returns>
    public ReadOnlyCollection<InvasionMonstersPerAreaDat> GetInvasionMonstersPerAreaDat()
    {
        invasionmonstersperareadat ??= InvasionMonstersPerAreaDat.Load(this).AsReadOnly();

        return invasionmonstersperareadat;
    }

    /// <summary>
    /// Gets BeyondDemonsDat data.
    /// </summary>
    /// <returns>readonly collection of BeyondDemonsDat.</returns>
    public ReadOnlyCollection<BeyondDemonsDat> GetBeyondDemonsDat()
    {
        beyonddemonsdat ??= BeyondDemonsDat.Load(this).AsReadOnly();

        return beyonddemonsdat;
    }

    /// <summary>
    /// Gets BeyondFactionsDat data.
    /// </summary>
    /// <returns>readonly collection of BeyondFactionsDat.</returns>
    public ReadOnlyCollection<BeyondFactionsDat> GetBeyondFactionsDat()
    {
        beyondfactionsdat ??= BeyondFactionsDat.Load(this).AsReadOnly();

        return beyondfactionsdat;
    }

    /// <summary>
    /// Gets BloodlinesDat data.
    /// </summary>
    /// <returns>readonly collection of BloodlinesDat.</returns>
    public ReadOnlyCollection<BloodlinesDat> GetBloodlinesDat()
    {
        bloodlinesdat ??= BloodlinesDat.Load(this).AsReadOnly();

        return bloodlinesdat;
    }

    /// <summary>
    /// Gets TormentSpiritsDat data.
    /// </summary>
    /// <returns>readonly collection of TormentSpiritsDat.</returns>
    public ReadOnlyCollection<TormentSpiritsDat> GetTormentSpiritsDat()
    {
        tormentspiritsdat ??= TormentSpiritsDat.Load(this).AsReadOnly();

        return tormentspiritsdat;
    }

    /// <summary>
    /// Gets DivinationCardArtDat data.
    /// </summary>
    /// <returns>readonly collection of DivinationCardArtDat.</returns>
    public ReadOnlyCollection<DivinationCardArtDat> GetDivinationCardArtDat()
    {
        divinationcardartdat ??= DivinationCardArtDat.Load(this).AsReadOnly();

        return divinationcardartdat;
    }

    /// <summary>
    /// Gets WarbandsGraphDat data.
    /// </summary>
    /// <returns>readonly collection of WarbandsGraphDat.</returns>
    public ReadOnlyCollection<WarbandsGraphDat> GetWarbandsGraphDat()
    {
        warbandsgraphdat ??= WarbandsGraphDat.Load(this).AsReadOnly();

        return warbandsgraphdat;
    }

    /// <summary>
    /// Gets WarbandsMapGraphDat data.
    /// </summary>
    /// <returns>readonly collection of WarbandsMapGraphDat.</returns>
    public ReadOnlyCollection<WarbandsMapGraphDat> GetWarbandsMapGraphDat()
    {
        warbandsmapgraphdat ??= WarbandsMapGraphDat.Load(this).AsReadOnly();

        return warbandsmapgraphdat;
    }

    /// <summary>
    /// Gets WarbandsPackMonstersDat data.
    /// </summary>
    /// <returns>readonly collection of WarbandsPackMonstersDat.</returns>
    public ReadOnlyCollection<WarbandsPackMonstersDat> GetWarbandsPackMonstersDat()
    {
        warbandspackmonstersdat ??= WarbandsPackMonstersDat.Load(this).AsReadOnly();

        return warbandspackmonstersdat;
    }

    /// <summary>
    /// Gets WarbandsPackNumbersDat data.
    /// </summary>
    /// <returns>readonly collection of WarbandsPackNumbersDat.</returns>
    public ReadOnlyCollection<WarbandsPackNumbersDat> GetWarbandsPackNumbersDat()
    {
        warbandspacknumbersdat ??= WarbandsPackNumbersDat.Load(this).AsReadOnly();

        return warbandspacknumbersdat;
    }

    /// <summary>
    /// Gets TalismanMonsterModsDat data.
    /// </summary>
    /// <returns>readonly collection of TalismanMonsterModsDat.</returns>
    public ReadOnlyCollection<TalismanMonsterModsDat> GetTalismanMonsterModsDat()
    {
        talismanmonstermodsdat ??= TalismanMonsterModsDat.Load(this).AsReadOnly();

        return talismanmonstermodsdat;
    }

    /// <summary>
    /// Gets TalismanPacksDat data.
    /// </summary>
    /// <returns>readonly collection of TalismanPacksDat.</returns>
    public ReadOnlyCollection<TalismanPacksDat> GetTalismanPacksDat()
    {
        talismanpacksdat ??= TalismanPacksDat.Load(this).AsReadOnly();

        return talismanpacksdat;
    }

    /// <summary>
    /// Gets TalismansDat data.
    /// </summary>
    /// <returns>readonly collection of TalismansDat.</returns>
    public ReadOnlyCollection<TalismansDat> GetTalismansDat()
    {
        talismansdat ??= TalismansDat.Load(this).AsReadOnly();

        return talismansdat;
    }

    /// <summary>
    /// Gets LabyrinthAreasDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthAreasDat.</returns>
    public ReadOnlyCollection<LabyrinthAreasDat> GetLabyrinthAreasDat()
    {
        labyrinthareasdat ??= LabyrinthAreasDat.Load(this).AsReadOnly();

        return labyrinthareasdat;
    }

    /// <summary>
    /// Gets LabyrinthBonusItemsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthBonusItemsDat.</returns>
    public ReadOnlyCollection<LabyrinthBonusItemsDat> GetLabyrinthBonusItemsDat()
    {
        labyrinthbonusitemsdat ??= LabyrinthBonusItemsDat.Load(this).AsReadOnly();

        return labyrinthbonusitemsdat;
    }

    /// <summary>
    /// Gets LabyrinthExclusionGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthExclusionGroupsDat.</returns>
    public ReadOnlyCollection<LabyrinthExclusionGroupsDat> GetLabyrinthExclusionGroupsDat()
    {
        labyrinthexclusiongroupsdat ??= LabyrinthExclusionGroupsDat.Load(this).AsReadOnly();

        return labyrinthexclusiongroupsdat;
    }

    /// <summary>
    /// Gets LabyrinthIzaroChestsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthIzaroChestsDat.</returns>
    public ReadOnlyCollection<LabyrinthIzaroChestsDat> GetLabyrinthIzaroChestsDat()
    {
        labyrinthizarochestsdat ??= LabyrinthIzaroChestsDat.Load(this).AsReadOnly();

        return labyrinthizarochestsdat;
    }

    /// <summary>
    /// Gets LabyrinthNodeOverridesDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthNodeOverridesDat.</returns>
    public ReadOnlyCollection<LabyrinthNodeOverridesDat> GetLabyrinthNodeOverridesDat()
    {
        labyrinthnodeoverridesdat ??= LabyrinthNodeOverridesDat.Load(this).AsReadOnly();

        return labyrinthnodeoverridesdat;
    }

    /// <summary>
    /// Gets LabyrinthRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthRewardTypesDat.</returns>
    public ReadOnlyCollection<LabyrinthRewardTypesDat> GetLabyrinthRewardTypesDat()
    {
        labyrinthrewardtypesdat ??= LabyrinthRewardTypesDat.Load(this).AsReadOnly();

        return labyrinthrewardtypesdat;
    }

    /// <summary>
    /// Gets LabyrinthsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthsDat.</returns>
    public ReadOnlyCollection<LabyrinthsDat> GetLabyrinthsDat()
    {
        labyrinthsdat ??= LabyrinthsDat.Load(this).AsReadOnly();

        return labyrinthsdat;
    }

    /// <summary>
    /// Gets LabyrinthSecretEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthSecretEffectsDat.</returns>
    public ReadOnlyCollection<LabyrinthSecretEffectsDat> GetLabyrinthSecretEffectsDat()
    {
        labyrinthsecreteffectsdat ??= LabyrinthSecretEffectsDat.Load(this).AsReadOnly();

        return labyrinthsecreteffectsdat;
    }

    /// <summary>
    /// Gets LabyrinthSecretsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthSecretsDat.</returns>
    public ReadOnlyCollection<LabyrinthSecretsDat> GetLabyrinthSecretsDat()
    {
        labyrinthsecretsdat ??= LabyrinthSecretsDat.Load(this).AsReadOnly();

        return labyrinthsecretsdat;
    }

    /// <summary>
    /// Gets LabyrinthSectionDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthSectionDat.</returns>
    public ReadOnlyCollection<LabyrinthSectionDat> GetLabyrinthSectionDat()
    {
        labyrinthsectiondat ??= LabyrinthSectionDat.Load(this).AsReadOnly();

        return labyrinthsectiondat;
    }

    /// <summary>
    /// Gets LabyrinthSectionLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthSectionLayoutDat.</returns>
    public ReadOnlyCollection<LabyrinthSectionLayoutDat> GetLabyrinthSectionLayoutDat()
    {
        labyrinthsectionlayoutdat ??= LabyrinthSectionLayoutDat.Load(this).AsReadOnly();

        return labyrinthsectionlayoutdat;
    }

    /// <summary>
    /// Gets LabyrinthTrialsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthTrialsDat.</returns>
    public ReadOnlyCollection<LabyrinthTrialsDat> GetLabyrinthTrialsDat()
    {
        labyrinthtrialsdat ??= LabyrinthTrialsDat.Load(this).AsReadOnly();

        return labyrinthtrialsdat;
    }

    /// <summary>
    /// Gets LabyrinthTrinketsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthTrinketsDat.</returns>
    public ReadOnlyCollection<LabyrinthTrinketsDat> GetLabyrinthTrinketsDat()
    {
        labyrinthtrinketsdat ??= LabyrinthTrinketsDat.Load(this).AsReadOnly();

        return labyrinthtrinketsdat;
    }

    /// <summary>
    /// Gets PerandusBossesDat data.
    /// </summary>
    /// <returns>readonly collection of PerandusBossesDat.</returns>
    public ReadOnlyCollection<PerandusBossesDat> GetPerandusBossesDat()
    {
        perandusbossesdat ??= PerandusBossesDat.Load(this).AsReadOnly();

        return perandusbossesdat;
    }

    /// <summary>
    /// Gets PerandusChestsDat data.
    /// </summary>
    /// <returns>readonly collection of PerandusChestsDat.</returns>
    public ReadOnlyCollection<PerandusChestsDat> GetPerandusChestsDat()
    {
        peranduschestsdat ??= PerandusChestsDat.Load(this).AsReadOnly();

        return peranduschestsdat;
    }

    /// <summary>
    /// Gets PerandusDaemonsDat data.
    /// </summary>
    /// <returns>readonly collection of PerandusDaemonsDat.</returns>
    public ReadOnlyCollection<PerandusDaemonsDat> GetPerandusDaemonsDat()
    {
        perandusdaemonsdat ??= PerandusDaemonsDat.Load(this).AsReadOnly();

        return perandusdaemonsdat;
    }

    /// <summary>
    /// Gets PerandusGuardsDat data.
    /// </summary>
    /// <returns>readonly collection of PerandusGuardsDat.</returns>
    public ReadOnlyCollection<PerandusGuardsDat> GetPerandusGuardsDat()
    {
        perandusguardsdat ??= PerandusGuardsDat.Load(this).AsReadOnly();

        return perandusguardsdat;
    }

    /// <summary>
    /// Gets PropheciesDat data.
    /// </summary>
    /// <returns>readonly collection of PropheciesDat.</returns>
    public ReadOnlyCollection<PropheciesDat> GetPropheciesDat()
    {
        propheciesdat ??= PropheciesDat.Load(this).AsReadOnly();

        return propheciesdat;
    }

    /// <summary>
    /// Gets ProphecyChainDat data.
    /// </summary>
    /// <returns>readonly collection of ProphecyChainDat.</returns>
    public ReadOnlyCollection<ProphecyChainDat> GetProphecyChainDat()
    {
        prophecychaindat ??= ProphecyChainDat.Load(this).AsReadOnly();

        return prophecychaindat;
    }

    /// <summary>
    /// Gets ProphecyTypeDat data.
    /// </summary>
    /// <returns>readonly collection of ProphecyTypeDat.</returns>
    public ReadOnlyCollection<ProphecyTypeDat> GetProphecyTypeDat()
    {
        prophecytypedat ??= ProphecyTypeDat.Load(this).AsReadOnly();

        return prophecytypedat;
    }

    /// <summary>
    /// Gets ShaperGuardiansDat data.
    /// </summary>
    /// <returns>readonly collection of ShaperGuardiansDat.</returns>
    public ReadOnlyCollection<ShaperGuardiansDat> GetShaperGuardiansDat()
    {
        shaperguardiansdat ??= ShaperGuardiansDat.Load(this).AsReadOnly();

        return shaperguardiansdat;
    }

    /// <summary>
    /// Gets EssencesDat data.
    /// </summary>
    /// <returns>readonly collection of EssencesDat.</returns>
    public ReadOnlyCollection<EssencesDat> GetEssencesDat()
    {
        essencesdat ??= EssencesDat.Load(this).AsReadOnly();

        return essencesdat;
    }

    /// <summary>
    /// Gets EssenceTypeDat data.
    /// </summary>
    /// <returns>readonly collection of EssenceTypeDat.</returns>
    public ReadOnlyCollection<EssenceTypeDat> GetEssenceTypeDat()
    {
        essencetypedat ??= EssenceTypeDat.Load(this).AsReadOnly();

        return essencetypedat;
    }

    /// <summary>
    /// Gets BreachBossLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BreachBossLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<BreachBossLifeScalingPerLevelDat> GetBreachBossLifeScalingPerLevelDat()
    {
        breachbosslifescalingperleveldat ??= BreachBossLifeScalingPerLevelDat.Load(this).AsReadOnly();

        return breachbosslifescalingperleveldat;
    }

    /// <summary>
    /// Gets BreachElementDat data.
    /// </summary>
    /// <returns>readonly collection of BreachElementDat.</returns>
    public ReadOnlyCollection<BreachElementDat> GetBreachElementDat()
    {
        breachelementdat ??= BreachElementDat.Load(this).AsReadOnly();

        return breachelementdat;
    }

    /// <summary>
    /// Gets BreachstoneUpgradesDat data.
    /// </summary>
    /// <returns>readonly collection of BreachstoneUpgradesDat.</returns>
    public ReadOnlyCollection<BreachstoneUpgradesDat> GetBreachstoneUpgradesDat()
    {
        breachstoneupgradesdat ??= BreachstoneUpgradesDat.Load(this).AsReadOnly();

        return breachstoneupgradesdat;
    }

    /// <summary>
    /// Gets HarbingersDat data.
    /// </summary>
    /// <returns>readonly collection of HarbingersDat.</returns>
    public ReadOnlyCollection<HarbingersDat> GetHarbingersDat()
    {
        harbingersdat ??= HarbingersDat.Load(this).AsReadOnly();

        return harbingersdat;
    }

    /// <summary>
    /// Gets PantheonPanelLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of PantheonPanelLayoutDat.</returns>
    public ReadOnlyCollection<PantheonPanelLayoutDat> GetPantheonPanelLayoutDat()
    {
        pantheonpanellayoutdat ??= PantheonPanelLayoutDat.Load(this).AsReadOnly();

        return pantheonpanellayoutdat;
    }

    /// <summary>
    /// Gets PantheonSoulsDat data.
    /// </summary>
    /// <returns>readonly collection of PantheonSoulsDat.</returns>
    public ReadOnlyCollection<PantheonSoulsDat> GetPantheonSoulsDat()
    {
        pantheonsoulsdat ??= PantheonSoulsDat.Load(this).AsReadOnly();

        return pantheonsoulsdat;
    }

    /// <summary>
    /// Gets AbyssObjectsDat data.
    /// </summary>
    /// <returns>readonly collection of AbyssObjectsDat.</returns>
    public ReadOnlyCollection<AbyssObjectsDat> GetAbyssObjectsDat()
    {
        abyssobjectsdat ??= AbyssObjectsDat.Load(this).AsReadOnly();

        return abyssobjectsdat;
    }

    /// <summary>
    /// Gets ElderBossArenasDat data.
    /// </summary>
    /// <returns>readonly collection of ElderBossArenasDat.</returns>
    public ReadOnlyCollection<ElderBossArenasDat> GetElderBossArenasDat()
    {
        elderbossarenasdat ??= ElderBossArenasDat.Load(this).AsReadOnly();

        return elderbossarenasdat;
    }

    /// <summary>
    /// Gets ElderMapBossOverrideDat data.
    /// </summary>
    /// <returns>readonly collection of ElderMapBossOverrideDat.</returns>
    public ReadOnlyCollection<ElderMapBossOverrideDat> GetElderMapBossOverrideDat()
    {
        eldermapbossoverridedat ??= ElderMapBossOverrideDat.Load(this).AsReadOnly();

        return eldermapbossoverridedat;
    }

    /// <summary>
    /// Gets ElderGuardiansDat data.
    /// </summary>
    /// <returns>readonly collection of ElderGuardiansDat.</returns>
    public ReadOnlyCollection<ElderGuardiansDat> GetElderGuardiansDat()
    {
        elderguardiansdat ??= ElderGuardiansDat.Load(this).AsReadOnly();

        return elderguardiansdat;
    }

    /// <summary>
    /// Gets BestiaryCapturableMonstersDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryCapturableMonstersDat.</returns>
    public ReadOnlyCollection<BestiaryCapturableMonstersDat> GetBestiaryCapturableMonstersDat()
    {
        bestiarycapturablemonstersdat ??= BestiaryCapturableMonstersDat.Load(this).AsReadOnly();

        return bestiarycapturablemonstersdat;
    }

    /// <summary>
    /// Gets BestiaryEncountersDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryEncountersDat.</returns>
    public ReadOnlyCollection<BestiaryEncountersDat> GetBestiaryEncountersDat()
    {
        bestiaryencountersdat ??= BestiaryEncountersDat.Load(this).AsReadOnly();

        return bestiaryencountersdat;
    }

    /// <summary>
    /// Gets BestiaryFamiliesDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryFamiliesDat.</returns>
    public ReadOnlyCollection<BestiaryFamiliesDat> GetBestiaryFamiliesDat()
    {
        bestiaryfamiliesdat ??= BestiaryFamiliesDat.Load(this).AsReadOnly();

        return bestiaryfamiliesdat;
    }

    /// <summary>
    /// Gets BestiaryGenusDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryGenusDat.</returns>
    public ReadOnlyCollection<BestiaryGenusDat> GetBestiaryGenusDat()
    {
        bestiarygenusdat ??= BestiaryGenusDat.Load(this).AsReadOnly();

        return bestiarygenusdat;
    }

    /// <summary>
    /// Gets BestiaryGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryGroupsDat.</returns>
    public ReadOnlyCollection<BestiaryGroupsDat> GetBestiaryGroupsDat()
    {
        bestiarygroupsdat ??= BestiaryGroupsDat.Load(this).AsReadOnly();

        return bestiarygroupsdat;
    }

    /// <summary>
    /// Gets BestiaryNetsDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryNetsDat.</returns>
    public ReadOnlyCollection<BestiaryNetsDat> GetBestiaryNetsDat()
    {
        bestiarynetsdat ??= BestiaryNetsDat.Load(this).AsReadOnly();

        return bestiarynetsdat;
    }

    /// <summary>
    /// Gets BestiaryRecipeComponentDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryRecipeComponentDat.</returns>
    public ReadOnlyCollection<BestiaryRecipeComponentDat> GetBestiaryRecipeComponentDat()
    {
        bestiaryrecipecomponentdat ??= BestiaryRecipeComponentDat.Load(this).AsReadOnly();

        return bestiaryrecipecomponentdat;
    }

    /// <summary>
    /// Gets BestiaryRecipeCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryRecipeCategoriesDat.</returns>
    public ReadOnlyCollection<BestiaryRecipeCategoriesDat> GetBestiaryRecipeCategoriesDat()
    {
        bestiaryrecipecategoriesdat ??= BestiaryRecipeCategoriesDat.Load(this).AsReadOnly();

        return bestiaryrecipecategoriesdat;
    }

    /// <summary>
    /// Gets BestiaryRecipesDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryRecipesDat.</returns>
    public ReadOnlyCollection<BestiaryRecipesDat> GetBestiaryRecipesDat()
    {
        bestiaryrecipesdat ??= BestiaryRecipesDat.Load(this).AsReadOnly();

        return bestiaryrecipesdat;
    }

    /// <summary>
    /// Gets ArchitectLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of ArchitectLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<ArchitectLifeScalingPerLevelDat> GetArchitectLifeScalingPerLevelDat()
    {
        architectlifescalingperleveldat ??= ArchitectLifeScalingPerLevelDat.Load(this).AsReadOnly();

        return architectlifescalingperleveldat;
    }

    /// <summary>
    /// Gets IncursionArchitectDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionArchitectDat.</returns>
    public ReadOnlyCollection<IncursionArchitectDat> GetIncursionArchitectDat()
    {
        incursionarchitectdat ??= IncursionArchitectDat.Load(this).AsReadOnly();

        return incursionarchitectdat;
    }

    /// <summary>
    /// Gets IncursionBracketsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionBracketsDat.</returns>
    public ReadOnlyCollection<IncursionBracketsDat> GetIncursionBracketsDat()
    {
        incursionbracketsdat ??= IncursionBracketsDat.Load(this).AsReadOnly();

        return incursionbracketsdat;
    }

    /// <summary>
    /// Gets IncursionChestRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionChestRewardsDat.</returns>
    public ReadOnlyCollection<IncursionChestRewardsDat> GetIncursionChestRewardsDat()
    {
        incursionchestrewardsdat ??= IncursionChestRewardsDat.Load(this).AsReadOnly();

        return incursionchestrewardsdat;
    }

    /// <summary>
    /// Gets IncursionChestsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionChestsDat.</returns>
    public ReadOnlyCollection<IncursionChestsDat> GetIncursionChestsDat()
    {
        incursionchestsdat ??= IncursionChestsDat.Load(this).AsReadOnly();

        return incursionchestsdat;
    }

    /// <summary>
    /// Gets IncursionRoomBossFightEventsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionRoomBossFightEventsDat.</returns>
    public ReadOnlyCollection<IncursionRoomBossFightEventsDat> GetIncursionRoomBossFightEventsDat()
    {
        incursionroombossfighteventsdat ??= IncursionRoomBossFightEventsDat.Load(this).AsReadOnly();

        return incursionroombossfighteventsdat;
    }

    /// <summary>
    /// Gets IncursionRoomsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionRoomsDat.</returns>
    public ReadOnlyCollection<IncursionRoomsDat> GetIncursionRoomsDat()
    {
        incursionroomsdat ??= IncursionRoomsDat.Load(this).AsReadOnly();

        return incursionroomsdat;
    }

    /// <summary>
    /// Gets IncursionUniqueUpgradeComponentsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionUniqueUpgradeComponentsDat.</returns>
    public ReadOnlyCollection<IncursionUniqueUpgradeComponentsDat> GetIncursionUniqueUpgradeComponentsDat()
    {
        incursionuniqueupgradecomponentsdat ??= IncursionUniqueUpgradeComponentsDat.Load(this).AsReadOnly();

        return incursionuniqueupgradecomponentsdat;
    }

    /// <summary>
    /// Gets DelveAzuriteShopDat data.
    /// </summary>
    /// <returns>readonly collection of DelveAzuriteShopDat.</returns>
    public ReadOnlyCollection<DelveAzuriteShopDat> GetDelveAzuriteShopDat()
    {
        delveazuriteshopdat ??= DelveAzuriteShopDat.Load(this).AsReadOnly();

        return delveazuriteshopdat;
    }

    /// <summary>
    /// Gets DelveBiomesDat data.
    /// </summary>
    /// <returns>readonly collection of DelveBiomesDat.</returns>
    public ReadOnlyCollection<DelveBiomesDat> GetDelveBiomesDat()
    {
        delvebiomesdat ??= DelveBiomesDat.Load(this).AsReadOnly();

        return delvebiomesdat;
    }

    /// <summary>
    /// Gets DelveCatchupDepthsDat data.
    /// </summary>
    /// <returns>readonly collection of DelveCatchupDepthsDat.</returns>
    public ReadOnlyCollection<DelveCatchupDepthsDat> GetDelveCatchupDepthsDat()
    {
        delvecatchupdepthsdat ??= DelveCatchupDepthsDat.Load(this).AsReadOnly();

        return delvecatchupdepthsdat;
    }

    /// <summary>
    /// Gets DelveCraftingModifierDescriptionsDat data.
    /// </summary>
    /// <returns>readonly collection of DelveCraftingModifierDescriptionsDat.</returns>
    public ReadOnlyCollection<DelveCraftingModifierDescriptionsDat> GetDelveCraftingModifierDescriptionsDat()
    {
        delvecraftingmodifierdescriptionsdat ??= DelveCraftingModifierDescriptionsDat.Load(this).AsReadOnly();

        return delvecraftingmodifierdescriptionsdat;
    }

    /// <summary>
    /// Gets DelveCraftingModifiersDat data.
    /// </summary>
    /// <returns>readonly collection of DelveCraftingModifiersDat.</returns>
    public ReadOnlyCollection<DelveCraftingModifiersDat> GetDelveCraftingModifiersDat()
    {
        delvecraftingmodifiersdat ??= DelveCraftingModifiersDat.Load(this).AsReadOnly();

        return delvecraftingmodifiersdat;
    }

    /// <summary>
    /// Gets DelveCraftingTagsDat data.
    /// </summary>
    /// <returns>readonly collection of DelveCraftingTagsDat.</returns>
    public ReadOnlyCollection<DelveCraftingTagsDat> GetDelveCraftingTagsDat()
    {
        delvecraftingtagsdat ??= DelveCraftingTagsDat.Load(this).AsReadOnly();

        return delvecraftingtagsdat;
    }

    /// <summary>
    /// Gets DelveDynamiteDat data.
    /// </summary>
    /// <returns>readonly collection of DelveDynamiteDat.</returns>
    public ReadOnlyCollection<DelveDynamiteDat> GetDelveDynamiteDat()
    {
        delvedynamitedat ??= DelveDynamiteDat.Load(this).AsReadOnly();

        return delvedynamitedat;
    }

    /// <summary>
    /// Gets DelveFeaturesDat data.
    /// </summary>
    /// <returns>readonly collection of DelveFeaturesDat.</returns>
    public ReadOnlyCollection<DelveFeaturesDat> GetDelveFeaturesDat()
    {
        delvefeaturesdat ??= DelveFeaturesDat.Load(this).AsReadOnly();

        return delvefeaturesdat;
    }

    /// <summary>
    /// Gets DelveFlaresDat data.
    /// </summary>
    /// <returns>readonly collection of DelveFlaresDat.</returns>
    public ReadOnlyCollection<DelveFlaresDat> GetDelveFlaresDat()
    {
        delveflaresdat ??= DelveFlaresDat.Load(this).AsReadOnly();

        return delveflaresdat;
    }

    /// <summary>
    /// Gets DelveLevelScalingDat data.
    /// </summary>
    /// <returns>readonly collection of DelveLevelScalingDat.</returns>
    public ReadOnlyCollection<DelveLevelScalingDat> GetDelveLevelScalingDat()
    {
        delvelevelscalingdat ??= DelveLevelScalingDat.Load(this).AsReadOnly();

        return delvelevelscalingdat;
    }

    /// <summary>
    /// Gets DelveMonsterSpawnersDat data.
    /// </summary>
    /// <returns>readonly collection of DelveMonsterSpawnersDat.</returns>
    public ReadOnlyCollection<DelveMonsterSpawnersDat> GetDelveMonsterSpawnersDat()
    {
        delvemonsterspawnersdat ??= DelveMonsterSpawnersDat.Load(this).AsReadOnly();

        return delvemonsterspawnersdat;
    }

    /// <summary>
    /// Gets DelveResourcePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of DelveResourcePerLevelDat.</returns>
    public ReadOnlyCollection<DelveResourcePerLevelDat> GetDelveResourcePerLevelDat()
    {
        delveresourceperleveldat ??= DelveResourcePerLevelDat.Load(this).AsReadOnly();

        return delveresourceperleveldat;
    }

    /// <summary>
    /// Gets DelveRewardTierConstantsDat data.
    /// </summary>
    /// <returns>readonly collection of DelveRewardTierConstantsDat.</returns>
    public ReadOnlyCollection<DelveRewardTierConstantsDat> GetDelveRewardTierConstantsDat()
    {
        delverewardtierconstantsdat ??= DelveRewardTierConstantsDat.Load(this).AsReadOnly();

        return delverewardtierconstantsdat;
    }

    /// <summary>
    /// Gets DelveRoomsDat data.
    /// </summary>
    /// <returns>readonly collection of DelveRoomsDat.</returns>
    public ReadOnlyCollection<DelveRoomsDat> GetDelveRoomsDat()
    {
        delveroomsdat ??= DelveRoomsDat.Load(this).AsReadOnly();

        return delveroomsdat;
    }

    /// <summary>
    /// Gets DelveUpgradesDat data.
    /// </summary>
    /// <returns>readonly collection of DelveUpgradesDat.</returns>
    public ReadOnlyCollection<DelveUpgradesDat> GetDelveUpgradesDat()
    {
        delveupgradesdat ??= DelveUpgradesDat.Load(this).AsReadOnly();

        return delveupgradesdat;
    }

    /// <summary>
    /// Gets BetrayalChoiceActionsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalChoiceActionsDat.</returns>
    public ReadOnlyCollection<BetrayalChoiceActionsDat> GetBetrayalChoiceActionsDat()
    {
        betrayalchoiceactionsdat ??= BetrayalChoiceActionsDat.Load(this).AsReadOnly();

        return betrayalchoiceactionsdat;
    }

    /// <summary>
    /// Gets BetrayalChoicesDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalChoicesDat.</returns>
    public ReadOnlyCollection<BetrayalChoicesDat> GetBetrayalChoicesDat()
    {
        betrayalchoicesdat ??= BetrayalChoicesDat.Load(this).AsReadOnly();

        return betrayalchoicesdat;
    }

    /// <summary>
    /// Gets BetrayalDialogueDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalDialogueDat.</returns>
    public ReadOnlyCollection<BetrayalDialogueDat> GetBetrayalDialogueDat()
    {
        betrayaldialoguedat ??= BetrayalDialogueDat.Load(this).AsReadOnly();

        return betrayaldialoguedat;
    }

    /// <summary>
    /// Gets BetrayalFortsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalFortsDat.</returns>
    public ReadOnlyCollection<BetrayalFortsDat> GetBetrayalFortsDat()
    {
        betrayalfortsdat ??= BetrayalFortsDat.Load(this).AsReadOnly();

        return betrayalfortsdat;
    }

    /// <summary>
    /// Gets BetrayalJobsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalJobsDat.</returns>
    public ReadOnlyCollection<BetrayalJobsDat> GetBetrayalJobsDat()
    {
        betrayaljobsdat ??= BetrayalJobsDat.Load(this).AsReadOnly();

        return betrayaljobsdat;
    }

    /// <summary>
    /// Gets BetrayalRanksDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalRanksDat.</returns>
    public ReadOnlyCollection<BetrayalRanksDat> GetBetrayalRanksDat()
    {
        betrayalranksdat ??= BetrayalRanksDat.Load(this).AsReadOnly();

        return betrayalranksdat;
    }

    /// <summary>
    /// Gets BetrayalRelationshipStateDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalRelationshipStateDat.</returns>
    public ReadOnlyCollection<BetrayalRelationshipStateDat> GetBetrayalRelationshipStateDat()
    {
        betrayalrelationshipstatedat ??= BetrayalRelationshipStateDat.Load(this).AsReadOnly();

        return betrayalrelationshipstatedat;
    }

    /// <summary>
    /// Gets BetrayalTargetJobAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalTargetJobAchievementsDat.</returns>
    public ReadOnlyCollection<BetrayalTargetJobAchievementsDat> GetBetrayalTargetJobAchievementsDat()
    {
        betrayaltargetjobachievementsdat ??= BetrayalTargetJobAchievementsDat.Load(this).AsReadOnly();

        return betrayaltargetjobachievementsdat;
    }

    /// <summary>
    /// Gets BetrayalTargetLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalTargetLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<BetrayalTargetLifeScalingPerLevelDat> GetBetrayalTargetLifeScalingPerLevelDat()
    {
        betrayaltargetlifescalingperleveldat ??= BetrayalTargetLifeScalingPerLevelDat.Load(this).AsReadOnly();

        return betrayaltargetlifescalingperleveldat;
    }

    /// <summary>
    /// Gets BetrayalTargetsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalTargetsDat.</returns>
    public ReadOnlyCollection<BetrayalTargetsDat> GetBetrayalTargetsDat()
    {
        betrayaltargetsdat ??= BetrayalTargetsDat.Load(this).AsReadOnly();

        return betrayaltargetsdat;
    }

    /// <summary>
    /// Gets BetrayalTraitorRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalTraitorRewardsDat.</returns>
    public ReadOnlyCollection<BetrayalTraitorRewardsDat> GetBetrayalTraitorRewardsDat()
    {
        betrayaltraitorrewardsdat ??= BetrayalTraitorRewardsDat.Load(this).AsReadOnly();

        return betrayaltraitorrewardsdat;
    }

    /// <summary>
    /// Gets BetrayalUpgradesDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalUpgradesDat.</returns>
    public ReadOnlyCollection<BetrayalUpgradesDat> GetBetrayalUpgradesDat()
    {
        betrayalupgradesdat ??= BetrayalUpgradesDat.Load(this).AsReadOnly();

        return betrayalupgradesdat;
    }

    /// <summary>
    /// Gets BetrayalWallLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalWallLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<BetrayalWallLifeScalingPerLevelDat> GetBetrayalWallLifeScalingPerLevelDat()
    {
        betrayalwalllifescalingperleveldat ??= BetrayalWallLifeScalingPerLevelDat.Load(this).AsReadOnly();

        return betrayalwalllifescalingperleveldat;
    }

    /// <summary>
    /// Gets SafehouseBYOCraftingDat data.
    /// </summary>
    /// <returns>readonly collection of SafehouseBYOCraftingDat.</returns>
    public ReadOnlyCollection<SafehouseBYOCraftingDat> GetSafehouseBYOCraftingDat()
    {
        safehousebyocraftingdat ??= SafehouseBYOCraftingDat.Load(this).AsReadOnly();

        return safehousebyocraftingdat;
    }

    /// <summary>
    /// Gets SafehouseCraftingSpreeTypeDat data.
    /// </summary>
    /// <returns>readonly collection of SafehouseCraftingSpreeTypeDat.</returns>
    public ReadOnlyCollection<SafehouseCraftingSpreeTypeDat> GetSafehouseCraftingSpreeTypeDat()
    {
        safehousecraftingspreetypedat ??= SafehouseCraftingSpreeTypeDat.Load(this).AsReadOnly();

        return safehousecraftingspreetypedat;
    }

    /// <summary>
    /// Gets SafehouseCraftingSpreeCurrenciesDat data.
    /// </summary>
    /// <returns>readonly collection of SafehouseCraftingSpreeCurrenciesDat.</returns>
    public ReadOnlyCollection<SafehouseCraftingSpreeCurrenciesDat> GetSafehouseCraftingSpreeCurrenciesDat()
    {
        safehousecraftingspreecurrenciesdat ??= SafehouseCraftingSpreeCurrenciesDat.Load(this).AsReadOnly();

        return safehousecraftingspreecurrenciesdat;
    }

    /// <summary>
    /// Gets ScarabsDat data.
    /// </summary>
    /// <returns>readonly collection of ScarabsDat.</returns>
    public ReadOnlyCollection<ScarabsDat> GetScarabsDat()
    {
        scarabsdat ??= ScarabsDat.Load(this).AsReadOnly();

        return scarabsdat;
    }

    /// <summary>
    /// Gets SynthesisAreasDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisAreasDat.</returns>
    public ReadOnlyCollection<SynthesisAreasDat> GetSynthesisAreasDat()
    {
        synthesisareasdat ??= SynthesisAreasDat.Load(this).AsReadOnly();

        return synthesisareasdat;
    }

    /// <summary>
    /// Gets SynthesisAreaSizeDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisAreaSizeDat.</returns>
    public ReadOnlyCollection<SynthesisAreaSizeDat> GetSynthesisAreaSizeDat()
    {
        synthesisareasizedat ??= SynthesisAreaSizeDat.Load(this).AsReadOnly();

        return synthesisareasizedat;
    }

    /// <summary>
    /// Gets SynthesisBonusesDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisBonusesDat.</returns>
    public ReadOnlyCollection<SynthesisBonusesDat> GetSynthesisBonusesDat()
    {
        synthesisbonusesdat ??= SynthesisBonusesDat.Load(this).AsReadOnly();

        return synthesisbonusesdat;
    }

    /// <summary>
    /// Gets SynthesisBracketsDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisBracketsDat.</returns>
    public ReadOnlyCollection<SynthesisBracketsDat> GetSynthesisBracketsDat()
    {
        synthesisbracketsdat ??= SynthesisBracketsDat.Load(this).AsReadOnly();

        return synthesisbracketsdat;
    }

    /// <summary>
    /// Gets SynthesisFragmentDialogueDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisFragmentDialogueDat.</returns>
    public ReadOnlyCollection<SynthesisFragmentDialogueDat> GetSynthesisFragmentDialogueDat()
    {
        synthesisfragmentdialoguedat ??= SynthesisFragmentDialogueDat.Load(this).AsReadOnly();

        return synthesisfragmentdialoguedat;
    }

    /// <summary>
    /// Gets SynthesisGlobalModsDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisGlobalModsDat.</returns>
    public ReadOnlyCollection<SynthesisGlobalModsDat> GetSynthesisGlobalModsDat()
    {
        synthesisglobalmodsdat ??= SynthesisGlobalModsDat.Load(this).AsReadOnly();

        return synthesisglobalmodsdat;
    }

    /// <summary>
    /// Gets SynthesisMonsterExperiencePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisMonsterExperiencePerLevelDat.</returns>
    public ReadOnlyCollection<SynthesisMonsterExperiencePerLevelDat> GetSynthesisMonsterExperiencePerLevelDat()
    {
        synthesismonsterexperienceperleveldat ??= SynthesisMonsterExperiencePerLevelDat.Load(this).AsReadOnly();

        return synthesismonsterexperienceperleveldat;
    }

    /// <summary>
    /// Gets SynthesisRewardCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisRewardCategoriesDat.</returns>
    public ReadOnlyCollection<SynthesisRewardCategoriesDat> GetSynthesisRewardCategoriesDat()
    {
        synthesisrewardcategoriesdat ??= SynthesisRewardCategoriesDat.Load(this).AsReadOnly();

        return synthesisrewardcategoriesdat;
    }

    /// <summary>
    /// Gets SynthesisRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisRewardTypesDat.</returns>
    public ReadOnlyCollection<SynthesisRewardTypesDat> GetSynthesisRewardTypesDat()
    {
        synthesisrewardtypesdat ??= SynthesisRewardTypesDat.Load(this).AsReadOnly();

        return synthesisrewardtypesdat;
    }

    /// <summary>
    /// Gets IncubatorsDat data.
    /// </summary>
    /// <returns>readonly collection of IncubatorsDat.</returns>
    public ReadOnlyCollection<IncubatorsDat> GetIncubatorsDat()
    {
        incubatorsdat ??= IncubatorsDat.Load(this).AsReadOnly();

        return incubatorsdat;
    }

    /// <summary>
    /// Gets LegionBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of LegionBalancePerLevelDat.</returns>
    public ReadOnlyCollection<LegionBalancePerLevelDat> GetLegionBalancePerLevelDat()
    {
        legionbalanceperleveldat ??= LegionBalancePerLevelDat.Load(this).AsReadOnly();

        return legionbalanceperleveldat;
    }

    /// <summary>
    /// Gets LegionChestTypesDat data.
    /// </summary>
    /// <returns>readonly collection of LegionChestTypesDat.</returns>
    public ReadOnlyCollection<LegionChestTypesDat> GetLegionChestTypesDat()
    {
        legionchesttypesdat ??= LegionChestTypesDat.Load(this).AsReadOnly();

        return legionchesttypesdat;
    }

    /// <summary>
    /// Gets LegionChestCountsDat data.
    /// </summary>
    /// <returns>readonly collection of LegionChestCountsDat.</returns>
    public ReadOnlyCollection<LegionChestCountsDat> GetLegionChestCountsDat()
    {
        legionchestcountsdat ??= LegionChestCountsDat.Load(this).AsReadOnly();

        return legionchestcountsdat;
    }

    /// <summary>
    /// Gets LegionChestsDat data.
    /// </summary>
    /// <returns>readonly collection of LegionChestsDat.</returns>
    public ReadOnlyCollection<LegionChestsDat> GetLegionChestsDat()
    {
        legionchestsdat ??= LegionChestsDat.Load(this).AsReadOnly();

        return legionchestsdat;
    }

    /// <summary>
    /// Gets LegionFactionsDat data.
    /// </summary>
    /// <returns>readonly collection of LegionFactionsDat.</returns>
    public ReadOnlyCollection<LegionFactionsDat> GetLegionFactionsDat()
    {
        legionfactionsdat ??= LegionFactionsDat.Load(this).AsReadOnly();

        return legionfactionsdat;
    }

    /// <summary>
    /// Gets LegionMonsterCountsDat data.
    /// </summary>
    /// <returns>readonly collection of LegionMonsterCountsDat.</returns>
    public ReadOnlyCollection<LegionMonsterCountsDat> GetLegionMonsterCountsDat()
    {
        legionmonstercountsdat ??= LegionMonsterCountsDat.Load(this).AsReadOnly();

        return legionmonstercountsdat;
    }

    /// <summary>
    /// Gets LegionMonsterVarietiesDat data.
    /// </summary>
    /// <returns>readonly collection of LegionMonsterVarietiesDat.</returns>
    public ReadOnlyCollection<LegionMonsterVarietiesDat> GetLegionMonsterVarietiesDat()
    {
        legionmonstervarietiesdat ??= LegionMonsterVarietiesDat.Load(this).AsReadOnly();

        return legionmonstervarietiesdat;
    }

    /// <summary>
    /// Gets LegionRanksDat data.
    /// </summary>
    /// <returns>readonly collection of LegionRanksDat.</returns>
    public ReadOnlyCollection<LegionRanksDat> GetLegionRanksDat()
    {
        legionranksdat ??= LegionRanksDat.Load(this).AsReadOnly();

        return legionranksdat;
    }

    /// <summary>
    /// Gets LegionRewardTypeVisualsDat data.
    /// </summary>
    /// <returns>readonly collection of LegionRewardTypeVisualsDat.</returns>
    public ReadOnlyCollection<LegionRewardTypeVisualsDat> GetLegionRewardTypeVisualsDat()
    {
        legionrewardtypevisualsdat ??= LegionRewardTypeVisualsDat.Load(this).AsReadOnly();

        return legionrewardtypevisualsdat;
    }

    /// <summary>
    /// Gets BlightBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BlightBalancePerLevelDat.</returns>
    public ReadOnlyCollection<BlightBalancePerLevelDat> GetBlightBalancePerLevelDat()
    {
        blightbalanceperleveldat ??= BlightBalancePerLevelDat.Load(this).AsReadOnly();

        return blightbalanceperleveldat;
    }

    /// <summary>
    /// Gets BlightBossLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BlightBossLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<BlightBossLifeScalingPerLevelDat> GetBlightBossLifeScalingPerLevelDat()
    {
        blightbosslifescalingperleveldat ??= BlightBossLifeScalingPerLevelDat.Load(this).AsReadOnly();

        return blightbosslifescalingperleveldat;
    }

    /// <summary>
    /// Gets BlightChestTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightChestTypesDat.</returns>
    public ReadOnlyCollection<BlightChestTypesDat> GetBlightChestTypesDat()
    {
        blightchesttypesdat ??= BlightChestTypesDat.Load(this).AsReadOnly();

        return blightchesttypesdat;
    }

    /// <summary>
    /// Gets BlightCraftingItemsDat data.
    /// </summary>
    /// <returns>readonly collection of BlightCraftingItemsDat.</returns>
    public ReadOnlyCollection<BlightCraftingItemsDat> GetBlightCraftingItemsDat()
    {
        blightcraftingitemsdat ??= BlightCraftingItemsDat.Load(this).AsReadOnly();

        return blightcraftingitemsdat;
    }

    /// <summary>
    /// Gets BlightCraftingRecipesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightCraftingRecipesDat.</returns>
    public ReadOnlyCollection<BlightCraftingRecipesDat> GetBlightCraftingRecipesDat()
    {
        blightcraftingrecipesdat ??= BlightCraftingRecipesDat.Load(this).AsReadOnly();

        return blightcraftingrecipesdat;
    }

    /// <summary>
    /// Gets BlightCraftingResultsDat data.
    /// </summary>
    /// <returns>readonly collection of BlightCraftingResultsDat.</returns>
    public ReadOnlyCollection<BlightCraftingResultsDat> GetBlightCraftingResultsDat()
    {
        blightcraftingresultsdat ??= BlightCraftingResultsDat.Load(this).AsReadOnly();

        return blightcraftingresultsdat;
    }

    /// <summary>
    /// Gets BlightCraftingTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightCraftingTypesDat.</returns>
    public ReadOnlyCollection<BlightCraftingTypesDat> GetBlightCraftingTypesDat()
    {
        blightcraftingtypesdat ??= BlightCraftingTypesDat.Load(this).AsReadOnly();

        return blightcraftingtypesdat;
    }

    /// <summary>
    /// Gets BlightCraftingUniquesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightCraftingUniquesDat.</returns>
    public ReadOnlyCollection<BlightCraftingUniquesDat> GetBlightCraftingUniquesDat()
    {
        blightcraftinguniquesdat ??= BlightCraftingUniquesDat.Load(this).AsReadOnly();

        return blightcraftinguniquesdat;
    }

    /// <summary>
    /// Gets BlightedSporeAurasDat data.
    /// </summary>
    /// <returns>readonly collection of BlightedSporeAurasDat.</returns>
    public ReadOnlyCollection<BlightedSporeAurasDat> GetBlightedSporeAurasDat()
    {
        blightedsporeaurasdat ??= BlightedSporeAurasDat.Load(this).AsReadOnly();

        return blightedsporeaurasdat;
    }

    /// <summary>
    /// Gets BlightEncounterTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightEncounterTypesDat.</returns>
    public ReadOnlyCollection<BlightEncounterTypesDat> GetBlightEncounterTypesDat()
    {
        blightencountertypesdat ??= BlightEncounterTypesDat.Load(this).AsReadOnly();

        return blightencountertypesdat;
    }

    /// <summary>
    /// Gets BlightEncounterWavesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightEncounterWavesDat.</returns>
    public ReadOnlyCollection<BlightEncounterWavesDat> GetBlightEncounterWavesDat()
    {
        blightencounterwavesdat ??= BlightEncounterWavesDat.Load(this).AsReadOnly();

        return blightencounterwavesdat;
    }

    /// <summary>
    /// Gets BlightRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightRewardTypesDat.</returns>
    public ReadOnlyCollection<BlightRewardTypesDat> GetBlightRewardTypesDat()
    {
        blightrewardtypesdat ??= BlightRewardTypesDat.Load(this).AsReadOnly();

        return blightrewardtypesdat;
    }

    /// <summary>
    /// Gets BlightTopologiesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightTopologiesDat.</returns>
    public ReadOnlyCollection<BlightTopologiesDat> GetBlightTopologiesDat()
    {
        blighttopologiesdat ??= BlightTopologiesDat.Load(this).AsReadOnly();

        return blighttopologiesdat;
    }

    /// <summary>
    /// Gets BlightTopologyNodesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightTopologyNodesDat.</returns>
    public ReadOnlyCollection<BlightTopologyNodesDat> GetBlightTopologyNodesDat()
    {
        blighttopologynodesdat ??= BlightTopologyNodesDat.Load(this).AsReadOnly();

        return blighttopologynodesdat;
    }

    /// <summary>
    /// Gets BlightTowerAurasDat data.
    /// </summary>
    /// <returns>readonly collection of BlightTowerAurasDat.</returns>
    public ReadOnlyCollection<BlightTowerAurasDat> GetBlightTowerAurasDat()
    {
        blighttoweraurasdat ??= BlightTowerAurasDat.Load(this).AsReadOnly();

        return blighttoweraurasdat;
    }

    /// <summary>
    /// Gets BlightTowersDat data.
    /// </summary>
    /// <returns>readonly collection of BlightTowersDat.</returns>
    public ReadOnlyCollection<BlightTowersDat> GetBlightTowersDat()
    {
        blighttowersdat ??= BlightTowersDat.Load(this).AsReadOnly();

        return blighttowersdat;
    }

    /// <summary>
    /// Gets BlightTowersPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BlightTowersPerLevelDat.</returns>
    public ReadOnlyCollection<BlightTowersPerLevelDat> GetBlightTowersPerLevelDat()
    {
        blighttowersperleveldat ??= BlightTowersPerLevelDat.Load(this).AsReadOnly();

        return blighttowersperleveldat;
    }

    /// <summary>
    /// Gets AtlasExileBossArenasDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasExileBossArenasDat.</returns>
    public ReadOnlyCollection<AtlasExileBossArenasDat> GetAtlasExileBossArenasDat()
    {
        atlasexilebossarenasdat ??= AtlasExileBossArenasDat.Load(this).AsReadOnly();

        return atlasexilebossarenasdat;
    }

    /// <summary>
    /// Gets AtlasExileInfluenceDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasExileInfluenceDat.</returns>
    public ReadOnlyCollection<AtlasExileInfluenceDat> GetAtlasExileInfluenceDat()
    {
        atlasexileinfluencedat ??= AtlasExileInfluenceDat.Load(this).AsReadOnly();

        return atlasexileinfluencedat;
    }

    /// <summary>
    /// Gets AtlasExilesDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasExilesDat.</returns>
    public ReadOnlyCollection<AtlasExilesDat> GetAtlasExilesDat()
    {
        atlasexilesdat ??= AtlasExilesDat.Load(this).AsReadOnly();

        return atlasexilesdat;
    }

    /// <summary>
    /// Gets AlternateQualityCurrencyDecayFactorsDat data.
    /// </summary>
    /// <returns>readonly collection of AlternateQualityCurrencyDecayFactorsDat.</returns>
    public ReadOnlyCollection<AlternateQualityCurrencyDecayFactorsDat> GetAlternateQualityCurrencyDecayFactorsDat()
    {
        alternatequalitycurrencydecayfactorsdat ??= AlternateQualityCurrencyDecayFactorsDat.Load(this).AsReadOnly();

        return alternatequalitycurrencydecayfactorsdat;
    }

    /// <summary>
    /// Gets AlternateQualityTypesDat data.
    /// </summary>
    /// <returns>readonly collection of AlternateQualityTypesDat.</returns>
    public ReadOnlyCollection<AlternateQualityTypesDat> GetAlternateQualityTypesDat()
    {
        alternatequalitytypesdat ??= AlternateQualityTypesDat.Load(this).AsReadOnly();

        return alternatequalitytypesdat;
    }

    /// <summary>
    /// Gets MetamorphLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<MetamorphLifeScalingPerLevelDat> GetMetamorphLifeScalingPerLevelDat()
    {
        metamorphlifescalingperleveldat ??= MetamorphLifeScalingPerLevelDat.Load(this).AsReadOnly();

        return metamorphlifescalingperleveldat;
    }

    /// <summary>
    /// Gets MetamorphosisMetaMonstersDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisMetaMonstersDat.</returns>
    public ReadOnlyCollection<MetamorphosisMetaMonstersDat> GetMetamorphosisMetaMonstersDat()
    {
        metamorphosismetamonstersdat ??= MetamorphosisMetaMonstersDat.Load(this).AsReadOnly();

        return metamorphosismetamonstersdat;
    }

    /// <summary>
    /// Gets MetamorphosisMetaSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisMetaSkillsDat.</returns>
    public ReadOnlyCollection<MetamorphosisMetaSkillsDat> GetMetamorphosisMetaSkillsDat()
    {
        metamorphosismetaskillsdat ??= MetamorphosisMetaSkillsDat.Load(this).AsReadOnly();

        return metamorphosismetaskillsdat;
    }

    /// <summary>
    /// Gets MetamorphosisMetaSkillTypesDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisMetaSkillTypesDat.</returns>
    public ReadOnlyCollection<MetamorphosisMetaSkillTypesDat> GetMetamorphosisMetaSkillTypesDat()
    {
        metamorphosismetaskilltypesdat ??= MetamorphosisMetaSkillTypesDat.Load(this).AsReadOnly();

        return metamorphosismetaskilltypesdat;
    }

    /// <summary>
    /// Gets MetamorphosisRewardTypeItemsClientDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisRewardTypeItemsClientDat.</returns>
    public ReadOnlyCollection<MetamorphosisRewardTypeItemsClientDat> GetMetamorphosisRewardTypeItemsClientDat()
    {
        metamorphosisrewardtypeitemsclientdat ??= MetamorphosisRewardTypeItemsClientDat.Load(this).AsReadOnly();

        return metamorphosisrewardtypeitemsclientdat;
    }

    /// <summary>
    /// Gets MetamorphosisRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisRewardTypesDat.</returns>
    public ReadOnlyCollection<MetamorphosisRewardTypesDat> GetMetamorphosisRewardTypesDat()
    {
        metamorphosisrewardtypesdat ??= MetamorphosisRewardTypesDat.Load(this).AsReadOnly();

        return metamorphosisrewardtypesdat;
    }

    /// <summary>
    /// Gets MetamorphosisScalingDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisScalingDat.</returns>
    public ReadOnlyCollection<MetamorphosisScalingDat> GetMetamorphosisScalingDat()
    {
        metamorphosisscalingdat ??= MetamorphosisScalingDat.Load(this).AsReadOnly();

        return metamorphosisscalingdat;
    }

    /// <summary>
    /// Gets AfflictionBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionBalancePerLevelDat.</returns>
    public ReadOnlyCollection<AfflictionBalancePerLevelDat> GetAfflictionBalancePerLevelDat()
    {
        afflictionbalanceperleveldat ??= AfflictionBalancePerLevelDat.Load(this).AsReadOnly();

        return afflictionbalanceperleveldat;
    }

    /// <summary>
    /// Gets AfflictionEndgameWaveModsDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionEndgameWaveModsDat.</returns>
    public ReadOnlyCollection<AfflictionEndgameWaveModsDat> GetAfflictionEndgameWaveModsDat()
    {
        afflictionendgamewavemodsdat ??= AfflictionEndgameWaveModsDat.Load(this).AsReadOnly();

        return afflictionendgamewavemodsdat;
    }

    /// <summary>
    /// Gets AfflictionFixedModsDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionFixedModsDat.</returns>
    public ReadOnlyCollection<AfflictionFixedModsDat> GetAfflictionFixedModsDat()
    {
        afflictionfixedmodsdat ??= AfflictionFixedModsDat.Load(this).AsReadOnly();

        return afflictionfixedmodsdat;
    }

    /// <summary>
    /// Gets AfflictionRandomModCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionRandomModCategoriesDat.</returns>
    public ReadOnlyCollection<AfflictionRandomModCategoriesDat> GetAfflictionRandomModCategoriesDat()
    {
        afflictionrandommodcategoriesdat ??= AfflictionRandomModCategoriesDat.Load(this).AsReadOnly();

        return afflictionrandommodcategoriesdat;
    }

    /// <summary>
    /// Gets AfflictionRewardMapModsDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionRewardMapModsDat.</returns>
    public ReadOnlyCollection<AfflictionRewardMapModsDat> GetAfflictionRewardMapModsDat()
    {
        afflictionrewardmapmodsdat ??= AfflictionRewardMapModsDat.Load(this).AsReadOnly();

        return afflictionrewardmapmodsdat;
    }

    /// <summary>
    /// Gets AfflictionRewardTypeVisualsDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionRewardTypeVisualsDat.</returns>
    public ReadOnlyCollection<AfflictionRewardTypeVisualsDat> GetAfflictionRewardTypeVisualsDat()
    {
        afflictionrewardtypevisualsdat ??= AfflictionRewardTypeVisualsDat.Load(this).AsReadOnly();

        return afflictionrewardtypevisualsdat;
    }

    /// <summary>
    /// Gets AfflictionSplitDemonsDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionSplitDemonsDat.</returns>
    public ReadOnlyCollection<AfflictionSplitDemonsDat> GetAfflictionSplitDemonsDat()
    {
        afflictionsplitdemonsdat ??= AfflictionSplitDemonsDat.Load(this).AsReadOnly();

        return afflictionsplitdemonsdat;
    }

    /// <summary>
    /// Gets AfflictionStartDialogueDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionStartDialogueDat.</returns>
    public ReadOnlyCollection<AfflictionStartDialogueDat> GetAfflictionStartDialogueDat()
    {
        afflictionstartdialoguedat ??= AfflictionStartDialogueDat.Load(this).AsReadOnly();

        return afflictionstartdialoguedat;
    }

    /// <summary>
    /// Gets HarvestCraftOptionIconsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestCraftOptionIconsDat.</returns>
    public ReadOnlyCollection<HarvestCraftOptionIconsDat> GetHarvestCraftOptionIconsDat()
    {
        harvestcraftoptioniconsdat ??= HarvestCraftOptionIconsDat.Load(this).AsReadOnly();

        return harvestcraftoptioniconsdat;
    }

    /// <summary>
    /// Gets HarvestCraftOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestCraftOptionsDat.</returns>
    public ReadOnlyCollection<HarvestCraftOptionsDat> GetHarvestCraftOptionsDat()
    {
        harvestcraftoptionsdat ??= HarvestCraftOptionsDat.Load(this).AsReadOnly();

        return harvestcraftoptionsdat;
    }

    /// <summary>
    /// Gets HarvestCraftTiersDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestCraftTiersDat.</returns>
    public ReadOnlyCollection<HarvestCraftTiersDat> GetHarvestCraftTiersDat()
    {
        harvestcrafttiersdat ??= HarvestCraftTiersDat.Load(this).AsReadOnly();

        return harvestcrafttiersdat;
    }

    /// <summary>
    /// Gets HarvestCraftFiltersDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestCraftFiltersDat.</returns>
    public ReadOnlyCollection<HarvestCraftFiltersDat> GetHarvestCraftFiltersDat()
    {
        harvestcraftfiltersdat ??= HarvestCraftFiltersDat.Load(this).AsReadOnly();

        return harvestcraftfiltersdat;
    }

    /// <summary>
    /// Gets HarvestDurabilityDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestDurabilityDat.</returns>
    public ReadOnlyCollection<HarvestDurabilityDat> GetHarvestDurabilityDat()
    {
        harvestdurabilitydat ??= HarvestDurabilityDat.Load(this).AsReadOnly();

        return harvestdurabilitydat;
    }

    /// <summary>
    /// Gets HarvestEncounterScalingDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestEncounterScalingDat.</returns>
    public ReadOnlyCollection<HarvestEncounterScalingDat> GetHarvestEncounterScalingDat()
    {
        harvestencounterscalingdat ??= HarvestEncounterScalingDat.Load(this).AsReadOnly();

        return harvestencounterscalingdat;
    }

    /// <summary>
    /// Gets HarvestInfrastructureDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestInfrastructureDat.</returns>
    public ReadOnlyCollection<HarvestInfrastructureDat> GetHarvestInfrastructureDat()
    {
        harvestinfrastructuredat ??= HarvestInfrastructureDat.Load(this).AsReadOnly();

        return harvestinfrastructuredat;
    }

    /// <summary>
    /// Gets HarvestObjectsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestObjectsDat.</returns>
    public ReadOnlyCollection<HarvestObjectsDat> GetHarvestObjectsDat()
    {
        harvestobjectsdat ??= HarvestObjectsDat.Load(this).AsReadOnly();

        return harvestobjectsdat;
    }

    /// <summary>
    /// Gets HarvestPerLevelValuesDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestPerLevelValuesDat.</returns>
    public ReadOnlyCollection<HarvestPerLevelValuesDat> GetHarvestPerLevelValuesDat()
    {
        harvestperlevelvaluesdat ??= HarvestPerLevelValuesDat.Load(this).AsReadOnly();

        return harvestperlevelvaluesdat;
    }

    /// <summary>
    /// Gets HarvestPlantBoostersDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestPlantBoostersDat.</returns>
    public ReadOnlyCollection<HarvestPlantBoostersDat> GetHarvestPlantBoostersDat()
    {
        harvestplantboostersdat ??= HarvestPlantBoostersDat.Load(this).AsReadOnly();

        return harvestplantboostersdat;
    }

    /// <summary>
    /// Gets HarvestLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<HarvestLifeScalingPerLevelDat> GetHarvestLifeScalingPerLevelDat()
    {
        harvestlifescalingperleveldat ??= HarvestLifeScalingPerLevelDat.Load(this).AsReadOnly();

        return harvestlifescalingperleveldat;
    }

    /// <summary>
    /// Gets HarvestSeedsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestSeedsDat.</returns>
    public ReadOnlyCollection<HarvestSeedsDat> GetHarvestSeedsDat()
    {
        harvestseedsdat ??= HarvestSeedsDat.Load(this).AsReadOnly();

        return harvestseedsdat;
    }

    /// <summary>
    /// Gets HarvestSeedItemsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestSeedItemsDat.</returns>
    public ReadOnlyCollection<HarvestSeedItemsDat> GetHarvestSeedItemsDat()
    {
        harvestseeditemsdat ??= HarvestSeedItemsDat.Load(this).AsReadOnly();

        return harvestseeditemsdat;
    }

    /// <summary>
    /// Gets HarvestSeedTypesDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestSeedTypesDat.</returns>
    public ReadOnlyCollection<HarvestSeedTypesDat> GetHarvestSeedTypesDat()
    {
        harvestseedtypesdat ??= HarvestSeedTypesDat.Load(this).AsReadOnly();

        return harvestseedtypesdat;
    }

    /// <summary>
    /// Gets HarvestSpecialCraftCostsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestSpecialCraftCostsDat.</returns>
    public ReadOnlyCollection<HarvestSpecialCraftCostsDat> GetHarvestSpecialCraftCostsDat()
    {
        harvestspecialcraftcostsdat ??= HarvestSpecialCraftCostsDat.Load(this).AsReadOnly();

        return harvestspecialcraftcostsdat;
    }

    /// <summary>
    /// Gets HarvestSpecialCraftOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestSpecialCraftOptionsDat.</returns>
    public ReadOnlyCollection<HarvestSpecialCraftOptionsDat> GetHarvestSpecialCraftOptionsDat()
    {
        harvestspecialcraftoptionsdat ??= HarvestSpecialCraftOptionsDat.Load(this).AsReadOnly();

        return harvestspecialcraftoptionsdat;
    }

    /// <summary>
    /// Gets HeistAreaFormationLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of HeistAreaFormationLayoutDat.</returns>
    public ReadOnlyCollection<HeistAreaFormationLayoutDat> GetHeistAreaFormationLayoutDat()
    {
        heistareaformationlayoutdat ??= HeistAreaFormationLayoutDat.Load(this).AsReadOnly();

        return heistareaformationlayoutdat;
    }

    /// <summary>
    /// Gets HeistAreasDat data.
    /// </summary>
    /// <returns>readonly collection of HeistAreasDat.</returns>
    public ReadOnlyCollection<HeistAreasDat> GetHeistAreasDat()
    {
        heistareasdat ??= HeistAreasDat.Load(this).AsReadOnly();

        return heistareasdat;
    }

    /// <summary>
    /// Gets HeistBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of HeistBalancePerLevelDat.</returns>
    public ReadOnlyCollection<HeistBalancePerLevelDat> GetHeistBalancePerLevelDat()
    {
        heistbalanceperleveldat ??= HeistBalancePerLevelDat.Load(this).AsReadOnly();

        return heistbalanceperleveldat;
    }

    /// <summary>
    /// Gets HeistChestRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of HeistChestRewardTypesDat.</returns>
    public ReadOnlyCollection<HeistChestRewardTypesDat> GetHeistChestRewardTypesDat()
    {
        heistchestrewardtypesdat ??= HeistChestRewardTypesDat.Load(this).AsReadOnly();

        return heistchestrewardtypesdat;
    }

    /// <summary>
    /// Gets HeistChestsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistChestsDat.</returns>
    public ReadOnlyCollection<HeistChestsDat> GetHeistChestsDat()
    {
        heistchestsdat ??= HeistChestsDat.Load(this).AsReadOnly();

        return heistchestsdat;
    }

    /// <summary>
    /// Gets HeistChokepointFormationDat data.
    /// </summary>
    /// <returns>readonly collection of HeistChokepointFormationDat.</returns>
    public ReadOnlyCollection<HeistChokepointFormationDat> GetHeistChokepointFormationDat()
    {
        heistchokepointformationdat ??= HeistChokepointFormationDat.Load(this).AsReadOnly();

        return heistchokepointformationdat;
    }

    /// <summary>
    /// Gets HeistConstantsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistConstantsDat.</returns>
    public ReadOnlyCollection<HeistConstantsDat> GetHeistConstantsDat()
    {
        heistconstantsdat ??= HeistConstantsDat.Load(this).AsReadOnly();

        return heistconstantsdat;
    }

    /// <summary>
    /// Gets HeistContractsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistContractsDat.</returns>
    public ReadOnlyCollection<HeistContractsDat> GetHeistContractsDat()
    {
        heistcontractsdat ??= HeistContractsDat.Load(this).AsReadOnly();

        return heistcontractsdat;
    }

    /// <summary>
    /// Gets HeistDoodadNPCsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistDoodadNPCsDat.</returns>
    public ReadOnlyCollection<HeistDoodadNPCsDat> GetHeistDoodadNPCsDat()
    {
        heistdoodadnpcsdat ??= HeistDoodadNPCsDat.Load(this).AsReadOnly();

        return heistdoodadnpcsdat;
    }

    /// <summary>
    /// Gets HeistDoorsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistDoorsDat.</returns>
    public ReadOnlyCollection<HeistDoorsDat> GetHeistDoorsDat()
    {
        heistdoorsdat ??= HeistDoorsDat.Load(this).AsReadOnly();

        return heistdoorsdat;
    }

    /// <summary>
    /// Gets HeistEquipmentDat data.
    /// </summary>
    /// <returns>readonly collection of HeistEquipmentDat.</returns>
    public ReadOnlyCollection<HeistEquipmentDat> GetHeistEquipmentDat()
    {
        heistequipmentdat ??= HeistEquipmentDat.Load(this).AsReadOnly();

        return heistequipmentdat;
    }

    /// <summary>
    /// Gets HeistGenerationDat data.
    /// </summary>
    /// <returns>readonly collection of HeistGenerationDat.</returns>
    public ReadOnlyCollection<HeistGenerationDat> GetHeistGenerationDat()
    {
        heistgenerationdat ??= HeistGenerationDat.Load(this).AsReadOnly();

        return heistgenerationdat;
    }

    /// <summary>
    /// Gets HeistIntroAreasDat data.
    /// </summary>
    /// <returns>readonly collection of HeistIntroAreasDat.</returns>
    public ReadOnlyCollection<HeistIntroAreasDat> GetHeistIntroAreasDat()
    {
        heistintroareasdat ??= HeistIntroAreasDat.Load(this).AsReadOnly();

        return heistintroareasdat;
    }

    /// <summary>
    /// Gets HeistJobsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistJobsDat.</returns>
    public ReadOnlyCollection<HeistJobsDat> GetHeistJobsDat()
    {
        heistjobsdat ??= HeistJobsDat.Load(this).AsReadOnly();

        return heistjobsdat;
    }

    /// <summary>
    /// Gets HeistJobsExperiencePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of HeistJobsExperiencePerLevelDat.</returns>
    public ReadOnlyCollection<HeistJobsExperiencePerLevelDat> GetHeistJobsExperiencePerLevelDat()
    {
        heistjobsexperienceperleveldat ??= HeistJobsExperiencePerLevelDat.Load(this).AsReadOnly();

        return heistjobsexperienceperleveldat;
    }

    /// <summary>
    /// Gets HeistLockTypeDat data.
    /// </summary>
    /// <returns>readonly collection of HeistLockTypeDat.</returns>
    public ReadOnlyCollection<HeistLockTypeDat> GetHeistLockTypeDat()
    {
        heistlocktypedat ??= HeistLockTypeDat.Load(this).AsReadOnly();

        return heistlocktypedat;
    }

    /// <summary>
    /// Gets HeistNPCAurasDat data.
    /// </summary>
    /// <returns>readonly collection of HeistNPCAurasDat.</returns>
    public ReadOnlyCollection<HeistNPCAurasDat> GetHeistNPCAurasDat()
    {
        heistnpcaurasdat ??= HeistNPCAurasDat.Load(this).AsReadOnly();

        return heistnpcaurasdat;
    }

    /// <summary>
    /// Gets HeistNPCBlueprintTypesDat data.
    /// </summary>
    /// <returns>readonly collection of HeistNPCBlueprintTypesDat.</returns>
    public ReadOnlyCollection<HeistNPCBlueprintTypesDat> GetHeistNPCBlueprintTypesDat()
    {
        heistnpcblueprinttypesdat ??= HeistNPCBlueprintTypesDat.Load(this).AsReadOnly();

        return heistnpcblueprinttypesdat;
    }

    /// <summary>
    /// Gets HeistNPCDialogueDat data.
    /// </summary>
    /// <returns>readonly collection of HeistNPCDialogueDat.</returns>
    public ReadOnlyCollection<HeistNPCDialogueDat> GetHeistNPCDialogueDat()
    {
        heistnpcdialoguedat ??= HeistNPCDialogueDat.Load(this).AsReadOnly();

        return heistnpcdialoguedat;
    }

    /// <summary>
    /// Gets HeistNPCsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistNPCsDat.</returns>
    public ReadOnlyCollection<HeistNPCsDat> GetHeistNPCsDat()
    {
        heistnpcsdat ??= HeistNPCsDat.Load(this).AsReadOnly();

        return heistnpcsdat;
    }

    /// <summary>
    /// Gets HeistNPCStatsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistNPCStatsDat.</returns>
    public ReadOnlyCollection<HeistNPCStatsDat> GetHeistNPCStatsDat()
    {
        heistnpcstatsdat ??= HeistNPCStatsDat.Load(this).AsReadOnly();

        return heistnpcstatsdat;
    }

    /// <summary>
    /// Gets HeistObjectivesDat data.
    /// </summary>
    /// <returns>readonly collection of HeistObjectivesDat.</returns>
    public ReadOnlyCollection<HeistObjectivesDat> GetHeistObjectivesDat()
    {
        heistobjectivesdat ??= HeistObjectivesDat.Load(this).AsReadOnly();

        return heistobjectivesdat;
    }

    /// <summary>
    /// Gets HeistObjectiveValueDescriptionsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistObjectiveValueDescriptionsDat.</returns>
    public ReadOnlyCollection<HeistObjectiveValueDescriptionsDat> GetHeistObjectiveValueDescriptionsDat()
    {
        heistobjectivevaluedescriptionsdat ??= HeistObjectiveValueDescriptionsDat.Load(this).AsReadOnly();

        return heistobjectivevaluedescriptionsdat;
    }

    /// <summary>
    /// Gets HeistPatrolPacksDat data.
    /// </summary>
    /// <returns>readonly collection of HeistPatrolPacksDat.</returns>
    public ReadOnlyCollection<HeistPatrolPacksDat> GetHeistPatrolPacksDat()
    {
        heistpatrolpacksdat ??= HeistPatrolPacksDat.Load(this).AsReadOnly();

        return heistpatrolpacksdat;
    }

    /// <summary>
    /// Gets HeistQuestContractsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistQuestContractsDat.</returns>
    public ReadOnlyCollection<HeistQuestContractsDat> GetHeistQuestContractsDat()
    {
        heistquestcontractsdat ??= HeistQuestContractsDat.Load(this).AsReadOnly();

        return heistquestcontractsdat;
    }

    /// <summary>
    /// Gets HeistRevealingNPCsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistRevealingNPCsDat.</returns>
    public ReadOnlyCollection<HeistRevealingNPCsDat> GetHeistRevealingNPCsDat()
    {
        heistrevealingnpcsdat ??= HeistRevealingNPCsDat.Load(this).AsReadOnly();

        return heistrevealingnpcsdat;
    }

    /// <summary>
    /// Gets HeistRoomsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistRoomsDat.</returns>
    public ReadOnlyCollection<HeistRoomsDat> GetHeistRoomsDat()
    {
        heistroomsdat ??= HeistRoomsDat.Load(this).AsReadOnly();

        return heistroomsdat;
    }

    /// <summary>
    /// Gets HeistValueScalingDat data.
    /// </summary>
    /// <returns>readonly collection of HeistValueScalingDat.</returns>
    public ReadOnlyCollection<HeistValueScalingDat> GetHeistValueScalingDat()
    {
        heistvaluescalingdat ??= HeistValueScalingDat.Load(this).AsReadOnly();

        return heistvaluescalingdat;
    }

    /// <summary>
    /// Gets InfluenceModUpgradesDat data.
    /// </summary>
    /// <returns>readonly collection of InfluenceModUpgradesDat.</returns>
    public ReadOnlyCollection<InfluenceModUpgradesDat> GetInfluenceModUpgradesDat()
    {
        influencemodupgradesdat ??= InfluenceModUpgradesDat.Load(this).AsReadOnly();

        return influencemodupgradesdat;
    }

    /// <summary>
    /// Gets MavenDialogDat data.
    /// </summary>
    /// <returns>readonly collection of MavenDialogDat.</returns>
    public ReadOnlyCollection<MavenDialogDat> GetMavenDialogDat()
    {
        mavendialogdat ??= MavenDialogDat.Load(this).AsReadOnly();

        return mavendialogdat;
    }

    /// <summary>
    /// Gets AtlasSkillGraphsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasSkillGraphsDat.</returns>
    public ReadOnlyCollection<AtlasSkillGraphsDat> GetAtlasSkillGraphsDat()
    {
        atlasskillgraphsdat ??= AtlasSkillGraphsDat.Load(this).AsReadOnly();

        return atlasskillgraphsdat;
    }

    /// <summary>
    /// Gets MavenFightsDat data.
    /// </summary>
    /// <returns>readonly collection of MavenFightsDat.</returns>
    public ReadOnlyCollection<MavenFightsDat> GetMavenFightsDat()
    {
        mavenfightsdat ??= MavenFightsDat.Load(this).AsReadOnly();

        return mavenfightsdat;
    }

    /// <summary>
    /// Gets MavenJewelRadiusKeystonesDat data.
    /// </summary>
    /// <returns>readonly collection of MavenJewelRadiusKeystonesDat.</returns>
    public ReadOnlyCollection<MavenJewelRadiusKeystonesDat> GetMavenJewelRadiusKeystonesDat()
    {
        mavenjewelradiuskeystonesdat ??= MavenJewelRadiusKeystonesDat.Load(this).AsReadOnly();

        return mavenjewelradiuskeystonesdat;
    }

    /// <summary>
    /// Gets RitualBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of RitualBalancePerLevelDat.</returns>
    public ReadOnlyCollection<RitualBalancePerLevelDat> GetRitualBalancePerLevelDat()
    {
        ritualbalanceperleveldat ??= RitualBalancePerLevelDat.Load(this).AsReadOnly();

        return ritualbalanceperleveldat;
    }

    /// <summary>
    /// Gets RitualConstantsDat data.
    /// </summary>
    /// <returns>readonly collection of RitualConstantsDat.</returns>
    public ReadOnlyCollection<RitualConstantsDat> GetRitualConstantsDat()
    {
        ritualconstantsdat ??= RitualConstantsDat.Load(this).AsReadOnly();

        return ritualconstantsdat;
    }

    /// <summary>
    /// Gets RitualRuneTypesDat data.
    /// </summary>
    /// <returns>readonly collection of RitualRuneTypesDat.</returns>
    public ReadOnlyCollection<RitualRuneTypesDat> GetRitualRuneTypesDat()
    {
        ritualrunetypesdat ??= RitualRuneTypesDat.Load(this).AsReadOnly();

        return ritualrunetypesdat;
    }

    /// <summary>
    /// Gets RitualSetKillAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of RitualSetKillAchievementsDat.</returns>
    public ReadOnlyCollection<RitualSetKillAchievementsDat> GetRitualSetKillAchievementsDat()
    {
        ritualsetkillachievementsdat ??= RitualSetKillAchievementsDat.Load(this).AsReadOnly();

        return ritualsetkillachievementsdat;
    }

    /// <summary>
    /// Gets RitualSpawnPatternsDat data.
    /// </summary>
    /// <returns>readonly collection of RitualSpawnPatternsDat.</returns>
    public ReadOnlyCollection<RitualSpawnPatternsDat> GetRitualSpawnPatternsDat()
    {
        ritualspawnpatternsdat ??= RitualSpawnPatternsDat.Load(this).AsReadOnly();

        return ritualspawnpatternsdat;
    }

    /// <summary>
    /// Gets UltimatumEncountersDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumEncountersDat.</returns>
    public ReadOnlyCollection<UltimatumEncountersDat> GetUltimatumEncountersDat()
    {
        ultimatumencountersdat ??= UltimatumEncountersDat.Load(this).AsReadOnly();

        return ultimatumencountersdat;
    }

    /// <summary>
    /// Gets UltimatumEncounterTypesDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumEncounterTypesDat.</returns>
    public ReadOnlyCollection<UltimatumEncounterTypesDat> GetUltimatumEncounterTypesDat()
    {
        ultimatumencountertypesdat ??= UltimatumEncounterTypesDat.Load(this).AsReadOnly();

        return ultimatumencountertypesdat;
    }

    /// <summary>
    /// Gets UltimatumItemisedRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumItemisedRewardsDat.</returns>
    public ReadOnlyCollection<UltimatumItemisedRewardsDat> GetUltimatumItemisedRewardsDat()
    {
        ultimatumitemisedrewardsdat ??= UltimatumItemisedRewardsDat.Load(this).AsReadOnly();

        return ultimatumitemisedrewardsdat;
    }

    /// <summary>
    /// Gets UltimatumMapModifiersDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumMapModifiersDat.</returns>
    public ReadOnlyCollection<UltimatumMapModifiersDat> GetUltimatumMapModifiersDat()
    {
        ultimatummapmodifiersdat ??= UltimatumMapModifiersDat.Load(this).AsReadOnly();

        return ultimatummapmodifiersdat;
    }

    /// <summary>
    /// Gets UltimatumModifiersDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumModifiersDat.</returns>
    public ReadOnlyCollection<UltimatumModifiersDat> GetUltimatumModifiersDat()
    {
        ultimatummodifiersdat ??= UltimatumModifiersDat.Load(this).AsReadOnly();

        return ultimatummodifiersdat;
    }

    /// <summary>
    /// Gets UltimatumModifierTypesDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumModifierTypesDat.</returns>
    public ReadOnlyCollection<UltimatumModifierTypesDat> GetUltimatumModifierTypesDat()
    {
        ultimatummodifiertypesdat ??= UltimatumModifierTypesDat.Load(this).AsReadOnly();

        return ultimatummodifiertypesdat;
    }

    /// <summary>
    /// Gets UltimatumTrialMasterAudioDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumTrialMasterAudioDat.</returns>
    public ReadOnlyCollection<UltimatumTrialMasterAudioDat> GetUltimatumTrialMasterAudioDat()
    {
        ultimatumtrialmasteraudiodat ??= UltimatumTrialMasterAudioDat.Load(this).AsReadOnly();

        return ultimatumtrialmasteraudiodat;
    }

    /// <summary>
    /// Gets ExpeditionAreasDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionAreasDat.</returns>
    public ReadOnlyCollection<ExpeditionAreasDat> GetExpeditionAreasDat()
    {
        expeditionareasdat ??= ExpeditionAreasDat.Load(this).AsReadOnly();

        return expeditionareasdat;
    }

    /// <summary>
    /// Gets ExpeditionBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionBalancePerLevelDat.</returns>
    public ReadOnlyCollection<ExpeditionBalancePerLevelDat> GetExpeditionBalancePerLevelDat()
    {
        expeditionbalanceperleveldat ??= ExpeditionBalancePerLevelDat.Load(this).AsReadOnly();

        return expeditionbalanceperleveldat;
    }

    /// <summary>
    /// Gets ExpeditionCurrencyDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionCurrencyDat.</returns>
    public ReadOnlyCollection<ExpeditionCurrencyDat> GetExpeditionCurrencyDat()
    {
        expeditioncurrencydat ??= ExpeditionCurrencyDat.Load(this).AsReadOnly();

        return expeditioncurrencydat;
    }

    /// <summary>
    /// Gets ExpeditionDealsDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionDealsDat.</returns>
    public ReadOnlyCollection<ExpeditionDealsDat> GetExpeditionDealsDat()
    {
        expeditiondealsdat ??= ExpeditionDealsDat.Load(this).AsReadOnly();

        return expeditiondealsdat;
    }

    /// <summary>
    /// Gets ExpeditionFactionsDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionFactionsDat.</returns>
    public ReadOnlyCollection<ExpeditionFactionsDat> GetExpeditionFactionsDat()
    {
        expeditionfactionsdat ??= ExpeditionFactionsDat.Load(this).AsReadOnly();

        return expeditionfactionsdat;
    }

    /// <summary>
    /// Gets ExpeditionMarkersCommonDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionMarkersCommonDat.</returns>
    public ReadOnlyCollection<ExpeditionMarkersCommonDat> GetExpeditionMarkersCommonDat()
    {
        expeditionmarkerscommondat ??= ExpeditionMarkersCommonDat.Load(this).AsReadOnly();

        return expeditionmarkerscommondat;
    }

    /// <summary>
    /// Gets ExpeditionNPCsDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionNPCsDat.</returns>
    public ReadOnlyCollection<ExpeditionNPCsDat> GetExpeditionNPCsDat()
    {
        expeditionnpcsdat ??= ExpeditionNPCsDat.Load(this).AsReadOnly();

        return expeditionnpcsdat;
    }

    /// <summary>
    /// Gets ExpeditionRelicModsDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionRelicModsDat.</returns>
    public ReadOnlyCollection<ExpeditionRelicModsDat> GetExpeditionRelicModsDat()
    {
        expeditionrelicmodsdat ??= ExpeditionRelicModsDat.Load(this).AsReadOnly();

        return expeditionrelicmodsdat;
    }

    /// <summary>
    /// Gets ExpeditionRelicsDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionRelicsDat.</returns>
    public ReadOnlyCollection<ExpeditionRelicsDat> GetExpeditionRelicsDat()
    {
        expeditionrelicsdat ??= ExpeditionRelicsDat.Load(this).AsReadOnly();

        return expeditionrelicsdat;
    }

    /// <summary>
    /// Gets ExpeditionStorageLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionStorageLayoutDat.</returns>
    public ReadOnlyCollection<ExpeditionStorageLayoutDat> GetExpeditionStorageLayoutDat()
    {
        expeditionstoragelayoutdat ??= ExpeditionStorageLayoutDat.Load(this).AsReadOnly();

        return expeditionstoragelayoutdat;
    }

    /// <summary>
    /// Gets ExpeditionTerrainFeaturesDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionTerrainFeaturesDat.</returns>
    public ReadOnlyCollection<ExpeditionTerrainFeaturesDat> GetExpeditionTerrainFeaturesDat()
    {
        expeditionterrainfeaturesdat ??= ExpeditionTerrainFeaturesDat.Load(this).AsReadOnly();

        return expeditionterrainfeaturesdat;
    }

    /// <summary>
    /// Gets HellscapeAOReplacementsDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeAOReplacementsDat.</returns>
    public ReadOnlyCollection<HellscapeAOReplacementsDat> GetHellscapeAOReplacementsDat()
    {
        hellscapeaoreplacementsdat ??= HellscapeAOReplacementsDat.Load(this).AsReadOnly();

        return hellscapeaoreplacementsdat;
    }

    /// <summary>
    /// Gets HellscapeAreaPacksDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeAreaPacksDat.</returns>
    public ReadOnlyCollection<HellscapeAreaPacksDat> GetHellscapeAreaPacksDat()
    {
        hellscapeareapacksdat ??= HellscapeAreaPacksDat.Load(this).AsReadOnly();

        return hellscapeareapacksdat;
    }

    /// <summary>
    /// Gets HellscapeExperienceLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeExperienceLevelsDat.</returns>
    public ReadOnlyCollection<HellscapeExperienceLevelsDat> GetHellscapeExperienceLevelsDat()
    {
        hellscapeexperiencelevelsdat ??= HellscapeExperienceLevelsDat.Load(this).AsReadOnly();

        return hellscapeexperiencelevelsdat;
    }

    /// <summary>
    /// Gets HellscapeFactionsDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeFactionsDat.</returns>
    public ReadOnlyCollection<HellscapeFactionsDat> GetHellscapeFactionsDat()
    {
        hellscapefactionsdat ??= HellscapeFactionsDat.Load(this).AsReadOnly();

        return hellscapefactionsdat;
    }

    /// <summary>
    /// Gets HellscapeImmuneMonstersDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeImmuneMonstersDat.</returns>
    public ReadOnlyCollection<HellscapeImmuneMonstersDat> GetHellscapeImmuneMonstersDat()
    {
        hellscapeimmunemonstersdat ??= HellscapeImmuneMonstersDat.Load(this).AsReadOnly();

        return hellscapeimmunemonstersdat;
    }

    /// <summary>
    /// Gets HellscapeItemModificationTiersDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeItemModificationTiersDat.</returns>
    public ReadOnlyCollection<HellscapeItemModificationTiersDat> GetHellscapeItemModificationTiersDat()
    {
        hellscapeitemmodificationtiersdat ??= HellscapeItemModificationTiersDat.Load(this).AsReadOnly();

        return hellscapeitemmodificationtiersdat;
    }

    /// <summary>
    /// Gets HellscapeLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<HellscapeLifeScalingPerLevelDat> GetHellscapeLifeScalingPerLevelDat()
    {
        hellscapelifescalingperleveldat ??= HellscapeLifeScalingPerLevelDat.Load(this).AsReadOnly();

        return hellscapelifescalingperleveldat;
    }

    /// <summary>
    /// Gets HellscapeModificationInventoryLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeModificationInventoryLayoutDat.</returns>
    public ReadOnlyCollection<HellscapeModificationInventoryLayoutDat> GetHellscapeModificationInventoryLayoutDat()
    {
        hellscapemodificationinventorylayoutdat ??= HellscapeModificationInventoryLayoutDat.Load(this).AsReadOnly();

        return hellscapemodificationinventorylayoutdat;
    }

    /// <summary>
    /// Gets HellscapeModsDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeModsDat.</returns>
    public ReadOnlyCollection<HellscapeModsDat> GetHellscapeModsDat()
    {
        hellscapemodsdat ??= HellscapeModsDat.Load(this).AsReadOnly();

        return hellscapemodsdat;
    }

    /// <summary>
    /// Gets HellscapeMonsterPacksDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeMonsterPacksDat.</returns>
    public ReadOnlyCollection<HellscapeMonsterPacksDat> GetHellscapeMonsterPacksDat()
    {
        hellscapemonsterpacksdat ??= HellscapeMonsterPacksDat.Load(this).AsReadOnly();

        return hellscapemonsterpacksdat;
    }

    /// <summary>
    /// Gets HellscapePassivesDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapePassivesDat.</returns>
    public ReadOnlyCollection<HellscapePassivesDat> GetHellscapePassivesDat()
    {
        hellscapepassivesdat ??= HellscapePassivesDat.Load(this).AsReadOnly();

        return hellscapepassivesdat;
    }

    /// <summary>
    /// Gets HellscapePassiveTreeDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapePassiveTreeDat.</returns>
    public ReadOnlyCollection<HellscapePassiveTreeDat> GetHellscapePassiveTreeDat()
    {
        hellscapepassivetreedat ??= HellscapePassiveTreeDat.Load(this).AsReadOnly();

        return hellscapepassivetreedat;
    }

    /// <summary>
    /// Gets ArchnemesisMetaRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of ArchnemesisMetaRewardsDat.</returns>
    public ReadOnlyCollection<ArchnemesisMetaRewardsDat> GetArchnemesisMetaRewardsDat()
    {
        archnemesismetarewardsdat ??= ArchnemesisMetaRewardsDat.Load(this).AsReadOnly();

        return archnemesismetarewardsdat;
    }

    /// <summary>
    /// Gets ArchnemesisModComboAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of ArchnemesisModComboAchievementsDat.</returns>
    public ReadOnlyCollection<ArchnemesisModComboAchievementsDat> GetArchnemesisModComboAchievementsDat()
    {
        archnemesismodcomboachievementsdat ??= ArchnemesisModComboAchievementsDat.Load(this).AsReadOnly();

        return archnemesismodcomboachievementsdat;
    }

    /// <summary>
    /// Gets ArchnemesisModsDat data.
    /// </summary>
    /// <returns>readonly collection of ArchnemesisModsDat.</returns>
    public ReadOnlyCollection<ArchnemesisModsDat> GetArchnemesisModsDat()
    {
        archnemesismodsdat ??= ArchnemesisModsDat.Load(this).AsReadOnly();

        return archnemesismodsdat;
    }

    /// <summary>
    /// Gets ArchnemesisModVisualsDat data.
    /// </summary>
    /// <returns>readonly collection of ArchnemesisModVisualsDat.</returns>
    public ReadOnlyCollection<ArchnemesisModVisualsDat> GetArchnemesisModVisualsDat()
    {
        archnemesismodvisualsdat ??= ArchnemesisModVisualsDat.Load(this).AsReadOnly();

        return archnemesismodvisualsdat;
    }

    /// <summary>
    /// Gets ArchnemesisRecipesDat data.
    /// </summary>
    /// <returns>readonly collection of ArchnemesisRecipesDat.</returns>
    public ReadOnlyCollection<ArchnemesisRecipesDat> GetArchnemesisRecipesDat()
    {
        archnemesisrecipesdat ??= ArchnemesisRecipesDat.Load(this).AsReadOnly();

        return archnemesisrecipesdat;
    }

    /// <summary>
    /// Gets AtlasPrimordialAltarChoicesDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPrimordialAltarChoicesDat.</returns>
    public ReadOnlyCollection<AtlasPrimordialAltarChoicesDat> GetAtlasPrimordialAltarChoicesDat()
    {
        atlasprimordialaltarchoicesdat ??= AtlasPrimordialAltarChoicesDat.Load(this).AsReadOnly();

        return atlasprimordialaltarchoicesdat;
    }

    /// <summary>
    /// Gets AtlasPrimordialAltarChoiceTypesDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPrimordialAltarChoiceTypesDat.</returns>
    public ReadOnlyCollection<AtlasPrimordialAltarChoiceTypesDat> GetAtlasPrimordialAltarChoiceTypesDat()
    {
        atlasprimordialaltarchoicetypesdat ??= AtlasPrimordialAltarChoiceTypesDat.Load(this).AsReadOnly();

        return atlasprimordialaltarchoicetypesdat;
    }

    /// <summary>
    /// Gets AtlasPrimordialBossesDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPrimordialBossesDat.</returns>
    public ReadOnlyCollection<AtlasPrimordialBossesDat> GetAtlasPrimordialBossesDat()
    {
        atlasprimordialbossesdat ??= AtlasPrimordialBossesDat.Load(this).AsReadOnly();

        return atlasprimordialbossesdat;
    }

    /// <summary>
    /// Gets AtlasPrimordialBossInfluenceDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPrimordialBossInfluenceDat.</returns>
    public ReadOnlyCollection<AtlasPrimordialBossInfluenceDat> GetAtlasPrimordialBossInfluenceDat()
    {
        atlasprimordialbossinfluencedat ??= AtlasPrimordialBossInfluenceDat.Load(this).AsReadOnly();

        return atlasprimordialbossinfluencedat;
    }

    /// <summary>
    /// Gets AtlasPrimordialBossOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPrimordialBossOptionsDat.</returns>
    public ReadOnlyCollection<AtlasPrimordialBossOptionsDat> GetAtlasPrimordialBossOptionsDat()
    {
        atlasprimordialbossoptionsdat ??= AtlasPrimordialBossOptionsDat.Load(this).AsReadOnly();

        return atlasprimordialbossoptionsdat;
    }

    /// <summary>
    /// Gets PrimordialBossLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of PrimordialBossLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<PrimordialBossLifeScalingPerLevelDat> GetPrimordialBossLifeScalingPerLevelDat()
    {
        primordialbosslifescalingperleveldat ??= PrimordialBossLifeScalingPerLevelDat.Load(this).AsReadOnly();

        return primordialbosslifescalingperleveldat;
    }

    /// <summary>
    /// Gets AtlasUpgradesInventoryLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasUpgradesInventoryLayoutDat.</returns>
    public ReadOnlyCollection<AtlasUpgradesInventoryLayoutDat> GetAtlasUpgradesInventoryLayoutDat()
    {
        atlasupgradesinventorylayoutdat ??= AtlasUpgradesInventoryLayoutDat.Load(this).AsReadOnly();

        return atlasupgradesinventorylayoutdat;
    }

    /// <summary>
    /// Gets AtlasPassiveSkillTreeGroupTypeDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPassiveSkillTreeGroupTypeDat.</returns>
    public ReadOnlyCollection<AtlasPassiveSkillTreeGroupTypeDat> GetAtlasPassiveSkillTreeGroupTypeDat()
    {
        atlaspassiveskilltreegrouptypedat ??= AtlasPassiveSkillTreeGroupTypeDat.Load(this).AsReadOnly();

        return atlaspassiveskilltreegrouptypedat;
    }

    /// <summary>
    /// Gets KiracLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of KiracLevelsDat.</returns>
    public ReadOnlyCollection<KiracLevelsDat> GetKiracLevelsDat()
    {
        kiraclevelsdat ??= KiracLevelsDat.Load(this).AsReadOnly();

        return kiraclevelsdat;
    }

    /// <summary>
    /// Gets ScoutingReportsDat data.
    /// </summary>
    /// <returns>readonly collection of ScoutingReportsDat.</returns>
    public ReadOnlyCollection<ScoutingReportsDat> GetScoutingReportsDat()
    {
        scoutingreportsdat ??= ScoutingReportsDat.Load(this).AsReadOnly();

        return scoutingreportsdat;
    }

    /// <summary>
    /// Gets DroneBaseTypesDat data.
    /// </summary>
    /// <returns>readonly collection of DroneBaseTypesDat.</returns>
    public ReadOnlyCollection<DroneBaseTypesDat> GetDroneBaseTypesDat()
    {
        dronebasetypesdat ??= DroneBaseTypesDat.Load(this).AsReadOnly();

        return dronebasetypesdat;
    }

    /// <summary>
    /// Gets DroneTypesDat data.
    /// </summary>
    /// <returns>readonly collection of DroneTypesDat.</returns>
    public ReadOnlyCollection<DroneTypesDat> GetDroneTypesDat()
    {
        dronetypesdat ??= DroneTypesDat.Load(this).AsReadOnly();

        return dronetypesdat;
    }

    /// <summary>
    /// Gets SentinelCraftingCurrencyDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelCraftingCurrencyDat.</returns>
    public ReadOnlyCollection<SentinelCraftingCurrencyDat> GetSentinelCraftingCurrencyDat()
    {
        sentinelcraftingcurrencydat ??= SentinelCraftingCurrencyDat.Load(this).AsReadOnly();

        return sentinelcraftingcurrencydat;
    }

    /// <summary>
    /// Gets SentinelDroneInventoryLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelDroneInventoryLayoutDat.</returns>
    public ReadOnlyCollection<SentinelDroneInventoryLayoutDat> GetSentinelDroneInventoryLayoutDat()
    {
        sentineldroneinventorylayoutdat ??= SentinelDroneInventoryLayoutDat.Load(this).AsReadOnly();

        return sentineldroneinventorylayoutdat;
    }

    /// <summary>
    /// Gets SentinelPassivesDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelPassivesDat.</returns>
    public ReadOnlyCollection<SentinelPassivesDat> GetSentinelPassivesDat()
    {
        sentinelpassivesdat ??= SentinelPassivesDat.Load(this).AsReadOnly();

        return sentinelpassivesdat;
    }

    /// <summary>
    /// Gets SentinelPassiveStatsDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelPassiveStatsDat.</returns>
    public ReadOnlyCollection<SentinelPassiveStatsDat> GetSentinelPassiveStatsDat()
    {
        sentinelpassivestatsdat ??= SentinelPassiveStatsDat.Load(this).AsReadOnly();

        return sentinelpassivestatsdat;
    }

    /// <summary>
    /// Gets SentinelPassiveTypesDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelPassiveTypesDat.</returns>
    public ReadOnlyCollection<SentinelPassiveTypesDat> GetSentinelPassiveTypesDat()
    {
        sentinelpassivetypesdat ??= SentinelPassiveTypesDat.Load(this).AsReadOnly();

        return sentinelpassivetypesdat;
    }

    /// <summary>
    /// Gets SentinelPowerExpLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelPowerExpLevelsDat.</returns>
    public ReadOnlyCollection<SentinelPowerExpLevelsDat> GetSentinelPowerExpLevelsDat()
    {
        sentinelpowerexplevelsdat ??= SentinelPowerExpLevelsDat.Load(this).AsReadOnly();

        return sentinelpowerexplevelsdat;
    }

    /// <summary>
    /// Gets SentinelStorageLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelStorageLayoutDat.</returns>
    public ReadOnlyCollection<SentinelStorageLayoutDat> GetSentinelStorageLayoutDat()
    {
        sentinelstoragelayoutdat ??= SentinelStorageLayoutDat.Load(this).AsReadOnly();

        return sentinelstoragelayoutdat;
    }

    /// <summary>
    /// Gets SentinelTaggedMonsterStatsDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelTaggedMonsterStatsDat.</returns>
    public ReadOnlyCollection<SentinelTaggedMonsterStatsDat> GetSentinelTaggedMonsterStatsDat()
    {
        sentineltaggedmonsterstatsdat ??= SentinelTaggedMonsterStatsDat.Load(this).AsReadOnly();

        return sentineltaggedmonsterstatsdat;
    }

    /// <summary>
    /// Gets ClientLakeDifficultyDat data.
    /// </summary>
    /// <returns>readonly collection of ClientLakeDifficultyDat.</returns>
    public ReadOnlyCollection<ClientLakeDifficultyDat> GetClientLakeDifficultyDat()
    {
        clientlakedifficultydat ??= ClientLakeDifficultyDat.Load(this).AsReadOnly();

        return clientlakedifficultydat;
    }

    /// <summary>
    /// Gets LakeBossLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of LakeBossLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<LakeBossLifeScalingPerLevelDat> GetLakeBossLifeScalingPerLevelDat()
    {
        lakebosslifescalingperleveldat ??= LakeBossLifeScalingPerLevelDat.Load(this).AsReadOnly();

        return lakebosslifescalingperleveldat;
    }

    /// <summary>
    /// Gets LakeMetaOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of LakeMetaOptionsDat.</returns>
    public ReadOnlyCollection<LakeMetaOptionsDat> GetLakeMetaOptionsDat()
    {
        lakemetaoptionsdat ??= LakeMetaOptionsDat.Load(this).AsReadOnly();

        return lakemetaoptionsdat;
    }

    /// <summary>
    /// Gets LakeMetaOptionsUnlockTextDat data.
    /// </summary>
    /// <returns>readonly collection of LakeMetaOptionsUnlockTextDat.</returns>
    public ReadOnlyCollection<LakeMetaOptionsUnlockTextDat> GetLakeMetaOptionsUnlockTextDat()
    {
        lakemetaoptionsunlocktextdat ??= LakeMetaOptionsUnlockTextDat.Load(this).AsReadOnly();

        return lakemetaoptionsunlocktextdat;
    }

    /// <summary>
    /// Gets LakeRoomCompletionDat data.
    /// </summary>
    /// <returns>readonly collection of LakeRoomCompletionDat.</returns>
    public ReadOnlyCollection<LakeRoomCompletionDat> GetLakeRoomCompletionDat()
    {
        lakeroomcompletiondat ??= LakeRoomCompletionDat.Load(this).AsReadOnly();

        return lakeroomcompletiondat;
    }

    /// <summary>
    /// Gets LakeRoomsDat data.
    /// </summary>
    /// <returns>readonly collection of LakeRoomsDat.</returns>
    public ReadOnlyCollection<LakeRoomsDat> GetLakeRoomsDat()
    {
        lakeroomsdat ??= LakeRoomsDat.Load(this).AsReadOnly();

        return lakeroomsdat;
    }

    /// <summary>
    /// Gets AchievementItemRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of AchievementItemRewardsDat.</returns>
    public ReadOnlyCollection<AchievementItemRewardsDat> GetAchievementItemRewardsDat()
    {
        achievementitemrewardsdat ??= AchievementItemRewardsDat.Load(this).AsReadOnly();

        return achievementitemrewardsdat;
    }

    /// <summary>
    /// Gets AchievementItemsDat data.
    /// </summary>
    /// <returns>readonly collection of AchievementItemsDat.</returns>
    public ReadOnlyCollection<AchievementItemsDat> GetAchievementItemsDat()
    {
        achievementitemsdat ??= AchievementItemsDat.Load(this).AsReadOnly();

        return achievementitemsdat;
    }

    /// <summary>
    /// Gets AchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of AchievementsDat.</returns>
    public ReadOnlyCollection<AchievementsDat> GetAchievementsDat()
    {
        achievementsdat ??= AchievementsDat.Load(this).AsReadOnly();

        return achievementsdat;
    }

    /// <summary>
    /// Gets AchievementSetRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of AchievementSetRewardsDat.</returns>
    public ReadOnlyCollection<AchievementSetRewardsDat> GetAchievementSetRewardsDat()
    {
        achievementsetrewardsdat ??= AchievementSetRewardsDat.Load(this).AsReadOnly();

        return achievementsetrewardsdat;
    }

    /// <summary>
    /// Gets AchievementSetsDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of AchievementSetsDisplayDat.</returns>
    public ReadOnlyCollection<AchievementSetsDisplayDat> GetAchievementSetsDisplayDat()
    {
        achievementsetsdisplaydat ??= AchievementSetsDisplayDat.Load(this).AsReadOnly();

        return achievementsetsdisplaydat;
    }

    /// <summary>
    /// Gets ActiveSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of ActiveSkillsDat.</returns>
    public ReadOnlyCollection<ActiveSkillsDat> GetActiveSkillsDat()
    {
        activeskillsdat ??= ActiveSkillsDat.Load(this).AsReadOnly();

        return activeskillsdat;
    }

    /// <summary>
    /// Gets ActiveSkillTypeDat data.
    /// </summary>
    /// <returns>readonly collection of ActiveSkillTypeDat.</returns>
    public ReadOnlyCollection<ActiveSkillTypeDat> GetActiveSkillTypeDat()
    {
        activeskilltypedat ??= ActiveSkillTypeDat.Load(this).AsReadOnly();

        return activeskilltypedat;
    }

    /// <summary>
    /// Gets ActsDat data.
    /// </summary>
    /// <returns>readonly collection of ActsDat.</returns>
    public ReadOnlyCollection<ActsDat> GetActsDat()
    {
        actsdat ??= ActsDat.Load(this).AsReadOnly();

        return actsdat;
    }

    /// <summary>
    /// Gets AddBuffToTargetVarietiesDat data.
    /// </summary>
    /// <returns>readonly collection of AddBuffToTargetVarietiesDat.</returns>
    public ReadOnlyCollection<AddBuffToTargetVarietiesDat> GetAddBuffToTargetVarietiesDat()
    {
        addbufftotargetvarietiesdat ??= AddBuffToTargetVarietiesDat.Load(this).AsReadOnly();

        return addbufftotargetvarietiesdat;
    }

    /// <summary>
    /// Gets AdditionalLifeScalingDat data.
    /// </summary>
    /// <returns>readonly collection of AdditionalLifeScalingDat.</returns>
    public ReadOnlyCollection<AdditionalLifeScalingDat> GetAdditionalLifeScalingDat()
    {
        additionallifescalingdat ??= AdditionalLifeScalingDat.Load(this).AsReadOnly();

        return additionallifescalingdat;
    }

    /// <summary>
    /// Gets AdditionalMonsterPacksFromStatsDat data.
    /// </summary>
    /// <returns>readonly collection of AdditionalMonsterPacksFromStatsDat.</returns>
    public ReadOnlyCollection<AdditionalMonsterPacksFromStatsDat> GetAdditionalMonsterPacksFromStatsDat()
    {
        additionalmonsterpacksfromstatsdat ??= AdditionalMonsterPacksFromStatsDat.Load(this).AsReadOnly();

        return additionalmonsterpacksfromstatsdat;
    }

    /// <summary>
    /// Gets AdvancedSkillsTutorialDat data.
    /// </summary>
    /// <returns>readonly collection of AdvancedSkillsTutorialDat.</returns>
    public ReadOnlyCollection<AdvancedSkillsTutorialDat> GetAdvancedSkillsTutorialDat()
    {
        advancedskillstutorialdat ??= AdvancedSkillsTutorialDat.Load(this).AsReadOnly();

        return advancedskillstutorialdat;
    }

    /// <summary>
    /// Gets AegisVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of AegisVariationsDat.</returns>
    public ReadOnlyCollection<AegisVariationsDat> GetAegisVariationsDat()
    {
        aegisvariationsdat ??= AegisVariationsDat.Load(this).AsReadOnly();

        return aegisvariationsdat;
    }

    /// <summary>
    /// Gets AlternatePassiveAdditionsDat data.
    /// </summary>
    /// <returns>readonly collection of AlternatePassiveAdditionsDat.</returns>
    public ReadOnlyCollection<AlternatePassiveAdditionsDat> GetAlternatePassiveAdditionsDat()
    {
        alternatepassiveadditionsdat ??= AlternatePassiveAdditionsDat.Load(this).AsReadOnly();

        return alternatepassiveadditionsdat;
    }

    /// <summary>
    /// Gets AlternatePassiveSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of AlternatePassiveSkillsDat.</returns>
    public ReadOnlyCollection<AlternatePassiveSkillsDat> GetAlternatePassiveSkillsDat()
    {
        alternatepassiveskillsdat ??= AlternatePassiveSkillsDat.Load(this).AsReadOnly();

        return alternatepassiveskillsdat;
    }

    /// <summary>
    /// Gets AlternateSkillTargetingBehavioursDat data.
    /// </summary>
    /// <returns>readonly collection of AlternateSkillTargetingBehavioursDat.</returns>
    public ReadOnlyCollection<AlternateSkillTargetingBehavioursDat> GetAlternateSkillTargetingBehavioursDat()
    {
        alternateskilltargetingbehavioursdat ??= AlternateSkillTargetingBehavioursDat.Load(this).AsReadOnly();

        return alternateskilltargetingbehavioursdat;
    }

    /// <summary>
    /// Gets AlternateTreeVersionsDat data.
    /// </summary>
    /// <returns>readonly collection of AlternateTreeVersionsDat.</returns>
    public ReadOnlyCollection<AlternateTreeVersionsDat> GetAlternateTreeVersionsDat()
    {
        alternatetreeversionsdat ??= AlternateTreeVersionsDat.Load(this).AsReadOnly();

        return alternatetreeversionsdat;
    }

    /// <summary>
    /// Gets AnimatedObjectFlagsDat data.
    /// </summary>
    /// <returns>readonly collection of AnimatedObjectFlagsDat.</returns>
    public ReadOnlyCollection<AnimatedObjectFlagsDat> GetAnimatedObjectFlagsDat()
    {
        animatedobjectflagsdat ??= AnimatedObjectFlagsDat.Load(this).AsReadOnly();

        return animatedobjectflagsdat;
    }

    /// <summary>
    /// Gets AnimationDat data.
    /// </summary>
    /// <returns>readonly collection of AnimationDat.</returns>
    public ReadOnlyCollection<AnimationDat> GetAnimationDat()
    {
        animationdat ??= AnimationDat.Load(this).AsReadOnly();

        return animationdat;
    }

    /// <summary>
    /// Gets ApplyDamageFunctionsDat data.
    /// </summary>
    /// <returns>readonly collection of ApplyDamageFunctionsDat.</returns>
    public ReadOnlyCollection<ApplyDamageFunctionsDat> GetApplyDamageFunctionsDat()
    {
        applydamagefunctionsdat ??= ApplyDamageFunctionsDat.Load(this).AsReadOnly();

        return applydamagefunctionsdat;
    }

    /// <summary>
    /// Gets ArchetypeRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of ArchetypeRewardsDat.</returns>
    public ReadOnlyCollection<ArchetypeRewardsDat> GetArchetypeRewardsDat()
    {
        archetyperewardsdat ??= ArchetypeRewardsDat.Load(this).AsReadOnly();

        return archetyperewardsdat;
    }

    /// <summary>
    /// Gets ArchetypesDat data.
    /// </summary>
    /// <returns>readonly collection of ArchetypesDat.</returns>
    public ReadOnlyCollection<ArchetypesDat> GetArchetypesDat()
    {
        archetypesdat ??= ArchetypesDat.Load(this).AsReadOnly();

        return archetypesdat;
    }

    /// <summary>
    /// Gets AreaInfluenceDoodadsDat data.
    /// </summary>
    /// <returns>readonly collection of AreaInfluenceDoodadsDat.</returns>
    public ReadOnlyCollection<AreaInfluenceDoodadsDat> GetAreaInfluenceDoodadsDat()
    {
        areainfluencedoodadsdat ??= AreaInfluenceDoodadsDat.Load(this).AsReadOnly();

        return areainfluencedoodadsdat;
    }

    /// <summary>
    /// Gets AreaTransitionAnimationsDat data.
    /// </summary>
    /// <returns>readonly collection of AreaTransitionAnimationsDat.</returns>
    public ReadOnlyCollection<AreaTransitionAnimationsDat> GetAreaTransitionAnimationsDat()
    {
        areatransitionanimationsdat ??= AreaTransitionAnimationsDat.Load(this).AsReadOnly();

        return areatransitionanimationsdat;
    }

    /// <summary>
    /// Gets AreaTransitionAnimationTypesDat data.
    /// </summary>
    /// <returns>readonly collection of AreaTransitionAnimationTypesDat.</returns>
    public ReadOnlyCollection<AreaTransitionAnimationTypesDat> GetAreaTransitionAnimationTypesDat()
    {
        areatransitionanimationtypesdat ??= AreaTransitionAnimationTypesDat.Load(this).AsReadOnly();

        return areatransitionanimationtypesdat;
    }

    /// <summary>
    /// Gets AreaTransitionInfoDat data.
    /// </summary>
    /// <returns>readonly collection of AreaTransitionInfoDat.</returns>
    public ReadOnlyCollection<AreaTransitionInfoDat> GetAreaTransitionInfoDat()
    {
        areatransitioninfodat ??= AreaTransitionInfoDat.Load(this).AsReadOnly();

        return areatransitioninfodat;
    }

    /// <summary>
    /// Gets ArmourTypesDat data.
    /// </summary>
    /// <returns>readonly collection of ArmourTypesDat.</returns>
    public ReadOnlyCollection<ArmourTypesDat> GetArmourTypesDat()
    {
        armourtypesdat ??= ArmourTypesDat.Load(this).AsReadOnly();

        return armourtypesdat;
    }

    /// <summary>
    /// Gets AscendancyDat data.
    /// </summary>
    /// <returns>readonly collection of AscendancyDat.</returns>
    public ReadOnlyCollection<AscendancyDat> GetAscendancyDat()
    {
        ascendancydat ??= AscendancyDat.Load(this).AsReadOnly();

        return ascendancydat;
    }

    /// <summary>
    /// Gets AtlasAwakeningStatsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasAwakeningStatsDat.</returns>
    public ReadOnlyCollection<AtlasAwakeningStatsDat> GetAtlasAwakeningStatsDat()
    {
        atlasawakeningstatsdat ??= AtlasAwakeningStatsDat.Load(this).AsReadOnly();

        return atlasawakeningstatsdat;
    }

    /// <summary>
    /// Gets AtlasBaseTypeDropsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasBaseTypeDropsDat.</returns>
    public ReadOnlyCollection<AtlasBaseTypeDropsDat> GetAtlasBaseTypeDropsDat()
    {
        atlasbasetypedropsdat ??= AtlasBaseTypeDropsDat.Load(this).AsReadOnly();

        return atlasbasetypedropsdat;
    }

    /// <summary>
    /// Gets AtlasFogDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasFogDat.</returns>
    public ReadOnlyCollection<AtlasFogDat> GetAtlasFogDat()
    {
        atlasfogdat ??= AtlasFogDat.Load(this).AsReadOnly();

        return atlasfogdat;
    }

    /// <summary>
    /// Gets AtlasInfluenceDataDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasInfluenceDataDat.</returns>
    public ReadOnlyCollection<AtlasInfluenceDataDat> GetAtlasInfluenceDataDat()
    {
        atlasinfluencedatadat ??= AtlasInfluenceDataDat.Load(this).AsReadOnly();

        return atlasinfluencedatadat;
    }

    /// <summary>
    /// Gets AtlasInfluenceOutcomesDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasInfluenceOutcomesDat.</returns>
    public ReadOnlyCollection<AtlasInfluenceOutcomesDat> GetAtlasInfluenceOutcomesDat()
    {
        atlasinfluenceoutcomesdat ??= AtlasInfluenceOutcomesDat.Load(this).AsReadOnly();

        return atlasinfluenceoutcomesdat;
    }

    /// <summary>
    /// Gets AtlasInfluenceSetsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasInfluenceSetsDat.</returns>
    public ReadOnlyCollection<AtlasInfluenceSetsDat> GetAtlasInfluenceSetsDat()
    {
        atlasinfluencesetsdat ??= AtlasInfluenceSetsDat.Load(this).AsReadOnly();

        return atlasinfluencesetsdat;
    }

    /// <summary>
    /// Gets AtlasModsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasModsDat.</returns>
    public ReadOnlyCollection<AtlasModsDat> GetAtlasModsDat()
    {
        atlasmodsdat ??= AtlasModsDat.Load(this).AsReadOnly();

        return atlasmodsdat;
    }

    /// <summary>
    /// Gets AtlasFavouredMapSlotsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasFavouredMapSlotsDat.</returns>
    public ReadOnlyCollection<AtlasFavouredMapSlotsDat> GetAtlasFavouredMapSlotsDat()
    {
        atlasfavouredmapslotsdat ??= AtlasFavouredMapSlotsDat.Load(this).AsReadOnly();

        return atlasfavouredmapslotsdat;
    }

    /// <summary>
    /// Gets AtlasNodeDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasNodeDat.</returns>
    public ReadOnlyCollection<AtlasNodeDat> GetAtlasNodeDat()
    {
        atlasnodedat ??= AtlasNodeDat.Load(this).AsReadOnly();

        return atlasnodedat;
    }

    /// <summary>
    /// Gets AtlasNodeDefinitionDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasNodeDefinitionDat.</returns>
    public ReadOnlyCollection<AtlasNodeDefinitionDat> GetAtlasNodeDefinitionDat()
    {
        atlasnodedefinitiondat ??= AtlasNodeDefinitionDat.Load(this).AsReadOnly();

        return atlasnodedefinitiondat;
    }

    /// <summary>
    /// Gets AtlasPositionsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPositionsDat.</returns>
    public ReadOnlyCollection<AtlasPositionsDat> GetAtlasPositionsDat()
    {
        atlaspositionsdat ??= AtlasPositionsDat.Load(this).AsReadOnly();

        return atlaspositionsdat;
    }

    /// <summary>
    /// Gets AtlasRegionsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasRegionsDat.</returns>
    public ReadOnlyCollection<AtlasRegionsDat> GetAtlasRegionsDat()
    {
        atlasregionsdat ??= AtlasRegionsDat.Load(this).AsReadOnly();

        return atlasregionsdat;
    }

    /// <summary>
    /// Gets AtlasRegionUpgradesInventoryLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasRegionUpgradesInventoryLayoutDat.</returns>
    public ReadOnlyCollection<AtlasRegionUpgradesInventoryLayoutDat> GetAtlasRegionUpgradesInventoryLayoutDat()
    {
        atlasregionupgradesinventorylayoutdat ??= AtlasRegionUpgradesInventoryLayoutDat.Load(this).AsReadOnly();

        return atlasregionupgradesinventorylayoutdat;
    }

    /// <summary>
    /// Gets AtlasRegionUpgradeRegionsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasRegionUpgradeRegionsDat.</returns>
    public ReadOnlyCollection<AtlasRegionUpgradeRegionsDat> GetAtlasRegionUpgradeRegionsDat()
    {
        atlasregionupgraderegionsdat ??= AtlasRegionUpgradeRegionsDat.Load(this).AsReadOnly();

        return atlasregionupgraderegionsdat;
    }

    /// <summary>
    /// Gets AtlasSectorDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasSectorDat.</returns>
    public ReadOnlyCollection<AtlasSectorDat> GetAtlasSectorDat()
    {
        atlassectordat ??= AtlasSectorDat.Load(this).AsReadOnly();

        return atlassectordat;
    }

    /// <summary>
    /// Gets AwardDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of AwardDisplayDat.</returns>
    public ReadOnlyCollection<AwardDisplayDat> GetAwardDisplayDat()
    {
        awarddisplaydat ??= AwardDisplayDat.Load(this).AsReadOnly();

        return awarddisplaydat;
    }

    /// <summary>
    /// Gets BackendErrorsDat data.
    /// </summary>
    /// <returns>readonly collection of BackendErrorsDat.</returns>
    public ReadOnlyCollection<BackendErrorsDat> GetBackendErrorsDat()
    {
        backenderrorsdat ??= BackendErrorsDat.Load(this).AsReadOnly();

        return backenderrorsdat;
    }

    /// <summary>
    /// Gets BaseItemTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BaseItemTypesDat.</returns>
    public ReadOnlyCollection<BaseItemTypesDat> GetBaseItemTypesDat()
    {
        baseitemtypesdat ??= BaseItemTypesDat.Load(this).AsReadOnly();

        return baseitemtypesdat;
    }

    /// <summary>
    /// Gets BindableVirtualKeysDat data.
    /// </summary>
    /// <returns>readonly collection of BindableVirtualKeysDat.</returns>
    public ReadOnlyCollection<BindableVirtualKeysDat> GetBindableVirtualKeysDat()
    {
        bindablevirtualkeysdat ??= BindableVirtualKeysDat.Load(this).AsReadOnly();

        return bindablevirtualkeysdat;
    }

    /// <summary>
    /// Gets BlightStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of BlightStashTabLayoutDat.</returns>
    public ReadOnlyCollection<BlightStashTabLayoutDat> GetBlightStashTabLayoutDat()
    {
        blightstashtablayoutdat ??= BlightStashTabLayoutDat.Load(this).AsReadOnly();

        return blightstashtablayoutdat;
    }

    /// <summary>
    /// Gets BloodTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BloodTypesDat.</returns>
    public ReadOnlyCollection<BloodTypesDat> GetBloodTypesDat()
    {
        bloodtypesdat ??= BloodTypesDat.Load(this).AsReadOnly();

        return bloodtypesdat;
    }

    /// <summary>
    /// Gets BuffDefinitionsDat data.
    /// </summary>
    /// <returns>readonly collection of BuffDefinitionsDat.</returns>
    public ReadOnlyCollection<BuffDefinitionsDat> GetBuffDefinitionsDat()
    {
        buffdefinitionsdat ??= BuffDefinitionsDat.Load(this).AsReadOnly();

        return buffdefinitionsdat;
    }

    /// <summary>
    /// Gets BuffTemplatesDat data.
    /// </summary>
    /// <returns>readonly collection of BuffTemplatesDat.</returns>
    public ReadOnlyCollection<BuffTemplatesDat> GetBuffTemplatesDat()
    {
        bufftemplatesdat ??= BuffTemplatesDat.Load(this).AsReadOnly();

        return bufftemplatesdat;
    }

    /// <summary>
    /// Gets BuffVisualOrbArtDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualOrbArtDat.</returns>
    public ReadOnlyCollection<BuffVisualOrbArtDat> GetBuffVisualOrbArtDat()
    {
        buffvisualorbartdat ??= BuffVisualOrbArtDat.Load(this).AsReadOnly();

        return buffvisualorbartdat;
    }

    /// <summary>
    /// Gets BuffVisualOrbsDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualOrbsDat.</returns>
    public ReadOnlyCollection<BuffVisualOrbsDat> GetBuffVisualOrbsDat()
    {
        buffvisualorbsdat ??= BuffVisualOrbsDat.Load(this).AsReadOnly();

        return buffvisualorbsdat;
    }

    /// <summary>
    /// Gets BuffVisualOrbTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualOrbTypesDat.</returns>
    public ReadOnlyCollection<BuffVisualOrbTypesDat> GetBuffVisualOrbTypesDat()
    {
        buffvisualorbtypesdat ??= BuffVisualOrbTypesDat.Load(this).AsReadOnly();

        return buffvisualorbtypesdat;
    }

    /// <summary>
    /// Gets BuffVisualsDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualsDat.</returns>
    public ReadOnlyCollection<BuffVisualsDat> GetBuffVisualsDat()
    {
        buffvisualsdat ??= BuffVisualsDat.Load(this).AsReadOnly();

        return buffvisualsdat;
    }

    /// <summary>
    /// Gets BuffVisualsArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualsArtVariationsDat.</returns>
    public ReadOnlyCollection<BuffVisualsArtVariationsDat> GetBuffVisualsArtVariationsDat()
    {
        buffvisualsartvariationsdat ??= BuffVisualsArtVariationsDat.Load(this).AsReadOnly();

        return buffvisualsartvariationsdat;
    }

    /// <summary>
    /// Gets BuffVisualSetEntriesDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualSetEntriesDat.</returns>
    public ReadOnlyCollection<BuffVisualSetEntriesDat> GetBuffVisualSetEntriesDat()
    {
        buffvisualsetentriesdat ??= BuffVisualSetEntriesDat.Load(this).AsReadOnly();

        return buffvisualsetentriesdat;
    }

    /// <summary>
    /// Gets CharacterAudioEventsDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterAudioEventsDat.</returns>
    public ReadOnlyCollection<CharacterAudioEventsDat> GetCharacterAudioEventsDat()
    {
        characteraudioeventsdat ??= CharacterAudioEventsDat.Load(this).AsReadOnly();

        return characteraudioeventsdat;
    }

    /// <summary>
    /// Gets CharacterEventTextAudioDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterEventTextAudioDat.</returns>
    public ReadOnlyCollection<CharacterEventTextAudioDat> GetCharacterEventTextAudioDat()
    {
        charactereventtextaudiodat ??= CharacterEventTextAudioDat.Load(this).AsReadOnly();

        return charactereventtextaudiodat;
    }

    /// <summary>
    /// Gets CharacterPanelDescriptionModesDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterPanelDescriptionModesDat.</returns>
    public ReadOnlyCollection<CharacterPanelDescriptionModesDat> GetCharacterPanelDescriptionModesDat()
    {
        characterpaneldescriptionmodesdat ??= CharacterPanelDescriptionModesDat.Load(this).AsReadOnly();

        return characterpaneldescriptionmodesdat;
    }

    /// <summary>
    /// Gets CharacterPanelStatsDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterPanelStatsDat.</returns>
    public ReadOnlyCollection<CharacterPanelStatsDat> GetCharacterPanelStatsDat()
    {
        characterpanelstatsdat ??= CharacterPanelStatsDat.Load(this).AsReadOnly();

        return characterpanelstatsdat;
    }

    /// <summary>
    /// Gets CharacterPanelTabsDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterPanelTabsDat.</returns>
    public ReadOnlyCollection<CharacterPanelTabsDat> GetCharacterPanelTabsDat()
    {
        characterpaneltabsdat ??= CharacterPanelTabsDat.Load(this).AsReadOnly();

        return characterpaneltabsdat;
    }

    /// <summary>
    /// Gets CharactersDat data.
    /// </summary>
    /// <returns>readonly collection of CharactersDat.</returns>
    public ReadOnlyCollection<CharactersDat> GetCharactersDat()
    {
        charactersdat ??= CharactersDat.Load(this).AsReadOnly();

        return charactersdat;
    }

    /// <summary>
    /// Gets CharacterStartQuestStateDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterStartQuestStateDat.</returns>
    public ReadOnlyCollection<CharacterStartQuestStateDat> GetCharacterStartQuestStateDat()
    {
        characterstartqueststatedat ??= CharacterStartQuestStateDat.Load(this).AsReadOnly();

        return characterstartqueststatedat;
    }

    /// <summary>
    /// Gets CharacterStartStatesDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterStartStatesDat.</returns>
    public ReadOnlyCollection<CharacterStartStatesDat> GetCharacterStartStatesDat()
    {
        characterstartstatesdat ??= CharacterStartStatesDat.Load(this).AsReadOnly();

        return characterstartstatesdat;
    }

    /// <summary>
    /// Gets CharacterStartStateSetDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterStartStateSetDat.</returns>
    public ReadOnlyCollection<CharacterStartStateSetDat> GetCharacterStartStateSetDat()
    {
        characterstartstatesetdat ??= CharacterStartStateSetDat.Load(this).AsReadOnly();

        return characterstartstatesetdat;
    }

    /// <summary>
    /// Gets CharacterTextAudioDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterTextAudioDat.</returns>
    public ReadOnlyCollection<CharacterTextAudioDat> GetCharacterTextAudioDat()
    {
        charactertextaudiodat ??= CharacterTextAudioDat.Load(this).AsReadOnly();

        return charactertextaudiodat;
    }

    /// <summary>
    /// Gets ChatIconsDat data.
    /// </summary>
    /// <returns>readonly collection of ChatIconsDat.</returns>
    public ReadOnlyCollection<ChatIconsDat> GetChatIconsDat()
    {
        chaticonsdat ??= ChatIconsDat.Load(this).AsReadOnly();

        return chaticonsdat;
    }

    /// <summary>
    /// Gets ChestClustersDat data.
    /// </summary>
    /// <returns>readonly collection of ChestClustersDat.</returns>
    public ReadOnlyCollection<ChestClustersDat> GetChestClustersDat()
    {
        chestclustersdat ??= ChestClustersDat.Load(this).AsReadOnly();

        return chestclustersdat;
    }

    /// <summary>
    /// Gets ChestEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of ChestEffectsDat.</returns>
    public ReadOnlyCollection<ChestEffectsDat> GetChestEffectsDat()
    {
        chesteffectsdat ??= ChestEffectsDat.Load(this).AsReadOnly();

        return chesteffectsdat;
    }

    /// <summary>
    /// Gets ChestsDat data.
    /// </summary>
    /// <returns>readonly collection of ChestsDat.</returns>
    public ReadOnlyCollection<ChestsDat> GetChestsDat()
    {
        chestsdat ??= ChestsDat.Load(this).AsReadOnly();

        return chestsdat;
    }

    /// <summary>
    /// Gets ClientStringsDat data.
    /// </summary>
    /// <returns>readonly collection of ClientStringsDat.</returns>
    public ReadOnlyCollection<ClientStringsDat> GetClientStringsDat()
    {
        clientstringsdat ??= ClientStringsDat.Load(this).AsReadOnly();

        return clientstringsdat;
    }

    /// <summary>
    /// Gets ClientLeagueActionDat data.
    /// </summary>
    /// <returns>readonly collection of ClientLeagueActionDat.</returns>
    public ReadOnlyCollection<ClientLeagueActionDat> GetClientLeagueActionDat()
    {
        clientleagueactiondat ??= ClientLeagueActionDat.Load(this).AsReadOnly();

        return clientleagueactiondat;
    }

    /// <summary>
    /// Gets CloneShotDat data.
    /// </summary>
    /// <returns>readonly collection of CloneShotDat.</returns>
    public ReadOnlyCollection<CloneShotDat> GetCloneShotDat()
    {
        cloneshotdat ??= CloneShotDat.Load(this).AsReadOnly();

        return cloneshotdat;
    }

    /// <summary>
    /// Gets ColoursDat data.
    /// </summary>
    /// <returns>readonly collection of ColoursDat.</returns>
    public ReadOnlyCollection<ColoursDat> GetColoursDat()
    {
        coloursdat ??= ColoursDat.Load(this).AsReadOnly();

        return coloursdat;
    }

    /// <summary>
    /// Gets CommandsDat data.
    /// </summary>
    /// <returns>readonly collection of CommandsDat.</returns>
    public ReadOnlyCollection<CommandsDat> GetCommandsDat()
    {
        commandsdat ??= CommandsDat.Load(this).AsReadOnly();

        return commandsdat;
    }

    /// <summary>
    /// Gets ComponentAttributeRequirementsDat data.
    /// </summary>
    /// <returns>readonly collection of ComponentAttributeRequirementsDat.</returns>
    public ReadOnlyCollection<ComponentAttributeRequirementsDat> GetComponentAttributeRequirementsDat()
    {
        componentattributerequirementsdat ??= ComponentAttributeRequirementsDat.Load(this).AsReadOnly();

        return componentattributerequirementsdat;
    }

    /// <summary>
    /// Gets ComponentChargesDat data.
    /// </summary>
    /// <returns>readonly collection of ComponentChargesDat.</returns>
    public ReadOnlyCollection<ComponentChargesDat> GetComponentChargesDat()
    {
        componentchargesdat ??= ComponentChargesDat.Load(this).AsReadOnly();

        return componentchargesdat;
    }

    /// <summary>
    /// Gets CoreLeaguesDat data.
    /// </summary>
    /// <returns>readonly collection of CoreLeaguesDat.</returns>
    public ReadOnlyCollection<CoreLeaguesDat> GetCoreLeaguesDat()
    {
        coreleaguesdat ??= CoreLeaguesDat.Load(this).AsReadOnly();

        return coreleaguesdat;
    }

    /// <summary>
    /// Gets CostTypesDat data.
    /// </summary>
    /// <returns>readonly collection of CostTypesDat.</returns>
    public ReadOnlyCollection<CostTypesDat> GetCostTypesDat()
    {
        costtypesdat ??= CostTypesDat.Load(this).AsReadOnly();

        return costtypesdat;
    }

    /// <summary>
    /// Gets CraftingBenchOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of CraftingBenchOptionsDat.</returns>
    public ReadOnlyCollection<CraftingBenchOptionsDat> GetCraftingBenchOptionsDat()
    {
        craftingbenchoptionsdat ??= CraftingBenchOptionsDat.Load(this).AsReadOnly();

        return craftingbenchoptionsdat;
    }

    /// <summary>
    /// Gets CraftingBenchSortCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of CraftingBenchSortCategoriesDat.</returns>
    public ReadOnlyCollection<CraftingBenchSortCategoriesDat> GetCraftingBenchSortCategoriesDat()
    {
        craftingbenchsortcategoriesdat ??= CraftingBenchSortCategoriesDat.Load(this).AsReadOnly();

        return craftingbenchsortcategoriesdat;
    }

    /// <summary>
    /// Gets CraftingBenchUnlockCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of CraftingBenchUnlockCategoriesDat.</returns>
    public ReadOnlyCollection<CraftingBenchUnlockCategoriesDat> GetCraftingBenchUnlockCategoriesDat()
    {
        craftingbenchunlockcategoriesdat ??= CraftingBenchUnlockCategoriesDat.Load(this).AsReadOnly();

        return craftingbenchunlockcategoriesdat;
    }

    /// <summary>
    /// Gets CraftingItemClassCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of CraftingItemClassCategoriesDat.</returns>
    public ReadOnlyCollection<CraftingItemClassCategoriesDat> GetCraftingItemClassCategoriesDat()
    {
        craftingitemclasscategoriesdat ??= CraftingItemClassCategoriesDat.Load(this).AsReadOnly();

        return craftingitemclasscategoriesdat;
    }

    /// <summary>
    /// Gets CurrencyItemsDat data.
    /// </summary>
    /// <returns>readonly collection of CurrencyItemsDat.</returns>
    public ReadOnlyCollection<CurrencyItemsDat> GetCurrencyItemsDat()
    {
        currencyitemsdat ??= CurrencyItemsDat.Load(this).AsReadOnly();

        return currencyitemsdat;
    }

    /// <summary>
    /// Gets CurrencyStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of CurrencyStashTabLayoutDat.</returns>
    public ReadOnlyCollection<CurrencyStashTabLayoutDat> GetCurrencyStashTabLayoutDat()
    {
        currencystashtablayoutdat ??= CurrencyStashTabLayoutDat.Load(this).AsReadOnly();

        return currencystashtablayoutdat;
    }

    /// <summary>
    /// Gets CustomLeagueModsDat data.
    /// </summary>
    /// <returns>readonly collection of CustomLeagueModsDat.</returns>
    public ReadOnlyCollection<CustomLeagueModsDat> GetCustomLeagueModsDat()
    {
        customleaguemodsdat ??= CustomLeagueModsDat.Load(this).AsReadOnly();

        return customleaguemodsdat;
    }

    /// <summary>
    /// Gets DaemonSpawningDataDat data.
    /// </summary>
    /// <returns>readonly collection of DaemonSpawningDataDat.</returns>
    public ReadOnlyCollection<DaemonSpawningDataDat> GetDaemonSpawningDataDat()
    {
        daemonspawningdatadat ??= DaemonSpawningDataDat.Load(this).AsReadOnly();

        return daemonspawningdatadat;
    }

    /// <summary>
    /// Gets DamageHitEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of DamageHitEffectsDat.</returns>
    public ReadOnlyCollection<DamageHitEffectsDat> GetDamageHitEffectsDat()
    {
        damagehiteffectsdat ??= DamageHitEffectsDat.Load(this).AsReadOnly();

        return damagehiteffectsdat;
    }

    /// <summary>
    /// Gets DamageParticleEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of DamageParticleEffectsDat.</returns>
    public ReadOnlyCollection<DamageParticleEffectsDat> GetDamageParticleEffectsDat()
    {
        damageparticleeffectsdat ??= DamageParticleEffectsDat.Load(this).AsReadOnly();

        return damageparticleeffectsdat;
    }

    /// <summary>
    /// Gets DancesDat data.
    /// </summary>
    /// <returns>readonly collection of DancesDat.</returns>
    public ReadOnlyCollection<DancesDat> GetDancesDat()
    {
        dancesdat ??= DancesDat.Load(this).AsReadOnly();

        return dancesdat;
    }

    /// <summary>
    /// Gets DaressoPitFightsDat data.
    /// </summary>
    /// <returns>readonly collection of DaressoPitFightsDat.</returns>
    public ReadOnlyCollection<DaressoPitFightsDat> GetDaressoPitFightsDat()
    {
        daressopitfightsdat ??= DaressoPitFightsDat.Load(this).AsReadOnly();

        return daressopitfightsdat;
    }

    /// <summary>
    /// Gets DefaultMonsterStatsDat data.
    /// </summary>
    /// <returns>readonly collection of DefaultMonsterStatsDat.</returns>
    public ReadOnlyCollection<DefaultMonsterStatsDat> GetDefaultMonsterStatsDat()
    {
        defaultmonsterstatsdat ??= DefaultMonsterStatsDat.Load(this).AsReadOnly();

        return defaultmonsterstatsdat;
    }

    /// <summary>
    /// Gets DeliriumStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of DeliriumStashTabLayoutDat.</returns>
    public ReadOnlyCollection<DeliriumStashTabLayoutDat> GetDeliriumStashTabLayoutDat()
    {
        deliriumstashtablayoutdat ??= DeliriumStashTabLayoutDat.Load(this).AsReadOnly();

        return deliriumstashtablayoutdat;
    }

    /// <summary>
    /// Gets DelveStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of DelveStashTabLayoutDat.</returns>
    public ReadOnlyCollection<DelveStashTabLayoutDat> GetDelveStashTabLayoutDat()
    {
        delvestashtablayoutdat ??= DelveStashTabLayoutDat.Load(this).AsReadOnly();

        return delvestashtablayoutdat;
    }

    /// <summary>
    /// Gets DescentExilesDat data.
    /// </summary>
    /// <returns>readonly collection of DescentExilesDat.</returns>
    public ReadOnlyCollection<DescentExilesDat> GetDescentExilesDat()
    {
        descentexilesdat ??= DescentExilesDat.Load(this).AsReadOnly();

        return descentexilesdat;
    }

    /// <summary>
    /// Gets DescentRewardChestsDat data.
    /// </summary>
    /// <returns>readonly collection of DescentRewardChestsDat.</returns>
    public ReadOnlyCollection<DescentRewardChestsDat> GetDescentRewardChestsDat()
    {
        descentrewardchestsdat ??= DescentRewardChestsDat.Load(this).AsReadOnly();

        return descentrewardchestsdat;
    }

    /// <summary>
    /// Gets DescentStarterChestDat data.
    /// </summary>
    /// <returns>readonly collection of DescentStarterChestDat.</returns>
    public ReadOnlyCollection<DescentStarterChestDat> GetDescentStarterChestDat()
    {
        descentstarterchestdat ??= DescentStarterChestDat.Load(this).AsReadOnly();

        return descentstarterchestdat;
    }

    /// <summary>
    /// Gets DialogueEventDat data.
    /// </summary>
    /// <returns>readonly collection of DialogueEventDat.</returns>
    public ReadOnlyCollection<DialogueEventDat> GetDialogueEventDat()
    {
        dialogueeventdat ??= DialogueEventDat.Load(this).AsReadOnly();

        return dialogueeventdat;
    }

    /// <summary>
    /// Gets DisplayMinionMonsterTypeDat data.
    /// </summary>
    /// <returns>readonly collection of DisplayMinionMonsterTypeDat.</returns>
    public ReadOnlyCollection<DisplayMinionMonsterTypeDat> GetDisplayMinionMonsterTypeDat()
    {
        displayminionmonstertypedat ??= DisplayMinionMonsterTypeDat.Load(this).AsReadOnly();

        return displayminionmonstertypedat;
    }

    /// <summary>
    /// Gets DivinationCardStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of DivinationCardStashTabLayoutDat.</returns>
    public ReadOnlyCollection<DivinationCardStashTabLayoutDat> GetDivinationCardStashTabLayoutDat()
    {
        divinationcardstashtablayoutdat ??= DivinationCardStashTabLayoutDat.Load(this).AsReadOnly();

        return divinationcardstashtablayoutdat;
    }

    /// <summary>
    /// Gets DoorsDat data.
    /// </summary>
    /// <returns>readonly collection of DoorsDat.</returns>
    public ReadOnlyCollection<DoorsDat> GetDoorsDat()
    {
        doorsdat ??= DoorsDat.Load(this).AsReadOnly();

        return doorsdat;
    }

    /// <summary>
    /// Gets DropEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of DropEffectsDat.</returns>
    public ReadOnlyCollection<DropEffectsDat> GetDropEffectsDat()
    {
        dropeffectsdat ??= DropEffectsDat.Load(this).AsReadOnly();

        return dropeffectsdat;
    }

    /// <summary>
    /// Gets DropPoolDat data.
    /// </summary>
    /// <returns>readonly collection of DropPoolDat.</returns>
    public ReadOnlyCollection<DropPoolDat> GetDropPoolDat()
    {
        droppooldat ??= DropPoolDat.Load(this).AsReadOnly();

        return droppooldat;
    }

    /// <summary>
    /// Gets EclipseModsDat data.
    /// </summary>
    /// <returns>readonly collection of EclipseModsDat.</returns>
    public ReadOnlyCollection<EclipseModsDat> GetEclipseModsDat()
    {
        eclipsemodsdat ??= EclipseModsDat.Load(this).AsReadOnly();

        return eclipsemodsdat;
    }

    /// <summary>
    /// Gets EffectDrivenSkillDat data.
    /// </summary>
    /// <returns>readonly collection of EffectDrivenSkillDat.</returns>
    public ReadOnlyCollection<EffectDrivenSkillDat> GetEffectDrivenSkillDat()
    {
        effectdrivenskilldat ??= EffectDrivenSkillDat.Load(this).AsReadOnly();

        return effectdrivenskilldat;
    }

    /// <summary>
    /// Gets EffectivenessCostConstantsDat data.
    /// </summary>
    /// <returns>readonly collection of EffectivenessCostConstantsDat.</returns>
    public ReadOnlyCollection<EffectivenessCostConstantsDat> GetEffectivenessCostConstantsDat()
    {
        effectivenesscostconstantsdat ??= EffectivenessCostConstantsDat.Load(this).AsReadOnly();

        return effectivenesscostconstantsdat;
    }

    /// <summary>
    /// Gets EinharMissionsDat data.
    /// </summary>
    /// <returns>readonly collection of EinharMissionsDat.</returns>
    public ReadOnlyCollection<EinharMissionsDat> GetEinharMissionsDat()
    {
        einharmissionsdat ??= EinharMissionsDat.Load(this).AsReadOnly();

        return einharmissionsdat;
    }

    /// <summary>
    /// Gets EinharPackFallbackDat data.
    /// </summary>
    /// <returns>readonly collection of EinharPackFallbackDat.</returns>
    public ReadOnlyCollection<EinharPackFallbackDat> GetEinharPackFallbackDat()
    {
        einharpackfallbackdat ??= EinharPackFallbackDat.Load(this).AsReadOnly();

        return einharpackfallbackdat;
    }

    /// <summary>
    /// Gets EndlessLedgeChestsDat data.
    /// </summary>
    /// <returns>readonly collection of EndlessLedgeChestsDat.</returns>
    public ReadOnlyCollection<EndlessLedgeChestsDat> GetEndlessLedgeChestsDat()
    {
        endlessledgechestsdat ??= EndlessLedgeChestsDat.Load(this).AsReadOnly();

        return endlessledgechestsdat;
    }

    /// <summary>
    /// Gets EnvironmentsDat data.
    /// </summary>
    /// <returns>readonly collection of EnvironmentsDat.</returns>
    public ReadOnlyCollection<EnvironmentsDat> GetEnvironmentsDat()
    {
        environmentsdat ??= EnvironmentsDat.Load(this).AsReadOnly();

        return environmentsdat;
    }

    /// <summary>
    /// Gets EnvironmentTransitionsDat data.
    /// </summary>
    /// <returns>readonly collection of EnvironmentTransitionsDat.</returns>
    public ReadOnlyCollection<EnvironmentTransitionsDat> GetEnvironmentTransitionsDat()
    {
        environmenttransitionsdat ??= EnvironmentTransitionsDat.Load(this).AsReadOnly();

        return environmenttransitionsdat;
    }

    /// <summary>
    /// Gets EssenceStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of EssenceStashTabLayoutDat.</returns>
    public ReadOnlyCollection<EssenceStashTabLayoutDat> GetEssenceStashTabLayoutDat()
    {
        essencestashtablayoutdat ??= EssenceStashTabLayoutDat.Load(this).AsReadOnly();

        return essencestashtablayoutdat;
    }

    /// <summary>
    /// Gets EventSeasonDat data.
    /// </summary>
    /// <returns>readonly collection of EventSeasonDat.</returns>
    public ReadOnlyCollection<EventSeasonDat> GetEventSeasonDat()
    {
        eventseasondat ??= EventSeasonDat.Load(this).AsReadOnly();

        return eventseasondat;
    }

    /// <summary>
    /// Gets EventSeasonRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of EventSeasonRewardsDat.</returns>
    public ReadOnlyCollection<EventSeasonRewardsDat> GetEventSeasonRewardsDat()
    {
        eventseasonrewardsdat ??= EventSeasonRewardsDat.Load(this).AsReadOnly();

        return eventseasonrewardsdat;
    }

    /// <summary>
    /// Gets EvergreenAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of EvergreenAchievementsDat.</returns>
    public ReadOnlyCollection<EvergreenAchievementsDat> GetEvergreenAchievementsDat()
    {
        evergreenachievementsdat ??= EvergreenAchievementsDat.Load(this).AsReadOnly();

        return evergreenachievementsdat;
    }

    /// <summary>
    /// Gets ExecuteGEALDat data.
    /// </summary>
    /// <returns>readonly collection of ExecuteGEALDat.</returns>
    public ReadOnlyCollection<ExecuteGEALDat> GetExecuteGEALDat()
    {
        executegealdat ??= ExecuteGEALDat.Load(this).AsReadOnly();

        return executegealdat;
    }

    /// <summary>
    /// Gets ExpandingPulseDat data.
    /// </summary>
    /// <returns>readonly collection of ExpandingPulseDat.</returns>
    public ReadOnlyCollection<ExpandingPulseDat> GetExpandingPulseDat()
    {
        expandingpulsedat ??= ExpandingPulseDat.Load(this).AsReadOnly();

        return expandingpulsedat;
    }

    /// <summary>
    /// Gets ExperienceLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of ExperienceLevelsDat.</returns>
    public ReadOnlyCollection<ExperienceLevelsDat> GetExperienceLevelsDat()
    {
        experiencelevelsdat ??= ExperienceLevelsDat.Load(this).AsReadOnly();

        return experiencelevelsdat;
    }

    /// <summary>
    /// Gets ExplodingStormBuffsDat data.
    /// </summary>
    /// <returns>readonly collection of ExplodingStormBuffsDat.</returns>
    public ReadOnlyCollection<ExplodingStormBuffsDat> GetExplodingStormBuffsDat()
    {
        explodingstormbuffsdat ??= ExplodingStormBuffsDat.Load(this).AsReadOnly();

        return explodingstormbuffsdat;
    }

    /// <summary>
    /// Gets ExtraTerrainFeaturesDat data.
    /// </summary>
    /// <returns>readonly collection of ExtraTerrainFeaturesDat.</returns>
    public ReadOnlyCollection<ExtraTerrainFeaturesDat> GetExtraTerrainFeaturesDat()
    {
        extraterrainfeaturesdat ??= ExtraTerrainFeaturesDat.Load(this).AsReadOnly();

        return extraterrainfeaturesdat;
    }

    /// <summary>
    /// Gets FixedHideoutDoodadTypesDat data.
    /// </summary>
    /// <returns>readonly collection of FixedHideoutDoodadTypesDat.</returns>
    public ReadOnlyCollection<FixedHideoutDoodadTypesDat> GetFixedHideoutDoodadTypesDat()
    {
        fixedhideoutdoodadtypesdat ??= FixedHideoutDoodadTypesDat.Load(this).AsReadOnly();

        return fixedhideoutdoodadtypesdat;
    }

    /// <summary>
    /// Gets FixedMissionsDat data.
    /// </summary>
    /// <returns>readonly collection of FixedMissionsDat.</returns>
    public ReadOnlyCollection<FixedMissionsDat> GetFixedMissionsDat()
    {
        fixedmissionsdat ??= FixedMissionsDat.Load(this).AsReadOnly();

        return fixedmissionsdat;
    }

    /// <summary>
    /// Gets FlasksDat data.
    /// </summary>
    /// <returns>readonly collection of FlasksDat.</returns>
    public ReadOnlyCollection<FlasksDat> GetFlasksDat()
    {
        flasksdat ??= FlasksDat.Load(this).AsReadOnly();

        return flasksdat;
    }

    /// <summary>
    /// Gets FlavourTextDat data.
    /// </summary>
    /// <returns>readonly collection of FlavourTextDat.</returns>
    public ReadOnlyCollection<FlavourTextDat> GetFlavourTextDat()
    {
        flavourtextdat ??= FlavourTextDat.Load(this).AsReadOnly();

        return flavourtextdat;
    }

    /// <summary>
    /// Gets FootprintsDat data.
    /// </summary>
    /// <returns>readonly collection of FootprintsDat.</returns>
    public ReadOnlyCollection<FootprintsDat> GetFootprintsDat()
    {
        footprintsdat ??= FootprintsDat.Load(this).AsReadOnly();

        return footprintsdat;
    }

    /// <summary>
    /// Gets FootstepAudioDat data.
    /// </summary>
    /// <returns>readonly collection of FootstepAudioDat.</returns>
    public ReadOnlyCollection<FootstepAudioDat> GetFootstepAudioDat()
    {
        footstepaudiodat ??= FootstepAudioDat.Load(this).AsReadOnly();

        return footstepaudiodat;
    }

    /// <summary>
    /// Gets FragmentStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of FragmentStashTabLayoutDat.</returns>
    public ReadOnlyCollection<FragmentStashTabLayoutDat> GetFragmentStashTabLayoutDat()
    {
        fragmentstashtablayoutdat ??= FragmentStashTabLayoutDat.Load(this).AsReadOnly();

        return fragmentstashtablayoutdat;
    }

    /// <summary>
    /// Gets GameConstantsDat data.
    /// </summary>
    /// <returns>readonly collection of GameConstantsDat.</returns>
    public ReadOnlyCollection<GameConstantsDat> GetGameConstantsDat()
    {
        gameconstantsdat ??= GameConstantsDat.Load(this).AsReadOnly();

        return gameconstantsdat;
    }

    /// <summary>
    /// Gets GameObjectTasksDat data.
    /// </summary>
    /// <returns>readonly collection of GameObjectTasksDat.</returns>
    public ReadOnlyCollection<GameObjectTasksDat> GetGameObjectTasksDat()
    {
        gameobjecttasksdat ??= GameObjectTasksDat.Load(this).AsReadOnly();

        return gameobjecttasksdat;
    }

    /// <summary>
    /// Gets GamepadButtonDat data.
    /// </summary>
    /// <returns>readonly collection of GamepadButtonDat.</returns>
    public ReadOnlyCollection<GamepadButtonDat> GetGamepadButtonDat()
    {
        gamepadbuttondat ??= GamepadButtonDat.Load(this).AsReadOnly();

        return gamepadbuttondat;
    }

    /// <summary>
    /// Gets GamepadTypeDat data.
    /// </summary>
    /// <returns>readonly collection of GamepadTypeDat.</returns>
    public ReadOnlyCollection<GamepadTypeDat> GetGamepadTypeDat()
    {
        gamepadtypedat ??= GamepadTypeDat.Load(this).AsReadOnly();

        return gamepadtypedat;
    }

    /// <summary>
    /// Gets GameStatsDat data.
    /// </summary>
    /// <returns>readonly collection of GameStatsDat.</returns>
    public ReadOnlyCollection<GameStatsDat> GetGameStatsDat()
    {
        gamestatsdat ??= GameStatsDat.Load(this).AsReadOnly();

        return gamestatsdat;
    }

    /// <summary>
    /// Gets GemTagsDat data.
    /// </summary>
    /// <returns>readonly collection of GemTagsDat.</returns>
    public ReadOnlyCollection<GemTagsDat> GetGemTagsDat()
    {
        gemtagsdat ??= GemTagsDat.Load(this).AsReadOnly();

        return gemtagsdat;
    }

    /// <summary>
    /// Gets GenericBuffAurasDat data.
    /// </summary>
    /// <returns>readonly collection of GenericBuffAurasDat.</returns>
    public ReadOnlyCollection<GenericBuffAurasDat> GetGenericBuffAurasDat()
    {
        genericbuffaurasdat ??= GenericBuffAurasDat.Load(this).AsReadOnly();

        return genericbuffaurasdat;
    }

    /// <summary>
    /// Gets GenericLeagueRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of GenericLeagueRewardTypesDat.</returns>
    public ReadOnlyCollection<GenericLeagueRewardTypesDat> GetGenericLeagueRewardTypesDat()
    {
        genericleaguerewardtypesdat ??= GenericLeagueRewardTypesDat.Load(this).AsReadOnly();

        return genericleaguerewardtypesdat;
    }

    /// <summary>
    /// Gets GenericLeagueRewardTypeVisualsDat data.
    /// </summary>
    /// <returns>readonly collection of GenericLeagueRewardTypeVisualsDat.</returns>
    public ReadOnlyCollection<GenericLeagueRewardTypeVisualsDat> GetGenericLeagueRewardTypeVisualsDat()
    {
        genericleaguerewardtypevisualsdat ??= GenericLeagueRewardTypeVisualsDat.Load(this).AsReadOnly();

        return genericleaguerewardtypevisualsdat;
    }

    /// <summary>
    /// Gets GeometryAttackDat data.
    /// </summary>
    /// <returns>readonly collection of GeometryAttackDat.</returns>
    public ReadOnlyCollection<GeometryAttackDat> GetGeometryAttackDat()
    {
        geometryattackdat ??= GeometryAttackDat.Load(this).AsReadOnly();

        return geometryattackdat;
    }

    /// <summary>
    /// Gets GeometryChannelDat data.
    /// </summary>
    /// <returns>readonly collection of GeometryChannelDat.</returns>
    public ReadOnlyCollection<GeometryChannelDat> GetGeometryChannelDat()
    {
        geometrychanneldat ??= GeometryChannelDat.Load(this).AsReadOnly();

        return geometrychanneldat;
    }

    /// <summary>
    /// Gets GeometryProjectilesDat data.
    /// </summary>
    /// <returns>readonly collection of GeometryProjectilesDat.</returns>
    public ReadOnlyCollection<GeometryProjectilesDat> GetGeometryProjectilesDat()
    {
        geometryprojectilesdat ??= GeometryProjectilesDat.Load(this).AsReadOnly();

        return geometryprojectilesdat;
    }

    /// <summary>
    /// Gets GeometryTriggerDat data.
    /// </summary>
    /// <returns>readonly collection of GeometryTriggerDat.</returns>
    public ReadOnlyCollection<GeometryTriggerDat> GetGeometryTriggerDat()
    {
        geometrytriggerdat ??= GeometryTriggerDat.Load(this).AsReadOnly();

        return geometrytriggerdat;
    }

    /// <summary>
    /// Gets GiftWrapArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of GiftWrapArtVariationsDat.</returns>
    public ReadOnlyCollection<GiftWrapArtVariationsDat> GetGiftWrapArtVariationsDat()
    {
        giftwrapartvariationsdat ??= GiftWrapArtVariationsDat.Load(this).AsReadOnly();

        return giftwrapartvariationsdat;
    }

    /// <summary>
    /// Gets GlobalAudioConfigDat data.
    /// </summary>
    /// <returns>readonly collection of GlobalAudioConfigDat.</returns>
    public ReadOnlyCollection<GlobalAudioConfigDat> GetGlobalAudioConfigDat()
    {
        globalaudioconfigdat ??= GlobalAudioConfigDat.Load(this).AsReadOnly();

        return globalaudioconfigdat;
    }

    /// <summary>
    /// Gets GrandmastersDat data.
    /// </summary>
    /// <returns>readonly collection of GrandmastersDat.</returns>
    public ReadOnlyCollection<GrandmastersDat> GetGrandmastersDat()
    {
        grandmastersdat ??= GrandmastersDat.Load(this).AsReadOnly();

        return grandmastersdat;
    }

    /// <summary>
    /// Gets GrantedEffectQualityStatsDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectQualityStatsDat.</returns>
    public ReadOnlyCollection<GrantedEffectQualityStatsDat> GetGrantedEffectQualityStatsDat()
    {
        grantedeffectqualitystatsdat ??= GrantedEffectQualityStatsDat.Load(this).AsReadOnly();

        return grantedeffectqualitystatsdat;
    }

    /// <summary>
    /// Gets GrantedEffectQualityTypesDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectQualityTypesDat.</returns>
    public ReadOnlyCollection<GrantedEffectQualityTypesDat> GetGrantedEffectQualityTypesDat()
    {
        grantedeffectqualitytypesdat ??= GrantedEffectQualityTypesDat.Load(this).AsReadOnly();

        return grantedeffectqualitytypesdat;
    }

    /// <summary>
    /// Gets GrantedEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectsDat.</returns>
    public ReadOnlyCollection<GrantedEffectsDat> GetGrantedEffectsDat()
    {
        grantedeffectsdat ??= GrantedEffectsDat.Load(this).AsReadOnly();

        return grantedeffectsdat;
    }

    /// <summary>
    /// Gets GrantedEffectsPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectsPerLevelDat.</returns>
    public ReadOnlyCollection<GrantedEffectsPerLevelDat> GetGrantedEffectsPerLevelDat()
    {
        grantedeffectsperleveldat ??= GrantedEffectsPerLevelDat.Load(this).AsReadOnly();

        return grantedeffectsperleveldat;
    }

    /// <summary>
    /// Gets GrantedEffectStatSetsDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectStatSetsDat.</returns>
    public ReadOnlyCollection<GrantedEffectStatSetsDat> GetGrantedEffectStatSetsDat()
    {
        grantedeffectstatsetsdat ??= GrantedEffectStatSetsDat.Load(this).AsReadOnly();

        return grantedeffectstatsetsdat;
    }

    /// <summary>
    /// Gets GrantedEffectStatSetsPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectStatSetsPerLevelDat.</returns>
    public ReadOnlyCollection<GrantedEffectStatSetsPerLevelDat> GetGrantedEffectStatSetsPerLevelDat()
    {
        grantedeffectstatsetsperleveldat ??= GrantedEffectStatSetsPerLevelDat.Load(this).AsReadOnly();

        return grantedeffectstatsetsperleveldat;
    }

    /// <summary>
    /// Gets GroundEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of GroundEffectsDat.</returns>
    public ReadOnlyCollection<GroundEffectsDat> GetGroundEffectsDat()
    {
        groundeffectsdat ??= GroundEffectsDat.Load(this).AsReadOnly();

        return groundeffectsdat;
    }

    /// <summary>
    /// Gets GroundEffectTypesDat data.
    /// </summary>
    /// <returns>readonly collection of GroundEffectTypesDat.</returns>
    public ReadOnlyCollection<GroundEffectTypesDat> GetGroundEffectTypesDat()
    {
        groundeffecttypesdat ??= GroundEffectTypesDat.Load(this).AsReadOnly();

        return groundeffecttypesdat;
    }

    /// <summary>
    /// Gets HarvestStorageLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestStorageLayoutDat.</returns>
    public ReadOnlyCollection<HarvestStorageLayoutDat> GetHarvestStorageLayoutDat()
    {
        harveststoragelayoutdat ??= HarvestStorageLayoutDat.Load(this).AsReadOnly();

        return harveststoragelayoutdat;
    }

    /// <summary>
    /// Gets HeistStorageLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of HeistStorageLayoutDat.</returns>
    public ReadOnlyCollection<HeistStorageLayoutDat> GetHeistStorageLayoutDat()
    {
        heiststoragelayoutdat ??= HeistStorageLayoutDat.Load(this).AsReadOnly();

        return heiststoragelayoutdat;
    }

    /// <summary>
    /// Gets HideoutDoodadsDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutDoodadsDat.</returns>
    public ReadOnlyCollection<HideoutDoodadsDat> GetHideoutDoodadsDat()
    {
        hideoutdoodadsdat ??= HideoutDoodadsDat.Load(this).AsReadOnly();

        return hideoutdoodadsdat;
    }

    /// <summary>
    /// Gets HideoutDoodadCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutDoodadCategoryDat.</returns>
    public ReadOnlyCollection<HideoutDoodadCategoryDat> GetHideoutDoodadCategoryDat()
    {
        hideoutdoodadcategorydat ??= HideoutDoodadCategoryDat.Load(this).AsReadOnly();

        return hideoutdoodadcategorydat;
    }

    /// <summary>
    /// Gets HideoutDoodadTagsDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutDoodadTagsDat.</returns>
    public ReadOnlyCollection<HideoutDoodadTagsDat> GetHideoutDoodadTagsDat()
    {
        hideoutdoodadtagsdat ??= HideoutDoodadTagsDat.Load(this).AsReadOnly();

        return hideoutdoodadtagsdat;
    }

    /// <summary>
    /// Gets HideoutNPCsDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutNPCsDat.</returns>
    public ReadOnlyCollection<HideoutNPCsDat> GetHideoutNPCsDat()
    {
        hideoutnpcsdat ??= HideoutNPCsDat.Load(this).AsReadOnly();

        return hideoutnpcsdat;
    }

    /// <summary>
    /// Gets HideoutRarityDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutRarityDat.</returns>
    public ReadOnlyCollection<HideoutRarityDat> GetHideoutRarityDat()
    {
        hideoutraritydat ??= HideoutRarityDat.Load(this).AsReadOnly();

        return hideoutraritydat;
    }

    /// <summary>
    /// Gets HideoutsDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutsDat.</returns>
    public ReadOnlyCollection<HideoutsDat> GetHideoutsDat()
    {
        hideoutsdat ??= HideoutsDat.Load(this).AsReadOnly();

        return hideoutsdat;
    }

    /// <summary>
    /// Gets ImpactSoundDataDat data.
    /// </summary>
    /// <returns>readonly collection of ImpactSoundDataDat.</returns>
    public ReadOnlyCollection<ImpactSoundDataDat> GetImpactSoundDataDat()
    {
        impactsounddatadat ??= ImpactSoundDataDat.Load(this).AsReadOnly();

        return impactsounddatadat;
    }

    /// <summary>
    /// Gets IndexableSupportGemsDat data.
    /// </summary>
    /// <returns>readonly collection of IndexableSupportGemsDat.</returns>
    public ReadOnlyCollection<IndexableSupportGemsDat> GetIndexableSupportGemsDat()
    {
        indexablesupportgemsdat ??= IndexableSupportGemsDat.Load(this).AsReadOnly();

        return indexablesupportgemsdat;
    }

    /// <summary>
    /// Gets InfluenceExaltsDat data.
    /// </summary>
    /// <returns>readonly collection of InfluenceExaltsDat.</returns>
    public ReadOnlyCollection<InfluenceExaltsDat> GetInfluenceExaltsDat()
    {
        influenceexaltsdat ??= InfluenceExaltsDat.Load(this).AsReadOnly();

        return influenceexaltsdat;
    }

    /// <summary>
    /// Gets InfluenceTagsDat data.
    /// </summary>
    /// <returns>readonly collection of InfluenceTagsDat.</returns>
    public ReadOnlyCollection<InfluenceTagsDat> GetInfluenceTagsDat()
    {
        influencetagsdat ??= InfluenceTagsDat.Load(this).AsReadOnly();

        return influencetagsdat;
    }

    /// <summary>
    /// Gets InventoriesDat data.
    /// </summary>
    /// <returns>readonly collection of InventoriesDat.</returns>
    public ReadOnlyCollection<InventoriesDat> GetInventoriesDat()
    {
        inventoriesdat ??= InventoriesDat.Load(this).AsReadOnly();

        return inventoriesdat;
    }

    /// <summary>
    /// Gets ItemClassCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of ItemClassCategoriesDat.</returns>
    public ReadOnlyCollection<ItemClassCategoriesDat> GetItemClassCategoriesDat()
    {
        itemclasscategoriesdat ??= ItemClassCategoriesDat.Load(this).AsReadOnly();

        return itemclasscategoriesdat;
    }

    /// <summary>
    /// Gets ItemClassesDat data.
    /// </summary>
    /// <returns>readonly collection of ItemClassesDat.</returns>
    public ReadOnlyCollection<ItemClassesDat> GetItemClassesDat()
    {
        itemclassesdat ??= ItemClassesDat.Load(this).AsReadOnly();

        return itemclassesdat;
    }

    /// <summary>
    /// Gets ItemCostPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of ItemCostPerLevelDat.</returns>
    public ReadOnlyCollection<ItemCostPerLevelDat> GetItemCostPerLevelDat()
    {
        itemcostperleveldat ??= ItemCostPerLevelDat.Load(this).AsReadOnly();

        return itemcostperleveldat;
    }

    /// <summary>
    /// Gets ItemCostsDat data.
    /// </summary>
    /// <returns>readonly collection of ItemCostsDat.</returns>
    public ReadOnlyCollection<ItemCostsDat> GetItemCostsDat()
    {
        itemcostsdat ??= ItemCostsDat.Load(this).AsReadOnly();

        return itemcostsdat;
    }

    /// <summary>
    /// Gets ItemFrameTypeDat data.
    /// </summary>
    /// <returns>readonly collection of ItemFrameTypeDat.</returns>
    public ReadOnlyCollection<ItemFrameTypeDat> GetItemFrameTypeDat()
    {
        itemframetypedat ??= ItemFrameTypeDat.Load(this).AsReadOnly();

        return itemframetypedat;
    }

    /// <summary>
    /// Gets ItemExperiencePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of ItemExperiencePerLevelDat.</returns>
    public ReadOnlyCollection<ItemExperiencePerLevelDat> GetItemExperiencePerLevelDat()
    {
        itemexperienceperleveldat ??= ItemExperiencePerLevelDat.Load(this).AsReadOnly();

        return itemexperienceperleveldat;
    }

    /// <summary>
    /// Gets ItemisedVisualEffectDat data.
    /// </summary>
    /// <returns>readonly collection of ItemisedVisualEffectDat.</returns>
    public ReadOnlyCollection<ItemisedVisualEffectDat> GetItemisedVisualEffectDat()
    {
        itemisedvisualeffectdat ??= ItemisedVisualEffectDat.Load(this).AsReadOnly();

        return itemisedvisualeffectdat;
    }

    /// <summary>
    /// Gets ItemNoteCodeDat data.
    /// </summary>
    /// <returns>readonly collection of ItemNoteCodeDat.</returns>
    public ReadOnlyCollection<ItemNoteCodeDat> GetItemNoteCodeDat()
    {
        itemnotecodedat ??= ItemNoteCodeDat.Load(this).AsReadOnly();

        return itemnotecodedat;
    }

    /// <summary>
    /// Gets ItemShopTypeDat data.
    /// </summary>
    /// <returns>readonly collection of ItemShopTypeDat.</returns>
    public ReadOnlyCollection<ItemShopTypeDat> GetItemShopTypeDat()
    {
        itemshoptypedat ??= ItemShopTypeDat.Load(this).AsReadOnly();

        return itemshoptypedat;
    }

    /// <summary>
    /// Gets ItemStancesDat data.
    /// </summary>
    /// <returns>readonly collection of ItemStancesDat.</returns>
    public ReadOnlyCollection<ItemStancesDat> GetItemStancesDat()
    {
        itemstancesdat ??= ItemStancesDat.Load(this).AsReadOnly();

        return itemstancesdat;
    }

    /// <summary>
    /// Gets ItemThemesDat data.
    /// </summary>
    /// <returns>readonly collection of ItemThemesDat.</returns>
    public ReadOnlyCollection<ItemThemesDat> GetItemThemesDat()
    {
        itemthemesdat ??= ItemThemesDat.Load(this).AsReadOnly();

        return itemthemesdat;
    }

    /// <summary>
    /// Gets ItemVisualEffectDat data.
    /// </summary>
    /// <returns>readonly collection of ItemVisualEffectDat.</returns>
    public ReadOnlyCollection<ItemVisualEffectDat> GetItemVisualEffectDat()
    {
        itemvisualeffectdat ??= ItemVisualEffectDat.Load(this).AsReadOnly();

        return itemvisualeffectdat;
    }

    /// <summary>
    /// Gets ItemVisualHeldBodyModelDat data.
    /// </summary>
    /// <returns>readonly collection of ItemVisualHeldBodyModelDat.</returns>
    public ReadOnlyCollection<ItemVisualHeldBodyModelDat> GetItemVisualHeldBodyModelDat()
    {
        itemvisualheldbodymodeldat ??= ItemVisualHeldBodyModelDat.Load(this).AsReadOnly();

        return itemvisualheldbodymodeldat;
    }

    /// <summary>
    /// Gets ItemVisualIdentityDat data.
    /// </summary>
    /// <returns>readonly collection of ItemVisualIdentityDat.</returns>
    public ReadOnlyCollection<ItemVisualIdentityDat> GetItemVisualIdentityDat()
    {
        itemvisualidentitydat ??= ItemVisualIdentityDat.Load(this).AsReadOnly();

        return itemvisualidentitydat;
    }

    /// <summary>
    /// Gets JobAssassinationSpawnerGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of JobAssassinationSpawnerGroupsDat.</returns>
    public ReadOnlyCollection<JobAssassinationSpawnerGroupsDat> GetJobAssassinationSpawnerGroupsDat()
    {
        jobassassinationspawnergroupsdat ??= JobAssassinationSpawnerGroupsDat.Load(this).AsReadOnly();

        return jobassassinationspawnergroupsdat;
    }

    /// <summary>
    /// Gets JobRaidBracketsDat data.
    /// </summary>
    /// <returns>readonly collection of JobRaidBracketsDat.</returns>
    public ReadOnlyCollection<JobRaidBracketsDat> GetJobRaidBracketsDat()
    {
        jobraidbracketsdat ??= JobRaidBracketsDat.Load(this).AsReadOnly();

        return jobraidbracketsdat;
    }

    /// <summary>
    /// Gets KillstreakThresholdsDat data.
    /// </summary>
    /// <returns>readonly collection of KillstreakThresholdsDat.</returns>
    public ReadOnlyCollection<KillstreakThresholdsDat> GetKillstreakThresholdsDat()
    {
        killstreakthresholdsdat ??= KillstreakThresholdsDat.Load(this).AsReadOnly();

        return killstreakthresholdsdat;
    }

    /// <summary>
    /// Gets LeagueFlagDat data.
    /// </summary>
    /// <returns>readonly collection of LeagueFlagDat.</returns>
    public ReadOnlyCollection<LeagueFlagDat> GetLeagueFlagDat()
    {
        leagueflagdat ??= LeagueFlagDat.Load(this).AsReadOnly();

        return leagueflagdat;
    }

    /// <summary>
    /// Gets LeagueInfoDat data.
    /// </summary>
    /// <returns>readonly collection of LeagueInfoDat.</returns>
    public ReadOnlyCollection<LeagueInfoDat> GetLeagueInfoDat()
    {
        leagueinfodat ??= LeagueInfoDat.Load(this).AsReadOnly();

        return leagueinfodat;
    }

    /// <summary>
    /// Gets LeagueProgressQuestFlagsDat data.
    /// </summary>
    /// <returns>readonly collection of LeagueProgressQuestFlagsDat.</returns>
    public ReadOnlyCollection<LeagueProgressQuestFlagsDat> GetLeagueProgressQuestFlagsDat()
    {
        leagueprogressquestflagsdat ??= LeagueProgressQuestFlagsDat.Load(this).AsReadOnly();

        return leagueprogressquestflagsdat;
    }

    /// <summary>
    /// Gets LeagueStaticRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of LeagueStaticRewardsDat.</returns>
    public ReadOnlyCollection<LeagueStaticRewardsDat> GetLeagueStaticRewardsDat()
    {
        leaguestaticrewardsdat ??= LeagueStaticRewardsDat.Load(this).AsReadOnly();

        return leaguestaticrewardsdat;
    }

    /// <summary>
    /// Gets LevelRelativePlayerScalingDat data.
    /// </summary>
    /// <returns>readonly collection of LevelRelativePlayerScalingDat.</returns>
    public ReadOnlyCollection<LevelRelativePlayerScalingDat> GetLevelRelativePlayerScalingDat()
    {
        levelrelativeplayerscalingdat ??= LevelRelativePlayerScalingDat.Load(this).AsReadOnly();

        return levelrelativeplayerscalingdat;
    }

    /// <summary>
    /// Gets MagicMonsterLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of MagicMonsterLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<MagicMonsterLifeScalingPerLevelDat> GetMagicMonsterLifeScalingPerLevelDat()
    {
        magicmonsterlifescalingperleveldat ??= MagicMonsterLifeScalingPerLevelDat.Load(this).AsReadOnly();

        return magicmonsterlifescalingperleveldat;
    }

    /// <summary>
    /// Gets MapCompletionAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of MapCompletionAchievementsDat.</returns>
    public ReadOnlyCollection<MapCompletionAchievementsDat> GetMapCompletionAchievementsDat()
    {
        mapcompletionachievementsdat ??= MapCompletionAchievementsDat.Load(this).AsReadOnly();

        return mapcompletionachievementsdat;
    }

    /// <summary>
    /// Gets MapConnectionsDat data.
    /// </summary>
    /// <returns>readonly collection of MapConnectionsDat.</returns>
    public ReadOnlyCollection<MapConnectionsDat> GetMapConnectionsDat()
    {
        mapconnectionsdat ??= MapConnectionsDat.Load(this).AsReadOnly();

        return mapconnectionsdat;
    }

    /// <summary>
    /// Gets MapCreationInformationDat data.
    /// </summary>
    /// <returns>readonly collection of MapCreationInformationDat.</returns>
    public ReadOnlyCollection<MapCreationInformationDat> GetMapCreationInformationDat()
    {
        mapcreationinformationdat ??= MapCreationInformationDat.Load(this).AsReadOnly();

        return mapcreationinformationdat;
    }

    /// <summary>
    /// Gets MapDeviceRecipesDat data.
    /// </summary>
    /// <returns>readonly collection of MapDeviceRecipesDat.</returns>
    public ReadOnlyCollection<MapDeviceRecipesDat> GetMapDeviceRecipesDat()
    {
        mapdevicerecipesdat ??= MapDeviceRecipesDat.Load(this).AsReadOnly();

        return mapdevicerecipesdat;
    }

    /// <summary>
    /// Gets MapDevicesDat data.
    /// </summary>
    /// <returns>readonly collection of MapDevicesDat.</returns>
    public ReadOnlyCollection<MapDevicesDat> GetMapDevicesDat()
    {
        mapdevicesdat ??= MapDevicesDat.Load(this).AsReadOnly();

        return mapdevicesdat;
    }

    /// <summary>
    /// Gets MapFragmentModsDat data.
    /// </summary>
    /// <returns>readonly collection of MapFragmentModsDat.</returns>
    public ReadOnlyCollection<MapFragmentModsDat> GetMapFragmentModsDat()
    {
        mapfragmentmodsdat ??= MapFragmentModsDat.Load(this).AsReadOnly();

        return mapfragmentmodsdat;
    }

    /// <summary>
    /// Gets MapInhabitantsDat data.
    /// </summary>
    /// <returns>readonly collection of MapInhabitantsDat.</returns>
    public ReadOnlyCollection<MapInhabitantsDat> GetMapInhabitantsDat()
    {
        mapinhabitantsdat ??= MapInhabitantsDat.Load(this).AsReadOnly();

        return mapinhabitantsdat;
    }

    /// <summary>
    /// Gets MapPinsDat data.
    /// </summary>
    /// <returns>readonly collection of MapPinsDat.</returns>
    public ReadOnlyCollection<MapPinsDat> GetMapPinsDat()
    {
        mappinsdat ??= MapPinsDat.Load(this).AsReadOnly();

        return mappinsdat;
    }

    /// <summary>
    /// Gets MapPurchaseCostsDat data.
    /// </summary>
    /// <returns>readonly collection of MapPurchaseCostsDat.</returns>
    public ReadOnlyCollection<MapPurchaseCostsDat> GetMapPurchaseCostsDat()
    {
        mappurchasecostsdat ??= MapPurchaseCostsDat.Load(this).AsReadOnly();

        return mappurchasecostsdat;
    }

    /// <summary>
    /// Gets MapsDat data.
    /// </summary>
    /// <returns>readonly collection of MapsDat.</returns>
    public ReadOnlyCollection<MapsDat> GetMapsDat()
    {
        mapsdat ??= MapsDat.Load(this).AsReadOnly();

        return mapsdat;
    }

    /// <summary>
    /// Gets MapSeriesDat data.
    /// </summary>
    /// <returns>readonly collection of MapSeriesDat.</returns>
    public ReadOnlyCollection<MapSeriesDat> GetMapSeriesDat()
    {
        mapseriesdat ??= MapSeriesDat.Load(this).AsReadOnly();

        return mapseriesdat;
    }

    /// <summary>
    /// Gets MapSeriesTiersDat data.
    /// </summary>
    /// <returns>readonly collection of MapSeriesTiersDat.</returns>
    public ReadOnlyCollection<MapSeriesTiersDat> GetMapSeriesTiersDat()
    {
        mapseriestiersdat ??= MapSeriesTiersDat.Load(this).AsReadOnly();

        return mapseriestiersdat;
    }

    /// <summary>
    /// Gets MapStashSpecialTypeEntriesDat data.
    /// </summary>
    /// <returns>readonly collection of MapStashSpecialTypeEntriesDat.</returns>
    public ReadOnlyCollection<MapStashSpecialTypeEntriesDat> GetMapStashSpecialTypeEntriesDat()
    {
        mapstashspecialtypeentriesdat ??= MapStashSpecialTypeEntriesDat.Load(this).AsReadOnly();

        return mapstashspecialtypeentriesdat;
    }

    /// <summary>
    /// Gets MapStashUniqueMapInfoDat data.
    /// </summary>
    /// <returns>readonly collection of MapStashUniqueMapInfoDat.</returns>
    public ReadOnlyCollection<MapStashUniqueMapInfoDat> GetMapStashUniqueMapInfoDat()
    {
        mapstashuniquemapinfodat ??= MapStashUniqueMapInfoDat.Load(this).AsReadOnly();

        return mapstashuniquemapinfodat;
    }

    /// <summary>
    /// Gets MapStatConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of MapStatConditionsDat.</returns>
    public ReadOnlyCollection<MapStatConditionsDat> GetMapStatConditionsDat()
    {
        mapstatconditionsdat ??= MapStatConditionsDat.Load(this).AsReadOnly();

        return mapstatconditionsdat;
    }

    /// <summary>
    /// Gets MapTierAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of MapTierAchievementsDat.</returns>
    public ReadOnlyCollection<MapTierAchievementsDat> GetMapTierAchievementsDat()
    {
        maptierachievementsdat ??= MapTierAchievementsDat.Load(this).AsReadOnly();

        return maptierachievementsdat;
    }

    /// <summary>
    /// Gets MapTiersDat data.
    /// </summary>
    /// <returns>readonly collection of MapTiersDat.</returns>
    public ReadOnlyCollection<MapTiersDat> GetMapTiersDat()
    {
        maptiersdat ??= MapTiersDat.Load(this).AsReadOnly();

        return maptiersdat;
    }

    /// <summary>
    /// Gets MasterHideoutLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of MasterHideoutLevelsDat.</returns>
    public ReadOnlyCollection<MasterHideoutLevelsDat> GetMasterHideoutLevelsDat()
    {
        masterhideoutlevelsdat ??= MasterHideoutLevelsDat.Load(this).AsReadOnly();

        return masterhideoutlevelsdat;
    }

    /// <summary>
    /// Gets MeleeDat data.
    /// </summary>
    /// <returns>readonly collection of MeleeDat.</returns>
    public ReadOnlyCollection<MeleeDat> GetMeleeDat()
    {
        meleedat ??= MeleeDat.Load(this).AsReadOnly();

        return meleedat;
    }

    /// <summary>
    /// Gets MeleeTrailsDat data.
    /// </summary>
    /// <returns>readonly collection of MeleeTrailsDat.</returns>
    public ReadOnlyCollection<MeleeTrailsDat> GetMeleeTrailsDat()
    {
        meleetrailsdat ??= MeleeTrailsDat.Load(this).AsReadOnly();

        return meleetrailsdat;
    }

    /// <summary>
    /// Gets MetamorphosisStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisStashTabLayoutDat.</returns>
    public ReadOnlyCollection<MetamorphosisStashTabLayoutDat> GetMetamorphosisStashTabLayoutDat()
    {
        metamorphosisstashtablayoutdat ??= MetamorphosisStashTabLayoutDat.Load(this).AsReadOnly();

        return metamorphosisstashtablayoutdat;
    }

    /// <summary>
    /// Gets MicroMigrationDataDat data.
    /// </summary>
    /// <returns>readonly collection of MicroMigrationDataDat.</returns>
    public ReadOnlyCollection<MicroMigrationDataDat> GetMicroMigrationDataDat()
    {
        micromigrationdatadat ??= MicroMigrationDataDat.Load(this).AsReadOnly();

        return micromigrationdatadat;
    }

    /// <summary>
    /// Gets MicrotransactionCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionCategoryDat.</returns>
    public ReadOnlyCollection<MicrotransactionCategoryDat> GetMicrotransactionCategoryDat()
    {
        microtransactioncategorydat ??= MicrotransactionCategoryDat.Load(this).AsReadOnly();

        return microtransactioncategorydat;
    }

    /// <summary>
    /// Gets MicrotransactionCharacterPortraitVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionCharacterPortraitVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionCharacterPortraitVariationsDat> GetMicrotransactionCharacterPortraitVariationsDat()
    {
        microtransactioncharacterportraitvariationsdat ??= MicrotransactionCharacterPortraitVariationsDat.Load(this).AsReadOnly();

        return microtransactioncharacterportraitvariationsdat;
    }

    /// <summary>
    /// Gets MicrotransactionCombineFormulaDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionCombineFormulaDat.</returns>
    public ReadOnlyCollection<MicrotransactionCombineFormulaDat> GetMicrotransactionCombineFormulaDat()
    {
        microtransactioncombineformuladat ??= MicrotransactionCombineFormulaDat.Load(this).AsReadOnly();

        return microtransactioncombineformuladat;
    }

    /// <summary>
    /// Gets MicrotransactionCursorVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionCursorVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionCursorVariationsDat> GetMicrotransactionCursorVariationsDat()
    {
        microtransactioncursorvariationsdat ??= MicrotransactionCursorVariationsDat.Load(this).AsReadOnly();

        return microtransactioncursorvariationsdat;
    }

    /// <summary>
    /// Gets MicrotransactionFireworksVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionFireworksVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionFireworksVariationsDat> GetMicrotransactionFireworksVariationsDat()
    {
        microtransactionfireworksvariationsdat ??= MicrotransactionFireworksVariationsDat.Load(this).AsReadOnly();

        return microtransactionfireworksvariationsdat;
    }

    /// <summary>
    /// Gets MicrotransactionGemCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionGemCategoryDat.</returns>
    public ReadOnlyCollection<MicrotransactionGemCategoryDat> GetMicrotransactionGemCategoryDat()
    {
        microtransactiongemcategorydat ??= MicrotransactionGemCategoryDat.Load(this).AsReadOnly();

        return microtransactiongemcategorydat;
    }

    /// <summary>
    /// Gets MicrotransactionPeriodicCharacterEffectVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionPeriodicCharacterEffectVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionPeriodicCharacterEffectVariationsDat> GetMicrotransactionPeriodicCharacterEffectVariationsDat()
    {
        microtransactionperiodiccharactereffectvariationsdat ??= MicrotransactionPeriodicCharacterEffectVariationsDat.Load(this).AsReadOnly();

        return microtransactionperiodiccharactereffectvariationsdat;
    }

    /// <summary>
    /// Gets MicrotransactionPortalVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionPortalVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionPortalVariationsDat> GetMicrotransactionPortalVariationsDat()
    {
        microtransactionportalvariationsdat ??= MicrotransactionPortalVariationsDat.Load(this).AsReadOnly();

        return microtransactionportalvariationsdat;
    }

    /// <summary>
    /// Gets MicrotransactionRarityDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionRarityDisplayDat.</returns>
    public ReadOnlyCollection<MicrotransactionRarityDisplayDat> GetMicrotransactionRarityDisplayDat()
    {
        microtransactionraritydisplaydat ??= MicrotransactionRarityDisplayDat.Load(this).AsReadOnly();

        return microtransactionraritydisplaydat;
    }

    /// <summary>
    /// Gets MicrotransactionRecycleOutcomesDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionRecycleOutcomesDat.</returns>
    public ReadOnlyCollection<MicrotransactionRecycleOutcomesDat> GetMicrotransactionRecycleOutcomesDat()
    {
        microtransactionrecycleoutcomesdat ??= MicrotransactionRecycleOutcomesDat.Load(this).AsReadOnly();

        return microtransactionrecycleoutcomesdat;
    }

    /// <summary>
    /// Gets MicrotransactionRecycleSalvageValuesDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionRecycleSalvageValuesDat.</returns>
    public ReadOnlyCollection<MicrotransactionRecycleSalvageValuesDat> GetMicrotransactionRecycleSalvageValuesDat()
    {
        microtransactionrecyclesalvagevaluesdat ??= MicrotransactionRecycleSalvageValuesDat.Load(this).AsReadOnly();

        return microtransactionrecyclesalvagevaluesdat;
    }

    /// <summary>
    /// Gets MicrotransactionSlotDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionSlotDat.</returns>
    public ReadOnlyCollection<MicrotransactionSlotDat> GetMicrotransactionSlotDat()
    {
        microtransactionslotdat ??= MicrotransactionSlotDat.Load(this).AsReadOnly();

        return microtransactionslotdat;
    }

    /// <summary>
    /// Gets MicrotransactionSocialFrameVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionSocialFrameVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionSocialFrameVariationsDat> GetMicrotransactionSocialFrameVariationsDat()
    {
        microtransactionsocialframevariationsdat ??= MicrotransactionSocialFrameVariationsDat.Load(this).AsReadOnly();

        return microtransactionsocialframevariationsdat;
    }

    /// <summary>
    /// Gets MinimapIconsDat data.
    /// </summary>
    /// <returns>readonly collection of MinimapIconsDat.</returns>
    public ReadOnlyCollection<MinimapIconsDat> GetMinimapIconsDat()
    {
        minimapiconsdat ??= MinimapIconsDat.Load(this).AsReadOnly();

        return minimapiconsdat;
    }

    /// <summary>
    /// Gets MiniQuestStatesDat data.
    /// </summary>
    /// <returns>readonly collection of MiniQuestStatesDat.</returns>
    public ReadOnlyCollection<MiniQuestStatesDat> GetMiniQuestStatesDat()
    {
        miniqueststatesdat ??= MiniQuestStatesDat.Load(this).AsReadOnly();

        return miniqueststatesdat;
    }

    /// <summary>
    /// Gets MiscAnimatedDat data.
    /// </summary>
    /// <returns>readonly collection of MiscAnimatedDat.</returns>
    public ReadOnlyCollection<MiscAnimatedDat> GetMiscAnimatedDat()
    {
        miscanimateddat ??= MiscAnimatedDat.Load(this).AsReadOnly();

        return miscanimateddat;
    }

    /// <summary>
    /// Gets MiscAnimatedArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscAnimatedArtVariationsDat.</returns>
    public ReadOnlyCollection<MiscAnimatedArtVariationsDat> GetMiscAnimatedArtVariationsDat()
    {
        miscanimatedartvariationsdat ??= MiscAnimatedArtVariationsDat.Load(this).AsReadOnly();

        return miscanimatedartvariationsdat;
    }

    /// <summary>
    /// Gets MiscBeamsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscBeamsDat.</returns>
    public ReadOnlyCollection<MiscBeamsDat> GetMiscBeamsDat()
    {
        miscbeamsdat ??= MiscBeamsDat.Load(this).AsReadOnly();

        return miscbeamsdat;
    }

    /// <summary>
    /// Gets MiscBeamsArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscBeamsArtVariationsDat.</returns>
    public ReadOnlyCollection<MiscBeamsArtVariationsDat> GetMiscBeamsArtVariationsDat()
    {
        miscbeamsartvariationsdat ??= MiscBeamsArtVariationsDat.Load(this).AsReadOnly();

        return miscbeamsartvariationsdat;
    }

    /// <summary>
    /// Gets MiscEffectPacksDat data.
    /// </summary>
    /// <returns>readonly collection of MiscEffectPacksDat.</returns>
    public ReadOnlyCollection<MiscEffectPacksDat> GetMiscEffectPacksDat()
    {
        misceffectpacksdat ??= MiscEffectPacksDat.Load(this).AsReadOnly();

        return misceffectpacksdat;
    }

    /// <summary>
    /// Gets MiscEffectPacksArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscEffectPacksArtVariationsDat.</returns>
    public ReadOnlyCollection<MiscEffectPacksArtVariationsDat> GetMiscEffectPacksArtVariationsDat()
    {
        misceffectpacksartvariationsdat ??= MiscEffectPacksArtVariationsDat.Load(this).AsReadOnly();

        return misceffectpacksartvariationsdat;
    }

    /// <summary>
    /// Gets MiscObjectsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscObjectsDat.</returns>
    public ReadOnlyCollection<MiscObjectsDat> GetMiscObjectsDat()
    {
        miscobjectsdat ??= MiscObjectsDat.Load(this).AsReadOnly();

        return miscobjectsdat;
    }

    /// <summary>
    /// Gets MiscObjectsArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscObjectsArtVariationsDat.</returns>
    public ReadOnlyCollection<MiscObjectsArtVariationsDat> GetMiscObjectsArtVariationsDat()
    {
        miscobjectsartvariationsdat ??= MiscObjectsArtVariationsDat.Load(this).AsReadOnly();

        return miscobjectsartvariationsdat;
    }

    /// <summary>
    /// Gets MissionFavourPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of MissionFavourPerLevelDat.</returns>
    public ReadOnlyCollection<MissionFavourPerLevelDat> GetMissionFavourPerLevelDat()
    {
        missionfavourperleveldat ??= MissionFavourPerLevelDat.Load(this).AsReadOnly();

        return missionfavourperleveldat;
    }

    /// <summary>
    /// Gets MissionTimerTypesDat data.
    /// </summary>
    /// <returns>readonly collection of MissionTimerTypesDat.</returns>
    public ReadOnlyCollection<MissionTimerTypesDat> GetMissionTimerTypesDat()
    {
        missiontimertypesdat ??= MissionTimerTypesDat.Load(this).AsReadOnly();

        return missiontimertypesdat;
    }

    /// <summary>
    /// Gets MissionTransitionTilesDat data.
    /// </summary>
    /// <returns>readonly collection of MissionTransitionTilesDat.</returns>
    public ReadOnlyCollection<MissionTransitionTilesDat> GetMissionTransitionTilesDat()
    {
        missiontransitiontilesdat ??= MissionTransitionTilesDat.Load(this).AsReadOnly();

        return missiontransitiontilesdat;
    }

    /// <summary>
    /// Gets ModEffectStatsDat data.
    /// </summary>
    /// <returns>readonly collection of ModEffectStatsDat.</returns>
    public ReadOnlyCollection<ModEffectStatsDat> GetModEffectStatsDat()
    {
        modeffectstatsdat ??= ModEffectStatsDat.Load(this).AsReadOnly();

        return modeffectstatsdat;
    }

    /// <summary>
    /// Gets ModEquivalenciesDat data.
    /// </summary>
    /// <returns>readonly collection of ModEquivalenciesDat.</returns>
    public ReadOnlyCollection<ModEquivalenciesDat> GetModEquivalenciesDat()
    {
        modequivalenciesdat ??= ModEquivalenciesDat.Load(this).AsReadOnly();

        return modequivalenciesdat;
    }

    /// <summary>
    /// Gets ModFamilyDat data.
    /// </summary>
    /// <returns>readonly collection of ModFamilyDat.</returns>
    public ReadOnlyCollection<ModFamilyDat> GetModFamilyDat()
    {
        modfamilydat ??= ModFamilyDat.Load(this).AsReadOnly();

        return modfamilydat;
    }

    /// <summary>
    /// Gets ModsDat data.
    /// </summary>
    /// <returns>readonly collection of ModsDat.</returns>
    public ReadOnlyCollection<ModsDat> GetModsDat()
    {
        modsdat ??= ModsDat.Load(this).AsReadOnly();

        return modsdat;
    }

    /// <summary>
    /// Gets ModSellPriceTypesDat data.
    /// </summary>
    /// <returns>readonly collection of ModSellPriceTypesDat.</returns>
    public ReadOnlyCollection<ModSellPriceTypesDat> GetModSellPriceTypesDat()
    {
        modsellpricetypesdat ??= ModSellPriceTypesDat.Load(this).AsReadOnly();

        return modsellpricetypesdat;
    }

    /// <summary>
    /// Gets ModTypeDat data.
    /// </summary>
    /// <returns>readonly collection of ModTypeDat.</returns>
    public ReadOnlyCollection<ModTypeDat> GetModTypeDat()
    {
        modtypedat ??= ModTypeDat.Load(this).AsReadOnly();

        return modtypedat;
    }

    /// <summary>
    /// Gets MonsterArmoursDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterArmoursDat.</returns>
    public ReadOnlyCollection<MonsterArmoursDat> GetMonsterArmoursDat()
    {
        monsterarmoursdat ??= MonsterArmoursDat.Load(this).AsReadOnly();

        return monsterarmoursdat;
    }

    /// <summary>
    /// Gets MonsterBonusesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterBonusesDat.</returns>
    public ReadOnlyCollection<MonsterBonusesDat> GetMonsterBonusesDat()
    {
        monsterbonusesdat ??= MonsterBonusesDat.Load(this).AsReadOnly();

        return monsterbonusesdat;
    }

    /// <summary>
    /// Gets MonsterConditionalEffectPacksDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterConditionalEffectPacksDat.</returns>
    public ReadOnlyCollection<MonsterConditionalEffectPacksDat> GetMonsterConditionalEffectPacksDat()
    {
        monsterconditionaleffectpacksdat ??= MonsterConditionalEffectPacksDat.Load(this).AsReadOnly();

        return monsterconditionaleffectpacksdat;
    }

    /// <summary>
    /// Gets MonsterConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterConditionsDat.</returns>
    public ReadOnlyCollection<MonsterConditionsDat> GetMonsterConditionsDat()
    {
        monsterconditionsdat ??= MonsterConditionsDat.Load(this).AsReadOnly();

        return monsterconditionsdat;
    }

    /// <summary>
    /// Gets MonsterDeathAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterDeathAchievementsDat.</returns>
    public ReadOnlyCollection<MonsterDeathAchievementsDat> GetMonsterDeathAchievementsDat()
    {
        monsterdeathachievementsdat ??= MonsterDeathAchievementsDat.Load(this).AsReadOnly();

        return monsterdeathachievementsdat;
    }

    /// <summary>
    /// Gets MonsterDeathConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterDeathConditionsDat.</returns>
    public ReadOnlyCollection<MonsterDeathConditionsDat> GetMonsterDeathConditionsDat()
    {
        monsterdeathconditionsdat ??= MonsterDeathConditionsDat.Load(this).AsReadOnly();

        return monsterdeathconditionsdat;
    }

    /// <summary>
    /// Gets MonsterGroupEntriesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterGroupEntriesDat.</returns>
    public ReadOnlyCollection<MonsterGroupEntriesDat> GetMonsterGroupEntriesDat()
    {
        monstergroupentriesdat ??= MonsterGroupEntriesDat.Load(this).AsReadOnly();

        return monstergroupentriesdat;
    }

    /// <summary>
    /// Gets MonsterHeightBracketsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterHeightBracketsDat.</returns>
    public ReadOnlyCollection<MonsterHeightBracketsDat> GetMonsterHeightBracketsDat()
    {
        monsterheightbracketsdat ??= MonsterHeightBracketsDat.Load(this).AsReadOnly();

        return monsterheightbracketsdat;
    }

    /// <summary>
    /// Gets MonsterHeightsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterHeightsDat.</returns>
    public ReadOnlyCollection<MonsterHeightsDat> GetMonsterHeightsDat()
    {
        monsterheightsdat ??= MonsterHeightsDat.Load(this).AsReadOnly();

        return monsterheightsdat;
    }

    /// <summary>
    /// Gets MonsterMapBossDifficultyDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterMapBossDifficultyDat.</returns>
    public ReadOnlyCollection<MonsterMapBossDifficultyDat> GetMonsterMapBossDifficultyDat()
    {
        monstermapbossdifficultydat ??= MonsterMapBossDifficultyDat.Load(this).AsReadOnly();

        return monstermapbossdifficultydat;
    }

    /// <summary>
    /// Gets MonsterMapDifficultyDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterMapDifficultyDat.</returns>
    public ReadOnlyCollection<MonsterMapDifficultyDat> GetMonsterMapDifficultyDat()
    {
        monstermapdifficultydat ??= MonsterMapDifficultyDat.Load(this).AsReadOnly();

        return monstermapdifficultydat;
    }

    /// <summary>
    /// Gets MonsterMortarDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterMortarDat.</returns>
    public ReadOnlyCollection<MonsterMortarDat> GetMonsterMortarDat()
    {
        monstermortardat ??= MonsterMortarDat.Load(this).AsReadOnly();

        return monstermortardat;
    }

    /// <summary>
    /// Gets MonsterPackCountsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterPackCountsDat.</returns>
    public ReadOnlyCollection<MonsterPackCountsDat> GetMonsterPackCountsDat()
    {
        monsterpackcountsdat ??= MonsterPackCountsDat.Load(this).AsReadOnly();

        return monsterpackcountsdat;
    }

    /// <summary>
    /// Gets MonsterPackEntriesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterPackEntriesDat.</returns>
    public ReadOnlyCollection<MonsterPackEntriesDat> GetMonsterPackEntriesDat()
    {
        monsterpackentriesdat ??= MonsterPackEntriesDat.Load(this).AsReadOnly();

        return monsterpackentriesdat;
    }

    /// <summary>
    /// Gets MonsterPacksDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterPacksDat.</returns>
    public ReadOnlyCollection<MonsterPacksDat> GetMonsterPacksDat()
    {
        monsterpacksdat ??= MonsterPacksDat.Load(this).AsReadOnly();

        return monsterpacksdat;
    }

    /// <summary>
    /// Gets MonsterProjectileAttackDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterProjectileAttackDat.</returns>
    public ReadOnlyCollection<MonsterProjectileAttackDat> GetMonsterProjectileAttackDat()
    {
        monsterprojectileattackdat ??= MonsterProjectileAttackDat.Load(this).AsReadOnly();

        return monsterprojectileattackdat;
    }

    /// <summary>
    /// Gets MonsterProjectileSpellDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterProjectileSpellDat.</returns>
    public ReadOnlyCollection<MonsterProjectileSpellDat> GetMonsterProjectileSpellDat()
    {
        monsterprojectilespelldat ??= MonsterProjectileSpellDat.Load(this).AsReadOnly();

        return monsterprojectilespelldat;
    }

    /// <summary>
    /// Gets MonsterResistancesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterResistancesDat.</returns>
    public ReadOnlyCollection<MonsterResistancesDat> GetMonsterResistancesDat()
    {
        monsterresistancesdat ??= MonsterResistancesDat.Load(this).AsReadOnly();

        return monsterresistancesdat;
    }

    /// <summary>
    /// Gets MonsterSegmentsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterSegmentsDat.</returns>
    public ReadOnlyCollection<MonsterSegmentsDat> GetMonsterSegmentsDat()
    {
        monstersegmentsdat ??= MonsterSegmentsDat.Load(this).AsReadOnly();

        return monstersegmentsdat;
    }

    /// <summary>
    /// Gets MonsterSpawnerGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterSpawnerGroupsDat.</returns>
    public ReadOnlyCollection<MonsterSpawnerGroupsDat> GetMonsterSpawnerGroupsDat()
    {
        monsterspawnergroupsdat ??= MonsterSpawnerGroupsDat.Load(this).AsReadOnly();

        return monsterspawnergroupsdat;
    }

    /// <summary>
    /// Gets MonsterSpawnerGroupsPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterSpawnerGroupsPerLevelDat.</returns>
    public ReadOnlyCollection<MonsterSpawnerGroupsPerLevelDat> GetMonsterSpawnerGroupsPerLevelDat()
    {
        monsterspawnergroupsperleveldat ??= MonsterSpawnerGroupsPerLevelDat.Load(this).AsReadOnly();

        return monsterspawnergroupsperleveldat;
    }

    /// <summary>
    /// Gets MonsterSpawnerOverridesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterSpawnerOverridesDat.</returns>
    public ReadOnlyCollection<MonsterSpawnerOverridesDat> GetMonsterSpawnerOverridesDat()
    {
        monsterspawneroverridesdat ??= MonsterSpawnerOverridesDat.Load(this).AsReadOnly();

        return monsterspawneroverridesdat;
    }

    /// <summary>
    /// Gets MonsterTypesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterTypesDat.</returns>
    public ReadOnlyCollection<MonsterTypesDat> GetMonsterTypesDat()
    {
        monstertypesdat ??= MonsterTypesDat.Load(this).AsReadOnly();

        return monstertypesdat;
    }

    /// <summary>
    /// Gets MonsterVarietiesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterVarietiesDat.</returns>
    public ReadOnlyCollection<MonsterVarietiesDat> GetMonsterVarietiesDat()
    {
        monstervarietiesdat ??= MonsterVarietiesDat.Load(this).AsReadOnly();

        return monstervarietiesdat;
    }

    /// <summary>
    /// Gets MonsterVarietiesArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterVarietiesArtVariationsDat.</returns>
    public ReadOnlyCollection<MonsterVarietiesArtVariationsDat> GetMonsterVarietiesArtVariationsDat()
    {
        monstervarietiesartvariationsdat ??= MonsterVarietiesArtVariationsDat.Load(this).AsReadOnly();

        return monstervarietiesartvariationsdat;
    }

    /// <summary>
    /// Gets MouseCursorSizeSettingsDat data.
    /// </summary>
    /// <returns>readonly collection of MouseCursorSizeSettingsDat.</returns>
    public ReadOnlyCollection<MouseCursorSizeSettingsDat> GetMouseCursorSizeSettingsDat()
    {
        mousecursorsizesettingsdat ??= MouseCursorSizeSettingsDat.Load(this).AsReadOnly();

        return mousecursorsizesettingsdat;
    }

    /// <summary>
    /// Gets MoveDaemonDat data.
    /// </summary>
    /// <returns>readonly collection of MoveDaemonDat.</returns>
    public ReadOnlyCollection<MoveDaemonDat> GetMoveDaemonDat()
    {
        movedaemondat ??= MoveDaemonDat.Load(this).AsReadOnly();

        return movedaemondat;
    }

    /// <summary>
    /// Gets MTXSetBonusDat data.
    /// </summary>
    /// <returns>readonly collection of MTXSetBonusDat.</returns>
    public ReadOnlyCollection<MTXSetBonusDat> GetMTXSetBonusDat()
    {
        mtxsetbonusdat ??= MTXSetBonusDat.Load(this).AsReadOnly();

        return mtxsetbonusdat;
    }

    /// <summary>
    /// Gets MultiPartAchievementAreasDat data.
    /// </summary>
    /// <returns>readonly collection of MultiPartAchievementAreasDat.</returns>
    public ReadOnlyCollection<MultiPartAchievementAreasDat> GetMultiPartAchievementAreasDat()
    {
        multipartachievementareasdat ??= MultiPartAchievementAreasDat.Load(this).AsReadOnly();

        return multipartachievementareasdat;
    }

    /// <summary>
    /// Gets MultiPartAchievementConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of MultiPartAchievementConditionsDat.</returns>
    public ReadOnlyCollection<MultiPartAchievementConditionsDat> GetMultiPartAchievementConditionsDat()
    {
        multipartachievementconditionsdat ??= MultiPartAchievementConditionsDat.Load(this).AsReadOnly();

        return multipartachievementconditionsdat;
    }

    /// <summary>
    /// Gets MultiPartAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of MultiPartAchievementsDat.</returns>
    public ReadOnlyCollection<MultiPartAchievementsDat> GetMultiPartAchievementsDat()
    {
        multipartachievementsdat ??= MultiPartAchievementsDat.Load(this).AsReadOnly();

        return multipartachievementsdat;
    }

    /// <summary>
    /// Gets MusicDat data.
    /// </summary>
    /// <returns>readonly collection of MusicDat.</returns>
    public ReadOnlyCollection<MusicDat> GetMusicDat()
    {
        musicdat ??= MusicDat.Load(this).AsReadOnly();

        return musicdat;
    }

    /// <summary>
    /// Gets MusicCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of MusicCategoriesDat.</returns>
    public ReadOnlyCollection<MusicCategoriesDat> GetMusicCategoriesDat()
    {
        musiccategoriesdat ??= MusicCategoriesDat.Load(this).AsReadOnly();

        return musiccategoriesdat;
    }

    /// <summary>
    /// Gets MysteryBoxesDat data.
    /// </summary>
    /// <returns>readonly collection of MysteryBoxesDat.</returns>
    public ReadOnlyCollection<MysteryBoxesDat> GetMysteryBoxesDat()
    {
        mysteryboxesdat ??= MysteryBoxesDat.Load(this).AsReadOnly();

        return mysteryboxesdat;
    }

    /// <summary>
    /// Gets NearbyMonsterConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of NearbyMonsterConditionsDat.</returns>
    public ReadOnlyCollection<NearbyMonsterConditionsDat> GetNearbyMonsterConditionsDat()
    {
        nearbymonsterconditionsdat ??= NearbyMonsterConditionsDat.Load(this).AsReadOnly();

        return nearbymonsterconditionsdat;
    }

    /// <summary>
    /// Gets NetTiersDat data.
    /// </summary>
    /// <returns>readonly collection of NetTiersDat.</returns>
    public ReadOnlyCollection<NetTiersDat> GetNetTiersDat()
    {
        nettiersdat ??= NetTiersDat.Load(this).AsReadOnly();

        return nettiersdat;
    }

    /// <summary>
    /// Gets NotificationsDat data.
    /// </summary>
    /// <returns>readonly collection of NotificationsDat.</returns>
    public ReadOnlyCollection<NotificationsDat> GetNotificationsDat()
    {
        notificationsdat ??= NotificationsDat.Load(this).AsReadOnly();

        return notificationsdat;
    }

    /// <summary>
    /// Gets NPCAudioDat data.
    /// </summary>
    /// <returns>readonly collection of NPCAudioDat.</returns>
    public ReadOnlyCollection<NPCAudioDat> GetNPCAudioDat()
    {
        npcaudiodat ??= NPCAudioDat.Load(this).AsReadOnly();

        return npcaudiodat;
    }

    /// <summary>
    /// Gets NPCConversationsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCConversationsDat.</returns>
    public ReadOnlyCollection<NPCConversationsDat> GetNPCConversationsDat()
    {
        npcconversationsdat ??= NPCConversationsDat.Load(this).AsReadOnly();

        return npcconversationsdat;
    }

    /// <summary>
    /// Gets NPCDialogueStylesDat data.
    /// </summary>
    /// <returns>readonly collection of NPCDialogueStylesDat.</returns>
    public ReadOnlyCollection<NPCDialogueStylesDat> GetNPCDialogueStylesDat()
    {
        npcdialoguestylesdat ??= NPCDialogueStylesDat.Load(this).AsReadOnly();

        return npcdialoguestylesdat;
    }

    /// <summary>
    /// Gets NPCFollowerVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCFollowerVariationsDat.</returns>
    public ReadOnlyCollection<NPCFollowerVariationsDat> GetNPCFollowerVariationsDat()
    {
        npcfollowervariationsdat ??= NPCFollowerVariationsDat.Load(this).AsReadOnly();

        return npcfollowervariationsdat;
    }

    /// <summary>
    /// Gets NPCMasterDat data.
    /// </summary>
    /// <returns>readonly collection of NPCMasterDat.</returns>
    public ReadOnlyCollection<NPCMasterDat> GetNPCMasterDat()
    {
        npcmasterdat ??= NPCMasterDat.Load(this).AsReadOnly();

        return npcmasterdat;
    }

    /// <summary>
    /// Gets NPCPortraitsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCPortraitsDat.</returns>
    public ReadOnlyCollection<NPCPortraitsDat> GetNPCPortraitsDat()
    {
        npcportraitsdat ??= NPCPortraitsDat.Load(this).AsReadOnly();

        return npcportraitsdat;
    }

    /// <summary>
    /// Gets NPCsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCsDat.</returns>
    public ReadOnlyCollection<NPCsDat> GetNPCsDat()
    {
        npcsdat ??= NPCsDat.Load(this).AsReadOnly();

        return npcsdat;
    }

    /// <summary>
    /// Gets NPCShopDat data.
    /// </summary>
    /// <returns>readonly collection of NPCShopDat.</returns>
    public ReadOnlyCollection<NPCShopDat> GetNPCShopDat()
    {
        npcshopdat ??= NPCShopDat.Load(this).AsReadOnly();

        return npcshopdat;
    }

    /// <summary>
    /// Gets NPCShopsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCShopsDat.</returns>
    public ReadOnlyCollection<NPCShopsDat> GetNPCShopsDat()
    {
        npcshopsdat ??= NPCShopsDat.Load(this).AsReadOnly();

        return npcshopsdat;
    }

    /// <summary>
    /// Gets NPCTalkDat data.
    /// </summary>
    /// <returns>readonly collection of NPCTalkDat.</returns>
    public ReadOnlyCollection<NPCTalkDat> GetNPCTalkDat()
    {
        npctalkdat ??= NPCTalkDat.Load(this).AsReadOnly();

        return npctalkdat;
    }

    /// <summary>
    /// Gets NPCTalkCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of NPCTalkCategoryDat.</returns>
    public ReadOnlyCollection<NPCTalkCategoryDat> GetNPCTalkCategoryDat()
    {
        npctalkcategorydat ??= NPCTalkCategoryDat.Load(this).AsReadOnly();

        return npctalkcategorydat;
    }

    /// <summary>
    /// Gets NPCTalkConsoleQuickActionsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCTalkConsoleQuickActionsDat.</returns>
    public ReadOnlyCollection<NPCTalkConsoleQuickActionsDat> GetNPCTalkConsoleQuickActionsDat()
    {
        npctalkconsolequickactionsdat ??= NPCTalkConsoleQuickActionsDat.Load(this).AsReadOnly();

        return npctalkconsolequickactionsdat;
    }

    /// <summary>
    /// Gets NPCTextAudioDat data.
    /// </summary>
    /// <returns>readonly collection of NPCTextAudioDat.</returns>
    public ReadOnlyCollection<NPCTextAudioDat> GetNPCTextAudioDat()
    {
        npctextaudiodat ??= NPCTextAudioDat.Load(this).AsReadOnly();

        return npctextaudiodat;
    }

    /// <summary>
    /// Gets OnKillAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of OnKillAchievementsDat.</returns>
    public ReadOnlyCollection<OnKillAchievementsDat> GetOnKillAchievementsDat()
    {
        onkillachievementsdat ??= OnKillAchievementsDat.Load(this).AsReadOnly();

        return onkillachievementsdat;
    }

    /// <summary>
    /// Gets PackFormationDat data.
    /// </summary>
    /// <returns>readonly collection of PackFormationDat.</returns>
    public ReadOnlyCollection<PackFormationDat> GetPackFormationDat()
    {
        packformationdat ??= PackFormationDat.Load(this).AsReadOnly();

        return packformationdat;
    }

    /// <summary>
    /// Gets PassiveJewelRadiiDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveJewelRadiiDat.</returns>
    public ReadOnlyCollection<PassiveJewelRadiiDat> GetPassiveJewelRadiiDat()
    {
        passivejewelradiidat ??= PassiveJewelRadiiDat.Load(this).AsReadOnly();

        return passivejewelradiidat;
    }

    /// <summary>
    /// Gets PassiveJewelSlotsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveJewelSlotsDat.</returns>
    public ReadOnlyCollection<PassiveJewelSlotsDat> GetPassiveJewelSlotsDat()
    {
        passivejewelslotsdat ??= PassiveJewelSlotsDat.Load(this).AsReadOnly();

        return passivejewelslotsdat;
    }

    /// <summary>
    /// Gets PassiveSkillFilterCatagoriesDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillFilterCatagoriesDat.</returns>
    public ReadOnlyCollection<PassiveSkillFilterCatagoriesDat> GetPassiveSkillFilterCatagoriesDat()
    {
        passiveskillfiltercatagoriesdat ??= PassiveSkillFilterCatagoriesDat.Load(this).AsReadOnly();

        return passiveskillfiltercatagoriesdat;
    }

    /// <summary>
    /// Gets PassiveSkillFilterOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillFilterOptionsDat.</returns>
    public ReadOnlyCollection<PassiveSkillFilterOptionsDat> GetPassiveSkillFilterOptionsDat()
    {
        passiveskillfilteroptionsdat ??= PassiveSkillFilterOptionsDat.Load(this).AsReadOnly();

        return passiveskillfilteroptionsdat;
    }

    /// <summary>
    /// Gets PassiveSkillMasteryGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillMasteryGroupsDat.</returns>
    public ReadOnlyCollection<PassiveSkillMasteryGroupsDat> GetPassiveSkillMasteryGroupsDat()
    {
        passiveskillmasterygroupsdat ??= PassiveSkillMasteryGroupsDat.Load(this).AsReadOnly();

        return passiveskillmasterygroupsdat;
    }

    /// <summary>
    /// Gets PassiveSkillMasteryEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillMasteryEffectsDat.</returns>
    public ReadOnlyCollection<PassiveSkillMasteryEffectsDat> GetPassiveSkillMasteryEffectsDat()
    {
        passiveskillmasteryeffectsdat ??= PassiveSkillMasteryEffectsDat.Load(this).AsReadOnly();

        return passiveskillmasteryeffectsdat;
    }

    /// <summary>
    /// Gets PassiveSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillsDat.</returns>
    public ReadOnlyCollection<PassiveSkillsDat> GetPassiveSkillsDat()
    {
        passiveskillsdat ??= PassiveSkillsDat.Load(this).AsReadOnly();

        return passiveskillsdat;
    }

    /// <summary>
    /// Gets PassiveSkillStatCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillStatCategoriesDat.</returns>
    public ReadOnlyCollection<PassiveSkillStatCategoriesDat> GetPassiveSkillStatCategoriesDat()
    {
        passiveskillstatcategoriesdat ??= PassiveSkillStatCategoriesDat.Load(this).AsReadOnly();

        return passiveskillstatcategoriesdat;
    }

    /// <summary>
    /// Gets PassiveSkillTreesDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillTreesDat.</returns>
    public ReadOnlyCollection<PassiveSkillTreesDat> GetPassiveSkillTreesDat()
    {
        passiveskilltreesdat ??= PassiveSkillTreesDat.Load(this).AsReadOnly();

        return passiveskilltreesdat;
    }

    /// <summary>
    /// Gets PassiveSkillTreeTutorialDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillTreeTutorialDat.</returns>
    public ReadOnlyCollection<PassiveSkillTreeTutorialDat> GetPassiveSkillTreeTutorialDat()
    {
        passiveskilltreetutorialdat ??= PassiveSkillTreeTutorialDat.Load(this).AsReadOnly();

        return passiveskilltreetutorialdat;
    }

    /// <summary>
    /// Gets PassiveSkillTreeUIArtDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillTreeUIArtDat.</returns>
    public ReadOnlyCollection<PassiveSkillTreeUIArtDat> GetPassiveSkillTreeUIArtDat()
    {
        passiveskilltreeuiartdat ??= PassiveSkillTreeUIArtDat.Load(this).AsReadOnly();

        return passiveskilltreeuiartdat;
    }

    /// <summary>
    /// Gets PassiveTreeExpansionJewelsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveTreeExpansionJewelsDat.</returns>
    public ReadOnlyCollection<PassiveTreeExpansionJewelsDat> GetPassiveTreeExpansionJewelsDat()
    {
        passivetreeexpansionjewelsdat ??= PassiveTreeExpansionJewelsDat.Load(this).AsReadOnly();

        return passivetreeexpansionjewelsdat;
    }

    /// <summary>
    /// Gets PassiveTreeExpansionJewelSizesDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveTreeExpansionJewelSizesDat.</returns>
    public ReadOnlyCollection<PassiveTreeExpansionJewelSizesDat> GetPassiveTreeExpansionJewelSizesDat()
    {
        passivetreeexpansionjewelsizesdat ??= PassiveTreeExpansionJewelSizesDat.Load(this).AsReadOnly();

        return passivetreeexpansionjewelsizesdat;
    }

    /// <summary>
    /// Gets PassiveTreeExpansionSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveTreeExpansionSkillsDat.</returns>
    public ReadOnlyCollection<PassiveTreeExpansionSkillsDat> GetPassiveTreeExpansionSkillsDat()
    {
        passivetreeexpansionskillsdat ??= PassiveTreeExpansionSkillsDat.Load(this).AsReadOnly();

        return passivetreeexpansionskillsdat;
    }

    /// <summary>
    /// Gets PassiveTreeExpansionSpecialSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveTreeExpansionSpecialSkillsDat.</returns>
    public ReadOnlyCollection<PassiveTreeExpansionSpecialSkillsDat> GetPassiveTreeExpansionSpecialSkillsDat()
    {
        passivetreeexpansionspecialskillsdat ??= PassiveTreeExpansionSpecialSkillsDat.Load(this).AsReadOnly();

        return passivetreeexpansionspecialskillsdat;
    }

    /// <summary>
    /// Gets PCBangRewardMicrosDat data.
    /// </summary>
    /// <returns>readonly collection of PCBangRewardMicrosDat.</returns>
    public ReadOnlyCollection<PCBangRewardMicrosDat> GetPCBangRewardMicrosDat()
    {
        pcbangrewardmicrosdat ??= PCBangRewardMicrosDat.Load(this).AsReadOnly();

        return pcbangrewardmicrosdat;
    }

    /// <summary>
    /// Gets PetDat data.
    /// </summary>
    /// <returns>readonly collection of PetDat.</returns>
    public ReadOnlyCollection<PetDat> GetPetDat()
    {
        petdat ??= PetDat.Load(this).AsReadOnly();

        return petdat;
    }

    /// <summary>
    /// Gets PlayerConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of PlayerConditionsDat.</returns>
    public ReadOnlyCollection<PlayerConditionsDat> GetPlayerConditionsDat()
    {
        playerconditionsdat ??= PlayerConditionsDat.Load(this).AsReadOnly();

        return playerconditionsdat;
    }

    /// <summary>
    /// Gets PlayerTradeWhisperFormatsDat data.
    /// </summary>
    /// <returns>readonly collection of PlayerTradeWhisperFormatsDat.</returns>
    public ReadOnlyCollection<PlayerTradeWhisperFormatsDat> GetPlayerTradeWhisperFormatsDat()
    {
        playertradewhisperformatsdat ??= PlayerTradeWhisperFormatsDat.Load(this).AsReadOnly();

        return playertradewhisperformatsdat;
    }

    /// <summary>
    /// Gets PreloadGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of PreloadGroupsDat.</returns>
    public ReadOnlyCollection<PreloadGroupsDat> GetPreloadGroupsDat()
    {
        preloadgroupsdat ??= PreloadGroupsDat.Load(this).AsReadOnly();

        return preloadgroupsdat;
    }

    /// <summary>
    /// Gets ProjectilesDat data.
    /// </summary>
    /// <returns>readonly collection of ProjectilesDat.</returns>
    public ReadOnlyCollection<ProjectilesDat> GetProjectilesDat()
    {
        projectilesdat ??= ProjectilesDat.Load(this).AsReadOnly();

        return projectilesdat;
    }

    /// <summary>
    /// Gets ProjectilesArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of ProjectilesArtVariationsDat.</returns>
    public ReadOnlyCollection<ProjectilesArtVariationsDat> GetProjectilesArtVariationsDat()
    {
        projectilesartvariationsdat ??= ProjectilesArtVariationsDat.Load(this).AsReadOnly();

        return projectilesartvariationsdat;
    }

    /// <summary>
    /// Gets ProjectileVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of ProjectileVariationsDat.</returns>
    public ReadOnlyCollection<ProjectileVariationsDat> GetProjectileVariationsDat()
    {
        projectilevariationsdat ??= ProjectileVariationsDat.Load(this).AsReadOnly();

        return projectilevariationsdat;
    }

    /// <summary>
    /// Gets PVPTypesDat data.
    /// </summary>
    /// <returns>readonly collection of PVPTypesDat.</returns>
    public ReadOnlyCollection<PVPTypesDat> GetPVPTypesDat()
    {
        pvptypesdat ??= PVPTypesDat.Load(this).AsReadOnly();

        return pvptypesdat;
    }

    /// <summary>
    /// Gets QuestDat data.
    /// </summary>
    /// <returns>readonly collection of QuestDat.</returns>
    public ReadOnlyCollection<QuestDat> GetQuestDat()
    {
        questdat ??= QuestDat.Load(this).AsReadOnly();

        return questdat;
    }

    /// <summary>
    /// Gets QuestAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of QuestAchievementsDat.</returns>
    public ReadOnlyCollection<QuestAchievementsDat> GetQuestAchievementsDat()
    {
        questachievementsdat ??= QuestAchievementsDat.Load(this).AsReadOnly();

        return questachievementsdat;
    }

    /// <summary>
    /// Gets QuestFlagsDat data.
    /// </summary>
    /// <returns>readonly collection of QuestFlagsDat.</returns>
    public ReadOnlyCollection<QuestFlagsDat> GetQuestFlagsDat()
    {
        questflagsdat ??= QuestFlagsDat.Load(this).AsReadOnly();

        return questflagsdat;
    }

    /// <summary>
    /// Gets QuestItemsDat data.
    /// </summary>
    /// <returns>readonly collection of QuestItemsDat.</returns>
    public ReadOnlyCollection<QuestItemsDat> GetQuestItemsDat()
    {
        questitemsdat ??= QuestItemsDat.Load(this).AsReadOnly();

        return questitemsdat;
    }

    /// <summary>
    /// Gets QuestRewardOffersDat data.
    /// </summary>
    /// <returns>readonly collection of QuestRewardOffersDat.</returns>
    public ReadOnlyCollection<QuestRewardOffersDat> GetQuestRewardOffersDat()
    {
        questrewardoffersdat ??= QuestRewardOffersDat.Load(this).AsReadOnly();

        return questrewardoffersdat;
    }

    /// <summary>
    /// Gets QuestRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of QuestRewardsDat.</returns>
    public ReadOnlyCollection<QuestRewardsDat> GetQuestRewardsDat()
    {
        questrewardsdat ??= QuestRewardsDat.Load(this).AsReadOnly();

        return questrewardsdat;
    }

    /// <summary>
    /// Gets QuestStatesDat data.
    /// </summary>
    /// <returns>readonly collection of QuestStatesDat.</returns>
    public ReadOnlyCollection<QuestStatesDat> GetQuestStatesDat()
    {
        queststatesdat ??= QuestStatesDat.Load(this).AsReadOnly();

        return queststatesdat;
    }

    /// <summary>
    /// Gets QuestStaticRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of QuestStaticRewardsDat.</returns>
    public ReadOnlyCollection<QuestStaticRewardsDat> GetQuestStaticRewardsDat()
    {
        queststaticrewardsdat ??= QuestStaticRewardsDat.Load(this).AsReadOnly();

        return queststaticrewardsdat;
    }

    /// <summary>
    /// Gets QuestTrackerGroupDat data.
    /// </summary>
    /// <returns>readonly collection of QuestTrackerGroupDat.</returns>
    public ReadOnlyCollection<QuestTrackerGroupDat> GetQuestTrackerGroupDat()
    {
        questtrackergroupdat ??= QuestTrackerGroupDat.Load(this).AsReadOnly();

        return questtrackergroupdat;
    }

    /// <summary>
    /// Gets QuestTypeDat data.
    /// </summary>
    /// <returns>readonly collection of QuestTypeDat.</returns>
    public ReadOnlyCollection<QuestTypeDat> GetQuestTypeDat()
    {
        questtypedat ??= QuestTypeDat.Load(this).AsReadOnly();

        return questtypedat;
    }

    /// <summary>
    /// Gets RacesDat data.
    /// </summary>
    /// <returns>readonly collection of RacesDat.</returns>
    public ReadOnlyCollection<RacesDat> GetRacesDat()
    {
        racesdat ??= RacesDat.Load(this).AsReadOnly();

        return racesdat;
    }

    /// <summary>
    /// Gets RaceTimesDat data.
    /// </summary>
    /// <returns>readonly collection of RaceTimesDat.</returns>
    public ReadOnlyCollection<RaceTimesDat> GetRaceTimesDat()
    {
        racetimesdat ??= RaceTimesDat.Load(this).AsReadOnly();

        return racetimesdat;
    }

    /// <summary>
    /// Gets RareMonsterLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of RareMonsterLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<RareMonsterLifeScalingPerLevelDat> GetRareMonsterLifeScalingPerLevelDat()
    {
        raremonsterlifescalingperleveldat ??= RareMonsterLifeScalingPerLevelDat.Load(this).AsReadOnly();

        return raremonsterlifescalingperleveldat;
    }

    /// <summary>
    /// Gets RarityDat data.
    /// </summary>
    /// <returns>readonly collection of RarityDat.</returns>
    public ReadOnlyCollection<RarityDat> GetRarityDat()
    {
        raritydat ??= RarityDat.Load(this).AsReadOnly();

        return raritydat;
    }

    /// <summary>
    /// Gets RealmsDat data.
    /// </summary>
    /// <returns>readonly collection of RealmsDat.</returns>
    public ReadOnlyCollection<RealmsDat> GetRealmsDat()
    {
        realmsdat ??= RealmsDat.Load(this).AsReadOnly();

        return realmsdat;
    }

    /// <summary>
    /// Gets RecipeUnlockDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of RecipeUnlockDisplayDat.</returns>
    public ReadOnlyCollection<RecipeUnlockDisplayDat> GetRecipeUnlockDisplayDat()
    {
        recipeunlockdisplaydat ??= RecipeUnlockDisplayDat.Load(this).AsReadOnly();

        return recipeunlockdisplaydat;
    }

    /// <summary>
    /// Gets RecipeUnlockObjectsDat data.
    /// </summary>
    /// <returns>readonly collection of RecipeUnlockObjectsDat.</returns>
    public ReadOnlyCollection<RecipeUnlockObjectsDat> GetRecipeUnlockObjectsDat()
    {
        recipeunlockobjectsdat ??= RecipeUnlockObjectsDat.Load(this).AsReadOnly();

        return recipeunlockobjectsdat;
    }

    /// <summary>
    /// Gets ReminderTextDat data.
    /// </summary>
    /// <returns>readonly collection of ReminderTextDat.</returns>
    public ReadOnlyCollection<ReminderTextDat> GetReminderTextDat()
    {
        remindertextdat ??= ReminderTextDat.Load(this).AsReadOnly();

        return remindertextdat;
    }

    /// <summary>
    /// Gets RulesetsDat data.
    /// </summary>
    /// <returns>readonly collection of RulesetsDat.</returns>
    public ReadOnlyCollection<RulesetsDat> GetRulesetsDat()
    {
        rulesetsdat ??= RulesetsDat.Load(this).AsReadOnly();

        return rulesetsdat;
    }

    /// <summary>
    /// Gets RunicCirclesDat data.
    /// </summary>
    /// <returns>readonly collection of RunicCirclesDat.</returns>
    public ReadOnlyCollection<RunicCirclesDat> GetRunicCirclesDat()
    {
        runiccirclesdat ??= RunicCirclesDat.Load(this).AsReadOnly();

        return runiccirclesdat;
    }

    /// <summary>
    /// Gets SalvageBoxesDat data.
    /// </summary>
    /// <returns>readonly collection of SalvageBoxesDat.</returns>
    public ReadOnlyCollection<SalvageBoxesDat> GetSalvageBoxesDat()
    {
        salvageboxesdat ??= SalvageBoxesDat.Load(this).AsReadOnly();

        return salvageboxesdat;
    }

    /// <summary>
    /// Gets SessionQuestFlagsDat data.
    /// </summary>
    /// <returns>readonly collection of SessionQuestFlagsDat.</returns>
    public ReadOnlyCollection<SessionQuestFlagsDat> GetSessionQuestFlagsDat()
    {
        sessionquestflagsdat ??= SessionQuestFlagsDat.Load(this).AsReadOnly();

        return sessionquestflagsdat;
    }

    /// <summary>
    /// Gets ShieldTypesDat data.
    /// </summary>
    /// <returns>readonly collection of ShieldTypesDat.</returns>
    public ReadOnlyCollection<ShieldTypesDat> GetShieldTypesDat()
    {
        shieldtypesdat ??= ShieldTypesDat.Load(this).AsReadOnly();

        return shieldtypesdat;
    }

    /// <summary>
    /// Gets ShopCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of ShopCategoryDat.</returns>
    public ReadOnlyCollection<ShopCategoryDat> GetShopCategoryDat()
    {
        shopcategorydat ??= ShopCategoryDat.Load(this).AsReadOnly();

        return shopcategorydat;
    }

    /// <summary>
    /// Gets ShopCountryDat data.
    /// </summary>
    /// <returns>readonly collection of ShopCountryDat.</returns>
    public ReadOnlyCollection<ShopCountryDat> GetShopCountryDat()
    {
        shopcountrydat ??= ShopCountryDat.Load(this).AsReadOnly();

        return shopcountrydat;
    }

    /// <summary>
    /// Gets ShopCurrencyDat data.
    /// </summary>
    /// <returns>readonly collection of ShopCurrencyDat.</returns>
    public ReadOnlyCollection<ShopCurrencyDat> GetShopCurrencyDat()
    {
        shopcurrencydat ??= ShopCurrencyDat.Load(this).AsReadOnly();

        return shopcurrencydat;
    }

    /// <summary>
    /// Gets ShopPaymentPackageDat data.
    /// </summary>
    /// <returns>readonly collection of ShopPaymentPackageDat.</returns>
    public ReadOnlyCollection<ShopPaymentPackageDat> GetShopPaymentPackageDat()
    {
        shoppaymentpackagedat ??= ShopPaymentPackageDat.Load(this).AsReadOnly();

        return shoppaymentpackagedat;
    }

    /// <summary>
    /// Gets ShopPaymentPackagePriceDat data.
    /// </summary>
    /// <returns>readonly collection of ShopPaymentPackagePriceDat.</returns>
    public ReadOnlyCollection<ShopPaymentPackagePriceDat> GetShopPaymentPackagePriceDat()
    {
        shoppaymentpackagepricedat ??= ShopPaymentPackagePriceDat.Load(this).AsReadOnly();

        return shoppaymentpackagepricedat;
    }

    /// <summary>
    /// Gets ShopRegionDat data.
    /// </summary>
    /// <returns>readonly collection of ShopRegionDat.</returns>
    public ReadOnlyCollection<ShopRegionDat> GetShopRegionDat()
    {
        shopregiondat ??= ShopRegionDat.Load(this).AsReadOnly();

        return shopregiondat;
    }

    /// <summary>
    /// Gets ShopTagDat data.
    /// </summary>
    /// <returns>readonly collection of ShopTagDat.</returns>
    public ReadOnlyCollection<ShopTagDat> GetShopTagDat()
    {
        shoptagdat ??= ShopTagDat.Load(this).AsReadOnly();

        return shoptagdat;
    }

    /// <summary>
    /// Gets ShopTokenDat data.
    /// </summary>
    /// <returns>readonly collection of ShopTokenDat.</returns>
    public ReadOnlyCollection<ShopTokenDat> GetShopTokenDat()
    {
        shoptokendat ??= ShopTokenDat.Load(this).AsReadOnly();

        return shoptokendat;
    }

    /// <summary>
    /// Gets SigilDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of SigilDisplayDat.</returns>
    public ReadOnlyCollection<SigilDisplayDat> GetSigilDisplayDat()
    {
        sigildisplaydat ??= SigilDisplayDat.Load(this).AsReadOnly();

        return sigildisplaydat;
    }

    /// <summary>
    /// Gets SingleGroundLaserDat data.
    /// </summary>
    /// <returns>readonly collection of SingleGroundLaserDat.</returns>
    public ReadOnlyCollection<SingleGroundLaserDat> GetSingleGroundLaserDat()
    {
        singlegroundlaserdat ??= SingleGroundLaserDat.Load(this).AsReadOnly();

        return singlegroundlaserdat;
    }

    /// <summary>
    /// Gets SkillArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillArtVariationsDat.</returns>
    public ReadOnlyCollection<SkillArtVariationsDat> GetSkillArtVariationsDat()
    {
        skillartvariationsdat ??= SkillArtVariationsDat.Load(this).AsReadOnly();

        return skillartvariationsdat;
    }

    /// <summary>
    /// Gets SkillGemInfoDat data.
    /// </summary>
    /// <returns>readonly collection of SkillGemInfoDat.</returns>
    public ReadOnlyCollection<SkillGemInfoDat> GetSkillGemInfoDat()
    {
        skillgeminfodat ??= SkillGemInfoDat.Load(this).AsReadOnly();

        return skillgeminfodat;
    }

    /// <summary>
    /// Gets SkillGemsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillGemsDat.</returns>
    public ReadOnlyCollection<SkillGemsDat> GetSkillGemsDat()
    {
        skillgemsdat ??= SkillGemsDat.Load(this).AsReadOnly();

        return skillgemsdat;
    }

    /// <summary>
    /// Gets SkillMineVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillMineVariationsDat.</returns>
    public ReadOnlyCollection<SkillMineVariationsDat> GetSkillMineVariationsDat()
    {
        skillminevariationsdat ??= SkillMineVariationsDat.Load(this).AsReadOnly();

        return skillminevariationsdat;
    }

    /// <summary>
    /// Gets SkillMorphDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of SkillMorphDisplayDat.</returns>
    public ReadOnlyCollection<SkillMorphDisplayDat> GetSkillMorphDisplayDat()
    {
        skillmorphdisplaydat ??= SkillMorphDisplayDat.Load(this).AsReadOnly();

        return skillmorphdisplaydat;
    }

    /// <summary>
    /// Gets SkillSurgeEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillSurgeEffectsDat.</returns>
    public ReadOnlyCollection<SkillSurgeEffectsDat> GetSkillSurgeEffectsDat()
    {
        skillsurgeeffectsdat ??= SkillSurgeEffectsDat.Load(this).AsReadOnly();

        return skillsurgeeffectsdat;
    }

    /// <summary>
    /// Gets SkillTotemVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillTotemVariationsDat.</returns>
    public ReadOnlyCollection<SkillTotemVariationsDat> GetSkillTotemVariationsDat()
    {
        skilltotemvariationsdat ??= SkillTotemVariationsDat.Load(this).AsReadOnly();

        return skilltotemvariationsdat;
    }

    /// <summary>
    /// Gets SkillTrapVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillTrapVariationsDat.</returns>
    public ReadOnlyCollection<SkillTrapVariationsDat> GetSkillTrapVariationsDat()
    {
        skilltrapvariationsdat ??= SkillTrapVariationsDat.Load(this).AsReadOnly();

        return skilltrapvariationsdat;
    }

    /// <summary>
    /// Gets SocketNotchesDat data.
    /// </summary>
    /// <returns>readonly collection of SocketNotchesDat.</returns>
    public ReadOnlyCollection<SocketNotchesDat> GetSocketNotchesDat()
    {
        socketnotchesdat ??= SocketNotchesDat.Load(this).AsReadOnly();

        return socketnotchesdat;
    }

    /// <summary>
    /// Gets SoundEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of SoundEffectsDat.</returns>
    public ReadOnlyCollection<SoundEffectsDat> GetSoundEffectsDat()
    {
        soundeffectsdat ??= SoundEffectsDat.Load(this).AsReadOnly();

        return soundeffectsdat;
    }

    /// <summary>
    /// Gets SpawnAdditionalChestsOrClustersDat data.
    /// </summary>
    /// <returns>readonly collection of SpawnAdditionalChestsOrClustersDat.</returns>
    public ReadOnlyCollection<SpawnAdditionalChestsOrClustersDat> GetSpawnAdditionalChestsOrClustersDat()
    {
        spawnadditionalchestsorclustersdat ??= SpawnAdditionalChestsOrClustersDat.Load(this).AsReadOnly();

        return spawnadditionalchestsorclustersdat;
    }

    /// <summary>
    /// Gets SpawnObjectDat data.
    /// </summary>
    /// <returns>readonly collection of SpawnObjectDat.</returns>
    public ReadOnlyCollection<SpawnObjectDat> GetSpawnObjectDat()
    {
        spawnobjectdat ??= SpawnObjectDat.Load(this).AsReadOnly();

        return spawnobjectdat;
    }

    /// <summary>
    /// Gets SpecialRoomsDat data.
    /// </summary>
    /// <returns>readonly collection of SpecialRoomsDat.</returns>
    public ReadOnlyCollection<SpecialRoomsDat> GetSpecialRoomsDat()
    {
        specialroomsdat ??= SpecialRoomsDat.Load(this).AsReadOnly();

        return specialroomsdat;
    }

    /// <summary>
    /// Gets SpecialTilesDat data.
    /// </summary>
    /// <returns>readonly collection of SpecialTilesDat.</returns>
    public ReadOnlyCollection<SpecialTilesDat> GetSpecialTilesDat()
    {
        specialtilesdat ??= SpecialTilesDat.Load(this).AsReadOnly();

        return specialtilesdat;
    }

    /// <summary>
    /// Gets SpectreOverridesDat data.
    /// </summary>
    /// <returns>readonly collection of SpectreOverridesDat.</returns>
    public ReadOnlyCollection<SpectreOverridesDat> GetSpectreOverridesDat()
    {
        spectreoverridesdat ??= SpectreOverridesDat.Load(this).AsReadOnly();

        return spectreoverridesdat;
    }

    /// <summary>
    /// Gets StartingPassiveSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of StartingPassiveSkillsDat.</returns>
    public ReadOnlyCollection<StartingPassiveSkillsDat> GetStartingPassiveSkillsDat()
    {
        startingpassiveskillsdat ??= StartingPassiveSkillsDat.Load(this).AsReadOnly();

        return startingpassiveskillsdat;
    }

    /// <summary>
    /// Gets StashTabAffinitiesDat data.
    /// </summary>
    /// <returns>readonly collection of StashTabAffinitiesDat.</returns>
    public ReadOnlyCollection<StashTabAffinitiesDat> GetStashTabAffinitiesDat()
    {
        stashtabaffinitiesdat ??= StashTabAffinitiesDat.Load(this).AsReadOnly();

        return stashtabaffinitiesdat;
    }

    /// <summary>
    /// Gets StashTypeDat data.
    /// </summary>
    /// <returns>readonly collection of StashTypeDat.</returns>
    public ReadOnlyCollection<StashTypeDat> GetStashTypeDat()
    {
        stashtypedat ??= StashTypeDat.Load(this).AsReadOnly();

        return stashtypedat;
    }

    /// <summary>
    /// Gets StatDescriptionFunctionsDat data.
    /// </summary>
    /// <returns>readonly collection of StatDescriptionFunctionsDat.</returns>
    public ReadOnlyCollection<StatDescriptionFunctionsDat> GetStatDescriptionFunctionsDat()
    {
        statdescriptionfunctionsdat ??= StatDescriptionFunctionsDat.Load(this).AsReadOnly();

        return statdescriptionfunctionsdat;
    }

    /// <summary>
    /// Gets StatsAffectingGenerationDat data.
    /// </summary>
    /// <returns>readonly collection of StatsAffectingGenerationDat.</returns>
    public ReadOnlyCollection<StatsAffectingGenerationDat> GetStatsAffectingGenerationDat()
    {
        statsaffectinggenerationdat ??= StatsAffectingGenerationDat.Load(this).AsReadOnly();

        return statsaffectinggenerationdat;
    }

    /// <summary>
    /// Gets StatsDat data.
    /// </summary>
    /// <returns>readonly collection of StatsDat.</returns>
    public ReadOnlyCollection<StatsDat> GetStatsDat()
    {
        statsdat ??= StatsDat.Load(this).AsReadOnly();

        return statsdat;
    }

    /// <summary>
    /// Gets StrDexIntMissionExtraRequirementDat data.
    /// </summary>
    /// <returns>readonly collection of StrDexIntMissionExtraRequirementDat.</returns>
    public ReadOnlyCollection<StrDexIntMissionExtraRequirementDat> GetStrDexIntMissionExtraRequirementDat()
    {
        strdexintmissionextrarequirementdat ??= StrDexIntMissionExtraRequirementDat.Load(this).AsReadOnly();

        return strdexintmissionextrarequirementdat;
    }

    /// <summary>
    /// Gets StrDexIntMissionsDat data.
    /// </summary>
    /// <returns>readonly collection of StrDexIntMissionsDat.</returns>
    public ReadOnlyCollection<StrDexIntMissionsDat> GetStrDexIntMissionsDat()
    {
        strdexintmissionsdat ??= StrDexIntMissionsDat.Load(this).AsReadOnly();

        return strdexintmissionsdat;
    }

    /// <summary>
    /// Gets SuicideExplosionDat data.
    /// </summary>
    /// <returns>readonly collection of SuicideExplosionDat.</returns>
    public ReadOnlyCollection<SuicideExplosionDat> GetSuicideExplosionDat()
    {
        suicideexplosiondat ??= SuicideExplosionDat.Load(this).AsReadOnly();

        return suicideexplosiondat;
    }

    /// <summary>
    /// Gets SummonedSpecificBarrelsDat data.
    /// </summary>
    /// <returns>readonly collection of SummonedSpecificBarrelsDat.</returns>
    public ReadOnlyCollection<SummonedSpecificBarrelsDat> GetSummonedSpecificBarrelsDat()
    {
        summonedspecificbarrelsdat ??= SummonedSpecificBarrelsDat.Load(this).AsReadOnly();

        return summonedspecificbarrelsdat;
    }

    /// <summary>
    /// Gets SummonedSpecificMonstersDat data.
    /// </summary>
    /// <returns>readonly collection of SummonedSpecificMonstersDat.</returns>
    public ReadOnlyCollection<SummonedSpecificMonstersDat> GetSummonedSpecificMonstersDat()
    {
        summonedspecificmonstersdat ??= SummonedSpecificMonstersDat.Load(this).AsReadOnly();

        return summonedspecificmonstersdat;
    }

    /// <summary>
    /// Gets SummonedSpecificMonstersOnDeathDat data.
    /// </summary>
    /// <returns>readonly collection of SummonedSpecificMonstersOnDeathDat.</returns>
    public ReadOnlyCollection<SummonedSpecificMonstersOnDeathDat> GetSummonedSpecificMonstersOnDeathDat()
    {
        summonedspecificmonstersondeathdat ??= SummonedSpecificMonstersOnDeathDat.Load(this).AsReadOnly();

        return summonedspecificmonstersondeathdat;
    }

    /// <summary>
    /// Gets SupporterPackSetsDat data.
    /// </summary>
    /// <returns>readonly collection of SupporterPackSetsDat.</returns>
    public ReadOnlyCollection<SupporterPackSetsDat> GetSupporterPackSetsDat()
    {
        supporterpacksetsdat ??= SupporterPackSetsDat.Load(this).AsReadOnly();

        return supporterpacksetsdat;
    }

    /// <summary>
    /// Gets SurgeEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of SurgeEffectsDat.</returns>
    public ReadOnlyCollection<SurgeEffectsDat> GetSurgeEffectsDat()
    {
        surgeeffectsdat ??= SurgeEffectsDat.Load(this).AsReadOnly();

        return surgeeffectsdat;
    }

    /// <summary>
    /// Gets SurgeTypesDat data.
    /// </summary>
    /// <returns>readonly collection of SurgeTypesDat.</returns>
    public ReadOnlyCollection<SurgeTypesDat> GetSurgeTypesDat()
    {
        surgetypesdat ??= SurgeTypesDat.Load(this).AsReadOnly();

        return surgetypesdat;
    }

    /// <summary>
    /// Gets TableChargeDat data.
    /// </summary>
    /// <returns>readonly collection of TableChargeDat.</returns>
    public ReadOnlyCollection<TableChargeDat> GetTableChargeDat()
    {
        tablechargedat ??= TableChargeDat.Load(this).AsReadOnly();

        return tablechargedat;
    }

    /// <summary>
    /// Gets TableMonsterSpawnersDat data.
    /// </summary>
    /// <returns>readonly collection of TableMonsterSpawnersDat.</returns>
    public ReadOnlyCollection<TableMonsterSpawnersDat> GetTableMonsterSpawnersDat()
    {
        tablemonsterspawnersdat ??= TableMonsterSpawnersDat.Load(this).AsReadOnly();

        return tablemonsterspawnersdat;
    }

    /// <summary>
    /// Gets TagsDat data.
    /// </summary>
    /// <returns>readonly collection of TagsDat.</returns>
    public ReadOnlyCollection<TagsDat> GetTagsDat()
    {
        tagsdat ??= TagsDat.Load(this).AsReadOnly();

        return tagsdat;
    }

    /// <summary>
    /// Gets TalkingPetAudioEventsDat data.
    /// </summary>
    /// <returns>readonly collection of TalkingPetAudioEventsDat.</returns>
    public ReadOnlyCollection<TalkingPetAudioEventsDat> GetTalkingPetAudioEventsDat()
    {
        talkingpetaudioeventsdat ??= TalkingPetAudioEventsDat.Load(this).AsReadOnly();

        return talkingpetaudioeventsdat;
    }

    /// <summary>
    /// Gets TalkingPetNPCAudioDat data.
    /// </summary>
    /// <returns>readonly collection of TalkingPetNPCAudioDat.</returns>
    public ReadOnlyCollection<TalkingPetNPCAudioDat> GetTalkingPetNPCAudioDat()
    {
        talkingpetnpcaudiodat ??= TalkingPetNPCAudioDat.Load(this).AsReadOnly();

        return talkingpetnpcaudiodat;
    }

    /// <summary>
    /// Gets TalkingPetsDat data.
    /// </summary>
    /// <returns>readonly collection of TalkingPetsDat.</returns>
    public ReadOnlyCollection<TalkingPetsDat> GetTalkingPetsDat()
    {
        talkingpetsdat ??= TalkingPetsDat.Load(this).AsReadOnly();

        return talkingpetsdat;
    }

    /// <summary>
    /// Gets TencentAutoLootPetCurrenciesDat data.
    /// </summary>
    /// <returns>readonly collection of TencentAutoLootPetCurrenciesDat.</returns>
    public ReadOnlyCollection<TencentAutoLootPetCurrenciesDat> GetTencentAutoLootPetCurrenciesDat()
    {
        tencentautolootpetcurrenciesdat ??= TencentAutoLootPetCurrenciesDat.Load(this).AsReadOnly();

        return tencentautolootpetcurrenciesdat;
    }

    /// <summary>
    /// Gets TencentAutoLootPetCurrenciesExcludableDat data.
    /// </summary>
    /// <returns>readonly collection of TencentAutoLootPetCurrenciesExcludableDat.</returns>
    public ReadOnlyCollection<TencentAutoLootPetCurrenciesExcludableDat> GetTencentAutoLootPetCurrenciesExcludableDat()
    {
        tencentautolootpetcurrenciesexcludabledat ??= TencentAutoLootPetCurrenciesExcludableDat.Load(this).AsReadOnly();

        return tencentautolootpetcurrenciesexcludabledat;
    }

    /// <summary>
    /// Gets TerrainPluginsDat data.
    /// </summary>
    /// <returns>readonly collection of TerrainPluginsDat.</returns>
    public ReadOnlyCollection<TerrainPluginsDat> GetTerrainPluginsDat()
    {
        terrainpluginsdat ??= TerrainPluginsDat.Load(this).AsReadOnly();

        return terrainpluginsdat;
    }

    /// <summary>
    /// Gets TipsDat data.
    /// </summary>
    /// <returns>readonly collection of TipsDat.</returns>
    public ReadOnlyCollection<TipsDat> GetTipsDat()
    {
        tipsdat ??= TipsDat.Load(this).AsReadOnly();

        return tipsdat;
    }

    /// <summary>
    /// Gets TopologiesDat data.
    /// </summary>
    /// <returns>readonly collection of TopologiesDat.</returns>
    public ReadOnlyCollection<TopologiesDat> GetTopologiesDat()
    {
        topologiesdat ??= TopologiesDat.Load(this).AsReadOnly();

        return topologiesdat;
    }

    /// <summary>
    /// Gets TradeMarketCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of TradeMarketCategoryDat.</returns>
    public ReadOnlyCollection<TradeMarketCategoryDat> GetTradeMarketCategoryDat()
    {
        trademarketcategorydat ??= TradeMarketCategoryDat.Load(this).AsReadOnly();

        return trademarketcategorydat;
    }

    /// <summary>
    /// Gets TradeMarketCategoryGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of TradeMarketCategoryGroupsDat.</returns>
    public ReadOnlyCollection<TradeMarketCategoryGroupsDat> GetTradeMarketCategoryGroupsDat()
    {
        trademarketcategorygroupsdat ??= TradeMarketCategoryGroupsDat.Load(this).AsReadOnly();

        return trademarketcategorygroupsdat;
    }

    /// <summary>
    /// Gets TradeMarketCategoryListAllClassDat data.
    /// </summary>
    /// <returns>readonly collection of TradeMarketCategoryListAllClassDat.</returns>
    public ReadOnlyCollection<TradeMarketCategoryListAllClassDat> GetTradeMarketCategoryListAllClassDat()
    {
        trademarketcategorylistallclassdat ??= TradeMarketCategoryListAllClassDat.Load(this).AsReadOnly();

        return trademarketcategorylistallclassdat;
    }

    /// <summary>
    /// Gets TradeMarketIndexItemAsDat data.
    /// </summary>
    /// <returns>readonly collection of TradeMarketIndexItemAsDat.</returns>
    public ReadOnlyCollection<TradeMarketIndexItemAsDat> GetTradeMarketIndexItemAsDat()
    {
        trademarketindexitemasdat ??= TradeMarketIndexItemAsDat.Load(this).AsReadOnly();

        return trademarketindexitemasdat;
    }

    /// <summary>
    /// Gets TreasureHunterMissionsDat data.
    /// </summary>
    /// <returns>readonly collection of TreasureHunterMissionsDat.</returns>
    public ReadOnlyCollection<TreasureHunterMissionsDat> GetTreasureHunterMissionsDat()
    {
        treasurehuntermissionsdat ??= TreasureHunterMissionsDat.Load(this).AsReadOnly();

        return treasurehuntermissionsdat;
    }

    /// <summary>
    /// Gets TriggerBeamDat data.
    /// </summary>
    /// <returns>readonly collection of TriggerBeamDat.</returns>
    public ReadOnlyCollection<TriggerBeamDat> GetTriggerBeamDat()
    {
        triggerbeamdat ??= TriggerBeamDat.Load(this).AsReadOnly();

        return triggerbeamdat;
    }

    /// <summary>
    /// Gets TriggerSpawnersDat data.
    /// </summary>
    /// <returns>readonly collection of TriggerSpawnersDat.</returns>
    public ReadOnlyCollection<TriggerSpawnersDat> GetTriggerSpawnersDat()
    {
        triggerspawnersdat ??= TriggerSpawnersDat.Load(this).AsReadOnly();

        return triggerspawnersdat;
    }

    /// <summary>
    /// Gets TutorialDat data.
    /// </summary>
    /// <returns>readonly collection of TutorialDat.</returns>
    public ReadOnlyCollection<TutorialDat> GetTutorialDat()
    {
        tutorialdat ??= TutorialDat.Load(this).AsReadOnly();

        return tutorialdat;
    }

    /// <summary>
    /// Gets UITalkTextDat data.
    /// </summary>
    /// <returns>readonly collection of UITalkTextDat.</returns>
    public ReadOnlyCollection<UITalkTextDat> GetUITalkTextDat()
    {
        uitalktextdat ??= UITalkTextDat.Load(this).AsReadOnly();

        return uitalktextdat;
    }

    /// <summary>
    /// Gets UniqueChestsDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueChestsDat.</returns>
    public ReadOnlyCollection<UniqueChestsDat> GetUniqueChestsDat()
    {
        uniquechestsdat ??= UniqueChestsDat.Load(this).AsReadOnly();

        return uniquechestsdat;
    }

    /// <summary>
    /// Gets UniqueJewelLimitsDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueJewelLimitsDat.</returns>
    public ReadOnlyCollection<UniqueJewelLimitsDat> GetUniqueJewelLimitsDat()
    {
        uniquejewellimitsdat ??= UniqueJewelLimitsDat.Load(this).AsReadOnly();

        return uniquejewellimitsdat;
    }

    /// <summary>
    /// Gets UniqueMapInfoDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueMapInfoDat.</returns>
    public ReadOnlyCollection<UniqueMapInfoDat> GetUniqueMapInfoDat()
    {
        uniquemapinfodat ??= UniqueMapInfoDat.Load(this).AsReadOnly();

        return uniquemapinfodat;
    }

    /// <summary>
    /// Gets UniqueMapsDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueMapsDat.</returns>
    public ReadOnlyCollection<UniqueMapsDat> GetUniqueMapsDat()
    {
        uniquemapsdat ??= UniqueMapsDat.Load(this).AsReadOnly();

        return uniquemapsdat;
    }

    /// <summary>
    /// Gets UniqueStashLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueStashLayoutDat.</returns>
    public ReadOnlyCollection<UniqueStashLayoutDat> GetUniqueStashLayoutDat()
    {
        uniquestashlayoutdat ??= UniqueStashLayoutDat.Load(this).AsReadOnly();

        return uniquestashlayoutdat;
    }

    /// <summary>
    /// Gets UniqueStashTypesDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueStashTypesDat.</returns>
    public ReadOnlyCollection<UniqueStashTypesDat> GetUniqueStashTypesDat()
    {
        uniquestashtypesdat ??= UniqueStashTypesDat.Load(this).AsReadOnly();

        return uniquestashtypesdat;
    }

    /// <summary>
    /// Gets VirtualStatContextFlagsDat data.
    /// </summary>
    /// <returns>readonly collection of VirtualStatContextFlagsDat.</returns>
    public ReadOnlyCollection<VirtualStatContextFlagsDat> GetVirtualStatContextFlagsDat()
    {
        virtualstatcontextflagsdat ??= VirtualStatContextFlagsDat.Load(this).AsReadOnly();

        return virtualstatcontextflagsdat;
    }

    /// <summary>
    /// Gets VoteStateDat data.
    /// </summary>
    /// <returns>readonly collection of VoteStateDat.</returns>
    public ReadOnlyCollection<VoteStateDat> GetVoteStateDat()
    {
        votestatedat ??= VoteStateDat.Load(this).AsReadOnly();

        return votestatedat;
    }

    /// <summary>
    /// Gets VoteTypeDat data.
    /// </summary>
    /// <returns>readonly collection of VoteTypeDat.</returns>
    public ReadOnlyCollection<VoteTypeDat> GetVoteTypeDat()
    {
        votetypedat ??= VoteTypeDat.Load(this).AsReadOnly();

        return votetypedat;
    }

    /// <summary>
    /// Gets WeaponClassesDat data.
    /// </summary>
    /// <returns>readonly collection of WeaponClassesDat.</returns>
    public ReadOnlyCollection<WeaponClassesDat> GetWeaponClassesDat()
    {
        weaponclassesdat ??= WeaponClassesDat.Load(this).AsReadOnly();

        return weaponclassesdat;
    }

    /// <summary>
    /// Gets WeaponImpactSoundDataDat data.
    /// </summary>
    /// <returns>readonly collection of WeaponImpactSoundDataDat.</returns>
    public ReadOnlyCollection<WeaponImpactSoundDataDat> GetWeaponImpactSoundDataDat()
    {
        weaponimpactsounddatadat ??= WeaponImpactSoundDataDat.Load(this).AsReadOnly();

        return weaponimpactsounddatadat;
    }

    /// <summary>
    /// Gets WeaponTypesDat data.
    /// </summary>
    /// <returns>readonly collection of WeaponTypesDat.</returns>
    public ReadOnlyCollection<WeaponTypesDat> GetWeaponTypesDat()
    {
        weapontypesdat ??= WeaponTypesDat.Load(this).AsReadOnly();

        return weapontypesdat;
    }

    /// <summary>
    /// Gets WindowCursorsDat data.
    /// </summary>
    /// <returns>readonly collection of WindowCursorsDat.</returns>
    public ReadOnlyCollection<WindowCursorsDat> GetWindowCursorsDat()
    {
        windowcursorsdat ??= WindowCursorsDat.Load(this).AsReadOnly();

        return windowcursorsdat;
    }

    /// <summary>
    /// Gets WordsDat data.
    /// </summary>
    /// <returns>readonly collection of WordsDat.</returns>
    public ReadOnlyCollection<WordsDat> GetWordsDat()
    {
        wordsdat ??= WordsDat.Load(this).AsReadOnly();

        return wordsdat;
    }

    /// <summary>
    /// Gets WorldAreasDat data.
    /// </summary>
    /// <returns>readonly collection of WorldAreasDat.</returns>
    public ReadOnlyCollection<WorldAreasDat> GetWorldAreasDat()
    {
        worldareasdat ??= WorldAreasDat.Load(this).AsReadOnly();

        return worldareasdat;
    }

    /// <summary>
    /// Gets WorldAreaLeagueChancesDat data.
    /// </summary>
    /// <returns>readonly collection of WorldAreaLeagueChancesDat.</returns>
    public ReadOnlyCollection<WorldAreaLeagueChancesDat> GetWorldAreaLeagueChancesDat()
    {
        worldarealeaguechancesdat ??= WorldAreaLeagueChancesDat.Load(this).AsReadOnly();

        return worldarealeaguechancesdat;
    }

    /// <summary>
    /// Gets WorldPopupIconTypesDat data.
    /// </summary>
    /// <returns>readonly collection of WorldPopupIconTypesDat.</returns>
    public ReadOnlyCollection<WorldPopupIconTypesDat> GetWorldPopupIconTypesDat()
    {
        worldpopupicontypesdat ??= WorldPopupIconTypesDat.Load(this).AsReadOnly();

        return worldpopupicontypesdat;
    }

    /// <summary>
    /// Gets ZanaLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of ZanaLevelsDat.</returns>
    public ReadOnlyCollection<ZanaLevelsDat> GetZanaLevelsDat()
    {
        zanalevelsdat ??= ZanaLevelsDat.Load(this).AsReadOnly();

        return zanalevelsdat;
    }
}
