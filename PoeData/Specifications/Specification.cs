// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Specifications.Repositories;
using Serilog;

namespace PoeData.Specifications;

/// <summary>
/// Class containing all Path of Exile data.
/// </summary>
public sealed partial class Specification
{
    /// <summary>Gets dat file magic number.</summary>
    /// thats where the table ends?
    internal static byte[] DatFileMagicNumber { get; } = new byte[] { (byte)'\xBB', (byte)'\xbb', (byte)'\xBB', (byte)'\xbb', (byte)'\xBB', (byte)'\xbb', (byte)'\xBB', (byte)'\xbb' };

    private readonly DataLoader dataLoader;
    private readonly ILogger logger;

    /// <summary>Gets data loader.</summary>
    internal DataLoader DataLoader => dataLoader;

    private RogueExilesRepository? rogueexilesrepository;
    private RogueExileLifeScalingPerLevelRepository? rogueexilelifescalingperlevelrepository;
    private ShrineBuffsRepository? shrinebuffsrepository;
    private ShrinesRepository? shrinesrepository;
    private ShrineSoundsRepository? shrinesoundsrepository;
    private StrongboxesRepository? strongboxesrepository;
    private InvasionMonsterRestrictionsRepository? invasionmonsterrestrictionsrepository;
    private InvasionMonstersPerAreaRepository? invasionmonstersperarearepository;
    private BeyondDemonsRepository? beyonddemonsrepository;
    private BeyondFactionsRepository? beyondfactionsrepository;
    private BloodlinesRepository? bloodlinesrepository;
    private TormentSpiritsRepository? tormentspiritsrepository;
    private DivinationCardArtRepository? divinationcardartrepository;
    private WarbandsGraphRepository? warbandsgraphrepository;
    private WarbandsMapGraphRepository? warbandsmapgraphrepository;
    private WarbandsPackMonstersRepository? warbandspackmonstersrepository;
    private WarbandsPackNumbersRepository? warbandspacknumbersrepository;
    private TalismanMonsterModsRepository? talismanmonstermodsrepository;
    private TalismanPacksRepository? talismanpacksrepository;
    private TalismansRepository? talismansrepository;
    private LabyrinthAreasRepository? labyrinthareasrepository;
    private LabyrinthBonusItemsRepository? labyrinthbonusitemsrepository;
    private LabyrinthExclusionGroupsRepository? labyrinthexclusiongroupsrepository;
    private LabyrinthIzaroChestsRepository? labyrinthizarochestsrepository;
    private LabyrinthNodeOverridesRepository? labyrinthnodeoverridesrepository;
    private LabyrinthRewardTypesRepository? labyrinthrewardtypesrepository;
    private LabyrinthsRepository? labyrinthsrepository;
    private LabyrinthSecretEffectsRepository? labyrinthsecreteffectsrepository;
    private LabyrinthSecretsRepository? labyrinthsecretsrepository;
    private LabyrinthSectionRepository? labyrinthsectionrepository;
    private LabyrinthSectionLayoutRepository? labyrinthsectionlayoutrepository;
    private LabyrinthTrialsRepository? labyrinthtrialsrepository;
    private LabyrinthTrinketsRepository? labyrinthtrinketsrepository;
    private PerandusBossesRepository? perandusbossesrepository;
    private PerandusChestsRepository? peranduschestsrepository;
    private PerandusDaemonsRepository? perandusdaemonsrepository;
    private PerandusGuardsRepository? perandusguardsrepository;
    private PropheciesRepository? propheciesrepository;
    private ProphecyChainRepository? prophecychainrepository;
    private ProphecyTypeRepository? prophecytyperepository;
    private ShaperGuardiansRepository? shaperguardiansrepository;
    private EssencesRepository? essencesrepository;
    private EssenceTypeRepository? essencetyperepository;
    private BreachBossLifeScalingPerLevelRepository? breachbosslifescalingperlevelrepository;
    private BreachElementRepository? breachelementrepository;
    private BreachstoneUpgradesRepository? breachstoneupgradesrepository;
    private HarbingersRepository? harbingersrepository;
    private PantheonPanelLayoutRepository? pantheonpanellayoutrepository;
    private PantheonSoulsRepository? pantheonsoulsrepository;
    private AbyssObjectsRepository? abyssobjectsrepository;
    private ElderBossArenasRepository? elderbossarenasrepository;
    private ElderMapBossOverrideRepository? eldermapbossoverriderepository;
    private ElderGuardiansRepository? elderguardiansrepository;
    private BestiaryCapturableMonstersRepository? bestiarycapturablemonstersrepository;
    private BestiaryEncountersRepository? bestiaryencountersrepository;
    private BestiaryFamiliesRepository? bestiaryfamiliesrepository;
    private BestiaryGenusRepository? bestiarygenusrepository;
    private BestiaryGroupsRepository? bestiarygroupsrepository;
    private BestiaryNetsRepository? bestiarynetsrepository;
    private BestiaryRecipeComponentRepository? bestiaryrecipecomponentrepository;
    private BestiaryRecipeCategoriesRepository? bestiaryrecipecategoriesrepository;
    private BestiaryRecipesRepository? bestiaryrecipesrepository;
    private ArchitectLifeScalingPerLevelRepository? architectlifescalingperlevelrepository;
    private IncursionArchitectRepository? incursionarchitectrepository;
    private IncursionBracketsRepository? incursionbracketsrepository;
    private IncursionChestRewardsRepository? incursionchestrewardsrepository;
    private IncursionChestsRepository? incursionchestsrepository;
    private IncursionRoomBossFightEventsRepository? incursionroombossfighteventsrepository;
    private IncursionRoomsRepository? incursionroomsrepository;
    private IncursionUniqueUpgradeComponentsRepository? incursionuniqueupgradecomponentsrepository;
    private DelveAzuriteShopRepository? delveazuriteshoprepository;
    private DelveBiomesRepository? delvebiomesrepository;
    private DelveCatchupDepthsRepository? delvecatchupdepthsrepository;
    private DelveCraftingModifierDescriptionsRepository? delvecraftingmodifierdescriptionsrepository;
    private DelveCraftingModifiersRepository? delvecraftingmodifiersrepository;
    private DelveCraftingTagsRepository? delvecraftingtagsrepository;
    private DelveDynamiteRepository? delvedynamiterepository;
    private DelveFeaturesRepository? delvefeaturesrepository;
    private DelveFlaresRepository? delveflaresrepository;
    private DelveLevelScalingRepository? delvelevelscalingrepository;
    private DelveMonsterSpawnersRepository? delvemonsterspawnersrepository;
    private DelveResourcePerLevelRepository? delveresourceperlevelrepository;
    private DelveRewardTierConstantsRepository? delverewardtierconstantsrepository;
    private DelveRoomsRepository? delveroomsrepository;
    private DelveUpgradesRepository? delveupgradesrepository;
    private BetrayalChoiceActionsRepository? betrayalchoiceactionsrepository;
    private BetrayalChoicesRepository? betrayalchoicesrepository;
    private BetrayalDialogueRepository? betrayaldialoguerepository;
    private BetrayalFortsRepository? betrayalfortsrepository;
    private BetrayalJobsRepository? betrayaljobsrepository;
    private BetrayalRanksRepository? betrayalranksrepository;
    private BetrayalRelationshipStateRepository? betrayalrelationshipstaterepository;
    private BetrayalTargetJobAchievementsRepository? betrayaltargetjobachievementsrepository;
    private BetrayalTargetLifeScalingPerLevelRepository? betrayaltargetlifescalingperlevelrepository;
    private BetrayalTargetsRepository? betrayaltargetsrepository;
    private BetrayalTraitorRewardsRepository? betrayaltraitorrewardsrepository;
    private BetrayalUpgradesRepository? betrayalupgradesrepository;
    private BetrayalWallLifeScalingPerLevelRepository? betrayalwalllifescalingperlevelrepository;
    private SafehouseBYOCraftingRepository? safehousebyocraftingrepository;
    private SafehouseCraftingSpreeTypeRepository? safehousecraftingspreetyperepository;
    private SafehouseCraftingSpreeCurrenciesRepository? safehousecraftingspreecurrenciesrepository;
    private ScarabsRepository? scarabsrepository;
    private SynthesisAreasRepository? synthesisareasrepository;
    private SynthesisAreaSizeRepository? synthesisareasizerepository;
    private SynthesisBonusesRepository? synthesisbonusesrepository;
    private SynthesisBracketsRepository? synthesisbracketsrepository;
    private SynthesisFragmentDialogueRepository? synthesisfragmentdialoguerepository;
    private SynthesisGlobalModsRepository? synthesisglobalmodsrepository;
    private SynthesisMonsterExperiencePerLevelRepository? synthesismonsterexperienceperlevelrepository;
    private SynthesisRewardCategoriesRepository? synthesisrewardcategoriesrepository;
    private SynthesisRewardTypesRepository? synthesisrewardtypesrepository;
    private IncubatorsRepository? incubatorsrepository;
    private LegionBalancePerLevelRepository? legionbalanceperlevelrepository;
    private LegionChestTypesRepository? legionchesttypesrepository;
    private LegionChestCountsRepository? legionchestcountsrepository;
    private LegionChestsRepository? legionchestsrepository;
    private LegionFactionsRepository? legionfactionsrepository;
    private LegionMonsterCountsRepository? legionmonstercountsrepository;
    private LegionMonsterVarietiesRepository? legionmonstervarietiesrepository;
    private LegionRanksRepository? legionranksrepository;
    private LegionRewardTypeVisualsRepository? legionrewardtypevisualsrepository;
    private BlightBalancePerLevelRepository? blightbalanceperlevelrepository;
    private BlightBossLifeScalingPerLevelRepository? blightbosslifescalingperlevelrepository;
    private BlightChestTypesRepository? blightchesttypesrepository;
    private BlightCraftingItemsRepository? blightcraftingitemsrepository;
    private BlightCraftingRecipesRepository? blightcraftingrecipesrepository;
    private BlightCraftingResultsRepository? blightcraftingresultsrepository;
    private BlightCraftingTypesRepository? blightcraftingtypesrepository;
    private BlightCraftingUniquesRepository? blightcraftinguniquesrepository;
    private BlightedSporeAurasRepository? blightedsporeaurasrepository;
    private BlightEncounterTypesRepository? blightencountertypesrepository;
    private BlightEncounterWavesRepository? blightencounterwavesrepository;
    private BlightRewardTypesRepository? blightrewardtypesrepository;
    private BlightTopologiesRepository? blighttopologiesrepository;
    private BlightTopologyNodesRepository? blighttopologynodesrepository;
    private BlightTowerAurasRepository? blighttoweraurasrepository;
    private BlightTowersRepository? blighttowersrepository;
    private BlightTowersPerLevelRepository? blighttowersperlevelrepository;
    private AtlasExileBossArenasRepository? atlasexilebossarenasrepository;
    private AtlasExileInfluenceRepository? atlasexileinfluencerepository;
    private AtlasExilesRepository? atlasexilesrepository;
    private AlternateQualityCurrencyDecayFactorsRepository? alternatequalitycurrencydecayfactorsrepository;
    private AlternateQualityTypesRepository? alternatequalitytypesrepository;
    private MetamorphLifeScalingPerLevelRepository? metamorphlifescalingperlevelrepository;
    private MetamorphosisMetaMonstersRepository? metamorphosismetamonstersrepository;
    private MetamorphosisMetaSkillsRepository? metamorphosismetaskillsrepository;
    private MetamorphosisMetaSkillTypesRepository? metamorphosismetaskilltypesrepository;
    private MetamorphosisRewardTypeItemsClientRepository? metamorphosisrewardtypeitemsclientrepository;
    private MetamorphosisRewardTypesRepository? metamorphosisrewardtypesrepository;
    private MetamorphosisScalingRepository? metamorphosisscalingrepository;
    private AfflictionBalancePerLevelRepository? afflictionbalanceperlevelrepository;
    private AfflictionEndgameWaveModsRepository? afflictionendgamewavemodsrepository;
    private AfflictionFixedModsRepository? afflictionfixedmodsrepository;
    private AfflictionRandomModCategoriesRepository? afflictionrandommodcategoriesrepository;
    private AfflictionRewardMapModsRepository? afflictionrewardmapmodsrepository;
    private AfflictionRewardTypeVisualsRepository? afflictionrewardtypevisualsrepository;
    private AfflictionSplitDemonsRepository? afflictionsplitdemonsrepository;
    private AfflictionStartDialogueRepository? afflictionstartdialoguerepository;
    private HarvestCraftOptionIconsRepository? harvestcraftoptioniconsrepository;
    private HarvestCraftOptionsRepository? harvestcraftoptionsrepository;
    private HarvestCraftTiersRepository? harvestcrafttiersrepository;
    private HarvestCraftFiltersRepository? harvestcraftfiltersrepository;
    private HarvestDurabilityRepository? harvestdurabilityrepository;
    private HarvestEncounterScalingRepository? harvestencounterscalingrepository;
    private HarvestInfrastructureRepository? harvestinfrastructurerepository;
    private HarvestObjectsRepository? harvestobjectsrepository;
    private HarvestPerLevelValuesRepository? harvestperlevelvaluesrepository;
    private HarvestPlantBoostersRepository? harvestplantboostersrepository;
    private HarvestLifeScalingPerLevelRepository? harvestlifescalingperlevelrepository;
    private HarvestSeedsRepository? harvestseedsrepository;
    private HarvestSeedItemsRepository? harvestseeditemsrepository;
    private HarvestSeedTypesRepository? harvestseedtypesrepository;
    private HarvestSpecialCraftCostsRepository? harvestspecialcraftcostsrepository;
    private HarvestSpecialCraftOptionsRepository? harvestspecialcraftoptionsrepository;
    private HeistAreaFormationLayoutRepository? heistareaformationlayoutrepository;
    private HeistAreasRepository? heistareasrepository;
    private HeistBalancePerLevelRepository? heistbalanceperlevelrepository;
    private HeistChestRewardTypesRepository? heistchestrewardtypesrepository;
    private HeistChestsRepository? heistchestsrepository;
    private HeistChokepointFormationRepository? heistchokepointformationrepository;
    private HeistConstantsRepository? heistconstantsrepository;
    private HeistContractsRepository? heistcontractsrepository;
    private HeistDoodadNPCsRepository? heistdoodadnpcsrepository;
    private HeistDoorsRepository? heistdoorsrepository;
    private HeistEquipmentRepository? heistequipmentrepository;
    private HeistGenerationRepository? heistgenerationrepository;
    private HeistIntroAreasRepository? heistintroareasrepository;
    private HeistJobsRepository? heistjobsrepository;
    private HeistJobsExperiencePerLevelRepository? heistjobsexperienceperlevelrepository;
    private HeistLockTypeRepository? heistlocktyperepository;
    private HeistNPCAurasRepository? heistnpcaurasrepository;
    private HeistNPCBlueprintTypesRepository? heistnpcblueprinttypesrepository;
    private HeistNPCDialogueRepository? heistnpcdialoguerepository;
    private HeistNPCsRepository? heistnpcsrepository;
    private HeistNPCStatsRepository? heistnpcstatsrepository;
    private HeistObjectivesRepository? heistobjectivesrepository;
    private HeistObjectiveValueDescriptionsRepository? heistobjectivevaluedescriptionsrepository;
    private HeistPatrolPacksRepository? heistpatrolpacksrepository;
    private HeistQuestContractsRepository? heistquestcontractsrepository;
    private HeistRevealingNPCsRepository? heistrevealingnpcsrepository;
    private HeistRoomsRepository? heistroomsrepository;
    private HeistValueScalingRepository? heistvaluescalingrepository;
    private InfluenceModUpgradesRepository? influencemodupgradesrepository;
    private MavenDialogRepository? mavendialogrepository;
    private AtlasSkillGraphsRepository? atlasskillgraphsrepository;
    private MavenFightsRepository? mavenfightsrepository;
    private MavenJewelRadiusKeystonesRepository? mavenjewelradiuskeystonesrepository;
    private RitualBalancePerLevelRepository? ritualbalanceperlevelrepository;
    private RitualConstantsRepository? ritualconstantsrepository;
    private RitualRuneTypesRepository? ritualrunetypesrepository;
    private RitualSetKillAchievementsRepository? ritualsetkillachievementsrepository;
    private RitualSpawnPatternsRepository? ritualspawnpatternsrepository;
    private UltimatumEncountersRepository? ultimatumencountersrepository;
    private UltimatumEncounterTypesRepository? ultimatumencountertypesrepository;
    private UltimatumItemisedRewardsRepository? ultimatumitemisedrewardsrepository;
    private UltimatumMapModifiersRepository? ultimatummapmodifiersrepository;
    private UltimatumModifiersRepository? ultimatummodifiersrepository;
    private UltimatumModifierTypesRepository? ultimatummodifiertypesrepository;
    private UltimatumTrialMasterAudioRepository? ultimatumtrialmasteraudiorepository;
    private ExpeditionAreasRepository? expeditionareasrepository;
    private ExpeditionBalancePerLevelRepository? expeditionbalanceperlevelrepository;
    private ExpeditionCurrencyRepository? expeditioncurrencyrepository;
    private ExpeditionDealsRepository? expeditiondealsrepository;
    private ExpeditionFactionsRepository? expeditionfactionsrepository;
    private ExpeditionMarkersCommonRepository? expeditionmarkerscommonrepository;
    private ExpeditionNPCsRepository? expeditionnpcsrepository;
    private ExpeditionRelicModsRepository? expeditionrelicmodsrepository;
    private ExpeditionRelicsRepository? expeditionrelicsrepository;
    private ExpeditionStorageLayoutRepository? expeditionstoragelayoutrepository;
    private ExpeditionTerrainFeaturesRepository? expeditionterrainfeaturesrepository;
    private HellscapeAOReplacementsRepository? hellscapeaoreplacementsrepository;
    private HellscapeAreaPacksRepository? hellscapeareapacksrepository;
    private HellscapeExperienceLevelsRepository? hellscapeexperiencelevelsrepository;
    private HellscapeFactionsRepository? hellscapefactionsrepository;
    private HellscapeImmuneMonstersRepository? hellscapeimmunemonstersrepository;
    private HellscapeItemModificationTiersRepository? hellscapeitemmodificationtiersrepository;
    private HellscapeLifeScalingPerLevelRepository? hellscapelifescalingperlevelrepository;
    private HellscapeModificationInventoryLayoutRepository? hellscapemodificationinventorylayoutrepository;
    private HellscapeModsRepository? hellscapemodsrepository;
    private HellscapeMonsterPacksRepository? hellscapemonsterpacksrepository;
    private HellscapePassivesRepository? hellscapepassivesrepository;
    private HellscapePassiveTreeRepository? hellscapepassivetreerepository;
    private ArchnemesisMetaRewardsRepository? archnemesismetarewardsrepository;
    private ArchnemesisModComboAchievementsRepository? archnemesismodcomboachievementsrepository;
    private ArchnemesisModsRepository? archnemesismodsrepository;
    private ArchnemesisModVisualsRepository? archnemesismodvisualsrepository;
    private ArchnemesisRecipesRepository? archnemesisrecipesrepository;
    private AtlasPrimordialAltarChoicesRepository? atlasprimordialaltarchoicesrepository;
    private AtlasPrimordialAltarChoiceTypesRepository? atlasprimordialaltarchoicetypesrepository;
    private AtlasPrimordialBossesRepository? atlasprimordialbossesrepository;
    private AtlasPrimordialBossInfluenceRepository? atlasprimordialbossinfluencerepository;
    private AtlasPrimordialBossOptionsRepository? atlasprimordialbossoptionsrepository;
    private PrimordialBossLifeScalingPerLevelRepository? primordialbosslifescalingperlevelrepository;
    private AtlasUpgradesInventoryLayoutRepository? atlasupgradesinventorylayoutrepository;
    private AtlasPassiveSkillTreeGroupTypeRepository? atlaspassiveskilltreegrouptyperepository;
    private KiracLevelsRepository? kiraclevelsrepository;
    private ScoutingReportsRepository? scoutingreportsrepository;
    private DroneBaseTypesRepository? dronebasetypesrepository;
    private DroneTypesRepository? dronetypesrepository;
    private SentinelCraftingCurrencyRepository? sentinelcraftingcurrencyrepository;
    private SentinelDroneInventoryLayoutRepository? sentineldroneinventorylayoutrepository;
    private SentinelPassivesRepository? sentinelpassivesrepository;
    private SentinelPassiveStatsRepository? sentinelpassivestatsrepository;
    private SentinelPassiveTypesRepository? sentinelpassivetypesrepository;
    private SentinelPowerExpLevelsRepository? sentinelpowerexplevelsrepository;
    private SentinelStorageLayoutRepository? sentinelstoragelayoutrepository;
    private SentinelTaggedMonsterStatsRepository? sentineltaggedmonsterstatsrepository;
    private ClientLakeDifficultyRepository? clientlakedifficultyrepository;
    private LakeBossLifeScalingPerLevelRepository? lakebosslifescalingperlevelrepository;
    private LakeMetaOptionsRepository? lakemetaoptionsrepository;
    private LakeMetaOptionsUnlockTextRepository? lakemetaoptionsunlocktextrepository;
    private LakeRoomCompletionRepository? lakeroomcompletionrepository;
    private LakeRoomsRepository? lakeroomsrepository;
    private WeaponPassiveSkillTypesRepository? weaponpassiveskilltypesrepository;
    private WeaponPassiveTreeBalancePerItemLevelRepository? weaponpassivetreebalanceperitemlevelrepository;
    private WeaponPassiveTreeUniqueBaseTypesRepository? weaponpassivetreeuniquebasetypesrepository;
    private WeaponPassiveSkillsRepository? weaponpassiveskillsrepository;
    private AchievementItemRewardsRepository? achievementitemrewardsrepository;
    private AchievementItemsRepository? achievementitemsrepository;
    private AchievementsRepository? achievementsrepository;
    private AchievementSetRewardsRepository? achievementsetrewardsrepository;
    private AchievementSetsDisplayRepository? achievementsetsdisplayrepository;
    private ActiveSkillsRepository? activeskillsrepository;
    private ActiveSkillTypeRepository? activeskilltyperepository;
    private ActsRepository? actsrepository;
    private AddBuffToTargetVarietiesRepository? addbufftotargetvarietiesrepository;
    private AdditionalLifeScalingRepository? additionallifescalingrepository;
    private AdditionalMonsterPacksFromStatsRepository? additionalmonsterpacksfromstatsrepository;
    private AdvancedSkillsTutorialRepository? advancedskillstutorialrepository;
    private AegisVariationsRepository? aegisvariationsrepository;
    private AlternatePassiveAdditionsRepository? alternatepassiveadditionsrepository;
    private AlternatePassiveSkillsRepository? alternatepassiveskillsrepository;
    private AlternateSkillTargetingBehavioursRepository? alternateskilltargetingbehavioursrepository;
    private AlternateTreeVersionsRepository? alternatetreeversionsrepository;
    private AnimatedObjectFlagsRepository? animatedobjectflagsrepository;
    private AnimationRepository? animationrepository;
    private ApplyDamageFunctionsRepository? applydamagefunctionsrepository;
    private ArchetypeRewardsRepository? archetyperewardsrepository;
    private ArchetypesRepository? archetypesrepository;
    private AreaInfluenceDoodadsRepository? areainfluencedoodadsrepository;
    private AreaTransitionAnimationsRepository? areatransitionanimationsrepository;
    private AreaTransitionAnimationTypesRepository? areatransitionanimationtypesrepository;
    private AreaTransitionInfoRepository? areatransitioninforepository;
    private ArmourTypesRepository? armourtypesrepository;
    private AscendancyRepository? ascendancyrepository;
    private AtlasAwakeningStatsRepository? atlasawakeningstatsrepository;
    private AtlasBaseTypeDropsRepository? atlasbasetypedropsrepository;
    private AtlasFogRepository? atlasfogrepository;
    private AtlasInfluenceDataRepository? atlasinfluencedatarepository;
    private AtlasInfluenceOutcomesRepository? atlasinfluenceoutcomesrepository;
    private AtlasInfluenceSetsRepository? atlasinfluencesetsrepository;
    private AtlasModsRepository? atlasmodsrepository;
    private AtlasFavouredMapSlotsRepository? atlasfavouredmapslotsrepository;
    private AtlasNodeRepository? atlasnoderepository;
    private AtlasNodeDefinitionRepository? atlasnodedefinitionrepository;
    private AtlasPositionsRepository? atlaspositionsrepository;
    private AtlasRegionsRepository? atlasregionsrepository;
    private AtlasRegionUpgradesInventoryLayoutRepository? atlasregionupgradesinventorylayoutrepository;
    private AtlasRegionUpgradeRegionsRepository? atlasregionupgraderegionsrepository;
    private AtlasSectorRepository? atlassectorrepository;
    private AwardDisplayRepository? awarddisplayrepository;
    private BackendErrorsRepository? backenderrorsrepository;
    private BaseItemTypesRepository? baseitemtypesrepository;
    private BindableVirtualKeysRepository? bindablevirtualkeysrepository;
    private BlightStashTabLayoutRepository? blightstashtablayoutrepository;
    private BloodTypesRepository? bloodtypesrepository;
    private BuffDefinitionsRepository? buffdefinitionsrepository;
    private BuffTemplatesRepository? bufftemplatesrepository;
    private BuffVisualOrbArtRepository? buffvisualorbartrepository;
    private BuffVisualOrbsRepository? buffvisualorbsrepository;
    private BuffVisualOrbTypesRepository? buffvisualorbtypesrepository;
    private BuffVisualsRepository? buffvisualsrepository;
    private BuffVisualsArtVariationsRepository? buffvisualsartvariationsrepository;
    private BuffVisualSetEntriesRepository? buffvisualsetentriesrepository;
    private CharacterAudioEventsRepository? characteraudioeventsrepository;
    private CharacterEventTextAudioRepository? charactereventtextaudiorepository;
    private CharacterPanelDescriptionModesRepository? characterpaneldescriptionmodesrepository;
    private CharacterPanelStatsRepository? characterpanelstatsrepository;
    private CharacterPanelTabsRepository? characterpaneltabsrepository;
    private CharactersRepository? charactersrepository;
    private CharacterStartQuestStateRepository? characterstartqueststaterepository;
    private CharacterStartStatesRepository? characterstartstatesrepository;
    private CharacterStartStateSetRepository? characterstartstatesetrepository;
    private CharacterTextAudioRepository? charactertextaudiorepository;
    private ChatIconsRepository? chaticonsrepository;
    private ChestClustersRepository? chestclustersrepository;
    private ChestEffectsRepository? chesteffectsrepository;
    private ChestsRepository? chestsrepository;
    private ClientStringsRepository? clientstringsrepository;
    private ClientLeagueActionRepository? clientleagueactionrepository;
    private CloneShotRepository? cloneshotrepository;
    private ColoursRepository? coloursrepository;
    private CommandsRepository? commandsrepository;
    private ComponentAttributeRequirementsRepository? componentattributerequirementsrepository;
    private ComponentChargesRepository? componentchargesrepository;
    private CoreLeaguesRepository? coreleaguesrepository;
    private CostTypesRepository? costtypesrepository;
    private CraftingBenchOptionsRepository? craftingbenchoptionsrepository;
    private CraftingBenchSortCategoriesRepository? craftingbenchsortcategoriesrepository;
    private CraftingBenchUnlockCategoriesRepository? craftingbenchunlockcategoriesrepository;
    private CraftingItemClassCategoriesRepository? craftingitemclasscategoriesrepository;
    private CurrencyItemsRepository? currencyitemsrepository;
    private CurrencyStashTabLayoutRepository? currencystashtablayoutrepository;
    private CustomLeagueModsRepository? customleaguemodsrepository;
    private DaemonSpawningDataRepository? daemonspawningdatarepository;
    private DamageHitEffectsRepository? damagehiteffectsrepository;
    private DamageParticleEffectsRepository? damageparticleeffectsrepository;
    private DancesRepository? dancesrepository;
    private DaressoPitFightsRepository? daressopitfightsrepository;
    private DefaultMonsterStatsRepository? defaultmonsterstatsrepository;
    private DeliriumStashTabLayoutRepository? deliriumstashtablayoutrepository;
    private DelveStashTabLayoutRepository? delvestashtablayoutrepository;
    private DescentExilesRepository? descentexilesrepository;
    private DescentRewardChestsRepository? descentrewardchestsrepository;
    private DescentStarterChestRepository? descentstarterchestrepository;
    private DialogueEventRepository? dialogueeventrepository;
    private DisplayMinionMonsterTypeRepository? displayminionmonstertyperepository;
    private DivinationCardStashTabLayoutRepository? divinationcardstashtablayoutrepository;
    private DoorsRepository? doorsrepository;
    private DropEffectsRepository? dropeffectsrepository;
    private DropPoolRepository? droppoolrepository;
    private EclipseModsRepository? eclipsemodsrepository;
    private EffectDrivenSkillRepository? effectdrivenskillrepository;
    private EffectivenessCostConstantsRepository? effectivenesscostconstantsrepository;
    private EinharMissionsRepository? einharmissionsrepository;
    private EinharPackFallbackRepository? einharpackfallbackrepository;
    private EndlessLedgeChestsRepository? endlessledgechestsrepository;
    private EnvironmentsRepository? environmentsrepository;
    private EnvironmentTransitionsRepository? environmenttransitionsrepository;
    private EssenceStashTabLayoutRepository? essencestashtablayoutrepository;
    private EventSeasonRepository? eventseasonrepository;
    private EventSeasonRewardsRepository? eventseasonrewardsrepository;
    private EvergreenAchievementsRepository? evergreenachievementsrepository;
    private ExecuteGEALRepository? executegealrepository;
    private ExpandingPulseRepository? expandingpulserepository;
    private ExperienceLevelsRepository? experiencelevelsrepository;
    private ExplodingStormBuffsRepository? explodingstormbuffsrepository;
    private ExtraTerrainFeaturesRepository? extraterrainfeaturesrepository;
    private FixedHideoutDoodadTypesRepository? fixedhideoutdoodadtypesrepository;
    private FixedMissionsRepository? fixedmissionsrepository;
    private FlasksRepository? flasksrepository;
    private FlavourTextRepository? flavourtextrepository;
    private FootprintsRepository? footprintsrepository;
    private FootstepAudioRepository? footstepaudiorepository;
    private FragmentStashTabLayoutRepository? fragmentstashtablayoutrepository;
    private GameConstantsRepository? gameconstantsrepository;
    private GameLogosRepository? gamelogosrepository;
    private GameObjectTasksRepository? gameobjecttasksrepository;
    private GamepadButtonRepository? gamepadbuttonrepository;
    private GamepadTypeRepository? gamepadtyperepository;
    private GameStatsRepository? gamestatsrepository;
    private GemTagsRepository? gemtagsrepository;
    private GenericBuffAurasRepository? genericbuffaurasrepository;
    private GenericLeagueRewardTypesRepository? genericleaguerewardtypesrepository;
    private GenericLeagueRewardTypeVisualsRepository? genericleaguerewardtypevisualsrepository;
    private GeometryAttackRepository? geometryattackrepository;
    private GeometryChannelRepository? geometrychannelrepository;
    private GeometryProjectilesRepository? geometryprojectilesrepository;
    private GeometryTriggerRepository? geometrytriggerrepository;
    private GiftWrapArtVariationsRepository? giftwrapartvariationsrepository;
    private GlobalAudioConfigRepository? globalaudioconfigrepository;
    private GrandmastersRepository? grandmastersrepository;
    private GrantedEffectQualityStatsRepository? grantedeffectqualitystatsrepository;
    private GrantedEffectQualityTypesRepository? grantedeffectqualitytypesrepository;
    private GrantedEffectsRepository? grantedeffectsrepository;
    private GrantedEffectsPerLevelRepository? grantedeffectsperlevelrepository;
    private GrantedEffectStatSetsRepository? grantedeffectstatsetsrepository;
    private GrantedEffectStatSetsPerLevelRepository? grantedeffectstatsetsperlevelrepository;
    private GroundEffectsRepository? groundeffectsrepository;
    private GroundEffectTypesRepository? groundeffecttypesrepository;
    private HarvestStorageLayoutRepository? harveststoragelayoutrepository;
    private HeistStorageLayoutRepository? heiststoragelayoutrepository;
    private HideoutDoodadsRepository? hideoutdoodadsrepository;
    private HideoutDoodadCategoryRepository? hideoutdoodadcategoryrepository;
    private HideoutDoodadTagsRepository? hideoutdoodadtagsrepository;
    private HideoutNPCsRepository? hideoutnpcsrepository;
    private HideoutRarityRepository? hideoutrarityrepository;
    private HideoutsRepository? hideoutsrepository;
    private ImpactSoundDataRepository? impactsounddatarepository;
    private IndexableSupportGemsRepository? indexablesupportgemsrepository;
    private InfluenceExaltsRepository? influenceexaltsrepository;
    private InfluenceTagsRepository? influencetagsrepository;
    private InventoriesRepository? inventoriesrepository;
    private ItemClassCategoriesRepository? itemclasscategoriesrepository;
    private ItemClassesRepository? itemclassesrepository;
    private ItemCostPerLevelRepository? itemcostperlevelrepository;
    private ItemCostsRepository? itemcostsrepository;
    private ItemFrameTypeRepository? itemframetyperepository;
    private ItemExperienceTypesRepository? itemexperiencetypesrepository;
    private ItemExperiencePerLevelRepository? itemexperienceperlevelrepository;
    private ItemisedVisualEffectRepository? itemisedvisualeffectrepository;
    private ItemNoteCodeRepository? itemnotecoderepository;
    private ItemShopTypeRepository? itemshoptyperepository;
    private ItemStancesRepository? itemstancesrepository;
    private ItemThemesRepository? itemthemesrepository;
    private ItemVisualEffectRepository? itemvisualeffectrepository;
    private ItemVisualHeldBodyModelRepository? itemvisualheldbodymodelrepository;
    private ItemVisualIdentityRepository? itemvisualidentityrepository;
    private JobAssassinationSpawnerGroupsRepository? jobassassinationspawnergroupsrepository;
    private JobRaidBracketsRepository? jobraidbracketsrepository;
    private KillstreakThresholdsRepository? killstreakthresholdsrepository;
    private LeagueFlagRepository? leagueflagrepository;
    private LeagueInfoRepository? leagueinforepository;
    private LeagueProgressQuestFlagsRepository? leagueprogressquestflagsrepository;
    private LeagueStaticRewardsRepository? leaguestaticrewardsrepository;
    private LevelRelativePlayerScalingRepository? levelrelativeplayerscalingrepository;
    private MagicMonsterLifeScalingPerLevelRepository? magicmonsterlifescalingperlevelrepository;
    private MapCompletionAchievementsRepository? mapcompletionachievementsrepository;
    private MapConnectionsRepository? mapconnectionsrepository;
    private MapCreationInformationRepository? mapcreationinformationrepository;
    private MapDeviceRecipesRepository? mapdevicerecipesrepository;
    private MapDevicesRepository? mapdevicesrepository;
    private MapFragmentModsRepository? mapfragmentmodsrepository;
    private MapInhabitantsRepository? mapinhabitantsrepository;
    private MapPinsRepository? mappinsrepository;
    private MapPurchaseCostsRepository? mappurchasecostsrepository;
    private MapsRepository? mapsrepository;
    private MapSeriesRepository? mapseriesrepository;
    private MapSeriesTiersRepository? mapseriestiersrepository;
    private MapStashSpecialTypeEntriesRepository? mapstashspecialtypeentriesrepository;
    private MapStashUniqueMapInfoRepository? mapstashuniquemapinforepository;
    private MapStatConditionsRepository? mapstatconditionsrepository;
    private MapTierAchievementsRepository? maptierachievementsrepository;
    private MapTiersRepository? maptiersrepository;
    private MasterHideoutLevelsRepository? masterhideoutlevelsrepository;
    private MeleeRepository? meleerepository;
    private MeleeTrailsRepository? meleetrailsrepository;
    private MetamorphosisStashTabLayoutRepository? metamorphosisstashtablayoutrepository;
    private MicroMigrationDataRepository? micromigrationdatarepository;
    private MicrotransactionCategoryRepository? microtransactioncategoryrepository;
    private MicrotransactionCharacterPortraitVariationsRepository? microtransactioncharacterportraitvariationsrepository;
    private MicrotransactionCombineFormulaRepository? microtransactioncombineformularepository;
    private MicrotransactionCursorVariationsRepository? microtransactioncursorvariationsrepository;
    private MicrotransactionFireworksVariationsRepository? microtransactionfireworksvariationsrepository;
    private MicrotransactionGemCategoryRepository? microtransactiongemcategoryrepository;
    private MicrotransactionPeriodicCharacterEffectVariationsRepository? microtransactionperiodiccharactereffectvariationsrepository;
    private MicrotransactionPortalVariationsRepository? microtransactionportalvariationsrepository;
    private MicrotransactionRarityDisplayRepository? microtransactionraritydisplayrepository;
    private MicrotransactionRecycleOutcomesRepository? microtransactionrecycleoutcomesrepository;
    private MicrotransactionRecycleSalvageValuesRepository? microtransactionrecyclesalvagevaluesrepository;
    private MicrotransactionSlotRepository? microtransactionslotrepository;
    private MicrotransactionSocialFrameVariationsRepository? microtransactionsocialframevariationsrepository;
    private MinimapIconsRepository? minimapiconsrepository;
    private MiniQuestStatesRepository? miniqueststatesrepository;
    private MiscAnimatedRepository? miscanimatedrepository;
    private MiscAnimatedArtVariationsRepository? miscanimatedartvariationsrepository;
    private MiscBeamsRepository? miscbeamsrepository;
    private MiscBeamsArtVariationsRepository? miscbeamsartvariationsrepository;
    private MiscEffectPacksRepository? misceffectpacksrepository;
    private MiscEffectPacksArtVariationsRepository? misceffectpacksartvariationsrepository;
    private MiscObjectsRepository? miscobjectsrepository;
    private MiscObjectsArtVariationsRepository? miscobjectsartvariationsrepository;
    private MissionFavourPerLevelRepository? missionfavourperlevelrepository;
    private MissionTimerTypesRepository? missiontimertypesrepository;
    private MissionTransitionTilesRepository? missiontransitiontilesrepository;
    private ModEffectStatsRepository? modeffectstatsrepository;
    private ModEquivalenciesRepository? modequivalenciesrepository;
    private ModFamilyRepository? modfamilyrepository;
    private ModsRepository? modsrepository;
    private ModSellPriceTypesRepository? modsellpricetypesrepository;
    private ModTypeRepository? modtyperepository;
    private MonsterArmoursRepository? monsterarmoursrepository;
    private MonsterBonusesRepository? monsterbonusesrepository;
    private MonsterConditionalEffectPacksRepository? monsterconditionaleffectpacksrepository;
    private MonsterConditionsRepository? monsterconditionsrepository;
    private MonsterDeathAchievementsRepository? monsterdeathachievementsrepository;
    private MonsterDeathConditionsRepository? monsterdeathconditionsrepository;
    private MonsterGroupEntriesRepository? monstergroupentriesrepository;
    private MonsterHeightBracketsRepository? monsterheightbracketsrepository;
    private MonsterHeightsRepository? monsterheightsrepository;
    private MonsterMapBossDifficultyRepository? monstermapbossdifficultyrepository;
    private MonsterMapDifficultyRepository? monstermapdifficultyrepository;
    private MonsterMortarRepository? monstermortarrepository;
    private MonsterPackCountsRepository? monsterpackcountsrepository;
    private MonsterPackEntriesRepository? monsterpackentriesrepository;
    private MonsterPacksRepository? monsterpacksrepository;
    private MonsterProjectileAttackRepository? monsterprojectileattackrepository;
    private MonsterProjectileSpellRepository? monsterprojectilespellrepository;
    private MonsterResistancesRepository? monsterresistancesrepository;
    private MonsterSegmentsRepository? monstersegmentsrepository;
    private MonsterSpawnerGroupsRepository? monsterspawnergroupsrepository;
    private MonsterSpawnerGroupsPerLevelRepository? monsterspawnergroupsperlevelrepository;
    private MonsterSpawnerOverridesRepository? monsterspawneroverridesrepository;
    private MonsterTypesRepository? monstertypesrepository;
    private MonsterVarietiesRepository? monstervarietiesrepository;
    private MonsterVarietiesArtVariationsRepository? monstervarietiesartvariationsrepository;
    private MouseCursorSizeSettingsRepository? mousecursorsizesettingsrepository;
    private MoveDaemonRepository? movedaemonrepository;
    private MTXSetBonusRepository? mtxsetbonusrepository;
    private MultiPartAchievementAreasRepository? multipartachievementareasrepository;
    private MultiPartAchievementConditionsRepository? multipartachievementconditionsrepository;
    private MultiPartAchievementsRepository? multipartachievementsrepository;
    private MusicRepository? musicrepository;
    private MusicCategoriesRepository? musiccategoriesrepository;
    private MysteryBoxesRepository? mysteryboxesrepository;
    private NearbyMonsterConditionsRepository? nearbymonsterconditionsrepository;
    private NetTiersRepository? nettiersrepository;
    private NotificationsRepository? notificationsrepository;
    private NPCAudioRepository? npcaudiorepository;
    private NPCConversationsRepository? npcconversationsrepository;
    private NPCDialogueStylesRepository? npcdialoguestylesrepository;
    private NPCFollowerVariationsRepository? npcfollowervariationsrepository;
    private NPCMasterRepository? npcmasterrepository;
    private NPCPortraitsRepository? npcportraitsrepository;
    private NPCsRepository? npcsrepository;
    private NPCShopRepository? npcshoprepository;
    private NPCShopsRepository? npcshopsrepository;
    private NPCTalkRepository? npctalkrepository;
    private NPCTalkCategoryRepository? npctalkcategoryrepository;
    private NPCTalkConsoleQuickActionsRepository? npctalkconsolequickactionsrepository;
    private NPCTextAudioRepository? npctextaudiorepository;
    private OnKillAchievementsRepository? onkillachievementsrepository;
    private PackFormationRepository? packformationrepository;
    private PassiveJewelRadiiRepository? passivejewelradiirepository;
    private PassiveJewelSlotsRepository? passivejewelslotsrepository;
    private PassiveSkillFilterCatagoriesRepository? passiveskillfiltercatagoriesrepository;
    private PassiveSkillFilterOptionsRepository? passiveskillfilteroptionsrepository;
    private PassiveSkillMasteryGroupsRepository? passiveskillmasterygroupsrepository;
    private PassiveSkillMasteryEffectsRepository? passiveskillmasteryeffectsrepository;
    private PassiveSkillsRepository? passiveskillsrepository;
    private PassiveSkillStatCategoriesRepository? passiveskillstatcategoriesrepository;
    private PassiveSkillTreesRepository? passiveskilltreesrepository;
    private PassiveSkillTreeTutorialRepository? passiveskilltreetutorialrepository;
    private PassiveSkillTreeUIArtRepository? passiveskilltreeuiartrepository;
    private PassiveTreeExpansionJewelsRepository? passivetreeexpansionjewelsrepository;
    private PassiveTreeExpansionJewelSizesRepository? passivetreeexpansionjewelsizesrepository;
    private PassiveTreeExpansionSkillsRepository? passivetreeexpansionskillsrepository;
    private PassiveTreeExpansionSpecialSkillsRepository? passivetreeexpansionspecialskillsrepository;
    private PCBangRewardMicrosRepository? pcbangrewardmicrosrepository;
    private PetRepository? petrepository;
    private PlayerConditionsRepository? playerconditionsrepository;
    private PlayerTradeWhisperFormatsRepository? playertradewhisperformatsrepository;
    private PreloadGroupsRepository? preloadgroupsrepository;
    private ProjectilesRepository? projectilesrepository;
    private ProjectilesArtVariationsRepository? projectilesartvariationsrepository;
    private ProjectileVariationsRepository? projectilevariationsrepository;
    private PVPTypesRepository? pvptypesrepository;
    private QuestRepository? questrepository;
    private QuestAchievementsRepository? questachievementsrepository;
    private QuestFlagsRepository? questflagsrepository;
    private QuestItemsRepository? questitemsrepository;
    private QuestRewardOffersRepository? questrewardoffersrepository;
    private QuestRewardsRepository? questrewardsrepository;
    private QuestStatesRepository? queststatesrepository;
    private QuestStaticRewardsRepository? queststaticrewardsrepository;
    private QuestTrackerGroupRepository? questtrackergrouprepository;
    private QuestTypeRepository? questtyperepository;
    private RacesRepository? racesrepository;
    private RaceTimesRepository? racetimesrepository;
    private RareMonsterLifeScalingPerLevelRepository? raremonsterlifescalingperlevelrepository;
    private RarityRepository? rarityrepository;
    private RealmsRepository? realmsrepository;
    private RecipeUnlockDisplayRepository? recipeunlockdisplayrepository;
    private RecipeUnlockObjectsRepository? recipeunlockobjectsrepository;
    private ReminderTextRepository? remindertextrepository;
    private RulesetsRepository? rulesetsrepository;
    private RunicCirclesRepository? runiccirclesrepository;
    private SalvageBoxesRepository? salvageboxesrepository;
    private SessionQuestFlagsRepository? sessionquestflagsrepository;
    private ShieldTypesRepository? shieldtypesrepository;
    private ShopCategoryRepository? shopcategoryrepository;
    private ShopCountryRepository? shopcountryrepository;
    private ShopCurrencyRepository? shopcurrencyrepository;
    private ShopPaymentPackageRepository? shoppaymentpackagerepository;
    private ShopPaymentPackagePriceRepository? shoppaymentpackagepricerepository;
    private ShopRegionRepository? shopregionrepository;
    private ShopTagRepository? shoptagrepository;
    private ShopTokenRepository? shoptokenrepository;
    private SigilDisplayRepository? sigildisplayrepository;
    private SingleGroundLaserRepository? singlegroundlaserrepository;
    private SkillArtVariationsRepository? skillartvariationsrepository;
    private SkillGemInfoRepository? skillgeminforepository;
    private SkillGemsRepository? skillgemsrepository;
    private SkillMineVariationsRepository? skillminevariationsrepository;
    private SkillMorphDisplayRepository? skillmorphdisplayrepository;
    private SkillSurgeEffectsRepository? skillsurgeeffectsrepository;
    private SkillTotemVariationsRepository? skilltotemvariationsrepository;
    private SkillTrapVariationsRepository? skilltrapvariationsrepository;
    private SocketNotchesRepository? socketnotchesrepository;
    private SoundEffectsRepository? soundeffectsrepository;
    private SpawnAdditionalChestsOrClustersRepository? spawnadditionalchestsorclustersrepository;
    private SpawnObjectRepository? spawnobjectrepository;
    private SpecialRoomsRepository? specialroomsrepository;
    private SpecialTilesRepository? specialtilesrepository;
    private SpectreOverridesRepository? spectreoverridesrepository;
    private StartingPassiveSkillsRepository? startingpassiveskillsrepository;
    private StashTabAffinitiesRepository? stashtabaffinitiesrepository;
    private StashTypeRepository? stashtyperepository;
    private StatDescriptionFunctionsRepository? statdescriptionfunctionsrepository;
    private StatsAffectingGenerationRepository? statsaffectinggenerationrepository;
    private StatsRepository? statsrepository;
    private StrDexIntMissionExtraRequirementRepository? strdexintmissionextrarequirementrepository;
    private StrDexIntMissionsRepository? strdexintmissionsrepository;
    private SuicideExplosionRepository? suicideexplosionrepository;
    private SummonedSpecificBarrelsRepository? summonedspecificbarrelsrepository;
    private SummonedSpecificMonstersRepository? summonedspecificmonstersrepository;
    private SummonedSpecificMonstersOnDeathRepository? summonedspecificmonstersondeathrepository;
    private SupporterPackSetsRepository? supporterpacksetsrepository;
    private SurgeEffectsRepository? surgeeffectsrepository;
    private SurgeTypesRepository? surgetypesrepository;
    private TableChargeRepository? tablechargerepository;
    private TableMonsterSpawnersRepository? tablemonsterspawnersrepository;
    private TagsRepository? tagsrepository;
    private TalkingPetAudioEventsRepository? talkingpetaudioeventsrepository;
    private TalkingPetNPCAudioRepository? talkingpetnpcaudiorepository;
    private TalkingPetsRepository? talkingpetsrepository;
    private TencentAutoLootPetCurrenciesRepository? tencentautolootpetcurrenciesrepository;
    private TencentAutoLootPetCurrenciesExcludableRepository? tencentautolootpetcurrenciesexcludablerepository;
    private TerrainPluginsRepository? terrainpluginsrepository;
    private TipsRepository? tipsrepository;
    private TopologiesRepository? topologiesrepository;
    private TradeMarketCategoryRepository? trademarketcategoryrepository;
    private TradeMarketCategoryGroupsRepository? trademarketcategorygroupsrepository;
    private TradeMarketCategoryListAllClassRepository? trademarketcategorylistallclassrepository;
    private TradeMarketIndexItemAsRepository? trademarketindexitemasrepository;
    private TreasureHunterMissionsRepository? treasurehuntermissionsrepository;
    private TriggerBeamRepository? triggerbeamrepository;
    private TriggerSpawnersRepository? triggerspawnersrepository;
    private TutorialRepository? tutorialrepository;
    private UITalkTextRepository? uitalktextrepository;
    private UniqueChestsRepository? uniquechestsrepository;
    private UniqueJewelLimitsRepository? uniquejewellimitsrepository;
    private UniqueMapInfoRepository? uniquemapinforepository;
    private UniqueMapsRepository? uniquemapsrepository;
    private UniqueStashLayoutRepository? uniquestashlayoutrepository;
    private UniqueStashTypesRepository? uniquestashtypesrepository;
    private VirtualStatContextFlagsRepository? virtualstatcontextflagsrepository;
    private VoteStateRepository? votestaterepository;
    private VoteTypeRepository? votetyperepository;
    private WeaponClassesRepository? weaponclassesrepository;
    private WeaponImpactSoundDataRepository? weaponimpactsounddatarepository;
    private WeaponTypesRepository? weapontypesrepository;
    private WindowCursorsRepository? windowcursorsrepository;
    private WordsRepository? wordsrepository;
    private WorldAreasRepository? worldareasrepository;
    private WorldAreaLeagueChancesRepository? worldarealeaguechancesrepository;
    private WorldPopupIconTypesRepository? worldpopupicontypesrepository;
    private ZanaLevelsRepository? zanalevelsrepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="Specification"/> class.
    /// </summary>
    /// <param name="config">Contains config data.</param>
    /// <param name="logger">Contains logger used through the application.</param>
    public Specification(IConfig config, ILogger logger)
    {
        if (config is null)
        {
            throw new ArgumentNullException(nameof(config));
        }

        if (logger is null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        dataLoader = new DataLoader(config, logger);

        this.logger = logger;
    }

    /// <summary>
    /// Gets RogueExilesRepository data.
    /// </summary>
    /// <returns>repository of RogueExilesRepository.</returns>
    public RogueExilesRepository LoadRogueExilesRepository()
    {
        rogueexilesrepository ??= new RogueExilesRepository(logger, this);
        return rogueexilesrepository;
    }

    /// <summary>
    /// Gets RogueExileLifeScalingPerLevelRepository data.
    /// </summary>
    /// <returns>repository of RogueExileLifeScalingPerLevelRepository.</returns>
    public RogueExileLifeScalingPerLevelRepository LoadRogueExileLifeScalingPerLevelRepository()
    {
        rogueexilelifescalingperlevelrepository ??= new RogueExileLifeScalingPerLevelRepository(logger, this);
        return rogueexilelifescalingperlevelrepository;
    }

    /// <summary>
    /// Gets ShrineBuffsRepository data.
    /// </summary>
    /// <returns>repository of ShrineBuffsRepository.</returns>
    public ShrineBuffsRepository LoadShrineBuffsRepository()
    {
        shrinebuffsrepository ??= new ShrineBuffsRepository(logger, this);
        return shrinebuffsrepository;
    }

    /// <summary>
    /// Gets ShrinesRepository data.
    /// </summary>
    /// <returns>repository of ShrinesRepository.</returns>
    public ShrinesRepository LoadShrinesRepository()
    {
        shrinesrepository ??= new ShrinesRepository(logger, this);
        return shrinesrepository;
    }

    /// <summary>
    /// Gets ShrineSoundsRepository data.
    /// </summary>
    /// <returns>repository of ShrineSoundsRepository.</returns>
    public ShrineSoundsRepository LoadShrineSoundsRepository()
    {
        shrinesoundsrepository ??= new ShrineSoundsRepository(logger, this);
        return shrinesoundsrepository;
    }

    /// <summary>
    /// Gets StrongboxesRepository data.
    /// </summary>
    /// <returns>repository of StrongboxesRepository.</returns>
    public StrongboxesRepository LoadStrongboxesRepository()
    {
        strongboxesrepository ??= new StrongboxesRepository(logger, this);
        return strongboxesrepository;
    }

    /// <summary>
    /// Gets InvasionMonsterRestrictionsRepository data.
    /// </summary>
    /// <returns>repository of InvasionMonsterRestrictionsRepository.</returns>
    public InvasionMonsterRestrictionsRepository LoadInvasionMonsterRestrictionsRepository()
    {
        invasionmonsterrestrictionsrepository ??= new InvasionMonsterRestrictionsRepository(logger, this);
        return invasionmonsterrestrictionsrepository;
    }

    /// <summary>
    /// Gets InvasionMonstersPerAreaRepository data.
    /// </summary>
    /// <returns>repository of InvasionMonstersPerAreaRepository.</returns>
    public InvasionMonstersPerAreaRepository LoadInvasionMonstersPerAreaRepository()
    {
        invasionmonstersperarearepository ??= new InvasionMonstersPerAreaRepository(logger, this);
        return invasionmonstersperarearepository;
    }

    /// <summary>
    /// Gets BeyondDemonsRepository data.
    /// </summary>
    /// <returns>repository of BeyondDemonsRepository.</returns>
    public BeyondDemonsRepository LoadBeyondDemonsRepository()
    {
        beyonddemonsrepository ??= new BeyondDemonsRepository(logger, this);
        return beyonddemonsrepository;
    }

    /// <summary>
    /// Gets BeyondFactionsRepository data.
    /// </summary>
    /// <returns>repository of BeyondFactionsRepository.</returns>
    public BeyondFactionsRepository LoadBeyondFactionsRepository()
    {
        beyondfactionsrepository ??= new BeyondFactionsRepository(logger, this);
        return beyondfactionsrepository;
    }

    /// <summary>
    /// Gets BloodlinesRepository data.
    /// </summary>
    /// <returns>repository of BloodlinesRepository.</returns>
    public BloodlinesRepository LoadBloodlinesRepository()
    {
        bloodlinesrepository ??= new BloodlinesRepository(logger, this);
        return bloodlinesrepository;
    }

    /// <summary>
    /// Gets TormentSpiritsRepository data.
    /// </summary>
    /// <returns>repository of TormentSpiritsRepository.</returns>
    public TormentSpiritsRepository LoadTormentSpiritsRepository()
    {
        tormentspiritsrepository ??= new TormentSpiritsRepository(logger, this);
        return tormentspiritsrepository;
    }

    /// <summary>
    /// Gets DivinationCardArtRepository data.
    /// </summary>
    /// <returns>repository of DivinationCardArtRepository.</returns>
    public DivinationCardArtRepository LoadDivinationCardArtRepository()
    {
        divinationcardartrepository ??= new DivinationCardArtRepository(logger, this);
        return divinationcardartrepository;
    }

    /// <summary>
    /// Gets WarbandsGraphRepository data.
    /// </summary>
    /// <returns>repository of WarbandsGraphRepository.</returns>
    public WarbandsGraphRepository LoadWarbandsGraphRepository()
    {
        warbandsgraphrepository ??= new WarbandsGraphRepository(logger, this);
        return warbandsgraphrepository;
    }

    /// <summary>
    /// Gets WarbandsMapGraphRepository data.
    /// </summary>
    /// <returns>repository of WarbandsMapGraphRepository.</returns>
    public WarbandsMapGraphRepository LoadWarbandsMapGraphRepository()
    {
        warbandsmapgraphrepository ??= new WarbandsMapGraphRepository(logger, this);
        return warbandsmapgraphrepository;
    }

    /// <summary>
    /// Gets WarbandsPackMonstersRepository data.
    /// </summary>
    /// <returns>repository of WarbandsPackMonstersRepository.</returns>
    public WarbandsPackMonstersRepository LoadWarbandsPackMonstersRepository()
    {
        warbandspackmonstersrepository ??= new WarbandsPackMonstersRepository(logger, this);
        return warbandspackmonstersrepository;
    }

    /// <summary>
    /// Gets WarbandsPackNumbersRepository data.
    /// </summary>
    /// <returns>repository of WarbandsPackNumbersRepository.</returns>
    public WarbandsPackNumbersRepository LoadWarbandsPackNumbersRepository()
    {
        warbandspacknumbersrepository ??= new WarbandsPackNumbersRepository(logger, this);
        return warbandspacknumbersrepository;
    }

    /// <summary>
    /// Gets TalismanMonsterModsRepository data.
    /// </summary>
    /// <returns>repository of TalismanMonsterModsRepository.</returns>
    public TalismanMonsterModsRepository LoadTalismanMonsterModsRepository()
    {
        talismanmonstermodsrepository ??= new TalismanMonsterModsRepository(logger, this);
        return talismanmonstermodsrepository;
    }

    /// <summary>
    /// Gets TalismanPacksRepository data.
    /// </summary>
    /// <returns>repository of TalismanPacksRepository.</returns>
    public TalismanPacksRepository LoadTalismanPacksRepository()
    {
        talismanpacksrepository ??= new TalismanPacksRepository(logger, this);
        return talismanpacksrepository;
    }

    /// <summary>
    /// Gets TalismansRepository data.
    /// </summary>
    /// <returns>repository of TalismansRepository.</returns>
    public TalismansRepository LoadTalismansRepository()
    {
        talismansrepository ??= new TalismansRepository(logger, this);
        return talismansrepository;
    }

    /// <summary>
    /// Gets LabyrinthAreasRepository data.
    /// </summary>
    /// <returns>repository of LabyrinthAreasRepository.</returns>
    public LabyrinthAreasRepository LoadLabyrinthAreasRepository()
    {
        labyrinthareasrepository ??= new LabyrinthAreasRepository(logger, this);
        return labyrinthareasrepository;
    }

    /// <summary>
    /// Gets LabyrinthBonusItemsRepository data.
    /// </summary>
    /// <returns>repository of LabyrinthBonusItemsRepository.</returns>
    public LabyrinthBonusItemsRepository LoadLabyrinthBonusItemsRepository()
    {
        labyrinthbonusitemsrepository ??= new LabyrinthBonusItemsRepository(logger, this);
        return labyrinthbonusitemsrepository;
    }

    /// <summary>
    /// Gets LabyrinthExclusionGroupsRepository data.
    /// </summary>
    /// <returns>repository of LabyrinthExclusionGroupsRepository.</returns>
    public LabyrinthExclusionGroupsRepository LoadLabyrinthExclusionGroupsRepository()
    {
        labyrinthexclusiongroupsrepository ??= new LabyrinthExclusionGroupsRepository(logger, this);
        return labyrinthexclusiongroupsrepository;
    }

    /// <summary>
    /// Gets LabyrinthIzaroChestsRepository data.
    /// </summary>
    /// <returns>repository of LabyrinthIzaroChestsRepository.</returns>
    public LabyrinthIzaroChestsRepository LoadLabyrinthIzaroChestsRepository()
    {
        labyrinthizarochestsrepository ??= new LabyrinthIzaroChestsRepository(logger, this);
        return labyrinthizarochestsrepository;
    }

    /// <summary>
    /// Gets LabyrinthNodeOverridesRepository data.
    /// </summary>
    /// <returns>repository of LabyrinthNodeOverridesRepository.</returns>
    public LabyrinthNodeOverridesRepository LoadLabyrinthNodeOverridesRepository()
    {
        labyrinthnodeoverridesrepository ??= new LabyrinthNodeOverridesRepository(logger, this);
        return labyrinthnodeoverridesrepository;
    }

    /// <summary>
    /// Gets LabyrinthRewardTypesRepository data.
    /// </summary>
    /// <returns>repository of LabyrinthRewardTypesRepository.</returns>
    public LabyrinthRewardTypesRepository LoadLabyrinthRewardTypesRepository()
    {
        labyrinthrewardtypesrepository ??= new LabyrinthRewardTypesRepository(logger, this);
        return labyrinthrewardtypesrepository;
    }

    /// <summary>
    /// Gets LabyrinthsRepository data.
    /// </summary>
    /// <returns>repository of LabyrinthsRepository.</returns>
    public LabyrinthsRepository LoadLabyrinthsRepository()
    {
        labyrinthsrepository ??= new LabyrinthsRepository(logger, this);
        return labyrinthsrepository;
    }

    /// <summary>
    /// Gets LabyrinthSecretEffectsRepository data.
    /// </summary>
    /// <returns>repository of LabyrinthSecretEffectsRepository.</returns>
    public LabyrinthSecretEffectsRepository LoadLabyrinthSecretEffectsRepository()
    {
        labyrinthsecreteffectsrepository ??= new LabyrinthSecretEffectsRepository(logger, this);
        return labyrinthsecreteffectsrepository;
    }

    /// <summary>
    /// Gets LabyrinthSecretsRepository data.
    /// </summary>
    /// <returns>repository of LabyrinthSecretsRepository.</returns>
    public LabyrinthSecretsRepository LoadLabyrinthSecretsRepository()
    {
        labyrinthsecretsrepository ??= new LabyrinthSecretsRepository(logger, this);
        return labyrinthsecretsrepository;
    }

    /// <summary>
    /// Gets LabyrinthSectionRepository data.
    /// </summary>
    /// <returns>repository of LabyrinthSectionRepository.</returns>
    public LabyrinthSectionRepository LoadLabyrinthSectionRepository()
    {
        labyrinthsectionrepository ??= new LabyrinthSectionRepository(logger, this);
        return labyrinthsectionrepository;
    }

    /// <summary>
    /// Gets LabyrinthSectionLayoutRepository data.
    /// </summary>
    /// <returns>repository of LabyrinthSectionLayoutRepository.</returns>
    public LabyrinthSectionLayoutRepository LoadLabyrinthSectionLayoutRepository()
    {
        labyrinthsectionlayoutrepository ??= new LabyrinthSectionLayoutRepository(logger, this);
        return labyrinthsectionlayoutrepository;
    }

    /// <summary>
    /// Gets LabyrinthTrialsRepository data.
    /// </summary>
    /// <returns>repository of LabyrinthTrialsRepository.</returns>
    public LabyrinthTrialsRepository LoadLabyrinthTrialsRepository()
    {
        labyrinthtrialsrepository ??= new LabyrinthTrialsRepository(logger, this);
        return labyrinthtrialsrepository;
    }

    /// <summary>
    /// Gets LabyrinthTrinketsRepository data.
    /// </summary>
    /// <returns>repository of LabyrinthTrinketsRepository.</returns>
    public LabyrinthTrinketsRepository LoadLabyrinthTrinketsRepository()
    {
        labyrinthtrinketsrepository ??= new LabyrinthTrinketsRepository(logger, this);
        return labyrinthtrinketsrepository;
    }

    /// <summary>
    /// Gets PerandusBossesRepository data.
    /// </summary>
    /// <returns>repository of PerandusBossesRepository.</returns>
    public PerandusBossesRepository LoadPerandusBossesRepository()
    {
        perandusbossesrepository ??= new PerandusBossesRepository(logger, this);
        return perandusbossesrepository;
    }

    /// <summary>
    /// Gets PerandusChestsRepository data.
    /// </summary>
    /// <returns>repository of PerandusChestsRepository.</returns>
    public PerandusChestsRepository LoadPerandusChestsRepository()
    {
        peranduschestsrepository ??= new PerandusChestsRepository(logger, this);
        return peranduschestsrepository;
    }

    /// <summary>
    /// Gets PerandusDaemonsRepository data.
    /// </summary>
    /// <returns>repository of PerandusDaemonsRepository.</returns>
    public PerandusDaemonsRepository LoadPerandusDaemonsRepository()
    {
        perandusdaemonsrepository ??= new PerandusDaemonsRepository(logger, this);
        return perandusdaemonsrepository;
    }

    /// <summary>
    /// Gets PerandusGuardsRepository data.
    /// </summary>
    /// <returns>repository of PerandusGuardsRepository.</returns>
    public PerandusGuardsRepository LoadPerandusGuardsRepository()
    {
        perandusguardsrepository ??= new PerandusGuardsRepository(logger, this);
        return perandusguardsrepository;
    }

    /// <summary>
    /// Gets PropheciesRepository data.
    /// </summary>
    /// <returns>repository of PropheciesRepository.</returns>
    public PropheciesRepository LoadPropheciesRepository()
    {
        propheciesrepository ??= new PropheciesRepository(logger, this);
        return propheciesrepository;
    }

    /// <summary>
    /// Gets ProphecyChainRepository data.
    /// </summary>
    /// <returns>repository of ProphecyChainRepository.</returns>
    public ProphecyChainRepository LoadProphecyChainRepository()
    {
        prophecychainrepository ??= new ProphecyChainRepository(logger, this);
        return prophecychainrepository;
    }

    /// <summary>
    /// Gets ProphecyTypeRepository data.
    /// </summary>
    /// <returns>repository of ProphecyTypeRepository.</returns>
    public ProphecyTypeRepository LoadProphecyTypeRepository()
    {
        prophecytyperepository ??= new ProphecyTypeRepository(logger, this);
        return prophecytyperepository;
    }

    /// <summary>
    /// Gets ShaperGuardiansRepository data.
    /// </summary>
    /// <returns>repository of ShaperGuardiansRepository.</returns>
    public ShaperGuardiansRepository LoadShaperGuardiansRepository()
    {
        shaperguardiansrepository ??= new ShaperGuardiansRepository(logger, this);
        return shaperguardiansrepository;
    }

    /// <summary>
    /// Gets EssencesRepository data.
    /// </summary>
    /// <returns>repository of EssencesRepository.</returns>
    public EssencesRepository LoadEssencesRepository()
    {
        essencesrepository ??= new EssencesRepository(logger, this);
        return essencesrepository;
    }

    /// <summary>
    /// Gets EssenceTypeRepository data.
    /// </summary>
    /// <returns>repository of EssenceTypeRepository.</returns>
    public EssenceTypeRepository LoadEssenceTypeRepository()
    {
        essencetyperepository ??= new EssenceTypeRepository(logger, this);
        return essencetyperepository;
    }

    /// <summary>
    /// Gets BreachBossLifeScalingPerLevelRepository data.
    /// </summary>
    /// <returns>repository of BreachBossLifeScalingPerLevelRepository.</returns>
    public BreachBossLifeScalingPerLevelRepository LoadBreachBossLifeScalingPerLevelRepository()
    {
        breachbosslifescalingperlevelrepository ??= new BreachBossLifeScalingPerLevelRepository(logger, this);
        return breachbosslifescalingperlevelrepository;
    }

    /// <summary>
    /// Gets BreachElementRepository data.
    /// </summary>
    /// <returns>repository of BreachElementRepository.</returns>
    public BreachElementRepository LoadBreachElementRepository()
    {
        breachelementrepository ??= new BreachElementRepository(logger, this);
        return breachelementrepository;
    }

    /// <summary>
    /// Gets BreachstoneUpgradesRepository data.
    /// </summary>
    /// <returns>repository of BreachstoneUpgradesRepository.</returns>
    public BreachstoneUpgradesRepository LoadBreachstoneUpgradesRepository()
    {
        breachstoneupgradesrepository ??= new BreachstoneUpgradesRepository(logger, this);
        return breachstoneupgradesrepository;
    }

    /// <summary>
    /// Gets HarbingersRepository data.
    /// </summary>
    /// <returns>repository of HarbingersRepository.</returns>
    public HarbingersRepository LoadHarbingersRepository()
    {
        harbingersrepository ??= new HarbingersRepository(logger, this);
        return harbingersrepository;
    }

    /// <summary>
    /// Gets PantheonPanelLayoutRepository data.
    /// </summary>
    /// <returns>repository of PantheonPanelLayoutRepository.</returns>
    public PantheonPanelLayoutRepository LoadPantheonPanelLayoutRepository()
    {
        pantheonpanellayoutrepository ??= new PantheonPanelLayoutRepository(logger, this);
        return pantheonpanellayoutrepository;
    }

    /// <summary>
    /// Gets PantheonSoulsRepository data.
    /// </summary>
    /// <returns>repository of PantheonSoulsRepository.</returns>
    public PantheonSoulsRepository LoadPantheonSoulsRepository()
    {
        pantheonsoulsrepository ??= new PantheonSoulsRepository(logger, this);
        return pantheonsoulsrepository;
    }

    /// <summary>
    /// Gets AbyssObjectsRepository data.
    /// </summary>
    /// <returns>repository of AbyssObjectsRepository.</returns>
    public AbyssObjectsRepository LoadAbyssObjectsRepository()
    {
        abyssobjectsrepository ??= new AbyssObjectsRepository(logger, this);
        return abyssobjectsrepository;
    }

    /// <summary>
    /// Gets ElderBossArenasRepository data.
    /// </summary>
    /// <returns>repository of ElderBossArenasRepository.</returns>
    public ElderBossArenasRepository LoadElderBossArenasRepository()
    {
        elderbossarenasrepository ??= new ElderBossArenasRepository(logger, this);
        return elderbossarenasrepository;
    }

    /// <summary>
    /// Gets ElderMapBossOverrideRepository data.
    /// </summary>
    /// <returns>repository of ElderMapBossOverrideRepository.</returns>
    public ElderMapBossOverrideRepository LoadElderMapBossOverrideRepository()
    {
        eldermapbossoverriderepository ??= new ElderMapBossOverrideRepository(logger, this);
        return eldermapbossoverriderepository;
    }

    /// <summary>
    /// Gets ElderGuardiansRepository data.
    /// </summary>
    /// <returns>repository of ElderGuardiansRepository.</returns>
    public ElderGuardiansRepository LoadElderGuardiansRepository()
    {
        elderguardiansrepository ??= new ElderGuardiansRepository(logger, this);
        return elderguardiansrepository;
    }

    /// <summary>
    /// Gets BestiaryCapturableMonstersRepository data.
    /// </summary>
    /// <returns>repository of BestiaryCapturableMonstersRepository.</returns>
    public BestiaryCapturableMonstersRepository LoadBestiaryCapturableMonstersRepository()
    {
        bestiarycapturablemonstersrepository ??= new BestiaryCapturableMonstersRepository(logger, this);
        return bestiarycapturablemonstersrepository;
    }

    /// <summary>
    /// Gets BestiaryEncountersRepository data.
    /// </summary>
    /// <returns>repository of BestiaryEncountersRepository.</returns>
    public BestiaryEncountersRepository LoadBestiaryEncountersRepository()
    {
        bestiaryencountersrepository ??= new BestiaryEncountersRepository(logger, this);
        return bestiaryencountersrepository;
    }

    /// <summary>
    /// Gets BestiaryFamiliesRepository data.
    /// </summary>
    /// <returns>repository of BestiaryFamiliesRepository.</returns>
    public BestiaryFamiliesRepository LoadBestiaryFamiliesRepository()
    {
        bestiaryfamiliesrepository ??= new BestiaryFamiliesRepository(logger, this);
        return bestiaryfamiliesrepository;
    }

    /// <summary>
    /// Gets BestiaryGenusRepository data.
    /// </summary>
    /// <returns>repository of BestiaryGenusRepository.</returns>
    public BestiaryGenusRepository LoadBestiaryGenusRepository()
    {
        bestiarygenusrepository ??= new BestiaryGenusRepository(logger, this);
        return bestiarygenusrepository;
    }

    /// <summary>
    /// Gets BestiaryGroupsRepository data.
    /// </summary>
    /// <returns>repository of BestiaryGroupsRepository.</returns>
    public BestiaryGroupsRepository LoadBestiaryGroupsRepository()
    {
        bestiarygroupsrepository ??= new BestiaryGroupsRepository(logger, this);
        return bestiarygroupsrepository;
    }

    /// <summary>
    /// Gets BestiaryNetsRepository data.
    /// </summary>
    /// <returns>repository of BestiaryNetsRepository.</returns>
    public BestiaryNetsRepository LoadBestiaryNetsRepository()
    {
        bestiarynetsrepository ??= new BestiaryNetsRepository(logger, this);
        return bestiarynetsrepository;
    }

    /// <summary>
    /// Gets BestiaryRecipeComponentRepository data.
    /// </summary>
    /// <returns>repository of BestiaryRecipeComponentRepository.</returns>
    public BestiaryRecipeComponentRepository LoadBestiaryRecipeComponentRepository()
    {
        bestiaryrecipecomponentrepository ??= new BestiaryRecipeComponentRepository(logger, this);
        return bestiaryrecipecomponentrepository;
    }

    /// <summary>
    /// Gets BestiaryRecipeCategoriesRepository data.
    /// </summary>
    /// <returns>repository of BestiaryRecipeCategoriesRepository.</returns>
    public BestiaryRecipeCategoriesRepository LoadBestiaryRecipeCategoriesRepository()
    {
        bestiaryrecipecategoriesrepository ??= new BestiaryRecipeCategoriesRepository(logger, this);
        return bestiaryrecipecategoriesrepository;
    }

    /// <summary>
    /// Gets BestiaryRecipesRepository data.
    /// </summary>
    /// <returns>repository of BestiaryRecipesRepository.</returns>
    public BestiaryRecipesRepository LoadBestiaryRecipesRepository()
    {
        bestiaryrecipesrepository ??= new BestiaryRecipesRepository(logger, this);
        return bestiaryrecipesrepository;
    }

    /// <summary>
    /// Gets ArchitectLifeScalingPerLevelRepository data.
    /// </summary>
    /// <returns>repository of ArchitectLifeScalingPerLevelRepository.</returns>
    public ArchitectLifeScalingPerLevelRepository LoadArchitectLifeScalingPerLevelRepository()
    {
        architectlifescalingperlevelrepository ??= new ArchitectLifeScalingPerLevelRepository(logger, this);
        return architectlifescalingperlevelrepository;
    }

    /// <summary>
    /// Gets IncursionArchitectRepository data.
    /// </summary>
    /// <returns>repository of IncursionArchitectRepository.</returns>
    public IncursionArchitectRepository LoadIncursionArchitectRepository()
    {
        incursionarchitectrepository ??= new IncursionArchitectRepository(logger, this);
        return incursionarchitectrepository;
    }

    /// <summary>
    /// Gets IncursionBracketsRepository data.
    /// </summary>
    /// <returns>repository of IncursionBracketsRepository.</returns>
    public IncursionBracketsRepository LoadIncursionBracketsRepository()
    {
        incursionbracketsrepository ??= new IncursionBracketsRepository(logger, this);
        return incursionbracketsrepository;
    }

    /// <summary>
    /// Gets IncursionChestRewardsRepository data.
    /// </summary>
    /// <returns>repository of IncursionChestRewardsRepository.</returns>
    public IncursionChestRewardsRepository LoadIncursionChestRewardsRepository()
    {
        incursionchestrewardsrepository ??= new IncursionChestRewardsRepository(logger, this);
        return incursionchestrewardsrepository;
    }

    /// <summary>
    /// Gets IncursionChestsRepository data.
    /// </summary>
    /// <returns>repository of IncursionChestsRepository.</returns>
    public IncursionChestsRepository LoadIncursionChestsRepository()
    {
        incursionchestsrepository ??= new IncursionChestsRepository(logger, this);
        return incursionchestsrepository;
    }

    /// <summary>
    /// Gets IncursionRoomBossFightEventsRepository data.
    /// </summary>
    /// <returns>repository of IncursionRoomBossFightEventsRepository.</returns>
    public IncursionRoomBossFightEventsRepository LoadIncursionRoomBossFightEventsRepository()
    {
        incursionroombossfighteventsrepository ??= new IncursionRoomBossFightEventsRepository(logger, this);
        return incursionroombossfighteventsrepository;
    }

    /// <summary>
    /// Gets IncursionRoomsRepository data.
    /// </summary>
    /// <returns>repository of IncursionRoomsRepository.</returns>
    public IncursionRoomsRepository LoadIncursionRoomsRepository()
    {
        incursionroomsrepository ??= new IncursionRoomsRepository(logger, this);
        return incursionroomsrepository;
    }

    /// <summary>
    /// Gets IncursionUniqueUpgradeComponentsRepository data.
    /// </summary>
    /// <returns>repository of IncursionUniqueUpgradeComponentsRepository.</returns>
    public IncursionUniqueUpgradeComponentsRepository LoadIncursionUniqueUpgradeComponentsRepository()
    {
        incursionuniqueupgradecomponentsrepository ??= new IncursionUniqueUpgradeComponentsRepository(logger, this);
        return incursionuniqueupgradecomponentsrepository;
    }

    /// <summary>
    /// Gets DelveAzuriteShopRepository data.
    /// </summary>
    /// <returns>repository of DelveAzuriteShopRepository.</returns>
    public DelveAzuriteShopRepository LoadDelveAzuriteShopRepository()
    {
        delveazuriteshoprepository ??= new DelveAzuriteShopRepository(logger, this);
        return delveazuriteshoprepository;
    }

    /// <summary>
    /// Gets DelveBiomesRepository data.
    /// </summary>
    /// <returns>repository of DelveBiomesRepository.</returns>
    public DelveBiomesRepository LoadDelveBiomesRepository()
    {
        delvebiomesrepository ??= new DelveBiomesRepository(logger, this);
        return delvebiomesrepository;
    }

    /// <summary>
    /// Gets DelveCatchupDepthsRepository data.
    /// </summary>
    /// <returns>repository of DelveCatchupDepthsRepository.</returns>
    public DelveCatchupDepthsRepository LoadDelveCatchupDepthsRepository()
    {
        delvecatchupdepthsrepository ??= new DelveCatchupDepthsRepository(logger, this);
        return delvecatchupdepthsrepository;
    }

    /// <summary>
    /// Gets DelveCraftingModifierDescriptionsRepository data.
    /// </summary>
    /// <returns>repository of DelveCraftingModifierDescriptionsRepository.</returns>
    public DelveCraftingModifierDescriptionsRepository LoadDelveCraftingModifierDescriptionsRepository()
    {
        delvecraftingmodifierdescriptionsrepository ??= new DelveCraftingModifierDescriptionsRepository(logger, this);
        return delvecraftingmodifierdescriptionsrepository;
    }

    /// <summary>
    /// Gets DelveCraftingModifiersRepository data.
    /// </summary>
    /// <returns>repository of DelveCraftingModifiersRepository.</returns>
    public DelveCraftingModifiersRepository LoadDelveCraftingModifiersRepository()
    {
        delvecraftingmodifiersrepository ??= new DelveCraftingModifiersRepository(logger, this);
        return delvecraftingmodifiersrepository;
    }

    /// <summary>
    /// Gets DelveCraftingTagsRepository data.
    /// </summary>
    /// <returns>repository of DelveCraftingTagsRepository.</returns>
    public DelveCraftingTagsRepository LoadDelveCraftingTagsRepository()
    {
        delvecraftingtagsrepository ??= new DelveCraftingTagsRepository(logger, this);
        return delvecraftingtagsrepository;
    }

    /// <summary>
    /// Gets DelveDynamiteRepository data.
    /// </summary>
    /// <returns>repository of DelveDynamiteRepository.</returns>
    public DelveDynamiteRepository LoadDelveDynamiteRepository()
    {
        delvedynamiterepository ??= new DelveDynamiteRepository(logger, this);
        return delvedynamiterepository;
    }

    /// <summary>
    /// Gets DelveFeaturesRepository data.
    /// </summary>
    /// <returns>repository of DelveFeaturesRepository.</returns>
    public DelveFeaturesRepository LoadDelveFeaturesRepository()
    {
        delvefeaturesrepository ??= new DelveFeaturesRepository(logger, this);
        return delvefeaturesrepository;
    }

    /// <summary>
    /// Gets DelveFlaresRepository data.
    /// </summary>
    /// <returns>repository of DelveFlaresRepository.</returns>
    public DelveFlaresRepository LoadDelveFlaresRepository()
    {
        delveflaresrepository ??= new DelveFlaresRepository(logger, this);
        return delveflaresrepository;
    }

    /// <summary>
    /// Gets DelveLevelScalingRepository data.
    /// </summary>
    /// <returns>repository of DelveLevelScalingRepository.</returns>
    public DelveLevelScalingRepository LoadDelveLevelScalingRepository()
    {
        delvelevelscalingrepository ??= new DelveLevelScalingRepository(logger, this);
        return delvelevelscalingrepository;
    }

    /// <summary>
    /// Gets DelveMonsterSpawnersRepository data.
    /// </summary>
    /// <returns>repository of DelveMonsterSpawnersRepository.</returns>
    public DelveMonsterSpawnersRepository LoadDelveMonsterSpawnersRepository()
    {
        delvemonsterspawnersrepository ??= new DelveMonsterSpawnersRepository(logger, this);
        return delvemonsterspawnersrepository;
    }

    /// <summary>
    /// Gets DelveResourcePerLevelRepository data.
    /// </summary>
    /// <returns>repository of DelveResourcePerLevelRepository.</returns>
    public DelveResourcePerLevelRepository LoadDelveResourcePerLevelRepository()
    {
        delveresourceperlevelrepository ??= new DelveResourcePerLevelRepository(logger, this);
        return delveresourceperlevelrepository;
    }

    /// <summary>
    /// Gets DelveRewardTierConstantsRepository data.
    /// </summary>
    /// <returns>repository of DelveRewardTierConstantsRepository.</returns>
    public DelveRewardTierConstantsRepository LoadDelveRewardTierConstantsRepository()
    {
        delverewardtierconstantsrepository ??= new DelveRewardTierConstantsRepository(logger, this);
        return delverewardtierconstantsrepository;
    }

    /// <summary>
    /// Gets DelveRoomsRepository data.
    /// </summary>
    /// <returns>repository of DelveRoomsRepository.</returns>
    public DelveRoomsRepository LoadDelveRoomsRepository()
    {
        delveroomsrepository ??= new DelveRoomsRepository(logger, this);
        return delveroomsrepository;
    }

    /// <summary>
    /// Gets DelveUpgradesRepository data.
    /// </summary>
    /// <returns>repository of DelveUpgradesRepository.</returns>
    public DelveUpgradesRepository LoadDelveUpgradesRepository()
    {
        delveupgradesrepository ??= new DelveUpgradesRepository(logger, this);
        return delveupgradesrepository;
    }

    /// <summary>
    /// Gets BetrayalChoiceActionsRepository data.
    /// </summary>
    /// <returns>repository of BetrayalChoiceActionsRepository.</returns>
    public BetrayalChoiceActionsRepository LoadBetrayalChoiceActionsRepository()
    {
        betrayalchoiceactionsrepository ??= new BetrayalChoiceActionsRepository(logger, this);
        return betrayalchoiceactionsrepository;
    }

    /// <summary>
    /// Gets BetrayalChoicesRepository data.
    /// </summary>
    /// <returns>repository of BetrayalChoicesRepository.</returns>
    public BetrayalChoicesRepository LoadBetrayalChoicesRepository()
    {
        betrayalchoicesrepository ??= new BetrayalChoicesRepository(logger, this);
        return betrayalchoicesrepository;
    }

    /// <summary>
    /// Gets BetrayalDialogueRepository data.
    /// </summary>
    /// <returns>repository of BetrayalDialogueRepository.</returns>
    public BetrayalDialogueRepository LoadBetrayalDialogueRepository()
    {
        betrayaldialoguerepository ??= new BetrayalDialogueRepository(logger, this);
        return betrayaldialoguerepository;
    }

    /// <summary>
    /// Gets BetrayalFortsRepository data.
    /// </summary>
    /// <returns>repository of BetrayalFortsRepository.</returns>
    public BetrayalFortsRepository LoadBetrayalFortsRepository()
    {
        betrayalfortsrepository ??= new BetrayalFortsRepository(logger, this);
        return betrayalfortsrepository;
    }

    /// <summary>
    /// Gets BetrayalJobsRepository data.
    /// </summary>
    /// <returns>repository of BetrayalJobsRepository.</returns>
    public BetrayalJobsRepository LoadBetrayalJobsRepository()
    {
        betrayaljobsrepository ??= new BetrayalJobsRepository(logger, this);
        return betrayaljobsrepository;
    }

    /// <summary>
    /// Gets BetrayalRanksRepository data.
    /// </summary>
    /// <returns>repository of BetrayalRanksRepository.</returns>
    public BetrayalRanksRepository LoadBetrayalRanksRepository()
    {
        betrayalranksrepository ??= new BetrayalRanksRepository(logger, this);
        return betrayalranksrepository;
    }

    /// <summary>
    /// Gets BetrayalRelationshipStateRepository data.
    /// </summary>
    /// <returns>repository of BetrayalRelationshipStateRepository.</returns>
    public BetrayalRelationshipStateRepository LoadBetrayalRelationshipStateRepository()
    {
        betrayalrelationshipstaterepository ??= new BetrayalRelationshipStateRepository(logger, this);
        return betrayalrelationshipstaterepository;
    }

    /// <summary>
    /// Gets BetrayalTargetJobAchievementsRepository data.
    /// </summary>
    /// <returns>repository of BetrayalTargetJobAchievementsRepository.</returns>
    public BetrayalTargetJobAchievementsRepository LoadBetrayalTargetJobAchievementsRepository()
    {
        betrayaltargetjobachievementsrepository ??= new BetrayalTargetJobAchievementsRepository(logger, this);
        return betrayaltargetjobachievementsrepository;
    }

    /// <summary>
    /// Gets BetrayalTargetLifeScalingPerLevelRepository data.
    /// </summary>
    /// <returns>repository of BetrayalTargetLifeScalingPerLevelRepository.</returns>
    public BetrayalTargetLifeScalingPerLevelRepository LoadBetrayalTargetLifeScalingPerLevelRepository()
    {
        betrayaltargetlifescalingperlevelrepository ??= new BetrayalTargetLifeScalingPerLevelRepository(logger, this);
        return betrayaltargetlifescalingperlevelrepository;
    }

    /// <summary>
    /// Gets BetrayalTargetsRepository data.
    /// </summary>
    /// <returns>repository of BetrayalTargetsRepository.</returns>
    public BetrayalTargetsRepository LoadBetrayalTargetsRepository()
    {
        betrayaltargetsrepository ??= new BetrayalTargetsRepository(logger, this);
        return betrayaltargetsrepository;
    }

    /// <summary>
    /// Gets BetrayalTraitorRewardsRepository data.
    /// </summary>
    /// <returns>repository of BetrayalTraitorRewardsRepository.</returns>
    public BetrayalTraitorRewardsRepository LoadBetrayalTraitorRewardsRepository()
    {
        betrayaltraitorrewardsrepository ??= new BetrayalTraitorRewardsRepository(logger, this);
        return betrayaltraitorrewardsrepository;
    }

    /// <summary>
    /// Gets BetrayalUpgradesRepository data.
    /// </summary>
    /// <returns>repository of BetrayalUpgradesRepository.</returns>
    public BetrayalUpgradesRepository LoadBetrayalUpgradesRepository()
    {
        betrayalupgradesrepository ??= new BetrayalUpgradesRepository(logger, this);
        return betrayalupgradesrepository;
    }

    /// <summary>
    /// Gets BetrayalWallLifeScalingPerLevelRepository data.
    /// </summary>
    /// <returns>repository of BetrayalWallLifeScalingPerLevelRepository.</returns>
    public BetrayalWallLifeScalingPerLevelRepository LoadBetrayalWallLifeScalingPerLevelRepository()
    {
        betrayalwalllifescalingperlevelrepository ??= new BetrayalWallLifeScalingPerLevelRepository(logger, this);
        return betrayalwalllifescalingperlevelrepository;
    }

    /// <summary>
    /// Gets SafehouseBYOCraftingRepository data.
    /// </summary>
    /// <returns>repository of SafehouseBYOCraftingRepository.</returns>
    public SafehouseBYOCraftingRepository LoadSafehouseBYOCraftingRepository()
    {
        safehousebyocraftingrepository ??= new SafehouseBYOCraftingRepository(logger, this);
        return safehousebyocraftingrepository;
    }

    /// <summary>
    /// Gets SafehouseCraftingSpreeTypeRepository data.
    /// </summary>
    /// <returns>repository of SafehouseCraftingSpreeTypeRepository.</returns>
    public SafehouseCraftingSpreeTypeRepository LoadSafehouseCraftingSpreeTypeRepository()
    {
        safehousecraftingspreetyperepository ??= new SafehouseCraftingSpreeTypeRepository(logger, this);
        return safehousecraftingspreetyperepository;
    }

    /// <summary>
    /// Gets SafehouseCraftingSpreeCurrenciesRepository data.
    /// </summary>
    /// <returns>repository of SafehouseCraftingSpreeCurrenciesRepository.</returns>
    public SafehouseCraftingSpreeCurrenciesRepository LoadSafehouseCraftingSpreeCurrenciesRepository()
    {
        safehousecraftingspreecurrenciesrepository ??= new SafehouseCraftingSpreeCurrenciesRepository(logger, this);
        return safehousecraftingspreecurrenciesrepository;
    }

    /// <summary>
    /// Gets ScarabsRepository data.
    /// </summary>
    /// <returns>repository of ScarabsRepository.</returns>
    public ScarabsRepository LoadScarabsRepository()
    {
        scarabsrepository ??= new ScarabsRepository(logger, this);
        return scarabsrepository;
    }

    /// <summary>
    /// Gets SynthesisAreasRepository data.
    /// </summary>
    /// <returns>repository of SynthesisAreasRepository.</returns>
    public SynthesisAreasRepository LoadSynthesisAreasRepository()
    {
        synthesisareasrepository ??= new SynthesisAreasRepository(logger, this);
        return synthesisareasrepository;
    }

    /// <summary>
    /// Gets SynthesisAreaSizeRepository data.
    /// </summary>
    /// <returns>repository of SynthesisAreaSizeRepository.</returns>
    public SynthesisAreaSizeRepository LoadSynthesisAreaSizeRepository()
    {
        synthesisareasizerepository ??= new SynthesisAreaSizeRepository(logger, this);
        return synthesisareasizerepository;
    }

    /// <summary>
    /// Gets SynthesisBonusesRepository data.
    /// </summary>
    /// <returns>repository of SynthesisBonusesRepository.</returns>
    public SynthesisBonusesRepository LoadSynthesisBonusesRepository()
    {
        synthesisbonusesrepository ??= new SynthesisBonusesRepository(logger, this);
        return synthesisbonusesrepository;
    }

    /// <summary>
    /// Gets SynthesisBracketsRepository data.
    /// </summary>
    /// <returns>repository of SynthesisBracketsRepository.</returns>
    public SynthesisBracketsRepository LoadSynthesisBracketsRepository()
    {
        synthesisbracketsrepository ??= new SynthesisBracketsRepository(logger, this);
        return synthesisbracketsrepository;
    }

    /// <summary>
    /// Gets SynthesisFragmentDialogueRepository data.
    /// </summary>
    /// <returns>repository of SynthesisFragmentDialogueRepository.</returns>
    public SynthesisFragmentDialogueRepository LoadSynthesisFragmentDialogueRepository()
    {
        synthesisfragmentdialoguerepository ??= new SynthesisFragmentDialogueRepository(logger, this);
        return synthesisfragmentdialoguerepository;
    }

    /// <summary>
    /// Gets SynthesisGlobalModsRepository data.
    /// </summary>
    /// <returns>repository of SynthesisGlobalModsRepository.</returns>
    public SynthesisGlobalModsRepository LoadSynthesisGlobalModsRepository()
    {
        synthesisglobalmodsrepository ??= new SynthesisGlobalModsRepository(logger, this);
        return synthesisglobalmodsrepository;
    }

    /// <summary>
    /// Gets SynthesisMonsterExperiencePerLevelRepository data.
    /// </summary>
    /// <returns>repository of SynthesisMonsterExperiencePerLevelRepository.</returns>
    public SynthesisMonsterExperiencePerLevelRepository LoadSynthesisMonsterExperiencePerLevelRepository()
    {
        synthesismonsterexperienceperlevelrepository ??= new SynthesisMonsterExperiencePerLevelRepository(logger, this);
        return synthesismonsterexperienceperlevelrepository;
    }

    /// <summary>
    /// Gets SynthesisRewardCategoriesRepository data.
    /// </summary>
    /// <returns>repository of SynthesisRewardCategoriesRepository.</returns>
    public SynthesisRewardCategoriesRepository LoadSynthesisRewardCategoriesRepository()
    {
        synthesisrewardcategoriesrepository ??= new SynthesisRewardCategoriesRepository(logger, this);
        return synthesisrewardcategoriesrepository;
    }

    /// <summary>
    /// Gets SynthesisRewardTypesRepository data.
    /// </summary>
    /// <returns>repository of SynthesisRewardTypesRepository.</returns>
    public SynthesisRewardTypesRepository LoadSynthesisRewardTypesRepository()
    {
        synthesisrewardtypesrepository ??= new SynthesisRewardTypesRepository(logger, this);
        return synthesisrewardtypesrepository;
    }

    /// <summary>
    /// Gets IncubatorsRepository data.
    /// </summary>
    /// <returns>repository of IncubatorsRepository.</returns>
    public IncubatorsRepository LoadIncubatorsRepository()
    {
        incubatorsrepository ??= new IncubatorsRepository(logger, this);
        return incubatorsrepository;
    }

    /// <summary>
    /// Gets LegionBalancePerLevelRepository data.
    /// </summary>
    /// <returns>repository of LegionBalancePerLevelRepository.</returns>
    public LegionBalancePerLevelRepository LoadLegionBalancePerLevelRepository()
    {
        legionbalanceperlevelrepository ??= new LegionBalancePerLevelRepository(logger, this);
        return legionbalanceperlevelrepository;
    }

    /// <summary>
    /// Gets LegionChestTypesRepository data.
    /// </summary>
    /// <returns>repository of LegionChestTypesRepository.</returns>
    public LegionChestTypesRepository LoadLegionChestTypesRepository()
    {
        legionchesttypesrepository ??= new LegionChestTypesRepository(logger, this);
        return legionchesttypesrepository;
    }

    /// <summary>
    /// Gets LegionChestCountsRepository data.
    /// </summary>
    /// <returns>repository of LegionChestCountsRepository.</returns>
    public LegionChestCountsRepository LoadLegionChestCountsRepository()
    {
        legionchestcountsrepository ??= new LegionChestCountsRepository(logger, this);
        return legionchestcountsrepository;
    }

    /// <summary>
    /// Gets LegionChestsRepository data.
    /// </summary>
    /// <returns>repository of LegionChestsRepository.</returns>
    public LegionChestsRepository LoadLegionChestsRepository()
    {
        legionchestsrepository ??= new LegionChestsRepository(logger, this);
        return legionchestsrepository;
    }

    /// <summary>
    /// Gets LegionFactionsRepository data.
    /// </summary>
    /// <returns>repository of LegionFactionsRepository.</returns>
    public LegionFactionsRepository LoadLegionFactionsRepository()
    {
        legionfactionsrepository ??= new LegionFactionsRepository(logger, this);
        return legionfactionsrepository;
    }

    /// <summary>
    /// Gets LegionMonsterCountsRepository data.
    /// </summary>
    /// <returns>repository of LegionMonsterCountsRepository.</returns>
    public LegionMonsterCountsRepository LoadLegionMonsterCountsRepository()
    {
        legionmonstercountsrepository ??= new LegionMonsterCountsRepository(logger, this);
        return legionmonstercountsrepository;
    }

    /// <summary>
    /// Gets LegionMonsterVarietiesRepository data.
    /// </summary>
    /// <returns>repository of LegionMonsterVarietiesRepository.</returns>
    public LegionMonsterVarietiesRepository LoadLegionMonsterVarietiesRepository()
    {
        legionmonstervarietiesrepository ??= new LegionMonsterVarietiesRepository(logger, this);
        return legionmonstervarietiesrepository;
    }

    /// <summary>
    /// Gets LegionRanksRepository data.
    /// </summary>
    /// <returns>repository of LegionRanksRepository.</returns>
    public LegionRanksRepository LoadLegionRanksRepository()
    {
        legionranksrepository ??= new LegionRanksRepository(logger, this);
        return legionranksrepository;
    }

    /// <summary>
    /// Gets LegionRewardTypeVisualsRepository data.
    /// </summary>
    /// <returns>repository of LegionRewardTypeVisualsRepository.</returns>
    public LegionRewardTypeVisualsRepository LoadLegionRewardTypeVisualsRepository()
    {
        legionrewardtypevisualsrepository ??= new LegionRewardTypeVisualsRepository(logger, this);
        return legionrewardtypevisualsrepository;
    }

    /// <summary>
    /// Gets BlightBalancePerLevelRepository data.
    /// </summary>
    /// <returns>repository of BlightBalancePerLevelRepository.</returns>
    public BlightBalancePerLevelRepository LoadBlightBalancePerLevelRepository()
    {
        blightbalanceperlevelrepository ??= new BlightBalancePerLevelRepository(logger, this);
        return blightbalanceperlevelrepository;
    }

    /// <summary>
    /// Gets BlightBossLifeScalingPerLevelRepository data.
    /// </summary>
    /// <returns>repository of BlightBossLifeScalingPerLevelRepository.</returns>
    public BlightBossLifeScalingPerLevelRepository LoadBlightBossLifeScalingPerLevelRepository()
    {
        blightbosslifescalingperlevelrepository ??= new BlightBossLifeScalingPerLevelRepository(logger, this);
        return blightbosslifescalingperlevelrepository;
    }

    /// <summary>
    /// Gets BlightChestTypesRepository data.
    /// </summary>
    /// <returns>repository of BlightChestTypesRepository.</returns>
    public BlightChestTypesRepository LoadBlightChestTypesRepository()
    {
        blightchesttypesrepository ??= new BlightChestTypesRepository(logger, this);
        return blightchesttypesrepository;
    }

    /// <summary>
    /// Gets BlightCraftingItemsRepository data.
    /// </summary>
    /// <returns>repository of BlightCraftingItemsRepository.</returns>
    public BlightCraftingItemsRepository LoadBlightCraftingItemsRepository()
    {
        blightcraftingitemsrepository ??= new BlightCraftingItemsRepository(logger, this);
        return blightcraftingitemsrepository;
    }

    /// <summary>
    /// Gets BlightCraftingRecipesRepository data.
    /// </summary>
    /// <returns>repository of BlightCraftingRecipesRepository.</returns>
    public BlightCraftingRecipesRepository LoadBlightCraftingRecipesRepository()
    {
        blightcraftingrecipesrepository ??= new BlightCraftingRecipesRepository(logger, this);
        return blightcraftingrecipesrepository;
    }

    /// <summary>
    /// Gets BlightCraftingResultsRepository data.
    /// </summary>
    /// <returns>repository of BlightCraftingResultsRepository.</returns>
    public BlightCraftingResultsRepository LoadBlightCraftingResultsRepository()
    {
        blightcraftingresultsrepository ??= new BlightCraftingResultsRepository(logger, this);
        return blightcraftingresultsrepository;
    }

    /// <summary>
    /// Gets BlightCraftingTypesRepository data.
    /// </summary>
    /// <returns>repository of BlightCraftingTypesRepository.</returns>
    public BlightCraftingTypesRepository LoadBlightCraftingTypesRepository()
    {
        blightcraftingtypesrepository ??= new BlightCraftingTypesRepository(logger, this);
        return blightcraftingtypesrepository;
    }

    /// <summary>
    /// Gets BlightCraftingUniquesRepository data.
    /// </summary>
    /// <returns>repository of BlightCraftingUniquesRepository.</returns>
    public BlightCraftingUniquesRepository LoadBlightCraftingUniquesRepository()
    {
        blightcraftinguniquesrepository ??= new BlightCraftingUniquesRepository(logger, this);
        return blightcraftinguniquesrepository;
    }

    /// <summary>
    /// Gets BlightedSporeAurasRepository data.
    /// </summary>
    /// <returns>repository of BlightedSporeAurasRepository.</returns>
    public BlightedSporeAurasRepository LoadBlightedSporeAurasRepository()
    {
        blightedsporeaurasrepository ??= new BlightedSporeAurasRepository(logger, this);
        return blightedsporeaurasrepository;
    }

    /// <summary>
    /// Gets BlightEncounterTypesRepository data.
    /// </summary>
    /// <returns>repository of BlightEncounterTypesRepository.</returns>
    public BlightEncounterTypesRepository LoadBlightEncounterTypesRepository()
    {
        blightencountertypesrepository ??= new BlightEncounterTypesRepository(logger, this);
        return blightencountertypesrepository;
    }

    /// <summary>
    /// Gets BlightEncounterWavesRepository data.
    /// </summary>
    /// <returns>repository of BlightEncounterWavesRepository.</returns>
    public BlightEncounterWavesRepository LoadBlightEncounterWavesRepository()
    {
        blightencounterwavesrepository ??= new BlightEncounterWavesRepository(logger, this);
        return blightencounterwavesrepository;
    }

    /// <summary>
    /// Gets BlightRewardTypesRepository data.
    /// </summary>
    /// <returns>repository of BlightRewardTypesRepository.</returns>
    public BlightRewardTypesRepository LoadBlightRewardTypesRepository()
    {
        blightrewardtypesrepository ??= new BlightRewardTypesRepository(logger, this);
        return blightrewardtypesrepository;
    }

    /// <summary>
    /// Gets BlightTopologiesRepository data.
    /// </summary>
    /// <returns>repository of BlightTopologiesRepository.</returns>
    public BlightTopologiesRepository LoadBlightTopologiesRepository()
    {
        blighttopologiesrepository ??= new BlightTopologiesRepository(logger, this);
        return blighttopologiesrepository;
    }

    /// <summary>
    /// Gets BlightTopologyNodesRepository data.
    /// </summary>
    /// <returns>repository of BlightTopologyNodesRepository.</returns>
    public BlightTopologyNodesRepository LoadBlightTopologyNodesRepository()
    {
        blighttopologynodesrepository ??= new BlightTopologyNodesRepository(logger, this);
        return blighttopologynodesrepository;
    }

    /// <summary>
    /// Gets BlightTowerAurasRepository data.
    /// </summary>
    /// <returns>repository of BlightTowerAurasRepository.</returns>
    public BlightTowerAurasRepository LoadBlightTowerAurasRepository()
    {
        blighttoweraurasrepository ??= new BlightTowerAurasRepository(logger, this);
        return blighttoweraurasrepository;
    }

    /// <summary>
    /// Gets BlightTowersRepository data.
    /// </summary>
    /// <returns>repository of BlightTowersRepository.</returns>
    public BlightTowersRepository LoadBlightTowersRepository()
    {
        blighttowersrepository ??= new BlightTowersRepository(logger, this);
        return blighttowersrepository;
    }

    /// <summary>
    /// Gets BlightTowersPerLevelRepository data.
    /// </summary>
    /// <returns>repository of BlightTowersPerLevelRepository.</returns>
    public BlightTowersPerLevelRepository LoadBlightTowersPerLevelRepository()
    {
        blighttowersperlevelrepository ??= new BlightTowersPerLevelRepository(logger, this);
        return blighttowersperlevelrepository;
    }

    /// <summary>
    /// Gets AtlasExileBossArenasRepository data.
    /// </summary>
    /// <returns>repository of AtlasExileBossArenasRepository.</returns>
    public AtlasExileBossArenasRepository LoadAtlasExileBossArenasRepository()
    {
        atlasexilebossarenasrepository ??= new AtlasExileBossArenasRepository(logger, this);
        return atlasexilebossarenasrepository;
    }

    /// <summary>
    /// Gets AtlasExileInfluenceRepository data.
    /// </summary>
    /// <returns>repository of AtlasExileInfluenceRepository.</returns>
    public AtlasExileInfluenceRepository LoadAtlasExileInfluenceRepository()
    {
        atlasexileinfluencerepository ??= new AtlasExileInfluenceRepository(logger, this);
        return atlasexileinfluencerepository;
    }

    /// <summary>
    /// Gets AtlasExilesRepository data.
    /// </summary>
    /// <returns>repository of AtlasExilesRepository.</returns>
    public AtlasExilesRepository LoadAtlasExilesRepository()
    {
        atlasexilesrepository ??= new AtlasExilesRepository(logger, this);
        return atlasexilesrepository;
    }

    /// <summary>
    /// Gets AlternateQualityCurrencyDecayFactorsRepository data.
    /// </summary>
    /// <returns>repository of AlternateQualityCurrencyDecayFactorsRepository.</returns>
    public AlternateQualityCurrencyDecayFactorsRepository LoadAlternateQualityCurrencyDecayFactorsRepository()
    {
        alternatequalitycurrencydecayfactorsrepository ??= new AlternateQualityCurrencyDecayFactorsRepository(logger, this);
        return alternatequalitycurrencydecayfactorsrepository;
    }

    /// <summary>
    /// Gets AlternateQualityTypesRepository data.
    /// </summary>
    /// <returns>repository of AlternateQualityTypesRepository.</returns>
    public AlternateQualityTypesRepository LoadAlternateQualityTypesRepository()
    {
        alternatequalitytypesrepository ??= new AlternateQualityTypesRepository(logger, this);
        return alternatequalitytypesrepository;
    }

    /// <summary>
    /// Gets MetamorphLifeScalingPerLevelRepository data.
    /// </summary>
    /// <returns>repository of MetamorphLifeScalingPerLevelRepository.</returns>
    public MetamorphLifeScalingPerLevelRepository LoadMetamorphLifeScalingPerLevelRepository()
    {
        metamorphlifescalingperlevelrepository ??= new MetamorphLifeScalingPerLevelRepository(logger, this);
        return metamorphlifescalingperlevelrepository;
    }

    /// <summary>
    /// Gets MetamorphosisMetaMonstersRepository data.
    /// </summary>
    /// <returns>repository of MetamorphosisMetaMonstersRepository.</returns>
    public MetamorphosisMetaMonstersRepository LoadMetamorphosisMetaMonstersRepository()
    {
        metamorphosismetamonstersrepository ??= new MetamorphosisMetaMonstersRepository(logger, this);
        return metamorphosismetamonstersrepository;
    }

    /// <summary>
    /// Gets MetamorphosisMetaSkillsRepository data.
    /// </summary>
    /// <returns>repository of MetamorphosisMetaSkillsRepository.</returns>
    public MetamorphosisMetaSkillsRepository LoadMetamorphosisMetaSkillsRepository()
    {
        metamorphosismetaskillsrepository ??= new MetamorphosisMetaSkillsRepository(logger, this);
        return metamorphosismetaskillsrepository;
    }

    /// <summary>
    /// Gets MetamorphosisMetaSkillTypesRepository data.
    /// </summary>
    /// <returns>repository of MetamorphosisMetaSkillTypesRepository.</returns>
    public MetamorphosisMetaSkillTypesRepository LoadMetamorphosisMetaSkillTypesRepository()
    {
        metamorphosismetaskilltypesrepository ??= new MetamorphosisMetaSkillTypesRepository(logger, this);
        return metamorphosismetaskilltypesrepository;
    }

    /// <summary>
    /// Gets MetamorphosisRewardTypeItemsClientRepository data.
    /// </summary>
    /// <returns>repository of MetamorphosisRewardTypeItemsClientRepository.</returns>
    public MetamorphosisRewardTypeItemsClientRepository LoadMetamorphosisRewardTypeItemsClientRepository()
    {
        metamorphosisrewardtypeitemsclientrepository ??= new MetamorphosisRewardTypeItemsClientRepository(logger, this);
        return metamorphosisrewardtypeitemsclientrepository;
    }

    /// <summary>
    /// Gets MetamorphosisRewardTypesRepository data.
    /// </summary>
    /// <returns>repository of MetamorphosisRewardTypesRepository.</returns>
    public MetamorphosisRewardTypesRepository LoadMetamorphosisRewardTypesRepository()
    {
        metamorphosisrewardtypesrepository ??= new MetamorphosisRewardTypesRepository(logger, this);
        return metamorphosisrewardtypesrepository;
    }

    /// <summary>
    /// Gets MetamorphosisScalingRepository data.
    /// </summary>
    /// <returns>repository of MetamorphosisScalingRepository.</returns>
    public MetamorphosisScalingRepository LoadMetamorphosisScalingRepository()
    {
        metamorphosisscalingrepository ??= new MetamorphosisScalingRepository(logger, this);
        return metamorphosisscalingrepository;
    }

    /// <summary>
    /// Gets AfflictionBalancePerLevelRepository data.
    /// </summary>
    /// <returns>repository of AfflictionBalancePerLevelRepository.</returns>
    public AfflictionBalancePerLevelRepository LoadAfflictionBalancePerLevelRepository()
    {
        afflictionbalanceperlevelrepository ??= new AfflictionBalancePerLevelRepository(logger, this);
        return afflictionbalanceperlevelrepository;
    }

    /// <summary>
    /// Gets AfflictionEndgameWaveModsRepository data.
    /// </summary>
    /// <returns>repository of AfflictionEndgameWaveModsRepository.</returns>
    public AfflictionEndgameWaveModsRepository LoadAfflictionEndgameWaveModsRepository()
    {
        afflictionendgamewavemodsrepository ??= new AfflictionEndgameWaveModsRepository(logger, this);
        return afflictionendgamewavemodsrepository;
    }

    /// <summary>
    /// Gets AfflictionFixedModsRepository data.
    /// </summary>
    /// <returns>repository of AfflictionFixedModsRepository.</returns>
    public AfflictionFixedModsRepository LoadAfflictionFixedModsRepository()
    {
        afflictionfixedmodsrepository ??= new AfflictionFixedModsRepository(logger, this);
        return afflictionfixedmodsrepository;
    }

    /// <summary>
    /// Gets AfflictionRandomModCategoriesRepository data.
    /// </summary>
    /// <returns>repository of AfflictionRandomModCategoriesRepository.</returns>
    public AfflictionRandomModCategoriesRepository LoadAfflictionRandomModCategoriesRepository()
    {
        afflictionrandommodcategoriesrepository ??= new AfflictionRandomModCategoriesRepository(logger, this);
        return afflictionrandommodcategoriesrepository;
    }

    /// <summary>
    /// Gets AfflictionRewardMapModsRepository data.
    /// </summary>
    /// <returns>repository of AfflictionRewardMapModsRepository.</returns>
    public AfflictionRewardMapModsRepository LoadAfflictionRewardMapModsRepository()
    {
        afflictionrewardmapmodsrepository ??= new AfflictionRewardMapModsRepository(logger, this);
        return afflictionrewardmapmodsrepository;
    }

    /// <summary>
    /// Gets AfflictionRewardTypeVisualsRepository data.
    /// </summary>
    /// <returns>repository of AfflictionRewardTypeVisualsRepository.</returns>
    public AfflictionRewardTypeVisualsRepository LoadAfflictionRewardTypeVisualsRepository()
    {
        afflictionrewardtypevisualsrepository ??= new AfflictionRewardTypeVisualsRepository(logger, this);
        return afflictionrewardtypevisualsrepository;
    }

    /// <summary>
    /// Gets AfflictionSplitDemonsRepository data.
    /// </summary>
    /// <returns>repository of AfflictionSplitDemonsRepository.</returns>
    public AfflictionSplitDemonsRepository LoadAfflictionSplitDemonsRepository()
    {
        afflictionsplitdemonsrepository ??= new AfflictionSplitDemonsRepository(logger, this);
        return afflictionsplitdemonsrepository;
    }

    /// <summary>
    /// Gets AfflictionStartDialogueRepository data.
    /// </summary>
    /// <returns>repository of AfflictionStartDialogueRepository.</returns>
    public AfflictionStartDialogueRepository LoadAfflictionStartDialogueRepository()
    {
        afflictionstartdialoguerepository ??= new AfflictionStartDialogueRepository(logger, this);
        return afflictionstartdialoguerepository;
    }

    /// <summary>
    /// Gets HarvestCraftOptionIconsRepository data.
    /// </summary>
    /// <returns>repository of HarvestCraftOptionIconsRepository.</returns>
    public HarvestCraftOptionIconsRepository LoadHarvestCraftOptionIconsRepository()
    {
        harvestcraftoptioniconsrepository ??= new HarvestCraftOptionIconsRepository(logger, this);
        return harvestcraftoptioniconsrepository;
    }

    /// <summary>
    /// Gets HarvestCraftOptionsRepository data.
    /// </summary>
    /// <returns>repository of HarvestCraftOptionsRepository.</returns>
    public HarvestCraftOptionsRepository LoadHarvestCraftOptionsRepository()
    {
        harvestcraftoptionsrepository ??= new HarvestCraftOptionsRepository(logger, this);
        return harvestcraftoptionsrepository;
    }

    /// <summary>
    /// Gets HarvestCraftTiersRepository data.
    /// </summary>
    /// <returns>repository of HarvestCraftTiersRepository.</returns>
    public HarvestCraftTiersRepository LoadHarvestCraftTiersRepository()
    {
        harvestcrafttiersrepository ??= new HarvestCraftTiersRepository(logger, this);
        return harvestcrafttiersrepository;
    }

    /// <summary>
    /// Gets HarvestCraftFiltersRepository data.
    /// </summary>
    /// <returns>repository of HarvestCraftFiltersRepository.</returns>
    public HarvestCraftFiltersRepository LoadHarvestCraftFiltersRepository()
    {
        harvestcraftfiltersrepository ??= new HarvestCraftFiltersRepository(logger, this);
        return harvestcraftfiltersrepository;
    }

    /// <summary>
    /// Gets HarvestDurabilityRepository data.
    /// </summary>
    /// <returns>repository of HarvestDurabilityRepository.</returns>
    public HarvestDurabilityRepository LoadHarvestDurabilityRepository()
    {
        harvestdurabilityrepository ??= new HarvestDurabilityRepository(logger, this);
        return harvestdurabilityrepository;
    }

    /// <summary>
    /// Gets HarvestEncounterScalingRepository data.
    /// </summary>
    /// <returns>repository of HarvestEncounterScalingRepository.</returns>
    public HarvestEncounterScalingRepository LoadHarvestEncounterScalingRepository()
    {
        harvestencounterscalingrepository ??= new HarvestEncounterScalingRepository(logger, this);
        return harvestencounterscalingrepository;
    }

    /// <summary>
    /// Gets HarvestInfrastructureRepository data.
    /// </summary>
    /// <returns>repository of HarvestInfrastructureRepository.</returns>
    public HarvestInfrastructureRepository LoadHarvestInfrastructureRepository()
    {
        harvestinfrastructurerepository ??= new HarvestInfrastructureRepository(logger, this);
        return harvestinfrastructurerepository;
    }

    /// <summary>
    /// Gets HarvestObjectsRepository data.
    /// </summary>
    /// <returns>repository of HarvestObjectsRepository.</returns>
    public HarvestObjectsRepository LoadHarvestObjectsRepository()
    {
        harvestobjectsrepository ??= new HarvestObjectsRepository(logger, this);
        return harvestobjectsrepository;
    }

    /// <summary>
    /// Gets HarvestPerLevelValuesRepository data.
    /// </summary>
    /// <returns>repository of HarvestPerLevelValuesRepository.</returns>
    public HarvestPerLevelValuesRepository LoadHarvestPerLevelValuesRepository()
    {
        harvestperlevelvaluesrepository ??= new HarvestPerLevelValuesRepository(logger, this);
        return harvestperlevelvaluesrepository;
    }

    /// <summary>
    /// Gets HarvestPlantBoostersRepository data.
    /// </summary>
    /// <returns>repository of HarvestPlantBoostersRepository.</returns>
    public HarvestPlantBoostersRepository LoadHarvestPlantBoostersRepository()
    {
        harvestplantboostersrepository ??= new HarvestPlantBoostersRepository(logger, this);
        return harvestplantboostersrepository;
    }

    /// <summary>
    /// Gets HarvestLifeScalingPerLevelRepository data.
    /// </summary>
    /// <returns>repository of HarvestLifeScalingPerLevelRepository.</returns>
    public HarvestLifeScalingPerLevelRepository LoadHarvestLifeScalingPerLevelRepository()
    {
        harvestlifescalingperlevelrepository ??= new HarvestLifeScalingPerLevelRepository(logger, this);
        return harvestlifescalingperlevelrepository;
    }

    /// <summary>
    /// Gets HarvestSeedsRepository data.
    /// </summary>
    /// <returns>repository of HarvestSeedsRepository.</returns>
    public HarvestSeedsRepository LoadHarvestSeedsRepository()
    {
        harvestseedsrepository ??= new HarvestSeedsRepository(logger, this);
        return harvestseedsrepository;
    }

    /// <summary>
    /// Gets HarvestSeedItemsRepository data.
    /// </summary>
    /// <returns>repository of HarvestSeedItemsRepository.</returns>
    public HarvestSeedItemsRepository LoadHarvestSeedItemsRepository()
    {
        harvestseeditemsrepository ??= new HarvestSeedItemsRepository(logger, this);
        return harvestseeditemsrepository;
    }

    /// <summary>
    /// Gets HarvestSeedTypesRepository data.
    /// </summary>
    /// <returns>repository of HarvestSeedTypesRepository.</returns>
    public HarvestSeedTypesRepository LoadHarvestSeedTypesRepository()
    {
        harvestseedtypesrepository ??= new HarvestSeedTypesRepository(logger, this);
        return harvestseedtypesrepository;
    }

    /// <summary>
    /// Gets HarvestSpecialCraftCostsRepository data.
    /// </summary>
    /// <returns>repository of HarvestSpecialCraftCostsRepository.</returns>
    public HarvestSpecialCraftCostsRepository LoadHarvestSpecialCraftCostsRepository()
    {
        harvestspecialcraftcostsrepository ??= new HarvestSpecialCraftCostsRepository(logger, this);
        return harvestspecialcraftcostsrepository;
    }

    /// <summary>
    /// Gets HarvestSpecialCraftOptionsRepository data.
    /// </summary>
    /// <returns>repository of HarvestSpecialCraftOptionsRepository.</returns>
    public HarvestSpecialCraftOptionsRepository LoadHarvestSpecialCraftOptionsRepository()
    {
        harvestspecialcraftoptionsrepository ??= new HarvestSpecialCraftOptionsRepository(logger, this);
        return harvestspecialcraftoptionsrepository;
    }

    /// <summary>
    /// Gets HeistAreaFormationLayoutRepository data.
    /// </summary>
    /// <returns>repository of HeistAreaFormationLayoutRepository.</returns>
    public HeistAreaFormationLayoutRepository LoadHeistAreaFormationLayoutRepository()
    {
        heistareaformationlayoutrepository ??= new HeistAreaFormationLayoutRepository(logger, this);
        return heistareaformationlayoutrepository;
    }

    /// <summary>
    /// Gets HeistAreasRepository data.
    /// </summary>
    /// <returns>repository of HeistAreasRepository.</returns>
    public HeistAreasRepository LoadHeistAreasRepository()
    {
        heistareasrepository ??= new HeistAreasRepository(logger, this);
        return heistareasrepository;
    }

    /// <summary>
    /// Gets HeistBalancePerLevelRepository data.
    /// </summary>
    /// <returns>repository of HeistBalancePerLevelRepository.</returns>
    public HeistBalancePerLevelRepository LoadHeistBalancePerLevelRepository()
    {
        heistbalanceperlevelrepository ??= new HeistBalancePerLevelRepository(logger, this);
        return heistbalanceperlevelrepository;
    }

    /// <summary>
    /// Gets HeistChestRewardTypesRepository data.
    /// </summary>
    /// <returns>repository of HeistChestRewardTypesRepository.</returns>
    public HeistChestRewardTypesRepository LoadHeistChestRewardTypesRepository()
    {
        heistchestrewardtypesrepository ??= new HeistChestRewardTypesRepository(logger, this);
        return heistchestrewardtypesrepository;
    }

    /// <summary>
    /// Gets HeistChestsRepository data.
    /// </summary>
    /// <returns>repository of HeistChestsRepository.</returns>
    public HeistChestsRepository LoadHeistChestsRepository()
    {
        heistchestsrepository ??= new HeistChestsRepository(logger, this);
        return heistchestsrepository;
    }

    /// <summary>
    /// Gets HeistChokepointFormationRepository data.
    /// </summary>
    /// <returns>repository of HeistChokepointFormationRepository.</returns>
    public HeistChokepointFormationRepository LoadHeistChokepointFormationRepository()
    {
        heistchokepointformationrepository ??= new HeistChokepointFormationRepository(logger, this);
        return heistchokepointformationrepository;
    }

    /// <summary>
    /// Gets HeistConstantsRepository data.
    /// </summary>
    /// <returns>repository of HeistConstantsRepository.</returns>
    public HeistConstantsRepository LoadHeistConstantsRepository()
    {
        heistconstantsrepository ??= new HeistConstantsRepository(logger, this);
        return heistconstantsrepository;
    }

    /// <summary>
    /// Gets HeistContractsRepository data.
    /// </summary>
    /// <returns>repository of HeistContractsRepository.</returns>
    public HeistContractsRepository LoadHeistContractsRepository()
    {
        heistcontractsrepository ??= new HeistContractsRepository(logger, this);
        return heistcontractsrepository;
    }

    /// <summary>
    /// Gets HeistDoodadNPCsRepository data.
    /// </summary>
    /// <returns>repository of HeistDoodadNPCsRepository.</returns>
    public HeistDoodadNPCsRepository LoadHeistDoodadNPCsRepository()
    {
        heistdoodadnpcsrepository ??= new HeistDoodadNPCsRepository(logger, this);
        return heistdoodadnpcsrepository;
    }

    /// <summary>
    /// Gets HeistDoorsRepository data.
    /// </summary>
    /// <returns>repository of HeistDoorsRepository.</returns>
    public HeistDoorsRepository LoadHeistDoorsRepository()
    {
        heistdoorsrepository ??= new HeistDoorsRepository(logger, this);
        return heistdoorsrepository;
    }

    /// <summary>
    /// Gets HeistEquipmentRepository data.
    /// </summary>
    /// <returns>repository of HeistEquipmentRepository.</returns>
    public HeistEquipmentRepository LoadHeistEquipmentRepository()
    {
        heistequipmentrepository ??= new HeistEquipmentRepository(logger, this);
        return heistequipmentrepository;
    }

    /// <summary>
    /// Gets HeistGenerationRepository data.
    /// </summary>
    /// <returns>repository of HeistGenerationRepository.</returns>
    public HeistGenerationRepository LoadHeistGenerationRepository()
    {
        heistgenerationrepository ??= new HeistGenerationRepository(logger, this);
        return heistgenerationrepository;
    }

    /// <summary>
    /// Gets HeistIntroAreasRepository data.
    /// </summary>
    /// <returns>repository of HeistIntroAreasRepository.</returns>
    public HeistIntroAreasRepository LoadHeistIntroAreasRepository()
    {
        heistintroareasrepository ??= new HeistIntroAreasRepository(logger, this);
        return heistintroareasrepository;
    }

    /// <summary>
    /// Gets HeistJobsRepository data.
    /// </summary>
    /// <returns>repository of HeistJobsRepository.</returns>
    public HeistJobsRepository LoadHeistJobsRepository()
    {
        heistjobsrepository ??= new HeistJobsRepository(logger, this);
        return heistjobsrepository;
    }

    /// <summary>
    /// Gets HeistJobsExperiencePerLevelRepository data.
    /// </summary>
    /// <returns>repository of HeistJobsExperiencePerLevelRepository.</returns>
    public HeistJobsExperiencePerLevelRepository LoadHeistJobsExperiencePerLevelRepository()
    {
        heistjobsexperienceperlevelrepository ??= new HeistJobsExperiencePerLevelRepository(logger, this);
        return heistjobsexperienceperlevelrepository;
    }

    /// <summary>
    /// Gets HeistLockTypeRepository data.
    /// </summary>
    /// <returns>repository of HeistLockTypeRepository.</returns>
    public HeistLockTypeRepository LoadHeistLockTypeRepository()
    {
        heistlocktyperepository ??= new HeistLockTypeRepository(logger, this);
        return heistlocktyperepository;
    }

    /// <summary>
    /// Gets HeistNPCAurasRepository data.
    /// </summary>
    /// <returns>repository of HeistNPCAurasRepository.</returns>
    public HeistNPCAurasRepository LoadHeistNPCAurasRepository()
    {
        heistnpcaurasrepository ??= new HeistNPCAurasRepository(logger, this);
        return heistnpcaurasrepository;
    }

    /// <summary>
    /// Gets HeistNPCBlueprintTypesRepository data.
    /// </summary>
    /// <returns>repository of HeistNPCBlueprintTypesRepository.</returns>
    public HeistNPCBlueprintTypesRepository LoadHeistNPCBlueprintTypesRepository()
    {
        heistnpcblueprinttypesrepository ??= new HeistNPCBlueprintTypesRepository(logger, this);
        return heistnpcblueprinttypesrepository;
    }

    /// <summary>
    /// Gets HeistNPCDialogueRepository data.
    /// </summary>
    /// <returns>repository of HeistNPCDialogueRepository.</returns>
    public HeistNPCDialogueRepository LoadHeistNPCDialogueRepository()
    {
        heistnpcdialoguerepository ??= new HeistNPCDialogueRepository(logger, this);
        return heistnpcdialoguerepository;
    }

    /// <summary>
    /// Gets HeistNPCsRepository data.
    /// </summary>
    /// <returns>repository of HeistNPCsRepository.</returns>
    public HeistNPCsRepository LoadHeistNPCsRepository()
    {
        heistnpcsrepository ??= new HeistNPCsRepository(logger, this);
        return heistnpcsrepository;
    }

    /// <summary>
    /// Gets HeistNPCStatsRepository data.
    /// </summary>
    /// <returns>repository of HeistNPCStatsRepository.</returns>
    public HeistNPCStatsRepository LoadHeistNPCStatsRepository()
    {
        heistnpcstatsrepository ??= new HeistNPCStatsRepository(logger, this);
        return heistnpcstatsrepository;
    }

    /// <summary>
    /// Gets HeistObjectivesRepository data.
    /// </summary>
    /// <returns>repository of HeistObjectivesRepository.</returns>
    public HeistObjectivesRepository LoadHeistObjectivesRepository()
    {
        heistobjectivesrepository ??= new HeistObjectivesRepository(logger, this);
        return heistobjectivesrepository;
    }

    /// <summary>
    /// Gets HeistObjectiveValueDescriptionsRepository data.
    /// </summary>
    /// <returns>repository of HeistObjectiveValueDescriptionsRepository.</returns>
    public HeistObjectiveValueDescriptionsRepository LoadHeistObjectiveValueDescriptionsRepository()
    {
        heistobjectivevaluedescriptionsrepository ??= new HeistObjectiveValueDescriptionsRepository(logger, this);
        return heistobjectivevaluedescriptionsrepository;
    }

    /// <summary>
    /// Gets HeistPatrolPacksRepository data.
    /// </summary>
    /// <returns>repository of HeistPatrolPacksRepository.</returns>
    public HeistPatrolPacksRepository LoadHeistPatrolPacksRepository()
    {
        heistpatrolpacksrepository ??= new HeistPatrolPacksRepository(logger, this);
        return heistpatrolpacksrepository;
    }

    /// <summary>
    /// Gets HeistQuestContractsRepository data.
    /// </summary>
    /// <returns>repository of HeistQuestContractsRepository.</returns>
    public HeistQuestContractsRepository LoadHeistQuestContractsRepository()
    {
        heistquestcontractsrepository ??= new HeistQuestContractsRepository(logger, this);
        return heistquestcontractsrepository;
    }

    /// <summary>
    /// Gets HeistRevealingNPCsRepository data.
    /// </summary>
    /// <returns>repository of HeistRevealingNPCsRepository.</returns>
    public HeistRevealingNPCsRepository LoadHeistRevealingNPCsRepository()
    {
        heistrevealingnpcsrepository ??= new HeistRevealingNPCsRepository(logger, this);
        return heistrevealingnpcsrepository;
    }

    /// <summary>
    /// Gets HeistRoomsRepository data.
    /// </summary>
    /// <returns>repository of HeistRoomsRepository.</returns>
    public HeistRoomsRepository LoadHeistRoomsRepository()
    {
        heistroomsrepository ??= new HeistRoomsRepository(logger, this);
        return heistroomsrepository;
    }

    /// <summary>
    /// Gets HeistValueScalingRepository data.
    /// </summary>
    /// <returns>repository of HeistValueScalingRepository.</returns>
    public HeistValueScalingRepository LoadHeistValueScalingRepository()
    {
        heistvaluescalingrepository ??= new HeistValueScalingRepository(logger, this);
        return heistvaluescalingrepository;
    }

    /// <summary>
    /// Gets InfluenceModUpgradesRepository data.
    /// </summary>
    /// <returns>repository of InfluenceModUpgradesRepository.</returns>
    public InfluenceModUpgradesRepository LoadInfluenceModUpgradesRepository()
    {
        influencemodupgradesrepository ??= new InfluenceModUpgradesRepository(logger, this);
        return influencemodupgradesrepository;
    }

    /// <summary>
    /// Gets MavenDialogRepository data.
    /// </summary>
    /// <returns>repository of MavenDialogRepository.</returns>
    public MavenDialogRepository LoadMavenDialogRepository()
    {
        mavendialogrepository ??= new MavenDialogRepository(logger, this);
        return mavendialogrepository;
    }

    /// <summary>
    /// Gets AtlasSkillGraphsRepository data.
    /// </summary>
    /// <returns>repository of AtlasSkillGraphsRepository.</returns>
    public AtlasSkillGraphsRepository LoadAtlasSkillGraphsRepository()
    {
        atlasskillgraphsrepository ??= new AtlasSkillGraphsRepository(logger, this);
        return atlasskillgraphsrepository;
    }

    /// <summary>
    /// Gets MavenFightsRepository data.
    /// </summary>
    /// <returns>repository of MavenFightsRepository.</returns>
    public MavenFightsRepository LoadMavenFightsRepository()
    {
        mavenfightsrepository ??= new MavenFightsRepository(logger, this);
        return mavenfightsrepository;
    }

    /// <summary>
    /// Gets MavenJewelRadiusKeystonesRepository data.
    /// </summary>
    /// <returns>repository of MavenJewelRadiusKeystonesRepository.</returns>
    public MavenJewelRadiusKeystonesRepository LoadMavenJewelRadiusKeystonesRepository()
    {
        mavenjewelradiuskeystonesrepository ??= new MavenJewelRadiusKeystonesRepository(logger, this);
        return mavenjewelradiuskeystonesrepository;
    }

    /// <summary>
    /// Gets RitualBalancePerLevelRepository data.
    /// </summary>
    /// <returns>repository of RitualBalancePerLevelRepository.</returns>
    public RitualBalancePerLevelRepository LoadRitualBalancePerLevelRepository()
    {
        ritualbalanceperlevelrepository ??= new RitualBalancePerLevelRepository(logger, this);
        return ritualbalanceperlevelrepository;
    }

    /// <summary>
    /// Gets RitualConstantsRepository data.
    /// </summary>
    /// <returns>repository of RitualConstantsRepository.</returns>
    public RitualConstantsRepository LoadRitualConstantsRepository()
    {
        ritualconstantsrepository ??= new RitualConstantsRepository(logger, this);
        return ritualconstantsrepository;
    }

    /// <summary>
    /// Gets RitualRuneTypesRepository data.
    /// </summary>
    /// <returns>repository of RitualRuneTypesRepository.</returns>
    public RitualRuneTypesRepository LoadRitualRuneTypesRepository()
    {
        ritualrunetypesrepository ??= new RitualRuneTypesRepository(logger, this);
        return ritualrunetypesrepository;
    }

    /// <summary>
    /// Gets RitualSetKillAchievementsRepository data.
    /// </summary>
    /// <returns>repository of RitualSetKillAchievementsRepository.</returns>
    public RitualSetKillAchievementsRepository LoadRitualSetKillAchievementsRepository()
    {
        ritualsetkillachievementsrepository ??= new RitualSetKillAchievementsRepository(logger, this);
        return ritualsetkillachievementsrepository;
    }

    /// <summary>
    /// Gets RitualSpawnPatternsRepository data.
    /// </summary>
    /// <returns>repository of RitualSpawnPatternsRepository.</returns>
    public RitualSpawnPatternsRepository LoadRitualSpawnPatternsRepository()
    {
        ritualspawnpatternsrepository ??= new RitualSpawnPatternsRepository(logger, this);
        return ritualspawnpatternsrepository;
    }

    /// <summary>
    /// Gets UltimatumEncountersRepository data.
    /// </summary>
    /// <returns>repository of UltimatumEncountersRepository.</returns>
    public UltimatumEncountersRepository LoadUltimatumEncountersRepository()
    {
        ultimatumencountersrepository ??= new UltimatumEncountersRepository(logger, this);
        return ultimatumencountersrepository;
    }

    /// <summary>
    /// Gets UltimatumEncounterTypesRepository data.
    /// </summary>
    /// <returns>repository of UltimatumEncounterTypesRepository.</returns>
    public UltimatumEncounterTypesRepository LoadUltimatumEncounterTypesRepository()
    {
        ultimatumencountertypesrepository ??= new UltimatumEncounterTypesRepository(logger, this);
        return ultimatumencountertypesrepository;
    }

    /// <summary>
    /// Gets UltimatumItemisedRewardsRepository data.
    /// </summary>
    /// <returns>repository of UltimatumItemisedRewardsRepository.</returns>
    public UltimatumItemisedRewardsRepository LoadUltimatumItemisedRewardsRepository()
    {
        ultimatumitemisedrewardsrepository ??= new UltimatumItemisedRewardsRepository(logger, this);
        return ultimatumitemisedrewardsrepository;
    }

    /// <summary>
    /// Gets UltimatumMapModifiersRepository data.
    /// </summary>
    /// <returns>repository of UltimatumMapModifiersRepository.</returns>
    public UltimatumMapModifiersRepository LoadUltimatumMapModifiersRepository()
    {
        ultimatummapmodifiersrepository ??= new UltimatumMapModifiersRepository(logger, this);
        return ultimatummapmodifiersrepository;
    }

    /// <summary>
    /// Gets UltimatumModifiersRepository data.
    /// </summary>
    /// <returns>repository of UltimatumModifiersRepository.</returns>
    public UltimatumModifiersRepository LoadUltimatumModifiersRepository()
    {
        ultimatummodifiersrepository ??= new UltimatumModifiersRepository(logger, this);
        return ultimatummodifiersrepository;
    }

    /// <summary>
    /// Gets UltimatumModifierTypesRepository data.
    /// </summary>
    /// <returns>repository of UltimatumModifierTypesRepository.</returns>
    public UltimatumModifierTypesRepository LoadUltimatumModifierTypesRepository()
    {
        ultimatummodifiertypesrepository ??= new UltimatumModifierTypesRepository(logger, this);
        return ultimatummodifiertypesrepository;
    }

    /// <summary>
    /// Gets UltimatumTrialMasterAudioRepository data.
    /// </summary>
    /// <returns>repository of UltimatumTrialMasterAudioRepository.</returns>
    public UltimatumTrialMasterAudioRepository LoadUltimatumTrialMasterAudioRepository()
    {
        ultimatumtrialmasteraudiorepository ??= new UltimatumTrialMasterAudioRepository(logger, this);
        return ultimatumtrialmasteraudiorepository;
    }

    /// <summary>
    /// Gets ExpeditionAreasRepository data.
    /// </summary>
    /// <returns>repository of ExpeditionAreasRepository.</returns>
    public ExpeditionAreasRepository LoadExpeditionAreasRepository()
    {
        expeditionareasrepository ??= new ExpeditionAreasRepository(logger, this);
        return expeditionareasrepository;
    }

    /// <summary>
    /// Gets ExpeditionBalancePerLevelRepository data.
    /// </summary>
    /// <returns>repository of ExpeditionBalancePerLevelRepository.</returns>
    public ExpeditionBalancePerLevelRepository LoadExpeditionBalancePerLevelRepository()
    {
        expeditionbalanceperlevelrepository ??= new ExpeditionBalancePerLevelRepository(logger, this);
        return expeditionbalanceperlevelrepository;
    }

    /// <summary>
    /// Gets ExpeditionCurrencyRepository data.
    /// </summary>
    /// <returns>repository of ExpeditionCurrencyRepository.</returns>
    public ExpeditionCurrencyRepository LoadExpeditionCurrencyRepository()
    {
        expeditioncurrencyrepository ??= new ExpeditionCurrencyRepository(logger, this);
        return expeditioncurrencyrepository;
    }

    /// <summary>
    /// Gets ExpeditionDealsRepository data.
    /// </summary>
    /// <returns>repository of ExpeditionDealsRepository.</returns>
    public ExpeditionDealsRepository LoadExpeditionDealsRepository()
    {
        expeditiondealsrepository ??= new ExpeditionDealsRepository(logger, this);
        return expeditiondealsrepository;
    }

    /// <summary>
    /// Gets ExpeditionFactionsRepository data.
    /// </summary>
    /// <returns>repository of ExpeditionFactionsRepository.</returns>
    public ExpeditionFactionsRepository LoadExpeditionFactionsRepository()
    {
        expeditionfactionsrepository ??= new ExpeditionFactionsRepository(logger, this);
        return expeditionfactionsrepository;
    }

    /// <summary>
    /// Gets ExpeditionMarkersCommonRepository data.
    /// </summary>
    /// <returns>repository of ExpeditionMarkersCommonRepository.</returns>
    public ExpeditionMarkersCommonRepository LoadExpeditionMarkersCommonRepository()
    {
        expeditionmarkerscommonrepository ??= new ExpeditionMarkersCommonRepository(logger, this);
        return expeditionmarkerscommonrepository;
    }

    /// <summary>
    /// Gets ExpeditionNPCsRepository data.
    /// </summary>
    /// <returns>repository of ExpeditionNPCsRepository.</returns>
    public ExpeditionNPCsRepository LoadExpeditionNPCsRepository()
    {
        expeditionnpcsrepository ??= new ExpeditionNPCsRepository(logger, this);
        return expeditionnpcsrepository;
    }

    /// <summary>
    /// Gets ExpeditionRelicModsRepository data.
    /// </summary>
    /// <returns>repository of ExpeditionRelicModsRepository.</returns>
    public ExpeditionRelicModsRepository LoadExpeditionRelicModsRepository()
    {
        expeditionrelicmodsrepository ??= new ExpeditionRelicModsRepository(logger, this);
        return expeditionrelicmodsrepository;
    }

    /// <summary>
    /// Gets ExpeditionRelicsRepository data.
    /// </summary>
    /// <returns>repository of ExpeditionRelicsRepository.</returns>
    public ExpeditionRelicsRepository LoadExpeditionRelicsRepository()
    {
        expeditionrelicsrepository ??= new ExpeditionRelicsRepository(logger, this);
        return expeditionrelicsrepository;
    }

    /// <summary>
    /// Gets ExpeditionStorageLayoutRepository data.
    /// </summary>
    /// <returns>repository of ExpeditionStorageLayoutRepository.</returns>
    public ExpeditionStorageLayoutRepository LoadExpeditionStorageLayoutRepository()
    {
        expeditionstoragelayoutrepository ??= new ExpeditionStorageLayoutRepository(logger, this);
        return expeditionstoragelayoutrepository;
    }

    /// <summary>
    /// Gets ExpeditionTerrainFeaturesRepository data.
    /// </summary>
    /// <returns>repository of ExpeditionTerrainFeaturesRepository.</returns>
    public ExpeditionTerrainFeaturesRepository LoadExpeditionTerrainFeaturesRepository()
    {
        expeditionterrainfeaturesrepository ??= new ExpeditionTerrainFeaturesRepository(logger, this);
        return expeditionterrainfeaturesrepository;
    }

    /// <summary>
    /// Gets HellscapeAOReplacementsRepository data.
    /// </summary>
    /// <returns>repository of HellscapeAOReplacementsRepository.</returns>
    public HellscapeAOReplacementsRepository LoadHellscapeAOReplacementsRepository()
    {
        hellscapeaoreplacementsrepository ??= new HellscapeAOReplacementsRepository(logger, this);
        return hellscapeaoreplacementsrepository;
    }

    /// <summary>
    /// Gets HellscapeAreaPacksRepository data.
    /// </summary>
    /// <returns>repository of HellscapeAreaPacksRepository.</returns>
    public HellscapeAreaPacksRepository LoadHellscapeAreaPacksRepository()
    {
        hellscapeareapacksrepository ??= new HellscapeAreaPacksRepository(logger, this);
        return hellscapeareapacksrepository;
    }

    /// <summary>
    /// Gets HellscapeExperienceLevelsRepository data.
    /// </summary>
    /// <returns>repository of HellscapeExperienceLevelsRepository.</returns>
    public HellscapeExperienceLevelsRepository LoadHellscapeExperienceLevelsRepository()
    {
        hellscapeexperiencelevelsrepository ??= new HellscapeExperienceLevelsRepository(logger, this);
        return hellscapeexperiencelevelsrepository;
    }

    /// <summary>
    /// Gets HellscapeFactionsRepository data.
    /// </summary>
    /// <returns>repository of HellscapeFactionsRepository.</returns>
    public HellscapeFactionsRepository LoadHellscapeFactionsRepository()
    {
        hellscapefactionsrepository ??= new HellscapeFactionsRepository(logger, this);
        return hellscapefactionsrepository;
    }

    /// <summary>
    /// Gets HellscapeImmuneMonstersRepository data.
    /// </summary>
    /// <returns>repository of HellscapeImmuneMonstersRepository.</returns>
    public HellscapeImmuneMonstersRepository LoadHellscapeImmuneMonstersRepository()
    {
        hellscapeimmunemonstersrepository ??= new HellscapeImmuneMonstersRepository(logger, this);
        return hellscapeimmunemonstersrepository;
    }

    /// <summary>
    /// Gets HellscapeItemModificationTiersRepository data.
    /// </summary>
    /// <returns>repository of HellscapeItemModificationTiersRepository.</returns>
    public HellscapeItemModificationTiersRepository LoadHellscapeItemModificationTiersRepository()
    {
        hellscapeitemmodificationtiersrepository ??= new HellscapeItemModificationTiersRepository(logger, this);
        return hellscapeitemmodificationtiersrepository;
    }

    /// <summary>
    /// Gets HellscapeLifeScalingPerLevelRepository data.
    /// </summary>
    /// <returns>repository of HellscapeLifeScalingPerLevelRepository.</returns>
    public HellscapeLifeScalingPerLevelRepository LoadHellscapeLifeScalingPerLevelRepository()
    {
        hellscapelifescalingperlevelrepository ??= new HellscapeLifeScalingPerLevelRepository(logger, this);
        return hellscapelifescalingperlevelrepository;
    }

    /// <summary>
    /// Gets HellscapeModificationInventoryLayoutRepository data.
    /// </summary>
    /// <returns>repository of HellscapeModificationInventoryLayoutRepository.</returns>
    public HellscapeModificationInventoryLayoutRepository LoadHellscapeModificationInventoryLayoutRepository()
    {
        hellscapemodificationinventorylayoutrepository ??= new HellscapeModificationInventoryLayoutRepository(logger, this);
        return hellscapemodificationinventorylayoutrepository;
    }

    /// <summary>
    /// Gets HellscapeModsRepository data.
    /// </summary>
    /// <returns>repository of HellscapeModsRepository.</returns>
    public HellscapeModsRepository LoadHellscapeModsRepository()
    {
        hellscapemodsrepository ??= new HellscapeModsRepository(logger, this);
        return hellscapemodsrepository;
    }

    /// <summary>
    /// Gets HellscapeMonsterPacksRepository data.
    /// </summary>
    /// <returns>repository of HellscapeMonsterPacksRepository.</returns>
    public HellscapeMonsterPacksRepository LoadHellscapeMonsterPacksRepository()
    {
        hellscapemonsterpacksrepository ??= new HellscapeMonsterPacksRepository(logger, this);
        return hellscapemonsterpacksrepository;
    }

    /// <summary>
    /// Gets HellscapePassivesRepository data.
    /// </summary>
    /// <returns>repository of HellscapePassivesRepository.</returns>
    public HellscapePassivesRepository LoadHellscapePassivesRepository()
    {
        hellscapepassivesrepository ??= new HellscapePassivesRepository(logger, this);
        return hellscapepassivesrepository;
    }

    /// <summary>
    /// Gets HellscapePassiveTreeRepository data.
    /// </summary>
    /// <returns>repository of HellscapePassiveTreeRepository.</returns>
    public HellscapePassiveTreeRepository LoadHellscapePassiveTreeRepository()
    {
        hellscapepassivetreerepository ??= new HellscapePassiveTreeRepository(logger, this);
        return hellscapepassivetreerepository;
    }

    /// <summary>
    /// Gets ArchnemesisMetaRewardsRepository data.
    /// </summary>
    /// <returns>repository of ArchnemesisMetaRewardsRepository.</returns>
    public ArchnemesisMetaRewardsRepository LoadArchnemesisMetaRewardsRepository()
    {
        archnemesismetarewardsrepository ??= new ArchnemesisMetaRewardsRepository(logger, this);
        return archnemesismetarewardsrepository;
    }

    /// <summary>
    /// Gets ArchnemesisModComboAchievementsRepository data.
    /// </summary>
    /// <returns>repository of ArchnemesisModComboAchievementsRepository.</returns>
    public ArchnemesisModComboAchievementsRepository LoadArchnemesisModComboAchievementsRepository()
    {
        archnemesismodcomboachievementsrepository ??= new ArchnemesisModComboAchievementsRepository(logger, this);
        return archnemesismodcomboachievementsrepository;
    }

    /// <summary>
    /// Gets ArchnemesisModsRepository data.
    /// </summary>
    /// <returns>repository of ArchnemesisModsRepository.</returns>
    public ArchnemesisModsRepository LoadArchnemesisModsRepository()
    {
        archnemesismodsrepository ??= new ArchnemesisModsRepository(logger, this);
        return archnemesismodsrepository;
    }

    /// <summary>
    /// Gets ArchnemesisModVisualsRepository data.
    /// </summary>
    /// <returns>repository of ArchnemesisModVisualsRepository.</returns>
    public ArchnemesisModVisualsRepository LoadArchnemesisModVisualsRepository()
    {
        archnemesismodvisualsrepository ??= new ArchnemesisModVisualsRepository(logger, this);
        return archnemesismodvisualsrepository;
    }

    /// <summary>
    /// Gets ArchnemesisRecipesRepository data.
    /// </summary>
    /// <returns>repository of ArchnemesisRecipesRepository.</returns>
    public ArchnemesisRecipesRepository LoadArchnemesisRecipesRepository()
    {
        archnemesisrecipesrepository ??= new ArchnemesisRecipesRepository(logger, this);
        return archnemesisrecipesrepository;
    }

    /// <summary>
    /// Gets AtlasPrimordialAltarChoicesRepository data.
    /// </summary>
    /// <returns>repository of AtlasPrimordialAltarChoicesRepository.</returns>
    public AtlasPrimordialAltarChoicesRepository LoadAtlasPrimordialAltarChoicesRepository()
    {
        atlasprimordialaltarchoicesrepository ??= new AtlasPrimordialAltarChoicesRepository(logger, this);
        return atlasprimordialaltarchoicesrepository;
    }

    /// <summary>
    /// Gets AtlasPrimordialAltarChoiceTypesRepository data.
    /// </summary>
    /// <returns>repository of AtlasPrimordialAltarChoiceTypesRepository.</returns>
    public AtlasPrimordialAltarChoiceTypesRepository LoadAtlasPrimordialAltarChoiceTypesRepository()
    {
        atlasprimordialaltarchoicetypesrepository ??= new AtlasPrimordialAltarChoiceTypesRepository(logger, this);
        return atlasprimordialaltarchoicetypesrepository;
    }

    /// <summary>
    /// Gets AtlasPrimordialBossesRepository data.
    /// </summary>
    /// <returns>repository of AtlasPrimordialBossesRepository.</returns>
    public AtlasPrimordialBossesRepository LoadAtlasPrimordialBossesRepository()
    {
        atlasprimordialbossesrepository ??= new AtlasPrimordialBossesRepository(logger, this);
        return atlasprimordialbossesrepository;
    }

    /// <summary>
    /// Gets AtlasPrimordialBossInfluenceRepository data.
    /// </summary>
    /// <returns>repository of AtlasPrimordialBossInfluenceRepository.</returns>
    public AtlasPrimordialBossInfluenceRepository LoadAtlasPrimordialBossInfluenceRepository()
    {
        atlasprimordialbossinfluencerepository ??= new AtlasPrimordialBossInfluenceRepository(logger, this);
        return atlasprimordialbossinfluencerepository;
    }

    /// <summary>
    /// Gets AtlasPrimordialBossOptionsRepository data.
    /// </summary>
    /// <returns>repository of AtlasPrimordialBossOptionsRepository.</returns>
    public AtlasPrimordialBossOptionsRepository LoadAtlasPrimordialBossOptionsRepository()
    {
        atlasprimordialbossoptionsrepository ??= new AtlasPrimordialBossOptionsRepository(logger, this);
        return atlasprimordialbossoptionsrepository;
    }

    /// <summary>
    /// Gets PrimordialBossLifeScalingPerLevelRepository data.
    /// </summary>
    /// <returns>repository of PrimordialBossLifeScalingPerLevelRepository.</returns>
    public PrimordialBossLifeScalingPerLevelRepository LoadPrimordialBossLifeScalingPerLevelRepository()
    {
        primordialbosslifescalingperlevelrepository ??= new PrimordialBossLifeScalingPerLevelRepository(logger, this);
        return primordialbosslifescalingperlevelrepository;
    }

    /// <summary>
    /// Gets AtlasUpgradesInventoryLayoutRepository data.
    /// </summary>
    /// <returns>repository of AtlasUpgradesInventoryLayoutRepository.</returns>
    public AtlasUpgradesInventoryLayoutRepository LoadAtlasUpgradesInventoryLayoutRepository()
    {
        atlasupgradesinventorylayoutrepository ??= new AtlasUpgradesInventoryLayoutRepository(logger, this);
        return atlasupgradesinventorylayoutrepository;
    }

    /// <summary>
    /// Gets AtlasPassiveSkillTreeGroupTypeRepository data.
    /// </summary>
    /// <returns>repository of AtlasPassiveSkillTreeGroupTypeRepository.</returns>
    public AtlasPassiveSkillTreeGroupTypeRepository LoadAtlasPassiveSkillTreeGroupTypeRepository()
    {
        atlaspassiveskilltreegrouptyperepository ??= new AtlasPassiveSkillTreeGroupTypeRepository(logger, this);
        return atlaspassiveskilltreegrouptyperepository;
    }

    /// <summary>
    /// Gets KiracLevelsRepository data.
    /// </summary>
    /// <returns>repository of KiracLevelsRepository.</returns>
    public KiracLevelsRepository LoadKiracLevelsRepository()
    {
        kiraclevelsrepository ??= new KiracLevelsRepository(logger, this);
        return kiraclevelsrepository;
    }

    /// <summary>
    /// Gets ScoutingReportsRepository data.
    /// </summary>
    /// <returns>repository of ScoutingReportsRepository.</returns>
    public ScoutingReportsRepository LoadScoutingReportsRepository()
    {
        scoutingreportsrepository ??= new ScoutingReportsRepository(logger, this);
        return scoutingreportsrepository;
    }

    /// <summary>
    /// Gets DroneBaseTypesRepository data.
    /// </summary>
    /// <returns>repository of DroneBaseTypesRepository.</returns>
    public DroneBaseTypesRepository LoadDroneBaseTypesRepository()
    {
        dronebasetypesrepository ??= new DroneBaseTypesRepository(logger, this);
        return dronebasetypesrepository;
    }

    /// <summary>
    /// Gets DroneTypesRepository data.
    /// </summary>
    /// <returns>repository of DroneTypesRepository.</returns>
    public DroneTypesRepository LoadDroneTypesRepository()
    {
        dronetypesrepository ??= new DroneTypesRepository(logger, this);
        return dronetypesrepository;
    }

    /// <summary>
    /// Gets SentinelCraftingCurrencyRepository data.
    /// </summary>
    /// <returns>repository of SentinelCraftingCurrencyRepository.</returns>
    public SentinelCraftingCurrencyRepository LoadSentinelCraftingCurrencyRepository()
    {
        sentinelcraftingcurrencyrepository ??= new SentinelCraftingCurrencyRepository(logger, this);
        return sentinelcraftingcurrencyrepository;
    }

    /// <summary>
    /// Gets SentinelDroneInventoryLayoutRepository data.
    /// </summary>
    /// <returns>repository of SentinelDroneInventoryLayoutRepository.</returns>
    public SentinelDroneInventoryLayoutRepository LoadSentinelDroneInventoryLayoutRepository()
    {
        sentineldroneinventorylayoutrepository ??= new SentinelDroneInventoryLayoutRepository(logger, this);
        return sentineldroneinventorylayoutrepository;
    }

    /// <summary>
    /// Gets SentinelPassivesRepository data.
    /// </summary>
    /// <returns>repository of SentinelPassivesRepository.</returns>
    public SentinelPassivesRepository LoadSentinelPassivesRepository()
    {
        sentinelpassivesrepository ??= new SentinelPassivesRepository(logger, this);
        return sentinelpassivesrepository;
    }

    /// <summary>
    /// Gets SentinelPassiveStatsRepository data.
    /// </summary>
    /// <returns>repository of SentinelPassiveStatsRepository.</returns>
    public SentinelPassiveStatsRepository LoadSentinelPassiveStatsRepository()
    {
        sentinelpassivestatsrepository ??= new SentinelPassiveStatsRepository(logger, this);
        return sentinelpassivestatsrepository;
    }

    /// <summary>
    /// Gets SentinelPassiveTypesRepository data.
    /// </summary>
    /// <returns>repository of SentinelPassiveTypesRepository.</returns>
    public SentinelPassiveTypesRepository LoadSentinelPassiveTypesRepository()
    {
        sentinelpassivetypesrepository ??= new SentinelPassiveTypesRepository(logger, this);
        return sentinelpassivetypesrepository;
    }

    /// <summary>
    /// Gets SentinelPowerExpLevelsRepository data.
    /// </summary>
    /// <returns>repository of SentinelPowerExpLevelsRepository.</returns>
    public SentinelPowerExpLevelsRepository LoadSentinelPowerExpLevelsRepository()
    {
        sentinelpowerexplevelsrepository ??= new SentinelPowerExpLevelsRepository(logger, this);
        return sentinelpowerexplevelsrepository;
    }

    /// <summary>
    /// Gets SentinelStorageLayoutRepository data.
    /// </summary>
    /// <returns>repository of SentinelStorageLayoutRepository.</returns>
    public SentinelStorageLayoutRepository LoadSentinelStorageLayoutRepository()
    {
        sentinelstoragelayoutrepository ??= new SentinelStorageLayoutRepository(logger, this);
        return sentinelstoragelayoutrepository;
    }

    /// <summary>
    /// Gets SentinelTaggedMonsterStatsRepository data.
    /// </summary>
    /// <returns>repository of SentinelTaggedMonsterStatsRepository.</returns>
    public SentinelTaggedMonsterStatsRepository LoadSentinelTaggedMonsterStatsRepository()
    {
        sentineltaggedmonsterstatsrepository ??= new SentinelTaggedMonsterStatsRepository(logger, this);
        return sentineltaggedmonsterstatsrepository;
    }

    /// <summary>
    /// Gets ClientLakeDifficultyRepository data.
    /// </summary>
    /// <returns>repository of ClientLakeDifficultyRepository.</returns>
    public ClientLakeDifficultyRepository LoadClientLakeDifficultyRepository()
    {
        clientlakedifficultyrepository ??= new ClientLakeDifficultyRepository(logger, this);
        return clientlakedifficultyrepository;
    }

    /// <summary>
    /// Gets LakeBossLifeScalingPerLevelRepository data.
    /// </summary>
    /// <returns>repository of LakeBossLifeScalingPerLevelRepository.</returns>
    public LakeBossLifeScalingPerLevelRepository LoadLakeBossLifeScalingPerLevelRepository()
    {
        lakebosslifescalingperlevelrepository ??= new LakeBossLifeScalingPerLevelRepository(logger, this);
        return lakebosslifescalingperlevelrepository;
    }

    /// <summary>
    /// Gets LakeMetaOptionsRepository data.
    /// </summary>
    /// <returns>repository of LakeMetaOptionsRepository.</returns>
    public LakeMetaOptionsRepository LoadLakeMetaOptionsRepository()
    {
        lakemetaoptionsrepository ??= new LakeMetaOptionsRepository(logger, this);
        return lakemetaoptionsrepository;
    }

    /// <summary>
    /// Gets LakeMetaOptionsUnlockTextRepository data.
    /// </summary>
    /// <returns>repository of LakeMetaOptionsUnlockTextRepository.</returns>
    public LakeMetaOptionsUnlockTextRepository LoadLakeMetaOptionsUnlockTextRepository()
    {
        lakemetaoptionsunlocktextrepository ??= new LakeMetaOptionsUnlockTextRepository(logger, this);
        return lakemetaoptionsunlocktextrepository;
    }

    /// <summary>
    /// Gets LakeRoomCompletionRepository data.
    /// </summary>
    /// <returns>repository of LakeRoomCompletionRepository.</returns>
    public LakeRoomCompletionRepository LoadLakeRoomCompletionRepository()
    {
        lakeroomcompletionrepository ??= new LakeRoomCompletionRepository(logger, this);
        return lakeroomcompletionrepository;
    }

    /// <summary>
    /// Gets LakeRoomsRepository data.
    /// </summary>
    /// <returns>repository of LakeRoomsRepository.</returns>
    public LakeRoomsRepository LoadLakeRoomsRepository()
    {
        lakeroomsrepository ??= new LakeRoomsRepository(logger, this);
        return lakeroomsrepository;
    }

    /// <summary>
    /// Gets WeaponPassiveSkillTypesRepository data.
    /// </summary>
    /// <returns>repository of WeaponPassiveSkillTypesRepository.</returns>
    public WeaponPassiveSkillTypesRepository LoadWeaponPassiveSkillTypesRepository()
    {
        weaponpassiveskilltypesrepository ??= new WeaponPassiveSkillTypesRepository(logger, this);
        return weaponpassiveskilltypesrepository;
    }

    /// <summary>
    /// Gets WeaponPassiveTreeBalancePerItemLevelRepository data.
    /// </summary>
    /// <returns>repository of WeaponPassiveTreeBalancePerItemLevelRepository.</returns>
    public WeaponPassiveTreeBalancePerItemLevelRepository LoadWeaponPassiveTreeBalancePerItemLevelRepository()
    {
        weaponpassivetreebalanceperitemlevelrepository ??= new WeaponPassiveTreeBalancePerItemLevelRepository(logger, this);
        return weaponpassivetreebalanceperitemlevelrepository;
    }

    /// <summary>
    /// Gets WeaponPassiveTreeUniqueBaseTypesRepository data.
    /// </summary>
    /// <returns>repository of WeaponPassiveTreeUniqueBaseTypesRepository.</returns>
    public WeaponPassiveTreeUniqueBaseTypesRepository LoadWeaponPassiveTreeUniqueBaseTypesRepository()
    {
        weaponpassivetreeuniquebasetypesrepository ??= new WeaponPassiveTreeUniqueBaseTypesRepository(logger, this);
        return weaponpassivetreeuniquebasetypesrepository;
    }

    /// <summary>
    /// Gets WeaponPassiveSkillsRepository data.
    /// </summary>
    /// <returns>repository of WeaponPassiveSkillsRepository.</returns>
    public WeaponPassiveSkillsRepository LoadWeaponPassiveSkillsRepository()
    {
        weaponpassiveskillsrepository ??= new WeaponPassiveSkillsRepository(logger, this);
        return weaponpassiveskillsrepository;
    }

    /// <summary>
    /// Gets AchievementItemRewardsRepository data.
    /// </summary>
    /// <returns>repository of AchievementItemRewardsRepository.</returns>
    public AchievementItemRewardsRepository LoadAchievementItemRewardsRepository()
    {
        achievementitemrewardsrepository ??= new AchievementItemRewardsRepository(logger, this);
        return achievementitemrewardsrepository;
    }

    /// <summary>
    /// Gets AchievementItemsRepository data.
    /// </summary>
    /// <returns>repository of AchievementItemsRepository.</returns>
    public AchievementItemsRepository LoadAchievementItemsRepository()
    {
        achievementitemsrepository ??= new AchievementItemsRepository(logger, this);
        return achievementitemsrepository;
    }

    /// <summary>
    /// Gets AchievementsRepository data.
    /// </summary>
    /// <returns>repository of AchievementsRepository.</returns>
    public AchievementsRepository LoadAchievementsRepository()
    {
        achievementsrepository ??= new AchievementsRepository(logger, this);
        return achievementsrepository;
    }

    /// <summary>
    /// Gets AchievementSetRewardsRepository data.
    /// </summary>
    /// <returns>repository of AchievementSetRewardsRepository.</returns>
    public AchievementSetRewardsRepository LoadAchievementSetRewardsRepository()
    {
        achievementsetrewardsrepository ??= new AchievementSetRewardsRepository(logger, this);
        return achievementsetrewardsrepository;
    }

    /// <summary>
    /// Gets AchievementSetsDisplayRepository data.
    /// </summary>
    /// <returns>repository of AchievementSetsDisplayRepository.</returns>
    public AchievementSetsDisplayRepository LoadAchievementSetsDisplayRepository()
    {
        achievementsetsdisplayrepository ??= new AchievementSetsDisplayRepository(logger, this);
        return achievementsetsdisplayrepository;
    }

    /// <summary>
    /// Gets ActiveSkillsRepository data.
    /// </summary>
    /// <returns>repository of ActiveSkillsRepository.</returns>
    public ActiveSkillsRepository LoadActiveSkillsRepository()
    {
        activeskillsrepository ??= new ActiveSkillsRepository(logger, this);
        return activeskillsrepository;
    }

    /// <summary>
    /// Gets ActiveSkillTypeRepository data.
    /// </summary>
    /// <returns>repository of ActiveSkillTypeRepository.</returns>
    public ActiveSkillTypeRepository LoadActiveSkillTypeRepository()
    {
        activeskilltyperepository ??= new ActiveSkillTypeRepository(logger, this);
        return activeskilltyperepository;
    }

    /// <summary>
    /// Gets ActsRepository data.
    /// </summary>
    /// <returns>repository of ActsRepository.</returns>
    public ActsRepository LoadActsRepository()
    {
        actsrepository ??= new ActsRepository(logger, this);
        return actsrepository;
    }

    /// <summary>
    /// Gets AddBuffToTargetVarietiesRepository data.
    /// </summary>
    /// <returns>repository of AddBuffToTargetVarietiesRepository.</returns>
    public AddBuffToTargetVarietiesRepository LoadAddBuffToTargetVarietiesRepository()
    {
        addbufftotargetvarietiesrepository ??= new AddBuffToTargetVarietiesRepository(logger, this);
        return addbufftotargetvarietiesrepository;
    }

    /// <summary>
    /// Gets AdditionalLifeScalingRepository data.
    /// </summary>
    /// <returns>repository of AdditionalLifeScalingRepository.</returns>
    public AdditionalLifeScalingRepository LoadAdditionalLifeScalingRepository()
    {
        additionallifescalingrepository ??= new AdditionalLifeScalingRepository(logger, this);
        return additionallifescalingrepository;
    }

    /// <summary>
    /// Gets AdditionalMonsterPacksFromStatsRepository data.
    /// </summary>
    /// <returns>repository of AdditionalMonsterPacksFromStatsRepository.</returns>
    public AdditionalMonsterPacksFromStatsRepository LoadAdditionalMonsterPacksFromStatsRepository()
    {
        additionalmonsterpacksfromstatsrepository ??= new AdditionalMonsterPacksFromStatsRepository(logger, this);
        return additionalmonsterpacksfromstatsrepository;
    }

    /// <summary>
    /// Gets AdvancedSkillsTutorialRepository data.
    /// </summary>
    /// <returns>repository of AdvancedSkillsTutorialRepository.</returns>
    public AdvancedSkillsTutorialRepository LoadAdvancedSkillsTutorialRepository()
    {
        advancedskillstutorialrepository ??= new AdvancedSkillsTutorialRepository(logger, this);
        return advancedskillstutorialrepository;
    }

    /// <summary>
    /// Gets AegisVariationsRepository data.
    /// </summary>
    /// <returns>repository of AegisVariationsRepository.</returns>
    public AegisVariationsRepository LoadAegisVariationsRepository()
    {
        aegisvariationsrepository ??= new AegisVariationsRepository(logger, this);
        return aegisvariationsrepository;
    }

    /// <summary>
    /// Gets AlternatePassiveAdditionsRepository data.
    /// </summary>
    /// <returns>repository of AlternatePassiveAdditionsRepository.</returns>
    public AlternatePassiveAdditionsRepository LoadAlternatePassiveAdditionsRepository()
    {
        alternatepassiveadditionsrepository ??= new AlternatePassiveAdditionsRepository(logger, this);
        return alternatepassiveadditionsrepository;
    }

    /// <summary>
    /// Gets AlternatePassiveSkillsRepository data.
    /// </summary>
    /// <returns>repository of AlternatePassiveSkillsRepository.</returns>
    public AlternatePassiveSkillsRepository LoadAlternatePassiveSkillsRepository()
    {
        alternatepassiveskillsrepository ??= new AlternatePassiveSkillsRepository(logger, this);
        return alternatepassiveskillsrepository;
    }

    /// <summary>
    /// Gets AlternateSkillTargetingBehavioursRepository data.
    /// </summary>
    /// <returns>repository of AlternateSkillTargetingBehavioursRepository.</returns>
    public AlternateSkillTargetingBehavioursRepository LoadAlternateSkillTargetingBehavioursRepository()
    {
        alternateskilltargetingbehavioursrepository ??= new AlternateSkillTargetingBehavioursRepository(logger, this);
        return alternateskilltargetingbehavioursrepository;
    }

    /// <summary>
    /// Gets AlternateTreeVersionsRepository data.
    /// </summary>
    /// <returns>repository of AlternateTreeVersionsRepository.</returns>
    public AlternateTreeVersionsRepository LoadAlternateTreeVersionsRepository()
    {
        alternatetreeversionsrepository ??= new AlternateTreeVersionsRepository(logger, this);
        return alternatetreeversionsrepository;
    }

    /// <summary>
    /// Gets AnimatedObjectFlagsRepository data.
    /// </summary>
    /// <returns>repository of AnimatedObjectFlagsRepository.</returns>
    public AnimatedObjectFlagsRepository LoadAnimatedObjectFlagsRepository()
    {
        animatedobjectflagsrepository ??= new AnimatedObjectFlagsRepository(logger, this);
        return animatedobjectflagsrepository;
    }

    /// <summary>
    /// Gets AnimationRepository data.
    /// </summary>
    /// <returns>repository of AnimationRepository.</returns>
    public AnimationRepository LoadAnimationRepository()
    {
        animationrepository ??= new AnimationRepository(logger, this);
        return animationrepository;
    }

    /// <summary>
    /// Gets ApplyDamageFunctionsRepository data.
    /// </summary>
    /// <returns>repository of ApplyDamageFunctionsRepository.</returns>
    public ApplyDamageFunctionsRepository LoadApplyDamageFunctionsRepository()
    {
        applydamagefunctionsrepository ??= new ApplyDamageFunctionsRepository(logger, this);
        return applydamagefunctionsrepository;
    }

    /// <summary>
    /// Gets ArchetypeRewardsRepository data.
    /// </summary>
    /// <returns>repository of ArchetypeRewardsRepository.</returns>
    public ArchetypeRewardsRepository LoadArchetypeRewardsRepository()
    {
        archetyperewardsrepository ??= new ArchetypeRewardsRepository(logger, this);
        return archetyperewardsrepository;
    }

    /// <summary>
    /// Gets ArchetypesRepository data.
    /// </summary>
    /// <returns>repository of ArchetypesRepository.</returns>
    public ArchetypesRepository LoadArchetypesRepository()
    {
        archetypesrepository ??= new ArchetypesRepository(logger, this);
        return archetypesrepository;
    }

    /// <summary>
    /// Gets AreaInfluenceDoodadsRepository data.
    /// </summary>
    /// <returns>repository of AreaInfluenceDoodadsRepository.</returns>
    public AreaInfluenceDoodadsRepository LoadAreaInfluenceDoodadsRepository()
    {
        areainfluencedoodadsrepository ??= new AreaInfluenceDoodadsRepository(logger, this);
        return areainfluencedoodadsrepository;
    }

    /// <summary>
    /// Gets AreaTransitionAnimationsRepository data.
    /// </summary>
    /// <returns>repository of AreaTransitionAnimationsRepository.</returns>
    public AreaTransitionAnimationsRepository LoadAreaTransitionAnimationsRepository()
    {
        areatransitionanimationsrepository ??= new AreaTransitionAnimationsRepository(logger, this);
        return areatransitionanimationsrepository;
    }

    /// <summary>
    /// Gets AreaTransitionAnimationTypesRepository data.
    /// </summary>
    /// <returns>repository of AreaTransitionAnimationTypesRepository.</returns>
    public AreaTransitionAnimationTypesRepository LoadAreaTransitionAnimationTypesRepository()
    {
        areatransitionanimationtypesrepository ??= new AreaTransitionAnimationTypesRepository(logger, this);
        return areatransitionanimationtypesrepository;
    }

    /// <summary>
    /// Gets AreaTransitionInfoRepository data.
    /// </summary>
    /// <returns>repository of AreaTransitionInfoRepository.</returns>
    public AreaTransitionInfoRepository LoadAreaTransitionInfoRepository()
    {
        areatransitioninforepository ??= new AreaTransitionInfoRepository(logger, this);
        return areatransitioninforepository;
    }

    /// <summary>
    /// Gets ArmourTypesRepository data.
    /// </summary>
    /// <returns>repository of ArmourTypesRepository.</returns>
    public ArmourTypesRepository LoadArmourTypesRepository()
    {
        armourtypesrepository ??= new ArmourTypesRepository(logger, this);
        return armourtypesrepository;
    }

    /// <summary>
    /// Gets AscendancyRepository data.
    /// </summary>
    /// <returns>repository of AscendancyRepository.</returns>
    public AscendancyRepository LoadAscendancyRepository()
    {
        ascendancyrepository ??= new AscendancyRepository(logger, this);
        return ascendancyrepository;
    }

    /// <summary>
    /// Gets AtlasAwakeningStatsRepository data.
    /// </summary>
    /// <returns>repository of AtlasAwakeningStatsRepository.</returns>
    public AtlasAwakeningStatsRepository LoadAtlasAwakeningStatsRepository()
    {
        atlasawakeningstatsrepository ??= new AtlasAwakeningStatsRepository(logger, this);
        return atlasawakeningstatsrepository;
    }

    /// <summary>
    /// Gets AtlasBaseTypeDropsRepository data.
    /// </summary>
    /// <returns>repository of AtlasBaseTypeDropsRepository.</returns>
    public AtlasBaseTypeDropsRepository LoadAtlasBaseTypeDropsRepository()
    {
        atlasbasetypedropsrepository ??= new AtlasBaseTypeDropsRepository(logger, this);
        return atlasbasetypedropsrepository;
    }

    /// <summary>
    /// Gets AtlasFogRepository data.
    /// </summary>
    /// <returns>repository of AtlasFogRepository.</returns>
    public AtlasFogRepository LoadAtlasFogRepository()
    {
        atlasfogrepository ??= new AtlasFogRepository(logger, this);
        return atlasfogrepository;
    }

    /// <summary>
    /// Gets AtlasInfluenceDataRepository data.
    /// </summary>
    /// <returns>repository of AtlasInfluenceDataRepository.</returns>
    public AtlasInfluenceDataRepository LoadAtlasInfluenceDataRepository()
    {
        atlasinfluencedatarepository ??= new AtlasInfluenceDataRepository(logger, this);
        return atlasinfluencedatarepository;
    }

    /// <summary>
    /// Gets AtlasInfluenceOutcomesRepository data.
    /// </summary>
    /// <returns>repository of AtlasInfluenceOutcomesRepository.</returns>
    public AtlasInfluenceOutcomesRepository LoadAtlasInfluenceOutcomesRepository()
    {
        atlasinfluenceoutcomesrepository ??= new AtlasInfluenceOutcomesRepository(logger, this);
        return atlasinfluenceoutcomesrepository;
    }

    /// <summary>
    /// Gets AtlasInfluenceSetsRepository data.
    /// </summary>
    /// <returns>repository of AtlasInfluenceSetsRepository.</returns>
    public AtlasInfluenceSetsRepository LoadAtlasInfluenceSetsRepository()
    {
        atlasinfluencesetsrepository ??= new AtlasInfluenceSetsRepository(logger, this);
        return atlasinfluencesetsrepository;
    }

    /// <summary>
    /// Gets AtlasModsRepository data.
    /// </summary>
    /// <returns>repository of AtlasModsRepository.</returns>
    public AtlasModsRepository LoadAtlasModsRepository()
    {
        atlasmodsrepository ??= new AtlasModsRepository(logger, this);
        return atlasmodsrepository;
    }

    /// <summary>
    /// Gets AtlasFavouredMapSlotsRepository data.
    /// </summary>
    /// <returns>repository of AtlasFavouredMapSlotsRepository.</returns>
    public AtlasFavouredMapSlotsRepository LoadAtlasFavouredMapSlotsRepository()
    {
        atlasfavouredmapslotsrepository ??= new AtlasFavouredMapSlotsRepository(logger, this);
        return atlasfavouredmapslotsrepository;
    }

    /// <summary>
    /// Gets AtlasNodeRepository data.
    /// </summary>
    /// <returns>repository of AtlasNodeRepository.</returns>
    public AtlasNodeRepository LoadAtlasNodeRepository()
    {
        atlasnoderepository ??= new AtlasNodeRepository(logger, this);
        return atlasnoderepository;
    }

    /// <summary>
    /// Gets AtlasNodeDefinitionRepository data.
    /// </summary>
    /// <returns>repository of AtlasNodeDefinitionRepository.</returns>
    public AtlasNodeDefinitionRepository LoadAtlasNodeDefinitionRepository()
    {
        atlasnodedefinitionrepository ??= new AtlasNodeDefinitionRepository(logger, this);
        return atlasnodedefinitionrepository;
    }

    /// <summary>
    /// Gets AtlasPositionsRepository data.
    /// </summary>
    /// <returns>repository of AtlasPositionsRepository.</returns>
    public AtlasPositionsRepository LoadAtlasPositionsRepository()
    {
        atlaspositionsrepository ??= new AtlasPositionsRepository(logger, this);
        return atlaspositionsrepository;
    }

    /// <summary>
    /// Gets AtlasRegionsRepository data.
    /// </summary>
    /// <returns>repository of AtlasRegionsRepository.</returns>
    public AtlasRegionsRepository LoadAtlasRegionsRepository()
    {
        atlasregionsrepository ??= new AtlasRegionsRepository(logger, this);
        return atlasregionsrepository;
    }

    /// <summary>
    /// Gets AtlasRegionUpgradesInventoryLayoutRepository data.
    /// </summary>
    /// <returns>repository of AtlasRegionUpgradesInventoryLayoutRepository.</returns>
    public AtlasRegionUpgradesInventoryLayoutRepository LoadAtlasRegionUpgradesInventoryLayoutRepository()
    {
        atlasregionupgradesinventorylayoutrepository ??= new AtlasRegionUpgradesInventoryLayoutRepository(logger, this);
        return atlasregionupgradesinventorylayoutrepository;
    }

    /// <summary>
    /// Gets AtlasRegionUpgradeRegionsRepository data.
    /// </summary>
    /// <returns>repository of AtlasRegionUpgradeRegionsRepository.</returns>
    public AtlasRegionUpgradeRegionsRepository LoadAtlasRegionUpgradeRegionsRepository()
    {
        atlasregionupgraderegionsrepository ??= new AtlasRegionUpgradeRegionsRepository(logger, this);
        return atlasregionupgraderegionsrepository;
    }

    /// <summary>
    /// Gets AtlasSectorRepository data.
    /// </summary>
    /// <returns>repository of AtlasSectorRepository.</returns>
    public AtlasSectorRepository LoadAtlasSectorRepository()
    {
        atlassectorrepository ??= new AtlasSectorRepository(logger, this);
        return atlassectorrepository;
    }

    /// <summary>
    /// Gets AwardDisplayRepository data.
    /// </summary>
    /// <returns>repository of AwardDisplayRepository.</returns>
    public AwardDisplayRepository LoadAwardDisplayRepository()
    {
        awarddisplayrepository ??= new AwardDisplayRepository(logger, this);
        return awarddisplayrepository;
    }

    /// <summary>
    /// Gets BackendErrorsRepository data.
    /// </summary>
    /// <returns>repository of BackendErrorsRepository.</returns>
    public BackendErrorsRepository LoadBackendErrorsRepository()
    {
        backenderrorsrepository ??= new BackendErrorsRepository(logger, this);
        return backenderrorsrepository;
    }

    /// <summary>
    /// Gets BaseItemTypesRepository data.
    /// </summary>
    /// <returns>repository of BaseItemTypesRepository.</returns>
    public BaseItemTypesRepository LoadBaseItemTypesRepository()
    {
        baseitemtypesrepository ??= new BaseItemTypesRepository(logger, this);
        return baseitemtypesrepository;
    }

    /// <summary>
    /// Gets BindableVirtualKeysRepository data.
    /// </summary>
    /// <returns>repository of BindableVirtualKeysRepository.</returns>
    public BindableVirtualKeysRepository LoadBindableVirtualKeysRepository()
    {
        bindablevirtualkeysrepository ??= new BindableVirtualKeysRepository(logger, this);
        return bindablevirtualkeysrepository;
    }

    /// <summary>
    /// Gets BlightStashTabLayoutRepository data.
    /// </summary>
    /// <returns>repository of BlightStashTabLayoutRepository.</returns>
    public BlightStashTabLayoutRepository LoadBlightStashTabLayoutRepository()
    {
        blightstashtablayoutrepository ??= new BlightStashTabLayoutRepository(logger, this);
        return blightstashtablayoutrepository;
    }

    /// <summary>
    /// Gets BloodTypesRepository data.
    /// </summary>
    /// <returns>repository of BloodTypesRepository.</returns>
    public BloodTypesRepository LoadBloodTypesRepository()
    {
        bloodtypesrepository ??= new BloodTypesRepository(logger, this);
        return bloodtypesrepository;
    }

    /// <summary>
    /// Gets BuffDefinitionsRepository data.
    /// </summary>
    /// <returns>repository of BuffDefinitionsRepository.</returns>
    public BuffDefinitionsRepository LoadBuffDefinitionsRepository()
    {
        buffdefinitionsrepository ??= new BuffDefinitionsRepository(logger, this);
        return buffdefinitionsrepository;
    }

    /// <summary>
    /// Gets BuffTemplatesRepository data.
    /// </summary>
    /// <returns>repository of BuffTemplatesRepository.</returns>
    public BuffTemplatesRepository LoadBuffTemplatesRepository()
    {
        bufftemplatesrepository ??= new BuffTemplatesRepository(logger, this);
        return bufftemplatesrepository;
    }

    /// <summary>
    /// Gets BuffVisualOrbArtRepository data.
    /// </summary>
    /// <returns>repository of BuffVisualOrbArtRepository.</returns>
    public BuffVisualOrbArtRepository LoadBuffVisualOrbArtRepository()
    {
        buffvisualorbartrepository ??= new BuffVisualOrbArtRepository(logger, this);
        return buffvisualorbartrepository;
    }

    /// <summary>
    /// Gets BuffVisualOrbsRepository data.
    /// </summary>
    /// <returns>repository of BuffVisualOrbsRepository.</returns>
    public BuffVisualOrbsRepository LoadBuffVisualOrbsRepository()
    {
        buffvisualorbsrepository ??= new BuffVisualOrbsRepository(logger, this);
        return buffvisualorbsrepository;
    }

    /// <summary>
    /// Gets BuffVisualOrbTypesRepository data.
    /// </summary>
    /// <returns>repository of BuffVisualOrbTypesRepository.</returns>
    public BuffVisualOrbTypesRepository LoadBuffVisualOrbTypesRepository()
    {
        buffvisualorbtypesrepository ??= new BuffVisualOrbTypesRepository(logger, this);
        return buffvisualorbtypesrepository;
    }

    /// <summary>
    /// Gets BuffVisualsRepository data.
    /// </summary>
    /// <returns>repository of BuffVisualsRepository.</returns>
    public BuffVisualsRepository LoadBuffVisualsRepository()
    {
        buffvisualsrepository ??= new BuffVisualsRepository(logger, this);
        return buffvisualsrepository;
    }

    /// <summary>
    /// Gets BuffVisualsArtVariationsRepository data.
    /// </summary>
    /// <returns>repository of BuffVisualsArtVariationsRepository.</returns>
    public BuffVisualsArtVariationsRepository LoadBuffVisualsArtVariationsRepository()
    {
        buffvisualsartvariationsrepository ??= new BuffVisualsArtVariationsRepository(logger, this);
        return buffvisualsartvariationsrepository;
    }

    /// <summary>
    /// Gets BuffVisualSetEntriesRepository data.
    /// </summary>
    /// <returns>repository of BuffVisualSetEntriesRepository.</returns>
    public BuffVisualSetEntriesRepository LoadBuffVisualSetEntriesRepository()
    {
        buffvisualsetentriesrepository ??= new BuffVisualSetEntriesRepository(logger, this);
        return buffvisualsetentriesrepository;
    }

    /// <summary>
    /// Gets CharacterAudioEventsRepository data.
    /// </summary>
    /// <returns>repository of CharacterAudioEventsRepository.</returns>
    public CharacterAudioEventsRepository LoadCharacterAudioEventsRepository()
    {
        characteraudioeventsrepository ??= new CharacterAudioEventsRepository(logger, this);
        return characteraudioeventsrepository;
    }

    /// <summary>
    /// Gets CharacterEventTextAudioRepository data.
    /// </summary>
    /// <returns>repository of CharacterEventTextAudioRepository.</returns>
    public CharacterEventTextAudioRepository LoadCharacterEventTextAudioRepository()
    {
        charactereventtextaudiorepository ??= new CharacterEventTextAudioRepository(logger, this);
        return charactereventtextaudiorepository;
    }

    /// <summary>
    /// Gets CharacterPanelDescriptionModesRepository data.
    /// </summary>
    /// <returns>repository of CharacterPanelDescriptionModesRepository.</returns>
    public CharacterPanelDescriptionModesRepository LoadCharacterPanelDescriptionModesRepository()
    {
        characterpaneldescriptionmodesrepository ??= new CharacterPanelDescriptionModesRepository(logger, this);
        return characterpaneldescriptionmodesrepository;
    }

    /// <summary>
    /// Gets CharacterPanelStatsRepository data.
    /// </summary>
    /// <returns>repository of CharacterPanelStatsRepository.</returns>
    public CharacterPanelStatsRepository LoadCharacterPanelStatsRepository()
    {
        characterpanelstatsrepository ??= new CharacterPanelStatsRepository(logger, this);
        return characterpanelstatsrepository;
    }

    /// <summary>
    /// Gets CharacterPanelTabsRepository data.
    /// </summary>
    /// <returns>repository of CharacterPanelTabsRepository.</returns>
    public CharacterPanelTabsRepository LoadCharacterPanelTabsRepository()
    {
        characterpaneltabsrepository ??= new CharacterPanelTabsRepository(logger, this);
        return characterpaneltabsrepository;
    }

    /// <summary>
    /// Gets CharactersRepository data.
    /// </summary>
    /// <returns>repository of CharactersRepository.</returns>
    public CharactersRepository LoadCharactersRepository()
    {
        charactersrepository ??= new CharactersRepository(logger, this);
        return charactersrepository;
    }

    /// <summary>
    /// Gets CharacterStartQuestStateRepository data.
    /// </summary>
    /// <returns>repository of CharacterStartQuestStateRepository.</returns>
    public CharacterStartQuestStateRepository LoadCharacterStartQuestStateRepository()
    {
        characterstartqueststaterepository ??= new CharacterStartQuestStateRepository(logger, this);
        return characterstartqueststaterepository;
    }

    /// <summary>
    /// Gets CharacterStartStatesRepository data.
    /// </summary>
    /// <returns>repository of CharacterStartStatesRepository.</returns>
    public CharacterStartStatesRepository LoadCharacterStartStatesRepository()
    {
        characterstartstatesrepository ??= new CharacterStartStatesRepository(logger, this);
        return characterstartstatesrepository;
    }

    /// <summary>
    /// Gets CharacterStartStateSetRepository data.
    /// </summary>
    /// <returns>repository of CharacterStartStateSetRepository.</returns>
    public CharacterStartStateSetRepository LoadCharacterStartStateSetRepository()
    {
        characterstartstatesetrepository ??= new CharacterStartStateSetRepository(logger, this);
        return characterstartstatesetrepository;
    }

    /// <summary>
    /// Gets CharacterTextAudioRepository data.
    /// </summary>
    /// <returns>repository of CharacterTextAudioRepository.</returns>
    public CharacterTextAudioRepository LoadCharacterTextAudioRepository()
    {
        charactertextaudiorepository ??= new CharacterTextAudioRepository(logger, this);
        return charactertextaudiorepository;
    }

    /// <summary>
    /// Gets ChatIconsRepository data.
    /// </summary>
    /// <returns>repository of ChatIconsRepository.</returns>
    public ChatIconsRepository LoadChatIconsRepository()
    {
        chaticonsrepository ??= new ChatIconsRepository(logger, this);
        return chaticonsrepository;
    }

    /// <summary>
    /// Gets ChestClustersRepository data.
    /// </summary>
    /// <returns>repository of ChestClustersRepository.</returns>
    public ChestClustersRepository LoadChestClustersRepository()
    {
        chestclustersrepository ??= new ChestClustersRepository(logger, this);
        return chestclustersrepository;
    }

    /// <summary>
    /// Gets ChestEffectsRepository data.
    /// </summary>
    /// <returns>repository of ChestEffectsRepository.</returns>
    public ChestEffectsRepository LoadChestEffectsRepository()
    {
        chesteffectsrepository ??= new ChestEffectsRepository(logger, this);
        return chesteffectsrepository;
    }

    /// <summary>
    /// Gets ChestsRepository data.
    /// </summary>
    /// <returns>repository of ChestsRepository.</returns>
    public ChestsRepository LoadChestsRepository()
    {
        chestsrepository ??= new ChestsRepository(logger, this);
        return chestsrepository;
    }

    /// <summary>
    /// Gets ClientStringsRepository data.
    /// </summary>
    /// <returns>repository of ClientStringsRepository.</returns>
    public ClientStringsRepository LoadClientStringsRepository()
    {
        clientstringsrepository ??= new ClientStringsRepository(logger, this);
        return clientstringsrepository;
    }

    /// <summary>
    /// Gets ClientLeagueActionRepository data.
    /// </summary>
    /// <returns>repository of ClientLeagueActionRepository.</returns>
    public ClientLeagueActionRepository LoadClientLeagueActionRepository()
    {
        clientleagueactionrepository ??= new ClientLeagueActionRepository(logger, this);
        return clientleagueactionrepository;
    }

    /// <summary>
    /// Gets CloneShotRepository data.
    /// </summary>
    /// <returns>repository of CloneShotRepository.</returns>
    public CloneShotRepository LoadCloneShotRepository()
    {
        cloneshotrepository ??= new CloneShotRepository(logger, this);
        return cloneshotrepository;
    }

    /// <summary>
    /// Gets ColoursRepository data.
    /// </summary>
    /// <returns>repository of ColoursRepository.</returns>
    public ColoursRepository LoadColoursRepository()
    {
        coloursrepository ??= new ColoursRepository(logger, this);
        return coloursrepository;
    }

    /// <summary>
    /// Gets CommandsRepository data.
    /// </summary>
    /// <returns>repository of CommandsRepository.</returns>
    public CommandsRepository LoadCommandsRepository()
    {
        commandsrepository ??= new CommandsRepository(logger, this);
        return commandsrepository;
    }

    /// <summary>
    /// Gets ComponentAttributeRequirementsRepository data.
    /// </summary>
    /// <returns>repository of ComponentAttributeRequirementsRepository.</returns>
    public ComponentAttributeRequirementsRepository LoadComponentAttributeRequirementsRepository()
    {
        componentattributerequirementsrepository ??= new ComponentAttributeRequirementsRepository(logger, this);
        return componentattributerequirementsrepository;
    }

    /// <summary>
    /// Gets ComponentChargesRepository data.
    /// </summary>
    /// <returns>repository of ComponentChargesRepository.</returns>
    public ComponentChargesRepository LoadComponentChargesRepository()
    {
        componentchargesrepository ??= new ComponentChargesRepository(logger, this);
        return componentchargesrepository;
    }

    /// <summary>
    /// Gets CoreLeaguesRepository data.
    /// </summary>
    /// <returns>repository of CoreLeaguesRepository.</returns>
    public CoreLeaguesRepository LoadCoreLeaguesRepository()
    {
        coreleaguesrepository ??= new CoreLeaguesRepository(logger, this);
        return coreleaguesrepository;
    }

    /// <summary>
    /// Gets CostTypesRepository data.
    /// </summary>
    /// <returns>repository of CostTypesRepository.</returns>
    public CostTypesRepository LoadCostTypesRepository()
    {
        costtypesrepository ??= new CostTypesRepository(logger, this);
        return costtypesrepository;
    }

    /// <summary>
    /// Gets CraftingBenchOptionsRepository data.
    /// </summary>
    /// <returns>repository of CraftingBenchOptionsRepository.</returns>
    public CraftingBenchOptionsRepository LoadCraftingBenchOptionsRepository()
    {
        craftingbenchoptionsrepository ??= new CraftingBenchOptionsRepository(logger, this);
        return craftingbenchoptionsrepository;
    }

    /// <summary>
    /// Gets CraftingBenchSortCategoriesRepository data.
    /// </summary>
    /// <returns>repository of CraftingBenchSortCategoriesRepository.</returns>
    public CraftingBenchSortCategoriesRepository LoadCraftingBenchSortCategoriesRepository()
    {
        craftingbenchsortcategoriesrepository ??= new CraftingBenchSortCategoriesRepository(logger, this);
        return craftingbenchsortcategoriesrepository;
    }

    /// <summary>
    /// Gets CraftingBenchUnlockCategoriesRepository data.
    /// </summary>
    /// <returns>repository of CraftingBenchUnlockCategoriesRepository.</returns>
    public CraftingBenchUnlockCategoriesRepository LoadCraftingBenchUnlockCategoriesRepository()
    {
        craftingbenchunlockcategoriesrepository ??= new CraftingBenchUnlockCategoriesRepository(logger, this);
        return craftingbenchunlockcategoriesrepository;
    }

    /// <summary>
    /// Gets CraftingItemClassCategoriesRepository data.
    /// </summary>
    /// <returns>repository of CraftingItemClassCategoriesRepository.</returns>
    public CraftingItemClassCategoriesRepository LoadCraftingItemClassCategoriesRepository()
    {
        craftingitemclasscategoriesrepository ??= new CraftingItemClassCategoriesRepository(logger, this);
        return craftingitemclasscategoriesrepository;
    }

    /// <summary>
    /// Gets CurrencyItemsRepository data.
    /// </summary>
    /// <returns>repository of CurrencyItemsRepository.</returns>
    public CurrencyItemsRepository LoadCurrencyItemsRepository()
    {
        currencyitemsrepository ??= new CurrencyItemsRepository(logger, this);
        return currencyitemsrepository;
    }

    /// <summary>
    /// Gets CurrencyStashTabLayoutRepository data.
    /// </summary>
    /// <returns>repository of CurrencyStashTabLayoutRepository.</returns>
    public CurrencyStashTabLayoutRepository LoadCurrencyStashTabLayoutRepository()
    {
        currencystashtablayoutrepository ??= new CurrencyStashTabLayoutRepository(logger, this);
        return currencystashtablayoutrepository;
    }

    /// <summary>
    /// Gets CustomLeagueModsRepository data.
    /// </summary>
    /// <returns>repository of CustomLeagueModsRepository.</returns>
    public CustomLeagueModsRepository LoadCustomLeagueModsRepository()
    {
        customleaguemodsrepository ??= new CustomLeagueModsRepository(logger, this);
        return customleaguemodsrepository;
    }

    /// <summary>
    /// Gets DaemonSpawningDataRepository data.
    /// </summary>
    /// <returns>repository of DaemonSpawningDataRepository.</returns>
    public DaemonSpawningDataRepository LoadDaemonSpawningDataRepository()
    {
        daemonspawningdatarepository ??= new DaemonSpawningDataRepository(logger, this);
        return daemonspawningdatarepository;
    }

    /// <summary>
    /// Gets DamageHitEffectsRepository data.
    /// </summary>
    /// <returns>repository of DamageHitEffectsRepository.</returns>
    public DamageHitEffectsRepository LoadDamageHitEffectsRepository()
    {
        damagehiteffectsrepository ??= new DamageHitEffectsRepository(logger, this);
        return damagehiteffectsrepository;
    }

    /// <summary>
    /// Gets DamageParticleEffectsRepository data.
    /// </summary>
    /// <returns>repository of DamageParticleEffectsRepository.</returns>
    public DamageParticleEffectsRepository LoadDamageParticleEffectsRepository()
    {
        damageparticleeffectsrepository ??= new DamageParticleEffectsRepository(logger, this);
        return damageparticleeffectsrepository;
    }

    /// <summary>
    /// Gets DancesRepository data.
    /// </summary>
    /// <returns>repository of DancesRepository.</returns>
    public DancesRepository LoadDancesRepository()
    {
        dancesrepository ??= new DancesRepository(logger, this);
        return dancesrepository;
    }

    /// <summary>
    /// Gets DaressoPitFightsRepository data.
    /// </summary>
    /// <returns>repository of DaressoPitFightsRepository.</returns>
    public DaressoPitFightsRepository LoadDaressoPitFightsRepository()
    {
        daressopitfightsrepository ??= new DaressoPitFightsRepository(logger, this);
        return daressopitfightsrepository;
    }

    /// <summary>
    /// Gets DefaultMonsterStatsRepository data.
    /// </summary>
    /// <returns>repository of DefaultMonsterStatsRepository.</returns>
    public DefaultMonsterStatsRepository LoadDefaultMonsterStatsRepository()
    {
        defaultmonsterstatsrepository ??= new DefaultMonsterStatsRepository(logger, this);
        return defaultmonsterstatsrepository;
    }

    /// <summary>
    /// Gets DeliriumStashTabLayoutRepository data.
    /// </summary>
    /// <returns>repository of DeliriumStashTabLayoutRepository.</returns>
    public DeliriumStashTabLayoutRepository LoadDeliriumStashTabLayoutRepository()
    {
        deliriumstashtablayoutrepository ??= new DeliriumStashTabLayoutRepository(logger, this);
        return deliriumstashtablayoutrepository;
    }

    /// <summary>
    /// Gets DelveStashTabLayoutRepository data.
    /// </summary>
    /// <returns>repository of DelveStashTabLayoutRepository.</returns>
    public DelveStashTabLayoutRepository LoadDelveStashTabLayoutRepository()
    {
        delvestashtablayoutrepository ??= new DelveStashTabLayoutRepository(logger, this);
        return delvestashtablayoutrepository;
    }

    /// <summary>
    /// Gets DescentExilesRepository data.
    /// </summary>
    /// <returns>repository of DescentExilesRepository.</returns>
    public DescentExilesRepository LoadDescentExilesRepository()
    {
        descentexilesrepository ??= new DescentExilesRepository(logger, this);
        return descentexilesrepository;
    }

    /// <summary>
    /// Gets DescentRewardChestsRepository data.
    /// </summary>
    /// <returns>repository of DescentRewardChestsRepository.</returns>
    public DescentRewardChestsRepository LoadDescentRewardChestsRepository()
    {
        descentrewardchestsrepository ??= new DescentRewardChestsRepository(logger, this);
        return descentrewardchestsrepository;
    }

    /// <summary>
    /// Gets DescentStarterChestRepository data.
    /// </summary>
    /// <returns>repository of DescentStarterChestRepository.</returns>
    public DescentStarterChestRepository LoadDescentStarterChestRepository()
    {
        descentstarterchestrepository ??= new DescentStarterChestRepository(logger, this);
        return descentstarterchestrepository;
    }

    /// <summary>
    /// Gets DialogueEventRepository data.
    /// </summary>
    /// <returns>repository of DialogueEventRepository.</returns>
    public DialogueEventRepository LoadDialogueEventRepository()
    {
        dialogueeventrepository ??= new DialogueEventRepository(logger, this);
        return dialogueeventrepository;
    }

    /// <summary>
    /// Gets DisplayMinionMonsterTypeRepository data.
    /// </summary>
    /// <returns>repository of DisplayMinionMonsterTypeRepository.</returns>
    public DisplayMinionMonsterTypeRepository LoadDisplayMinionMonsterTypeRepository()
    {
        displayminionmonstertyperepository ??= new DisplayMinionMonsterTypeRepository(logger, this);
        return displayminionmonstertyperepository;
    }

    /// <summary>
    /// Gets DivinationCardStashTabLayoutRepository data.
    /// </summary>
    /// <returns>repository of DivinationCardStashTabLayoutRepository.</returns>
    public DivinationCardStashTabLayoutRepository LoadDivinationCardStashTabLayoutRepository()
    {
        divinationcardstashtablayoutrepository ??= new DivinationCardStashTabLayoutRepository(logger, this);
        return divinationcardstashtablayoutrepository;
    }

    /// <summary>
    /// Gets DoorsRepository data.
    /// </summary>
    /// <returns>repository of DoorsRepository.</returns>
    public DoorsRepository LoadDoorsRepository()
    {
        doorsrepository ??= new DoorsRepository(logger, this);
        return doorsrepository;
    }

    /// <summary>
    /// Gets DropEffectsRepository data.
    /// </summary>
    /// <returns>repository of DropEffectsRepository.</returns>
    public DropEffectsRepository LoadDropEffectsRepository()
    {
        dropeffectsrepository ??= new DropEffectsRepository(logger, this);
        return dropeffectsrepository;
    }

    /// <summary>
    /// Gets DropPoolRepository data.
    /// </summary>
    /// <returns>repository of DropPoolRepository.</returns>
    public DropPoolRepository LoadDropPoolRepository()
    {
        droppoolrepository ??= new DropPoolRepository(logger, this);
        return droppoolrepository;
    }

    /// <summary>
    /// Gets EclipseModsRepository data.
    /// </summary>
    /// <returns>repository of EclipseModsRepository.</returns>
    public EclipseModsRepository LoadEclipseModsRepository()
    {
        eclipsemodsrepository ??= new EclipseModsRepository(logger, this);
        return eclipsemodsrepository;
    }

    /// <summary>
    /// Gets EffectDrivenSkillRepository data.
    /// </summary>
    /// <returns>repository of EffectDrivenSkillRepository.</returns>
    public EffectDrivenSkillRepository LoadEffectDrivenSkillRepository()
    {
        effectdrivenskillrepository ??= new EffectDrivenSkillRepository(logger, this);
        return effectdrivenskillrepository;
    }

    /// <summary>
    /// Gets EffectivenessCostConstantsRepository data.
    /// </summary>
    /// <returns>repository of EffectivenessCostConstantsRepository.</returns>
    public EffectivenessCostConstantsRepository LoadEffectivenessCostConstantsRepository()
    {
        effectivenesscostconstantsrepository ??= new EffectivenessCostConstantsRepository(logger, this);
        return effectivenesscostconstantsrepository;
    }

    /// <summary>
    /// Gets EinharMissionsRepository data.
    /// </summary>
    /// <returns>repository of EinharMissionsRepository.</returns>
    public EinharMissionsRepository LoadEinharMissionsRepository()
    {
        einharmissionsrepository ??= new EinharMissionsRepository(logger, this);
        return einharmissionsrepository;
    }

    /// <summary>
    /// Gets EinharPackFallbackRepository data.
    /// </summary>
    /// <returns>repository of EinharPackFallbackRepository.</returns>
    public EinharPackFallbackRepository LoadEinharPackFallbackRepository()
    {
        einharpackfallbackrepository ??= new EinharPackFallbackRepository(logger, this);
        return einharpackfallbackrepository;
    }

    /// <summary>
    /// Gets EndlessLedgeChestsRepository data.
    /// </summary>
    /// <returns>repository of EndlessLedgeChestsRepository.</returns>
    public EndlessLedgeChestsRepository LoadEndlessLedgeChestsRepository()
    {
        endlessledgechestsrepository ??= new EndlessLedgeChestsRepository(logger, this);
        return endlessledgechestsrepository;
    }

    /// <summary>
    /// Gets EnvironmentsRepository data.
    /// </summary>
    /// <returns>repository of EnvironmentsRepository.</returns>
    public EnvironmentsRepository LoadEnvironmentsRepository()
    {
        environmentsrepository ??= new EnvironmentsRepository(logger, this);
        return environmentsrepository;
    }

    /// <summary>
    /// Gets EnvironmentTransitionsRepository data.
    /// </summary>
    /// <returns>repository of EnvironmentTransitionsRepository.</returns>
    public EnvironmentTransitionsRepository LoadEnvironmentTransitionsRepository()
    {
        environmenttransitionsrepository ??= new EnvironmentTransitionsRepository(logger, this);
        return environmenttransitionsrepository;
    }

    /// <summary>
    /// Gets EssenceStashTabLayoutRepository data.
    /// </summary>
    /// <returns>repository of EssenceStashTabLayoutRepository.</returns>
    public EssenceStashTabLayoutRepository LoadEssenceStashTabLayoutRepository()
    {
        essencestashtablayoutrepository ??= new EssenceStashTabLayoutRepository(logger, this);
        return essencestashtablayoutrepository;
    }

    /// <summary>
    /// Gets EventSeasonRepository data.
    /// </summary>
    /// <returns>repository of EventSeasonRepository.</returns>
    public EventSeasonRepository LoadEventSeasonRepository()
    {
        eventseasonrepository ??= new EventSeasonRepository(logger, this);
        return eventseasonrepository;
    }

    /// <summary>
    /// Gets EventSeasonRewardsRepository data.
    /// </summary>
    /// <returns>repository of EventSeasonRewardsRepository.</returns>
    public EventSeasonRewardsRepository LoadEventSeasonRewardsRepository()
    {
        eventseasonrewardsrepository ??= new EventSeasonRewardsRepository(logger, this);
        return eventseasonrewardsrepository;
    }

    /// <summary>
    /// Gets EvergreenAchievementsRepository data.
    /// </summary>
    /// <returns>repository of EvergreenAchievementsRepository.</returns>
    public EvergreenAchievementsRepository LoadEvergreenAchievementsRepository()
    {
        evergreenachievementsrepository ??= new EvergreenAchievementsRepository(logger, this);
        return evergreenachievementsrepository;
    }

    /// <summary>
    /// Gets ExecuteGEALRepository data.
    /// </summary>
    /// <returns>repository of ExecuteGEALRepository.</returns>
    public ExecuteGEALRepository LoadExecuteGEALRepository()
    {
        executegealrepository ??= new ExecuteGEALRepository(logger, this);
        return executegealrepository;
    }

    /// <summary>
    /// Gets ExpandingPulseRepository data.
    /// </summary>
    /// <returns>repository of ExpandingPulseRepository.</returns>
    public ExpandingPulseRepository LoadExpandingPulseRepository()
    {
        expandingpulserepository ??= new ExpandingPulseRepository(logger, this);
        return expandingpulserepository;
    }

    /// <summary>
    /// Gets ExperienceLevelsRepository data.
    /// </summary>
    /// <returns>repository of ExperienceLevelsRepository.</returns>
    public ExperienceLevelsRepository LoadExperienceLevelsRepository()
    {
        experiencelevelsrepository ??= new ExperienceLevelsRepository(logger, this);
        return experiencelevelsrepository;
    }

    /// <summary>
    /// Gets ExplodingStormBuffsRepository data.
    /// </summary>
    /// <returns>repository of ExplodingStormBuffsRepository.</returns>
    public ExplodingStormBuffsRepository LoadExplodingStormBuffsRepository()
    {
        explodingstormbuffsrepository ??= new ExplodingStormBuffsRepository(logger, this);
        return explodingstormbuffsrepository;
    }

    /// <summary>
    /// Gets ExtraTerrainFeaturesRepository data.
    /// </summary>
    /// <returns>repository of ExtraTerrainFeaturesRepository.</returns>
    public ExtraTerrainFeaturesRepository LoadExtraTerrainFeaturesRepository()
    {
        extraterrainfeaturesrepository ??= new ExtraTerrainFeaturesRepository(logger, this);
        return extraterrainfeaturesrepository;
    }

    /// <summary>
    /// Gets FixedHideoutDoodadTypesRepository data.
    /// </summary>
    /// <returns>repository of FixedHideoutDoodadTypesRepository.</returns>
    public FixedHideoutDoodadTypesRepository LoadFixedHideoutDoodadTypesRepository()
    {
        fixedhideoutdoodadtypesrepository ??= new FixedHideoutDoodadTypesRepository(logger, this);
        return fixedhideoutdoodadtypesrepository;
    }

    /// <summary>
    /// Gets FixedMissionsRepository data.
    /// </summary>
    /// <returns>repository of FixedMissionsRepository.</returns>
    public FixedMissionsRepository LoadFixedMissionsRepository()
    {
        fixedmissionsrepository ??= new FixedMissionsRepository(logger, this);
        return fixedmissionsrepository;
    }

    /// <summary>
    /// Gets FlasksRepository data.
    /// </summary>
    /// <returns>repository of FlasksRepository.</returns>
    public FlasksRepository LoadFlasksRepository()
    {
        flasksrepository ??= new FlasksRepository(logger, this);
        return flasksrepository;
    }

    /// <summary>
    /// Gets FlavourTextRepository data.
    /// </summary>
    /// <returns>repository of FlavourTextRepository.</returns>
    public FlavourTextRepository LoadFlavourTextRepository()
    {
        flavourtextrepository ??= new FlavourTextRepository(logger, this);
        return flavourtextrepository;
    }

    /// <summary>
    /// Gets FootprintsRepository data.
    /// </summary>
    /// <returns>repository of FootprintsRepository.</returns>
    public FootprintsRepository LoadFootprintsRepository()
    {
        footprintsrepository ??= new FootprintsRepository(logger, this);
        return footprintsrepository;
    }

    /// <summary>
    /// Gets FootstepAudioRepository data.
    /// </summary>
    /// <returns>repository of FootstepAudioRepository.</returns>
    public FootstepAudioRepository LoadFootstepAudioRepository()
    {
        footstepaudiorepository ??= new FootstepAudioRepository(logger, this);
        return footstepaudiorepository;
    }

    /// <summary>
    /// Gets FragmentStashTabLayoutRepository data.
    /// </summary>
    /// <returns>repository of FragmentStashTabLayoutRepository.</returns>
    public FragmentStashTabLayoutRepository LoadFragmentStashTabLayoutRepository()
    {
        fragmentstashtablayoutrepository ??= new FragmentStashTabLayoutRepository(logger, this);
        return fragmentstashtablayoutrepository;
    }

    /// <summary>
    /// Gets GameConstantsRepository data.
    /// </summary>
    /// <returns>repository of GameConstantsRepository.</returns>
    public GameConstantsRepository LoadGameConstantsRepository()
    {
        gameconstantsrepository ??= new GameConstantsRepository(logger, this);
        return gameconstantsrepository;
    }

    /// <summary>
    /// Gets GameLogosRepository data.
    /// </summary>
    /// <returns>repository of GameLogosRepository.</returns>
    public GameLogosRepository LoadGameLogosRepository()
    {
        gamelogosrepository ??= new GameLogosRepository(logger, this);
        return gamelogosrepository;
    }

    /// <summary>
    /// Gets GameObjectTasksRepository data.
    /// </summary>
    /// <returns>repository of GameObjectTasksRepository.</returns>
    public GameObjectTasksRepository LoadGameObjectTasksRepository()
    {
        gameobjecttasksrepository ??= new GameObjectTasksRepository(logger, this);
        return gameobjecttasksrepository;
    }

    /// <summary>
    /// Gets GamepadButtonRepository data.
    /// </summary>
    /// <returns>repository of GamepadButtonRepository.</returns>
    public GamepadButtonRepository LoadGamepadButtonRepository()
    {
        gamepadbuttonrepository ??= new GamepadButtonRepository(logger, this);
        return gamepadbuttonrepository;
    }

    /// <summary>
    /// Gets GamepadTypeRepository data.
    /// </summary>
    /// <returns>repository of GamepadTypeRepository.</returns>
    public GamepadTypeRepository LoadGamepadTypeRepository()
    {
        gamepadtyperepository ??= new GamepadTypeRepository(logger, this);
        return gamepadtyperepository;
    }

    /// <summary>
    /// Gets GameStatsRepository data.
    /// </summary>
    /// <returns>repository of GameStatsRepository.</returns>
    public GameStatsRepository LoadGameStatsRepository()
    {
        gamestatsrepository ??= new GameStatsRepository(logger, this);
        return gamestatsrepository;
    }

    /// <summary>
    /// Gets GemTagsRepository data.
    /// </summary>
    /// <returns>repository of GemTagsRepository.</returns>
    public GemTagsRepository LoadGemTagsRepository()
    {
        gemtagsrepository ??= new GemTagsRepository(logger, this);
        return gemtagsrepository;
    }

    /// <summary>
    /// Gets GenericBuffAurasRepository data.
    /// </summary>
    /// <returns>repository of GenericBuffAurasRepository.</returns>
    public GenericBuffAurasRepository LoadGenericBuffAurasRepository()
    {
        genericbuffaurasrepository ??= new GenericBuffAurasRepository(logger, this);
        return genericbuffaurasrepository;
    }

    /// <summary>
    /// Gets GenericLeagueRewardTypesRepository data.
    /// </summary>
    /// <returns>repository of GenericLeagueRewardTypesRepository.</returns>
    public GenericLeagueRewardTypesRepository LoadGenericLeagueRewardTypesRepository()
    {
        genericleaguerewardtypesrepository ??= new GenericLeagueRewardTypesRepository(logger, this);
        return genericleaguerewardtypesrepository;
    }

    /// <summary>
    /// Gets GenericLeagueRewardTypeVisualsRepository data.
    /// </summary>
    /// <returns>repository of GenericLeagueRewardTypeVisualsRepository.</returns>
    public GenericLeagueRewardTypeVisualsRepository LoadGenericLeagueRewardTypeVisualsRepository()
    {
        genericleaguerewardtypevisualsrepository ??= new GenericLeagueRewardTypeVisualsRepository(logger, this);
        return genericleaguerewardtypevisualsrepository;
    }

    /// <summary>
    /// Gets GeometryAttackRepository data.
    /// </summary>
    /// <returns>repository of GeometryAttackRepository.</returns>
    public GeometryAttackRepository LoadGeometryAttackRepository()
    {
        geometryattackrepository ??= new GeometryAttackRepository(logger, this);
        return geometryattackrepository;
    }

    /// <summary>
    /// Gets GeometryChannelRepository data.
    /// </summary>
    /// <returns>repository of GeometryChannelRepository.</returns>
    public GeometryChannelRepository LoadGeometryChannelRepository()
    {
        geometrychannelrepository ??= new GeometryChannelRepository(logger, this);
        return geometrychannelrepository;
    }

    /// <summary>
    /// Gets GeometryProjectilesRepository data.
    /// </summary>
    /// <returns>repository of GeometryProjectilesRepository.</returns>
    public GeometryProjectilesRepository LoadGeometryProjectilesRepository()
    {
        geometryprojectilesrepository ??= new GeometryProjectilesRepository(logger, this);
        return geometryprojectilesrepository;
    }

    /// <summary>
    /// Gets GeometryTriggerRepository data.
    /// </summary>
    /// <returns>repository of GeometryTriggerRepository.</returns>
    public GeometryTriggerRepository LoadGeometryTriggerRepository()
    {
        geometrytriggerrepository ??= new GeometryTriggerRepository(logger, this);
        return geometrytriggerrepository;
    }

    /// <summary>
    /// Gets GiftWrapArtVariationsRepository data.
    /// </summary>
    /// <returns>repository of GiftWrapArtVariationsRepository.</returns>
    public GiftWrapArtVariationsRepository LoadGiftWrapArtVariationsRepository()
    {
        giftwrapartvariationsrepository ??= new GiftWrapArtVariationsRepository(logger, this);
        return giftwrapartvariationsrepository;
    }

    /// <summary>
    /// Gets GlobalAudioConfigRepository data.
    /// </summary>
    /// <returns>repository of GlobalAudioConfigRepository.</returns>
    public GlobalAudioConfigRepository LoadGlobalAudioConfigRepository()
    {
        globalaudioconfigrepository ??= new GlobalAudioConfigRepository(logger, this);
        return globalaudioconfigrepository;
    }

    /// <summary>
    /// Gets GrandmastersRepository data.
    /// </summary>
    /// <returns>repository of GrandmastersRepository.</returns>
    public GrandmastersRepository LoadGrandmastersRepository()
    {
        grandmastersrepository ??= new GrandmastersRepository(logger, this);
        return grandmastersrepository;
    }

    /// <summary>
    /// Gets GrantedEffectQualityStatsRepository data.
    /// </summary>
    /// <returns>repository of GrantedEffectQualityStatsRepository.</returns>
    public GrantedEffectQualityStatsRepository LoadGrantedEffectQualityStatsRepository()
    {
        grantedeffectqualitystatsrepository ??= new GrantedEffectQualityStatsRepository(logger, this);
        return grantedeffectqualitystatsrepository;
    }

    /// <summary>
    /// Gets GrantedEffectQualityTypesRepository data.
    /// </summary>
    /// <returns>repository of GrantedEffectQualityTypesRepository.</returns>
    public GrantedEffectQualityTypesRepository LoadGrantedEffectQualityTypesRepository()
    {
        grantedeffectqualitytypesrepository ??= new GrantedEffectQualityTypesRepository(logger, this);
        return grantedeffectqualitytypesrepository;
    }

    /// <summary>
    /// Gets GrantedEffectsRepository data.
    /// </summary>
    /// <returns>repository of GrantedEffectsRepository.</returns>
    public GrantedEffectsRepository LoadGrantedEffectsRepository()
    {
        grantedeffectsrepository ??= new GrantedEffectsRepository(logger, this);
        return grantedeffectsrepository;
    }

    /// <summary>
    /// Gets GrantedEffectsPerLevelRepository data.
    /// </summary>
    /// <returns>repository of GrantedEffectsPerLevelRepository.</returns>
    public GrantedEffectsPerLevelRepository LoadGrantedEffectsPerLevelRepository()
    {
        grantedeffectsperlevelrepository ??= new GrantedEffectsPerLevelRepository(logger, this);
        return grantedeffectsperlevelrepository;
    }

    /// <summary>
    /// Gets GrantedEffectStatSetsRepository data.
    /// </summary>
    /// <returns>repository of GrantedEffectStatSetsRepository.</returns>
    public GrantedEffectStatSetsRepository LoadGrantedEffectStatSetsRepository()
    {
        grantedeffectstatsetsrepository ??= new GrantedEffectStatSetsRepository(logger, this);
        return grantedeffectstatsetsrepository;
    }

    /// <summary>
    /// Gets GrantedEffectStatSetsPerLevelRepository data.
    /// </summary>
    /// <returns>repository of GrantedEffectStatSetsPerLevelRepository.</returns>
    public GrantedEffectStatSetsPerLevelRepository LoadGrantedEffectStatSetsPerLevelRepository()
    {
        grantedeffectstatsetsperlevelrepository ??= new GrantedEffectStatSetsPerLevelRepository(logger, this);
        return grantedeffectstatsetsperlevelrepository;
    }

    /// <summary>
    /// Gets GroundEffectsRepository data.
    /// </summary>
    /// <returns>repository of GroundEffectsRepository.</returns>
    public GroundEffectsRepository LoadGroundEffectsRepository()
    {
        groundeffectsrepository ??= new GroundEffectsRepository(logger, this);
        return groundeffectsrepository;
    }

    /// <summary>
    /// Gets GroundEffectTypesRepository data.
    /// </summary>
    /// <returns>repository of GroundEffectTypesRepository.</returns>
    public GroundEffectTypesRepository LoadGroundEffectTypesRepository()
    {
        groundeffecttypesrepository ??= new GroundEffectTypesRepository(logger, this);
        return groundeffecttypesrepository;
    }

    /// <summary>
    /// Gets HarvestStorageLayoutRepository data.
    /// </summary>
    /// <returns>repository of HarvestStorageLayoutRepository.</returns>
    public HarvestStorageLayoutRepository LoadHarvestStorageLayoutRepository()
    {
        harveststoragelayoutrepository ??= new HarvestStorageLayoutRepository(logger, this);
        return harveststoragelayoutrepository;
    }

    /// <summary>
    /// Gets HeistStorageLayoutRepository data.
    /// </summary>
    /// <returns>repository of HeistStorageLayoutRepository.</returns>
    public HeistStorageLayoutRepository LoadHeistStorageLayoutRepository()
    {
        heiststoragelayoutrepository ??= new HeistStorageLayoutRepository(logger, this);
        return heiststoragelayoutrepository;
    }

    /// <summary>
    /// Gets HideoutDoodadsRepository data.
    /// </summary>
    /// <returns>repository of HideoutDoodadsRepository.</returns>
    public HideoutDoodadsRepository LoadHideoutDoodadsRepository()
    {
        hideoutdoodadsrepository ??= new HideoutDoodadsRepository(logger, this);
        return hideoutdoodadsrepository;
    }

    /// <summary>
    /// Gets HideoutDoodadCategoryRepository data.
    /// </summary>
    /// <returns>repository of HideoutDoodadCategoryRepository.</returns>
    public HideoutDoodadCategoryRepository LoadHideoutDoodadCategoryRepository()
    {
        hideoutdoodadcategoryrepository ??= new HideoutDoodadCategoryRepository(logger, this);
        return hideoutdoodadcategoryrepository;
    }

    /// <summary>
    /// Gets HideoutDoodadTagsRepository data.
    /// </summary>
    /// <returns>repository of HideoutDoodadTagsRepository.</returns>
    public HideoutDoodadTagsRepository LoadHideoutDoodadTagsRepository()
    {
        hideoutdoodadtagsrepository ??= new HideoutDoodadTagsRepository(logger, this);
        return hideoutdoodadtagsrepository;
    }

    /// <summary>
    /// Gets HideoutNPCsRepository data.
    /// </summary>
    /// <returns>repository of HideoutNPCsRepository.</returns>
    public HideoutNPCsRepository LoadHideoutNPCsRepository()
    {
        hideoutnpcsrepository ??= new HideoutNPCsRepository(logger, this);
        return hideoutnpcsrepository;
    }

    /// <summary>
    /// Gets HideoutRarityRepository data.
    /// </summary>
    /// <returns>repository of HideoutRarityRepository.</returns>
    public HideoutRarityRepository LoadHideoutRarityRepository()
    {
        hideoutrarityrepository ??= new HideoutRarityRepository(logger, this);
        return hideoutrarityrepository;
    }

    /// <summary>
    /// Gets HideoutsRepository data.
    /// </summary>
    /// <returns>repository of HideoutsRepository.</returns>
    public HideoutsRepository LoadHideoutsRepository()
    {
        hideoutsrepository ??= new HideoutsRepository(logger, this);
        return hideoutsrepository;
    }

    /// <summary>
    /// Gets ImpactSoundDataRepository data.
    /// </summary>
    /// <returns>repository of ImpactSoundDataRepository.</returns>
    public ImpactSoundDataRepository LoadImpactSoundDataRepository()
    {
        impactsounddatarepository ??= new ImpactSoundDataRepository(logger, this);
        return impactsounddatarepository;
    }

    /// <summary>
    /// Gets IndexableSupportGemsRepository data.
    /// </summary>
    /// <returns>repository of IndexableSupportGemsRepository.</returns>
    public IndexableSupportGemsRepository LoadIndexableSupportGemsRepository()
    {
        indexablesupportgemsrepository ??= new IndexableSupportGemsRepository(logger, this);
        return indexablesupportgemsrepository;
    }

    /// <summary>
    /// Gets InfluenceExaltsRepository data.
    /// </summary>
    /// <returns>repository of InfluenceExaltsRepository.</returns>
    public InfluenceExaltsRepository LoadInfluenceExaltsRepository()
    {
        influenceexaltsrepository ??= new InfluenceExaltsRepository(logger, this);
        return influenceexaltsrepository;
    }

    /// <summary>
    /// Gets InfluenceTagsRepository data.
    /// </summary>
    /// <returns>repository of InfluenceTagsRepository.</returns>
    public InfluenceTagsRepository LoadInfluenceTagsRepository()
    {
        influencetagsrepository ??= new InfluenceTagsRepository(logger, this);
        return influencetagsrepository;
    }

    /// <summary>
    /// Gets InventoriesRepository data.
    /// </summary>
    /// <returns>repository of InventoriesRepository.</returns>
    public InventoriesRepository LoadInventoriesRepository()
    {
        inventoriesrepository ??= new InventoriesRepository(logger, this);
        return inventoriesrepository;
    }

    /// <summary>
    /// Gets ItemClassCategoriesRepository data.
    /// </summary>
    /// <returns>repository of ItemClassCategoriesRepository.</returns>
    public ItemClassCategoriesRepository LoadItemClassCategoriesRepository()
    {
        itemclasscategoriesrepository ??= new ItemClassCategoriesRepository(logger, this);
        return itemclasscategoriesrepository;
    }

    /// <summary>
    /// Gets ItemClassesRepository data.
    /// </summary>
    /// <returns>repository of ItemClassesRepository.</returns>
    public ItemClassesRepository LoadItemClassesRepository()
    {
        itemclassesrepository ??= new ItemClassesRepository(logger, this);
        return itemclassesrepository;
    }

    /// <summary>
    /// Gets ItemCostPerLevelRepository data.
    /// </summary>
    /// <returns>repository of ItemCostPerLevelRepository.</returns>
    public ItemCostPerLevelRepository LoadItemCostPerLevelRepository()
    {
        itemcostperlevelrepository ??= new ItemCostPerLevelRepository(logger, this);
        return itemcostperlevelrepository;
    }

    /// <summary>
    /// Gets ItemCostsRepository data.
    /// </summary>
    /// <returns>repository of ItemCostsRepository.</returns>
    public ItemCostsRepository LoadItemCostsRepository()
    {
        itemcostsrepository ??= new ItemCostsRepository(logger, this);
        return itemcostsrepository;
    }

    /// <summary>
    /// Gets ItemFrameTypeRepository data.
    /// </summary>
    /// <returns>repository of ItemFrameTypeRepository.</returns>
    public ItemFrameTypeRepository LoadItemFrameTypeRepository()
    {
        itemframetyperepository ??= new ItemFrameTypeRepository(logger, this);
        return itemframetyperepository;
    }

    /// <summary>
    /// Gets ItemExperienceTypesRepository data.
    /// </summary>
    /// <returns>repository of ItemExperienceTypesRepository.</returns>
    public ItemExperienceTypesRepository LoadItemExperienceTypesRepository()
    {
        itemexperiencetypesrepository ??= new ItemExperienceTypesRepository(logger, this);
        return itemexperiencetypesrepository;
    }

    /// <summary>
    /// Gets ItemExperiencePerLevelRepository data.
    /// </summary>
    /// <returns>repository of ItemExperiencePerLevelRepository.</returns>
    public ItemExperiencePerLevelRepository LoadItemExperiencePerLevelRepository()
    {
        itemexperienceperlevelrepository ??= new ItemExperiencePerLevelRepository(logger, this);
        return itemexperienceperlevelrepository;
    }

    /// <summary>
    /// Gets ItemisedVisualEffectRepository data.
    /// </summary>
    /// <returns>repository of ItemisedVisualEffectRepository.</returns>
    public ItemisedVisualEffectRepository LoadItemisedVisualEffectRepository()
    {
        itemisedvisualeffectrepository ??= new ItemisedVisualEffectRepository(logger, this);
        return itemisedvisualeffectrepository;
    }

    /// <summary>
    /// Gets ItemNoteCodeRepository data.
    /// </summary>
    /// <returns>repository of ItemNoteCodeRepository.</returns>
    public ItemNoteCodeRepository LoadItemNoteCodeRepository()
    {
        itemnotecoderepository ??= new ItemNoteCodeRepository(logger, this);
        return itemnotecoderepository;
    }

    /// <summary>
    /// Gets ItemShopTypeRepository data.
    /// </summary>
    /// <returns>repository of ItemShopTypeRepository.</returns>
    public ItemShopTypeRepository LoadItemShopTypeRepository()
    {
        itemshoptyperepository ??= new ItemShopTypeRepository(logger, this);
        return itemshoptyperepository;
    }

    /// <summary>
    /// Gets ItemStancesRepository data.
    /// </summary>
    /// <returns>repository of ItemStancesRepository.</returns>
    public ItemStancesRepository LoadItemStancesRepository()
    {
        itemstancesrepository ??= new ItemStancesRepository(logger, this);
        return itemstancesrepository;
    }

    /// <summary>
    /// Gets ItemThemesRepository data.
    /// </summary>
    /// <returns>repository of ItemThemesRepository.</returns>
    public ItemThemesRepository LoadItemThemesRepository()
    {
        itemthemesrepository ??= new ItemThemesRepository(logger, this);
        return itemthemesrepository;
    }

    /// <summary>
    /// Gets ItemVisualEffectRepository data.
    /// </summary>
    /// <returns>repository of ItemVisualEffectRepository.</returns>
    public ItemVisualEffectRepository LoadItemVisualEffectRepository()
    {
        itemvisualeffectrepository ??= new ItemVisualEffectRepository(logger, this);
        return itemvisualeffectrepository;
    }

    /// <summary>
    /// Gets ItemVisualHeldBodyModelRepository data.
    /// </summary>
    /// <returns>repository of ItemVisualHeldBodyModelRepository.</returns>
    public ItemVisualHeldBodyModelRepository LoadItemVisualHeldBodyModelRepository()
    {
        itemvisualheldbodymodelrepository ??= new ItemVisualHeldBodyModelRepository(logger, this);
        return itemvisualheldbodymodelrepository;
    }

    /// <summary>
    /// Gets ItemVisualIdentityRepository data.
    /// </summary>
    /// <returns>repository of ItemVisualIdentityRepository.</returns>
    public ItemVisualIdentityRepository LoadItemVisualIdentityRepository()
    {
        itemvisualidentityrepository ??= new ItemVisualIdentityRepository(logger, this);
        return itemvisualidentityrepository;
    }

    /// <summary>
    /// Gets JobAssassinationSpawnerGroupsRepository data.
    /// </summary>
    /// <returns>repository of JobAssassinationSpawnerGroupsRepository.</returns>
    public JobAssassinationSpawnerGroupsRepository LoadJobAssassinationSpawnerGroupsRepository()
    {
        jobassassinationspawnergroupsrepository ??= new JobAssassinationSpawnerGroupsRepository(logger, this);
        return jobassassinationspawnergroupsrepository;
    }

    /// <summary>
    /// Gets JobRaidBracketsRepository data.
    /// </summary>
    /// <returns>repository of JobRaidBracketsRepository.</returns>
    public JobRaidBracketsRepository LoadJobRaidBracketsRepository()
    {
        jobraidbracketsrepository ??= new JobRaidBracketsRepository(logger, this);
        return jobraidbracketsrepository;
    }

    /// <summary>
    /// Gets KillstreakThresholdsRepository data.
    /// </summary>
    /// <returns>repository of KillstreakThresholdsRepository.</returns>
    public KillstreakThresholdsRepository LoadKillstreakThresholdsRepository()
    {
        killstreakthresholdsrepository ??= new KillstreakThresholdsRepository(logger, this);
        return killstreakthresholdsrepository;
    }

    /// <summary>
    /// Gets LeagueFlagRepository data.
    /// </summary>
    /// <returns>repository of LeagueFlagRepository.</returns>
    public LeagueFlagRepository LoadLeagueFlagRepository()
    {
        leagueflagrepository ??= new LeagueFlagRepository(logger, this);
        return leagueflagrepository;
    }

    /// <summary>
    /// Gets LeagueInfoRepository data.
    /// </summary>
    /// <returns>repository of LeagueInfoRepository.</returns>
    public LeagueInfoRepository LoadLeagueInfoRepository()
    {
        leagueinforepository ??= new LeagueInfoRepository(logger, this);
        return leagueinforepository;
    }

    /// <summary>
    /// Gets LeagueProgressQuestFlagsRepository data.
    /// </summary>
    /// <returns>repository of LeagueProgressQuestFlagsRepository.</returns>
    public LeagueProgressQuestFlagsRepository LoadLeagueProgressQuestFlagsRepository()
    {
        leagueprogressquestflagsrepository ??= new LeagueProgressQuestFlagsRepository(logger, this);
        return leagueprogressquestflagsrepository;
    }

    /// <summary>
    /// Gets LeagueStaticRewardsRepository data.
    /// </summary>
    /// <returns>repository of LeagueStaticRewardsRepository.</returns>
    public LeagueStaticRewardsRepository LoadLeagueStaticRewardsRepository()
    {
        leaguestaticrewardsrepository ??= new LeagueStaticRewardsRepository(logger, this);
        return leaguestaticrewardsrepository;
    }

    /// <summary>
    /// Gets LevelRelativePlayerScalingRepository data.
    /// </summary>
    /// <returns>repository of LevelRelativePlayerScalingRepository.</returns>
    public LevelRelativePlayerScalingRepository LoadLevelRelativePlayerScalingRepository()
    {
        levelrelativeplayerscalingrepository ??= new LevelRelativePlayerScalingRepository(logger, this);
        return levelrelativeplayerscalingrepository;
    }

    /// <summary>
    /// Gets MagicMonsterLifeScalingPerLevelRepository data.
    /// </summary>
    /// <returns>repository of MagicMonsterLifeScalingPerLevelRepository.</returns>
    public MagicMonsterLifeScalingPerLevelRepository LoadMagicMonsterLifeScalingPerLevelRepository()
    {
        magicmonsterlifescalingperlevelrepository ??= new MagicMonsterLifeScalingPerLevelRepository(logger, this);
        return magicmonsterlifescalingperlevelrepository;
    }

    /// <summary>
    /// Gets MapCompletionAchievementsRepository data.
    /// </summary>
    /// <returns>repository of MapCompletionAchievementsRepository.</returns>
    public MapCompletionAchievementsRepository LoadMapCompletionAchievementsRepository()
    {
        mapcompletionachievementsrepository ??= new MapCompletionAchievementsRepository(logger, this);
        return mapcompletionachievementsrepository;
    }

    /// <summary>
    /// Gets MapConnectionsRepository data.
    /// </summary>
    /// <returns>repository of MapConnectionsRepository.</returns>
    public MapConnectionsRepository LoadMapConnectionsRepository()
    {
        mapconnectionsrepository ??= new MapConnectionsRepository(logger, this);
        return mapconnectionsrepository;
    }

    /// <summary>
    /// Gets MapCreationInformationRepository data.
    /// </summary>
    /// <returns>repository of MapCreationInformationRepository.</returns>
    public MapCreationInformationRepository LoadMapCreationInformationRepository()
    {
        mapcreationinformationrepository ??= new MapCreationInformationRepository(logger, this);
        return mapcreationinformationrepository;
    }

    /// <summary>
    /// Gets MapDeviceRecipesRepository data.
    /// </summary>
    /// <returns>repository of MapDeviceRecipesRepository.</returns>
    public MapDeviceRecipesRepository LoadMapDeviceRecipesRepository()
    {
        mapdevicerecipesrepository ??= new MapDeviceRecipesRepository(logger, this);
        return mapdevicerecipesrepository;
    }

    /// <summary>
    /// Gets MapDevicesRepository data.
    /// </summary>
    /// <returns>repository of MapDevicesRepository.</returns>
    public MapDevicesRepository LoadMapDevicesRepository()
    {
        mapdevicesrepository ??= new MapDevicesRepository(logger, this);
        return mapdevicesrepository;
    }

    /// <summary>
    /// Gets MapFragmentModsRepository data.
    /// </summary>
    /// <returns>repository of MapFragmentModsRepository.</returns>
    public MapFragmentModsRepository LoadMapFragmentModsRepository()
    {
        mapfragmentmodsrepository ??= new MapFragmentModsRepository(logger, this);
        return mapfragmentmodsrepository;
    }

    /// <summary>
    /// Gets MapInhabitantsRepository data.
    /// </summary>
    /// <returns>repository of MapInhabitantsRepository.</returns>
    public MapInhabitantsRepository LoadMapInhabitantsRepository()
    {
        mapinhabitantsrepository ??= new MapInhabitantsRepository(logger, this);
        return mapinhabitantsrepository;
    }

    /// <summary>
    /// Gets MapPinsRepository data.
    /// </summary>
    /// <returns>repository of MapPinsRepository.</returns>
    public MapPinsRepository LoadMapPinsRepository()
    {
        mappinsrepository ??= new MapPinsRepository(logger, this);
        return mappinsrepository;
    }

    /// <summary>
    /// Gets MapPurchaseCostsRepository data.
    /// </summary>
    /// <returns>repository of MapPurchaseCostsRepository.</returns>
    public MapPurchaseCostsRepository LoadMapPurchaseCostsRepository()
    {
        mappurchasecostsrepository ??= new MapPurchaseCostsRepository(logger, this);
        return mappurchasecostsrepository;
    }

    /// <summary>
    /// Gets MapsRepository data.
    /// </summary>
    /// <returns>repository of MapsRepository.</returns>
    public MapsRepository LoadMapsRepository()
    {
        mapsrepository ??= new MapsRepository(logger, this);
        return mapsrepository;
    }

    /// <summary>
    /// Gets MapSeriesRepository data.
    /// </summary>
    /// <returns>repository of MapSeriesRepository.</returns>
    public MapSeriesRepository LoadMapSeriesRepository()
    {
        mapseriesrepository ??= new MapSeriesRepository(logger, this);
        return mapseriesrepository;
    }

    /// <summary>
    /// Gets MapSeriesTiersRepository data.
    /// </summary>
    /// <returns>repository of MapSeriesTiersRepository.</returns>
    public MapSeriesTiersRepository LoadMapSeriesTiersRepository()
    {
        mapseriestiersrepository ??= new MapSeriesTiersRepository(logger, this);
        return mapseriestiersrepository;
    }

    /// <summary>
    /// Gets MapStashSpecialTypeEntriesRepository data.
    /// </summary>
    /// <returns>repository of MapStashSpecialTypeEntriesRepository.</returns>
    public MapStashSpecialTypeEntriesRepository LoadMapStashSpecialTypeEntriesRepository()
    {
        mapstashspecialtypeentriesrepository ??= new MapStashSpecialTypeEntriesRepository(logger, this);
        return mapstashspecialtypeentriesrepository;
    }

    /// <summary>
    /// Gets MapStashUniqueMapInfoRepository data.
    /// </summary>
    /// <returns>repository of MapStashUniqueMapInfoRepository.</returns>
    public MapStashUniqueMapInfoRepository LoadMapStashUniqueMapInfoRepository()
    {
        mapstashuniquemapinforepository ??= new MapStashUniqueMapInfoRepository(logger, this);
        return mapstashuniquemapinforepository;
    }

    /// <summary>
    /// Gets MapStatConditionsRepository data.
    /// </summary>
    /// <returns>repository of MapStatConditionsRepository.</returns>
    public MapStatConditionsRepository LoadMapStatConditionsRepository()
    {
        mapstatconditionsrepository ??= new MapStatConditionsRepository(logger, this);
        return mapstatconditionsrepository;
    }

    /// <summary>
    /// Gets MapTierAchievementsRepository data.
    /// </summary>
    /// <returns>repository of MapTierAchievementsRepository.</returns>
    public MapTierAchievementsRepository LoadMapTierAchievementsRepository()
    {
        maptierachievementsrepository ??= new MapTierAchievementsRepository(logger, this);
        return maptierachievementsrepository;
    }

    /// <summary>
    /// Gets MapTiersRepository data.
    /// </summary>
    /// <returns>repository of MapTiersRepository.</returns>
    public MapTiersRepository LoadMapTiersRepository()
    {
        maptiersrepository ??= new MapTiersRepository(logger, this);
        return maptiersrepository;
    }

    /// <summary>
    /// Gets MasterHideoutLevelsRepository data.
    /// </summary>
    /// <returns>repository of MasterHideoutLevelsRepository.</returns>
    public MasterHideoutLevelsRepository LoadMasterHideoutLevelsRepository()
    {
        masterhideoutlevelsrepository ??= new MasterHideoutLevelsRepository(logger, this);
        return masterhideoutlevelsrepository;
    }

    /// <summary>
    /// Gets MeleeRepository data.
    /// </summary>
    /// <returns>repository of MeleeRepository.</returns>
    public MeleeRepository LoadMeleeRepository()
    {
        meleerepository ??= new MeleeRepository(logger, this);
        return meleerepository;
    }

    /// <summary>
    /// Gets MeleeTrailsRepository data.
    /// </summary>
    /// <returns>repository of MeleeTrailsRepository.</returns>
    public MeleeTrailsRepository LoadMeleeTrailsRepository()
    {
        meleetrailsrepository ??= new MeleeTrailsRepository(logger, this);
        return meleetrailsrepository;
    }

    /// <summary>
    /// Gets MetamorphosisStashTabLayoutRepository data.
    /// </summary>
    /// <returns>repository of MetamorphosisStashTabLayoutRepository.</returns>
    public MetamorphosisStashTabLayoutRepository LoadMetamorphosisStashTabLayoutRepository()
    {
        metamorphosisstashtablayoutrepository ??= new MetamorphosisStashTabLayoutRepository(logger, this);
        return metamorphosisstashtablayoutrepository;
    }

    /// <summary>
    /// Gets MicroMigrationDataRepository data.
    /// </summary>
    /// <returns>repository of MicroMigrationDataRepository.</returns>
    public MicroMigrationDataRepository LoadMicroMigrationDataRepository()
    {
        micromigrationdatarepository ??= new MicroMigrationDataRepository(logger, this);
        return micromigrationdatarepository;
    }

    /// <summary>
    /// Gets MicrotransactionCategoryRepository data.
    /// </summary>
    /// <returns>repository of MicrotransactionCategoryRepository.</returns>
    public MicrotransactionCategoryRepository LoadMicrotransactionCategoryRepository()
    {
        microtransactioncategoryrepository ??= new MicrotransactionCategoryRepository(logger, this);
        return microtransactioncategoryrepository;
    }

    /// <summary>
    /// Gets MicrotransactionCharacterPortraitVariationsRepository data.
    /// </summary>
    /// <returns>repository of MicrotransactionCharacterPortraitVariationsRepository.</returns>
    public MicrotransactionCharacterPortraitVariationsRepository LoadMicrotransactionCharacterPortraitVariationsRepository()
    {
        microtransactioncharacterportraitvariationsrepository ??= new MicrotransactionCharacterPortraitVariationsRepository(logger, this);
        return microtransactioncharacterportraitvariationsrepository;
    }

    /// <summary>
    /// Gets MicrotransactionCombineFormulaRepository data.
    /// </summary>
    /// <returns>repository of MicrotransactionCombineFormulaRepository.</returns>
    public MicrotransactionCombineFormulaRepository LoadMicrotransactionCombineFormulaRepository()
    {
        microtransactioncombineformularepository ??= new MicrotransactionCombineFormulaRepository(logger, this);
        return microtransactioncombineformularepository;
    }

    /// <summary>
    /// Gets MicrotransactionCursorVariationsRepository data.
    /// </summary>
    /// <returns>repository of MicrotransactionCursorVariationsRepository.</returns>
    public MicrotransactionCursorVariationsRepository LoadMicrotransactionCursorVariationsRepository()
    {
        microtransactioncursorvariationsrepository ??= new MicrotransactionCursorVariationsRepository(logger, this);
        return microtransactioncursorvariationsrepository;
    }

    /// <summary>
    /// Gets MicrotransactionFireworksVariationsRepository data.
    /// </summary>
    /// <returns>repository of MicrotransactionFireworksVariationsRepository.</returns>
    public MicrotransactionFireworksVariationsRepository LoadMicrotransactionFireworksVariationsRepository()
    {
        microtransactionfireworksvariationsrepository ??= new MicrotransactionFireworksVariationsRepository(logger, this);
        return microtransactionfireworksvariationsrepository;
    }

    /// <summary>
    /// Gets MicrotransactionGemCategoryRepository data.
    /// </summary>
    /// <returns>repository of MicrotransactionGemCategoryRepository.</returns>
    public MicrotransactionGemCategoryRepository LoadMicrotransactionGemCategoryRepository()
    {
        microtransactiongemcategoryrepository ??= new MicrotransactionGemCategoryRepository(logger, this);
        return microtransactiongemcategoryrepository;
    }

    /// <summary>
    /// Gets MicrotransactionPeriodicCharacterEffectVariationsRepository data.
    /// </summary>
    /// <returns>repository of MicrotransactionPeriodicCharacterEffectVariationsRepository.</returns>
    public MicrotransactionPeriodicCharacterEffectVariationsRepository LoadMicrotransactionPeriodicCharacterEffectVariationsRepository()
    {
        microtransactionperiodiccharactereffectvariationsrepository ??= new MicrotransactionPeriodicCharacterEffectVariationsRepository(logger, this);
        return microtransactionperiodiccharactereffectvariationsrepository;
    }

    /// <summary>
    /// Gets MicrotransactionPortalVariationsRepository data.
    /// </summary>
    /// <returns>repository of MicrotransactionPortalVariationsRepository.</returns>
    public MicrotransactionPortalVariationsRepository LoadMicrotransactionPortalVariationsRepository()
    {
        microtransactionportalvariationsrepository ??= new MicrotransactionPortalVariationsRepository(logger, this);
        return microtransactionportalvariationsrepository;
    }

    /// <summary>
    /// Gets MicrotransactionRarityDisplayRepository data.
    /// </summary>
    /// <returns>repository of MicrotransactionRarityDisplayRepository.</returns>
    public MicrotransactionRarityDisplayRepository LoadMicrotransactionRarityDisplayRepository()
    {
        microtransactionraritydisplayrepository ??= new MicrotransactionRarityDisplayRepository(logger, this);
        return microtransactionraritydisplayrepository;
    }

    /// <summary>
    /// Gets MicrotransactionRecycleOutcomesRepository data.
    /// </summary>
    /// <returns>repository of MicrotransactionRecycleOutcomesRepository.</returns>
    public MicrotransactionRecycleOutcomesRepository LoadMicrotransactionRecycleOutcomesRepository()
    {
        microtransactionrecycleoutcomesrepository ??= new MicrotransactionRecycleOutcomesRepository(logger, this);
        return microtransactionrecycleoutcomesrepository;
    }

    /// <summary>
    /// Gets MicrotransactionRecycleSalvageValuesRepository data.
    /// </summary>
    /// <returns>repository of MicrotransactionRecycleSalvageValuesRepository.</returns>
    public MicrotransactionRecycleSalvageValuesRepository LoadMicrotransactionRecycleSalvageValuesRepository()
    {
        microtransactionrecyclesalvagevaluesrepository ??= new MicrotransactionRecycleSalvageValuesRepository(logger, this);
        return microtransactionrecyclesalvagevaluesrepository;
    }

    /// <summary>
    /// Gets MicrotransactionSlotRepository data.
    /// </summary>
    /// <returns>repository of MicrotransactionSlotRepository.</returns>
    public MicrotransactionSlotRepository LoadMicrotransactionSlotRepository()
    {
        microtransactionslotrepository ??= new MicrotransactionSlotRepository(logger, this);
        return microtransactionslotrepository;
    }

    /// <summary>
    /// Gets MicrotransactionSocialFrameVariationsRepository data.
    /// </summary>
    /// <returns>repository of MicrotransactionSocialFrameVariationsRepository.</returns>
    public MicrotransactionSocialFrameVariationsRepository LoadMicrotransactionSocialFrameVariationsRepository()
    {
        microtransactionsocialframevariationsrepository ??= new MicrotransactionSocialFrameVariationsRepository(logger, this);
        return microtransactionsocialframevariationsrepository;
    }

    /// <summary>
    /// Gets MinimapIconsRepository data.
    /// </summary>
    /// <returns>repository of MinimapIconsRepository.</returns>
    public MinimapIconsRepository LoadMinimapIconsRepository()
    {
        minimapiconsrepository ??= new MinimapIconsRepository(logger, this);
        return minimapiconsrepository;
    }

    /// <summary>
    /// Gets MiniQuestStatesRepository data.
    /// </summary>
    /// <returns>repository of MiniQuestStatesRepository.</returns>
    public MiniQuestStatesRepository LoadMiniQuestStatesRepository()
    {
        miniqueststatesrepository ??= new MiniQuestStatesRepository(logger, this);
        return miniqueststatesrepository;
    }

    /// <summary>
    /// Gets MiscAnimatedRepository data.
    /// </summary>
    /// <returns>repository of MiscAnimatedRepository.</returns>
    public MiscAnimatedRepository LoadMiscAnimatedRepository()
    {
        miscanimatedrepository ??= new MiscAnimatedRepository(logger, this);
        return miscanimatedrepository;
    }

    /// <summary>
    /// Gets MiscAnimatedArtVariationsRepository data.
    /// </summary>
    /// <returns>repository of MiscAnimatedArtVariationsRepository.</returns>
    public MiscAnimatedArtVariationsRepository LoadMiscAnimatedArtVariationsRepository()
    {
        miscanimatedartvariationsrepository ??= new MiscAnimatedArtVariationsRepository(logger, this);
        return miscanimatedartvariationsrepository;
    }

    /// <summary>
    /// Gets MiscBeamsRepository data.
    /// </summary>
    /// <returns>repository of MiscBeamsRepository.</returns>
    public MiscBeamsRepository LoadMiscBeamsRepository()
    {
        miscbeamsrepository ??= new MiscBeamsRepository(logger, this);
        return miscbeamsrepository;
    }

    /// <summary>
    /// Gets MiscBeamsArtVariationsRepository data.
    /// </summary>
    /// <returns>repository of MiscBeamsArtVariationsRepository.</returns>
    public MiscBeamsArtVariationsRepository LoadMiscBeamsArtVariationsRepository()
    {
        miscbeamsartvariationsrepository ??= new MiscBeamsArtVariationsRepository(logger, this);
        return miscbeamsartvariationsrepository;
    }

    /// <summary>
    /// Gets MiscEffectPacksRepository data.
    /// </summary>
    /// <returns>repository of MiscEffectPacksRepository.</returns>
    public MiscEffectPacksRepository LoadMiscEffectPacksRepository()
    {
        misceffectpacksrepository ??= new MiscEffectPacksRepository(logger, this);
        return misceffectpacksrepository;
    }

    /// <summary>
    /// Gets MiscEffectPacksArtVariationsRepository data.
    /// </summary>
    /// <returns>repository of MiscEffectPacksArtVariationsRepository.</returns>
    public MiscEffectPacksArtVariationsRepository LoadMiscEffectPacksArtVariationsRepository()
    {
        misceffectpacksartvariationsrepository ??= new MiscEffectPacksArtVariationsRepository(logger, this);
        return misceffectpacksartvariationsrepository;
    }

    /// <summary>
    /// Gets MiscObjectsRepository data.
    /// </summary>
    /// <returns>repository of MiscObjectsRepository.</returns>
    public MiscObjectsRepository LoadMiscObjectsRepository()
    {
        miscobjectsrepository ??= new MiscObjectsRepository(logger, this);
        return miscobjectsrepository;
    }

    /// <summary>
    /// Gets MiscObjectsArtVariationsRepository data.
    /// </summary>
    /// <returns>repository of MiscObjectsArtVariationsRepository.</returns>
    public MiscObjectsArtVariationsRepository LoadMiscObjectsArtVariationsRepository()
    {
        miscobjectsartvariationsrepository ??= new MiscObjectsArtVariationsRepository(logger, this);
        return miscobjectsartvariationsrepository;
    }

    /// <summary>
    /// Gets MissionFavourPerLevelRepository data.
    /// </summary>
    /// <returns>repository of MissionFavourPerLevelRepository.</returns>
    public MissionFavourPerLevelRepository LoadMissionFavourPerLevelRepository()
    {
        missionfavourperlevelrepository ??= new MissionFavourPerLevelRepository(logger, this);
        return missionfavourperlevelrepository;
    }

    /// <summary>
    /// Gets MissionTimerTypesRepository data.
    /// </summary>
    /// <returns>repository of MissionTimerTypesRepository.</returns>
    public MissionTimerTypesRepository LoadMissionTimerTypesRepository()
    {
        missiontimertypesrepository ??= new MissionTimerTypesRepository(logger, this);
        return missiontimertypesrepository;
    }

    /// <summary>
    /// Gets MissionTransitionTilesRepository data.
    /// </summary>
    /// <returns>repository of MissionTransitionTilesRepository.</returns>
    public MissionTransitionTilesRepository LoadMissionTransitionTilesRepository()
    {
        missiontransitiontilesrepository ??= new MissionTransitionTilesRepository(logger, this);
        return missiontransitiontilesrepository;
    }

    /// <summary>
    /// Gets ModEffectStatsRepository data.
    /// </summary>
    /// <returns>repository of ModEffectStatsRepository.</returns>
    public ModEffectStatsRepository LoadModEffectStatsRepository()
    {
        modeffectstatsrepository ??= new ModEffectStatsRepository(logger, this);
        return modeffectstatsrepository;
    }

    /// <summary>
    /// Gets ModEquivalenciesRepository data.
    /// </summary>
    /// <returns>repository of ModEquivalenciesRepository.</returns>
    public ModEquivalenciesRepository LoadModEquivalenciesRepository()
    {
        modequivalenciesrepository ??= new ModEquivalenciesRepository(logger, this);
        return modequivalenciesrepository;
    }

    /// <summary>
    /// Gets ModFamilyRepository data.
    /// </summary>
    /// <returns>repository of ModFamilyRepository.</returns>
    public ModFamilyRepository LoadModFamilyRepository()
    {
        modfamilyrepository ??= new ModFamilyRepository(logger, this);
        return modfamilyrepository;
    }

    /// <summary>
    /// Gets ModsRepository data.
    /// </summary>
    /// <returns>repository of ModsRepository.</returns>
    public ModsRepository LoadModsRepository()
    {
        modsrepository ??= new ModsRepository(logger, this);
        return modsrepository;
    }

    /// <summary>
    /// Gets ModSellPriceTypesRepository data.
    /// </summary>
    /// <returns>repository of ModSellPriceTypesRepository.</returns>
    public ModSellPriceTypesRepository LoadModSellPriceTypesRepository()
    {
        modsellpricetypesrepository ??= new ModSellPriceTypesRepository(logger, this);
        return modsellpricetypesrepository;
    }

    /// <summary>
    /// Gets ModTypeRepository data.
    /// </summary>
    /// <returns>repository of ModTypeRepository.</returns>
    public ModTypeRepository LoadModTypeRepository()
    {
        modtyperepository ??= new ModTypeRepository(logger, this);
        return modtyperepository;
    }

    /// <summary>
    /// Gets MonsterArmoursRepository data.
    /// </summary>
    /// <returns>repository of MonsterArmoursRepository.</returns>
    public MonsterArmoursRepository LoadMonsterArmoursRepository()
    {
        monsterarmoursrepository ??= new MonsterArmoursRepository(logger, this);
        return monsterarmoursrepository;
    }

    /// <summary>
    /// Gets MonsterBonusesRepository data.
    /// </summary>
    /// <returns>repository of MonsterBonusesRepository.</returns>
    public MonsterBonusesRepository LoadMonsterBonusesRepository()
    {
        monsterbonusesrepository ??= new MonsterBonusesRepository(logger, this);
        return monsterbonusesrepository;
    }

    /// <summary>
    /// Gets MonsterConditionalEffectPacksRepository data.
    /// </summary>
    /// <returns>repository of MonsterConditionalEffectPacksRepository.</returns>
    public MonsterConditionalEffectPacksRepository LoadMonsterConditionalEffectPacksRepository()
    {
        monsterconditionaleffectpacksrepository ??= new MonsterConditionalEffectPacksRepository(logger, this);
        return monsterconditionaleffectpacksrepository;
    }

    /// <summary>
    /// Gets MonsterConditionsRepository data.
    /// </summary>
    /// <returns>repository of MonsterConditionsRepository.</returns>
    public MonsterConditionsRepository LoadMonsterConditionsRepository()
    {
        monsterconditionsrepository ??= new MonsterConditionsRepository(logger, this);
        return monsterconditionsrepository;
    }

    /// <summary>
    /// Gets MonsterDeathAchievementsRepository data.
    /// </summary>
    /// <returns>repository of MonsterDeathAchievementsRepository.</returns>
    public MonsterDeathAchievementsRepository LoadMonsterDeathAchievementsRepository()
    {
        monsterdeathachievementsrepository ??= new MonsterDeathAchievementsRepository(logger, this);
        return monsterdeathachievementsrepository;
    }

    /// <summary>
    /// Gets MonsterDeathConditionsRepository data.
    /// </summary>
    /// <returns>repository of MonsterDeathConditionsRepository.</returns>
    public MonsterDeathConditionsRepository LoadMonsterDeathConditionsRepository()
    {
        monsterdeathconditionsrepository ??= new MonsterDeathConditionsRepository(logger, this);
        return monsterdeathconditionsrepository;
    }

    /// <summary>
    /// Gets MonsterGroupEntriesRepository data.
    /// </summary>
    /// <returns>repository of MonsterGroupEntriesRepository.</returns>
    public MonsterGroupEntriesRepository LoadMonsterGroupEntriesRepository()
    {
        monstergroupentriesrepository ??= new MonsterGroupEntriesRepository(logger, this);
        return monstergroupentriesrepository;
    }

    /// <summary>
    /// Gets MonsterHeightBracketsRepository data.
    /// </summary>
    /// <returns>repository of MonsterHeightBracketsRepository.</returns>
    public MonsterHeightBracketsRepository LoadMonsterHeightBracketsRepository()
    {
        monsterheightbracketsrepository ??= new MonsterHeightBracketsRepository(logger, this);
        return monsterheightbracketsrepository;
    }

    /// <summary>
    /// Gets MonsterHeightsRepository data.
    /// </summary>
    /// <returns>repository of MonsterHeightsRepository.</returns>
    public MonsterHeightsRepository LoadMonsterHeightsRepository()
    {
        monsterheightsrepository ??= new MonsterHeightsRepository(logger, this);
        return monsterheightsrepository;
    }

    /// <summary>
    /// Gets MonsterMapBossDifficultyRepository data.
    /// </summary>
    /// <returns>repository of MonsterMapBossDifficultyRepository.</returns>
    public MonsterMapBossDifficultyRepository LoadMonsterMapBossDifficultyRepository()
    {
        monstermapbossdifficultyrepository ??= new MonsterMapBossDifficultyRepository(logger, this);
        return monstermapbossdifficultyrepository;
    }

    /// <summary>
    /// Gets MonsterMapDifficultyRepository data.
    /// </summary>
    /// <returns>repository of MonsterMapDifficultyRepository.</returns>
    public MonsterMapDifficultyRepository LoadMonsterMapDifficultyRepository()
    {
        monstermapdifficultyrepository ??= new MonsterMapDifficultyRepository(logger, this);
        return monstermapdifficultyrepository;
    }

    /// <summary>
    /// Gets MonsterMortarRepository data.
    /// </summary>
    /// <returns>repository of MonsterMortarRepository.</returns>
    public MonsterMortarRepository LoadMonsterMortarRepository()
    {
        monstermortarrepository ??= new MonsterMortarRepository(logger, this);
        return monstermortarrepository;
    }

    /// <summary>
    /// Gets MonsterPackCountsRepository data.
    /// </summary>
    /// <returns>repository of MonsterPackCountsRepository.</returns>
    public MonsterPackCountsRepository LoadMonsterPackCountsRepository()
    {
        monsterpackcountsrepository ??= new MonsterPackCountsRepository(logger, this);
        return monsterpackcountsrepository;
    }

    /// <summary>
    /// Gets MonsterPackEntriesRepository data.
    /// </summary>
    /// <returns>repository of MonsterPackEntriesRepository.</returns>
    public MonsterPackEntriesRepository LoadMonsterPackEntriesRepository()
    {
        monsterpackentriesrepository ??= new MonsterPackEntriesRepository(logger, this);
        return monsterpackentriesrepository;
    }

    /// <summary>
    /// Gets MonsterPacksRepository data.
    /// </summary>
    /// <returns>repository of MonsterPacksRepository.</returns>
    public MonsterPacksRepository LoadMonsterPacksRepository()
    {
        monsterpacksrepository ??= new MonsterPacksRepository(logger, this);
        return monsterpacksrepository;
    }

    /// <summary>
    /// Gets MonsterProjectileAttackRepository data.
    /// </summary>
    /// <returns>repository of MonsterProjectileAttackRepository.</returns>
    public MonsterProjectileAttackRepository LoadMonsterProjectileAttackRepository()
    {
        monsterprojectileattackrepository ??= new MonsterProjectileAttackRepository(logger, this);
        return monsterprojectileattackrepository;
    }

    /// <summary>
    /// Gets MonsterProjectileSpellRepository data.
    /// </summary>
    /// <returns>repository of MonsterProjectileSpellRepository.</returns>
    public MonsterProjectileSpellRepository LoadMonsterProjectileSpellRepository()
    {
        monsterprojectilespellrepository ??= new MonsterProjectileSpellRepository(logger, this);
        return monsterprojectilespellrepository;
    }

    /// <summary>
    /// Gets MonsterResistancesRepository data.
    /// </summary>
    /// <returns>repository of MonsterResistancesRepository.</returns>
    public MonsterResistancesRepository LoadMonsterResistancesRepository()
    {
        monsterresistancesrepository ??= new MonsterResistancesRepository(logger, this);
        return monsterresistancesrepository;
    }

    /// <summary>
    /// Gets MonsterSegmentsRepository data.
    /// </summary>
    /// <returns>repository of MonsterSegmentsRepository.</returns>
    public MonsterSegmentsRepository LoadMonsterSegmentsRepository()
    {
        monstersegmentsrepository ??= new MonsterSegmentsRepository(logger, this);
        return monstersegmentsrepository;
    }

    /// <summary>
    /// Gets MonsterSpawnerGroupsRepository data.
    /// </summary>
    /// <returns>repository of MonsterSpawnerGroupsRepository.</returns>
    public MonsterSpawnerGroupsRepository LoadMonsterSpawnerGroupsRepository()
    {
        monsterspawnergroupsrepository ??= new MonsterSpawnerGroupsRepository(logger, this);
        return monsterspawnergroupsrepository;
    }

    /// <summary>
    /// Gets MonsterSpawnerGroupsPerLevelRepository data.
    /// </summary>
    /// <returns>repository of MonsterSpawnerGroupsPerLevelRepository.</returns>
    public MonsterSpawnerGroupsPerLevelRepository LoadMonsterSpawnerGroupsPerLevelRepository()
    {
        monsterspawnergroupsperlevelrepository ??= new MonsterSpawnerGroupsPerLevelRepository(logger, this);
        return monsterspawnergroupsperlevelrepository;
    }

    /// <summary>
    /// Gets MonsterSpawnerOverridesRepository data.
    /// </summary>
    /// <returns>repository of MonsterSpawnerOverridesRepository.</returns>
    public MonsterSpawnerOverridesRepository LoadMonsterSpawnerOverridesRepository()
    {
        monsterspawneroverridesrepository ??= new MonsterSpawnerOverridesRepository(logger, this);
        return monsterspawneroverridesrepository;
    }

    /// <summary>
    /// Gets MonsterTypesRepository data.
    /// </summary>
    /// <returns>repository of MonsterTypesRepository.</returns>
    public MonsterTypesRepository LoadMonsterTypesRepository()
    {
        monstertypesrepository ??= new MonsterTypesRepository(logger, this);
        return monstertypesrepository;
    }

    /// <summary>
    /// Gets MonsterVarietiesRepository data.
    /// </summary>
    /// <returns>repository of MonsterVarietiesRepository.</returns>
    public MonsterVarietiesRepository LoadMonsterVarietiesRepository()
    {
        monstervarietiesrepository ??= new MonsterVarietiesRepository(logger, this);
        return monstervarietiesrepository;
    }

    /// <summary>
    /// Gets MonsterVarietiesArtVariationsRepository data.
    /// </summary>
    /// <returns>repository of MonsterVarietiesArtVariationsRepository.</returns>
    public MonsterVarietiesArtVariationsRepository LoadMonsterVarietiesArtVariationsRepository()
    {
        monstervarietiesartvariationsrepository ??= new MonsterVarietiesArtVariationsRepository(logger, this);
        return monstervarietiesartvariationsrepository;
    }

    /// <summary>
    /// Gets MouseCursorSizeSettingsRepository data.
    /// </summary>
    /// <returns>repository of MouseCursorSizeSettingsRepository.</returns>
    public MouseCursorSizeSettingsRepository LoadMouseCursorSizeSettingsRepository()
    {
        mousecursorsizesettingsrepository ??= new MouseCursorSizeSettingsRepository(logger, this);
        return mousecursorsizesettingsrepository;
    }

    /// <summary>
    /// Gets MoveDaemonRepository data.
    /// </summary>
    /// <returns>repository of MoveDaemonRepository.</returns>
    public MoveDaemonRepository LoadMoveDaemonRepository()
    {
        movedaemonrepository ??= new MoveDaemonRepository(logger, this);
        return movedaemonrepository;
    }

    /// <summary>
    /// Gets MTXSetBonusRepository data.
    /// </summary>
    /// <returns>repository of MTXSetBonusRepository.</returns>
    public MTXSetBonusRepository LoadMTXSetBonusRepository()
    {
        mtxsetbonusrepository ??= new MTXSetBonusRepository(logger, this);
        return mtxsetbonusrepository;
    }

    /// <summary>
    /// Gets MultiPartAchievementAreasRepository data.
    /// </summary>
    /// <returns>repository of MultiPartAchievementAreasRepository.</returns>
    public MultiPartAchievementAreasRepository LoadMultiPartAchievementAreasRepository()
    {
        multipartachievementareasrepository ??= new MultiPartAchievementAreasRepository(logger, this);
        return multipartachievementareasrepository;
    }

    /// <summary>
    /// Gets MultiPartAchievementConditionsRepository data.
    /// </summary>
    /// <returns>repository of MultiPartAchievementConditionsRepository.</returns>
    public MultiPartAchievementConditionsRepository LoadMultiPartAchievementConditionsRepository()
    {
        multipartachievementconditionsrepository ??= new MultiPartAchievementConditionsRepository(logger, this);
        return multipartachievementconditionsrepository;
    }

    /// <summary>
    /// Gets MultiPartAchievementsRepository data.
    /// </summary>
    /// <returns>repository of MultiPartAchievementsRepository.</returns>
    public MultiPartAchievementsRepository LoadMultiPartAchievementsRepository()
    {
        multipartachievementsrepository ??= new MultiPartAchievementsRepository(logger, this);
        return multipartachievementsrepository;
    }

    /// <summary>
    /// Gets MusicRepository data.
    /// </summary>
    /// <returns>repository of MusicRepository.</returns>
    public MusicRepository LoadMusicRepository()
    {
        musicrepository ??= new MusicRepository(logger, this);
        return musicrepository;
    }

    /// <summary>
    /// Gets MusicCategoriesRepository data.
    /// </summary>
    /// <returns>repository of MusicCategoriesRepository.</returns>
    public MusicCategoriesRepository LoadMusicCategoriesRepository()
    {
        musiccategoriesrepository ??= new MusicCategoriesRepository(logger, this);
        return musiccategoriesrepository;
    }

    /// <summary>
    /// Gets MysteryBoxesRepository data.
    /// </summary>
    /// <returns>repository of MysteryBoxesRepository.</returns>
    public MysteryBoxesRepository LoadMysteryBoxesRepository()
    {
        mysteryboxesrepository ??= new MysteryBoxesRepository(logger, this);
        return mysteryboxesrepository;
    }

    /// <summary>
    /// Gets NearbyMonsterConditionsRepository data.
    /// </summary>
    /// <returns>repository of NearbyMonsterConditionsRepository.</returns>
    public NearbyMonsterConditionsRepository LoadNearbyMonsterConditionsRepository()
    {
        nearbymonsterconditionsrepository ??= new NearbyMonsterConditionsRepository(logger, this);
        return nearbymonsterconditionsrepository;
    }

    /// <summary>
    /// Gets NetTiersRepository data.
    /// </summary>
    /// <returns>repository of NetTiersRepository.</returns>
    public NetTiersRepository LoadNetTiersRepository()
    {
        nettiersrepository ??= new NetTiersRepository(logger, this);
        return nettiersrepository;
    }

    /// <summary>
    /// Gets NotificationsRepository data.
    /// </summary>
    /// <returns>repository of NotificationsRepository.</returns>
    public NotificationsRepository LoadNotificationsRepository()
    {
        notificationsrepository ??= new NotificationsRepository(logger, this);
        return notificationsrepository;
    }

    /// <summary>
    /// Gets NPCAudioRepository data.
    /// </summary>
    /// <returns>repository of NPCAudioRepository.</returns>
    public NPCAudioRepository LoadNPCAudioRepository()
    {
        npcaudiorepository ??= new NPCAudioRepository(logger, this);
        return npcaudiorepository;
    }

    /// <summary>
    /// Gets NPCConversationsRepository data.
    /// </summary>
    /// <returns>repository of NPCConversationsRepository.</returns>
    public NPCConversationsRepository LoadNPCConversationsRepository()
    {
        npcconversationsrepository ??= new NPCConversationsRepository(logger, this);
        return npcconversationsrepository;
    }

    /// <summary>
    /// Gets NPCDialogueStylesRepository data.
    /// </summary>
    /// <returns>repository of NPCDialogueStylesRepository.</returns>
    public NPCDialogueStylesRepository LoadNPCDialogueStylesRepository()
    {
        npcdialoguestylesrepository ??= new NPCDialogueStylesRepository(logger, this);
        return npcdialoguestylesrepository;
    }

    /// <summary>
    /// Gets NPCFollowerVariationsRepository data.
    /// </summary>
    /// <returns>repository of NPCFollowerVariationsRepository.</returns>
    public NPCFollowerVariationsRepository LoadNPCFollowerVariationsRepository()
    {
        npcfollowervariationsrepository ??= new NPCFollowerVariationsRepository(logger, this);
        return npcfollowervariationsrepository;
    }

    /// <summary>
    /// Gets NPCMasterRepository data.
    /// </summary>
    /// <returns>repository of NPCMasterRepository.</returns>
    public NPCMasterRepository LoadNPCMasterRepository()
    {
        npcmasterrepository ??= new NPCMasterRepository(logger, this);
        return npcmasterrepository;
    }

    /// <summary>
    /// Gets NPCPortraitsRepository data.
    /// </summary>
    /// <returns>repository of NPCPortraitsRepository.</returns>
    public NPCPortraitsRepository LoadNPCPortraitsRepository()
    {
        npcportraitsrepository ??= new NPCPortraitsRepository(logger, this);
        return npcportraitsrepository;
    }

    /// <summary>
    /// Gets NPCsRepository data.
    /// </summary>
    /// <returns>repository of NPCsRepository.</returns>
    public NPCsRepository LoadNPCsRepository()
    {
        npcsrepository ??= new NPCsRepository(logger, this);
        return npcsrepository;
    }

    /// <summary>
    /// Gets NPCShopRepository data.
    /// </summary>
    /// <returns>repository of NPCShopRepository.</returns>
    public NPCShopRepository LoadNPCShopRepository()
    {
        npcshoprepository ??= new NPCShopRepository(logger, this);
        return npcshoprepository;
    }

    /// <summary>
    /// Gets NPCShopsRepository data.
    /// </summary>
    /// <returns>repository of NPCShopsRepository.</returns>
    public NPCShopsRepository LoadNPCShopsRepository()
    {
        npcshopsrepository ??= new NPCShopsRepository(logger, this);
        return npcshopsrepository;
    }

    /// <summary>
    /// Gets NPCTalkRepository data.
    /// </summary>
    /// <returns>repository of NPCTalkRepository.</returns>
    public NPCTalkRepository LoadNPCTalkRepository()
    {
        npctalkrepository ??= new NPCTalkRepository(logger, this);
        return npctalkrepository;
    }

    /// <summary>
    /// Gets NPCTalkCategoryRepository data.
    /// </summary>
    /// <returns>repository of NPCTalkCategoryRepository.</returns>
    public NPCTalkCategoryRepository LoadNPCTalkCategoryRepository()
    {
        npctalkcategoryrepository ??= new NPCTalkCategoryRepository(logger, this);
        return npctalkcategoryrepository;
    }

    /// <summary>
    /// Gets NPCTalkConsoleQuickActionsRepository data.
    /// </summary>
    /// <returns>repository of NPCTalkConsoleQuickActionsRepository.</returns>
    public NPCTalkConsoleQuickActionsRepository LoadNPCTalkConsoleQuickActionsRepository()
    {
        npctalkconsolequickactionsrepository ??= new NPCTalkConsoleQuickActionsRepository(logger, this);
        return npctalkconsolequickactionsrepository;
    }

    /// <summary>
    /// Gets NPCTextAudioRepository data.
    /// </summary>
    /// <returns>repository of NPCTextAudioRepository.</returns>
    public NPCTextAudioRepository LoadNPCTextAudioRepository()
    {
        npctextaudiorepository ??= new NPCTextAudioRepository(logger, this);
        return npctextaudiorepository;
    }

    /// <summary>
    /// Gets OnKillAchievementsRepository data.
    /// </summary>
    /// <returns>repository of OnKillAchievementsRepository.</returns>
    public OnKillAchievementsRepository LoadOnKillAchievementsRepository()
    {
        onkillachievementsrepository ??= new OnKillAchievementsRepository(logger, this);
        return onkillachievementsrepository;
    }

    /// <summary>
    /// Gets PackFormationRepository data.
    /// </summary>
    /// <returns>repository of PackFormationRepository.</returns>
    public PackFormationRepository LoadPackFormationRepository()
    {
        packformationrepository ??= new PackFormationRepository(logger, this);
        return packformationrepository;
    }

    /// <summary>
    /// Gets PassiveJewelRadiiRepository data.
    /// </summary>
    /// <returns>repository of PassiveJewelRadiiRepository.</returns>
    public PassiveJewelRadiiRepository LoadPassiveJewelRadiiRepository()
    {
        passivejewelradiirepository ??= new PassiveJewelRadiiRepository(logger, this);
        return passivejewelradiirepository;
    }

    /// <summary>
    /// Gets PassiveJewelSlotsRepository data.
    /// </summary>
    /// <returns>repository of PassiveJewelSlotsRepository.</returns>
    public PassiveJewelSlotsRepository LoadPassiveJewelSlotsRepository()
    {
        passivejewelslotsrepository ??= new PassiveJewelSlotsRepository(logger, this);
        return passivejewelslotsrepository;
    }

    /// <summary>
    /// Gets PassiveSkillFilterCatagoriesRepository data.
    /// </summary>
    /// <returns>repository of PassiveSkillFilterCatagoriesRepository.</returns>
    public PassiveSkillFilterCatagoriesRepository LoadPassiveSkillFilterCatagoriesRepository()
    {
        passiveskillfiltercatagoriesrepository ??= new PassiveSkillFilterCatagoriesRepository(logger, this);
        return passiveskillfiltercatagoriesrepository;
    }

    /// <summary>
    /// Gets PassiveSkillFilterOptionsRepository data.
    /// </summary>
    /// <returns>repository of PassiveSkillFilterOptionsRepository.</returns>
    public PassiveSkillFilterOptionsRepository LoadPassiveSkillFilterOptionsRepository()
    {
        passiveskillfilteroptionsrepository ??= new PassiveSkillFilterOptionsRepository(logger, this);
        return passiveskillfilteroptionsrepository;
    }

    /// <summary>
    /// Gets PassiveSkillMasteryGroupsRepository data.
    /// </summary>
    /// <returns>repository of PassiveSkillMasteryGroupsRepository.</returns>
    public PassiveSkillMasteryGroupsRepository LoadPassiveSkillMasteryGroupsRepository()
    {
        passiveskillmasterygroupsrepository ??= new PassiveSkillMasteryGroupsRepository(logger, this);
        return passiveskillmasterygroupsrepository;
    }

    /// <summary>
    /// Gets PassiveSkillMasteryEffectsRepository data.
    /// </summary>
    /// <returns>repository of PassiveSkillMasteryEffectsRepository.</returns>
    public PassiveSkillMasteryEffectsRepository LoadPassiveSkillMasteryEffectsRepository()
    {
        passiveskillmasteryeffectsrepository ??= new PassiveSkillMasteryEffectsRepository(logger, this);
        return passiveskillmasteryeffectsrepository;
    }

    /// <summary>
    /// Gets PassiveSkillsRepository data.
    /// </summary>
    /// <returns>repository of PassiveSkillsRepository.</returns>
    public PassiveSkillsRepository LoadPassiveSkillsRepository()
    {
        passiveskillsrepository ??= new PassiveSkillsRepository(logger, this);
        return passiveskillsrepository;
    }

    /// <summary>
    /// Gets PassiveSkillStatCategoriesRepository data.
    /// </summary>
    /// <returns>repository of PassiveSkillStatCategoriesRepository.</returns>
    public PassiveSkillStatCategoriesRepository LoadPassiveSkillStatCategoriesRepository()
    {
        passiveskillstatcategoriesrepository ??= new PassiveSkillStatCategoriesRepository(logger, this);
        return passiveskillstatcategoriesrepository;
    }

    /// <summary>
    /// Gets PassiveSkillTreesRepository data.
    /// </summary>
    /// <returns>repository of PassiveSkillTreesRepository.</returns>
    public PassiveSkillTreesRepository LoadPassiveSkillTreesRepository()
    {
        passiveskilltreesrepository ??= new PassiveSkillTreesRepository(logger, this);
        return passiveskilltreesrepository;
    }

    /// <summary>
    /// Gets PassiveSkillTreeTutorialRepository data.
    /// </summary>
    /// <returns>repository of PassiveSkillTreeTutorialRepository.</returns>
    public PassiveSkillTreeTutorialRepository LoadPassiveSkillTreeTutorialRepository()
    {
        passiveskilltreetutorialrepository ??= new PassiveSkillTreeTutorialRepository(logger, this);
        return passiveskilltreetutorialrepository;
    }

    /// <summary>
    /// Gets PassiveSkillTreeUIArtRepository data.
    /// </summary>
    /// <returns>repository of PassiveSkillTreeUIArtRepository.</returns>
    public PassiveSkillTreeUIArtRepository LoadPassiveSkillTreeUIArtRepository()
    {
        passiveskilltreeuiartrepository ??= new PassiveSkillTreeUIArtRepository(logger, this);
        return passiveskilltreeuiartrepository;
    }

    /// <summary>
    /// Gets PassiveTreeExpansionJewelsRepository data.
    /// </summary>
    /// <returns>repository of PassiveTreeExpansionJewelsRepository.</returns>
    public PassiveTreeExpansionJewelsRepository LoadPassiveTreeExpansionJewelsRepository()
    {
        passivetreeexpansionjewelsrepository ??= new PassiveTreeExpansionJewelsRepository(logger, this);
        return passivetreeexpansionjewelsrepository;
    }

    /// <summary>
    /// Gets PassiveTreeExpansionJewelSizesRepository data.
    /// </summary>
    /// <returns>repository of PassiveTreeExpansionJewelSizesRepository.</returns>
    public PassiveTreeExpansionJewelSizesRepository LoadPassiveTreeExpansionJewelSizesRepository()
    {
        passivetreeexpansionjewelsizesrepository ??= new PassiveTreeExpansionJewelSizesRepository(logger, this);
        return passivetreeexpansionjewelsizesrepository;
    }

    /// <summary>
    /// Gets PassiveTreeExpansionSkillsRepository data.
    /// </summary>
    /// <returns>repository of PassiveTreeExpansionSkillsRepository.</returns>
    public PassiveTreeExpansionSkillsRepository LoadPassiveTreeExpansionSkillsRepository()
    {
        passivetreeexpansionskillsrepository ??= new PassiveTreeExpansionSkillsRepository(logger, this);
        return passivetreeexpansionskillsrepository;
    }

    /// <summary>
    /// Gets PassiveTreeExpansionSpecialSkillsRepository data.
    /// </summary>
    /// <returns>repository of PassiveTreeExpansionSpecialSkillsRepository.</returns>
    public PassiveTreeExpansionSpecialSkillsRepository LoadPassiveTreeExpansionSpecialSkillsRepository()
    {
        passivetreeexpansionspecialskillsrepository ??= new PassiveTreeExpansionSpecialSkillsRepository(logger, this);
        return passivetreeexpansionspecialskillsrepository;
    }

    /// <summary>
    /// Gets PCBangRewardMicrosRepository data.
    /// </summary>
    /// <returns>repository of PCBangRewardMicrosRepository.</returns>
    public PCBangRewardMicrosRepository LoadPCBangRewardMicrosRepository()
    {
        pcbangrewardmicrosrepository ??= new PCBangRewardMicrosRepository(logger, this);
        return pcbangrewardmicrosrepository;
    }

    /// <summary>
    /// Gets PetRepository data.
    /// </summary>
    /// <returns>repository of PetRepository.</returns>
    public PetRepository LoadPetRepository()
    {
        petrepository ??= new PetRepository(logger, this);
        return petrepository;
    }

    /// <summary>
    /// Gets PlayerConditionsRepository data.
    /// </summary>
    /// <returns>repository of PlayerConditionsRepository.</returns>
    public PlayerConditionsRepository LoadPlayerConditionsRepository()
    {
        playerconditionsrepository ??= new PlayerConditionsRepository(logger, this);
        return playerconditionsrepository;
    }

    /// <summary>
    /// Gets PlayerTradeWhisperFormatsRepository data.
    /// </summary>
    /// <returns>repository of PlayerTradeWhisperFormatsRepository.</returns>
    public PlayerTradeWhisperFormatsRepository LoadPlayerTradeWhisperFormatsRepository()
    {
        playertradewhisperformatsrepository ??= new PlayerTradeWhisperFormatsRepository(logger, this);
        return playertradewhisperformatsrepository;
    }

    /// <summary>
    /// Gets PreloadGroupsRepository data.
    /// </summary>
    /// <returns>repository of PreloadGroupsRepository.</returns>
    public PreloadGroupsRepository LoadPreloadGroupsRepository()
    {
        preloadgroupsrepository ??= new PreloadGroupsRepository(logger, this);
        return preloadgroupsrepository;
    }

    /// <summary>
    /// Gets ProjectilesRepository data.
    /// </summary>
    /// <returns>repository of ProjectilesRepository.</returns>
    public ProjectilesRepository LoadProjectilesRepository()
    {
        projectilesrepository ??= new ProjectilesRepository(logger, this);
        return projectilesrepository;
    }

    /// <summary>
    /// Gets ProjectilesArtVariationsRepository data.
    /// </summary>
    /// <returns>repository of ProjectilesArtVariationsRepository.</returns>
    public ProjectilesArtVariationsRepository LoadProjectilesArtVariationsRepository()
    {
        projectilesartvariationsrepository ??= new ProjectilesArtVariationsRepository(logger, this);
        return projectilesartvariationsrepository;
    }

    /// <summary>
    /// Gets ProjectileVariationsRepository data.
    /// </summary>
    /// <returns>repository of ProjectileVariationsRepository.</returns>
    public ProjectileVariationsRepository LoadProjectileVariationsRepository()
    {
        projectilevariationsrepository ??= new ProjectileVariationsRepository(logger, this);
        return projectilevariationsrepository;
    }

    /// <summary>
    /// Gets PVPTypesRepository data.
    /// </summary>
    /// <returns>repository of PVPTypesRepository.</returns>
    public PVPTypesRepository LoadPVPTypesRepository()
    {
        pvptypesrepository ??= new PVPTypesRepository(logger, this);
        return pvptypesrepository;
    }

    /// <summary>
    /// Gets QuestRepository data.
    /// </summary>
    /// <returns>repository of QuestRepository.</returns>
    public QuestRepository LoadQuestRepository()
    {
        questrepository ??= new QuestRepository(logger, this);
        return questrepository;
    }

    /// <summary>
    /// Gets QuestAchievementsRepository data.
    /// </summary>
    /// <returns>repository of QuestAchievementsRepository.</returns>
    public QuestAchievementsRepository LoadQuestAchievementsRepository()
    {
        questachievementsrepository ??= new QuestAchievementsRepository(logger, this);
        return questachievementsrepository;
    }

    /// <summary>
    /// Gets QuestFlagsRepository data.
    /// </summary>
    /// <returns>repository of QuestFlagsRepository.</returns>
    public QuestFlagsRepository LoadQuestFlagsRepository()
    {
        questflagsrepository ??= new QuestFlagsRepository(logger, this);
        return questflagsrepository;
    }

    /// <summary>
    /// Gets QuestItemsRepository data.
    /// </summary>
    /// <returns>repository of QuestItemsRepository.</returns>
    public QuestItemsRepository LoadQuestItemsRepository()
    {
        questitemsrepository ??= new QuestItemsRepository(logger, this);
        return questitemsrepository;
    }

    /// <summary>
    /// Gets QuestRewardOffersRepository data.
    /// </summary>
    /// <returns>repository of QuestRewardOffersRepository.</returns>
    public QuestRewardOffersRepository LoadQuestRewardOffersRepository()
    {
        questrewardoffersrepository ??= new QuestRewardOffersRepository(logger, this);
        return questrewardoffersrepository;
    }

    /// <summary>
    /// Gets QuestRewardsRepository data.
    /// </summary>
    /// <returns>repository of QuestRewardsRepository.</returns>
    public QuestRewardsRepository LoadQuestRewardsRepository()
    {
        questrewardsrepository ??= new QuestRewardsRepository(logger, this);
        return questrewardsrepository;
    }

    /// <summary>
    /// Gets QuestStatesRepository data.
    /// </summary>
    /// <returns>repository of QuestStatesRepository.</returns>
    public QuestStatesRepository LoadQuestStatesRepository()
    {
        queststatesrepository ??= new QuestStatesRepository(logger, this);
        return queststatesrepository;
    }

    /// <summary>
    /// Gets QuestStaticRewardsRepository data.
    /// </summary>
    /// <returns>repository of QuestStaticRewardsRepository.</returns>
    public QuestStaticRewardsRepository LoadQuestStaticRewardsRepository()
    {
        queststaticrewardsrepository ??= new QuestStaticRewardsRepository(logger, this);
        return queststaticrewardsrepository;
    }

    /// <summary>
    /// Gets QuestTrackerGroupRepository data.
    /// </summary>
    /// <returns>repository of QuestTrackerGroupRepository.</returns>
    public QuestTrackerGroupRepository LoadQuestTrackerGroupRepository()
    {
        questtrackergrouprepository ??= new QuestTrackerGroupRepository(logger, this);
        return questtrackergrouprepository;
    }

    /// <summary>
    /// Gets QuestTypeRepository data.
    /// </summary>
    /// <returns>repository of QuestTypeRepository.</returns>
    public QuestTypeRepository LoadQuestTypeRepository()
    {
        questtyperepository ??= new QuestTypeRepository(logger, this);
        return questtyperepository;
    }

    /// <summary>
    /// Gets RacesRepository data.
    /// </summary>
    /// <returns>repository of RacesRepository.</returns>
    public RacesRepository LoadRacesRepository()
    {
        racesrepository ??= new RacesRepository(logger, this);
        return racesrepository;
    }

    /// <summary>
    /// Gets RaceTimesRepository data.
    /// </summary>
    /// <returns>repository of RaceTimesRepository.</returns>
    public RaceTimesRepository LoadRaceTimesRepository()
    {
        racetimesrepository ??= new RaceTimesRepository(logger, this);
        return racetimesrepository;
    }

    /// <summary>
    /// Gets RareMonsterLifeScalingPerLevelRepository data.
    /// </summary>
    /// <returns>repository of RareMonsterLifeScalingPerLevelRepository.</returns>
    public RareMonsterLifeScalingPerLevelRepository LoadRareMonsterLifeScalingPerLevelRepository()
    {
        raremonsterlifescalingperlevelrepository ??= new RareMonsterLifeScalingPerLevelRepository(logger, this);
        return raremonsterlifescalingperlevelrepository;
    }

    /// <summary>
    /// Gets RarityRepository data.
    /// </summary>
    /// <returns>repository of RarityRepository.</returns>
    public RarityRepository LoadRarityRepository()
    {
        rarityrepository ??= new RarityRepository(logger, this);
        return rarityrepository;
    }

    /// <summary>
    /// Gets RealmsRepository data.
    /// </summary>
    /// <returns>repository of RealmsRepository.</returns>
    public RealmsRepository LoadRealmsRepository()
    {
        realmsrepository ??= new RealmsRepository(logger, this);
        return realmsrepository;
    }

    /// <summary>
    /// Gets RecipeUnlockDisplayRepository data.
    /// </summary>
    /// <returns>repository of RecipeUnlockDisplayRepository.</returns>
    public RecipeUnlockDisplayRepository LoadRecipeUnlockDisplayRepository()
    {
        recipeunlockdisplayrepository ??= new RecipeUnlockDisplayRepository(logger, this);
        return recipeunlockdisplayrepository;
    }

    /// <summary>
    /// Gets RecipeUnlockObjectsRepository data.
    /// </summary>
    /// <returns>repository of RecipeUnlockObjectsRepository.</returns>
    public RecipeUnlockObjectsRepository LoadRecipeUnlockObjectsRepository()
    {
        recipeunlockobjectsrepository ??= new RecipeUnlockObjectsRepository(logger, this);
        return recipeunlockobjectsrepository;
    }

    /// <summary>
    /// Gets ReminderTextRepository data.
    /// </summary>
    /// <returns>repository of ReminderTextRepository.</returns>
    public ReminderTextRepository LoadReminderTextRepository()
    {
        remindertextrepository ??= new ReminderTextRepository(logger, this);
        return remindertextrepository;
    }

    /// <summary>
    /// Gets RulesetsRepository data.
    /// </summary>
    /// <returns>repository of RulesetsRepository.</returns>
    public RulesetsRepository LoadRulesetsRepository()
    {
        rulesetsrepository ??= new RulesetsRepository(logger, this);
        return rulesetsrepository;
    }

    /// <summary>
    /// Gets RunicCirclesRepository data.
    /// </summary>
    /// <returns>repository of RunicCirclesRepository.</returns>
    public RunicCirclesRepository LoadRunicCirclesRepository()
    {
        runiccirclesrepository ??= new RunicCirclesRepository(logger, this);
        return runiccirclesrepository;
    }

    /// <summary>
    /// Gets SalvageBoxesRepository data.
    /// </summary>
    /// <returns>repository of SalvageBoxesRepository.</returns>
    public SalvageBoxesRepository LoadSalvageBoxesRepository()
    {
        salvageboxesrepository ??= new SalvageBoxesRepository(logger, this);
        return salvageboxesrepository;
    }

    /// <summary>
    /// Gets SessionQuestFlagsRepository data.
    /// </summary>
    /// <returns>repository of SessionQuestFlagsRepository.</returns>
    public SessionQuestFlagsRepository LoadSessionQuestFlagsRepository()
    {
        sessionquestflagsrepository ??= new SessionQuestFlagsRepository(logger, this);
        return sessionquestflagsrepository;
    }

    /// <summary>
    /// Gets ShieldTypesRepository data.
    /// </summary>
    /// <returns>repository of ShieldTypesRepository.</returns>
    public ShieldTypesRepository LoadShieldTypesRepository()
    {
        shieldtypesrepository ??= new ShieldTypesRepository(logger, this);
        return shieldtypesrepository;
    }

    /// <summary>
    /// Gets ShopCategoryRepository data.
    /// </summary>
    /// <returns>repository of ShopCategoryRepository.</returns>
    public ShopCategoryRepository LoadShopCategoryRepository()
    {
        shopcategoryrepository ??= new ShopCategoryRepository(logger, this);
        return shopcategoryrepository;
    }

    /// <summary>
    /// Gets ShopCountryRepository data.
    /// </summary>
    /// <returns>repository of ShopCountryRepository.</returns>
    public ShopCountryRepository LoadShopCountryRepository()
    {
        shopcountryrepository ??= new ShopCountryRepository(logger, this);
        return shopcountryrepository;
    }

    /// <summary>
    /// Gets ShopCurrencyRepository data.
    /// </summary>
    /// <returns>repository of ShopCurrencyRepository.</returns>
    public ShopCurrencyRepository LoadShopCurrencyRepository()
    {
        shopcurrencyrepository ??= new ShopCurrencyRepository(logger, this);
        return shopcurrencyrepository;
    }

    /// <summary>
    /// Gets ShopPaymentPackageRepository data.
    /// </summary>
    /// <returns>repository of ShopPaymentPackageRepository.</returns>
    public ShopPaymentPackageRepository LoadShopPaymentPackageRepository()
    {
        shoppaymentpackagerepository ??= new ShopPaymentPackageRepository(logger, this);
        return shoppaymentpackagerepository;
    }

    /// <summary>
    /// Gets ShopPaymentPackagePriceRepository data.
    /// </summary>
    /// <returns>repository of ShopPaymentPackagePriceRepository.</returns>
    public ShopPaymentPackagePriceRepository LoadShopPaymentPackagePriceRepository()
    {
        shoppaymentpackagepricerepository ??= new ShopPaymentPackagePriceRepository(logger, this);
        return shoppaymentpackagepricerepository;
    }

    /// <summary>
    /// Gets ShopRegionRepository data.
    /// </summary>
    /// <returns>repository of ShopRegionRepository.</returns>
    public ShopRegionRepository LoadShopRegionRepository()
    {
        shopregionrepository ??= new ShopRegionRepository(logger, this);
        return shopregionrepository;
    }

    /// <summary>
    /// Gets ShopTagRepository data.
    /// </summary>
    /// <returns>repository of ShopTagRepository.</returns>
    public ShopTagRepository LoadShopTagRepository()
    {
        shoptagrepository ??= new ShopTagRepository(logger, this);
        return shoptagrepository;
    }

    /// <summary>
    /// Gets ShopTokenRepository data.
    /// </summary>
    /// <returns>repository of ShopTokenRepository.</returns>
    public ShopTokenRepository LoadShopTokenRepository()
    {
        shoptokenrepository ??= new ShopTokenRepository(logger, this);
        return shoptokenrepository;
    }

    /// <summary>
    /// Gets SigilDisplayRepository data.
    /// </summary>
    /// <returns>repository of SigilDisplayRepository.</returns>
    public SigilDisplayRepository LoadSigilDisplayRepository()
    {
        sigildisplayrepository ??= new SigilDisplayRepository(logger, this);
        return sigildisplayrepository;
    }

    /// <summary>
    /// Gets SingleGroundLaserRepository data.
    /// </summary>
    /// <returns>repository of SingleGroundLaserRepository.</returns>
    public SingleGroundLaserRepository LoadSingleGroundLaserRepository()
    {
        singlegroundlaserrepository ??= new SingleGroundLaserRepository(logger, this);
        return singlegroundlaserrepository;
    }

    /// <summary>
    /// Gets SkillArtVariationsRepository data.
    /// </summary>
    /// <returns>repository of SkillArtVariationsRepository.</returns>
    public SkillArtVariationsRepository LoadSkillArtVariationsRepository()
    {
        skillartvariationsrepository ??= new SkillArtVariationsRepository(logger, this);
        return skillartvariationsrepository;
    }

    /// <summary>
    /// Gets SkillGemInfoRepository data.
    /// </summary>
    /// <returns>repository of SkillGemInfoRepository.</returns>
    public SkillGemInfoRepository LoadSkillGemInfoRepository()
    {
        skillgeminforepository ??= new SkillGemInfoRepository(logger, this);
        return skillgeminforepository;
    }

    /// <summary>
    /// Gets SkillGemsRepository data.
    /// </summary>
    /// <returns>repository of SkillGemsRepository.</returns>
    public SkillGemsRepository LoadSkillGemsRepository()
    {
        skillgemsrepository ??= new SkillGemsRepository(logger, this);
        return skillgemsrepository;
    }

    /// <summary>
    /// Gets SkillMineVariationsRepository data.
    /// </summary>
    /// <returns>repository of SkillMineVariationsRepository.</returns>
    public SkillMineVariationsRepository LoadSkillMineVariationsRepository()
    {
        skillminevariationsrepository ??= new SkillMineVariationsRepository(logger, this);
        return skillminevariationsrepository;
    }

    /// <summary>
    /// Gets SkillMorphDisplayRepository data.
    /// </summary>
    /// <returns>repository of SkillMorphDisplayRepository.</returns>
    public SkillMorphDisplayRepository LoadSkillMorphDisplayRepository()
    {
        skillmorphdisplayrepository ??= new SkillMorphDisplayRepository(logger, this);
        return skillmorphdisplayrepository;
    }

    /// <summary>
    /// Gets SkillSurgeEffectsRepository data.
    /// </summary>
    /// <returns>repository of SkillSurgeEffectsRepository.</returns>
    public SkillSurgeEffectsRepository LoadSkillSurgeEffectsRepository()
    {
        skillsurgeeffectsrepository ??= new SkillSurgeEffectsRepository(logger, this);
        return skillsurgeeffectsrepository;
    }

    /// <summary>
    /// Gets SkillTotemVariationsRepository data.
    /// </summary>
    /// <returns>repository of SkillTotemVariationsRepository.</returns>
    public SkillTotemVariationsRepository LoadSkillTotemVariationsRepository()
    {
        skilltotemvariationsrepository ??= new SkillTotemVariationsRepository(logger, this);
        return skilltotemvariationsrepository;
    }

    /// <summary>
    /// Gets SkillTrapVariationsRepository data.
    /// </summary>
    /// <returns>repository of SkillTrapVariationsRepository.</returns>
    public SkillTrapVariationsRepository LoadSkillTrapVariationsRepository()
    {
        skilltrapvariationsrepository ??= new SkillTrapVariationsRepository(logger, this);
        return skilltrapvariationsrepository;
    }

    /// <summary>
    /// Gets SocketNotchesRepository data.
    /// </summary>
    /// <returns>repository of SocketNotchesRepository.</returns>
    public SocketNotchesRepository LoadSocketNotchesRepository()
    {
        socketnotchesrepository ??= new SocketNotchesRepository(logger, this);
        return socketnotchesrepository;
    }

    /// <summary>
    /// Gets SoundEffectsRepository data.
    /// </summary>
    /// <returns>repository of SoundEffectsRepository.</returns>
    public SoundEffectsRepository LoadSoundEffectsRepository()
    {
        soundeffectsrepository ??= new SoundEffectsRepository(logger, this);
        return soundeffectsrepository;
    }

    /// <summary>
    /// Gets SpawnAdditionalChestsOrClustersRepository data.
    /// </summary>
    /// <returns>repository of SpawnAdditionalChestsOrClustersRepository.</returns>
    public SpawnAdditionalChestsOrClustersRepository LoadSpawnAdditionalChestsOrClustersRepository()
    {
        spawnadditionalchestsorclustersrepository ??= new SpawnAdditionalChestsOrClustersRepository(logger, this);
        return spawnadditionalchestsorclustersrepository;
    }

    /// <summary>
    /// Gets SpawnObjectRepository data.
    /// </summary>
    /// <returns>repository of SpawnObjectRepository.</returns>
    public SpawnObjectRepository LoadSpawnObjectRepository()
    {
        spawnobjectrepository ??= new SpawnObjectRepository(logger, this);
        return spawnobjectrepository;
    }

    /// <summary>
    /// Gets SpecialRoomsRepository data.
    /// </summary>
    /// <returns>repository of SpecialRoomsRepository.</returns>
    public SpecialRoomsRepository LoadSpecialRoomsRepository()
    {
        specialroomsrepository ??= new SpecialRoomsRepository(logger, this);
        return specialroomsrepository;
    }

    /// <summary>
    /// Gets SpecialTilesRepository data.
    /// </summary>
    /// <returns>repository of SpecialTilesRepository.</returns>
    public SpecialTilesRepository LoadSpecialTilesRepository()
    {
        specialtilesrepository ??= new SpecialTilesRepository(logger, this);
        return specialtilesrepository;
    }

    /// <summary>
    /// Gets SpectreOverridesRepository data.
    /// </summary>
    /// <returns>repository of SpectreOverridesRepository.</returns>
    public SpectreOverridesRepository LoadSpectreOverridesRepository()
    {
        spectreoverridesrepository ??= new SpectreOverridesRepository(logger, this);
        return spectreoverridesrepository;
    }

    /// <summary>
    /// Gets StartingPassiveSkillsRepository data.
    /// </summary>
    /// <returns>repository of StartingPassiveSkillsRepository.</returns>
    public StartingPassiveSkillsRepository LoadStartingPassiveSkillsRepository()
    {
        startingpassiveskillsrepository ??= new StartingPassiveSkillsRepository(logger, this);
        return startingpassiveskillsrepository;
    }

    /// <summary>
    /// Gets StashTabAffinitiesRepository data.
    /// </summary>
    /// <returns>repository of StashTabAffinitiesRepository.</returns>
    public StashTabAffinitiesRepository LoadStashTabAffinitiesRepository()
    {
        stashtabaffinitiesrepository ??= new StashTabAffinitiesRepository(logger, this);
        return stashtabaffinitiesrepository;
    }

    /// <summary>
    /// Gets StashTypeRepository data.
    /// </summary>
    /// <returns>repository of StashTypeRepository.</returns>
    public StashTypeRepository LoadStashTypeRepository()
    {
        stashtyperepository ??= new StashTypeRepository(logger, this);
        return stashtyperepository;
    }

    /// <summary>
    /// Gets StatDescriptionFunctionsRepository data.
    /// </summary>
    /// <returns>repository of StatDescriptionFunctionsRepository.</returns>
    public StatDescriptionFunctionsRepository LoadStatDescriptionFunctionsRepository()
    {
        statdescriptionfunctionsrepository ??= new StatDescriptionFunctionsRepository(logger, this);
        return statdescriptionfunctionsrepository;
    }

    /// <summary>
    /// Gets StatsAffectingGenerationRepository data.
    /// </summary>
    /// <returns>repository of StatsAffectingGenerationRepository.</returns>
    public StatsAffectingGenerationRepository LoadStatsAffectingGenerationRepository()
    {
        statsaffectinggenerationrepository ??= new StatsAffectingGenerationRepository(logger, this);
        return statsaffectinggenerationrepository;
    }

    /// <summary>
    /// Gets StatsRepository data.
    /// </summary>
    /// <returns>repository of StatsRepository.</returns>
    public StatsRepository LoadStatsRepository()
    {
        statsrepository ??= new StatsRepository(logger, this);
        return statsrepository;
    }

    /// <summary>
    /// Gets StrDexIntMissionExtraRequirementRepository data.
    /// </summary>
    /// <returns>repository of StrDexIntMissionExtraRequirementRepository.</returns>
    public StrDexIntMissionExtraRequirementRepository LoadStrDexIntMissionExtraRequirementRepository()
    {
        strdexintmissionextrarequirementrepository ??= new StrDexIntMissionExtraRequirementRepository(logger, this);
        return strdexintmissionextrarequirementrepository;
    }

    /// <summary>
    /// Gets StrDexIntMissionsRepository data.
    /// </summary>
    /// <returns>repository of StrDexIntMissionsRepository.</returns>
    public StrDexIntMissionsRepository LoadStrDexIntMissionsRepository()
    {
        strdexintmissionsrepository ??= new StrDexIntMissionsRepository(logger, this);
        return strdexintmissionsrepository;
    }

    /// <summary>
    /// Gets SuicideExplosionRepository data.
    /// </summary>
    /// <returns>repository of SuicideExplosionRepository.</returns>
    public SuicideExplosionRepository LoadSuicideExplosionRepository()
    {
        suicideexplosionrepository ??= new SuicideExplosionRepository(logger, this);
        return suicideexplosionrepository;
    }

    /// <summary>
    /// Gets SummonedSpecificBarrelsRepository data.
    /// </summary>
    /// <returns>repository of SummonedSpecificBarrelsRepository.</returns>
    public SummonedSpecificBarrelsRepository LoadSummonedSpecificBarrelsRepository()
    {
        summonedspecificbarrelsrepository ??= new SummonedSpecificBarrelsRepository(logger, this);
        return summonedspecificbarrelsrepository;
    }

    /// <summary>
    /// Gets SummonedSpecificMonstersRepository data.
    /// </summary>
    /// <returns>repository of SummonedSpecificMonstersRepository.</returns>
    public SummonedSpecificMonstersRepository LoadSummonedSpecificMonstersRepository()
    {
        summonedspecificmonstersrepository ??= new SummonedSpecificMonstersRepository(logger, this);
        return summonedspecificmonstersrepository;
    }

    /// <summary>
    /// Gets SummonedSpecificMonstersOnDeathRepository data.
    /// </summary>
    /// <returns>repository of SummonedSpecificMonstersOnDeathRepository.</returns>
    public SummonedSpecificMonstersOnDeathRepository LoadSummonedSpecificMonstersOnDeathRepository()
    {
        summonedspecificmonstersondeathrepository ??= new SummonedSpecificMonstersOnDeathRepository(logger, this);
        return summonedspecificmonstersondeathrepository;
    }

    /// <summary>
    /// Gets SupporterPackSetsRepository data.
    /// </summary>
    /// <returns>repository of SupporterPackSetsRepository.</returns>
    public SupporterPackSetsRepository LoadSupporterPackSetsRepository()
    {
        supporterpacksetsrepository ??= new SupporterPackSetsRepository(logger, this);
        return supporterpacksetsrepository;
    }

    /// <summary>
    /// Gets SurgeEffectsRepository data.
    /// </summary>
    /// <returns>repository of SurgeEffectsRepository.</returns>
    public SurgeEffectsRepository LoadSurgeEffectsRepository()
    {
        surgeeffectsrepository ??= new SurgeEffectsRepository(logger, this);
        return surgeeffectsrepository;
    }

    /// <summary>
    /// Gets SurgeTypesRepository data.
    /// </summary>
    /// <returns>repository of SurgeTypesRepository.</returns>
    public SurgeTypesRepository LoadSurgeTypesRepository()
    {
        surgetypesrepository ??= new SurgeTypesRepository(logger, this);
        return surgetypesrepository;
    }

    /// <summary>
    /// Gets TableChargeRepository data.
    /// </summary>
    /// <returns>repository of TableChargeRepository.</returns>
    public TableChargeRepository LoadTableChargeRepository()
    {
        tablechargerepository ??= new TableChargeRepository(logger, this);
        return tablechargerepository;
    }

    /// <summary>
    /// Gets TableMonsterSpawnersRepository data.
    /// </summary>
    /// <returns>repository of TableMonsterSpawnersRepository.</returns>
    public TableMonsterSpawnersRepository LoadTableMonsterSpawnersRepository()
    {
        tablemonsterspawnersrepository ??= new TableMonsterSpawnersRepository(logger, this);
        return tablemonsterspawnersrepository;
    }

    /// <summary>
    /// Gets TagsRepository data.
    /// </summary>
    /// <returns>repository of TagsRepository.</returns>
    public TagsRepository LoadTagsRepository()
    {
        tagsrepository ??= new TagsRepository(logger, this);
        return tagsrepository;
    }

    /// <summary>
    /// Gets TalkingPetAudioEventsRepository data.
    /// </summary>
    /// <returns>repository of TalkingPetAudioEventsRepository.</returns>
    public TalkingPetAudioEventsRepository LoadTalkingPetAudioEventsRepository()
    {
        talkingpetaudioeventsrepository ??= new TalkingPetAudioEventsRepository(logger, this);
        return talkingpetaudioeventsrepository;
    }

    /// <summary>
    /// Gets TalkingPetNPCAudioRepository data.
    /// </summary>
    /// <returns>repository of TalkingPetNPCAudioRepository.</returns>
    public TalkingPetNPCAudioRepository LoadTalkingPetNPCAudioRepository()
    {
        talkingpetnpcaudiorepository ??= new TalkingPetNPCAudioRepository(logger, this);
        return talkingpetnpcaudiorepository;
    }

    /// <summary>
    /// Gets TalkingPetsRepository data.
    /// </summary>
    /// <returns>repository of TalkingPetsRepository.</returns>
    public TalkingPetsRepository LoadTalkingPetsRepository()
    {
        talkingpetsrepository ??= new TalkingPetsRepository(logger, this);
        return talkingpetsrepository;
    }

    /// <summary>
    /// Gets TencentAutoLootPetCurrenciesRepository data.
    /// </summary>
    /// <returns>repository of TencentAutoLootPetCurrenciesRepository.</returns>
    public TencentAutoLootPetCurrenciesRepository LoadTencentAutoLootPetCurrenciesRepository()
    {
        tencentautolootpetcurrenciesrepository ??= new TencentAutoLootPetCurrenciesRepository(logger, this);
        return tencentautolootpetcurrenciesrepository;
    }

    /// <summary>
    /// Gets TencentAutoLootPetCurrenciesExcludableRepository data.
    /// </summary>
    /// <returns>repository of TencentAutoLootPetCurrenciesExcludableRepository.</returns>
    public TencentAutoLootPetCurrenciesExcludableRepository LoadTencentAutoLootPetCurrenciesExcludableRepository()
    {
        tencentautolootpetcurrenciesexcludablerepository ??= new TencentAutoLootPetCurrenciesExcludableRepository(logger, this);
        return tencentautolootpetcurrenciesexcludablerepository;
    }

    /// <summary>
    /// Gets TerrainPluginsRepository data.
    /// </summary>
    /// <returns>repository of TerrainPluginsRepository.</returns>
    public TerrainPluginsRepository LoadTerrainPluginsRepository()
    {
        terrainpluginsrepository ??= new TerrainPluginsRepository(logger, this);
        return terrainpluginsrepository;
    }

    /// <summary>
    /// Gets TipsRepository data.
    /// </summary>
    /// <returns>repository of TipsRepository.</returns>
    public TipsRepository LoadTipsRepository()
    {
        tipsrepository ??= new TipsRepository(logger, this);
        return tipsrepository;
    }

    /// <summary>
    /// Gets TopologiesRepository data.
    /// </summary>
    /// <returns>repository of TopologiesRepository.</returns>
    public TopologiesRepository LoadTopologiesRepository()
    {
        topologiesrepository ??= new TopologiesRepository(logger, this);
        return topologiesrepository;
    }

    /// <summary>
    /// Gets TradeMarketCategoryRepository data.
    /// </summary>
    /// <returns>repository of TradeMarketCategoryRepository.</returns>
    public TradeMarketCategoryRepository LoadTradeMarketCategoryRepository()
    {
        trademarketcategoryrepository ??= new TradeMarketCategoryRepository(logger, this);
        return trademarketcategoryrepository;
    }

    /// <summary>
    /// Gets TradeMarketCategoryGroupsRepository data.
    /// </summary>
    /// <returns>repository of TradeMarketCategoryGroupsRepository.</returns>
    public TradeMarketCategoryGroupsRepository LoadTradeMarketCategoryGroupsRepository()
    {
        trademarketcategorygroupsrepository ??= new TradeMarketCategoryGroupsRepository(logger, this);
        return trademarketcategorygroupsrepository;
    }

    /// <summary>
    /// Gets TradeMarketCategoryListAllClassRepository data.
    /// </summary>
    /// <returns>repository of TradeMarketCategoryListAllClassRepository.</returns>
    public TradeMarketCategoryListAllClassRepository LoadTradeMarketCategoryListAllClassRepository()
    {
        trademarketcategorylistallclassrepository ??= new TradeMarketCategoryListAllClassRepository(logger, this);
        return trademarketcategorylistallclassrepository;
    }

    /// <summary>
    /// Gets TradeMarketIndexItemAsRepository data.
    /// </summary>
    /// <returns>repository of TradeMarketIndexItemAsRepository.</returns>
    public TradeMarketIndexItemAsRepository LoadTradeMarketIndexItemAsRepository()
    {
        trademarketindexitemasrepository ??= new TradeMarketIndexItemAsRepository(logger, this);
        return trademarketindexitemasrepository;
    }

    /// <summary>
    /// Gets TreasureHunterMissionsRepository data.
    /// </summary>
    /// <returns>repository of TreasureHunterMissionsRepository.</returns>
    public TreasureHunterMissionsRepository LoadTreasureHunterMissionsRepository()
    {
        treasurehuntermissionsrepository ??= new TreasureHunterMissionsRepository(logger, this);
        return treasurehuntermissionsrepository;
    }

    /// <summary>
    /// Gets TriggerBeamRepository data.
    /// </summary>
    /// <returns>repository of TriggerBeamRepository.</returns>
    public TriggerBeamRepository LoadTriggerBeamRepository()
    {
        triggerbeamrepository ??= new TriggerBeamRepository(logger, this);
        return triggerbeamrepository;
    }

    /// <summary>
    /// Gets TriggerSpawnersRepository data.
    /// </summary>
    /// <returns>repository of TriggerSpawnersRepository.</returns>
    public TriggerSpawnersRepository LoadTriggerSpawnersRepository()
    {
        triggerspawnersrepository ??= new TriggerSpawnersRepository(logger, this);
        return triggerspawnersrepository;
    }

    /// <summary>
    /// Gets TutorialRepository data.
    /// </summary>
    /// <returns>repository of TutorialRepository.</returns>
    public TutorialRepository LoadTutorialRepository()
    {
        tutorialrepository ??= new TutorialRepository(logger, this);
        return tutorialrepository;
    }

    /// <summary>
    /// Gets UITalkTextRepository data.
    /// </summary>
    /// <returns>repository of UITalkTextRepository.</returns>
    public UITalkTextRepository LoadUITalkTextRepository()
    {
        uitalktextrepository ??= new UITalkTextRepository(logger, this);
        return uitalktextrepository;
    }

    /// <summary>
    /// Gets UniqueChestsRepository data.
    /// </summary>
    /// <returns>repository of UniqueChestsRepository.</returns>
    public UniqueChestsRepository LoadUniqueChestsRepository()
    {
        uniquechestsrepository ??= new UniqueChestsRepository(logger, this);
        return uniquechestsrepository;
    }

    /// <summary>
    /// Gets UniqueJewelLimitsRepository data.
    /// </summary>
    /// <returns>repository of UniqueJewelLimitsRepository.</returns>
    public UniqueJewelLimitsRepository LoadUniqueJewelLimitsRepository()
    {
        uniquejewellimitsrepository ??= new UniqueJewelLimitsRepository(logger, this);
        return uniquejewellimitsrepository;
    }

    /// <summary>
    /// Gets UniqueMapInfoRepository data.
    /// </summary>
    /// <returns>repository of UniqueMapInfoRepository.</returns>
    public UniqueMapInfoRepository LoadUniqueMapInfoRepository()
    {
        uniquemapinforepository ??= new UniqueMapInfoRepository(logger, this);
        return uniquemapinforepository;
    }

    /// <summary>
    /// Gets UniqueMapsRepository data.
    /// </summary>
    /// <returns>repository of UniqueMapsRepository.</returns>
    public UniqueMapsRepository LoadUniqueMapsRepository()
    {
        uniquemapsrepository ??= new UniqueMapsRepository(logger, this);
        return uniquemapsrepository;
    }

    /// <summary>
    /// Gets UniqueStashLayoutRepository data.
    /// </summary>
    /// <returns>repository of UniqueStashLayoutRepository.</returns>
    public UniqueStashLayoutRepository LoadUniqueStashLayoutRepository()
    {
        uniquestashlayoutrepository ??= new UniqueStashLayoutRepository(logger, this);
        return uniquestashlayoutrepository;
    }

    /// <summary>
    /// Gets UniqueStashTypesRepository data.
    /// </summary>
    /// <returns>repository of UniqueStashTypesRepository.</returns>
    public UniqueStashTypesRepository LoadUniqueStashTypesRepository()
    {
        uniquestashtypesrepository ??= new UniqueStashTypesRepository(logger, this);
        return uniquestashtypesrepository;
    }

    /// <summary>
    /// Gets VirtualStatContextFlagsRepository data.
    /// </summary>
    /// <returns>repository of VirtualStatContextFlagsRepository.</returns>
    public VirtualStatContextFlagsRepository LoadVirtualStatContextFlagsRepository()
    {
        virtualstatcontextflagsrepository ??= new VirtualStatContextFlagsRepository(logger, this);
        return virtualstatcontextflagsrepository;
    }

    /// <summary>
    /// Gets VoteStateRepository data.
    /// </summary>
    /// <returns>repository of VoteStateRepository.</returns>
    public VoteStateRepository LoadVoteStateRepository()
    {
        votestaterepository ??= new VoteStateRepository(logger, this);
        return votestaterepository;
    }

    /// <summary>
    /// Gets VoteTypeRepository data.
    /// </summary>
    /// <returns>repository of VoteTypeRepository.</returns>
    public VoteTypeRepository LoadVoteTypeRepository()
    {
        votetyperepository ??= new VoteTypeRepository(logger, this);
        return votetyperepository;
    }

    /// <summary>
    /// Gets WeaponClassesRepository data.
    /// </summary>
    /// <returns>repository of WeaponClassesRepository.</returns>
    public WeaponClassesRepository LoadWeaponClassesRepository()
    {
        weaponclassesrepository ??= new WeaponClassesRepository(logger, this);
        return weaponclassesrepository;
    }

    /// <summary>
    /// Gets WeaponImpactSoundDataRepository data.
    /// </summary>
    /// <returns>repository of WeaponImpactSoundDataRepository.</returns>
    public WeaponImpactSoundDataRepository LoadWeaponImpactSoundDataRepository()
    {
        weaponimpactsounddatarepository ??= new WeaponImpactSoundDataRepository(logger, this);
        return weaponimpactsounddatarepository;
    }

    /// <summary>
    /// Gets WeaponTypesRepository data.
    /// </summary>
    /// <returns>repository of WeaponTypesRepository.</returns>
    public WeaponTypesRepository LoadWeaponTypesRepository()
    {
        weapontypesrepository ??= new WeaponTypesRepository(logger, this);
        return weapontypesrepository;
    }

    /// <summary>
    /// Gets WindowCursorsRepository data.
    /// </summary>
    /// <returns>repository of WindowCursorsRepository.</returns>
    public WindowCursorsRepository LoadWindowCursorsRepository()
    {
        windowcursorsrepository ??= new WindowCursorsRepository(logger, this);
        return windowcursorsrepository;
    }

    /// <summary>
    /// Gets WordsRepository data.
    /// </summary>
    /// <returns>repository of WordsRepository.</returns>
    public WordsRepository LoadWordsRepository()
    {
        wordsrepository ??= new WordsRepository(logger, this);
        return wordsrepository;
    }

    /// <summary>
    /// Gets WorldAreasRepository data.
    /// </summary>
    /// <returns>repository of WorldAreasRepository.</returns>
    public WorldAreasRepository LoadWorldAreasRepository()
    {
        worldareasrepository ??= new WorldAreasRepository(logger, this);
        return worldareasrepository;
    }

    /// <summary>
    /// Gets WorldAreaLeagueChancesRepository data.
    /// </summary>
    /// <returns>repository of WorldAreaLeagueChancesRepository.</returns>
    public WorldAreaLeagueChancesRepository LoadWorldAreaLeagueChancesRepository()
    {
        worldarealeaguechancesrepository ??= new WorldAreaLeagueChancesRepository(logger, this);
        return worldarealeaguechancesrepository;
    }

    /// <summary>
    /// Gets WorldPopupIconTypesRepository data.
    /// </summary>
    /// <returns>repository of WorldPopupIconTypesRepository.</returns>
    public WorldPopupIconTypesRepository LoadWorldPopupIconTypesRepository()
    {
        worldpopupicontypesrepository ??= new WorldPopupIconTypesRepository(logger, this);
        return worldpopupicontypesrepository;
    }

    /// <summary>
    /// Gets ZanaLevelsRepository data.
    /// </summary>
    /// <returns>repository of ZanaLevelsRepository.</returns>
    public ZanaLevelsRepository LoadZanaLevelsRepository()
    {
        zanalevelsrepository ??= new ZanaLevelsRepository(logger, this);
        return zanalevelsrepository;
    }
}
