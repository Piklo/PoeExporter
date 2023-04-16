// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Specifications.DatFiles;
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

    private readonly DataLoader dataLoader;

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
    }

    /// <summary>
    /// Gets RogueExilesDat data.
    /// </summary>
    /// <returns>readonly collection of RogueExilesDat.</returns>
    public ReadOnlyCollection<RogueExilesDat> LoadRogueExilesDat()
    {
        return RogueExilesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RogueExileLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of RogueExileLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<RogueExileLifeScalingPerLevelDat> LoadRogueExileLifeScalingPerLevelDat()
    {
        return RogueExileLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShrineBuffsDat data.
    /// </summary>
    /// <returns>readonly collection of ShrineBuffsDat.</returns>
    public ReadOnlyCollection<ShrineBuffsDat> LoadShrineBuffsDat()
    {
        return ShrineBuffsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShrinesDat data.
    /// </summary>
    /// <returns>readonly collection of ShrinesDat.</returns>
    public ReadOnlyCollection<ShrinesDat> LoadShrinesDat()
    {
        return ShrinesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShrineSoundsDat data.
    /// </summary>
    /// <returns>readonly collection of ShrineSoundsDat.</returns>
    public ReadOnlyCollection<ShrineSoundsDat> LoadShrineSoundsDat()
    {
        return ShrineSoundsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StrongboxesDat data.
    /// </summary>
    /// <returns>readonly collection of StrongboxesDat.</returns>
    public ReadOnlyCollection<StrongboxesDat> LoadStrongboxesDat()
    {
        return StrongboxesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets InvasionMonsterRestrictionsDat data.
    /// </summary>
    /// <returns>readonly collection of InvasionMonsterRestrictionsDat.</returns>
    public ReadOnlyCollection<InvasionMonsterRestrictionsDat> LoadInvasionMonsterRestrictionsDat()
    {
        return InvasionMonsterRestrictionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets InvasionMonstersPerAreaDat data.
    /// </summary>
    /// <returns>readonly collection of InvasionMonstersPerAreaDat.</returns>
    public ReadOnlyCollection<InvasionMonstersPerAreaDat> LoadInvasionMonstersPerAreaDat()
    {
        return InvasionMonstersPerAreaDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BeyondDemonsDat data.
    /// </summary>
    /// <returns>readonly collection of BeyondDemonsDat.</returns>
    public ReadOnlyCollection<BeyondDemonsDat> LoadBeyondDemonsDat()
    {
        return BeyondDemonsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BeyondFactionsDat data.
    /// </summary>
    /// <returns>readonly collection of BeyondFactionsDat.</returns>
    public ReadOnlyCollection<BeyondFactionsDat> LoadBeyondFactionsDat()
    {
        return BeyondFactionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BloodlinesDat data.
    /// </summary>
    /// <returns>readonly collection of BloodlinesDat.</returns>
    public ReadOnlyCollection<BloodlinesDat> LoadBloodlinesDat()
    {
        return BloodlinesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TormentSpiritsDat data.
    /// </summary>
    /// <returns>readonly collection of TormentSpiritsDat.</returns>
    public ReadOnlyCollection<TormentSpiritsDat> LoadTormentSpiritsDat()
    {
        return TormentSpiritsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DivinationCardArtDat data.
    /// </summary>
    /// <returns>readonly collection of DivinationCardArtDat.</returns>
    public ReadOnlyCollection<DivinationCardArtDat> LoadDivinationCardArtDat()
    {
        return DivinationCardArtDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WarbandsGraphDat data.
    /// </summary>
    /// <returns>readonly collection of WarbandsGraphDat.</returns>
    public ReadOnlyCollection<WarbandsGraphDat> LoadWarbandsGraphDat()
    {
        return WarbandsGraphDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WarbandsMapGraphDat data.
    /// </summary>
    /// <returns>readonly collection of WarbandsMapGraphDat.</returns>
    public ReadOnlyCollection<WarbandsMapGraphDat> LoadWarbandsMapGraphDat()
    {
        return WarbandsMapGraphDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WarbandsPackMonstersDat data.
    /// </summary>
    /// <returns>readonly collection of WarbandsPackMonstersDat.</returns>
    public ReadOnlyCollection<WarbandsPackMonstersDat> LoadWarbandsPackMonstersDat()
    {
        return WarbandsPackMonstersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WarbandsPackNumbersDat data.
    /// </summary>
    /// <returns>readonly collection of WarbandsPackNumbersDat.</returns>
    public ReadOnlyCollection<WarbandsPackNumbersDat> LoadWarbandsPackNumbersDat()
    {
        return WarbandsPackNumbersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TalismanMonsterModsDat data.
    /// </summary>
    /// <returns>readonly collection of TalismanMonsterModsDat.</returns>
    public ReadOnlyCollection<TalismanMonsterModsDat> LoadTalismanMonsterModsDat()
    {
        return TalismanMonsterModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TalismanPacksDat data.
    /// </summary>
    /// <returns>readonly collection of TalismanPacksDat.</returns>
    public ReadOnlyCollection<TalismanPacksDat> LoadTalismanPacksDat()
    {
        return TalismanPacksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TalismansDat data.
    /// </summary>
    /// <returns>readonly collection of TalismansDat.</returns>
    public ReadOnlyCollection<TalismansDat> LoadTalismansDat()
    {
        return TalismansDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthAreasDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthAreasDat.</returns>
    public ReadOnlyCollection<LabyrinthAreasDat> LoadLabyrinthAreasDat()
    {
        return LabyrinthAreasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthBonusItemsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthBonusItemsDat.</returns>
    public ReadOnlyCollection<LabyrinthBonusItemsDat> LoadLabyrinthBonusItemsDat()
    {
        return LabyrinthBonusItemsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthExclusionGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthExclusionGroupsDat.</returns>
    public ReadOnlyCollection<LabyrinthExclusionGroupsDat> LoadLabyrinthExclusionGroupsDat()
    {
        return LabyrinthExclusionGroupsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthIzaroChestsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthIzaroChestsDat.</returns>
    public ReadOnlyCollection<LabyrinthIzaroChestsDat> LoadLabyrinthIzaroChestsDat()
    {
        return LabyrinthIzaroChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthNodeOverridesDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthNodeOverridesDat.</returns>
    public ReadOnlyCollection<LabyrinthNodeOverridesDat> LoadLabyrinthNodeOverridesDat()
    {
        return LabyrinthNodeOverridesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthRewardTypesDat.</returns>
    public ReadOnlyCollection<LabyrinthRewardTypesDat> LoadLabyrinthRewardTypesDat()
    {
        return LabyrinthRewardTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthsDat.</returns>
    public ReadOnlyCollection<LabyrinthsDat> LoadLabyrinthsDat()
    {
        return LabyrinthsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthSecretEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthSecretEffectsDat.</returns>
    public ReadOnlyCollection<LabyrinthSecretEffectsDat> LoadLabyrinthSecretEffectsDat()
    {
        return LabyrinthSecretEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthSecretsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthSecretsDat.</returns>
    public ReadOnlyCollection<LabyrinthSecretsDat> LoadLabyrinthSecretsDat()
    {
        return LabyrinthSecretsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthSectionDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthSectionDat.</returns>
    public ReadOnlyCollection<LabyrinthSectionDat> LoadLabyrinthSectionDat()
    {
        return LabyrinthSectionDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthSectionLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthSectionLayoutDat.</returns>
    public ReadOnlyCollection<LabyrinthSectionLayoutDat> LoadLabyrinthSectionLayoutDat()
    {
        return LabyrinthSectionLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthTrialsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthTrialsDat.</returns>
    public ReadOnlyCollection<LabyrinthTrialsDat> LoadLabyrinthTrialsDat()
    {
        return LabyrinthTrialsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthTrinketsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthTrinketsDat.</returns>
    public ReadOnlyCollection<LabyrinthTrinketsDat> LoadLabyrinthTrinketsDat()
    {
        return LabyrinthTrinketsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PerandusBossesDat data.
    /// </summary>
    /// <returns>readonly collection of PerandusBossesDat.</returns>
    public ReadOnlyCollection<PerandusBossesDat> LoadPerandusBossesDat()
    {
        return PerandusBossesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PerandusChestsDat data.
    /// </summary>
    /// <returns>readonly collection of PerandusChestsDat.</returns>
    public ReadOnlyCollection<PerandusChestsDat> LoadPerandusChestsDat()
    {
        return PerandusChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PerandusDaemonsDat data.
    /// </summary>
    /// <returns>readonly collection of PerandusDaemonsDat.</returns>
    public ReadOnlyCollection<PerandusDaemonsDat> LoadPerandusDaemonsDat()
    {
        return PerandusDaemonsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PerandusGuardsDat data.
    /// </summary>
    /// <returns>readonly collection of PerandusGuardsDat.</returns>
    public ReadOnlyCollection<PerandusGuardsDat> LoadPerandusGuardsDat()
    {
        return PerandusGuardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PropheciesDat data.
    /// </summary>
    /// <returns>readonly collection of PropheciesDat.</returns>
    public ReadOnlyCollection<PropheciesDat> LoadPropheciesDat()
    {
        return PropheciesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ProphecyChainDat data.
    /// </summary>
    /// <returns>readonly collection of ProphecyChainDat.</returns>
    public ReadOnlyCollection<ProphecyChainDat> LoadProphecyChainDat()
    {
        return ProphecyChainDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ProphecyTypeDat data.
    /// </summary>
    /// <returns>readonly collection of ProphecyTypeDat.</returns>
    public ReadOnlyCollection<ProphecyTypeDat> LoadProphecyTypeDat()
    {
        return ProphecyTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShaperGuardiansDat data.
    /// </summary>
    /// <returns>readonly collection of ShaperGuardiansDat.</returns>
    public ReadOnlyCollection<ShaperGuardiansDat> LoadShaperGuardiansDat()
    {
        return ShaperGuardiansDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EssencesDat data.
    /// </summary>
    /// <returns>readonly collection of EssencesDat.</returns>
    public ReadOnlyCollection<EssencesDat> LoadEssencesDat()
    {
        return EssencesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EssenceTypeDat data.
    /// </summary>
    /// <returns>readonly collection of EssenceTypeDat.</returns>
    public ReadOnlyCollection<EssenceTypeDat> LoadEssenceTypeDat()
    {
        return EssenceTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BreachBossLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BreachBossLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<BreachBossLifeScalingPerLevelDat> LoadBreachBossLifeScalingPerLevelDat()
    {
        return BreachBossLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BreachElementDat data.
    /// </summary>
    /// <returns>readonly collection of BreachElementDat.</returns>
    public ReadOnlyCollection<BreachElementDat> LoadBreachElementDat()
    {
        return BreachElementDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BreachstoneUpgradesDat data.
    /// </summary>
    /// <returns>readonly collection of BreachstoneUpgradesDat.</returns>
    public ReadOnlyCollection<BreachstoneUpgradesDat> LoadBreachstoneUpgradesDat()
    {
        return BreachstoneUpgradesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarbingersDat data.
    /// </summary>
    /// <returns>readonly collection of HarbingersDat.</returns>
    public ReadOnlyCollection<HarbingersDat> LoadHarbingersDat()
    {
        return HarbingersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PantheonPanelLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of PantheonPanelLayoutDat.</returns>
    public ReadOnlyCollection<PantheonPanelLayoutDat> LoadPantheonPanelLayoutDat()
    {
        return PantheonPanelLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PantheonSoulsDat data.
    /// </summary>
    /// <returns>readonly collection of PantheonSoulsDat.</returns>
    public ReadOnlyCollection<PantheonSoulsDat> LoadPantheonSoulsDat()
    {
        return PantheonSoulsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AbyssObjectsDat data.
    /// </summary>
    /// <returns>readonly collection of AbyssObjectsDat.</returns>
    public ReadOnlyCollection<AbyssObjectsDat> LoadAbyssObjectsDat()
    {
        return AbyssObjectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ElderBossArenasDat data.
    /// </summary>
    /// <returns>readonly collection of ElderBossArenasDat.</returns>
    public ReadOnlyCollection<ElderBossArenasDat> LoadElderBossArenasDat()
    {
        return ElderBossArenasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ElderMapBossOverrideDat data.
    /// </summary>
    /// <returns>readonly collection of ElderMapBossOverrideDat.</returns>
    public ReadOnlyCollection<ElderMapBossOverrideDat> LoadElderMapBossOverrideDat()
    {
        return ElderMapBossOverrideDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ElderGuardiansDat data.
    /// </summary>
    /// <returns>readonly collection of ElderGuardiansDat.</returns>
    public ReadOnlyCollection<ElderGuardiansDat> LoadElderGuardiansDat()
    {
        return ElderGuardiansDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryCapturableMonstersDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryCapturableMonstersDat.</returns>
    public ReadOnlyCollection<BestiaryCapturableMonstersDat> LoadBestiaryCapturableMonstersDat()
    {
        return BestiaryCapturableMonstersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryEncountersDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryEncountersDat.</returns>
    public ReadOnlyCollection<BestiaryEncountersDat> LoadBestiaryEncountersDat()
    {
        return BestiaryEncountersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryFamiliesDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryFamiliesDat.</returns>
    public ReadOnlyCollection<BestiaryFamiliesDat> LoadBestiaryFamiliesDat()
    {
        return BestiaryFamiliesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryGenusDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryGenusDat.</returns>
    public ReadOnlyCollection<BestiaryGenusDat> LoadBestiaryGenusDat()
    {
        return BestiaryGenusDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryGroupsDat.</returns>
    public ReadOnlyCollection<BestiaryGroupsDat> LoadBestiaryGroupsDat()
    {
        return BestiaryGroupsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryNetsDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryNetsDat.</returns>
    public ReadOnlyCollection<BestiaryNetsDat> LoadBestiaryNetsDat()
    {
        return BestiaryNetsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryRecipeComponentDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryRecipeComponentDat.</returns>
    public ReadOnlyCollection<BestiaryRecipeComponentDat> LoadBestiaryRecipeComponentDat()
    {
        return BestiaryRecipeComponentDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryRecipeCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryRecipeCategoriesDat.</returns>
    public ReadOnlyCollection<BestiaryRecipeCategoriesDat> LoadBestiaryRecipeCategoriesDat()
    {
        return BestiaryRecipeCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryRecipesDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryRecipesDat.</returns>
    public ReadOnlyCollection<BestiaryRecipesDat> LoadBestiaryRecipesDat()
    {
        return BestiaryRecipesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArchitectLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of ArchitectLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<ArchitectLifeScalingPerLevelDat> LoadArchitectLifeScalingPerLevelDat()
    {
        return ArchitectLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IncursionArchitectDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionArchitectDat.</returns>
    public ReadOnlyCollection<IncursionArchitectDat> LoadIncursionArchitectDat()
    {
        return IncursionArchitectDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IncursionBracketsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionBracketsDat.</returns>
    public ReadOnlyCollection<IncursionBracketsDat> LoadIncursionBracketsDat()
    {
        return IncursionBracketsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IncursionChestRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionChestRewardsDat.</returns>
    public ReadOnlyCollection<IncursionChestRewardsDat> LoadIncursionChestRewardsDat()
    {
        return IncursionChestRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IncursionChestsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionChestsDat.</returns>
    public ReadOnlyCollection<IncursionChestsDat> LoadIncursionChestsDat()
    {
        return IncursionChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IncursionRoomBossFightEventsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionRoomBossFightEventsDat.</returns>
    public ReadOnlyCollection<IncursionRoomBossFightEventsDat> LoadIncursionRoomBossFightEventsDat()
    {
        return IncursionRoomBossFightEventsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IncursionRoomsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionRoomsDat.</returns>
    public ReadOnlyCollection<IncursionRoomsDat> LoadIncursionRoomsDat()
    {
        return IncursionRoomsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IncursionUniqueUpgradeComponentsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionUniqueUpgradeComponentsDat.</returns>
    public ReadOnlyCollection<IncursionUniqueUpgradeComponentsDat> LoadIncursionUniqueUpgradeComponentsDat()
    {
        return IncursionUniqueUpgradeComponentsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveAzuriteShopDat data.
    /// </summary>
    /// <returns>readonly collection of DelveAzuriteShopDat.</returns>
    public ReadOnlyCollection<DelveAzuriteShopDat> LoadDelveAzuriteShopDat()
    {
        return DelveAzuriteShopDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveBiomesDat data.
    /// </summary>
    /// <returns>readonly collection of DelveBiomesDat.</returns>
    public ReadOnlyCollection<DelveBiomesDat> LoadDelveBiomesDat()
    {
        return DelveBiomesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveCatchupDepthsDat data.
    /// </summary>
    /// <returns>readonly collection of DelveCatchupDepthsDat.</returns>
    public ReadOnlyCollection<DelveCatchupDepthsDat> LoadDelveCatchupDepthsDat()
    {
        return DelveCatchupDepthsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveCraftingModifierDescriptionsDat data.
    /// </summary>
    /// <returns>readonly collection of DelveCraftingModifierDescriptionsDat.</returns>
    public ReadOnlyCollection<DelveCraftingModifierDescriptionsDat> LoadDelveCraftingModifierDescriptionsDat()
    {
        return DelveCraftingModifierDescriptionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveCraftingModifiersDat data.
    /// </summary>
    /// <returns>readonly collection of DelveCraftingModifiersDat.</returns>
    public ReadOnlyCollection<DelveCraftingModifiersDat> LoadDelveCraftingModifiersDat()
    {
        return DelveCraftingModifiersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveCraftingTagsDat data.
    /// </summary>
    /// <returns>readonly collection of DelveCraftingTagsDat.</returns>
    public ReadOnlyCollection<DelveCraftingTagsDat> LoadDelveCraftingTagsDat()
    {
        return DelveCraftingTagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveDynamiteDat data.
    /// </summary>
    /// <returns>readonly collection of DelveDynamiteDat.</returns>
    public ReadOnlyCollection<DelveDynamiteDat> LoadDelveDynamiteDat()
    {
        return DelveDynamiteDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveFeaturesDat data.
    /// </summary>
    /// <returns>readonly collection of DelveFeaturesDat.</returns>
    public ReadOnlyCollection<DelveFeaturesDat> LoadDelveFeaturesDat()
    {
        return DelveFeaturesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveFlaresDat data.
    /// </summary>
    /// <returns>readonly collection of DelveFlaresDat.</returns>
    public ReadOnlyCollection<DelveFlaresDat> LoadDelveFlaresDat()
    {
        return DelveFlaresDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveLevelScalingDat data.
    /// </summary>
    /// <returns>readonly collection of DelveLevelScalingDat.</returns>
    public ReadOnlyCollection<DelveLevelScalingDat> LoadDelveLevelScalingDat()
    {
        return DelveLevelScalingDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveMonsterSpawnersDat data.
    /// </summary>
    /// <returns>readonly collection of DelveMonsterSpawnersDat.</returns>
    public ReadOnlyCollection<DelveMonsterSpawnersDat> LoadDelveMonsterSpawnersDat()
    {
        return DelveMonsterSpawnersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveResourcePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of DelveResourcePerLevelDat.</returns>
    public ReadOnlyCollection<DelveResourcePerLevelDat> LoadDelveResourcePerLevelDat()
    {
        return DelveResourcePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveRewardTierConstantsDat data.
    /// </summary>
    /// <returns>readonly collection of DelveRewardTierConstantsDat.</returns>
    public ReadOnlyCollection<DelveRewardTierConstantsDat> LoadDelveRewardTierConstantsDat()
    {
        return DelveRewardTierConstantsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveRoomsDat data.
    /// </summary>
    /// <returns>readonly collection of DelveRoomsDat.</returns>
    public ReadOnlyCollection<DelveRoomsDat> LoadDelveRoomsDat()
    {
        return DelveRoomsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveUpgradesDat data.
    /// </summary>
    /// <returns>readonly collection of DelveUpgradesDat.</returns>
    public ReadOnlyCollection<DelveUpgradesDat> LoadDelveUpgradesDat()
    {
        return DelveUpgradesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalChoiceActionsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalChoiceActionsDat.</returns>
    public ReadOnlyCollection<BetrayalChoiceActionsDat> LoadBetrayalChoiceActionsDat()
    {
        return BetrayalChoiceActionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalChoicesDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalChoicesDat.</returns>
    public ReadOnlyCollection<BetrayalChoicesDat> LoadBetrayalChoicesDat()
    {
        return BetrayalChoicesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalDialogueDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalDialogueDat.</returns>
    public ReadOnlyCollection<BetrayalDialogueDat> LoadBetrayalDialogueDat()
    {
        return BetrayalDialogueDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalFortsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalFortsDat.</returns>
    public ReadOnlyCollection<BetrayalFortsDat> LoadBetrayalFortsDat()
    {
        return BetrayalFortsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalJobsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalJobsDat.</returns>
    public ReadOnlyCollection<BetrayalJobsDat> LoadBetrayalJobsDat()
    {
        return BetrayalJobsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalRanksDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalRanksDat.</returns>
    public ReadOnlyCollection<BetrayalRanksDat> LoadBetrayalRanksDat()
    {
        return BetrayalRanksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalRelationshipStateDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalRelationshipStateDat.</returns>
    public ReadOnlyCollection<BetrayalRelationshipStateDat> LoadBetrayalRelationshipStateDat()
    {
        return BetrayalRelationshipStateDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalTargetJobAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalTargetJobAchievementsDat.</returns>
    public ReadOnlyCollection<BetrayalTargetJobAchievementsDat> LoadBetrayalTargetJobAchievementsDat()
    {
        return BetrayalTargetJobAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalTargetLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalTargetLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<BetrayalTargetLifeScalingPerLevelDat> LoadBetrayalTargetLifeScalingPerLevelDat()
    {
        return BetrayalTargetLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalTargetsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalTargetsDat.</returns>
    public ReadOnlyCollection<BetrayalTargetsDat> LoadBetrayalTargetsDat()
    {
        return BetrayalTargetsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalTraitorRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalTraitorRewardsDat.</returns>
    public ReadOnlyCollection<BetrayalTraitorRewardsDat> LoadBetrayalTraitorRewardsDat()
    {
        return BetrayalTraitorRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalUpgradesDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalUpgradesDat.</returns>
    public ReadOnlyCollection<BetrayalUpgradesDat> LoadBetrayalUpgradesDat()
    {
        return BetrayalUpgradesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalWallLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalWallLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<BetrayalWallLifeScalingPerLevelDat> LoadBetrayalWallLifeScalingPerLevelDat()
    {
        return BetrayalWallLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SafehouseBYOCraftingDat data.
    /// </summary>
    /// <returns>readonly collection of SafehouseBYOCraftingDat.</returns>
    public ReadOnlyCollection<SafehouseBYOCraftingDat> LoadSafehouseBYOCraftingDat()
    {
        return SafehouseBYOCraftingDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SafehouseCraftingSpreeTypeDat data.
    /// </summary>
    /// <returns>readonly collection of SafehouseCraftingSpreeTypeDat.</returns>
    public ReadOnlyCollection<SafehouseCraftingSpreeTypeDat> LoadSafehouseCraftingSpreeTypeDat()
    {
        return SafehouseCraftingSpreeTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SafehouseCraftingSpreeCurrenciesDat data.
    /// </summary>
    /// <returns>readonly collection of SafehouseCraftingSpreeCurrenciesDat.</returns>
    public ReadOnlyCollection<SafehouseCraftingSpreeCurrenciesDat> LoadSafehouseCraftingSpreeCurrenciesDat()
    {
        return SafehouseCraftingSpreeCurrenciesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ScarabsDat data.
    /// </summary>
    /// <returns>readonly collection of ScarabsDat.</returns>
    public ReadOnlyCollection<ScarabsDat> LoadScarabsDat()
    {
        return ScarabsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisAreasDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisAreasDat.</returns>
    public ReadOnlyCollection<SynthesisAreasDat> LoadSynthesisAreasDat()
    {
        return SynthesisAreasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisAreaSizeDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisAreaSizeDat.</returns>
    public ReadOnlyCollection<SynthesisAreaSizeDat> LoadSynthesisAreaSizeDat()
    {
        return SynthesisAreaSizeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisBonusesDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisBonusesDat.</returns>
    public ReadOnlyCollection<SynthesisBonusesDat> LoadSynthesisBonusesDat()
    {
        return SynthesisBonusesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisBracketsDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisBracketsDat.</returns>
    public ReadOnlyCollection<SynthesisBracketsDat> LoadSynthesisBracketsDat()
    {
        return SynthesisBracketsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisFragmentDialogueDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisFragmentDialogueDat.</returns>
    public ReadOnlyCollection<SynthesisFragmentDialogueDat> LoadSynthesisFragmentDialogueDat()
    {
        return SynthesisFragmentDialogueDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisGlobalModsDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisGlobalModsDat.</returns>
    public ReadOnlyCollection<SynthesisGlobalModsDat> LoadSynthesisGlobalModsDat()
    {
        return SynthesisGlobalModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisMonsterExperiencePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisMonsterExperiencePerLevelDat.</returns>
    public ReadOnlyCollection<SynthesisMonsterExperiencePerLevelDat> LoadSynthesisMonsterExperiencePerLevelDat()
    {
        return SynthesisMonsterExperiencePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisRewardCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisRewardCategoriesDat.</returns>
    public ReadOnlyCollection<SynthesisRewardCategoriesDat> LoadSynthesisRewardCategoriesDat()
    {
        return SynthesisRewardCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisRewardTypesDat.</returns>
    public ReadOnlyCollection<SynthesisRewardTypesDat> LoadSynthesisRewardTypesDat()
    {
        return SynthesisRewardTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IncubatorsDat data.
    /// </summary>
    /// <returns>readonly collection of IncubatorsDat.</returns>
    public ReadOnlyCollection<IncubatorsDat> LoadIncubatorsDat()
    {
        return IncubatorsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of LegionBalancePerLevelDat.</returns>
    public ReadOnlyCollection<LegionBalancePerLevelDat> LoadLegionBalancePerLevelDat()
    {
        return LegionBalancePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionChestTypesDat data.
    /// </summary>
    /// <returns>readonly collection of LegionChestTypesDat.</returns>
    public ReadOnlyCollection<LegionChestTypesDat> LoadLegionChestTypesDat()
    {
        return LegionChestTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionChestCountsDat data.
    /// </summary>
    /// <returns>readonly collection of LegionChestCountsDat.</returns>
    public ReadOnlyCollection<LegionChestCountsDat> LoadLegionChestCountsDat()
    {
        return LegionChestCountsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionChestsDat data.
    /// </summary>
    /// <returns>readonly collection of LegionChestsDat.</returns>
    public ReadOnlyCollection<LegionChestsDat> LoadLegionChestsDat()
    {
        return LegionChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionFactionsDat data.
    /// </summary>
    /// <returns>readonly collection of LegionFactionsDat.</returns>
    public ReadOnlyCollection<LegionFactionsDat> LoadLegionFactionsDat()
    {
        return LegionFactionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionMonsterCountsDat data.
    /// </summary>
    /// <returns>readonly collection of LegionMonsterCountsDat.</returns>
    public ReadOnlyCollection<LegionMonsterCountsDat> LoadLegionMonsterCountsDat()
    {
        return LegionMonsterCountsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionMonsterVarietiesDat data.
    /// </summary>
    /// <returns>readonly collection of LegionMonsterVarietiesDat.</returns>
    public ReadOnlyCollection<LegionMonsterVarietiesDat> LoadLegionMonsterVarietiesDat()
    {
        return LegionMonsterVarietiesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionRanksDat data.
    /// </summary>
    /// <returns>readonly collection of LegionRanksDat.</returns>
    public ReadOnlyCollection<LegionRanksDat> LoadLegionRanksDat()
    {
        return LegionRanksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionRewardTypeVisualsDat data.
    /// </summary>
    /// <returns>readonly collection of LegionRewardTypeVisualsDat.</returns>
    public ReadOnlyCollection<LegionRewardTypeVisualsDat> LoadLegionRewardTypeVisualsDat()
    {
        return LegionRewardTypeVisualsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BlightBalancePerLevelDat.</returns>
    public ReadOnlyCollection<BlightBalancePerLevelDat> LoadBlightBalancePerLevelDat()
    {
        return BlightBalancePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightBossLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BlightBossLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<BlightBossLifeScalingPerLevelDat> LoadBlightBossLifeScalingPerLevelDat()
    {
        return BlightBossLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightChestTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightChestTypesDat.</returns>
    public ReadOnlyCollection<BlightChestTypesDat> LoadBlightChestTypesDat()
    {
        return BlightChestTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightCraftingItemsDat data.
    /// </summary>
    /// <returns>readonly collection of BlightCraftingItemsDat.</returns>
    public ReadOnlyCollection<BlightCraftingItemsDat> LoadBlightCraftingItemsDat()
    {
        return BlightCraftingItemsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightCraftingRecipesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightCraftingRecipesDat.</returns>
    public ReadOnlyCollection<BlightCraftingRecipesDat> LoadBlightCraftingRecipesDat()
    {
        return BlightCraftingRecipesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightCraftingResultsDat data.
    /// </summary>
    /// <returns>readonly collection of BlightCraftingResultsDat.</returns>
    public ReadOnlyCollection<BlightCraftingResultsDat> LoadBlightCraftingResultsDat()
    {
        return BlightCraftingResultsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightCraftingTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightCraftingTypesDat.</returns>
    public ReadOnlyCollection<BlightCraftingTypesDat> LoadBlightCraftingTypesDat()
    {
        return BlightCraftingTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightCraftingUniquesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightCraftingUniquesDat.</returns>
    public ReadOnlyCollection<BlightCraftingUniquesDat> LoadBlightCraftingUniquesDat()
    {
        return BlightCraftingUniquesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightedSporeAurasDat data.
    /// </summary>
    /// <returns>readonly collection of BlightedSporeAurasDat.</returns>
    public ReadOnlyCollection<BlightedSporeAurasDat> LoadBlightedSporeAurasDat()
    {
        return BlightedSporeAurasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightEncounterTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightEncounterTypesDat.</returns>
    public ReadOnlyCollection<BlightEncounterTypesDat> LoadBlightEncounterTypesDat()
    {
        return BlightEncounterTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightEncounterWavesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightEncounterWavesDat.</returns>
    public ReadOnlyCollection<BlightEncounterWavesDat> LoadBlightEncounterWavesDat()
    {
        return BlightEncounterWavesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightRewardTypesDat.</returns>
    public ReadOnlyCollection<BlightRewardTypesDat> LoadBlightRewardTypesDat()
    {
        return BlightRewardTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightTopologiesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightTopologiesDat.</returns>
    public ReadOnlyCollection<BlightTopologiesDat> LoadBlightTopologiesDat()
    {
        return BlightTopologiesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightTopologyNodesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightTopologyNodesDat.</returns>
    public ReadOnlyCollection<BlightTopologyNodesDat> LoadBlightTopologyNodesDat()
    {
        return BlightTopologyNodesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightTowerAurasDat data.
    /// </summary>
    /// <returns>readonly collection of BlightTowerAurasDat.</returns>
    public ReadOnlyCollection<BlightTowerAurasDat> LoadBlightTowerAurasDat()
    {
        return BlightTowerAurasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightTowersDat data.
    /// </summary>
    /// <returns>readonly collection of BlightTowersDat.</returns>
    public ReadOnlyCollection<BlightTowersDat> LoadBlightTowersDat()
    {
        return BlightTowersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightTowersPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BlightTowersPerLevelDat.</returns>
    public ReadOnlyCollection<BlightTowersPerLevelDat> LoadBlightTowersPerLevelDat()
    {
        return BlightTowersPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasExileBossArenasDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasExileBossArenasDat.</returns>
    public ReadOnlyCollection<AtlasExileBossArenasDat> LoadAtlasExileBossArenasDat()
    {
        return AtlasExileBossArenasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasExileInfluenceDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasExileInfluenceDat.</returns>
    public ReadOnlyCollection<AtlasExileInfluenceDat> LoadAtlasExileInfluenceDat()
    {
        return AtlasExileInfluenceDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasExilesDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasExilesDat.</returns>
    public ReadOnlyCollection<AtlasExilesDat> LoadAtlasExilesDat()
    {
        return AtlasExilesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AlternateQualityCurrencyDecayFactorsDat data.
    /// </summary>
    /// <returns>readonly collection of AlternateQualityCurrencyDecayFactorsDat.</returns>
    public ReadOnlyCollection<AlternateQualityCurrencyDecayFactorsDat> LoadAlternateQualityCurrencyDecayFactorsDat()
    {
        return AlternateQualityCurrencyDecayFactorsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AlternateQualityTypesDat data.
    /// </summary>
    /// <returns>readonly collection of AlternateQualityTypesDat.</returns>
    public ReadOnlyCollection<AlternateQualityTypesDat> LoadAlternateQualityTypesDat()
    {
        return AlternateQualityTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MetamorphLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<MetamorphLifeScalingPerLevelDat> LoadMetamorphLifeScalingPerLevelDat()
    {
        return MetamorphLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MetamorphosisMetaMonstersDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisMetaMonstersDat.</returns>
    public ReadOnlyCollection<MetamorphosisMetaMonstersDat> LoadMetamorphosisMetaMonstersDat()
    {
        return MetamorphosisMetaMonstersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MetamorphosisMetaSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisMetaSkillsDat.</returns>
    public ReadOnlyCollection<MetamorphosisMetaSkillsDat> LoadMetamorphosisMetaSkillsDat()
    {
        return MetamorphosisMetaSkillsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MetamorphosisMetaSkillTypesDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisMetaSkillTypesDat.</returns>
    public ReadOnlyCollection<MetamorphosisMetaSkillTypesDat> LoadMetamorphosisMetaSkillTypesDat()
    {
        return MetamorphosisMetaSkillTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MetamorphosisRewardTypeItemsClientDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisRewardTypeItemsClientDat.</returns>
    public ReadOnlyCollection<MetamorphosisRewardTypeItemsClientDat> LoadMetamorphosisRewardTypeItemsClientDat()
    {
        return MetamorphosisRewardTypeItemsClientDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MetamorphosisRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisRewardTypesDat.</returns>
    public ReadOnlyCollection<MetamorphosisRewardTypesDat> LoadMetamorphosisRewardTypesDat()
    {
        return MetamorphosisRewardTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MetamorphosisScalingDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisScalingDat.</returns>
    public ReadOnlyCollection<MetamorphosisScalingDat> LoadMetamorphosisScalingDat()
    {
        return MetamorphosisScalingDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AfflictionBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionBalancePerLevelDat.</returns>
    public ReadOnlyCollection<AfflictionBalancePerLevelDat> LoadAfflictionBalancePerLevelDat()
    {
        return AfflictionBalancePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AfflictionEndgameWaveModsDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionEndgameWaveModsDat.</returns>
    public ReadOnlyCollection<AfflictionEndgameWaveModsDat> LoadAfflictionEndgameWaveModsDat()
    {
        return AfflictionEndgameWaveModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AfflictionFixedModsDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionFixedModsDat.</returns>
    public ReadOnlyCollection<AfflictionFixedModsDat> LoadAfflictionFixedModsDat()
    {
        return AfflictionFixedModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AfflictionRandomModCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionRandomModCategoriesDat.</returns>
    public ReadOnlyCollection<AfflictionRandomModCategoriesDat> LoadAfflictionRandomModCategoriesDat()
    {
        return AfflictionRandomModCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AfflictionRewardMapModsDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionRewardMapModsDat.</returns>
    public ReadOnlyCollection<AfflictionRewardMapModsDat> LoadAfflictionRewardMapModsDat()
    {
        return AfflictionRewardMapModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AfflictionRewardTypeVisualsDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionRewardTypeVisualsDat.</returns>
    public ReadOnlyCollection<AfflictionRewardTypeVisualsDat> LoadAfflictionRewardTypeVisualsDat()
    {
        return AfflictionRewardTypeVisualsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AfflictionSplitDemonsDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionSplitDemonsDat.</returns>
    public ReadOnlyCollection<AfflictionSplitDemonsDat> LoadAfflictionSplitDemonsDat()
    {
        return AfflictionSplitDemonsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AfflictionStartDialogueDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionStartDialogueDat.</returns>
    public ReadOnlyCollection<AfflictionStartDialogueDat> LoadAfflictionStartDialogueDat()
    {
        return AfflictionStartDialogueDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestCraftOptionIconsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestCraftOptionIconsDat.</returns>
    public ReadOnlyCollection<HarvestCraftOptionIconsDat> LoadHarvestCraftOptionIconsDat()
    {
        return HarvestCraftOptionIconsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestCraftOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestCraftOptionsDat.</returns>
    public ReadOnlyCollection<HarvestCraftOptionsDat> LoadHarvestCraftOptionsDat()
    {
        return HarvestCraftOptionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestCraftTiersDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestCraftTiersDat.</returns>
    public ReadOnlyCollection<HarvestCraftTiersDat> LoadHarvestCraftTiersDat()
    {
        return HarvestCraftTiersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestCraftFiltersDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestCraftFiltersDat.</returns>
    public ReadOnlyCollection<HarvestCraftFiltersDat> LoadHarvestCraftFiltersDat()
    {
        return HarvestCraftFiltersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestDurabilityDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestDurabilityDat.</returns>
    public ReadOnlyCollection<HarvestDurabilityDat> LoadHarvestDurabilityDat()
    {
        return HarvestDurabilityDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestEncounterScalingDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestEncounterScalingDat.</returns>
    public ReadOnlyCollection<HarvestEncounterScalingDat> LoadHarvestEncounterScalingDat()
    {
        return HarvestEncounterScalingDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestInfrastructureDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestInfrastructureDat.</returns>
    public ReadOnlyCollection<HarvestInfrastructureDat> LoadHarvestInfrastructureDat()
    {
        return HarvestInfrastructureDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestObjectsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestObjectsDat.</returns>
    public ReadOnlyCollection<HarvestObjectsDat> LoadHarvestObjectsDat()
    {
        return HarvestObjectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestPerLevelValuesDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestPerLevelValuesDat.</returns>
    public ReadOnlyCollection<HarvestPerLevelValuesDat> LoadHarvestPerLevelValuesDat()
    {
        return HarvestPerLevelValuesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestPlantBoostersDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestPlantBoostersDat.</returns>
    public ReadOnlyCollection<HarvestPlantBoostersDat> LoadHarvestPlantBoostersDat()
    {
        return HarvestPlantBoostersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<HarvestLifeScalingPerLevelDat> LoadHarvestLifeScalingPerLevelDat()
    {
        return HarvestLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestSeedsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestSeedsDat.</returns>
    public ReadOnlyCollection<HarvestSeedsDat> LoadHarvestSeedsDat()
    {
        return HarvestSeedsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestSeedItemsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestSeedItemsDat.</returns>
    public ReadOnlyCollection<HarvestSeedItemsDat> LoadHarvestSeedItemsDat()
    {
        return HarvestSeedItemsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestSeedTypesDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestSeedTypesDat.</returns>
    public ReadOnlyCollection<HarvestSeedTypesDat> LoadHarvestSeedTypesDat()
    {
        return HarvestSeedTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestSpecialCraftCostsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestSpecialCraftCostsDat.</returns>
    public ReadOnlyCollection<HarvestSpecialCraftCostsDat> LoadHarvestSpecialCraftCostsDat()
    {
        return HarvestSpecialCraftCostsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestSpecialCraftOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestSpecialCraftOptionsDat.</returns>
    public ReadOnlyCollection<HarvestSpecialCraftOptionsDat> LoadHarvestSpecialCraftOptionsDat()
    {
        return HarvestSpecialCraftOptionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistAreaFormationLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of HeistAreaFormationLayoutDat.</returns>
    public ReadOnlyCollection<HeistAreaFormationLayoutDat> LoadHeistAreaFormationLayoutDat()
    {
        return HeistAreaFormationLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistAreasDat data.
    /// </summary>
    /// <returns>readonly collection of HeistAreasDat.</returns>
    public ReadOnlyCollection<HeistAreasDat> LoadHeistAreasDat()
    {
        return HeistAreasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of HeistBalancePerLevelDat.</returns>
    public ReadOnlyCollection<HeistBalancePerLevelDat> LoadHeistBalancePerLevelDat()
    {
        return HeistBalancePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistChestRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of HeistChestRewardTypesDat.</returns>
    public ReadOnlyCollection<HeistChestRewardTypesDat> LoadHeistChestRewardTypesDat()
    {
        return HeistChestRewardTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistChestsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistChestsDat.</returns>
    public ReadOnlyCollection<HeistChestsDat> LoadHeistChestsDat()
    {
        return HeistChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistChokepointFormationDat data.
    /// </summary>
    /// <returns>readonly collection of HeistChokepointFormationDat.</returns>
    public ReadOnlyCollection<HeistChokepointFormationDat> LoadHeistChokepointFormationDat()
    {
        return HeistChokepointFormationDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistConstantsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistConstantsDat.</returns>
    public ReadOnlyCollection<HeistConstantsDat> LoadHeistConstantsDat()
    {
        return HeistConstantsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistContractsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistContractsDat.</returns>
    public ReadOnlyCollection<HeistContractsDat> LoadHeistContractsDat()
    {
        return HeistContractsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistDoodadNPCsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistDoodadNPCsDat.</returns>
    public ReadOnlyCollection<HeistDoodadNPCsDat> LoadHeistDoodadNPCsDat()
    {
        return HeistDoodadNPCsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistDoorsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistDoorsDat.</returns>
    public ReadOnlyCollection<HeistDoorsDat> LoadHeistDoorsDat()
    {
        return HeistDoorsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistEquipmentDat data.
    /// </summary>
    /// <returns>readonly collection of HeistEquipmentDat.</returns>
    public ReadOnlyCollection<HeistEquipmentDat> LoadHeistEquipmentDat()
    {
        return HeistEquipmentDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistGenerationDat data.
    /// </summary>
    /// <returns>readonly collection of HeistGenerationDat.</returns>
    public ReadOnlyCollection<HeistGenerationDat> LoadHeistGenerationDat()
    {
        return HeistGenerationDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistIntroAreasDat data.
    /// </summary>
    /// <returns>readonly collection of HeistIntroAreasDat.</returns>
    public ReadOnlyCollection<HeistIntroAreasDat> LoadHeistIntroAreasDat()
    {
        return HeistIntroAreasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistJobsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistJobsDat.</returns>
    public ReadOnlyCollection<HeistJobsDat> LoadHeistJobsDat()
    {
        return HeistJobsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistJobsExperiencePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of HeistJobsExperiencePerLevelDat.</returns>
    public ReadOnlyCollection<HeistJobsExperiencePerLevelDat> LoadHeistJobsExperiencePerLevelDat()
    {
        return HeistJobsExperiencePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistLockTypeDat data.
    /// </summary>
    /// <returns>readonly collection of HeistLockTypeDat.</returns>
    public ReadOnlyCollection<HeistLockTypeDat> LoadHeistLockTypeDat()
    {
        return HeistLockTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistNPCAurasDat data.
    /// </summary>
    /// <returns>readonly collection of HeistNPCAurasDat.</returns>
    public ReadOnlyCollection<HeistNPCAurasDat> LoadHeistNPCAurasDat()
    {
        return HeistNPCAurasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistNPCBlueprintTypesDat data.
    /// </summary>
    /// <returns>readonly collection of HeistNPCBlueprintTypesDat.</returns>
    public ReadOnlyCollection<HeistNPCBlueprintTypesDat> LoadHeistNPCBlueprintTypesDat()
    {
        return HeistNPCBlueprintTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistNPCDialogueDat data.
    /// </summary>
    /// <returns>readonly collection of HeistNPCDialogueDat.</returns>
    public ReadOnlyCollection<HeistNPCDialogueDat> LoadHeistNPCDialogueDat()
    {
        return HeistNPCDialogueDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistNPCsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistNPCsDat.</returns>
    public ReadOnlyCollection<HeistNPCsDat> LoadHeistNPCsDat()
    {
        return HeistNPCsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistNPCStatsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistNPCStatsDat.</returns>
    public ReadOnlyCollection<HeistNPCStatsDat> LoadHeistNPCStatsDat()
    {
        return HeistNPCStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistObjectivesDat data.
    /// </summary>
    /// <returns>readonly collection of HeistObjectivesDat.</returns>
    public ReadOnlyCollection<HeistObjectivesDat> LoadHeistObjectivesDat()
    {
        return HeistObjectivesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistObjectiveValueDescriptionsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistObjectiveValueDescriptionsDat.</returns>
    public ReadOnlyCollection<HeistObjectiveValueDescriptionsDat> LoadHeistObjectiveValueDescriptionsDat()
    {
        return HeistObjectiveValueDescriptionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistPatrolPacksDat data.
    /// </summary>
    /// <returns>readonly collection of HeistPatrolPacksDat.</returns>
    public ReadOnlyCollection<HeistPatrolPacksDat> LoadHeistPatrolPacksDat()
    {
        return HeistPatrolPacksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistQuestContractsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistQuestContractsDat.</returns>
    public ReadOnlyCollection<HeistQuestContractsDat> LoadHeistQuestContractsDat()
    {
        return HeistQuestContractsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistRevealingNPCsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistRevealingNPCsDat.</returns>
    public ReadOnlyCollection<HeistRevealingNPCsDat> LoadHeistRevealingNPCsDat()
    {
        return HeistRevealingNPCsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistRoomsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistRoomsDat.</returns>
    public ReadOnlyCollection<HeistRoomsDat> LoadHeistRoomsDat()
    {
        return HeistRoomsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistValueScalingDat data.
    /// </summary>
    /// <returns>readonly collection of HeistValueScalingDat.</returns>
    public ReadOnlyCollection<HeistValueScalingDat> LoadHeistValueScalingDat()
    {
        return HeistValueScalingDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets InfluenceModUpgradesDat data.
    /// </summary>
    /// <returns>readonly collection of InfluenceModUpgradesDat.</returns>
    public ReadOnlyCollection<InfluenceModUpgradesDat> LoadInfluenceModUpgradesDat()
    {
        return InfluenceModUpgradesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MavenDialogDat data.
    /// </summary>
    /// <returns>readonly collection of MavenDialogDat.</returns>
    public ReadOnlyCollection<MavenDialogDat> LoadMavenDialogDat()
    {
        return MavenDialogDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasSkillGraphsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasSkillGraphsDat.</returns>
    public ReadOnlyCollection<AtlasSkillGraphsDat> LoadAtlasSkillGraphsDat()
    {
        return AtlasSkillGraphsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MavenFightsDat data.
    /// </summary>
    /// <returns>readonly collection of MavenFightsDat.</returns>
    public ReadOnlyCollection<MavenFightsDat> LoadMavenFightsDat()
    {
        return MavenFightsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MavenJewelRadiusKeystonesDat data.
    /// </summary>
    /// <returns>readonly collection of MavenJewelRadiusKeystonesDat.</returns>
    public ReadOnlyCollection<MavenJewelRadiusKeystonesDat> LoadMavenJewelRadiusKeystonesDat()
    {
        return MavenJewelRadiusKeystonesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RitualBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of RitualBalancePerLevelDat.</returns>
    public ReadOnlyCollection<RitualBalancePerLevelDat> LoadRitualBalancePerLevelDat()
    {
        return RitualBalancePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RitualConstantsDat data.
    /// </summary>
    /// <returns>readonly collection of RitualConstantsDat.</returns>
    public ReadOnlyCollection<RitualConstantsDat> LoadRitualConstantsDat()
    {
        return RitualConstantsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RitualRuneTypesDat data.
    /// </summary>
    /// <returns>readonly collection of RitualRuneTypesDat.</returns>
    public ReadOnlyCollection<RitualRuneTypesDat> LoadRitualRuneTypesDat()
    {
        return RitualRuneTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RitualSetKillAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of RitualSetKillAchievementsDat.</returns>
    public ReadOnlyCollection<RitualSetKillAchievementsDat> LoadRitualSetKillAchievementsDat()
    {
        return RitualSetKillAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RitualSpawnPatternsDat data.
    /// </summary>
    /// <returns>readonly collection of RitualSpawnPatternsDat.</returns>
    public ReadOnlyCollection<RitualSpawnPatternsDat> LoadRitualSpawnPatternsDat()
    {
        return RitualSpawnPatternsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UltimatumEncountersDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumEncountersDat.</returns>
    public ReadOnlyCollection<UltimatumEncountersDat> LoadUltimatumEncountersDat()
    {
        return UltimatumEncountersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UltimatumEncounterTypesDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumEncounterTypesDat.</returns>
    public ReadOnlyCollection<UltimatumEncounterTypesDat> LoadUltimatumEncounterTypesDat()
    {
        return UltimatumEncounterTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UltimatumItemisedRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumItemisedRewardsDat.</returns>
    public ReadOnlyCollection<UltimatumItemisedRewardsDat> LoadUltimatumItemisedRewardsDat()
    {
        return UltimatumItemisedRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UltimatumMapModifiersDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumMapModifiersDat.</returns>
    public ReadOnlyCollection<UltimatumMapModifiersDat> LoadUltimatumMapModifiersDat()
    {
        return UltimatumMapModifiersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UltimatumModifiersDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumModifiersDat.</returns>
    public ReadOnlyCollection<UltimatumModifiersDat> LoadUltimatumModifiersDat()
    {
        return UltimatumModifiersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UltimatumModifierTypesDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumModifierTypesDat.</returns>
    public ReadOnlyCollection<UltimatumModifierTypesDat> LoadUltimatumModifierTypesDat()
    {
        return UltimatumModifierTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UltimatumTrialMasterAudioDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumTrialMasterAudioDat.</returns>
    public ReadOnlyCollection<UltimatumTrialMasterAudioDat> LoadUltimatumTrialMasterAudioDat()
    {
        return UltimatumTrialMasterAudioDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionAreasDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionAreasDat.</returns>
    public ReadOnlyCollection<ExpeditionAreasDat> LoadExpeditionAreasDat()
    {
        return ExpeditionAreasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionBalancePerLevelDat.</returns>
    public ReadOnlyCollection<ExpeditionBalancePerLevelDat> LoadExpeditionBalancePerLevelDat()
    {
        return ExpeditionBalancePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionCurrencyDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionCurrencyDat.</returns>
    public ReadOnlyCollection<ExpeditionCurrencyDat> LoadExpeditionCurrencyDat()
    {
        return ExpeditionCurrencyDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionDealsDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionDealsDat.</returns>
    public ReadOnlyCollection<ExpeditionDealsDat> LoadExpeditionDealsDat()
    {
        return ExpeditionDealsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionFactionsDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionFactionsDat.</returns>
    public ReadOnlyCollection<ExpeditionFactionsDat> LoadExpeditionFactionsDat()
    {
        return ExpeditionFactionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionMarkersCommonDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionMarkersCommonDat.</returns>
    public ReadOnlyCollection<ExpeditionMarkersCommonDat> LoadExpeditionMarkersCommonDat()
    {
        return ExpeditionMarkersCommonDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionNPCsDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionNPCsDat.</returns>
    public ReadOnlyCollection<ExpeditionNPCsDat> LoadExpeditionNPCsDat()
    {
        return ExpeditionNPCsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionRelicModsDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionRelicModsDat.</returns>
    public ReadOnlyCollection<ExpeditionRelicModsDat> LoadExpeditionRelicModsDat()
    {
        return ExpeditionRelicModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionRelicsDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionRelicsDat.</returns>
    public ReadOnlyCollection<ExpeditionRelicsDat> LoadExpeditionRelicsDat()
    {
        return ExpeditionRelicsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionStorageLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionStorageLayoutDat.</returns>
    public ReadOnlyCollection<ExpeditionStorageLayoutDat> LoadExpeditionStorageLayoutDat()
    {
        return ExpeditionStorageLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionTerrainFeaturesDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionTerrainFeaturesDat.</returns>
    public ReadOnlyCollection<ExpeditionTerrainFeaturesDat> LoadExpeditionTerrainFeaturesDat()
    {
        return ExpeditionTerrainFeaturesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeAOReplacementsDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeAOReplacementsDat.</returns>
    public ReadOnlyCollection<HellscapeAOReplacementsDat> LoadHellscapeAOReplacementsDat()
    {
        return HellscapeAOReplacementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeAreaPacksDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeAreaPacksDat.</returns>
    public ReadOnlyCollection<HellscapeAreaPacksDat> LoadHellscapeAreaPacksDat()
    {
        return HellscapeAreaPacksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeExperienceLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeExperienceLevelsDat.</returns>
    public ReadOnlyCollection<HellscapeExperienceLevelsDat> LoadHellscapeExperienceLevelsDat()
    {
        return HellscapeExperienceLevelsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeFactionsDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeFactionsDat.</returns>
    public ReadOnlyCollection<HellscapeFactionsDat> LoadHellscapeFactionsDat()
    {
        return HellscapeFactionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeImmuneMonstersDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeImmuneMonstersDat.</returns>
    public ReadOnlyCollection<HellscapeImmuneMonstersDat> LoadHellscapeImmuneMonstersDat()
    {
        return HellscapeImmuneMonstersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeItemModificationTiersDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeItemModificationTiersDat.</returns>
    public ReadOnlyCollection<HellscapeItemModificationTiersDat> LoadHellscapeItemModificationTiersDat()
    {
        return HellscapeItemModificationTiersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<HellscapeLifeScalingPerLevelDat> LoadHellscapeLifeScalingPerLevelDat()
    {
        return HellscapeLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeModificationInventoryLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeModificationInventoryLayoutDat.</returns>
    public ReadOnlyCollection<HellscapeModificationInventoryLayoutDat> LoadHellscapeModificationInventoryLayoutDat()
    {
        return HellscapeModificationInventoryLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeModsDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeModsDat.</returns>
    public ReadOnlyCollection<HellscapeModsDat> LoadHellscapeModsDat()
    {
        return HellscapeModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeMonsterPacksDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeMonsterPacksDat.</returns>
    public ReadOnlyCollection<HellscapeMonsterPacksDat> LoadHellscapeMonsterPacksDat()
    {
        return HellscapeMonsterPacksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapePassivesDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapePassivesDat.</returns>
    public ReadOnlyCollection<HellscapePassivesDat> LoadHellscapePassivesDat()
    {
        return HellscapePassivesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapePassiveTreeDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapePassiveTreeDat.</returns>
    public ReadOnlyCollection<HellscapePassiveTreeDat> LoadHellscapePassiveTreeDat()
    {
        return HellscapePassiveTreeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArchnemesisMetaRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of ArchnemesisMetaRewardsDat.</returns>
    public ReadOnlyCollection<ArchnemesisMetaRewardsDat> LoadArchnemesisMetaRewardsDat()
    {
        return ArchnemesisMetaRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArchnemesisModComboAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of ArchnemesisModComboAchievementsDat.</returns>
    public ReadOnlyCollection<ArchnemesisModComboAchievementsDat> LoadArchnemesisModComboAchievementsDat()
    {
        return ArchnemesisModComboAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArchnemesisModsDat data.
    /// </summary>
    /// <returns>readonly collection of ArchnemesisModsDat.</returns>
    public ReadOnlyCollection<ArchnemesisModsDat> LoadArchnemesisModsDat()
    {
        return ArchnemesisModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArchnemesisModVisualsDat data.
    /// </summary>
    /// <returns>readonly collection of ArchnemesisModVisualsDat.</returns>
    public ReadOnlyCollection<ArchnemesisModVisualsDat> LoadArchnemesisModVisualsDat()
    {
        return ArchnemesisModVisualsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArchnemesisRecipesDat data.
    /// </summary>
    /// <returns>readonly collection of ArchnemesisRecipesDat.</returns>
    public ReadOnlyCollection<ArchnemesisRecipesDat> LoadArchnemesisRecipesDat()
    {
        return ArchnemesisRecipesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasPrimordialAltarChoicesDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPrimordialAltarChoicesDat.</returns>
    public ReadOnlyCollection<AtlasPrimordialAltarChoicesDat> LoadAtlasPrimordialAltarChoicesDat()
    {
        return AtlasPrimordialAltarChoicesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasPrimordialAltarChoiceTypesDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPrimordialAltarChoiceTypesDat.</returns>
    public ReadOnlyCollection<AtlasPrimordialAltarChoiceTypesDat> LoadAtlasPrimordialAltarChoiceTypesDat()
    {
        return AtlasPrimordialAltarChoiceTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasPrimordialBossesDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPrimordialBossesDat.</returns>
    public ReadOnlyCollection<AtlasPrimordialBossesDat> LoadAtlasPrimordialBossesDat()
    {
        return AtlasPrimordialBossesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasPrimordialBossInfluenceDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPrimordialBossInfluenceDat.</returns>
    public ReadOnlyCollection<AtlasPrimordialBossInfluenceDat> LoadAtlasPrimordialBossInfluenceDat()
    {
        return AtlasPrimordialBossInfluenceDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasPrimordialBossOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPrimordialBossOptionsDat.</returns>
    public ReadOnlyCollection<AtlasPrimordialBossOptionsDat> LoadAtlasPrimordialBossOptionsDat()
    {
        return AtlasPrimordialBossOptionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PrimordialBossLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of PrimordialBossLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<PrimordialBossLifeScalingPerLevelDat> LoadPrimordialBossLifeScalingPerLevelDat()
    {
        return PrimordialBossLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasUpgradesInventoryLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasUpgradesInventoryLayoutDat.</returns>
    public ReadOnlyCollection<AtlasUpgradesInventoryLayoutDat> LoadAtlasUpgradesInventoryLayoutDat()
    {
        return AtlasUpgradesInventoryLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasPassiveSkillTreeGroupTypeDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPassiveSkillTreeGroupTypeDat.</returns>
    public ReadOnlyCollection<AtlasPassiveSkillTreeGroupTypeDat> LoadAtlasPassiveSkillTreeGroupTypeDat()
    {
        return AtlasPassiveSkillTreeGroupTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets KiracLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of KiracLevelsDat.</returns>
    public ReadOnlyCollection<KiracLevelsDat> LoadKiracLevelsDat()
    {
        return KiracLevelsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ScoutingReportsDat data.
    /// </summary>
    /// <returns>readonly collection of ScoutingReportsDat.</returns>
    public ReadOnlyCollection<ScoutingReportsDat> LoadScoutingReportsDat()
    {
        return ScoutingReportsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DroneBaseTypesDat data.
    /// </summary>
    /// <returns>readonly collection of DroneBaseTypesDat.</returns>
    public ReadOnlyCollection<DroneBaseTypesDat> LoadDroneBaseTypesDat()
    {
        return DroneBaseTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DroneTypesDat data.
    /// </summary>
    /// <returns>readonly collection of DroneTypesDat.</returns>
    public ReadOnlyCollection<DroneTypesDat> LoadDroneTypesDat()
    {
        return DroneTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SentinelCraftingCurrencyDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelCraftingCurrencyDat.</returns>
    public ReadOnlyCollection<SentinelCraftingCurrencyDat> LoadSentinelCraftingCurrencyDat()
    {
        return SentinelCraftingCurrencyDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SentinelDroneInventoryLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelDroneInventoryLayoutDat.</returns>
    public ReadOnlyCollection<SentinelDroneInventoryLayoutDat> LoadSentinelDroneInventoryLayoutDat()
    {
        return SentinelDroneInventoryLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SentinelPassivesDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelPassivesDat.</returns>
    public ReadOnlyCollection<SentinelPassivesDat> LoadSentinelPassivesDat()
    {
        return SentinelPassivesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SentinelPassiveStatsDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelPassiveStatsDat.</returns>
    public ReadOnlyCollection<SentinelPassiveStatsDat> LoadSentinelPassiveStatsDat()
    {
        return SentinelPassiveStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SentinelPassiveTypesDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelPassiveTypesDat.</returns>
    public ReadOnlyCollection<SentinelPassiveTypesDat> LoadSentinelPassiveTypesDat()
    {
        return SentinelPassiveTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SentinelPowerExpLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelPowerExpLevelsDat.</returns>
    public ReadOnlyCollection<SentinelPowerExpLevelsDat> LoadSentinelPowerExpLevelsDat()
    {
        return SentinelPowerExpLevelsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SentinelStorageLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelStorageLayoutDat.</returns>
    public ReadOnlyCollection<SentinelStorageLayoutDat> LoadSentinelStorageLayoutDat()
    {
        return SentinelStorageLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SentinelTaggedMonsterStatsDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelTaggedMonsterStatsDat.</returns>
    public ReadOnlyCollection<SentinelTaggedMonsterStatsDat> LoadSentinelTaggedMonsterStatsDat()
    {
        return SentinelTaggedMonsterStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ClientLakeDifficultyDat data.
    /// </summary>
    /// <returns>readonly collection of ClientLakeDifficultyDat.</returns>
    public ReadOnlyCollection<ClientLakeDifficultyDat> LoadClientLakeDifficultyDat()
    {
        return ClientLakeDifficultyDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LakeBossLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of LakeBossLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<LakeBossLifeScalingPerLevelDat> LoadLakeBossLifeScalingPerLevelDat()
    {
        return LakeBossLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LakeMetaOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of LakeMetaOptionsDat.</returns>
    public ReadOnlyCollection<LakeMetaOptionsDat> LoadLakeMetaOptionsDat()
    {
        return LakeMetaOptionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LakeMetaOptionsUnlockTextDat data.
    /// </summary>
    /// <returns>readonly collection of LakeMetaOptionsUnlockTextDat.</returns>
    public ReadOnlyCollection<LakeMetaOptionsUnlockTextDat> LoadLakeMetaOptionsUnlockTextDat()
    {
        return LakeMetaOptionsUnlockTextDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LakeRoomCompletionDat data.
    /// </summary>
    /// <returns>readonly collection of LakeRoomCompletionDat.</returns>
    public ReadOnlyCollection<LakeRoomCompletionDat> LoadLakeRoomCompletionDat()
    {
        return LakeRoomCompletionDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LakeRoomsDat data.
    /// </summary>
    /// <returns>readonly collection of LakeRoomsDat.</returns>
    public ReadOnlyCollection<LakeRoomsDat> LoadLakeRoomsDat()
    {
        return LakeRoomsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WeaponPassiveSkillTypesDat data.
    /// </summary>
    /// <returns>readonly collection of WeaponPassiveSkillTypesDat.</returns>
    public ReadOnlyCollection<WeaponPassiveSkillTypesDat> LoadWeaponPassiveSkillTypesDat()
    {
        return WeaponPassiveSkillTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WeaponPassiveTreeBalancePerItemLevelDat data.
    /// </summary>
    /// <returns>readonly collection of WeaponPassiveTreeBalancePerItemLevelDat.</returns>
    public ReadOnlyCollection<WeaponPassiveTreeBalancePerItemLevelDat> LoadWeaponPassiveTreeBalancePerItemLevelDat()
    {
        return WeaponPassiveTreeBalancePerItemLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WeaponPassiveTreeUniqueBaseTypesDat data.
    /// </summary>
    /// <returns>readonly collection of WeaponPassiveTreeUniqueBaseTypesDat.</returns>
    public ReadOnlyCollection<WeaponPassiveTreeUniqueBaseTypesDat> LoadWeaponPassiveTreeUniqueBaseTypesDat()
    {
        return WeaponPassiveTreeUniqueBaseTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WeaponPassiveSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of WeaponPassiveSkillsDat.</returns>
    public ReadOnlyCollection<WeaponPassiveSkillsDat> LoadWeaponPassiveSkillsDat()
    {
        return WeaponPassiveSkillsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AchievementItemRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of AchievementItemRewardsDat.</returns>
    public ReadOnlyCollection<AchievementItemRewardsDat> LoadAchievementItemRewardsDat()
    {
        return AchievementItemRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AchievementItemsDat data.
    /// </summary>
    /// <returns>readonly collection of AchievementItemsDat.</returns>
    public ReadOnlyCollection<AchievementItemsDat> LoadAchievementItemsDat()
    {
        return AchievementItemsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of AchievementsDat.</returns>
    public ReadOnlyCollection<AchievementsDat> LoadAchievementsDat()
    {
        return AchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AchievementSetRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of AchievementSetRewardsDat.</returns>
    public ReadOnlyCollection<AchievementSetRewardsDat> LoadAchievementSetRewardsDat()
    {
        return AchievementSetRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AchievementSetsDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of AchievementSetsDisplayDat.</returns>
    public ReadOnlyCollection<AchievementSetsDisplayDat> LoadAchievementSetsDisplayDat()
    {
        return AchievementSetsDisplayDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ActiveSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of ActiveSkillsDat.</returns>
    public ReadOnlyCollection<ActiveSkillsDat> LoadActiveSkillsDat()
    {
        return ActiveSkillsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ActiveSkillTypeDat data.
    /// </summary>
    /// <returns>readonly collection of ActiveSkillTypeDat.</returns>
    public ReadOnlyCollection<ActiveSkillTypeDat> LoadActiveSkillTypeDat()
    {
        return ActiveSkillTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ActsDat data.
    /// </summary>
    /// <returns>readonly collection of ActsDat.</returns>
    public ReadOnlyCollection<ActsDat> LoadActsDat()
    {
        return ActsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AddBuffToTargetVarietiesDat data.
    /// </summary>
    /// <returns>readonly collection of AddBuffToTargetVarietiesDat.</returns>
    public ReadOnlyCollection<AddBuffToTargetVarietiesDat> LoadAddBuffToTargetVarietiesDat()
    {
        return AddBuffToTargetVarietiesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AdditionalLifeScalingDat data.
    /// </summary>
    /// <returns>readonly collection of AdditionalLifeScalingDat.</returns>
    public ReadOnlyCollection<AdditionalLifeScalingDat> LoadAdditionalLifeScalingDat()
    {
        return AdditionalLifeScalingDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AdditionalMonsterPacksFromStatsDat data.
    /// </summary>
    /// <returns>readonly collection of AdditionalMonsterPacksFromStatsDat.</returns>
    public ReadOnlyCollection<AdditionalMonsterPacksFromStatsDat> LoadAdditionalMonsterPacksFromStatsDat()
    {
        return AdditionalMonsterPacksFromStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AdvancedSkillsTutorialDat data.
    /// </summary>
    /// <returns>readonly collection of AdvancedSkillsTutorialDat.</returns>
    public ReadOnlyCollection<AdvancedSkillsTutorialDat> LoadAdvancedSkillsTutorialDat()
    {
        return AdvancedSkillsTutorialDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AegisVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of AegisVariationsDat.</returns>
    public ReadOnlyCollection<AegisVariationsDat> LoadAegisVariationsDat()
    {
        return AegisVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AlternatePassiveAdditionsDat data.
    /// </summary>
    /// <returns>readonly collection of AlternatePassiveAdditionsDat.</returns>
    public ReadOnlyCollection<AlternatePassiveAdditionsDat> LoadAlternatePassiveAdditionsDat()
    {
        return AlternatePassiveAdditionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AlternatePassiveSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of AlternatePassiveSkillsDat.</returns>
    public ReadOnlyCollection<AlternatePassiveSkillsDat> LoadAlternatePassiveSkillsDat()
    {
        return AlternatePassiveSkillsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AlternateSkillTargetingBehavioursDat data.
    /// </summary>
    /// <returns>readonly collection of AlternateSkillTargetingBehavioursDat.</returns>
    public ReadOnlyCollection<AlternateSkillTargetingBehavioursDat> LoadAlternateSkillTargetingBehavioursDat()
    {
        return AlternateSkillTargetingBehavioursDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AlternateTreeVersionsDat data.
    /// </summary>
    /// <returns>readonly collection of AlternateTreeVersionsDat.</returns>
    public ReadOnlyCollection<AlternateTreeVersionsDat> LoadAlternateTreeVersionsDat()
    {
        return AlternateTreeVersionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AnimatedObjectFlagsDat data.
    /// </summary>
    /// <returns>readonly collection of AnimatedObjectFlagsDat.</returns>
    public ReadOnlyCollection<AnimatedObjectFlagsDat> LoadAnimatedObjectFlagsDat()
    {
        return AnimatedObjectFlagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AnimationDat data.
    /// </summary>
    /// <returns>readonly collection of AnimationDat.</returns>
    public ReadOnlyCollection<AnimationDat> LoadAnimationDat()
    {
        return AnimationDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ApplyDamageFunctionsDat data.
    /// </summary>
    /// <returns>readonly collection of ApplyDamageFunctionsDat.</returns>
    public ReadOnlyCollection<ApplyDamageFunctionsDat> LoadApplyDamageFunctionsDat()
    {
        return ApplyDamageFunctionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArchetypeRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of ArchetypeRewardsDat.</returns>
    public ReadOnlyCollection<ArchetypeRewardsDat> LoadArchetypeRewardsDat()
    {
        return ArchetypeRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArchetypesDat data.
    /// </summary>
    /// <returns>readonly collection of ArchetypesDat.</returns>
    public ReadOnlyCollection<ArchetypesDat> LoadArchetypesDat()
    {
        return ArchetypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AreaInfluenceDoodadsDat data.
    /// </summary>
    /// <returns>readonly collection of AreaInfluenceDoodadsDat.</returns>
    public ReadOnlyCollection<AreaInfluenceDoodadsDat> LoadAreaInfluenceDoodadsDat()
    {
        return AreaInfluenceDoodadsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AreaTransitionAnimationsDat data.
    /// </summary>
    /// <returns>readonly collection of AreaTransitionAnimationsDat.</returns>
    public ReadOnlyCollection<AreaTransitionAnimationsDat> LoadAreaTransitionAnimationsDat()
    {
        return AreaTransitionAnimationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AreaTransitionAnimationTypesDat data.
    /// </summary>
    /// <returns>readonly collection of AreaTransitionAnimationTypesDat.</returns>
    public ReadOnlyCollection<AreaTransitionAnimationTypesDat> LoadAreaTransitionAnimationTypesDat()
    {
        return AreaTransitionAnimationTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AreaTransitionInfoDat data.
    /// </summary>
    /// <returns>readonly collection of AreaTransitionInfoDat.</returns>
    public ReadOnlyCollection<AreaTransitionInfoDat> LoadAreaTransitionInfoDat()
    {
        return AreaTransitionInfoDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArmourTypesDat data.
    /// </summary>
    /// <returns>readonly collection of ArmourTypesDat.</returns>
    public ReadOnlyCollection<ArmourTypesDat> LoadArmourTypesDat()
    {
        return ArmourTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AscendancyDat data.
    /// </summary>
    /// <returns>readonly collection of AscendancyDat.</returns>
    public ReadOnlyCollection<AscendancyDat> LoadAscendancyDat()
    {
        return AscendancyDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasAwakeningStatsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasAwakeningStatsDat.</returns>
    public ReadOnlyCollection<AtlasAwakeningStatsDat> LoadAtlasAwakeningStatsDat()
    {
        return AtlasAwakeningStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasBaseTypeDropsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasBaseTypeDropsDat.</returns>
    public ReadOnlyCollection<AtlasBaseTypeDropsDat> LoadAtlasBaseTypeDropsDat()
    {
        return AtlasBaseTypeDropsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasFogDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasFogDat.</returns>
    public ReadOnlyCollection<AtlasFogDat> LoadAtlasFogDat()
    {
        return AtlasFogDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasInfluenceDataDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasInfluenceDataDat.</returns>
    public ReadOnlyCollection<AtlasInfluenceDataDat> LoadAtlasInfluenceDataDat()
    {
        return AtlasInfluenceDataDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasInfluenceOutcomesDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasInfluenceOutcomesDat.</returns>
    public ReadOnlyCollection<AtlasInfluenceOutcomesDat> LoadAtlasInfluenceOutcomesDat()
    {
        return AtlasInfluenceOutcomesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasInfluenceSetsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasInfluenceSetsDat.</returns>
    public ReadOnlyCollection<AtlasInfluenceSetsDat> LoadAtlasInfluenceSetsDat()
    {
        return AtlasInfluenceSetsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasModsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasModsDat.</returns>
    public ReadOnlyCollection<AtlasModsDat> LoadAtlasModsDat()
    {
        return AtlasModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasFavouredMapSlotsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasFavouredMapSlotsDat.</returns>
    public ReadOnlyCollection<AtlasFavouredMapSlotsDat> LoadAtlasFavouredMapSlotsDat()
    {
        return AtlasFavouredMapSlotsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasNodeDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasNodeDat.</returns>
    public ReadOnlyCollection<AtlasNodeDat> LoadAtlasNodeDat()
    {
        return AtlasNodeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasNodeDefinitionDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasNodeDefinitionDat.</returns>
    public ReadOnlyCollection<AtlasNodeDefinitionDat> LoadAtlasNodeDefinitionDat()
    {
        return AtlasNodeDefinitionDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasPositionsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPositionsDat.</returns>
    public ReadOnlyCollection<AtlasPositionsDat> LoadAtlasPositionsDat()
    {
        return AtlasPositionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasRegionsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasRegionsDat.</returns>
    public ReadOnlyCollection<AtlasRegionsDat> LoadAtlasRegionsDat()
    {
        return AtlasRegionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasRegionUpgradesInventoryLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasRegionUpgradesInventoryLayoutDat.</returns>
    public ReadOnlyCollection<AtlasRegionUpgradesInventoryLayoutDat> LoadAtlasRegionUpgradesInventoryLayoutDat()
    {
        return AtlasRegionUpgradesInventoryLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasRegionUpgradeRegionsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasRegionUpgradeRegionsDat.</returns>
    public ReadOnlyCollection<AtlasRegionUpgradeRegionsDat> LoadAtlasRegionUpgradeRegionsDat()
    {
        return AtlasRegionUpgradeRegionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasSectorDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasSectorDat.</returns>
    public ReadOnlyCollection<AtlasSectorDat> LoadAtlasSectorDat()
    {
        return AtlasSectorDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AwardDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of AwardDisplayDat.</returns>
    public ReadOnlyCollection<AwardDisplayDat> LoadAwardDisplayDat()
    {
        return AwardDisplayDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BackendErrorsDat data.
    /// </summary>
    /// <returns>readonly collection of BackendErrorsDat.</returns>
    public ReadOnlyCollection<BackendErrorsDat> LoadBackendErrorsDat()
    {
        return BackendErrorsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BaseItemTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BaseItemTypesDat.</returns>
    public ReadOnlyCollection<BaseItemTypesDat> LoadBaseItemTypesDat()
    {
        return BaseItemTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BindableVirtualKeysDat data.
    /// </summary>
    /// <returns>readonly collection of BindableVirtualKeysDat.</returns>
    public ReadOnlyCollection<BindableVirtualKeysDat> LoadBindableVirtualKeysDat()
    {
        return BindableVirtualKeysDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of BlightStashTabLayoutDat.</returns>
    public ReadOnlyCollection<BlightStashTabLayoutDat> LoadBlightStashTabLayoutDat()
    {
        return BlightStashTabLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BloodTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BloodTypesDat.</returns>
    public ReadOnlyCollection<BloodTypesDat> LoadBloodTypesDat()
    {
        return BloodTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BuffDefinitionsDat data.
    /// </summary>
    /// <returns>readonly collection of BuffDefinitionsDat.</returns>
    public ReadOnlyCollection<BuffDefinitionsDat> LoadBuffDefinitionsDat()
    {
        return BuffDefinitionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BuffTemplatesDat data.
    /// </summary>
    /// <returns>readonly collection of BuffTemplatesDat.</returns>
    public ReadOnlyCollection<BuffTemplatesDat> LoadBuffTemplatesDat()
    {
        return BuffTemplatesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BuffVisualOrbArtDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualOrbArtDat.</returns>
    public ReadOnlyCollection<BuffVisualOrbArtDat> LoadBuffVisualOrbArtDat()
    {
        return BuffVisualOrbArtDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BuffVisualOrbsDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualOrbsDat.</returns>
    public ReadOnlyCollection<BuffVisualOrbsDat> LoadBuffVisualOrbsDat()
    {
        return BuffVisualOrbsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BuffVisualOrbTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualOrbTypesDat.</returns>
    public ReadOnlyCollection<BuffVisualOrbTypesDat> LoadBuffVisualOrbTypesDat()
    {
        return BuffVisualOrbTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BuffVisualsDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualsDat.</returns>
    public ReadOnlyCollection<BuffVisualsDat> LoadBuffVisualsDat()
    {
        return BuffVisualsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BuffVisualsArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualsArtVariationsDat.</returns>
    public ReadOnlyCollection<BuffVisualsArtVariationsDat> LoadBuffVisualsArtVariationsDat()
    {
        return BuffVisualsArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BuffVisualSetEntriesDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualSetEntriesDat.</returns>
    public ReadOnlyCollection<BuffVisualSetEntriesDat> LoadBuffVisualSetEntriesDat()
    {
        return BuffVisualSetEntriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterAudioEventsDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterAudioEventsDat.</returns>
    public ReadOnlyCollection<CharacterAudioEventsDat> LoadCharacterAudioEventsDat()
    {
        return CharacterAudioEventsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterEventTextAudioDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterEventTextAudioDat.</returns>
    public ReadOnlyCollection<CharacterEventTextAudioDat> LoadCharacterEventTextAudioDat()
    {
        return CharacterEventTextAudioDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterPanelDescriptionModesDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterPanelDescriptionModesDat.</returns>
    public ReadOnlyCollection<CharacterPanelDescriptionModesDat> LoadCharacterPanelDescriptionModesDat()
    {
        return CharacterPanelDescriptionModesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterPanelStatsDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterPanelStatsDat.</returns>
    public ReadOnlyCollection<CharacterPanelStatsDat> LoadCharacterPanelStatsDat()
    {
        return CharacterPanelStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterPanelTabsDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterPanelTabsDat.</returns>
    public ReadOnlyCollection<CharacterPanelTabsDat> LoadCharacterPanelTabsDat()
    {
        return CharacterPanelTabsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharactersDat data.
    /// </summary>
    /// <returns>readonly collection of CharactersDat.</returns>
    public ReadOnlyCollection<CharactersDat> LoadCharactersDat()
    {
        return CharactersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterStartQuestStateDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterStartQuestStateDat.</returns>
    public ReadOnlyCollection<CharacterStartQuestStateDat> LoadCharacterStartQuestStateDat()
    {
        return CharacterStartQuestStateDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterStartStatesDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterStartStatesDat.</returns>
    public ReadOnlyCollection<CharacterStartStatesDat> LoadCharacterStartStatesDat()
    {
        return CharacterStartStatesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterStartStateSetDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterStartStateSetDat.</returns>
    public ReadOnlyCollection<CharacterStartStateSetDat> LoadCharacterStartStateSetDat()
    {
        return CharacterStartStateSetDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterTextAudioDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterTextAudioDat.</returns>
    public ReadOnlyCollection<CharacterTextAudioDat> LoadCharacterTextAudioDat()
    {
        return CharacterTextAudioDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ChatIconsDat data.
    /// </summary>
    /// <returns>readonly collection of ChatIconsDat.</returns>
    public ReadOnlyCollection<ChatIconsDat> LoadChatIconsDat()
    {
        return ChatIconsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ChestClustersDat data.
    /// </summary>
    /// <returns>readonly collection of ChestClustersDat.</returns>
    public ReadOnlyCollection<ChestClustersDat> LoadChestClustersDat()
    {
        return ChestClustersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ChestEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of ChestEffectsDat.</returns>
    public ReadOnlyCollection<ChestEffectsDat> LoadChestEffectsDat()
    {
        return ChestEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ChestsDat data.
    /// </summary>
    /// <returns>readonly collection of ChestsDat.</returns>
    public ReadOnlyCollection<ChestsDat> LoadChestsDat()
    {
        return ChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ClientStringsDat data.
    /// </summary>
    /// <returns>readonly collection of ClientStringsDat.</returns>
    public ReadOnlyCollection<ClientStringsDat> LoadClientStringsDat()
    {
        return ClientStringsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ClientLeagueActionDat data.
    /// </summary>
    /// <returns>readonly collection of ClientLeagueActionDat.</returns>
    public ReadOnlyCollection<ClientLeagueActionDat> LoadClientLeagueActionDat()
    {
        return ClientLeagueActionDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CloneShotDat data.
    /// </summary>
    /// <returns>readonly collection of CloneShotDat.</returns>
    public ReadOnlyCollection<CloneShotDat> LoadCloneShotDat()
    {
        return CloneShotDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ColoursDat data.
    /// </summary>
    /// <returns>readonly collection of ColoursDat.</returns>
    public ReadOnlyCollection<ColoursDat> LoadColoursDat()
    {
        return ColoursDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CommandsDat data.
    /// </summary>
    /// <returns>readonly collection of CommandsDat.</returns>
    public ReadOnlyCollection<CommandsDat> LoadCommandsDat()
    {
        return CommandsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ComponentAttributeRequirementsDat data.
    /// </summary>
    /// <returns>readonly collection of ComponentAttributeRequirementsDat.</returns>
    public ReadOnlyCollection<ComponentAttributeRequirementsDat> LoadComponentAttributeRequirementsDat()
    {
        return ComponentAttributeRequirementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ComponentChargesDat data.
    /// </summary>
    /// <returns>readonly collection of ComponentChargesDat.</returns>
    public ReadOnlyCollection<ComponentChargesDat> LoadComponentChargesDat()
    {
        return ComponentChargesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CoreLeaguesDat data.
    /// </summary>
    /// <returns>readonly collection of CoreLeaguesDat.</returns>
    public ReadOnlyCollection<CoreLeaguesDat> LoadCoreLeaguesDat()
    {
        return CoreLeaguesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CostTypesDat data.
    /// </summary>
    /// <returns>readonly collection of CostTypesDat.</returns>
    public ReadOnlyCollection<CostTypesDat> LoadCostTypesDat()
    {
        return CostTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CraftingBenchOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of CraftingBenchOptionsDat.</returns>
    public ReadOnlyCollection<CraftingBenchOptionsDat> LoadCraftingBenchOptionsDat()
    {
        return CraftingBenchOptionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CraftingBenchSortCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of CraftingBenchSortCategoriesDat.</returns>
    public ReadOnlyCollection<CraftingBenchSortCategoriesDat> LoadCraftingBenchSortCategoriesDat()
    {
        return CraftingBenchSortCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CraftingBenchUnlockCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of CraftingBenchUnlockCategoriesDat.</returns>
    public ReadOnlyCollection<CraftingBenchUnlockCategoriesDat> LoadCraftingBenchUnlockCategoriesDat()
    {
        return CraftingBenchUnlockCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CraftingItemClassCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of CraftingItemClassCategoriesDat.</returns>
    public ReadOnlyCollection<CraftingItemClassCategoriesDat> LoadCraftingItemClassCategoriesDat()
    {
        return CraftingItemClassCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CurrencyItemsDat data.
    /// </summary>
    /// <returns>readonly collection of CurrencyItemsDat.</returns>
    public ReadOnlyCollection<CurrencyItemsDat> LoadCurrencyItemsDat()
    {
        return CurrencyItemsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CurrencyStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of CurrencyStashTabLayoutDat.</returns>
    public ReadOnlyCollection<CurrencyStashTabLayoutDat> LoadCurrencyStashTabLayoutDat()
    {
        return CurrencyStashTabLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CustomLeagueModsDat data.
    /// </summary>
    /// <returns>readonly collection of CustomLeagueModsDat.</returns>
    public ReadOnlyCollection<CustomLeagueModsDat> LoadCustomLeagueModsDat()
    {
        return CustomLeagueModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DaemonSpawningDataDat data.
    /// </summary>
    /// <returns>readonly collection of DaemonSpawningDataDat.</returns>
    public ReadOnlyCollection<DaemonSpawningDataDat> LoadDaemonSpawningDataDat()
    {
        return DaemonSpawningDataDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DamageHitEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of DamageHitEffectsDat.</returns>
    public ReadOnlyCollection<DamageHitEffectsDat> LoadDamageHitEffectsDat()
    {
        return DamageHitEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DamageParticleEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of DamageParticleEffectsDat.</returns>
    public ReadOnlyCollection<DamageParticleEffectsDat> LoadDamageParticleEffectsDat()
    {
        return DamageParticleEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DancesDat data.
    /// </summary>
    /// <returns>readonly collection of DancesDat.</returns>
    public ReadOnlyCollection<DancesDat> LoadDancesDat()
    {
        return DancesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DaressoPitFightsDat data.
    /// </summary>
    /// <returns>readonly collection of DaressoPitFightsDat.</returns>
    public ReadOnlyCollection<DaressoPitFightsDat> LoadDaressoPitFightsDat()
    {
        return DaressoPitFightsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DefaultMonsterStatsDat data.
    /// </summary>
    /// <returns>readonly collection of DefaultMonsterStatsDat.</returns>
    public ReadOnlyCollection<DefaultMonsterStatsDat> LoadDefaultMonsterStatsDat()
    {
        return DefaultMonsterStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DeliriumStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of DeliriumStashTabLayoutDat.</returns>
    public ReadOnlyCollection<DeliriumStashTabLayoutDat> LoadDeliriumStashTabLayoutDat()
    {
        return DeliriumStashTabLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of DelveStashTabLayoutDat.</returns>
    public ReadOnlyCollection<DelveStashTabLayoutDat> LoadDelveStashTabLayoutDat()
    {
        return DelveStashTabLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DescentExilesDat data.
    /// </summary>
    /// <returns>readonly collection of DescentExilesDat.</returns>
    public ReadOnlyCollection<DescentExilesDat> LoadDescentExilesDat()
    {
        return DescentExilesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DescentRewardChestsDat data.
    /// </summary>
    /// <returns>readonly collection of DescentRewardChestsDat.</returns>
    public ReadOnlyCollection<DescentRewardChestsDat> LoadDescentRewardChestsDat()
    {
        return DescentRewardChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DescentStarterChestDat data.
    /// </summary>
    /// <returns>readonly collection of DescentStarterChestDat.</returns>
    public ReadOnlyCollection<DescentStarterChestDat> LoadDescentStarterChestDat()
    {
        return DescentStarterChestDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DialogueEventDat data.
    /// </summary>
    /// <returns>readonly collection of DialogueEventDat.</returns>
    public ReadOnlyCollection<DialogueEventDat> LoadDialogueEventDat()
    {
        return DialogueEventDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DisplayMinionMonsterTypeDat data.
    /// </summary>
    /// <returns>readonly collection of DisplayMinionMonsterTypeDat.</returns>
    public ReadOnlyCollection<DisplayMinionMonsterTypeDat> LoadDisplayMinionMonsterTypeDat()
    {
        return DisplayMinionMonsterTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DivinationCardStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of DivinationCardStashTabLayoutDat.</returns>
    public ReadOnlyCollection<DivinationCardStashTabLayoutDat> LoadDivinationCardStashTabLayoutDat()
    {
        return DivinationCardStashTabLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DoorsDat data.
    /// </summary>
    /// <returns>readonly collection of DoorsDat.</returns>
    public ReadOnlyCollection<DoorsDat> LoadDoorsDat()
    {
        return DoorsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DropEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of DropEffectsDat.</returns>
    public ReadOnlyCollection<DropEffectsDat> LoadDropEffectsDat()
    {
        return DropEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DropPoolDat data.
    /// </summary>
    /// <returns>readonly collection of DropPoolDat.</returns>
    public ReadOnlyCollection<DropPoolDat> LoadDropPoolDat()
    {
        return DropPoolDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EclipseModsDat data.
    /// </summary>
    /// <returns>readonly collection of EclipseModsDat.</returns>
    public ReadOnlyCollection<EclipseModsDat> LoadEclipseModsDat()
    {
        return EclipseModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EffectDrivenSkillDat data.
    /// </summary>
    /// <returns>readonly collection of EffectDrivenSkillDat.</returns>
    public ReadOnlyCollection<EffectDrivenSkillDat> LoadEffectDrivenSkillDat()
    {
        return EffectDrivenSkillDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EffectivenessCostConstantsDat data.
    /// </summary>
    /// <returns>readonly collection of EffectivenessCostConstantsDat.</returns>
    public ReadOnlyCollection<EffectivenessCostConstantsDat> LoadEffectivenessCostConstantsDat()
    {
        return EffectivenessCostConstantsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EinharMissionsDat data.
    /// </summary>
    /// <returns>readonly collection of EinharMissionsDat.</returns>
    public ReadOnlyCollection<EinharMissionsDat> LoadEinharMissionsDat()
    {
        return EinharMissionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EinharPackFallbackDat data.
    /// </summary>
    /// <returns>readonly collection of EinharPackFallbackDat.</returns>
    public ReadOnlyCollection<EinharPackFallbackDat> LoadEinharPackFallbackDat()
    {
        return EinharPackFallbackDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EndlessLedgeChestsDat data.
    /// </summary>
    /// <returns>readonly collection of EndlessLedgeChestsDat.</returns>
    public ReadOnlyCollection<EndlessLedgeChestsDat> LoadEndlessLedgeChestsDat()
    {
        return EndlessLedgeChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EnvironmentsDat data.
    /// </summary>
    /// <returns>readonly collection of EnvironmentsDat.</returns>
    public ReadOnlyCollection<EnvironmentsDat> LoadEnvironmentsDat()
    {
        return EnvironmentsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EnvironmentTransitionsDat data.
    /// </summary>
    /// <returns>readonly collection of EnvironmentTransitionsDat.</returns>
    public ReadOnlyCollection<EnvironmentTransitionsDat> LoadEnvironmentTransitionsDat()
    {
        return EnvironmentTransitionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EssenceStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of EssenceStashTabLayoutDat.</returns>
    public ReadOnlyCollection<EssenceStashTabLayoutDat> LoadEssenceStashTabLayoutDat()
    {
        return EssenceStashTabLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EventSeasonDat data.
    /// </summary>
    /// <returns>readonly collection of EventSeasonDat.</returns>
    public ReadOnlyCollection<EventSeasonDat> LoadEventSeasonDat()
    {
        return EventSeasonDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EventSeasonRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of EventSeasonRewardsDat.</returns>
    public ReadOnlyCollection<EventSeasonRewardsDat> LoadEventSeasonRewardsDat()
    {
        return EventSeasonRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EvergreenAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of EvergreenAchievementsDat.</returns>
    public ReadOnlyCollection<EvergreenAchievementsDat> LoadEvergreenAchievementsDat()
    {
        return EvergreenAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExecuteGEALDat data.
    /// </summary>
    /// <returns>readonly collection of ExecuteGEALDat.</returns>
    public ReadOnlyCollection<ExecuteGEALDat> LoadExecuteGEALDat()
    {
        return ExecuteGEALDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpandingPulseDat data.
    /// </summary>
    /// <returns>readonly collection of ExpandingPulseDat.</returns>
    public ReadOnlyCollection<ExpandingPulseDat> LoadExpandingPulseDat()
    {
        return ExpandingPulseDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExperienceLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of ExperienceLevelsDat.</returns>
    public ReadOnlyCollection<ExperienceLevelsDat> LoadExperienceLevelsDat()
    {
        return ExperienceLevelsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExplodingStormBuffsDat data.
    /// </summary>
    /// <returns>readonly collection of ExplodingStormBuffsDat.</returns>
    public ReadOnlyCollection<ExplodingStormBuffsDat> LoadExplodingStormBuffsDat()
    {
        return ExplodingStormBuffsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExtraTerrainFeaturesDat data.
    /// </summary>
    /// <returns>readonly collection of ExtraTerrainFeaturesDat.</returns>
    public ReadOnlyCollection<ExtraTerrainFeaturesDat> LoadExtraTerrainFeaturesDat()
    {
        return ExtraTerrainFeaturesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets FixedHideoutDoodadTypesDat data.
    /// </summary>
    /// <returns>readonly collection of FixedHideoutDoodadTypesDat.</returns>
    public ReadOnlyCollection<FixedHideoutDoodadTypesDat> LoadFixedHideoutDoodadTypesDat()
    {
        return FixedHideoutDoodadTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets FixedMissionsDat data.
    /// </summary>
    /// <returns>readonly collection of FixedMissionsDat.</returns>
    public ReadOnlyCollection<FixedMissionsDat> LoadFixedMissionsDat()
    {
        return FixedMissionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets FlasksDat data.
    /// </summary>
    /// <returns>readonly collection of FlasksDat.</returns>
    public ReadOnlyCollection<FlasksDat> LoadFlasksDat()
    {
        return FlasksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets FlavourTextDat data.
    /// </summary>
    /// <returns>readonly collection of FlavourTextDat.</returns>
    public ReadOnlyCollection<FlavourTextDat> LoadFlavourTextDat()
    {
        return FlavourTextDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets FootprintsDat data.
    /// </summary>
    /// <returns>readonly collection of FootprintsDat.</returns>
    public ReadOnlyCollection<FootprintsDat> LoadFootprintsDat()
    {
        return FootprintsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets FootstepAudioDat data.
    /// </summary>
    /// <returns>readonly collection of FootstepAudioDat.</returns>
    public ReadOnlyCollection<FootstepAudioDat> LoadFootstepAudioDat()
    {
        return FootstepAudioDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets FragmentStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of FragmentStashTabLayoutDat.</returns>
    public ReadOnlyCollection<FragmentStashTabLayoutDat> LoadFragmentStashTabLayoutDat()
    {
        return FragmentStashTabLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GameConstantsDat data.
    /// </summary>
    /// <returns>readonly collection of GameConstantsDat.</returns>
    public ReadOnlyCollection<GameConstantsDat> LoadGameConstantsDat()
    {
        return GameConstantsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GameLogosDat data.
    /// </summary>
    /// <returns>readonly collection of GameLogosDat.</returns>
    public ReadOnlyCollection<GameLogosDat> LoadGameLogosDat()
    {
        return GameLogosDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GameObjectTasksDat data.
    /// </summary>
    /// <returns>readonly collection of GameObjectTasksDat.</returns>
    public ReadOnlyCollection<GameObjectTasksDat> LoadGameObjectTasksDat()
    {
        return GameObjectTasksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GamepadButtonDat data.
    /// </summary>
    /// <returns>readonly collection of GamepadButtonDat.</returns>
    public ReadOnlyCollection<GamepadButtonDat> LoadGamepadButtonDat()
    {
        return GamepadButtonDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GamepadTypeDat data.
    /// </summary>
    /// <returns>readonly collection of GamepadTypeDat.</returns>
    public ReadOnlyCollection<GamepadTypeDat> LoadGamepadTypeDat()
    {
        return GamepadTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GameStatsDat data.
    /// </summary>
    /// <returns>readonly collection of GameStatsDat.</returns>
    public ReadOnlyCollection<GameStatsDat> LoadGameStatsDat()
    {
        return GameStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GemTagsDat data.
    /// </summary>
    /// <returns>readonly collection of GemTagsDat.</returns>
    public ReadOnlyCollection<GemTagsDat> LoadGemTagsDat()
    {
        return GemTagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GenericBuffAurasDat data.
    /// </summary>
    /// <returns>readonly collection of GenericBuffAurasDat.</returns>
    public ReadOnlyCollection<GenericBuffAurasDat> LoadGenericBuffAurasDat()
    {
        return GenericBuffAurasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GenericLeagueRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of GenericLeagueRewardTypesDat.</returns>
    public ReadOnlyCollection<GenericLeagueRewardTypesDat> LoadGenericLeagueRewardTypesDat()
    {
        return GenericLeagueRewardTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GenericLeagueRewardTypeVisualsDat data.
    /// </summary>
    /// <returns>readonly collection of GenericLeagueRewardTypeVisualsDat.</returns>
    public ReadOnlyCollection<GenericLeagueRewardTypeVisualsDat> LoadGenericLeagueRewardTypeVisualsDat()
    {
        return GenericLeagueRewardTypeVisualsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GeometryAttackDat data.
    /// </summary>
    /// <returns>readonly collection of GeometryAttackDat.</returns>
    public ReadOnlyCollection<GeometryAttackDat> LoadGeometryAttackDat()
    {
        return GeometryAttackDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GeometryChannelDat data.
    /// </summary>
    /// <returns>readonly collection of GeometryChannelDat.</returns>
    public ReadOnlyCollection<GeometryChannelDat> LoadGeometryChannelDat()
    {
        return GeometryChannelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GeometryProjectilesDat data.
    /// </summary>
    /// <returns>readonly collection of GeometryProjectilesDat.</returns>
    public ReadOnlyCollection<GeometryProjectilesDat> LoadGeometryProjectilesDat()
    {
        return GeometryProjectilesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GeometryTriggerDat data.
    /// </summary>
    /// <returns>readonly collection of GeometryTriggerDat.</returns>
    public ReadOnlyCollection<GeometryTriggerDat> LoadGeometryTriggerDat()
    {
        return GeometryTriggerDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GiftWrapArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of GiftWrapArtVariationsDat.</returns>
    public ReadOnlyCollection<GiftWrapArtVariationsDat> LoadGiftWrapArtVariationsDat()
    {
        return GiftWrapArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GlobalAudioConfigDat data.
    /// </summary>
    /// <returns>readonly collection of GlobalAudioConfigDat.</returns>
    public ReadOnlyCollection<GlobalAudioConfigDat> LoadGlobalAudioConfigDat()
    {
        return GlobalAudioConfigDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GrandmastersDat data.
    /// </summary>
    /// <returns>readonly collection of GrandmastersDat.</returns>
    public ReadOnlyCollection<GrandmastersDat> LoadGrandmastersDat()
    {
        return GrandmastersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GrantedEffectQualityStatsDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectQualityStatsDat.</returns>
    public ReadOnlyCollection<GrantedEffectQualityStatsDat> LoadGrantedEffectQualityStatsDat()
    {
        return GrantedEffectQualityStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GrantedEffectQualityTypesDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectQualityTypesDat.</returns>
    public ReadOnlyCollection<GrantedEffectQualityTypesDat> LoadGrantedEffectQualityTypesDat()
    {
        return GrantedEffectQualityTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GrantedEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectsDat.</returns>
    public ReadOnlyCollection<GrantedEffectsDat> LoadGrantedEffectsDat()
    {
        return GrantedEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GrantedEffectsPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectsPerLevelDat.</returns>
    public ReadOnlyCollection<GrantedEffectsPerLevelDat> LoadGrantedEffectsPerLevelDat()
    {
        return GrantedEffectsPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GrantedEffectStatSetsDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectStatSetsDat.</returns>
    public ReadOnlyCollection<GrantedEffectStatSetsDat> LoadGrantedEffectStatSetsDat()
    {
        return GrantedEffectStatSetsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GrantedEffectStatSetsPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectStatSetsPerLevelDat.</returns>
    public ReadOnlyCollection<GrantedEffectStatSetsPerLevelDat> LoadGrantedEffectStatSetsPerLevelDat()
    {
        return GrantedEffectStatSetsPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GroundEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of GroundEffectsDat.</returns>
    public ReadOnlyCollection<GroundEffectsDat> LoadGroundEffectsDat()
    {
        return GroundEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GroundEffectTypesDat data.
    /// </summary>
    /// <returns>readonly collection of GroundEffectTypesDat.</returns>
    public ReadOnlyCollection<GroundEffectTypesDat> LoadGroundEffectTypesDat()
    {
        return GroundEffectTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestStorageLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestStorageLayoutDat.</returns>
    public ReadOnlyCollection<HarvestStorageLayoutDat> LoadHarvestStorageLayoutDat()
    {
        return HarvestStorageLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistStorageLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of HeistStorageLayoutDat.</returns>
    public ReadOnlyCollection<HeistStorageLayoutDat> LoadHeistStorageLayoutDat()
    {
        return HeistStorageLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HideoutDoodadsDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutDoodadsDat.</returns>
    public ReadOnlyCollection<HideoutDoodadsDat> LoadHideoutDoodadsDat()
    {
        return HideoutDoodadsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HideoutDoodadCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutDoodadCategoryDat.</returns>
    public ReadOnlyCollection<HideoutDoodadCategoryDat> LoadHideoutDoodadCategoryDat()
    {
        return HideoutDoodadCategoryDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HideoutDoodadTagsDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutDoodadTagsDat.</returns>
    public ReadOnlyCollection<HideoutDoodadTagsDat> LoadHideoutDoodadTagsDat()
    {
        return HideoutDoodadTagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HideoutNPCsDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutNPCsDat.</returns>
    public ReadOnlyCollection<HideoutNPCsDat> LoadHideoutNPCsDat()
    {
        return HideoutNPCsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HideoutRarityDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutRarityDat.</returns>
    public ReadOnlyCollection<HideoutRarityDat> LoadHideoutRarityDat()
    {
        return HideoutRarityDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HideoutsDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutsDat.</returns>
    public ReadOnlyCollection<HideoutsDat> LoadHideoutsDat()
    {
        return HideoutsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ImpactSoundDataDat data.
    /// </summary>
    /// <returns>readonly collection of ImpactSoundDataDat.</returns>
    public ReadOnlyCollection<ImpactSoundDataDat> LoadImpactSoundDataDat()
    {
        return ImpactSoundDataDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IndexableSupportGemsDat data.
    /// </summary>
    /// <returns>readonly collection of IndexableSupportGemsDat.</returns>
    public ReadOnlyCollection<IndexableSupportGemsDat> LoadIndexableSupportGemsDat()
    {
        return IndexableSupportGemsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets InfluenceExaltsDat data.
    /// </summary>
    /// <returns>readonly collection of InfluenceExaltsDat.</returns>
    public ReadOnlyCollection<InfluenceExaltsDat> LoadInfluenceExaltsDat()
    {
        return InfluenceExaltsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets InfluenceTagsDat data.
    /// </summary>
    /// <returns>readonly collection of InfluenceTagsDat.</returns>
    public ReadOnlyCollection<InfluenceTagsDat> LoadInfluenceTagsDat()
    {
        return InfluenceTagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets InventoriesDat data.
    /// </summary>
    /// <returns>readonly collection of InventoriesDat.</returns>
    public ReadOnlyCollection<InventoriesDat> LoadInventoriesDat()
    {
        return InventoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemClassCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of ItemClassCategoriesDat.</returns>
    public ReadOnlyCollection<ItemClassCategoriesDat> LoadItemClassCategoriesDat()
    {
        return ItemClassCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemClassesDat data.
    /// </summary>
    /// <returns>readonly collection of ItemClassesDat.</returns>
    public ReadOnlyCollection<ItemClassesDat> LoadItemClassesDat()
    {
        return ItemClassesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemCostPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of ItemCostPerLevelDat.</returns>
    public ReadOnlyCollection<ItemCostPerLevelDat> LoadItemCostPerLevelDat()
    {
        return ItemCostPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemCostsDat data.
    /// </summary>
    /// <returns>readonly collection of ItemCostsDat.</returns>
    public ReadOnlyCollection<ItemCostsDat> LoadItemCostsDat()
    {
        return ItemCostsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemFrameTypeDat data.
    /// </summary>
    /// <returns>readonly collection of ItemFrameTypeDat.</returns>
    public ReadOnlyCollection<ItemFrameTypeDat> LoadItemFrameTypeDat()
    {
        return ItemFrameTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemExperienceTypesDat data.
    /// </summary>
    /// <returns>readonly collection of ItemExperienceTypesDat.</returns>
    public ReadOnlyCollection<ItemExperienceTypesDat> LoadItemExperienceTypesDat()
    {
        return ItemExperienceTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemExperiencePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of ItemExperiencePerLevelDat.</returns>
    public ReadOnlyCollection<ItemExperiencePerLevelDat> LoadItemExperiencePerLevelDat()
    {
        return ItemExperiencePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemisedVisualEffectDat data.
    /// </summary>
    /// <returns>readonly collection of ItemisedVisualEffectDat.</returns>
    public ReadOnlyCollection<ItemisedVisualEffectDat> LoadItemisedVisualEffectDat()
    {
        return ItemisedVisualEffectDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemNoteCodeDat data.
    /// </summary>
    /// <returns>readonly collection of ItemNoteCodeDat.</returns>
    public ReadOnlyCollection<ItemNoteCodeDat> LoadItemNoteCodeDat()
    {
        return ItemNoteCodeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemShopTypeDat data.
    /// </summary>
    /// <returns>readonly collection of ItemShopTypeDat.</returns>
    public ReadOnlyCollection<ItemShopTypeDat> LoadItemShopTypeDat()
    {
        return ItemShopTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemStancesDat data.
    /// </summary>
    /// <returns>readonly collection of ItemStancesDat.</returns>
    public ReadOnlyCollection<ItemStancesDat> LoadItemStancesDat()
    {
        return ItemStancesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemThemesDat data.
    /// </summary>
    /// <returns>readonly collection of ItemThemesDat.</returns>
    public ReadOnlyCollection<ItemThemesDat> LoadItemThemesDat()
    {
        return ItemThemesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemVisualEffectDat data.
    /// </summary>
    /// <returns>readonly collection of ItemVisualEffectDat.</returns>
    public ReadOnlyCollection<ItemVisualEffectDat> LoadItemVisualEffectDat()
    {
        return ItemVisualEffectDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemVisualHeldBodyModelDat data.
    /// </summary>
    /// <returns>readonly collection of ItemVisualHeldBodyModelDat.</returns>
    public ReadOnlyCollection<ItemVisualHeldBodyModelDat> LoadItemVisualHeldBodyModelDat()
    {
        return ItemVisualHeldBodyModelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemVisualIdentityDat data.
    /// </summary>
    /// <returns>readonly collection of ItemVisualIdentityDat.</returns>
    public ReadOnlyCollection<ItemVisualIdentityDat> LoadItemVisualIdentityDat()
    {
        return ItemVisualIdentityDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets JobAssassinationSpawnerGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of JobAssassinationSpawnerGroupsDat.</returns>
    public ReadOnlyCollection<JobAssassinationSpawnerGroupsDat> LoadJobAssassinationSpawnerGroupsDat()
    {
        return JobAssassinationSpawnerGroupsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets JobRaidBracketsDat data.
    /// </summary>
    /// <returns>readonly collection of JobRaidBracketsDat.</returns>
    public ReadOnlyCollection<JobRaidBracketsDat> LoadJobRaidBracketsDat()
    {
        return JobRaidBracketsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets KillstreakThresholdsDat data.
    /// </summary>
    /// <returns>readonly collection of KillstreakThresholdsDat.</returns>
    public ReadOnlyCollection<KillstreakThresholdsDat> LoadKillstreakThresholdsDat()
    {
        return KillstreakThresholdsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LeagueFlagDat data.
    /// </summary>
    /// <returns>readonly collection of LeagueFlagDat.</returns>
    public ReadOnlyCollection<LeagueFlagDat> LoadLeagueFlagDat()
    {
        return LeagueFlagDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LeagueInfoDat data.
    /// </summary>
    /// <returns>readonly collection of LeagueInfoDat.</returns>
    public ReadOnlyCollection<LeagueInfoDat> LoadLeagueInfoDat()
    {
        return LeagueInfoDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LeagueProgressQuestFlagsDat data.
    /// </summary>
    /// <returns>readonly collection of LeagueProgressQuestFlagsDat.</returns>
    public ReadOnlyCollection<LeagueProgressQuestFlagsDat> LoadLeagueProgressQuestFlagsDat()
    {
        return LeagueProgressQuestFlagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LeagueStaticRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of LeagueStaticRewardsDat.</returns>
    public ReadOnlyCollection<LeagueStaticRewardsDat> LoadLeagueStaticRewardsDat()
    {
        return LeagueStaticRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LevelRelativePlayerScalingDat data.
    /// </summary>
    /// <returns>readonly collection of LevelRelativePlayerScalingDat.</returns>
    public ReadOnlyCollection<LevelRelativePlayerScalingDat> LoadLevelRelativePlayerScalingDat()
    {
        return LevelRelativePlayerScalingDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MagicMonsterLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of MagicMonsterLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<MagicMonsterLifeScalingPerLevelDat> LoadMagicMonsterLifeScalingPerLevelDat()
    {
        return MagicMonsterLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapCompletionAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of MapCompletionAchievementsDat.</returns>
    public ReadOnlyCollection<MapCompletionAchievementsDat> LoadMapCompletionAchievementsDat()
    {
        return MapCompletionAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapConnectionsDat data.
    /// </summary>
    /// <returns>readonly collection of MapConnectionsDat.</returns>
    public ReadOnlyCollection<MapConnectionsDat> LoadMapConnectionsDat()
    {
        return MapConnectionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapCreationInformationDat data.
    /// </summary>
    /// <returns>readonly collection of MapCreationInformationDat.</returns>
    public ReadOnlyCollection<MapCreationInformationDat> LoadMapCreationInformationDat()
    {
        return MapCreationInformationDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapDeviceRecipesDat data.
    /// </summary>
    /// <returns>readonly collection of MapDeviceRecipesDat.</returns>
    public ReadOnlyCollection<MapDeviceRecipesDat> LoadMapDeviceRecipesDat()
    {
        return MapDeviceRecipesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapDevicesDat data.
    /// </summary>
    /// <returns>readonly collection of MapDevicesDat.</returns>
    public ReadOnlyCollection<MapDevicesDat> LoadMapDevicesDat()
    {
        return MapDevicesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapFragmentModsDat data.
    /// </summary>
    /// <returns>readonly collection of MapFragmentModsDat.</returns>
    public ReadOnlyCollection<MapFragmentModsDat> LoadMapFragmentModsDat()
    {
        return MapFragmentModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapInhabitantsDat data.
    /// </summary>
    /// <returns>readonly collection of MapInhabitantsDat.</returns>
    public ReadOnlyCollection<MapInhabitantsDat> LoadMapInhabitantsDat()
    {
        return MapInhabitantsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapPinsDat data.
    /// </summary>
    /// <returns>readonly collection of MapPinsDat.</returns>
    public ReadOnlyCollection<MapPinsDat> LoadMapPinsDat()
    {
        return MapPinsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapPurchaseCostsDat data.
    /// </summary>
    /// <returns>readonly collection of MapPurchaseCostsDat.</returns>
    public ReadOnlyCollection<MapPurchaseCostsDat> LoadMapPurchaseCostsDat()
    {
        return MapPurchaseCostsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapsDat data.
    /// </summary>
    /// <returns>readonly collection of MapsDat.</returns>
    public ReadOnlyCollection<MapsDat> LoadMapsDat()
    {
        return MapsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapSeriesDat data.
    /// </summary>
    /// <returns>readonly collection of MapSeriesDat.</returns>
    public ReadOnlyCollection<MapSeriesDat> LoadMapSeriesDat()
    {
        return MapSeriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapSeriesTiersDat data.
    /// </summary>
    /// <returns>readonly collection of MapSeriesTiersDat.</returns>
    public ReadOnlyCollection<MapSeriesTiersDat> LoadMapSeriesTiersDat()
    {
        return MapSeriesTiersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapStashSpecialTypeEntriesDat data.
    /// </summary>
    /// <returns>readonly collection of MapStashSpecialTypeEntriesDat.</returns>
    public ReadOnlyCollection<MapStashSpecialTypeEntriesDat> LoadMapStashSpecialTypeEntriesDat()
    {
        return MapStashSpecialTypeEntriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapStashUniqueMapInfoDat data.
    /// </summary>
    /// <returns>readonly collection of MapStashUniqueMapInfoDat.</returns>
    public ReadOnlyCollection<MapStashUniqueMapInfoDat> LoadMapStashUniqueMapInfoDat()
    {
        return MapStashUniqueMapInfoDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapStatConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of MapStatConditionsDat.</returns>
    public ReadOnlyCollection<MapStatConditionsDat> LoadMapStatConditionsDat()
    {
        return MapStatConditionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapTierAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of MapTierAchievementsDat.</returns>
    public ReadOnlyCollection<MapTierAchievementsDat> LoadMapTierAchievementsDat()
    {
        return MapTierAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapTiersDat data.
    /// </summary>
    /// <returns>readonly collection of MapTiersDat.</returns>
    public ReadOnlyCollection<MapTiersDat> LoadMapTiersDat()
    {
        return MapTiersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MasterHideoutLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of MasterHideoutLevelsDat.</returns>
    public ReadOnlyCollection<MasterHideoutLevelsDat> LoadMasterHideoutLevelsDat()
    {
        return MasterHideoutLevelsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MeleeDat data.
    /// </summary>
    /// <returns>readonly collection of MeleeDat.</returns>
    public ReadOnlyCollection<MeleeDat> LoadMeleeDat()
    {
        return MeleeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MeleeTrailsDat data.
    /// </summary>
    /// <returns>readonly collection of MeleeTrailsDat.</returns>
    public ReadOnlyCollection<MeleeTrailsDat> LoadMeleeTrailsDat()
    {
        return MeleeTrailsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MetamorphosisStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisStashTabLayoutDat.</returns>
    public ReadOnlyCollection<MetamorphosisStashTabLayoutDat> LoadMetamorphosisStashTabLayoutDat()
    {
        return MetamorphosisStashTabLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicroMigrationDataDat data.
    /// </summary>
    /// <returns>readonly collection of MicroMigrationDataDat.</returns>
    public ReadOnlyCollection<MicroMigrationDataDat> LoadMicroMigrationDataDat()
    {
        return MicroMigrationDataDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionCategoryDat.</returns>
    public ReadOnlyCollection<MicrotransactionCategoryDat> LoadMicrotransactionCategoryDat()
    {
        return MicrotransactionCategoryDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionCharacterPortraitVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionCharacterPortraitVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionCharacterPortraitVariationsDat> LoadMicrotransactionCharacterPortraitVariationsDat()
    {
        return MicrotransactionCharacterPortraitVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionCombineFormulaDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionCombineFormulaDat.</returns>
    public ReadOnlyCollection<MicrotransactionCombineFormulaDat> LoadMicrotransactionCombineFormulaDat()
    {
        return MicrotransactionCombineFormulaDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionCursorVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionCursorVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionCursorVariationsDat> LoadMicrotransactionCursorVariationsDat()
    {
        return MicrotransactionCursorVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionFireworksVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionFireworksVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionFireworksVariationsDat> LoadMicrotransactionFireworksVariationsDat()
    {
        return MicrotransactionFireworksVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionGemCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionGemCategoryDat.</returns>
    public ReadOnlyCollection<MicrotransactionGemCategoryDat> LoadMicrotransactionGemCategoryDat()
    {
        return MicrotransactionGemCategoryDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionPeriodicCharacterEffectVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionPeriodicCharacterEffectVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionPeriodicCharacterEffectVariationsDat> LoadMicrotransactionPeriodicCharacterEffectVariationsDat()
    {
        return MicrotransactionPeriodicCharacterEffectVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionPortalVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionPortalVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionPortalVariationsDat> LoadMicrotransactionPortalVariationsDat()
    {
        return MicrotransactionPortalVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionRarityDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionRarityDisplayDat.</returns>
    public ReadOnlyCollection<MicrotransactionRarityDisplayDat> LoadMicrotransactionRarityDisplayDat()
    {
        return MicrotransactionRarityDisplayDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionRecycleOutcomesDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionRecycleOutcomesDat.</returns>
    public ReadOnlyCollection<MicrotransactionRecycleOutcomesDat> LoadMicrotransactionRecycleOutcomesDat()
    {
        return MicrotransactionRecycleOutcomesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionRecycleSalvageValuesDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionRecycleSalvageValuesDat.</returns>
    public ReadOnlyCollection<MicrotransactionRecycleSalvageValuesDat> LoadMicrotransactionRecycleSalvageValuesDat()
    {
        return MicrotransactionRecycleSalvageValuesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionSlotDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionSlotDat.</returns>
    public ReadOnlyCollection<MicrotransactionSlotDat> LoadMicrotransactionSlotDat()
    {
        return MicrotransactionSlotDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionSocialFrameVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionSocialFrameVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionSocialFrameVariationsDat> LoadMicrotransactionSocialFrameVariationsDat()
    {
        return MicrotransactionSocialFrameVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MinimapIconsDat data.
    /// </summary>
    /// <returns>readonly collection of MinimapIconsDat.</returns>
    public ReadOnlyCollection<MinimapIconsDat> LoadMinimapIconsDat()
    {
        return MinimapIconsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiniQuestStatesDat data.
    /// </summary>
    /// <returns>readonly collection of MiniQuestStatesDat.</returns>
    public ReadOnlyCollection<MiniQuestStatesDat> LoadMiniQuestStatesDat()
    {
        return MiniQuestStatesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiscAnimatedDat data.
    /// </summary>
    /// <returns>readonly collection of MiscAnimatedDat.</returns>
    public ReadOnlyCollection<MiscAnimatedDat> LoadMiscAnimatedDat()
    {
        return MiscAnimatedDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiscAnimatedArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscAnimatedArtVariationsDat.</returns>
    public ReadOnlyCollection<MiscAnimatedArtVariationsDat> LoadMiscAnimatedArtVariationsDat()
    {
        return MiscAnimatedArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiscBeamsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscBeamsDat.</returns>
    public ReadOnlyCollection<MiscBeamsDat> LoadMiscBeamsDat()
    {
        return MiscBeamsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiscBeamsArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscBeamsArtVariationsDat.</returns>
    public ReadOnlyCollection<MiscBeamsArtVariationsDat> LoadMiscBeamsArtVariationsDat()
    {
        return MiscBeamsArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiscEffectPacksDat data.
    /// </summary>
    /// <returns>readonly collection of MiscEffectPacksDat.</returns>
    public ReadOnlyCollection<MiscEffectPacksDat> LoadMiscEffectPacksDat()
    {
        return MiscEffectPacksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiscEffectPacksArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscEffectPacksArtVariationsDat.</returns>
    public ReadOnlyCollection<MiscEffectPacksArtVariationsDat> LoadMiscEffectPacksArtVariationsDat()
    {
        return MiscEffectPacksArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiscObjectsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscObjectsDat.</returns>
    public ReadOnlyCollection<MiscObjectsDat> LoadMiscObjectsDat()
    {
        return MiscObjectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiscObjectsArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscObjectsArtVariationsDat.</returns>
    public ReadOnlyCollection<MiscObjectsArtVariationsDat> LoadMiscObjectsArtVariationsDat()
    {
        return MiscObjectsArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MissionFavourPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of MissionFavourPerLevelDat.</returns>
    public ReadOnlyCollection<MissionFavourPerLevelDat> LoadMissionFavourPerLevelDat()
    {
        return MissionFavourPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MissionTimerTypesDat data.
    /// </summary>
    /// <returns>readonly collection of MissionTimerTypesDat.</returns>
    public ReadOnlyCollection<MissionTimerTypesDat> LoadMissionTimerTypesDat()
    {
        return MissionTimerTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MissionTransitionTilesDat data.
    /// </summary>
    /// <returns>readonly collection of MissionTransitionTilesDat.</returns>
    public ReadOnlyCollection<MissionTransitionTilesDat> LoadMissionTransitionTilesDat()
    {
        return MissionTransitionTilesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ModEffectStatsDat data.
    /// </summary>
    /// <returns>readonly collection of ModEffectStatsDat.</returns>
    public ReadOnlyCollection<ModEffectStatsDat> LoadModEffectStatsDat()
    {
        return ModEffectStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ModEquivalenciesDat data.
    /// </summary>
    /// <returns>readonly collection of ModEquivalenciesDat.</returns>
    public ReadOnlyCollection<ModEquivalenciesDat> LoadModEquivalenciesDat()
    {
        return ModEquivalenciesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ModFamilyDat data.
    /// </summary>
    /// <returns>readonly collection of ModFamilyDat.</returns>
    public ReadOnlyCollection<ModFamilyDat> LoadModFamilyDat()
    {
        return ModFamilyDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ModsDat data.
    /// </summary>
    /// <returns>readonly collection of ModsDat.</returns>
    public ReadOnlyCollection<ModsDat> LoadModsDat()
    {
        return ModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ModSellPriceTypesDat data.
    /// </summary>
    /// <returns>readonly collection of ModSellPriceTypesDat.</returns>
    public ReadOnlyCollection<ModSellPriceTypesDat> LoadModSellPriceTypesDat()
    {
        return ModSellPriceTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ModTypeDat data.
    /// </summary>
    /// <returns>readonly collection of ModTypeDat.</returns>
    public ReadOnlyCollection<ModTypeDat> LoadModTypeDat()
    {
        return ModTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterArmoursDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterArmoursDat.</returns>
    public ReadOnlyCollection<MonsterArmoursDat> LoadMonsterArmoursDat()
    {
        return MonsterArmoursDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterBonusesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterBonusesDat.</returns>
    public ReadOnlyCollection<MonsterBonusesDat> LoadMonsterBonusesDat()
    {
        return MonsterBonusesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterConditionalEffectPacksDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterConditionalEffectPacksDat.</returns>
    public ReadOnlyCollection<MonsterConditionalEffectPacksDat> LoadMonsterConditionalEffectPacksDat()
    {
        return MonsterConditionalEffectPacksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterConditionsDat.</returns>
    public ReadOnlyCollection<MonsterConditionsDat> LoadMonsterConditionsDat()
    {
        return MonsterConditionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterDeathAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterDeathAchievementsDat.</returns>
    public ReadOnlyCollection<MonsterDeathAchievementsDat> LoadMonsterDeathAchievementsDat()
    {
        return MonsterDeathAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterDeathConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterDeathConditionsDat.</returns>
    public ReadOnlyCollection<MonsterDeathConditionsDat> LoadMonsterDeathConditionsDat()
    {
        return MonsterDeathConditionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterGroupEntriesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterGroupEntriesDat.</returns>
    public ReadOnlyCollection<MonsterGroupEntriesDat> LoadMonsterGroupEntriesDat()
    {
        return MonsterGroupEntriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterHeightBracketsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterHeightBracketsDat.</returns>
    public ReadOnlyCollection<MonsterHeightBracketsDat> LoadMonsterHeightBracketsDat()
    {
        return MonsterHeightBracketsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterHeightsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterHeightsDat.</returns>
    public ReadOnlyCollection<MonsterHeightsDat> LoadMonsterHeightsDat()
    {
        return MonsterHeightsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterMapBossDifficultyDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterMapBossDifficultyDat.</returns>
    public ReadOnlyCollection<MonsterMapBossDifficultyDat> LoadMonsterMapBossDifficultyDat()
    {
        return MonsterMapBossDifficultyDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterMapDifficultyDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterMapDifficultyDat.</returns>
    public ReadOnlyCollection<MonsterMapDifficultyDat> LoadMonsterMapDifficultyDat()
    {
        return MonsterMapDifficultyDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterMortarDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterMortarDat.</returns>
    public ReadOnlyCollection<MonsterMortarDat> LoadMonsterMortarDat()
    {
        return MonsterMortarDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterPackCountsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterPackCountsDat.</returns>
    public ReadOnlyCollection<MonsterPackCountsDat> LoadMonsterPackCountsDat()
    {
        return MonsterPackCountsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterPackEntriesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterPackEntriesDat.</returns>
    public ReadOnlyCollection<MonsterPackEntriesDat> LoadMonsterPackEntriesDat()
    {
        return MonsterPackEntriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterPacksDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterPacksDat.</returns>
    public ReadOnlyCollection<MonsterPacksDat> LoadMonsterPacksDat()
    {
        return MonsterPacksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterProjectileAttackDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterProjectileAttackDat.</returns>
    public ReadOnlyCollection<MonsterProjectileAttackDat> LoadMonsterProjectileAttackDat()
    {
        return MonsterProjectileAttackDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterProjectileSpellDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterProjectileSpellDat.</returns>
    public ReadOnlyCollection<MonsterProjectileSpellDat> LoadMonsterProjectileSpellDat()
    {
        return MonsterProjectileSpellDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterResistancesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterResistancesDat.</returns>
    public ReadOnlyCollection<MonsterResistancesDat> LoadMonsterResistancesDat()
    {
        return MonsterResistancesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterSegmentsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterSegmentsDat.</returns>
    public ReadOnlyCollection<MonsterSegmentsDat> LoadMonsterSegmentsDat()
    {
        return MonsterSegmentsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterSpawnerGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterSpawnerGroupsDat.</returns>
    public ReadOnlyCollection<MonsterSpawnerGroupsDat> LoadMonsterSpawnerGroupsDat()
    {
        return MonsterSpawnerGroupsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterSpawnerGroupsPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterSpawnerGroupsPerLevelDat.</returns>
    public ReadOnlyCollection<MonsterSpawnerGroupsPerLevelDat> LoadMonsterSpawnerGroupsPerLevelDat()
    {
        return MonsterSpawnerGroupsPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterSpawnerOverridesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterSpawnerOverridesDat.</returns>
    public ReadOnlyCollection<MonsterSpawnerOverridesDat> LoadMonsterSpawnerOverridesDat()
    {
        return MonsterSpawnerOverridesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterTypesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterTypesDat.</returns>
    public ReadOnlyCollection<MonsterTypesDat> LoadMonsterTypesDat()
    {
        return MonsterTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterVarietiesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterVarietiesDat.</returns>
    public ReadOnlyCollection<MonsterVarietiesDat> LoadMonsterVarietiesDat()
    {
        return MonsterVarietiesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterVarietiesArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterVarietiesArtVariationsDat.</returns>
    public ReadOnlyCollection<MonsterVarietiesArtVariationsDat> LoadMonsterVarietiesArtVariationsDat()
    {
        return MonsterVarietiesArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MouseCursorSizeSettingsDat data.
    /// </summary>
    /// <returns>readonly collection of MouseCursorSizeSettingsDat.</returns>
    public ReadOnlyCollection<MouseCursorSizeSettingsDat> LoadMouseCursorSizeSettingsDat()
    {
        return MouseCursorSizeSettingsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MoveDaemonDat data.
    /// </summary>
    /// <returns>readonly collection of MoveDaemonDat.</returns>
    public ReadOnlyCollection<MoveDaemonDat> LoadMoveDaemonDat()
    {
        return MoveDaemonDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MTXSetBonusDat data.
    /// </summary>
    /// <returns>readonly collection of MTXSetBonusDat.</returns>
    public ReadOnlyCollection<MTXSetBonusDat> LoadMTXSetBonusDat()
    {
        return MTXSetBonusDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MultiPartAchievementAreasDat data.
    /// </summary>
    /// <returns>readonly collection of MultiPartAchievementAreasDat.</returns>
    public ReadOnlyCollection<MultiPartAchievementAreasDat> LoadMultiPartAchievementAreasDat()
    {
        return MultiPartAchievementAreasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MultiPartAchievementConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of MultiPartAchievementConditionsDat.</returns>
    public ReadOnlyCollection<MultiPartAchievementConditionsDat> LoadMultiPartAchievementConditionsDat()
    {
        return MultiPartAchievementConditionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MultiPartAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of MultiPartAchievementsDat.</returns>
    public ReadOnlyCollection<MultiPartAchievementsDat> LoadMultiPartAchievementsDat()
    {
        return MultiPartAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MusicDat data.
    /// </summary>
    /// <returns>readonly collection of MusicDat.</returns>
    public ReadOnlyCollection<MusicDat> LoadMusicDat()
    {
        return MusicDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MusicCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of MusicCategoriesDat.</returns>
    public ReadOnlyCollection<MusicCategoriesDat> LoadMusicCategoriesDat()
    {
        return MusicCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MysteryBoxesDat data.
    /// </summary>
    /// <returns>readonly collection of MysteryBoxesDat.</returns>
    public ReadOnlyCollection<MysteryBoxesDat> LoadMysteryBoxesDat()
    {
        return MysteryBoxesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NearbyMonsterConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of NearbyMonsterConditionsDat.</returns>
    public ReadOnlyCollection<NearbyMonsterConditionsDat> LoadNearbyMonsterConditionsDat()
    {
        return NearbyMonsterConditionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NetTiersDat data.
    /// </summary>
    /// <returns>readonly collection of NetTiersDat.</returns>
    public ReadOnlyCollection<NetTiersDat> LoadNetTiersDat()
    {
        return NetTiersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NotificationsDat data.
    /// </summary>
    /// <returns>readonly collection of NotificationsDat.</returns>
    public ReadOnlyCollection<NotificationsDat> LoadNotificationsDat()
    {
        return NotificationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCAudioDat data.
    /// </summary>
    /// <returns>readonly collection of NPCAudioDat.</returns>
    public ReadOnlyCollection<NPCAudioDat> LoadNPCAudioDat()
    {
        return NPCAudioDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCConversationsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCConversationsDat.</returns>
    public ReadOnlyCollection<NPCConversationsDat> LoadNPCConversationsDat()
    {
        return NPCConversationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCDialogueStylesDat data.
    /// </summary>
    /// <returns>readonly collection of NPCDialogueStylesDat.</returns>
    public ReadOnlyCollection<NPCDialogueStylesDat> LoadNPCDialogueStylesDat()
    {
        return NPCDialogueStylesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCFollowerVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCFollowerVariationsDat.</returns>
    public ReadOnlyCollection<NPCFollowerVariationsDat> LoadNPCFollowerVariationsDat()
    {
        return NPCFollowerVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCMasterDat data.
    /// </summary>
    /// <returns>readonly collection of NPCMasterDat.</returns>
    public ReadOnlyCollection<NPCMasterDat> LoadNPCMasterDat()
    {
        return NPCMasterDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCPortraitsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCPortraitsDat.</returns>
    public ReadOnlyCollection<NPCPortraitsDat> LoadNPCPortraitsDat()
    {
        return NPCPortraitsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCsDat.</returns>
    public ReadOnlyCollection<NPCsDat> LoadNPCsDat()
    {
        return NPCsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCShopDat data.
    /// </summary>
    /// <returns>readonly collection of NPCShopDat.</returns>
    public ReadOnlyCollection<NPCShopDat> LoadNPCShopDat()
    {
        return NPCShopDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCShopsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCShopsDat.</returns>
    public ReadOnlyCollection<NPCShopsDat> LoadNPCShopsDat()
    {
        return NPCShopsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCTalkDat data.
    /// </summary>
    /// <returns>readonly collection of NPCTalkDat.</returns>
    public ReadOnlyCollection<NPCTalkDat> LoadNPCTalkDat()
    {
        return NPCTalkDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCTalkCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of NPCTalkCategoryDat.</returns>
    public ReadOnlyCollection<NPCTalkCategoryDat> LoadNPCTalkCategoryDat()
    {
        return NPCTalkCategoryDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCTalkConsoleQuickActionsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCTalkConsoleQuickActionsDat.</returns>
    public ReadOnlyCollection<NPCTalkConsoleQuickActionsDat> LoadNPCTalkConsoleQuickActionsDat()
    {
        return NPCTalkConsoleQuickActionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCTextAudioDat data.
    /// </summary>
    /// <returns>readonly collection of NPCTextAudioDat.</returns>
    public ReadOnlyCollection<NPCTextAudioDat> LoadNPCTextAudioDat()
    {
        return NPCTextAudioDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets OnKillAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of OnKillAchievementsDat.</returns>
    public ReadOnlyCollection<OnKillAchievementsDat> LoadOnKillAchievementsDat()
    {
        return OnKillAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PackFormationDat data.
    /// </summary>
    /// <returns>readonly collection of PackFormationDat.</returns>
    public ReadOnlyCollection<PackFormationDat> LoadPackFormationDat()
    {
        return PackFormationDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveJewelRadiiDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveJewelRadiiDat.</returns>
    public ReadOnlyCollection<PassiveJewelRadiiDat> LoadPassiveJewelRadiiDat()
    {
        return PassiveJewelRadiiDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveJewelSlotsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveJewelSlotsDat.</returns>
    public ReadOnlyCollection<PassiveJewelSlotsDat> LoadPassiveJewelSlotsDat()
    {
        return PassiveJewelSlotsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillFilterCatagoriesDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillFilterCatagoriesDat.</returns>
    public ReadOnlyCollection<PassiveSkillFilterCatagoriesDat> LoadPassiveSkillFilterCatagoriesDat()
    {
        return PassiveSkillFilterCatagoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillFilterOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillFilterOptionsDat.</returns>
    public ReadOnlyCollection<PassiveSkillFilterOptionsDat> LoadPassiveSkillFilterOptionsDat()
    {
        return PassiveSkillFilterOptionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillMasteryGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillMasteryGroupsDat.</returns>
    public ReadOnlyCollection<PassiveSkillMasteryGroupsDat> LoadPassiveSkillMasteryGroupsDat()
    {
        return PassiveSkillMasteryGroupsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillMasteryEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillMasteryEffectsDat.</returns>
    public ReadOnlyCollection<PassiveSkillMasteryEffectsDat> LoadPassiveSkillMasteryEffectsDat()
    {
        return PassiveSkillMasteryEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillsDat.</returns>
    public ReadOnlyCollection<PassiveSkillsDat> LoadPassiveSkillsDat()
    {
        return PassiveSkillsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillStatCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillStatCategoriesDat.</returns>
    public ReadOnlyCollection<PassiveSkillStatCategoriesDat> LoadPassiveSkillStatCategoriesDat()
    {
        return PassiveSkillStatCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillTreesDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillTreesDat.</returns>
    public ReadOnlyCollection<PassiveSkillTreesDat> LoadPassiveSkillTreesDat()
    {
        return PassiveSkillTreesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillTreeTutorialDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillTreeTutorialDat.</returns>
    public ReadOnlyCollection<PassiveSkillTreeTutorialDat> LoadPassiveSkillTreeTutorialDat()
    {
        return PassiveSkillTreeTutorialDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillTreeUIArtDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillTreeUIArtDat.</returns>
    public ReadOnlyCollection<PassiveSkillTreeUIArtDat> LoadPassiveSkillTreeUIArtDat()
    {
        return PassiveSkillTreeUIArtDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveTreeExpansionJewelsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveTreeExpansionJewelsDat.</returns>
    public ReadOnlyCollection<PassiveTreeExpansionJewelsDat> LoadPassiveTreeExpansionJewelsDat()
    {
        return PassiveTreeExpansionJewelsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveTreeExpansionJewelSizesDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveTreeExpansionJewelSizesDat.</returns>
    public ReadOnlyCollection<PassiveTreeExpansionJewelSizesDat> LoadPassiveTreeExpansionJewelSizesDat()
    {
        return PassiveTreeExpansionJewelSizesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveTreeExpansionSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveTreeExpansionSkillsDat.</returns>
    public ReadOnlyCollection<PassiveTreeExpansionSkillsDat> LoadPassiveTreeExpansionSkillsDat()
    {
        return PassiveTreeExpansionSkillsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveTreeExpansionSpecialSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveTreeExpansionSpecialSkillsDat.</returns>
    public ReadOnlyCollection<PassiveTreeExpansionSpecialSkillsDat> LoadPassiveTreeExpansionSpecialSkillsDat()
    {
        return PassiveTreeExpansionSpecialSkillsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PCBangRewardMicrosDat data.
    /// </summary>
    /// <returns>readonly collection of PCBangRewardMicrosDat.</returns>
    public ReadOnlyCollection<PCBangRewardMicrosDat> LoadPCBangRewardMicrosDat()
    {
        return PCBangRewardMicrosDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PetDat data.
    /// </summary>
    /// <returns>readonly collection of PetDat.</returns>
    public ReadOnlyCollection<PetDat> LoadPetDat()
    {
        return PetDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PlayerConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of PlayerConditionsDat.</returns>
    public ReadOnlyCollection<PlayerConditionsDat> LoadPlayerConditionsDat()
    {
        return PlayerConditionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PlayerTradeWhisperFormatsDat data.
    /// </summary>
    /// <returns>readonly collection of PlayerTradeWhisperFormatsDat.</returns>
    public ReadOnlyCollection<PlayerTradeWhisperFormatsDat> LoadPlayerTradeWhisperFormatsDat()
    {
        return PlayerTradeWhisperFormatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PreloadGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of PreloadGroupsDat.</returns>
    public ReadOnlyCollection<PreloadGroupsDat> LoadPreloadGroupsDat()
    {
        return PreloadGroupsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ProjectilesDat data.
    /// </summary>
    /// <returns>readonly collection of ProjectilesDat.</returns>
    public ReadOnlyCollection<ProjectilesDat> LoadProjectilesDat()
    {
        return ProjectilesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ProjectilesArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of ProjectilesArtVariationsDat.</returns>
    public ReadOnlyCollection<ProjectilesArtVariationsDat> LoadProjectilesArtVariationsDat()
    {
        return ProjectilesArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ProjectileVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of ProjectileVariationsDat.</returns>
    public ReadOnlyCollection<ProjectileVariationsDat> LoadProjectileVariationsDat()
    {
        return ProjectileVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PVPTypesDat data.
    /// </summary>
    /// <returns>readonly collection of PVPTypesDat.</returns>
    public ReadOnlyCollection<PVPTypesDat> LoadPVPTypesDat()
    {
        return PVPTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestDat data.
    /// </summary>
    /// <returns>readonly collection of QuestDat.</returns>
    public ReadOnlyCollection<QuestDat> LoadQuestDat()
    {
        return QuestDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of QuestAchievementsDat.</returns>
    public ReadOnlyCollection<QuestAchievementsDat> LoadQuestAchievementsDat()
    {
        return QuestAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestFlagsDat data.
    /// </summary>
    /// <returns>readonly collection of QuestFlagsDat.</returns>
    public ReadOnlyCollection<QuestFlagsDat> LoadQuestFlagsDat()
    {
        return QuestFlagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestItemsDat data.
    /// </summary>
    /// <returns>readonly collection of QuestItemsDat.</returns>
    public ReadOnlyCollection<QuestItemsDat> LoadQuestItemsDat()
    {
        return QuestItemsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestRewardOffersDat data.
    /// </summary>
    /// <returns>readonly collection of QuestRewardOffersDat.</returns>
    public ReadOnlyCollection<QuestRewardOffersDat> LoadQuestRewardOffersDat()
    {
        return QuestRewardOffersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of QuestRewardsDat.</returns>
    public ReadOnlyCollection<QuestRewardsDat> LoadQuestRewardsDat()
    {
        return QuestRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestStatesDat data.
    /// </summary>
    /// <returns>readonly collection of QuestStatesDat.</returns>
    public ReadOnlyCollection<QuestStatesDat> LoadQuestStatesDat()
    {
        return QuestStatesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestStaticRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of QuestStaticRewardsDat.</returns>
    public ReadOnlyCollection<QuestStaticRewardsDat> LoadQuestStaticRewardsDat()
    {
        return QuestStaticRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestTrackerGroupDat data.
    /// </summary>
    /// <returns>readonly collection of QuestTrackerGroupDat.</returns>
    public ReadOnlyCollection<QuestTrackerGroupDat> LoadQuestTrackerGroupDat()
    {
        return QuestTrackerGroupDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestTypeDat data.
    /// </summary>
    /// <returns>readonly collection of QuestTypeDat.</returns>
    public ReadOnlyCollection<QuestTypeDat> LoadQuestTypeDat()
    {
        return QuestTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RacesDat data.
    /// </summary>
    /// <returns>readonly collection of RacesDat.</returns>
    public ReadOnlyCollection<RacesDat> LoadRacesDat()
    {
        return RacesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RaceTimesDat data.
    /// </summary>
    /// <returns>readonly collection of RaceTimesDat.</returns>
    public ReadOnlyCollection<RaceTimesDat> LoadRaceTimesDat()
    {
        return RaceTimesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RareMonsterLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of RareMonsterLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<RareMonsterLifeScalingPerLevelDat> LoadRareMonsterLifeScalingPerLevelDat()
    {
        return RareMonsterLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RarityDat data.
    /// </summary>
    /// <returns>readonly collection of RarityDat.</returns>
    public ReadOnlyCollection<RarityDat> LoadRarityDat()
    {
        return RarityDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RealmsDat data.
    /// </summary>
    /// <returns>readonly collection of RealmsDat.</returns>
    public ReadOnlyCollection<RealmsDat> LoadRealmsDat()
    {
        return RealmsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RecipeUnlockDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of RecipeUnlockDisplayDat.</returns>
    public ReadOnlyCollection<RecipeUnlockDisplayDat> LoadRecipeUnlockDisplayDat()
    {
        return RecipeUnlockDisplayDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RecipeUnlockObjectsDat data.
    /// </summary>
    /// <returns>readonly collection of RecipeUnlockObjectsDat.</returns>
    public ReadOnlyCollection<RecipeUnlockObjectsDat> LoadRecipeUnlockObjectsDat()
    {
        return RecipeUnlockObjectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ReminderTextDat data.
    /// </summary>
    /// <returns>readonly collection of ReminderTextDat.</returns>
    public ReadOnlyCollection<ReminderTextDat> LoadReminderTextDat()
    {
        return ReminderTextDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RulesetsDat data.
    /// </summary>
    /// <returns>readonly collection of RulesetsDat.</returns>
    public ReadOnlyCollection<RulesetsDat> LoadRulesetsDat()
    {
        return RulesetsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RunicCirclesDat data.
    /// </summary>
    /// <returns>readonly collection of RunicCirclesDat.</returns>
    public ReadOnlyCollection<RunicCirclesDat> LoadRunicCirclesDat()
    {
        return RunicCirclesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SalvageBoxesDat data.
    /// </summary>
    /// <returns>readonly collection of SalvageBoxesDat.</returns>
    public ReadOnlyCollection<SalvageBoxesDat> LoadSalvageBoxesDat()
    {
        return SalvageBoxesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SessionQuestFlagsDat data.
    /// </summary>
    /// <returns>readonly collection of SessionQuestFlagsDat.</returns>
    public ReadOnlyCollection<SessionQuestFlagsDat> LoadSessionQuestFlagsDat()
    {
        return SessionQuestFlagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShieldTypesDat data.
    /// </summary>
    /// <returns>readonly collection of ShieldTypesDat.</returns>
    public ReadOnlyCollection<ShieldTypesDat> LoadShieldTypesDat()
    {
        return ShieldTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShopCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of ShopCategoryDat.</returns>
    public ReadOnlyCollection<ShopCategoryDat> LoadShopCategoryDat()
    {
        return ShopCategoryDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShopCountryDat data.
    /// </summary>
    /// <returns>readonly collection of ShopCountryDat.</returns>
    public ReadOnlyCollection<ShopCountryDat> LoadShopCountryDat()
    {
        return ShopCountryDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShopCurrencyDat data.
    /// </summary>
    /// <returns>readonly collection of ShopCurrencyDat.</returns>
    public ReadOnlyCollection<ShopCurrencyDat> LoadShopCurrencyDat()
    {
        return ShopCurrencyDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShopPaymentPackageDat data.
    /// </summary>
    /// <returns>readonly collection of ShopPaymentPackageDat.</returns>
    public ReadOnlyCollection<ShopPaymentPackageDat> LoadShopPaymentPackageDat()
    {
        return ShopPaymentPackageDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShopPaymentPackagePriceDat data.
    /// </summary>
    /// <returns>readonly collection of ShopPaymentPackagePriceDat.</returns>
    public ReadOnlyCollection<ShopPaymentPackagePriceDat> LoadShopPaymentPackagePriceDat()
    {
        return ShopPaymentPackagePriceDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShopRegionDat data.
    /// </summary>
    /// <returns>readonly collection of ShopRegionDat.</returns>
    public ReadOnlyCollection<ShopRegionDat> LoadShopRegionDat()
    {
        return ShopRegionDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShopTagDat data.
    /// </summary>
    /// <returns>readonly collection of ShopTagDat.</returns>
    public ReadOnlyCollection<ShopTagDat> LoadShopTagDat()
    {
        return ShopTagDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShopTokenDat data.
    /// </summary>
    /// <returns>readonly collection of ShopTokenDat.</returns>
    public ReadOnlyCollection<ShopTokenDat> LoadShopTokenDat()
    {
        return ShopTokenDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SigilDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of SigilDisplayDat.</returns>
    public ReadOnlyCollection<SigilDisplayDat> LoadSigilDisplayDat()
    {
        return SigilDisplayDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SingleGroundLaserDat data.
    /// </summary>
    /// <returns>readonly collection of SingleGroundLaserDat.</returns>
    public ReadOnlyCollection<SingleGroundLaserDat> LoadSingleGroundLaserDat()
    {
        return SingleGroundLaserDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SkillArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillArtVariationsDat.</returns>
    public ReadOnlyCollection<SkillArtVariationsDat> LoadSkillArtVariationsDat()
    {
        return SkillArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SkillGemInfoDat data.
    /// </summary>
    /// <returns>readonly collection of SkillGemInfoDat.</returns>
    public ReadOnlyCollection<SkillGemInfoDat> LoadSkillGemInfoDat()
    {
        return SkillGemInfoDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SkillGemsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillGemsDat.</returns>
    public ReadOnlyCollection<SkillGemsDat> LoadSkillGemsDat()
    {
        return SkillGemsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SkillMineVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillMineVariationsDat.</returns>
    public ReadOnlyCollection<SkillMineVariationsDat> LoadSkillMineVariationsDat()
    {
        return SkillMineVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SkillMorphDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of SkillMorphDisplayDat.</returns>
    public ReadOnlyCollection<SkillMorphDisplayDat> LoadSkillMorphDisplayDat()
    {
        return SkillMorphDisplayDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SkillSurgeEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillSurgeEffectsDat.</returns>
    public ReadOnlyCollection<SkillSurgeEffectsDat> LoadSkillSurgeEffectsDat()
    {
        return SkillSurgeEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SkillTotemVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillTotemVariationsDat.</returns>
    public ReadOnlyCollection<SkillTotemVariationsDat> LoadSkillTotemVariationsDat()
    {
        return SkillTotemVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SkillTrapVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillTrapVariationsDat.</returns>
    public ReadOnlyCollection<SkillTrapVariationsDat> LoadSkillTrapVariationsDat()
    {
        return SkillTrapVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SocketNotchesDat data.
    /// </summary>
    /// <returns>readonly collection of SocketNotchesDat.</returns>
    public ReadOnlyCollection<SocketNotchesDat> LoadSocketNotchesDat()
    {
        return SocketNotchesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SoundEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of SoundEffectsDat.</returns>
    public ReadOnlyCollection<SoundEffectsDat> LoadSoundEffectsDat()
    {
        return SoundEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SpawnAdditionalChestsOrClustersDat data.
    /// </summary>
    /// <returns>readonly collection of SpawnAdditionalChestsOrClustersDat.</returns>
    public ReadOnlyCollection<SpawnAdditionalChestsOrClustersDat> LoadSpawnAdditionalChestsOrClustersDat()
    {
        return SpawnAdditionalChestsOrClustersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SpawnObjectDat data.
    /// </summary>
    /// <returns>readonly collection of SpawnObjectDat.</returns>
    public ReadOnlyCollection<SpawnObjectDat> LoadSpawnObjectDat()
    {
        return SpawnObjectDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SpecialRoomsDat data.
    /// </summary>
    /// <returns>readonly collection of SpecialRoomsDat.</returns>
    public ReadOnlyCollection<SpecialRoomsDat> LoadSpecialRoomsDat()
    {
        return SpecialRoomsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SpecialTilesDat data.
    /// </summary>
    /// <returns>readonly collection of SpecialTilesDat.</returns>
    public ReadOnlyCollection<SpecialTilesDat> LoadSpecialTilesDat()
    {
        return SpecialTilesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SpectreOverridesDat data.
    /// </summary>
    /// <returns>readonly collection of SpectreOverridesDat.</returns>
    public ReadOnlyCollection<SpectreOverridesDat> LoadSpectreOverridesDat()
    {
        return SpectreOverridesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StartingPassiveSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of StartingPassiveSkillsDat.</returns>
    public ReadOnlyCollection<StartingPassiveSkillsDat> LoadStartingPassiveSkillsDat()
    {
        return StartingPassiveSkillsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StashTabAffinitiesDat data.
    /// </summary>
    /// <returns>readonly collection of StashTabAffinitiesDat.</returns>
    public ReadOnlyCollection<StashTabAffinitiesDat> LoadStashTabAffinitiesDat()
    {
        return StashTabAffinitiesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StashTypeDat data.
    /// </summary>
    /// <returns>readonly collection of StashTypeDat.</returns>
    public ReadOnlyCollection<StashTypeDat> LoadStashTypeDat()
    {
        return StashTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StatDescriptionFunctionsDat data.
    /// </summary>
    /// <returns>readonly collection of StatDescriptionFunctionsDat.</returns>
    public ReadOnlyCollection<StatDescriptionFunctionsDat> LoadStatDescriptionFunctionsDat()
    {
        return StatDescriptionFunctionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StatsAffectingGenerationDat data.
    /// </summary>
    /// <returns>readonly collection of StatsAffectingGenerationDat.</returns>
    public ReadOnlyCollection<StatsAffectingGenerationDat> LoadStatsAffectingGenerationDat()
    {
        return StatsAffectingGenerationDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StatsDat data.
    /// </summary>
    /// <returns>readonly collection of StatsDat.</returns>
    public ReadOnlyCollection<StatsDat> LoadStatsDat()
    {
        return StatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StrDexIntMissionExtraRequirementDat data.
    /// </summary>
    /// <returns>readonly collection of StrDexIntMissionExtraRequirementDat.</returns>
    public ReadOnlyCollection<StrDexIntMissionExtraRequirementDat> LoadStrDexIntMissionExtraRequirementDat()
    {
        return StrDexIntMissionExtraRequirementDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StrDexIntMissionsDat data.
    /// </summary>
    /// <returns>readonly collection of StrDexIntMissionsDat.</returns>
    public ReadOnlyCollection<StrDexIntMissionsDat> LoadStrDexIntMissionsDat()
    {
        return StrDexIntMissionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SuicideExplosionDat data.
    /// </summary>
    /// <returns>readonly collection of SuicideExplosionDat.</returns>
    public ReadOnlyCollection<SuicideExplosionDat> LoadSuicideExplosionDat()
    {
        return SuicideExplosionDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SummonedSpecificBarrelsDat data.
    /// </summary>
    /// <returns>readonly collection of SummonedSpecificBarrelsDat.</returns>
    public ReadOnlyCollection<SummonedSpecificBarrelsDat> LoadSummonedSpecificBarrelsDat()
    {
        return SummonedSpecificBarrelsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SummonedSpecificMonstersDat data.
    /// </summary>
    /// <returns>readonly collection of SummonedSpecificMonstersDat.</returns>
    public ReadOnlyCollection<SummonedSpecificMonstersDat> LoadSummonedSpecificMonstersDat()
    {
        return SummonedSpecificMonstersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SummonedSpecificMonstersOnDeathDat data.
    /// </summary>
    /// <returns>readonly collection of SummonedSpecificMonstersOnDeathDat.</returns>
    public ReadOnlyCollection<SummonedSpecificMonstersOnDeathDat> LoadSummonedSpecificMonstersOnDeathDat()
    {
        return SummonedSpecificMonstersOnDeathDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SupporterPackSetsDat data.
    /// </summary>
    /// <returns>readonly collection of SupporterPackSetsDat.</returns>
    public ReadOnlyCollection<SupporterPackSetsDat> LoadSupporterPackSetsDat()
    {
        return SupporterPackSetsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SurgeEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of SurgeEffectsDat.</returns>
    public ReadOnlyCollection<SurgeEffectsDat> LoadSurgeEffectsDat()
    {
        return SurgeEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SurgeTypesDat data.
    /// </summary>
    /// <returns>readonly collection of SurgeTypesDat.</returns>
    public ReadOnlyCollection<SurgeTypesDat> LoadSurgeTypesDat()
    {
        return SurgeTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TableChargeDat data.
    /// </summary>
    /// <returns>readonly collection of TableChargeDat.</returns>
    public ReadOnlyCollection<TableChargeDat> LoadTableChargeDat()
    {
        return TableChargeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TableMonsterSpawnersDat data.
    /// </summary>
    /// <returns>readonly collection of TableMonsterSpawnersDat.</returns>
    public ReadOnlyCollection<TableMonsterSpawnersDat> LoadTableMonsterSpawnersDat()
    {
        return TableMonsterSpawnersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TagsDat data.
    /// </summary>
    /// <returns>readonly collection of TagsDat.</returns>
    public ReadOnlyCollection<TagsDat> LoadTagsDat()
    {
        return TagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TalkingPetAudioEventsDat data.
    /// </summary>
    /// <returns>readonly collection of TalkingPetAudioEventsDat.</returns>
    public ReadOnlyCollection<TalkingPetAudioEventsDat> LoadTalkingPetAudioEventsDat()
    {
        return TalkingPetAudioEventsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TalkingPetNPCAudioDat data.
    /// </summary>
    /// <returns>readonly collection of TalkingPetNPCAudioDat.</returns>
    public ReadOnlyCollection<TalkingPetNPCAudioDat> LoadTalkingPetNPCAudioDat()
    {
        return TalkingPetNPCAudioDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TalkingPetsDat data.
    /// </summary>
    /// <returns>readonly collection of TalkingPetsDat.</returns>
    public ReadOnlyCollection<TalkingPetsDat> LoadTalkingPetsDat()
    {
        return TalkingPetsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TencentAutoLootPetCurrenciesDat data.
    /// </summary>
    /// <returns>readonly collection of TencentAutoLootPetCurrenciesDat.</returns>
    public ReadOnlyCollection<TencentAutoLootPetCurrenciesDat> LoadTencentAutoLootPetCurrenciesDat()
    {
        return TencentAutoLootPetCurrenciesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TencentAutoLootPetCurrenciesExcludableDat data.
    /// </summary>
    /// <returns>readonly collection of TencentAutoLootPetCurrenciesExcludableDat.</returns>
    public ReadOnlyCollection<TencentAutoLootPetCurrenciesExcludableDat> LoadTencentAutoLootPetCurrenciesExcludableDat()
    {
        return TencentAutoLootPetCurrenciesExcludableDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TerrainPluginsDat data.
    /// </summary>
    /// <returns>readonly collection of TerrainPluginsDat.</returns>
    public ReadOnlyCollection<TerrainPluginsDat> LoadTerrainPluginsDat()
    {
        return TerrainPluginsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TipsDat data.
    /// </summary>
    /// <returns>readonly collection of TipsDat.</returns>
    public ReadOnlyCollection<TipsDat> LoadTipsDat()
    {
        return TipsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TopologiesDat data.
    /// </summary>
    /// <returns>readonly collection of TopologiesDat.</returns>
    public ReadOnlyCollection<TopologiesDat> LoadTopologiesDat()
    {
        return TopologiesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TradeMarketCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of TradeMarketCategoryDat.</returns>
    public ReadOnlyCollection<TradeMarketCategoryDat> LoadTradeMarketCategoryDat()
    {
        return TradeMarketCategoryDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TradeMarketCategoryGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of TradeMarketCategoryGroupsDat.</returns>
    public ReadOnlyCollection<TradeMarketCategoryGroupsDat> LoadTradeMarketCategoryGroupsDat()
    {
        return TradeMarketCategoryGroupsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TradeMarketCategoryListAllClassDat data.
    /// </summary>
    /// <returns>readonly collection of TradeMarketCategoryListAllClassDat.</returns>
    public ReadOnlyCollection<TradeMarketCategoryListAllClassDat> LoadTradeMarketCategoryListAllClassDat()
    {
        return TradeMarketCategoryListAllClassDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TradeMarketIndexItemAsDat data.
    /// </summary>
    /// <returns>readonly collection of TradeMarketIndexItemAsDat.</returns>
    public ReadOnlyCollection<TradeMarketIndexItemAsDat> LoadTradeMarketIndexItemAsDat()
    {
        return TradeMarketIndexItemAsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TreasureHunterMissionsDat data.
    /// </summary>
    /// <returns>readonly collection of TreasureHunterMissionsDat.</returns>
    public ReadOnlyCollection<TreasureHunterMissionsDat> LoadTreasureHunterMissionsDat()
    {
        return TreasureHunterMissionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TriggerBeamDat data.
    /// </summary>
    /// <returns>readonly collection of TriggerBeamDat.</returns>
    public ReadOnlyCollection<TriggerBeamDat> LoadTriggerBeamDat()
    {
        return TriggerBeamDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TriggerSpawnersDat data.
    /// </summary>
    /// <returns>readonly collection of TriggerSpawnersDat.</returns>
    public ReadOnlyCollection<TriggerSpawnersDat> LoadTriggerSpawnersDat()
    {
        return TriggerSpawnersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TutorialDat data.
    /// </summary>
    /// <returns>readonly collection of TutorialDat.</returns>
    public ReadOnlyCollection<TutorialDat> LoadTutorialDat()
    {
        return TutorialDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UITalkTextDat data.
    /// </summary>
    /// <returns>readonly collection of UITalkTextDat.</returns>
    public ReadOnlyCollection<UITalkTextDat> LoadUITalkTextDat()
    {
        return UITalkTextDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UniqueChestsDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueChestsDat.</returns>
    public ReadOnlyCollection<UniqueChestsDat> LoadUniqueChestsDat()
    {
        return UniqueChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UniqueJewelLimitsDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueJewelLimitsDat.</returns>
    public ReadOnlyCollection<UniqueJewelLimitsDat> LoadUniqueJewelLimitsDat()
    {
        return UniqueJewelLimitsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UniqueMapInfoDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueMapInfoDat.</returns>
    public ReadOnlyCollection<UniqueMapInfoDat> LoadUniqueMapInfoDat()
    {
        return UniqueMapInfoDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UniqueMapsDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueMapsDat.</returns>
    public ReadOnlyCollection<UniqueMapsDat> LoadUniqueMapsDat()
    {
        return UniqueMapsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UniqueStashLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueStashLayoutDat.</returns>
    public ReadOnlyCollection<UniqueStashLayoutDat> LoadUniqueStashLayoutDat()
    {
        return UniqueStashLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UniqueStashTypesDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueStashTypesDat.</returns>
    public ReadOnlyCollection<UniqueStashTypesDat> LoadUniqueStashTypesDat()
    {
        return UniqueStashTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets VirtualStatContextFlagsDat data.
    /// </summary>
    /// <returns>readonly collection of VirtualStatContextFlagsDat.</returns>
    public ReadOnlyCollection<VirtualStatContextFlagsDat> LoadVirtualStatContextFlagsDat()
    {
        return VirtualStatContextFlagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets VoteStateDat data.
    /// </summary>
    /// <returns>readonly collection of VoteStateDat.</returns>
    public ReadOnlyCollection<VoteStateDat> LoadVoteStateDat()
    {
        return VoteStateDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets VoteTypeDat data.
    /// </summary>
    /// <returns>readonly collection of VoteTypeDat.</returns>
    public ReadOnlyCollection<VoteTypeDat> LoadVoteTypeDat()
    {
        return VoteTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WeaponClassesDat data.
    /// </summary>
    /// <returns>readonly collection of WeaponClassesDat.</returns>
    public ReadOnlyCollection<WeaponClassesDat> LoadWeaponClassesDat()
    {
        return WeaponClassesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WeaponImpactSoundDataDat data.
    /// </summary>
    /// <returns>readonly collection of WeaponImpactSoundDataDat.</returns>
    public ReadOnlyCollection<WeaponImpactSoundDataDat> LoadWeaponImpactSoundDataDat()
    {
        return WeaponImpactSoundDataDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WeaponTypesDat data.
    /// </summary>
    /// <returns>readonly collection of WeaponTypesDat.</returns>
    public ReadOnlyCollection<WeaponTypesDat> LoadWeaponTypesDat()
    {
        return WeaponTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WindowCursorsDat data.
    /// </summary>
    /// <returns>readonly collection of WindowCursorsDat.</returns>
    public ReadOnlyCollection<WindowCursorsDat> LoadWindowCursorsDat()
    {
        return WindowCursorsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WordsDat data.
    /// </summary>
    /// <returns>readonly collection of WordsDat.</returns>
    public ReadOnlyCollection<WordsDat> LoadWordsDat()
    {
        return WordsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WorldAreasDat data.
    /// </summary>
    /// <returns>readonly collection of WorldAreasDat.</returns>
    public ReadOnlyCollection<WorldAreasDat> LoadWorldAreasDat()
    {
        return WorldAreasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WorldAreaLeagueChancesDat data.
    /// </summary>
    /// <returns>readonly collection of WorldAreaLeagueChancesDat.</returns>
    public ReadOnlyCollection<WorldAreaLeagueChancesDat> LoadWorldAreaLeagueChancesDat()
    {
        return WorldAreaLeagueChancesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WorldPopupIconTypesDat data.
    /// </summary>
    /// <returns>readonly collection of WorldPopupIconTypesDat.</returns>
    public ReadOnlyCollection<WorldPopupIconTypesDat> LoadWorldPopupIconTypesDat()
    {
        return WorldPopupIconTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ZanaLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of ZanaLevelsDat.</returns>
    public ReadOnlyCollection<ZanaLevelsDat> LoadZanaLevelsDat()
    {
        return ZanaLevelsDat.Load(dataLoader).AsReadOnly();
    }
}
