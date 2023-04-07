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
    public ReadOnlyCollection<RogueExilesDat> GetRogueExilesDat()
    {
        return RogueExilesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RogueExileLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of RogueExileLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<RogueExileLifeScalingPerLevelDat> GetRogueExileLifeScalingPerLevelDat()
    {
        return RogueExileLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShrineBuffsDat data.
    /// </summary>
    /// <returns>readonly collection of ShrineBuffsDat.</returns>
    public ReadOnlyCollection<ShrineBuffsDat> GetShrineBuffsDat()
    {
        return ShrineBuffsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShrinesDat data.
    /// </summary>
    /// <returns>readonly collection of ShrinesDat.</returns>
    public ReadOnlyCollection<ShrinesDat> GetShrinesDat()
    {
        return ShrinesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShrineSoundsDat data.
    /// </summary>
    /// <returns>readonly collection of ShrineSoundsDat.</returns>
    public ReadOnlyCollection<ShrineSoundsDat> GetShrineSoundsDat()
    {
        return ShrineSoundsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StrongboxesDat data.
    /// </summary>
    /// <returns>readonly collection of StrongboxesDat.</returns>
    public ReadOnlyCollection<StrongboxesDat> GetStrongboxesDat()
    {
        return StrongboxesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets InvasionMonsterRestrictionsDat data.
    /// </summary>
    /// <returns>readonly collection of InvasionMonsterRestrictionsDat.</returns>
    public ReadOnlyCollection<InvasionMonsterRestrictionsDat> GetInvasionMonsterRestrictionsDat()
    {
        return InvasionMonsterRestrictionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets InvasionMonstersPerAreaDat data.
    /// </summary>
    /// <returns>readonly collection of InvasionMonstersPerAreaDat.</returns>
    public ReadOnlyCollection<InvasionMonstersPerAreaDat> GetInvasionMonstersPerAreaDat()
    {
        return InvasionMonstersPerAreaDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BeyondDemonsDat data.
    /// </summary>
    /// <returns>readonly collection of BeyondDemonsDat.</returns>
    public ReadOnlyCollection<BeyondDemonsDat> GetBeyondDemonsDat()
    {
        return BeyondDemonsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BeyondFactionsDat data.
    /// </summary>
    /// <returns>readonly collection of BeyondFactionsDat.</returns>
    public ReadOnlyCollection<BeyondFactionsDat> GetBeyondFactionsDat()
    {
        return BeyondFactionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BloodlinesDat data.
    /// </summary>
    /// <returns>readonly collection of BloodlinesDat.</returns>
    public ReadOnlyCollection<BloodlinesDat> GetBloodlinesDat()
    {
        return BloodlinesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TormentSpiritsDat data.
    /// </summary>
    /// <returns>readonly collection of TormentSpiritsDat.</returns>
    public ReadOnlyCollection<TormentSpiritsDat> GetTormentSpiritsDat()
    {
        return TormentSpiritsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DivinationCardArtDat data.
    /// </summary>
    /// <returns>readonly collection of DivinationCardArtDat.</returns>
    public ReadOnlyCollection<DivinationCardArtDat> GetDivinationCardArtDat()
    {
        return DivinationCardArtDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WarbandsGraphDat data.
    /// </summary>
    /// <returns>readonly collection of WarbandsGraphDat.</returns>
    public ReadOnlyCollection<WarbandsGraphDat> GetWarbandsGraphDat()
    {
        return WarbandsGraphDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WarbandsMapGraphDat data.
    /// </summary>
    /// <returns>readonly collection of WarbandsMapGraphDat.</returns>
    public ReadOnlyCollection<WarbandsMapGraphDat> GetWarbandsMapGraphDat()
    {
        return WarbandsMapGraphDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WarbandsPackMonstersDat data.
    /// </summary>
    /// <returns>readonly collection of WarbandsPackMonstersDat.</returns>
    public ReadOnlyCollection<WarbandsPackMonstersDat> GetWarbandsPackMonstersDat()
    {
        return WarbandsPackMonstersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WarbandsPackNumbersDat data.
    /// </summary>
    /// <returns>readonly collection of WarbandsPackNumbersDat.</returns>
    public ReadOnlyCollection<WarbandsPackNumbersDat> GetWarbandsPackNumbersDat()
    {
        return WarbandsPackNumbersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TalismanMonsterModsDat data.
    /// </summary>
    /// <returns>readonly collection of TalismanMonsterModsDat.</returns>
    public ReadOnlyCollection<TalismanMonsterModsDat> GetTalismanMonsterModsDat()
    {
        return TalismanMonsterModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TalismanPacksDat data.
    /// </summary>
    /// <returns>readonly collection of TalismanPacksDat.</returns>
    public ReadOnlyCollection<TalismanPacksDat> GetTalismanPacksDat()
    {
        return TalismanPacksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TalismansDat data.
    /// </summary>
    /// <returns>readonly collection of TalismansDat.</returns>
    public ReadOnlyCollection<TalismansDat> GetTalismansDat()
    {
        return TalismansDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthAreasDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthAreasDat.</returns>
    public ReadOnlyCollection<LabyrinthAreasDat> GetLabyrinthAreasDat()
    {
        return LabyrinthAreasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthBonusItemsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthBonusItemsDat.</returns>
    public ReadOnlyCollection<LabyrinthBonusItemsDat> GetLabyrinthBonusItemsDat()
    {
        return LabyrinthBonusItemsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthExclusionGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthExclusionGroupsDat.</returns>
    public ReadOnlyCollection<LabyrinthExclusionGroupsDat> GetLabyrinthExclusionGroupsDat()
    {
        return LabyrinthExclusionGroupsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthIzaroChestsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthIzaroChestsDat.</returns>
    public ReadOnlyCollection<LabyrinthIzaroChestsDat> GetLabyrinthIzaroChestsDat()
    {
        return LabyrinthIzaroChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthNodeOverridesDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthNodeOverridesDat.</returns>
    public ReadOnlyCollection<LabyrinthNodeOverridesDat> GetLabyrinthNodeOverridesDat()
    {
        return LabyrinthNodeOverridesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthRewardTypesDat.</returns>
    public ReadOnlyCollection<LabyrinthRewardTypesDat> GetLabyrinthRewardTypesDat()
    {
        return LabyrinthRewardTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthsDat.</returns>
    public ReadOnlyCollection<LabyrinthsDat> GetLabyrinthsDat()
    {
        return LabyrinthsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthSecretEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthSecretEffectsDat.</returns>
    public ReadOnlyCollection<LabyrinthSecretEffectsDat> GetLabyrinthSecretEffectsDat()
    {
        return LabyrinthSecretEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthSecretsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthSecretsDat.</returns>
    public ReadOnlyCollection<LabyrinthSecretsDat> GetLabyrinthSecretsDat()
    {
        return LabyrinthSecretsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthSectionDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthSectionDat.</returns>
    public ReadOnlyCollection<LabyrinthSectionDat> GetLabyrinthSectionDat()
    {
        return LabyrinthSectionDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthSectionLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthSectionLayoutDat.</returns>
    public ReadOnlyCollection<LabyrinthSectionLayoutDat> GetLabyrinthSectionLayoutDat()
    {
        return LabyrinthSectionLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthTrialsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthTrialsDat.</returns>
    public ReadOnlyCollection<LabyrinthTrialsDat> GetLabyrinthTrialsDat()
    {
        return LabyrinthTrialsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LabyrinthTrinketsDat data.
    /// </summary>
    /// <returns>readonly collection of LabyrinthTrinketsDat.</returns>
    public ReadOnlyCollection<LabyrinthTrinketsDat> GetLabyrinthTrinketsDat()
    {
        return LabyrinthTrinketsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PerandusBossesDat data.
    /// </summary>
    /// <returns>readonly collection of PerandusBossesDat.</returns>
    public ReadOnlyCollection<PerandusBossesDat> GetPerandusBossesDat()
    {
        return PerandusBossesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PerandusChestsDat data.
    /// </summary>
    /// <returns>readonly collection of PerandusChestsDat.</returns>
    public ReadOnlyCollection<PerandusChestsDat> GetPerandusChestsDat()
    {
        return PerandusChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PerandusDaemonsDat data.
    /// </summary>
    /// <returns>readonly collection of PerandusDaemonsDat.</returns>
    public ReadOnlyCollection<PerandusDaemonsDat> GetPerandusDaemonsDat()
    {
        return PerandusDaemonsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PerandusGuardsDat data.
    /// </summary>
    /// <returns>readonly collection of PerandusGuardsDat.</returns>
    public ReadOnlyCollection<PerandusGuardsDat> GetPerandusGuardsDat()
    {
        return PerandusGuardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PropheciesDat data.
    /// </summary>
    /// <returns>readonly collection of PropheciesDat.</returns>
    public ReadOnlyCollection<PropheciesDat> GetPropheciesDat()
    {
        return PropheciesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ProphecyChainDat data.
    /// </summary>
    /// <returns>readonly collection of ProphecyChainDat.</returns>
    public ReadOnlyCollection<ProphecyChainDat> GetProphecyChainDat()
    {
        return ProphecyChainDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ProphecyTypeDat data.
    /// </summary>
    /// <returns>readonly collection of ProphecyTypeDat.</returns>
    public ReadOnlyCollection<ProphecyTypeDat> GetProphecyTypeDat()
    {
        return ProphecyTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShaperGuardiansDat data.
    /// </summary>
    /// <returns>readonly collection of ShaperGuardiansDat.</returns>
    public ReadOnlyCollection<ShaperGuardiansDat> GetShaperGuardiansDat()
    {
        return ShaperGuardiansDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EssencesDat data.
    /// </summary>
    /// <returns>readonly collection of EssencesDat.</returns>
    public ReadOnlyCollection<EssencesDat> GetEssencesDat()
    {
        return EssencesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EssenceTypeDat data.
    /// </summary>
    /// <returns>readonly collection of EssenceTypeDat.</returns>
    public ReadOnlyCollection<EssenceTypeDat> GetEssenceTypeDat()
    {
        return EssenceTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BreachBossLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BreachBossLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<BreachBossLifeScalingPerLevelDat> GetBreachBossLifeScalingPerLevelDat()
    {
        return BreachBossLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BreachElementDat data.
    /// </summary>
    /// <returns>readonly collection of BreachElementDat.</returns>
    public ReadOnlyCollection<BreachElementDat> GetBreachElementDat()
    {
        return BreachElementDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BreachstoneUpgradesDat data.
    /// </summary>
    /// <returns>readonly collection of BreachstoneUpgradesDat.</returns>
    public ReadOnlyCollection<BreachstoneUpgradesDat> GetBreachstoneUpgradesDat()
    {
        return BreachstoneUpgradesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarbingersDat data.
    /// </summary>
    /// <returns>readonly collection of HarbingersDat.</returns>
    public ReadOnlyCollection<HarbingersDat> GetHarbingersDat()
    {
        return HarbingersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PantheonPanelLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of PantheonPanelLayoutDat.</returns>
    public ReadOnlyCollection<PantheonPanelLayoutDat> GetPantheonPanelLayoutDat()
    {
        return PantheonPanelLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PantheonSoulsDat data.
    /// </summary>
    /// <returns>readonly collection of PantheonSoulsDat.</returns>
    public ReadOnlyCollection<PantheonSoulsDat> GetPantheonSoulsDat()
    {
        return PantheonSoulsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AbyssObjectsDat data.
    /// </summary>
    /// <returns>readonly collection of AbyssObjectsDat.</returns>
    public ReadOnlyCollection<AbyssObjectsDat> GetAbyssObjectsDat()
    {
        return AbyssObjectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ElderBossArenasDat data.
    /// </summary>
    /// <returns>readonly collection of ElderBossArenasDat.</returns>
    public ReadOnlyCollection<ElderBossArenasDat> GetElderBossArenasDat()
    {
        return ElderBossArenasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ElderMapBossOverrideDat data.
    /// </summary>
    /// <returns>readonly collection of ElderMapBossOverrideDat.</returns>
    public ReadOnlyCollection<ElderMapBossOverrideDat> GetElderMapBossOverrideDat()
    {
        return ElderMapBossOverrideDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ElderGuardiansDat data.
    /// </summary>
    /// <returns>readonly collection of ElderGuardiansDat.</returns>
    public ReadOnlyCollection<ElderGuardiansDat> GetElderGuardiansDat()
    {
        return ElderGuardiansDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryCapturableMonstersDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryCapturableMonstersDat.</returns>
    public ReadOnlyCollection<BestiaryCapturableMonstersDat> GetBestiaryCapturableMonstersDat()
    {
        return BestiaryCapturableMonstersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryEncountersDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryEncountersDat.</returns>
    public ReadOnlyCollection<BestiaryEncountersDat> GetBestiaryEncountersDat()
    {
        return BestiaryEncountersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryFamiliesDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryFamiliesDat.</returns>
    public ReadOnlyCollection<BestiaryFamiliesDat> GetBestiaryFamiliesDat()
    {
        return BestiaryFamiliesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryGenusDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryGenusDat.</returns>
    public ReadOnlyCollection<BestiaryGenusDat> GetBestiaryGenusDat()
    {
        return BestiaryGenusDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryGroupsDat.</returns>
    public ReadOnlyCollection<BestiaryGroupsDat> GetBestiaryGroupsDat()
    {
        return BestiaryGroupsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryNetsDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryNetsDat.</returns>
    public ReadOnlyCollection<BestiaryNetsDat> GetBestiaryNetsDat()
    {
        return BestiaryNetsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryRecipeComponentDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryRecipeComponentDat.</returns>
    public ReadOnlyCollection<BestiaryRecipeComponentDat> GetBestiaryRecipeComponentDat()
    {
        return BestiaryRecipeComponentDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryRecipeCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryRecipeCategoriesDat.</returns>
    public ReadOnlyCollection<BestiaryRecipeCategoriesDat> GetBestiaryRecipeCategoriesDat()
    {
        return BestiaryRecipeCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BestiaryRecipesDat data.
    /// </summary>
    /// <returns>readonly collection of BestiaryRecipesDat.</returns>
    public ReadOnlyCollection<BestiaryRecipesDat> GetBestiaryRecipesDat()
    {
        return BestiaryRecipesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArchitectLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of ArchitectLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<ArchitectLifeScalingPerLevelDat> GetArchitectLifeScalingPerLevelDat()
    {
        return ArchitectLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IncursionArchitectDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionArchitectDat.</returns>
    public ReadOnlyCollection<IncursionArchitectDat> GetIncursionArchitectDat()
    {
        return IncursionArchitectDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IncursionBracketsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionBracketsDat.</returns>
    public ReadOnlyCollection<IncursionBracketsDat> GetIncursionBracketsDat()
    {
        return IncursionBracketsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IncursionChestRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionChestRewardsDat.</returns>
    public ReadOnlyCollection<IncursionChestRewardsDat> GetIncursionChestRewardsDat()
    {
        return IncursionChestRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IncursionChestsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionChestsDat.</returns>
    public ReadOnlyCollection<IncursionChestsDat> GetIncursionChestsDat()
    {
        return IncursionChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IncursionRoomBossFightEventsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionRoomBossFightEventsDat.</returns>
    public ReadOnlyCollection<IncursionRoomBossFightEventsDat> GetIncursionRoomBossFightEventsDat()
    {
        return IncursionRoomBossFightEventsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IncursionRoomsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionRoomsDat.</returns>
    public ReadOnlyCollection<IncursionRoomsDat> GetIncursionRoomsDat()
    {
        return IncursionRoomsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IncursionUniqueUpgradeComponentsDat data.
    /// </summary>
    /// <returns>readonly collection of IncursionUniqueUpgradeComponentsDat.</returns>
    public ReadOnlyCollection<IncursionUniqueUpgradeComponentsDat> GetIncursionUniqueUpgradeComponentsDat()
    {
        return IncursionUniqueUpgradeComponentsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveAzuriteShopDat data.
    /// </summary>
    /// <returns>readonly collection of DelveAzuriteShopDat.</returns>
    public ReadOnlyCollection<DelveAzuriteShopDat> GetDelveAzuriteShopDat()
    {
        return DelveAzuriteShopDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveBiomesDat data.
    /// </summary>
    /// <returns>readonly collection of DelveBiomesDat.</returns>
    public ReadOnlyCollection<DelveBiomesDat> GetDelveBiomesDat()
    {
        return DelveBiomesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveCatchupDepthsDat data.
    /// </summary>
    /// <returns>readonly collection of DelveCatchupDepthsDat.</returns>
    public ReadOnlyCollection<DelveCatchupDepthsDat> GetDelveCatchupDepthsDat()
    {
        return DelveCatchupDepthsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveCraftingModifierDescriptionsDat data.
    /// </summary>
    /// <returns>readonly collection of DelveCraftingModifierDescriptionsDat.</returns>
    public ReadOnlyCollection<DelveCraftingModifierDescriptionsDat> GetDelveCraftingModifierDescriptionsDat()
    {
        return DelveCraftingModifierDescriptionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveCraftingModifiersDat data.
    /// </summary>
    /// <returns>readonly collection of DelveCraftingModifiersDat.</returns>
    public ReadOnlyCollection<DelveCraftingModifiersDat> GetDelveCraftingModifiersDat()
    {
        return DelveCraftingModifiersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveCraftingTagsDat data.
    /// </summary>
    /// <returns>readonly collection of DelveCraftingTagsDat.</returns>
    public ReadOnlyCollection<DelveCraftingTagsDat> GetDelveCraftingTagsDat()
    {
        return DelveCraftingTagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveDynamiteDat data.
    /// </summary>
    /// <returns>readonly collection of DelveDynamiteDat.</returns>
    public ReadOnlyCollection<DelveDynamiteDat> GetDelveDynamiteDat()
    {
        return DelveDynamiteDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveFeaturesDat data.
    /// </summary>
    /// <returns>readonly collection of DelveFeaturesDat.</returns>
    public ReadOnlyCollection<DelveFeaturesDat> GetDelveFeaturesDat()
    {
        return DelveFeaturesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveFlaresDat data.
    /// </summary>
    /// <returns>readonly collection of DelveFlaresDat.</returns>
    public ReadOnlyCollection<DelveFlaresDat> GetDelveFlaresDat()
    {
        return DelveFlaresDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveLevelScalingDat data.
    /// </summary>
    /// <returns>readonly collection of DelveLevelScalingDat.</returns>
    public ReadOnlyCollection<DelveLevelScalingDat> GetDelveLevelScalingDat()
    {
        return DelveLevelScalingDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveMonsterSpawnersDat data.
    /// </summary>
    /// <returns>readonly collection of DelveMonsterSpawnersDat.</returns>
    public ReadOnlyCollection<DelveMonsterSpawnersDat> GetDelveMonsterSpawnersDat()
    {
        return DelveMonsterSpawnersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveResourcePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of DelveResourcePerLevelDat.</returns>
    public ReadOnlyCollection<DelveResourcePerLevelDat> GetDelveResourcePerLevelDat()
    {
        return DelveResourcePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveRewardTierConstantsDat data.
    /// </summary>
    /// <returns>readonly collection of DelveRewardTierConstantsDat.</returns>
    public ReadOnlyCollection<DelveRewardTierConstantsDat> GetDelveRewardTierConstantsDat()
    {
        return DelveRewardTierConstantsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveRoomsDat data.
    /// </summary>
    /// <returns>readonly collection of DelveRoomsDat.</returns>
    public ReadOnlyCollection<DelveRoomsDat> GetDelveRoomsDat()
    {
        return DelveRoomsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveUpgradesDat data.
    /// </summary>
    /// <returns>readonly collection of DelveUpgradesDat.</returns>
    public ReadOnlyCollection<DelveUpgradesDat> GetDelveUpgradesDat()
    {
        return DelveUpgradesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalChoiceActionsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalChoiceActionsDat.</returns>
    public ReadOnlyCollection<BetrayalChoiceActionsDat> GetBetrayalChoiceActionsDat()
    {
        return BetrayalChoiceActionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalChoicesDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalChoicesDat.</returns>
    public ReadOnlyCollection<BetrayalChoicesDat> GetBetrayalChoicesDat()
    {
        return BetrayalChoicesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalDialogueDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalDialogueDat.</returns>
    public ReadOnlyCollection<BetrayalDialogueDat> GetBetrayalDialogueDat()
    {
        return BetrayalDialogueDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalFortsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalFortsDat.</returns>
    public ReadOnlyCollection<BetrayalFortsDat> GetBetrayalFortsDat()
    {
        return BetrayalFortsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalJobsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalJobsDat.</returns>
    public ReadOnlyCollection<BetrayalJobsDat> GetBetrayalJobsDat()
    {
        return BetrayalJobsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalRanksDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalRanksDat.</returns>
    public ReadOnlyCollection<BetrayalRanksDat> GetBetrayalRanksDat()
    {
        return BetrayalRanksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalRelationshipStateDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalRelationshipStateDat.</returns>
    public ReadOnlyCollection<BetrayalRelationshipStateDat> GetBetrayalRelationshipStateDat()
    {
        return BetrayalRelationshipStateDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalTargetJobAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalTargetJobAchievementsDat.</returns>
    public ReadOnlyCollection<BetrayalTargetJobAchievementsDat> GetBetrayalTargetJobAchievementsDat()
    {
        return BetrayalTargetJobAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalTargetLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalTargetLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<BetrayalTargetLifeScalingPerLevelDat> GetBetrayalTargetLifeScalingPerLevelDat()
    {
        return BetrayalTargetLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalTargetsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalTargetsDat.</returns>
    public ReadOnlyCollection<BetrayalTargetsDat> GetBetrayalTargetsDat()
    {
        return BetrayalTargetsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalTraitorRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalTraitorRewardsDat.</returns>
    public ReadOnlyCollection<BetrayalTraitorRewardsDat> GetBetrayalTraitorRewardsDat()
    {
        return BetrayalTraitorRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalUpgradesDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalUpgradesDat.</returns>
    public ReadOnlyCollection<BetrayalUpgradesDat> GetBetrayalUpgradesDat()
    {
        return BetrayalUpgradesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BetrayalWallLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BetrayalWallLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<BetrayalWallLifeScalingPerLevelDat> GetBetrayalWallLifeScalingPerLevelDat()
    {
        return BetrayalWallLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SafehouseBYOCraftingDat data.
    /// </summary>
    /// <returns>readonly collection of SafehouseBYOCraftingDat.</returns>
    public ReadOnlyCollection<SafehouseBYOCraftingDat> GetSafehouseBYOCraftingDat()
    {
        return SafehouseBYOCraftingDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SafehouseCraftingSpreeTypeDat data.
    /// </summary>
    /// <returns>readonly collection of SafehouseCraftingSpreeTypeDat.</returns>
    public ReadOnlyCollection<SafehouseCraftingSpreeTypeDat> GetSafehouseCraftingSpreeTypeDat()
    {
        return SafehouseCraftingSpreeTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SafehouseCraftingSpreeCurrenciesDat data.
    /// </summary>
    /// <returns>readonly collection of SafehouseCraftingSpreeCurrenciesDat.</returns>
    public ReadOnlyCollection<SafehouseCraftingSpreeCurrenciesDat> GetSafehouseCraftingSpreeCurrenciesDat()
    {
        return SafehouseCraftingSpreeCurrenciesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ScarabsDat data.
    /// </summary>
    /// <returns>readonly collection of ScarabsDat.</returns>
    public ReadOnlyCollection<ScarabsDat> GetScarabsDat()
    {
        return ScarabsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisAreasDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisAreasDat.</returns>
    public ReadOnlyCollection<SynthesisAreasDat> GetSynthesisAreasDat()
    {
        return SynthesisAreasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisAreaSizeDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisAreaSizeDat.</returns>
    public ReadOnlyCollection<SynthesisAreaSizeDat> GetSynthesisAreaSizeDat()
    {
        return SynthesisAreaSizeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisBonusesDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisBonusesDat.</returns>
    public ReadOnlyCollection<SynthesisBonusesDat> GetSynthesisBonusesDat()
    {
        return SynthesisBonusesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisBracketsDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisBracketsDat.</returns>
    public ReadOnlyCollection<SynthesisBracketsDat> GetSynthesisBracketsDat()
    {
        return SynthesisBracketsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisFragmentDialogueDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisFragmentDialogueDat.</returns>
    public ReadOnlyCollection<SynthesisFragmentDialogueDat> GetSynthesisFragmentDialogueDat()
    {
        return SynthesisFragmentDialogueDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisGlobalModsDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisGlobalModsDat.</returns>
    public ReadOnlyCollection<SynthesisGlobalModsDat> GetSynthesisGlobalModsDat()
    {
        return SynthesisGlobalModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisMonsterExperiencePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisMonsterExperiencePerLevelDat.</returns>
    public ReadOnlyCollection<SynthesisMonsterExperiencePerLevelDat> GetSynthesisMonsterExperiencePerLevelDat()
    {
        return SynthesisMonsterExperiencePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisRewardCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisRewardCategoriesDat.</returns>
    public ReadOnlyCollection<SynthesisRewardCategoriesDat> GetSynthesisRewardCategoriesDat()
    {
        return SynthesisRewardCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SynthesisRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of SynthesisRewardTypesDat.</returns>
    public ReadOnlyCollection<SynthesisRewardTypesDat> GetSynthesisRewardTypesDat()
    {
        return SynthesisRewardTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IncubatorsDat data.
    /// </summary>
    /// <returns>readonly collection of IncubatorsDat.</returns>
    public ReadOnlyCollection<IncubatorsDat> GetIncubatorsDat()
    {
        return IncubatorsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of LegionBalancePerLevelDat.</returns>
    public ReadOnlyCollection<LegionBalancePerLevelDat> GetLegionBalancePerLevelDat()
    {
        return LegionBalancePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionChestTypesDat data.
    /// </summary>
    /// <returns>readonly collection of LegionChestTypesDat.</returns>
    public ReadOnlyCollection<LegionChestTypesDat> GetLegionChestTypesDat()
    {
        return LegionChestTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionChestCountsDat data.
    /// </summary>
    /// <returns>readonly collection of LegionChestCountsDat.</returns>
    public ReadOnlyCollection<LegionChestCountsDat> GetLegionChestCountsDat()
    {
        return LegionChestCountsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionChestsDat data.
    /// </summary>
    /// <returns>readonly collection of LegionChestsDat.</returns>
    public ReadOnlyCollection<LegionChestsDat> GetLegionChestsDat()
    {
        return LegionChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionFactionsDat data.
    /// </summary>
    /// <returns>readonly collection of LegionFactionsDat.</returns>
    public ReadOnlyCollection<LegionFactionsDat> GetLegionFactionsDat()
    {
        return LegionFactionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionMonsterCountsDat data.
    /// </summary>
    /// <returns>readonly collection of LegionMonsterCountsDat.</returns>
    public ReadOnlyCollection<LegionMonsterCountsDat> GetLegionMonsterCountsDat()
    {
        return LegionMonsterCountsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionMonsterVarietiesDat data.
    /// </summary>
    /// <returns>readonly collection of LegionMonsterVarietiesDat.</returns>
    public ReadOnlyCollection<LegionMonsterVarietiesDat> GetLegionMonsterVarietiesDat()
    {
        return LegionMonsterVarietiesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionRanksDat data.
    /// </summary>
    /// <returns>readonly collection of LegionRanksDat.</returns>
    public ReadOnlyCollection<LegionRanksDat> GetLegionRanksDat()
    {
        return LegionRanksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LegionRewardTypeVisualsDat data.
    /// </summary>
    /// <returns>readonly collection of LegionRewardTypeVisualsDat.</returns>
    public ReadOnlyCollection<LegionRewardTypeVisualsDat> GetLegionRewardTypeVisualsDat()
    {
        return LegionRewardTypeVisualsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BlightBalancePerLevelDat.</returns>
    public ReadOnlyCollection<BlightBalancePerLevelDat> GetBlightBalancePerLevelDat()
    {
        return BlightBalancePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightBossLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BlightBossLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<BlightBossLifeScalingPerLevelDat> GetBlightBossLifeScalingPerLevelDat()
    {
        return BlightBossLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightChestTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightChestTypesDat.</returns>
    public ReadOnlyCollection<BlightChestTypesDat> GetBlightChestTypesDat()
    {
        return BlightChestTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightCraftingItemsDat data.
    /// </summary>
    /// <returns>readonly collection of BlightCraftingItemsDat.</returns>
    public ReadOnlyCollection<BlightCraftingItemsDat> GetBlightCraftingItemsDat()
    {
        return BlightCraftingItemsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightCraftingRecipesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightCraftingRecipesDat.</returns>
    public ReadOnlyCollection<BlightCraftingRecipesDat> GetBlightCraftingRecipesDat()
    {
        return BlightCraftingRecipesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightCraftingResultsDat data.
    /// </summary>
    /// <returns>readonly collection of BlightCraftingResultsDat.</returns>
    public ReadOnlyCollection<BlightCraftingResultsDat> GetBlightCraftingResultsDat()
    {
        return BlightCraftingResultsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightCraftingTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightCraftingTypesDat.</returns>
    public ReadOnlyCollection<BlightCraftingTypesDat> GetBlightCraftingTypesDat()
    {
        return BlightCraftingTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightCraftingUniquesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightCraftingUniquesDat.</returns>
    public ReadOnlyCollection<BlightCraftingUniquesDat> GetBlightCraftingUniquesDat()
    {
        return BlightCraftingUniquesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightedSporeAurasDat data.
    /// </summary>
    /// <returns>readonly collection of BlightedSporeAurasDat.</returns>
    public ReadOnlyCollection<BlightedSporeAurasDat> GetBlightedSporeAurasDat()
    {
        return BlightedSporeAurasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightEncounterTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightEncounterTypesDat.</returns>
    public ReadOnlyCollection<BlightEncounterTypesDat> GetBlightEncounterTypesDat()
    {
        return BlightEncounterTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightEncounterWavesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightEncounterWavesDat.</returns>
    public ReadOnlyCollection<BlightEncounterWavesDat> GetBlightEncounterWavesDat()
    {
        return BlightEncounterWavesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightRewardTypesDat.</returns>
    public ReadOnlyCollection<BlightRewardTypesDat> GetBlightRewardTypesDat()
    {
        return BlightRewardTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightTopologiesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightTopologiesDat.</returns>
    public ReadOnlyCollection<BlightTopologiesDat> GetBlightTopologiesDat()
    {
        return BlightTopologiesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightTopologyNodesDat data.
    /// </summary>
    /// <returns>readonly collection of BlightTopologyNodesDat.</returns>
    public ReadOnlyCollection<BlightTopologyNodesDat> GetBlightTopologyNodesDat()
    {
        return BlightTopologyNodesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightTowerAurasDat data.
    /// </summary>
    /// <returns>readonly collection of BlightTowerAurasDat.</returns>
    public ReadOnlyCollection<BlightTowerAurasDat> GetBlightTowerAurasDat()
    {
        return BlightTowerAurasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightTowersDat data.
    /// </summary>
    /// <returns>readonly collection of BlightTowersDat.</returns>
    public ReadOnlyCollection<BlightTowersDat> GetBlightTowersDat()
    {
        return BlightTowersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightTowersPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of BlightTowersPerLevelDat.</returns>
    public ReadOnlyCollection<BlightTowersPerLevelDat> GetBlightTowersPerLevelDat()
    {
        return BlightTowersPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasExileBossArenasDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasExileBossArenasDat.</returns>
    public ReadOnlyCollection<AtlasExileBossArenasDat> GetAtlasExileBossArenasDat()
    {
        return AtlasExileBossArenasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasExileInfluenceDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasExileInfluenceDat.</returns>
    public ReadOnlyCollection<AtlasExileInfluenceDat> GetAtlasExileInfluenceDat()
    {
        return AtlasExileInfluenceDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasExilesDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasExilesDat.</returns>
    public ReadOnlyCollection<AtlasExilesDat> GetAtlasExilesDat()
    {
        return AtlasExilesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AlternateQualityCurrencyDecayFactorsDat data.
    /// </summary>
    /// <returns>readonly collection of AlternateQualityCurrencyDecayFactorsDat.</returns>
    public ReadOnlyCollection<AlternateQualityCurrencyDecayFactorsDat> GetAlternateQualityCurrencyDecayFactorsDat()
    {
        return AlternateQualityCurrencyDecayFactorsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AlternateQualityTypesDat data.
    /// </summary>
    /// <returns>readonly collection of AlternateQualityTypesDat.</returns>
    public ReadOnlyCollection<AlternateQualityTypesDat> GetAlternateQualityTypesDat()
    {
        return AlternateQualityTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MetamorphLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<MetamorphLifeScalingPerLevelDat> GetMetamorphLifeScalingPerLevelDat()
    {
        return MetamorphLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MetamorphosisMetaMonstersDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisMetaMonstersDat.</returns>
    public ReadOnlyCollection<MetamorphosisMetaMonstersDat> GetMetamorphosisMetaMonstersDat()
    {
        return MetamorphosisMetaMonstersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MetamorphosisMetaSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisMetaSkillsDat.</returns>
    public ReadOnlyCollection<MetamorphosisMetaSkillsDat> GetMetamorphosisMetaSkillsDat()
    {
        return MetamorphosisMetaSkillsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MetamorphosisMetaSkillTypesDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisMetaSkillTypesDat.</returns>
    public ReadOnlyCollection<MetamorphosisMetaSkillTypesDat> GetMetamorphosisMetaSkillTypesDat()
    {
        return MetamorphosisMetaSkillTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MetamorphosisRewardTypeItemsClientDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisRewardTypeItemsClientDat.</returns>
    public ReadOnlyCollection<MetamorphosisRewardTypeItemsClientDat> GetMetamorphosisRewardTypeItemsClientDat()
    {
        return MetamorphosisRewardTypeItemsClientDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MetamorphosisRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisRewardTypesDat.</returns>
    public ReadOnlyCollection<MetamorphosisRewardTypesDat> GetMetamorphosisRewardTypesDat()
    {
        return MetamorphosisRewardTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MetamorphosisScalingDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisScalingDat.</returns>
    public ReadOnlyCollection<MetamorphosisScalingDat> GetMetamorphosisScalingDat()
    {
        return MetamorphosisScalingDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AfflictionBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionBalancePerLevelDat.</returns>
    public ReadOnlyCollection<AfflictionBalancePerLevelDat> GetAfflictionBalancePerLevelDat()
    {
        return AfflictionBalancePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AfflictionEndgameWaveModsDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionEndgameWaveModsDat.</returns>
    public ReadOnlyCollection<AfflictionEndgameWaveModsDat> GetAfflictionEndgameWaveModsDat()
    {
        return AfflictionEndgameWaveModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AfflictionFixedModsDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionFixedModsDat.</returns>
    public ReadOnlyCollection<AfflictionFixedModsDat> GetAfflictionFixedModsDat()
    {
        return AfflictionFixedModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AfflictionRandomModCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionRandomModCategoriesDat.</returns>
    public ReadOnlyCollection<AfflictionRandomModCategoriesDat> GetAfflictionRandomModCategoriesDat()
    {
        return AfflictionRandomModCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AfflictionRewardMapModsDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionRewardMapModsDat.</returns>
    public ReadOnlyCollection<AfflictionRewardMapModsDat> GetAfflictionRewardMapModsDat()
    {
        return AfflictionRewardMapModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AfflictionRewardTypeVisualsDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionRewardTypeVisualsDat.</returns>
    public ReadOnlyCollection<AfflictionRewardTypeVisualsDat> GetAfflictionRewardTypeVisualsDat()
    {
        return AfflictionRewardTypeVisualsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AfflictionSplitDemonsDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionSplitDemonsDat.</returns>
    public ReadOnlyCollection<AfflictionSplitDemonsDat> GetAfflictionSplitDemonsDat()
    {
        return AfflictionSplitDemonsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AfflictionStartDialogueDat data.
    /// </summary>
    /// <returns>readonly collection of AfflictionStartDialogueDat.</returns>
    public ReadOnlyCollection<AfflictionStartDialogueDat> GetAfflictionStartDialogueDat()
    {
        return AfflictionStartDialogueDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestCraftOptionIconsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestCraftOptionIconsDat.</returns>
    public ReadOnlyCollection<HarvestCraftOptionIconsDat> GetHarvestCraftOptionIconsDat()
    {
        return HarvestCraftOptionIconsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestCraftOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestCraftOptionsDat.</returns>
    public ReadOnlyCollection<HarvestCraftOptionsDat> GetHarvestCraftOptionsDat()
    {
        return HarvestCraftOptionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestCraftTiersDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestCraftTiersDat.</returns>
    public ReadOnlyCollection<HarvestCraftTiersDat> GetHarvestCraftTiersDat()
    {
        return HarvestCraftTiersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestCraftFiltersDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestCraftFiltersDat.</returns>
    public ReadOnlyCollection<HarvestCraftFiltersDat> GetHarvestCraftFiltersDat()
    {
        return HarvestCraftFiltersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestDurabilityDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestDurabilityDat.</returns>
    public ReadOnlyCollection<HarvestDurabilityDat> GetHarvestDurabilityDat()
    {
        return HarvestDurabilityDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestEncounterScalingDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestEncounterScalingDat.</returns>
    public ReadOnlyCollection<HarvestEncounterScalingDat> GetHarvestEncounterScalingDat()
    {
        return HarvestEncounterScalingDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestInfrastructureDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestInfrastructureDat.</returns>
    public ReadOnlyCollection<HarvestInfrastructureDat> GetHarvestInfrastructureDat()
    {
        return HarvestInfrastructureDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestObjectsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestObjectsDat.</returns>
    public ReadOnlyCollection<HarvestObjectsDat> GetHarvestObjectsDat()
    {
        return HarvestObjectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestPerLevelValuesDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestPerLevelValuesDat.</returns>
    public ReadOnlyCollection<HarvestPerLevelValuesDat> GetHarvestPerLevelValuesDat()
    {
        return HarvestPerLevelValuesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestPlantBoostersDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestPlantBoostersDat.</returns>
    public ReadOnlyCollection<HarvestPlantBoostersDat> GetHarvestPlantBoostersDat()
    {
        return HarvestPlantBoostersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<HarvestLifeScalingPerLevelDat> GetHarvestLifeScalingPerLevelDat()
    {
        return HarvestLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestSeedsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestSeedsDat.</returns>
    public ReadOnlyCollection<HarvestSeedsDat> GetHarvestSeedsDat()
    {
        return HarvestSeedsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestSeedItemsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestSeedItemsDat.</returns>
    public ReadOnlyCollection<HarvestSeedItemsDat> GetHarvestSeedItemsDat()
    {
        return HarvestSeedItemsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestSeedTypesDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestSeedTypesDat.</returns>
    public ReadOnlyCollection<HarvestSeedTypesDat> GetHarvestSeedTypesDat()
    {
        return HarvestSeedTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestSpecialCraftCostsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestSpecialCraftCostsDat.</returns>
    public ReadOnlyCollection<HarvestSpecialCraftCostsDat> GetHarvestSpecialCraftCostsDat()
    {
        return HarvestSpecialCraftCostsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestSpecialCraftOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestSpecialCraftOptionsDat.</returns>
    public ReadOnlyCollection<HarvestSpecialCraftOptionsDat> GetHarvestSpecialCraftOptionsDat()
    {
        return HarvestSpecialCraftOptionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistAreaFormationLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of HeistAreaFormationLayoutDat.</returns>
    public ReadOnlyCollection<HeistAreaFormationLayoutDat> GetHeistAreaFormationLayoutDat()
    {
        return HeistAreaFormationLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistAreasDat data.
    /// </summary>
    /// <returns>readonly collection of HeistAreasDat.</returns>
    public ReadOnlyCollection<HeistAreasDat> GetHeistAreasDat()
    {
        return HeistAreasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of HeistBalancePerLevelDat.</returns>
    public ReadOnlyCollection<HeistBalancePerLevelDat> GetHeistBalancePerLevelDat()
    {
        return HeistBalancePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistChestRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of HeistChestRewardTypesDat.</returns>
    public ReadOnlyCollection<HeistChestRewardTypesDat> GetHeistChestRewardTypesDat()
    {
        return HeistChestRewardTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistChestsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistChestsDat.</returns>
    public ReadOnlyCollection<HeistChestsDat> GetHeistChestsDat()
    {
        return HeistChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistChokepointFormationDat data.
    /// </summary>
    /// <returns>readonly collection of HeistChokepointFormationDat.</returns>
    public ReadOnlyCollection<HeistChokepointFormationDat> GetHeistChokepointFormationDat()
    {
        return HeistChokepointFormationDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistConstantsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistConstantsDat.</returns>
    public ReadOnlyCollection<HeistConstantsDat> GetHeistConstantsDat()
    {
        return HeistConstantsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistContractsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistContractsDat.</returns>
    public ReadOnlyCollection<HeistContractsDat> GetHeistContractsDat()
    {
        return HeistContractsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistDoodadNPCsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistDoodadNPCsDat.</returns>
    public ReadOnlyCollection<HeistDoodadNPCsDat> GetHeistDoodadNPCsDat()
    {
        return HeistDoodadNPCsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistDoorsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistDoorsDat.</returns>
    public ReadOnlyCollection<HeistDoorsDat> GetHeistDoorsDat()
    {
        return HeistDoorsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistEquipmentDat data.
    /// </summary>
    /// <returns>readonly collection of HeistEquipmentDat.</returns>
    public ReadOnlyCollection<HeistEquipmentDat> GetHeistEquipmentDat()
    {
        return HeistEquipmentDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistGenerationDat data.
    /// </summary>
    /// <returns>readonly collection of HeistGenerationDat.</returns>
    public ReadOnlyCollection<HeistGenerationDat> GetHeistGenerationDat()
    {
        return HeistGenerationDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistIntroAreasDat data.
    /// </summary>
    /// <returns>readonly collection of HeistIntroAreasDat.</returns>
    public ReadOnlyCollection<HeistIntroAreasDat> GetHeistIntroAreasDat()
    {
        return HeistIntroAreasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistJobsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistJobsDat.</returns>
    public ReadOnlyCollection<HeistJobsDat> GetHeistJobsDat()
    {
        return HeistJobsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistJobsExperiencePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of HeistJobsExperiencePerLevelDat.</returns>
    public ReadOnlyCollection<HeistJobsExperiencePerLevelDat> GetHeistJobsExperiencePerLevelDat()
    {
        return HeistJobsExperiencePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistLockTypeDat data.
    /// </summary>
    /// <returns>readonly collection of HeistLockTypeDat.</returns>
    public ReadOnlyCollection<HeistLockTypeDat> GetHeistLockTypeDat()
    {
        return HeistLockTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistNPCAurasDat data.
    /// </summary>
    /// <returns>readonly collection of HeistNPCAurasDat.</returns>
    public ReadOnlyCollection<HeistNPCAurasDat> GetHeistNPCAurasDat()
    {
        return HeistNPCAurasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistNPCBlueprintTypesDat data.
    /// </summary>
    /// <returns>readonly collection of HeistNPCBlueprintTypesDat.</returns>
    public ReadOnlyCollection<HeistNPCBlueprintTypesDat> GetHeistNPCBlueprintTypesDat()
    {
        return HeistNPCBlueprintTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistNPCDialogueDat data.
    /// </summary>
    /// <returns>readonly collection of HeistNPCDialogueDat.</returns>
    public ReadOnlyCollection<HeistNPCDialogueDat> GetHeistNPCDialogueDat()
    {
        return HeistNPCDialogueDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistNPCsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistNPCsDat.</returns>
    public ReadOnlyCollection<HeistNPCsDat> GetHeistNPCsDat()
    {
        return HeistNPCsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistNPCStatsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistNPCStatsDat.</returns>
    public ReadOnlyCollection<HeistNPCStatsDat> GetHeistNPCStatsDat()
    {
        return HeistNPCStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistObjectivesDat data.
    /// </summary>
    /// <returns>readonly collection of HeistObjectivesDat.</returns>
    public ReadOnlyCollection<HeistObjectivesDat> GetHeistObjectivesDat()
    {
        return HeistObjectivesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistObjectiveValueDescriptionsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistObjectiveValueDescriptionsDat.</returns>
    public ReadOnlyCollection<HeistObjectiveValueDescriptionsDat> GetHeistObjectiveValueDescriptionsDat()
    {
        return HeistObjectiveValueDescriptionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistPatrolPacksDat data.
    /// </summary>
    /// <returns>readonly collection of HeistPatrolPacksDat.</returns>
    public ReadOnlyCollection<HeistPatrolPacksDat> GetHeistPatrolPacksDat()
    {
        return HeistPatrolPacksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistQuestContractsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistQuestContractsDat.</returns>
    public ReadOnlyCollection<HeistQuestContractsDat> GetHeistQuestContractsDat()
    {
        return HeistQuestContractsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistRevealingNPCsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistRevealingNPCsDat.</returns>
    public ReadOnlyCollection<HeistRevealingNPCsDat> GetHeistRevealingNPCsDat()
    {
        return HeistRevealingNPCsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistRoomsDat data.
    /// </summary>
    /// <returns>readonly collection of HeistRoomsDat.</returns>
    public ReadOnlyCollection<HeistRoomsDat> GetHeistRoomsDat()
    {
        return HeistRoomsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistValueScalingDat data.
    /// </summary>
    /// <returns>readonly collection of HeistValueScalingDat.</returns>
    public ReadOnlyCollection<HeistValueScalingDat> GetHeistValueScalingDat()
    {
        return HeistValueScalingDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets InfluenceModUpgradesDat data.
    /// </summary>
    /// <returns>readonly collection of InfluenceModUpgradesDat.</returns>
    public ReadOnlyCollection<InfluenceModUpgradesDat> GetInfluenceModUpgradesDat()
    {
        return InfluenceModUpgradesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MavenDialogDat data.
    /// </summary>
    /// <returns>readonly collection of MavenDialogDat.</returns>
    public ReadOnlyCollection<MavenDialogDat> GetMavenDialogDat()
    {
        return MavenDialogDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasSkillGraphsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasSkillGraphsDat.</returns>
    public ReadOnlyCollection<AtlasSkillGraphsDat> GetAtlasSkillGraphsDat()
    {
        return AtlasSkillGraphsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MavenFightsDat data.
    /// </summary>
    /// <returns>readonly collection of MavenFightsDat.</returns>
    public ReadOnlyCollection<MavenFightsDat> GetMavenFightsDat()
    {
        return MavenFightsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MavenJewelRadiusKeystonesDat data.
    /// </summary>
    /// <returns>readonly collection of MavenJewelRadiusKeystonesDat.</returns>
    public ReadOnlyCollection<MavenJewelRadiusKeystonesDat> GetMavenJewelRadiusKeystonesDat()
    {
        return MavenJewelRadiusKeystonesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RitualBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of RitualBalancePerLevelDat.</returns>
    public ReadOnlyCollection<RitualBalancePerLevelDat> GetRitualBalancePerLevelDat()
    {
        return RitualBalancePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RitualConstantsDat data.
    /// </summary>
    /// <returns>readonly collection of RitualConstantsDat.</returns>
    public ReadOnlyCollection<RitualConstantsDat> GetRitualConstantsDat()
    {
        return RitualConstantsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RitualRuneTypesDat data.
    /// </summary>
    /// <returns>readonly collection of RitualRuneTypesDat.</returns>
    public ReadOnlyCollection<RitualRuneTypesDat> GetRitualRuneTypesDat()
    {
        return RitualRuneTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RitualSetKillAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of RitualSetKillAchievementsDat.</returns>
    public ReadOnlyCollection<RitualSetKillAchievementsDat> GetRitualSetKillAchievementsDat()
    {
        return RitualSetKillAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RitualSpawnPatternsDat data.
    /// </summary>
    /// <returns>readonly collection of RitualSpawnPatternsDat.</returns>
    public ReadOnlyCollection<RitualSpawnPatternsDat> GetRitualSpawnPatternsDat()
    {
        return RitualSpawnPatternsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UltimatumEncountersDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumEncountersDat.</returns>
    public ReadOnlyCollection<UltimatumEncountersDat> GetUltimatumEncountersDat()
    {
        return UltimatumEncountersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UltimatumEncounterTypesDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumEncounterTypesDat.</returns>
    public ReadOnlyCollection<UltimatumEncounterTypesDat> GetUltimatumEncounterTypesDat()
    {
        return UltimatumEncounterTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UltimatumItemisedRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumItemisedRewardsDat.</returns>
    public ReadOnlyCollection<UltimatumItemisedRewardsDat> GetUltimatumItemisedRewardsDat()
    {
        return UltimatumItemisedRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UltimatumMapModifiersDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumMapModifiersDat.</returns>
    public ReadOnlyCollection<UltimatumMapModifiersDat> GetUltimatumMapModifiersDat()
    {
        return UltimatumMapModifiersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UltimatumModifiersDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumModifiersDat.</returns>
    public ReadOnlyCollection<UltimatumModifiersDat> GetUltimatumModifiersDat()
    {
        return UltimatumModifiersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UltimatumModifierTypesDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumModifierTypesDat.</returns>
    public ReadOnlyCollection<UltimatumModifierTypesDat> GetUltimatumModifierTypesDat()
    {
        return UltimatumModifierTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UltimatumTrialMasterAudioDat data.
    /// </summary>
    /// <returns>readonly collection of UltimatumTrialMasterAudioDat.</returns>
    public ReadOnlyCollection<UltimatumTrialMasterAudioDat> GetUltimatumTrialMasterAudioDat()
    {
        return UltimatumTrialMasterAudioDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionAreasDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionAreasDat.</returns>
    public ReadOnlyCollection<ExpeditionAreasDat> GetExpeditionAreasDat()
    {
        return ExpeditionAreasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionBalancePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionBalancePerLevelDat.</returns>
    public ReadOnlyCollection<ExpeditionBalancePerLevelDat> GetExpeditionBalancePerLevelDat()
    {
        return ExpeditionBalancePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionCurrencyDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionCurrencyDat.</returns>
    public ReadOnlyCollection<ExpeditionCurrencyDat> GetExpeditionCurrencyDat()
    {
        return ExpeditionCurrencyDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionDealsDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionDealsDat.</returns>
    public ReadOnlyCollection<ExpeditionDealsDat> GetExpeditionDealsDat()
    {
        return ExpeditionDealsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionFactionsDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionFactionsDat.</returns>
    public ReadOnlyCollection<ExpeditionFactionsDat> GetExpeditionFactionsDat()
    {
        return ExpeditionFactionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionMarkersCommonDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionMarkersCommonDat.</returns>
    public ReadOnlyCollection<ExpeditionMarkersCommonDat> GetExpeditionMarkersCommonDat()
    {
        return ExpeditionMarkersCommonDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionNPCsDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionNPCsDat.</returns>
    public ReadOnlyCollection<ExpeditionNPCsDat> GetExpeditionNPCsDat()
    {
        return ExpeditionNPCsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionRelicModsDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionRelicModsDat.</returns>
    public ReadOnlyCollection<ExpeditionRelicModsDat> GetExpeditionRelicModsDat()
    {
        return ExpeditionRelicModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionRelicsDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionRelicsDat.</returns>
    public ReadOnlyCollection<ExpeditionRelicsDat> GetExpeditionRelicsDat()
    {
        return ExpeditionRelicsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionStorageLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionStorageLayoutDat.</returns>
    public ReadOnlyCollection<ExpeditionStorageLayoutDat> GetExpeditionStorageLayoutDat()
    {
        return ExpeditionStorageLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpeditionTerrainFeaturesDat data.
    /// </summary>
    /// <returns>readonly collection of ExpeditionTerrainFeaturesDat.</returns>
    public ReadOnlyCollection<ExpeditionTerrainFeaturesDat> GetExpeditionTerrainFeaturesDat()
    {
        return ExpeditionTerrainFeaturesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeAOReplacementsDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeAOReplacementsDat.</returns>
    public ReadOnlyCollection<HellscapeAOReplacementsDat> GetHellscapeAOReplacementsDat()
    {
        return HellscapeAOReplacementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeAreaPacksDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeAreaPacksDat.</returns>
    public ReadOnlyCollection<HellscapeAreaPacksDat> GetHellscapeAreaPacksDat()
    {
        return HellscapeAreaPacksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeExperienceLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeExperienceLevelsDat.</returns>
    public ReadOnlyCollection<HellscapeExperienceLevelsDat> GetHellscapeExperienceLevelsDat()
    {
        return HellscapeExperienceLevelsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeFactionsDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeFactionsDat.</returns>
    public ReadOnlyCollection<HellscapeFactionsDat> GetHellscapeFactionsDat()
    {
        return HellscapeFactionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeImmuneMonstersDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeImmuneMonstersDat.</returns>
    public ReadOnlyCollection<HellscapeImmuneMonstersDat> GetHellscapeImmuneMonstersDat()
    {
        return HellscapeImmuneMonstersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeItemModificationTiersDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeItemModificationTiersDat.</returns>
    public ReadOnlyCollection<HellscapeItemModificationTiersDat> GetHellscapeItemModificationTiersDat()
    {
        return HellscapeItemModificationTiersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<HellscapeLifeScalingPerLevelDat> GetHellscapeLifeScalingPerLevelDat()
    {
        return HellscapeLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeModificationInventoryLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeModificationInventoryLayoutDat.</returns>
    public ReadOnlyCollection<HellscapeModificationInventoryLayoutDat> GetHellscapeModificationInventoryLayoutDat()
    {
        return HellscapeModificationInventoryLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeModsDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeModsDat.</returns>
    public ReadOnlyCollection<HellscapeModsDat> GetHellscapeModsDat()
    {
        return HellscapeModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapeMonsterPacksDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapeMonsterPacksDat.</returns>
    public ReadOnlyCollection<HellscapeMonsterPacksDat> GetHellscapeMonsterPacksDat()
    {
        return HellscapeMonsterPacksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapePassivesDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapePassivesDat.</returns>
    public ReadOnlyCollection<HellscapePassivesDat> GetHellscapePassivesDat()
    {
        return HellscapePassivesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HellscapePassiveTreeDat data.
    /// </summary>
    /// <returns>readonly collection of HellscapePassiveTreeDat.</returns>
    public ReadOnlyCollection<HellscapePassiveTreeDat> GetHellscapePassiveTreeDat()
    {
        return HellscapePassiveTreeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArchnemesisMetaRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of ArchnemesisMetaRewardsDat.</returns>
    public ReadOnlyCollection<ArchnemesisMetaRewardsDat> GetArchnemesisMetaRewardsDat()
    {
        return ArchnemesisMetaRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArchnemesisModComboAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of ArchnemesisModComboAchievementsDat.</returns>
    public ReadOnlyCollection<ArchnemesisModComboAchievementsDat> GetArchnemesisModComboAchievementsDat()
    {
        return ArchnemesisModComboAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArchnemesisModsDat data.
    /// </summary>
    /// <returns>readonly collection of ArchnemesisModsDat.</returns>
    public ReadOnlyCollection<ArchnemesisModsDat> GetArchnemesisModsDat()
    {
        return ArchnemesisModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArchnemesisModVisualsDat data.
    /// </summary>
    /// <returns>readonly collection of ArchnemesisModVisualsDat.</returns>
    public ReadOnlyCollection<ArchnemesisModVisualsDat> GetArchnemesisModVisualsDat()
    {
        return ArchnemesisModVisualsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArchnemesisRecipesDat data.
    /// </summary>
    /// <returns>readonly collection of ArchnemesisRecipesDat.</returns>
    public ReadOnlyCollection<ArchnemesisRecipesDat> GetArchnemesisRecipesDat()
    {
        return ArchnemesisRecipesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasPrimordialAltarChoicesDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPrimordialAltarChoicesDat.</returns>
    public ReadOnlyCollection<AtlasPrimordialAltarChoicesDat> GetAtlasPrimordialAltarChoicesDat()
    {
        return AtlasPrimordialAltarChoicesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasPrimordialAltarChoiceTypesDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPrimordialAltarChoiceTypesDat.</returns>
    public ReadOnlyCollection<AtlasPrimordialAltarChoiceTypesDat> GetAtlasPrimordialAltarChoiceTypesDat()
    {
        return AtlasPrimordialAltarChoiceTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasPrimordialBossesDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPrimordialBossesDat.</returns>
    public ReadOnlyCollection<AtlasPrimordialBossesDat> GetAtlasPrimordialBossesDat()
    {
        return AtlasPrimordialBossesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasPrimordialBossInfluenceDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPrimordialBossInfluenceDat.</returns>
    public ReadOnlyCollection<AtlasPrimordialBossInfluenceDat> GetAtlasPrimordialBossInfluenceDat()
    {
        return AtlasPrimordialBossInfluenceDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasPrimordialBossOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPrimordialBossOptionsDat.</returns>
    public ReadOnlyCollection<AtlasPrimordialBossOptionsDat> GetAtlasPrimordialBossOptionsDat()
    {
        return AtlasPrimordialBossOptionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PrimordialBossLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of PrimordialBossLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<PrimordialBossLifeScalingPerLevelDat> GetPrimordialBossLifeScalingPerLevelDat()
    {
        return PrimordialBossLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasUpgradesInventoryLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasUpgradesInventoryLayoutDat.</returns>
    public ReadOnlyCollection<AtlasUpgradesInventoryLayoutDat> GetAtlasUpgradesInventoryLayoutDat()
    {
        return AtlasUpgradesInventoryLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasPassiveSkillTreeGroupTypeDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPassiveSkillTreeGroupTypeDat.</returns>
    public ReadOnlyCollection<AtlasPassiveSkillTreeGroupTypeDat> GetAtlasPassiveSkillTreeGroupTypeDat()
    {
        return AtlasPassiveSkillTreeGroupTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets KiracLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of KiracLevelsDat.</returns>
    public ReadOnlyCollection<KiracLevelsDat> GetKiracLevelsDat()
    {
        return KiracLevelsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ScoutingReportsDat data.
    /// </summary>
    /// <returns>readonly collection of ScoutingReportsDat.</returns>
    public ReadOnlyCollection<ScoutingReportsDat> GetScoutingReportsDat()
    {
        return ScoutingReportsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DroneBaseTypesDat data.
    /// </summary>
    /// <returns>readonly collection of DroneBaseTypesDat.</returns>
    public ReadOnlyCollection<DroneBaseTypesDat> GetDroneBaseTypesDat()
    {
        return DroneBaseTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DroneTypesDat data.
    /// </summary>
    /// <returns>readonly collection of DroneTypesDat.</returns>
    public ReadOnlyCollection<DroneTypesDat> GetDroneTypesDat()
    {
        return DroneTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SentinelCraftingCurrencyDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelCraftingCurrencyDat.</returns>
    public ReadOnlyCollection<SentinelCraftingCurrencyDat> GetSentinelCraftingCurrencyDat()
    {
        return SentinelCraftingCurrencyDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SentinelDroneInventoryLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelDroneInventoryLayoutDat.</returns>
    public ReadOnlyCollection<SentinelDroneInventoryLayoutDat> GetSentinelDroneInventoryLayoutDat()
    {
        return SentinelDroneInventoryLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SentinelPassivesDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelPassivesDat.</returns>
    public ReadOnlyCollection<SentinelPassivesDat> GetSentinelPassivesDat()
    {
        return SentinelPassivesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SentinelPassiveStatsDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelPassiveStatsDat.</returns>
    public ReadOnlyCollection<SentinelPassiveStatsDat> GetSentinelPassiveStatsDat()
    {
        return SentinelPassiveStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SentinelPassiveTypesDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelPassiveTypesDat.</returns>
    public ReadOnlyCollection<SentinelPassiveTypesDat> GetSentinelPassiveTypesDat()
    {
        return SentinelPassiveTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SentinelPowerExpLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelPowerExpLevelsDat.</returns>
    public ReadOnlyCollection<SentinelPowerExpLevelsDat> GetSentinelPowerExpLevelsDat()
    {
        return SentinelPowerExpLevelsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SentinelStorageLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelStorageLayoutDat.</returns>
    public ReadOnlyCollection<SentinelStorageLayoutDat> GetSentinelStorageLayoutDat()
    {
        return SentinelStorageLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SentinelTaggedMonsterStatsDat data.
    /// </summary>
    /// <returns>readonly collection of SentinelTaggedMonsterStatsDat.</returns>
    public ReadOnlyCollection<SentinelTaggedMonsterStatsDat> GetSentinelTaggedMonsterStatsDat()
    {
        return SentinelTaggedMonsterStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ClientLakeDifficultyDat data.
    /// </summary>
    /// <returns>readonly collection of ClientLakeDifficultyDat.</returns>
    public ReadOnlyCollection<ClientLakeDifficultyDat> GetClientLakeDifficultyDat()
    {
        return ClientLakeDifficultyDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LakeBossLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of LakeBossLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<LakeBossLifeScalingPerLevelDat> GetLakeBossLifeScalingPerLevelDat()
    {
        return LakeBossLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LakeMetaOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of LakeMetaOptionsDat.</returns>
    public ReadOnlyCollection<LakeMetaOptionsDat> GetLakeMetaOptionsDat()
    {
        return LakeMetaOptionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LakeMetaOptionsUnlockTextDat data.
    /// </summary>
    /// <returns>readonly collection of LakeMetaOptionsUnlockTextDat.</returns>
    public ReadOnlyCollection<LakeMetaOptionsUnlockTextDat> GetLakeMetaOptionsUnlockTextDat()
    {
        return LakeMetaOptionsUnlockTextDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LakeRoomCompletionDat data.
    /// </summary>
    /// <returns>readonly collection of LakeRoomCompletionDat.</returns>
    public ReadOnlyCollection<LakeRoomCompletionDat> GetLakeRoomCompletionDat()
    {
        return LakeRoomCompletionDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LakeRoomsDat data.
    /// </summary>
    /// <returns>readonly collection of LakeRoomsDat.</returns>
    public ReadOnlyCollection<LakeRoomsDat> GetLakeRoomsDat()
    {
        return LakeRoomsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AchievementItemRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of AchievementItemRewardsDat.</returns>
    public ReadOnlyCollection<AchievementItemRewardsDat> GetAchievementItemRewardsDat()
    {
        return AchievementItemRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AchievementItemsDat data.
    /// </summary>
    /// <returns>readonly collection of AchievementItemsDat.</returns>
    public ReadOnlyCollection<AchievementItemsDat> GetAchievementItemsDat()
    {
        return AchievementItemsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of AchievementsDat.</returns>
    public ReadOnlyCollection<AchievementsDat> GetAchievementsDat()
    {
        return AchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AchievementSetRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of AchievementSetRewardsDat.</returns>
    public ReadOnlyCollection<AchievementSetRewardsDat> GetAchievementSetRewardsDat()
    {
        return AchievementSetRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AchievementSetsDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of AchievementSetsDisplayDat.</returns>
    public ReadOnlyCollection<AchievementSetsDisplayDat> GetAchievementSetsDisplayDat()
    {
        return AchievementSetsDisplayDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ActiveSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of ActiveSkillsDat.</returns>
    public ReadOnlyCollection<ActiveSkillsDat> GetActiveSkillsDat()
    {
        return ActiveSkillsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ActiveSkillTypeDat data.
    /// </summary>
    /// <returns>readonly collection of ActiveSkillTypeDat.</returns>
    public ReadOnlyCollection<ActiveSkillTypeDat> GetActiveSkillTypeDat()
    {
        return ActiveSkillTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ActsDat data.
    /// </summary>
    /// <returns>readonly collection of ActsDat.</returns>
    public ReadOnlyCollection<ActsDat> GetActsDat()
    {
        return ActsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AddBuffToTargetVarietiesDat data.
    /// </summary>
    /// <returns>readonly collection of AddBuffToTargetVarietiesDat.</returns>
    public ReadOnlyCollection<AddBuffToTargetVarietiesDat> GetAddBuffToTargetVarietiesDat()
    {
        return AddBuffToTargetVarietiesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AdditionalLifeScalingDat data.
    /// </summary>
    /// <returns>readonly collection of AdditionalLifeScalingDat.</returns>
    public ReadOnlyCollection<AdditionalLifeScalingDat> GetAdditionalLifeScalingDat()
    {
        return AdditionalLifeScalingDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AdditionalMonsterPacksFromStatsDat data.
    /// </summary>
    /// <returns>readonly collection of AdditionalMonsterPacksFromStatsDat.</returns>
    public ReadOnlyCollection<AdditionalMonsterPacksFromStatsDat> GetAdditionalMonsterPacksFromStatsDat()
    {
        return AdditionalMonsterPacksFromStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AdvancedSkillsTutorialDat data.
    /// </summary>
    /// <returns>readonly collection of AdvancedSkillsTutorialDat.</returns>
    public ReadOnlyCollection<AdvancedSkillsTutorialDat> GetAdvancedSkillsTutorialDat()
    {
        return AdvancedSkillsTutorialDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AegisVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of AegisVariationsDat.</returns>
    public ReadOnlyCollection<AegisVariationsDat> GetAegisVariationsDat()
    {
        return AegisVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AlternatePassiveAdditionsDat data.
    /// </summary>
    /// <returns>readonly collection of AlternatePassiveAdditionsDat.</returns>
    public ReadOnlyCollection<AlternatePassiveAdditionsDat> GetAlternatePassiveAdditionsDat()
    {
        return AlternatePassiveAdditionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AlternatePassiveSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of AlternatePassiveSkillsDat.</returns>
    public ReadOnlyCollection<AlternatePassiveSkillsDat> GetAlternatePassiveSkillsDat()
    {
        return AlternatePassiveSkillsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AlternateSkillTargetingBehavioursDat data.
    /// </summary>
    /// <returns>readonly collection of AlternateSkillTargetingBehavioursDat.</returns>
    public ReadOnlyCollection<AlternateSkillTargetingBehavioursDat> GetAlternateSkillTargetingBehavioursDat()
    {
        return AlternateSkillTargetingBehavioursDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AlternateTreeVersionsDat data.
    /// </summary>
    /// <returns>readonly collection of AlternateTreeVersionsDat.</returns>
    public ReadOnlyCollection<AlternateTreeVersionsDat> GetAlternateTreeVersionsDat()
    {
        return AlternateTreeVersionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AnimatedObjectFlagsDat data.
    /// </summary>
    /// <returns>readonly collection of AnimatedObjectFlagsDat.</returns>
    public ReadOnlyCollection<AnimatedObjectFlagsDat> GetAnimatedObjectFlagsDat()
    {
        return AnimatedObjectFlagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AnimationDat data.
    /// </summary>
    /// <returns>readonly collection of AnimationDat.</returns>
    public ReadOnlyCollection<AnimationDat> GetAnimationDat()
    {
        return AnimationDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ApplyDamageFunctionsDat data.
    /// </summary>
    /// <returns>readonly collection of ApplyDamageFunctionsDat.</returns>
    public ReadOnlyCollection<ApplyDamageFunctionsDat> GetApplyDamageFunctionsDat()
    {
        return ApplyDamageFunctionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArchetypeRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of ArchetypeRewardsDat.</returns>
    public ReadOnlyCollection<ArchetypeRewardsDat> GetArchetypeRewardsDat()
    {
        return ArchetypeRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArchetypesDat data.
    /// </summary>
    /// <returns>readonly collection of ArchetypesDat.</returns>
    public ReadOnlyCollection<ArchetypesDat> GetArchetypesDat()
    {
        return ArchetypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AreaInfluenceDoodadsDat data.
    /// </summary>
    /// <returns>readonly collection of AreaInfluenceDoodadsDat.</returns>
    public ReadOnlyCollection<AreaInfluenceDoodadsDat> GetAreaInfluenceDoodadsDat()
    {
        return AreaInfluenceDoodadsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AreaTransitionAnimationsDat data.
    /// </summary>
    /// <returns>readonly collection of AreaTransitionAnimationsDat.</returns>
    public ReadOnlyCollection<AreaTransitionAnimationsDat> GetAreaTransitionAnimationsDat()
    {
        return AreaTransitionAnimationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AreaTransitionAnimationTypesDat data.
    /// </summary>
    /// <returns>readonly collection of AreaTransitionAnimationTypesDat.</returns>
    public ReadOnlyCollection<AreaTransitionAnimationTypesDat> GetAreaTransitionAnimationTypesDat()
    {
        return AreaTransitionAnimationTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AreaTransitionInfoDat data.
    /// </summary>
    /// <returns>readonly collection of AreaTransitionInfoDat.</returns>
    public ReadOnlyCollection<AreaTransitionInfoDat> GetAreaTransitionInfoDat()
    {
        return AreaTransitionInfoDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ArmourTypesDat data.
    /// </summary>
    /// <returns>readonly collection of ArmourTypesDat.</returns>
    public ReadOnlyCollection<ArmourTypesDat> GetArmourTypesDat()
    {
        return ArmourTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AscendancyDat data.
    /// </summary>
    /// <returns>readonly collection of AscendancyDat.</returns>
    public ReadOnlyCollection<AscendancyDat> GetAscendancyDat()
    {
        return AscendancyDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasAwakeningStatsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasAwakeningStatsDat.</returns>
    public ReadOnlyCollection<AtlasAwakeningStatsDat> GetAtlasAwakeningStatsDat()
    {
        return AtlasAwakeningStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasBaseTypeDropsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasBaseTypeDropsDat.</returns>
    public ReadOnlyCollection<AtlasBaseTypeDropsDat> GetAtlasBaseTypeDropsDat()
    {
        return AtlasBaseTypeDropsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasFogDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasFogDat.</returns>
    public ReadOnlyCollection<AtlasFogDat> GetAtlasFogDat()
    {
        return AtlasFogDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasInfluenceDataDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasInfluenceDataDat.</returns>
    public ReadOnlyCollection<AtlasInfluenceDataDat> GetAtlasInfluenceDataDat()
    {
        return AtlasInfluenceDataDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasInfluenceOutcomesDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasInfluenceOutcomesDat.</returns>
    public ReadOnlyCollection<AtlasInfluenceOutcomesDat> GetAtlasInfluenceOutcomesDat()
    {
        return AtlasInfluenceOutcomesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasInfluenceSetsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasInfluenceSetsDat.</returns>
    public ReadOnlyCollection<AtlasInfluenceSetsDat> GetAtlasInfluenceSetsDat()
    {
        return AtlasInfluenceSetsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasModsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasModsDat.</returns>
    public ReadOnlyCollection<AtlasModsDat> GetAtlasModsDat()
    {
        return AtlasModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasFavouredMapSlotsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasFavouredMapSlotsDat.</returns>
    public ReadOnlyCollection<AtlasFavouredMapSlotsDat> GetAtlasFavouredMapSlotsDat()
    {
        return AtlasFavouredMapSlotsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasNodeDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasNodeDat.</returns>
    public ReadOnlyCollection<AtlasNodeDat> GetAtlasNodeDat()
    {
        return AtlasNodeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasNodeDefinitionDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasNodeDefinitionDat.</returns>
    public ReadOnlyCollection<AtlasNodeDefinitionDat> GetAtlasNodeDefinitionDat()
    {
        return AtlasNodeDefinitionDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasPositionsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasPositionsDat.</returns>
    public ReadOnlyCollection<AtlasPositionsDat> GetAtlasPositionsDat()
    {
        return AtlasPositionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasRegionsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasRegionsDat.</returns>
    public ReadOnlyCollection<AtlasRegionsDat> GetAtlasRegionsDat()
    {
        return AtlasRegionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasRegionUpgradesInventoryLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasRegionUpgradesInventoryLayoutDat.</returns>
    public ReadOnlyCollection<AtlasRegionUpgradesInventoryLayoutDat> GetAtlasRegionUpgradesInventoryLayoutDat()
    {
        return AtlasRegionUpgradesInventoryLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasRegionUpgradeRegionsDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasRegionUpgradeRegionsDat.</returns>
    public ReadOnlyCollection<AtlasRegionUpgradeRegionsDat> GetAtlasRegionUpgradeRegionsDat()
    {
        return AtlasRegionUpgradeRegionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AtlasSectorDat data.
    /// </summary>
    /// <returns>readonly collection of AtlasSectorDat.</returns>
    public ReadOnlyCollection<AtlasSectorDat> GetAtlasSectorDat()
    {
        return AtlasSectorDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets AwardDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of AwardDisplayDat.</returns>
    public ReadOnlyCollection<AwardDisplayDat> GetAwardDisplayDat()
    {
        return AwardDisplayDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BackendErrorsDat data.
    /// </summary>
    /// <returns>readonly collection of BackendErrorsDat.</returns>
    public ReadOnlyCollection<BackendErrorsDat> GetBackendErrorsDat()
    {
        return BackendErrorsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BaseItemTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BaseItemTypesDat.</returns>
    public ReadOnlyCollection<BaseItemTypesDat> GetBaseItemTypesDat()
    {
        return BaseItemTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BindableVirtualKeysDat data.
    /// </summary>
    /// <returns>readonly collection of BindableVirtualKeysDat.</returns>
    public ReadOnlyCollection<BindableVirtualKeysDat> GetBindableVirtualKeysDat()
    {
        return BindableVirtualKeysDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BlightStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of BlightStashTabLayoutDat.</returns>
    public ReadOnlyCollection<BlightStashTabLayoutDat> GetBlightStashTabLayoutDat()
    {
        return BlightStashTabLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BloodTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BloodTypesDat.</returns>
    public ReadOnlyCollection<BloodTypesDat> GetBloodTypesDat()
    {
        return BloodTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BuffDefinitionsDat data.
    /// </summary>
    /// <returns>readonly collection of BuffDefinitionsDat.</returns>
    public ReadOnlyCollection<BuffDefinitionsDat> GetBuffDefinitionsDat()
    {
        return BuffDefinitionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BuffTemplatesDat data.
    /// </summary>
    /// <returns>readonly collection of BuffTemplatesDat.</returns>
    public ReadOnlyCollection<BuffTemplatesDat> GetBuffTemplatesDat()
    {
        return BuffTemplatesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BuffVisualOrbArtDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualOrbArtDat.</returns>
    public ReadOnlyCollection<BuffVisualOrbArtDat> GetBuffVisualOrbArtDat()
    {
        return BuffVisualOrbArtDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BuffVisualOrbsDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualOrbsDat.</returns>
    public ReadOnlyCollection<BuffVisualOrbsDat> GetBuffVisualOrbsDat()
    {
        return BuffVisualOrbsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BuffVisualOrbTypesDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualOrbTypesDat.</returns>
    public ReadOnlyCollection<BuffVisualOrbTypesDat> GetBuffVisualOrbTypesDat()
    {
        return BuffVisualOrbTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BuffVisualsDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualsDat.</returns>
    public ReadOnlyCollection<BuffVisualsDat> GetBuffVisualsDat()
    {
        return BuffVisualsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BuffVisualsArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualsArtVariationsDat.</returns>
    public ReadOnlyCollection<BuffVisualsArtVariationsDat> GetBuffVisualsArtVariationsDat()
    {
        return BuffVisualsArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets BuffVisualSetEntriesDat data.
    /// </summary>
    /// <returns>readonly collection of BuffVisualSetEntriesDat.</returns>
    public ReadOnlyCollection<BuffVisualSetEntriesDat> GetBuffVisualSetEntriesDat()
    {
        return BuffVisualSetEntriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterAudioEventsDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterAudioEventsDat.</returns>
    public ReadOnlyCollection<CharacterAudioEventsDat> GetCharacterAudioEventsDat()
    {
        return CharacterAudioEventsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterEventTextAudioDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterEventTextAudioDat.</returns>
    public ReadOnlyCollection<CharacterEventTextAudioDat> GetCharacterEventTextAudioDat()
    {
        return CharacterEventTextAudioDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterPanelDescriptionModesDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterPanelDescriptionModesDat.</returns>
    public ReadOnlyCollection<CharacterPanelDescriptionModesDat> GetCharacterPanelDescriptionModesDat()
    {
        return CharacterPanelDescriptionModesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterPanelStatsDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterPanelStatsDat.</returns>
    public ReadOnlyCollection<CharacterPanelStatsDat> GetCharacterPanelStatsDat()
    {
        return CharacterPanelStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterPanelTabsDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterPanelTabsDat.</returns>
    public ReadOnlyCollection<CharacterPanelTabsDat> GetCharacterPanelTabsDat()
    {
        return CharacterPanelTabsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharactersDat data.
    /// </summary>
    /// <returns>readonly collection of CharactersDat.</returns>
    public ReadOnlyCollection<CharactersDat> GetCharactersDat()
    {
        return CharactersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterStartQuestStateDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterStartQuestStateDat.</returns>
    public ReadOnlyCollection<CharacterStartQuestStateDat> GetCharacterStartQuestStateDat()
    {
        return CharacterStartQuestStateDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterStartStatesDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterStartStatesDat.</returns>
    public ReadOnlyCollection<CharacterStartStatesDat> GetCharacterStartStatesDat()
    {
        return CharacterStartStatesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterStartStateSetDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterStartStateSetDat.</returns>
    public ReadOnlyCollection<CharacterStartStateSetDat> GetCharacterStartStateSetDat()
    {
        return CharacterStartStateSetDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CharacterTextAudioDat data.
    /// </summary>
    /// <returns>readonly collection of CharacterTextAudioDat.</returns>
    public ReadOnlyCollection<CharacterTextAudioDat> GetCharacterTextAudioDat()
    {
        return CharacterTextAudioDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ChatIconsDat data.
    /// </summary>
    /// <returns>readonly collection of ChatIconsDat.</returns>
    public ReadOnlyCollection<ChatIconsDat> GetChatIconsDat()
    {
        return ChatIconsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ChestClustersDat data.
    /// </summary>
    /// <returns>readonly collection of ChestClustersDat.</returns>
    public ReadOnlyCollection<ChestClustersDat> GetChestClustersDat()
    {
        return ChestClustersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ChestEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of ChestEffectsDat.</returns>
    public ReadOnlyCollection<ChestEffectsDat> GetChestEffectsDat()
    {
        return ChestEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ChestsDat data.
    /// </summary>
    /// <returns>readonly collection of ChestsDat.</returns>
    public ReadOnlyCollection<ChestsDat> GetChestsDat()
    {
        return ChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ClientStringsDat data.
    /// </summary>
    /// <returns>readonly collection of ClientStringsDat.</returns>
    public ReadOnlyCollection<ClientStringsDat> GetClientStringsDat()
    {
        return ClientStringsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ClientLeagueActionDat data.
    /// </summary>
    /// <returns>readonly collection of ClientLeagueActionDat.</returns>
    public ReadOnlyCollection<ClientLeagueActionDat> GetClientLeagueActionDat()
    {
        return ClientLeagueActionDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CloneShotDat data.
    /// </summary>
    /// <returns>readonly collection of CloneShotDat.</returns>
    public ReadOnlyCollection<CloneShotDat> GetCloneShotDat()
    {
        return CloneShotDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ColoursDat data.
    /// </summary>
    /// <returns>readonly collection of ColoursDat.</returns>
    public ReadOnlyCollection<ColoursDat> GetColoursDat()
    {
        return ColoursDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CommandsDat data.
    /// </summary>
    /// <returns>readonly collection of CommandsDat.</returns>
    public ReadOnlyCollection<CommandsDat> GetCommandsDat()
    {
        return CommandsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ComponentAttributeRequirementsDat data.
    /// </summary>
    /// <returns>readonly collection of ComponentAttributeRequirementsDat.</returns>
    public ReadOnlyCollection<ComponentAttributeRequirementsDat> GetComponentAttributeRequirementsDat()
    {
        return ComponentAttributeRequirementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ComponentChargesDat data.
    /// </summary>
    /// <returns>readonly collection of ComponentChargesDat.</returns>
    public ReadOnlyCollection<ComponentChargesDat> GetComponentChargesDat()
    {
        return ComponentChargesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CoreLeaguesDat data.
    /// </summary>
    /// <returns>readonly collection of CoreLeaguesDat.</returns>
    public ReadOnlyCollection<CoreLeaguesDat> GetCoreLeaguesDat()
    {
        return CoreLeaguesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CostTypesDat data.
    /// </summary>
    /// <returns>readonly collection of CostTypesDat.</returns>
    public ReadOnlyCollection<CostTypesDat> GetCostTypesDat()
    {
        return CostTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CraftingBenchOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of CraftingBenchOptionsDat.</returns>
    public ReadOnlyCollection<CraftingBenchOptionsDat> GetCraftingBenchOptionsDat()
    {
        return CraftingBenchOptionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CraftingBenchSortCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of CraftingBenchSortCategoriesDat.</returns>
    public ReadOnlyCollection<CraftingBenchSortCategoriesDat> GetCraftingBenchSortCategoriesDat()
    {
        return CraftingBenchSortCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CraftingBenchUnlockCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of CraftingBenchUnlockCategoriesDat.</returns>
    public ReadOnlyCollection<CraftingBenchUnlockCategoriesDat> GetCraftingBenchUnlockCategoriesDat()
    {
        return CraftingBenchUnlockCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CraftingItemClassCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of CraftingItemClassCategoriesDat.</returns>
    public ReadOnlyCollection<CraftingItemClassCategoriesDat> GetCraftingItemClassCategoriesDat()
    {
        return CraftingItemClassCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CurrencyItemsDat data.
    /// </summary>
    /// <returns>readonly collection of CurrencyItemsDat.</returns>
    public ReadOnlyCollection<CurrencyItemsDat> GetCurrencyItemsDat()
    {
        return CurrencyItemsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CurrencyStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of CurrencyStashTabLayoutDat.</returns>
    public ReadOnlyCollection<CurrencyStashTabLayoutDat> GetCurrencyStashTabLayoutDat()
    {
        return CurrencyStashTabLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets CustomLeagueModsDat data.
    /// </summary>
    /// <returns>readonly collection of CustomLeagueModsDat.</returns>
    public ReadOnlyCollection<CustomLeagueModsDat> GetCustomLeagueModsDat()
    {
        return CustomLeagueModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DaemonSpawningDataDat data.
    /// </summary>
    /// <returns>readonly collection of DaemonSpawningDataDat.</returns>
    public ReadOnlyCollection<DaemonSpawningDataDat> GetDaemonSpawningDataDat()
    {
        return DaemonSpawningDataDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DamageHitEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of DamageHitEffectsDat.</returns>
    public ReadOnlyCollection<DamageHitEffectsDat> GetDamageHitEffectsDat()
    {
        return DamageHitEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DamageParticleEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of DamageParticleEffectsDat.</returns>
    public ReadOnlyCollection<DamageParticleEffectsDat> GetDamageParticleEffectsDat()
    {
        return DamageParticleEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DancesDat data.
    /// </summary>
    /// <returns>readonly collection of DancesDat.</returns>
    public ReadOnlyCollection<DancesDat> GetDancesDat()
    {
        return DancesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DaressoPitFightsDat data.
    /// </summary>
    /// <returns>readonly collection of DaressoPitFightsDat.</returns>
    public ReadOnlyCollection<DaressoPitFightsDat> GetDaressoPitFightsDat()
    {
        return DaressoPitFightsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DefaultMonsterStatsDat data.
    /// </summary>
    /// <returns>readonly collection of DefaultMonsterStatsDat.</returns>
    public ReadOnlyCollection<DefaultMonsterStatsDat> GetDefaultMonsterStatsDat()
    {
        return DefaultMonsterStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DeliriumStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of DeliriumStashTabLayoutDat.</returns>
    public ReadOnlyCollection<DeliriumStashTabLayoutDat> GetDeliriumStashTabLayoutDat()
    {
        return DeliriumStashTabLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DelveStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of DelveStashTabLayoutDat.</returns>
    public ReadOnlyCollection<DelveStashTabLayoutDat> GetDelveStashTabLayoutDat()
    {
        return DelveStashTabLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DescentExilesDat data.
    /// </summary>
    /// <returns>readonly collection of DescentExilesDat.</returns>
    public ReadOnlyCollection<DescentExilesDat> GetDescentExilesDat()
    {
        return DescentExilesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DescentRewardChestsDat data.
    /// </summary>
    /// <returns>readonly collection of DescentRewardChestsDat.</returns>
    public ReadOnlyCollection<DescentRewardChestsDat> GetDescentRewardChestsDat()
    {
        return DescentRewardChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DescentStarterChestDat data.
    /// </summary>
    /// <returns>readonly collection of DescentStarterChestDat.</returns>
    public ReadOnlyCollection<DescentStarterChestDat> GetDescentStarterChestDat()
    {
        return DescentStarterChestDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DialogueEventDat data.
    /// </summary>
    /// <returns>readonly collection of DialogueEventDat.</returns>
    public ReadOnlyCollection<DialogueEventDat> GetDialogueEventDat()
    {
        return DialogueEventDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DisplayMinionMonsterTypeDat data.
    /// </summary>
    /// <returns>readonly collection of DisplayMinionMonsterTypeDat.</returns>
    public ReadOnlyCollection<DisplayMinionMonsterTypeDat> GetDisplayMinionMonsterTypeDat()
    {
        return DisplayMinionMonsterTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DivinationCardStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of DivinationCardStashTabLayoutDat.</returns>
    public ReadOnlyCollection<DivinationCardStashTabLayoutDat> GetDivinationCardStashTabLayoutDat()
    {
        return DivinationCardStashTabLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DoorsDat data.
    /// </summary>
    /// <returns>readonly collection of DoorsDat.</returns>
    public ReadOnlyCollection<DoorsDat> GetDoorsDat()
    {
        return DoorsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DropEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of DropEffectsDat.</returns>
    public ReadOnlyCollection<DropEffectsDat> GetDropEffectsDat()
    {
        return DropEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets DropPoolDat data.
    /// </summary>
    /// <returns>readonly collection of DropPoolDat.</returns>
    public ReadOnlyCollection<DropPoolDat> GetDropPoolDat()
    {
        return DropPoolDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EclipseModsDat data.
    /// </summary>
    /// <returns>readonly collection of EclipseModsDat.</returns>
    public ReadOnlyCollection<EclipseModsDat> GetEclipseModsDat()
    {
        return EclipseModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EffectDrivenSkillDat data.
    /// </summary>
    /// <returns>readonly collection of EffectDrivenSkillDat.</returns>
    public ReadOnlyCollection<EffectDrivenSkillDat> GetEffectDrivenSkillDat()
    {
        return EffectDrivenSkillDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EffectivenessCostConstantsDat data.
    /// </summary>
    /// <returns>readonly collection of EffectivenessCostConstantsDat.</returns>
    public ReadOnlyCollection<EffectivenessCostConstantsDat> GetEffectivenessCostConstantsDat()
    {
        return EffectivenessCostConstantsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EinharMissionsDat data.
    /// </summary>
    /// <returns>readonly collection of EinharMissionsDat.</returns>
    public ReadOnlyCollection<EinharMissionsDat> GetEinharMissionsDat()
    {
        return EinharMissionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EinharPackFallbackDat data.
    /// </summary>
    /// <returns>readonly collection of EinharPackFallbackDat.</returns>
    public ReadOnlyCollection<EinharPackFallbackDat> GetEinharPackFallbackDat()
    {
        return EinharPackFallbackDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EndlessLedgeChestsDat data.
    /// </summary>
    /// <returns>readonly collection of EndlessLedgeChestsDat.</returns>
    public ReadOnlyCollection<EndlessLedgeChestsDat> GetEndlessLedgeChestsDat()
    {
        return EndlessLedgeChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EnvironmentsDat data.
    /// </summary>
    /// <returns>readonly collection of EnvironmentsDat.</returns>
    public ReadOnlyCollection<EnvironmentsDat> GetEnvironmentsDat()
    {
        return EnvironmentsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EnvironmentTransitionsDat data.
    /// </summary>
    /// <returns>readonly collection of EnvironmentTransitionsDat.</returns>
    public ReadOnlyCollection<EnvironmentTransitionsDat> GetEnvironmentTransitionsDat()
    {
        return EnvironmentTransitionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EssenceStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of EssenceStashTabLayoutDat.</returns>
    public ReadOnlyCollection<EssenceStashTabLayoutDat> GetEssenceStashTabLayoutDat()
    {
        return EssenceStashTabLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EventSeasonDat data.
    /// </summary>
    /// <returns>readonly collection of EventSeasonDat.</returns>
    public ReadOnlyCollection<EventSeasonDat> GetEventSeasonDat()
    {
        return EventSeasonDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EventSeasonRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of EventSeasonRewardsDat.</returns>
    public ReadOnlyCollection<EventSeasonRewardsDat> GetEventSeasonRewardsDat()
    {
        return EventSeasonRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets EvergreenAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of EvergreenAchievementsDat.</returns>
    public ReadOnlyCollection<EvergreenAchievementsDat> GetEvergreenAchievementsDat()
    {
        return EvergreenAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExecuteGEALDat data.
    /// </summary>
    /// <returns>readonly collection of ExecuteGEALDat.</returns>
    public ReadOnlyCollection<ExecuteGEALDat> GetExecuteGEALDat()
    {
        return ExecuteGEALDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExpandingPulseDat data.
    /// </summary>
    /// <returns>readonly collection of ExpandingPulseDat.</returns>
    public ReadOnlyCollection<ExpandingPulseDat> GetExpandingPulseDat()
    {
        return ExpandingPulseDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExperienceLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of ExperienceLevelsDat.</returns>
    public ReadOnlyCollection<ExperienceLevelsDat> GetExperienceLevelsDat()
    {
        return ExperienceLevelsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExplodingStormBuffsDat data.
    /// </summary>
    /// <returns>readonly collection of ExplodingStormBuffsDat.</returns>
    public ReadOnlyCollection<ExplodingStormBuffsDat> GetExplodingStormBuffsDat()
    {
        return ExplodingStormBuffsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ExtraTerrainFeaturesDat data.
    /// </summary>
    /// <returns>readonly collection of ExtraTerrainFeaturesDat.</returns>
    public ReadOnlyCollection<ExtraTerrainFeaturesDat> GetExtraTerrainFeaturesDat()
    {
        return ExtraTerrainFeaturesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets FixedHideoutDoodadTypesDat data.
    /// </summary>
    /// <returns>readonly collection of FixedHideoutDoodadTypesDat.</returns>
    public ReadOnlyCollection<FixedHideoutDoodadTypesDat> GetFixedHideoutDoodadTypesDat()
    {
        return FixedHideoutDoodadTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets FixedMissionsDat data.
    /// </summary>
    /// <returns>readonly collection of FixedMissionsDat.</returns>
    public ReadOnlyCollection<FixedMissionsDat> GetFixedMissionsDat()
    {
        return FixedMissionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets FlasksDat data.
    /// </summary>
    /// <returns>readonly collection of FlasksDat.</returns>
    public ReadOnlyCollection<FlasksDat> GetFlasksDat()
    {
        return FlasksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets FlavourTextDat data.
    /// </summary>
    /// <returns>readonly collection of FlavourTextDat.</returns>
    public ReadOnlyCollection<FlavourTextDat> GetFlavourTextDat()
    {
        return FlavourTextDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets FootprintsDat data.
    /// </summary>
    /// <returns>readonly collection of FootprintsDat.</returns>
    public ReadOnlyCollection<FootprintsDat> GetFootprintsDat()
    {
        return FootprintsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets FootstepAudioDat data.
    /// </summary>
    /// <returns>readonly collection of FootstepAudioDat.</returns>
    public ReadOnlyCollection<FootstepAudioDat> GetFootstepAudioDat()
    {
        return FootstepAudioDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets FragmentStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of FragmentStashTabLayoutDat.</returns>
    public ReadOnlyCollection<FragmentStashTabLayoutDat> GetFragmentStashTabLayoutDat()
    {
        return FragmentStashTabLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GameConstantsDat data.
    /// </summary>
    /// <returns>readonly collection of GameConstantsDat.</returns>
    public ReadOnlyCollection<GameConstantsDat> GetGameConstantsDat()
    {
        return GameConstantsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GameObjectTasksDat data.
    /// </summary>
    /// <returns>readonly collection of GameObjectTasksDat.</returns>
    public ReadOnlyCollection<GameObjectTasksDat> GetGameObjectTasksDat()
    {
        return GameObjectTasksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GamepadButtonDat data.
    /// </summary>
    /// <returns>readonly collection of GamepadButtonDat.</returns>
    public ReadOnlyCollection<GamepadButtonDat> GetGamepadButtonDat()
    {
        return GamepadButtonDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GamepadTypeDat data.
    /// </summary>
    /// <returns>readonly collection of GamepadTypeDat.</returns>
    public ReadOnlyCollection<GamepadTypeDat> GetGamepadTypeDat()
    {
        return GamepadTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GameStatsDat data.
    /// </summary>
    /// <returns>readonly collection of GameStatsDat.</returns>
    public ReadOnlyCollection<GameStatsDat> GetGameStatsDat()
    {
        return GameStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GemTagsDat data.
    /// </summary>
    /// <returns>readonly collection of GemTagsDat.</returns>
    public ReadOnlyCollection<GemTagsDat> GetGemTagsDat()
    {
        return GemTagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GenericBuffAurasDat data.
    /// </summary>
    /// <returns>readonly collection of GenericBuffAurasDat.</returns>
    public ReadOnlyCollection<GenericBuffAurasDat> GetGenericBuffAurasDat()
    {
        return GenericBuffAurasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GenericLeagueRewardTypesDat data.
    /// </summary>
    /// <returns>readonly collection of GenericLeagueRewardTypesDat.</returns>
    public ReadOnlyCollection<GenericLeagueRewardTypesDat> GetGenericLeagueRewardTypesDat()
    {
        return GenericLeagueRewardTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GenericLeagueRewardTypeVisualsDat data.
    /// </summary>
    /// <returns>readonly collection of GenericLeagueRewardTypeVisualsDat.</returns>
    public ReadOnlyCollection<GenericLeagueRewardTypeVisualsDat> GetGenericLeagueRewardTypeVisualsDat()
    {
        return GenericLeagueRewardTypeVisualsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GeometryAttackDat data.
    /// </summary>
    /// <returns>readonly collection of GeometryAttackDat.</returns>
    public ReadOnlyCollection<GeometryAttackDat> GetGeometryAttackDat()
    {
        return GeometryAttackDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GeometryChannelDat data.
    /// </summary>
    /// <returns>readonly collection of GeometryChannelDat.</returns>
    public ReadOnlyCollection<GeometryChannelDat> GetGeometryChannelDat()
    {
        return GeometryChannelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GeometryProjectilesDat data.
    /// </summary>
    /// <returns>readonly collection of GeometryProjectilesDat.</returns>
    public ReadOnlyCollection<GeometryProjectilesDat> GetGeometryProjectilesDat()
    {
        return GeometryProjectilesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GeometryTriggerDat data.
    /// </summary>
    /// <returns>readonly collection of GeometryTriggerDat.</returns>
    public ReadOnlyCollection<GeometryTriggerDat> GetGeometryTriggerDat()
    {
        return GeometryTriggerDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GiftWrapArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of GiftWrapArtVariationsDat.</returns>
    public ReadOnlyCollection<GiftWrapArtVariationsDat> GetGiftWrapArtVariationsDat()
    {
        return GiftWrapArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GlobalAudioConfigDat data.
    /// </summary>
    /// <returns>readonly collection of GlobalAudioConfigDat.</returns>
    public ReadOnlyCollection<GlobalAudioConfigDat> GetGlobalAudioConfigDat()
    {
        return GlobalAudioConfigDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GrandmastersDat data.
    /// </summary>
    /// <returns>readonly collection of GrandmastersDat.</returns>
    public ReadOnlyCollection<GrandmastersDat> GetGrandmastersDat()
    {
        return GrandmastersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GrantedEffectQualityStatsDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectQualityStatsDat.</returns>
    public ReadOnlyCollection<GrantedEffectQualityStatsDat> GetGrantedEffectQualityStatsDat()
    {
        return GrantedEffectQualityStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GrantedEffectQualityTypesDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectQualityTypesDat.</returns>
    public ReadOnlyCollection<GrantedEffectQualityTypesDat> GetGrantedEffectQualityTypesDat()
    {
        return GrantedEffectQualityTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GrantedEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectsDat.</returns>
    public ReadOnlyCollection<GrantedEffectsDat> GetGrantedEffectsDat()
    {
        return GrantedEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GrantedEffectsPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectsPerLevelDat.</returns>
    public ReadOnlyCollection<GrantedEffectsPerLevelDat> GetGrantedEffectsPerLevelDat()
    {
        return GrantedEffectsPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GrantedEffectStatSetsDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectStatSetsDat.</returns>
    public ReadOnlyCollection<GrantedEffectStatSetsDat> GetGrantedEffectStatSetsDat()
    {
        return GrantedEffectStatSetsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GrantedEffectStatSetsPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of GrantedEffectStatSetsPerLevelDat.</returns>
    public ReadOnlyCollection<GrantedEffectStatSetsPerLevelDat> GetGrantedEffectStatSetsPerLevelDat()
    {
        return GrantedEffectStatSetsPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GroundEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of GroundEffectsDat.</returns>
    public ReadOnlyCollection<GroundEffectsDat> GetGroundEffectsDat()
    {
        return GroundEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets GroundEffectTypesDat data.
    /// </summary>
    /// <returns>readonly collection of GroundEffectTypesDat.</returns>
    public ReadOnlyCollection<GroundEffectTypesDat> GetGroundEffectTypesDat()
    {
        return GroundEffectTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HarvestStorageLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of HarvestStorageLayoutDat.</returns>
    public ReadOnlyCollection<HarvestStorageLayoutDat> GetHarvestStorageLayoutDat()
    {
        return HarvestStorageLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HeistStorageLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of HeistStorageLayoutDat.</returns>
    public ReadOnlyCollection<HeistStorageLayoutDat> GetHeistStorageLayoutDat()
    {
        return HeistStorageLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HideoutDoodadsDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutDoodadsDat.</returns>
    public ReadOnlyCollection<HideoutDoodadsDat> GetHideoutDoodadsDat()
    {
        return HideoutDoodadsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HideoutDoodadCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutDoodadCategoryDat.</returns>
    public ReadOnlyCollection<HideoutDoodadCategoryDat> GetHideoutDoodadCategoryDat()
    {
        return HideoutDoodadCategoryDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HideoutDoodadTagsDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutDoodadTagsDat.</returns>
    public ReadOnlyCollection<HideoutDoodadTagsDat> GetHideoutDoodadTagsDat()
    {
        return HideoutDoodadTagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HideoutNPCsDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutNPCsDat.</returns>
    public ReadOnlyCollection<HideoutNPCsDat> GetHideoutNPCsDat()
    {
        return HideoutNPCsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HideoutRarityDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutRarityDat.</returns>
    public ReadOnlyCollection<HideoutRarityDat> GetHideoutRarityDat()
    {
        return HideoutRarityDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets HideoutsDat data.
    /// </summary>
    /// <returns>readonly collection of HideoutsDat.</returns>
    public ReadOnlyCollection<HideoutsDat> GetHideoutsDat()
    {
        return HideoutsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ImpactSoundDataDat data.
    /// </summary>
    /// <returns>readonly collection of ImpactSoundDataDat.</returns>
    public ReadOnlyCollection<ImpactSoundDataDat> GetImpactSoundDataDat()
    {
        return ImpactSoundDataDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets IndexableSupportGemsDat data.
    /// </summary>
    /// <returns>readonly collection of IndexableSupportGemsDat.</returns>
    public ReadOnlyCollection<IndexableSupportGemsDat> GetIndexableSupportGemsDat()
    {
        return IndexableSupportGemsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets InfluenceExaltsDat data.
    /// </summary>
    /// <returns>readonly collection of InfluenceExaltsDat.</returns>
    public ReadOnlyCollection<InfluenceExaltsDat> GetInfluenceExaltsDat()
    {
        return InfluenceExaltsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets InfluenceTagsDat data.
    /// </summary>
    /// <returns>readonly collection of InfluenceTagsDat.</returns>
    public ReadOnlyCollection<InfluenceTagsDat> GetInfluenceTagsDat()
    {
        return InfluenceTagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets InventoriesDat data.
    /// </summary>
    /// <returns>readonly collection of InventoriesDat.</returns>
    public ReadOnlyCollection<InventoriesDat> GetInventoriesDat()
    {
        return InventoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemClassCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of ItemClassCategoriesDat.</returns>
    public ReadOnlyCollection<ItemClassCategoriesDat> GetItemClassCategoriesDat()
    {
        return ItemClassCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemClassesDat data.
    /// </summary>
    /// <returns>readonly collection of ItemClassesDat.</returns>
    public ReadOnlyCollection<ItemClassesDat> GetItemClassesDat()
    {
        return ItemClassesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemCostPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of ItemCostPerLevelDat.</returns>
    public ReadOnlyCollection<ItemCostPerLevelDat> GetItemCostPerLevelDat()
    {
        return ItemCostPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemCostsDat data.
    /// </summary>
    /// <returns>readonly collection of ItemCostsDat.</returns>
    public ReadOnlyCollection<ItemCostsDat> GetItemCostsDat()
    {
        return ItemCostsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemFrameTypeDat data.
    /// </summary>
    /// <returns>readonly collection of ItemFrameTypeDat.</returns>
    public ReadOnlyCollection<ItemFrameTypeDat> GetItemFrameTypeDat()
    {
        return ItemFrameTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemExperiencePerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of ItemExperiencePerLevelDat.</returns>
    public ReadOnlyCollection<ItemExperiencePerLevelDat> GetItemExperiencePerLevelDat()
    {
        return ItemExperiencePerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemisedVisualEffectDat data.
    /// </summary>
    /// <returns>readonly collection of ItemisedVisualEffectDat.</returns>
    public ReadOnlyCollection<ItemisedVisualEffectDat> GetItemisedVisualEffectDat()
    {
        return ItemisedVisualEffectDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemNoteCodeDat data.
    /// </summary>
    /// <returns>readonly collection of ItemNoteCodeDat.</returns>
    public ReadOnlyCollection<ItemNoteCodeDat> GetItemNoteCodeDat()
    {
        return ItemNoteCodeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemShopTypeDat data.
    /// </summary>
    /// <returns>readonly collection of ItemShopTypeDat.</returns>
    public ReadOnlyCollection<ItemShopTypeDat> GetItemShopTypeDat()
    {
        return ItemShopTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemStancesDat data.
    /// </summary>
    /// <returns>readonly collection of ItemStancesDat.</returns>
    public ReadOnlyCollection<ItemStancesDat> GetItemStancesDat()
    {
        return ItemStancesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemThemesDat data.
    /// </summary>
    /// <returns>readonly collection of ItemThemesDat.</returns>
    public ReadOnlyCollection<ItemThemesDat> GetItemThemesDat()
    {
        return ItemThemesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemVisualEffectDat data.
    /// </summary>
    /// <returns>readonly collection of ItemVisualEffectDat.</returns>
    public ReadOnlyCollection<ItemVisualEffectDat> GetItemVisualEffectDat()
    {
        return ItemVisualEffectDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemVisualHeldBodyModelDat data.
    /// </summary>
    /// <returns>readonly collection of ItemVisualHeldBodyModelDat.</returns>
    public ReadOnlyCollection<ItemVisualHeldBodyModelDat> GetItemVisualHeldBodyModelDat()
    {
        return ItemVisualHeldBodyModelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ItemVisualIdentityDat data.
    /// </summary>
    /// <returns>readonly collection of ItemVisualIdentityDat.</returns>
    public ReadOnlyCollection<ItemVisualIdentityDat> GetItemVisualIdentityDat()
    {
        return ItemVisualIdentityDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets JobAssassinationSpawnerGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of JobAssassinationSpawnerGroupsDat.</returns>
    public ReadOnlyCollection<JobAssassinationSpawnerGroupsDat> GetJobAssassinationSpawnerGroupsDat()
    {
        return JobAssassinationSpawnerGroupsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets JobRaidBracketsDat data.
    /// </summary>
    /// <returns>readonly collection of JobRaidBracketsDat.</returns>
    public ReadOnlyCollection<JobRaidBracketsDat> GetJobRaidBracketsDat()
    {
        return JobRaidBracketsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets KillstreakThresholdsDat data.
    /// </summary>
    /// <returns>readonly collection of KillstreakThresholdsDat.</returns>
    public ReadOnlyCollection<KillstreakThresholdsDat> GetKillstreakThresholdsDat()
    {
        return KillstreakThresholdsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LeagueFlagDat data.
    /// </summary>
    /// <returns>readonly collection of LeagueFlagDat.</returns>
    public ReadOnlyCollection<LeagueFlagDat> GetLeagueFlagDat()
    {
        return LeagueFlagDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LeagueInfoDat data.
    /// </summary>
    /// <returns>readonly collection of LeagueInfoDat.</returns>
    public ReadOnlyCollection<LeagueInfoDat> GetLeagueInfoDat()
    {
        return LeagueInfoDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LeagueProgressQuestFlagsDat data.
    /// </summary>
    /// <returns>readonly collection of LeagueProgressQuestFlagsDat.</returns>
    public ReadOnlyCollection<LeagueProgressQuestFlagsDat> GetLeagueProgressQuestFlagsDat()
    {
        return LeagueProgressQuestFlagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LeagueStaticRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of LeagueStaticRewardsDat.</returns>
    public ReadOnlyCollection<LeagueStaticRewardsDat> GetLeagueStaticRewardsDat()
    {
        return LeagueStaticRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets LevelRelativePlayerScalingDat data.
    /// </summary>
    /// <returns>readonly collection of LevelRelativePlayerScalingDat.</returns>
    public ReadOnlyCollection<LevelRelativePlayerScalingDat> GetLevelRelativePlayerScalingDat()
    {
        return LevelRelativePlayerScalingDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MagicMonsterLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of MagicMonsterLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<MagicMonsterLifeScalingPerLevelDat> GetMagicMonsterLifeScalingPerLevelDat()
    {
        return MagicMonsterLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapCompletionAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of MapCompletionAchievementsDat.</returns>
    public ReadOnlyCollection<MapCompletionAchievementsDat> GetMapCompletionAchievementsDat()
    {
        return MapCompletionAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapConnectionsDat data.
    /// </summary>
    /// <returns>readonly collection of MapConnectionsDat.</returns>
    public ReadOnlyCollection<MapConnectionsDat> GetMapConnectionsDat()
    {
        return MapConnectionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapCreationInformationDat data.
    /// </summary>
    /// <returns>readonly collection of MapCreationInformationDat.</returns>
    public ReadOnlyCollection<MapCreationInformationDat> GetMapCreationInformationDat()
    {
        return MapCreationInformationDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapDeviceRecipesDat data.
    /// </summary>
    /// <returns>readonly collection of MapDeviceRecipesDat.</returns>
    public ReadOnlyCollection<MapDeviceRecipesDat> GetMapDeviceRecipesDat()
    {
        return MapDeviceRecipesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapDevicesDat data.
    /// </summary>
    /// <returns>readonly collection of MapDevicesDat.</returns>
    public ReadOnlyCollection<MapDevicesDat> GetMapDevicesDat()
    {
        return MapDevicesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapFragmentModsDat data.
    /// </summary>
    /// <returns>readonly collection of MapFragmentModsDat.</returns>
    public ReadOnlyCollection<MapFragmentModsDat> GetMapFragmentModsDat()
    {
        return MapFragmentModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapInhabitantsDat data.
    /// </summary>
    /// <returns>readonly collection of MapInhabitantsDat.</returns>
    public ReadOnlyCollection<MapInhabitantsDat> GetMapInhabitantsDat()
    {
        return MapInhabitantsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapPinsDat data.
    /// </summary>
    /// <returns>readonly collection of MapPinsDat.</returns>
    public ReadOnlyCollection<MapPinsDat> GetMapPinsDat()
    {
        return MapPinsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapPurchaseCostsDat data.
    /// </summary>
    /// <returns>readonly collection of MapPurchaseCostsDat.</returns>
    public ReadOnlyCollection<MapPurchaseCostsDat> GetMapPurchaseCostsDat()
    {
        return MapPurchaseCostsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapsDat data.
    /// </summary>
    /// <returns>readonly collection of MapsDat.</returns>
    public ReadOnlyCollection<MapsDat> GetMapsDat()
    {
        return MapsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapSeriesDat data.
    /// </summary>
    /// <returns>readonly collection of MapSeriesDat.</returns>
    public ReadOnlyCollection<MapSeriesDat> GetMapSeriesDat()
    {
        return MapSeriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapSeriesTiersDat data.
    /// </summary>
    /// <returns>readonly collection of MapSeriesTiersDat.</returns>
    public ReadOnlyCollection<MapSeriesTiersDat> GetMapSeriesTiersDat()
    {
        return MapSeriesTiersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapStashSpecialTypeEntriesDat data.
    /// </summary>
    /// <returns>readonly collection of MapStashSpecialTypeEntriesDat.</returns>
    public ReadOnlyCollection<MapStashSpecialTypeEntriesDat> GetMapStashSpecialTypeEntriesDat()
    {
        return MapStashSpecialTypeEntriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapStashUniqueMapInfoDat data.
    /// </summary>
    /// <returns>readonly collection of MapStashUniqueMapInfoDat.</returns>
    public ReadOnlyCollection<MapStashUniqueMapInfoDat> GetMapStashUniqueMapInfoDat()
    {
        return MapStashUniqueMapInfoDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapStatConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of MapStatConditionsDat.</returns>
    public ReadOnlyCollection<MapStatConditionsDat> GetMapStatConditionsDat()
    {
        return MapStatConditionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapTierAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of MapTierAchievementsDat.</returns>
    public ReadOnlyCollection<MapTierAchievementsDat> GetMapTierAchievementsDat()
    {
        return MapTierAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MapTiersDat data.
    /// </summary>
    /// <returns>readonly collection of MapTiersDat.</returns>
    public ReadOnlyCollection<MapTiersDat> GetMapTiersDat()
    {
        return MapTiersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MasterHideoutLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of MasterHideoutLevelsDat.</returns>
    public ReadOnlyCollection<MasterHideoutLevelsDat> GetMasterHideoutLevelsDat()
    {
        return MasterHideoutLevelsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MeleeDat data.
    /// </summary>
    /// <returns>readonly collection of MeleeDat.</returns>
    public ReadOnlyCollection<MeleeDat> GetMeleeDat()
    {
        return MeleeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MeleeTrailsDat data.
    /// </summary>
    /// <returns>readonly collection of MeleeTrailsDat.</returns>
    public ReadOnlyCollection<MeleeTrailsDat> GetMeleeTrailsDat()
    {
        return MeleeTrailsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MetamorphosisStashTabLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of MetamorphosisStashTabLayoutDat.</returns>
    public ReadOnlyCollection<MetamorphosisStashTabLayoutDat> GetMetamorphosisStashTabLayoutDat()
    {
        return MetamorphosisStashTabLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicroMigrationDataDat data.
    /// </summary>
    /// <returns>readonly collection of MicroMigrationDataDat.</returns>
    public ReadOnlyCollection<MicroMigrationDataDat> GetMicroMigrationDataDat()
    {
        return MicroMigrationDataDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionCategoryDat.</returns>
    public ReadOnlyCollection<MicrotransactionCategoryDat> GetMicrotransactionCategoryDat()
    {
        return MicrotransactionCategoryDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionCharacterPortraitVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionCharacterPortraitVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionCharacterPortraitVariationsDat> GetMicrotransactionCharacterPortraitVariationsDat()
    {
        return MicrotransactionCharacterPortraitVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionCombineFormulaDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionCombineFormulaDat.</returns>
    public ReadOnlyCollection<MicrotransactionCombineFormulaDat> GetMicrotransactionCombineFormulaDat()
    {
        return MicrotransactionCombineFormulaDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionCursorVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionCursorVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionCursorVariationsDat> GetMicrotransactionCursorVariationsDat()
    {
        return MicrotransactionCursorVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionFireworksVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionFireworksVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionFireworksVariationsDat> GetMicrotransactionFireworksVariationsDat()
    {
        return MicrotransactionFireworksVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionGemCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionGemCategoryDat.</returns>
    public ReadOnlyCollection<MicrotransactionGemCategoryDat> GetMicrotransactionGemCategoryDat()
    {
        return MicrotransactionGemCategoryDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionPeriodicCharacterEffectVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionPeriodicCharacterEffectVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionPeriodicCharacterEffectVariationsDat> GetMicrotransactionPeriodicCharacterEffectVariationsDat()
    {
        return MicrotransactionPeriodicCharacterEffectVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionPortalVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionPortalVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionPortalVariationsDat> GetMicrotransactionPortalVariationsDat()
    {
        return MicrotransactionPortalVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionRarityDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionRarityDisplayDat.</returns>
    public ReadOnlyCollection<MicrotransactionRarityDisplayDat> GetMicrotransactionRarityDisplayDat()
    {
        return MicrotransactionRarityDisplayDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionRecycleOutcomesDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionRecycleOutcomesDat.</returns>
    public ReadOnlyCollection<MicrotransactionRecycleOutcomesDat> GetMicrotransactionRecycleOutcomesDat()
    {
        return MicrotransactionRecycleOutcomesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionRecycleSalvageValuesDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionRecycleSalvageValuesDat.</returns>
    public ReadOnlyCollection<MicrotransactionRecycleSalvageValuesDat> GetMicrotransactionRecycleSalvageValuesDat()
    {
        return MicrotransactionRecycleSalvageValuesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionSlotDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionSlotDat.</returns>
    public ReadOnlyCollection<MicrotransactionSlotDat> GetMicrotransactionSlotDat()
    {
        return MicrotransactionSlotDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MicrotransactionSocialFrameVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MicrotransactionSocialFrameVariationsDat.</returns>
    public ReadOnlyCollection<MicrotransactionSocialFrameVariationsDat> GetMicrotransactionSocialFrameVariationsDat()
    {
        return MicrotransactionSocialFrameVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MinimapIconsDat data.
    /// </summary>
    /// <returns>readonly collection of MinimapIconsDat.</returns>
    public ReadOnlyCollection<MinimapIconsDat> GetMinimapIconsDat()
    {
        return MinimapIconsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiniQuestStatesDat data.
    /// </summary>
    /// <returns>readonly collection of MiniQuestStatesDat.</returns>
    public ReadOnlyCollection<MiniQuestStatesDat> GetMiniQuestStatesDat()
    {
        return MiniQuestStatesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiscAnimatedDat data.
    /// </summary>
    /// <returns>readonly collection of MiscAnimatedDat.</returns>
    public ReadOnlyCollection<MiscAnimatedDat> GetMiscAnimatedDat()
    {
        return MiscAnimatedDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiscAnimatedArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscAnimatedArtVariationsDat.</returns>
    public ReadOnlyCollection<MiscAnimatedArtVariationsDat> GetMiscAnimatedArtVariationsDat()
    {
        return MiscAnimatedArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiscBeamsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscBeamsDat.</returns>
    public ReadOnlyCollection<MiscBeamsDat> GetMiscBeamsDat()
    {
        return MiscBeamsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiscBeamsArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscBeamsArtVariationsDat.</returns>
    public ReadOnlyCollection<MiscBeamsArtVariationsDat> GetMiscBeamsArtVariationsDat()
    {
        return MiscBeamsArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiscEffectPacksDat data.
    /// </summary>
    /// <returns>readonly collection of MiscEffectPacksDat.</returns>
    public ReadOnlyCollection<MiscEffectPacksDat> GetMiscEffectPacksDat()
    {
        return MiscEffectPacksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiscEffectPacksArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscEffectPacksArtVariationsDat.</returns>
    public ReadOnlyCollection<MiscEffectPacksArtVariationsDat> GetMiscEffectPacksArtVariationsDat()
    {
        return MiscEffectPacksArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiscObjectsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscObjectsDat.</returns>
    public ReadOnlyCollection<MiscObjectsDat> GetMiscObjectsDat()
    {
        return MiscObjectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MiscObjectsArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MiscObjectsArtVariationsDat.</returns>
    public ReadOnlyCollection<MiscObjectsArtVariationsDat> GetMiscObjectsArtVariationsDat()
    {
        return MiscObjectsArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MissionFavourPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of MissionFavourPerLevelDat.</returns>
    public ReadOnlyCollection<MissionFavourPerLevelDat> GetMissionFavourPerLevelDat()
    {
        return MissionFavourPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MissionTimerTypesDat data.
    /// </summary>
    /// <returns>readonly collection of MissionTimerTypesDat.</returns>
    public ReadOnlyCollection<MissionTimerTypesDat> GetMissionTimerTypesDat()
    {
        return MissionTimerTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MissionTransitionTilesDat data.
    /// </summary>
    /// <returns>readonly collection of MissionTransitionTilesDat.</returns>
    public ReadOnlyCollection<MissionTransitionTilesDat> GetMissionTransitionTilesDat()
    {
        return MissionTransitionTilesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ModEffectStatsDat data.
    /// </summary>
    /// <returns>readonly collection of ModEffectStatsDat.</returns>
    public ReadOnlyCollection<ModEffectStatsDat> GetModEffectStatsDat()
    {
        return ModEffectStatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ModEquivalenciesDat data.
    /// </summary>
    /// <returns>readonly collection of ModEquivalenciesDat.</returns>
    public ReadOnlyCollection<ModEquivalenciesDat> GetModEquivalenciesDat()
    {
        return ModEquivalenciesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ModFamilyDat data.
    /// </summary>
    /// <returns>readonly collection of ModFamilyDat.</returns>
    public ReadOnlyCollection<ModFamilyDat> GetModFamilyDat()
    {
        return ModFamilyDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ModsDat data.
    /// </summary>
    /// <returns>readonly collection of ModsDat.</returns>
    public ReadOnlyCollection<ModsDat> GetModsDat()
    {
        return ModsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ModSellPriceTypesDat data.
    /// </summary>
    /// <returns>readonly collection of ModSellPriceTypesDat.</returns>
    public ReadOnlyCollection<ModSellPriceTypesDat> GetModSellPriceTypesDat()
    {
        return ModSellPriceTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ModTypeDat data.
    /// </summary>
    /// <returns>readonly collection of ModTypeDat.</returns>
    public ReadOnlyCollection<ModTypeDat> GetModTypeDat()
    {
        return ModTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterArmoursDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterArmoursDat.</returns>
    public ReadOnlyCollection<MonsterArmoursDat> GetMonsterArmoursDat()
    {
        return MonsterArmoursDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterBonusesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterBonusesDat.</returns>
    public ReadOnlyCollection<MonsterBonusesDat> GetMonsterBonusesDat()
    {
        return MonsterBonusesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterConditionalEffectPacksDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterConditionalEffectPacksDat.</returns>
    public ReadOnlyCollection<MonsterConditionalEffectPacksDat> GetMonsterConditionalEffectPacksDat()
    {
        return MonsterConditionalEffectPacksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterConditionsDat.</returns>
    public ReadOnlyCollection<MonsterConditionsDat> GetMonsterConditionsDat()
    {
        return MonsterConditionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterDeathAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterDeathAchievementsDat.</returns>
    public ReadOnlyCollection<MonsterDeathAchievementsDat> GetMonsterDeathAchievementsDat()
    {
        return MonsterDeathAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterDeathConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterDeathConditionsDat.</returns>
    public ReadOnlyCollection<MonsterDeathConditionsDat> GetMonsterDeathConditionsDat()
    {
        return MonsterDeathConditionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterGroupEntriesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterGroupEntriesDat.</returns>
    public ReadOnlyCollection<MonsterGroupEntriesDat> GetMonsterGroupEntriesDat()
    {
        return MonsterGroupEntriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterHeightBracketsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterHeightBracketsDat.</returns>
    public ReadOnlyCollection<MonsterHeightBracketsDat> GetMonsterHeightBracketsDat()
    {
        return MonsterHeightBracketsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterHeightsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterHeightsDat.</returns>
    public ReadOnlyCollection<MonsterHeightsDat> GetMonsterHeightsDat()
    {
        return MonsterHeightsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterMapBossDifficultyDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterMapBossDifficultyDat.</returns>
    public ReadOnlyCollection<MonsterMapBossDifficultyDat> GetMonsterMapBossDifficultyDat()
    {
        return MonsterMapBossDifficultyDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterMapDifficultyDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterMapDifficultyDat.</returns>
    public ReadOnlyCollection<MonsterMapDifficultyDat> GetMonsterMapDifficultyDat()
    {
        return MonsterMapDifficultyDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterMortarDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterMortarDat.</returns>
    public ReadOnlyCollection<MonsterMortarDat> GetMonsterMortarDat()
    {
        return MonsterMortarDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterPackCountsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterPackCountsDat.</returns>
    public ReadOnlyCollection<MonsterPackCountsDat> GetMonsterPackCountsDat()
    {
        return MonsterPackCountsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterPackEntriesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterPackEntriesDat.</returns>
    public ReadOnlyCollection<MonsterPackEntriesDat> GetMonsterPackEntriesDat()
    {
        return MonsterPackEntriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterPacksDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterPacksDat.</returns>
    public ReadOnlyCollection<MonsterPacksDat> GetMonsterPacksDat()
    {
        return MonsterPacksDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterProjectileAttackDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterProjectileAttackDat.</returns>
    public ReadOnlyCollection<MonsterProjectileAttackDat> GetMonsterProjectileAttackDat()
    {
        return MonsterProjectileAttackDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterProjectileSpellDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterProjectileSpellDat.</returns>
    public ReadOnlyCollection<MonsterProjectileSpellDat> GetMonsterProjectileSpellDat()
    {
        return MonsterProjectileSpellDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterResistancesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterResistancesDat.</returns>
    public ReadOnlyCollection<MonsterResistancesDat> GetMonsterResistancesDat()
    {
        return MonsterResistancesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterSegmentsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterSegmentsDat.</returns>
    public ReadOnlyCollection<MonsterSegmentsDat> GetMonsterSegmentsDat()
    {
        return MonsterSegmentsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterSpawnerGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterSpawnerGroupsDat.</returns>
    public ReadOnlyCollection<MonsterSpawnerGroupsDat> GetMonsterSpawnerGroupsDat()
    {
        return MonsterSpawnerGroupsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterSpawnerGroupsPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterSpawnerGroupsPerLevelDat.</returns>
    public ReadOnlyCollection<MonsterSpawnerGroupsPerLevelDat> GetMonsterSpawnerGroupsPerLevelDat()
    {
        return MonsterSpawnerGroupsPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterSpawnerOverridesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterSpawnerOverridesDat.</returns>
    public ReadOnlyCollection<MonsterSpawnerOverridesDat> GetMonsterSpawnerOverridesDat()
    {
        return MonsterSpawnerOverridesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterTypesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterTypesDat.</returns>
    public ReadOnlyCollection<MonsterTypesDat> GetMonsterTypesDat()
    {
        return MonsterTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterVarietiesDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterVarietiesDat.</returns>
    public ReadOnlyCollection<MonsterVarietiesDat> GetMonsterVarietiesDat()
    {
        return MonsterVarietiesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MonsterVarietiesArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of MonsterVarietiesArtVariationsDat.</returns>
    public ReadOnlyCollection<MonsterVarietiesArtVariationsDat> GetMonsterVarietiesArtVariationsDat()
    {
        return MonsterVarietiesArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MouseCursorSizeSettingsDat data.
    /// </summary>
    /// <returns>readonly collection of MouseCursorSizeSettingsDat.</returns>
    public ReadOnlyCollection<MouseCursorSizeSettingsDat> GetMouseCursorSizeSettingsDat()
    {
        return MouseCursorSizeSettingsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MoveDaemonDat data.
    /// </summary>
    /// <returns>readonly collection of MoveDaemonDat.</returns>
    public ReadOnlyCollection<MoveDaemonDat> GetMoveDaemonDat()
    {
        return MoveDaemonDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MTXSetBonusDat data.
    /// </summary>
    /// <returns>readonly collection of MTXSetBonusDat.</returns>
    public ReadOnlyCollection<MTXSetBonusDat> GetMTXSetBonusDat()
    {
        return MTXSetBonusDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MultiPartAchievementAreasDat data.
    /// </summary>
    /// <returns>readonly collection of MultiPartAchievementAreasDat.</returns>
    public ReadOnlyCollection<MultiPartAchievementAreasDat> GetMultiPartAchievementAreasDat()
    {
        return MultiPartAchievementAreasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MultiPartAchievementConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of MultiPartAchievementConditionsDat.</returns>
    public ReadOnlyCollection<MultiPartAchievementConditionsDat> GetMultiPartAchievementConditionsDat()
    {
        return MultiPartAchievementConditionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MultiPartAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of MultiPartAchievementsDat.</returns>
    public ReadOnlyCollection<MultiPartAchievementsDat> GetMultiPartAchievementsDat()
    {
        return MultiPartAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MusicDat data.
    /// </summary>
    /// <returns>readonly collection of MusicDat.</returns>
    public ReadOnlyCollection<MusicDat> GetMusicDat()
    {
        return MusicDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MusicCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of MusicCategoriesDat.</returns>
    public ReadOnlyCollection<MusicCategoriesDat> GetMusicCategoriesDat()
    {
        return MusicCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets MysteryBoxesDat data.
    /// </summary>
    /// <returns>readonly collection of MysteryBoxesDat.</returns>
    public ReadOnlyCollection<MysteryBoxesDat> GetMysteryBoxesDat()
    {
        return MysteryBoxesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NearbyMonsterConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of NearbyMonsterConditionsDat.</returns>
    public ReadOnlyCollection<NearbyMonsterConditionsDat> GetNearbyMonsterConditionsDat()
    {
        return NearbyMonsterConditionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NetTiersDat data.
    /// </summary>
    /// <returns>readonly collection of NetTiersDat.</returns>
    public ReadOnlyCollection<NetTiersDat> GetNetTiersDat()
    {
        return NetTiersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NotificationsDat data.
    /// </summary>
    /// <returns>readonly collection of NotificationsDat.</returns>
    public ReadOnlyCollection<NotificationsDat> GetNotificationsDat()
    {
        return NotificationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCAudioDat data.
    /// </summary>
    /// <returns>readonly collection of NPCAudioDat.</returns>
    public ReadOnlyCollection<NPCAudioDat> GetNPCAudioDat()
    {
        return NPCAudioDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCConversationsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCConversationsDat.</returns>
    public ReadOnlyCollection<NPCConversationsDat> GetNPCConversationsDat()
    {
        return NPCConversationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCDialogueStylesDat data.
    /// </summary>
    /// <returns>readonly collection of NPCDialogueStylesDat.</returns>
    public ReadOnlyCollection<NPCDialogueStylesDat> GetNPCDialogueStylesDat()
    {
        return NPCDialogueStylesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCFollowerVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCFollowerVariationsDat.</returns>
    public ReadOnlyCollection<NPCFollowerVariationsDat> GetNPCFollowerVariationsDat()
    {
        return NPCFollowerVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCMasterDat data.
    /// </summary>
    /// <returns>readonly collection of NPCMasterDat.</returns>
    public ReadOnlyCollection<NPCMasterDat> GetNPCMasterDat()
    {
        return NPCMasterDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCPortraitsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCPortraitsDat.</returns>
    public ReadOnlyCollection<NPCPortraitsDat> GetNPCPortraitsDat()
    {
        return NPCPortraitsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCsDat.</returns>
    public ReadOnlyCollection<NPCsDat> GetNPCsDat()
    {
        return NPCsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCShopDat data.
    /// </summary>
    /// <returns>readonly collection of NPCShopDat.</returns>
    public ReadOnlyCollection<NPCShopDat> GetNPCShopDat()
    {
        return NPCShopDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCShopsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCShopsDat.</returns>
    public ReadOnlyCollection<NPCShopsDat> GetNPCShopsDat()
    {
        return NPCShopsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCTalkDat data.
    /// </summary>
    /// <returns>readonly collection of NPCTalkDat.</returns>
    public ReadOnlyCollection<NPCTalkDat> GetNPCTalkDat()
    {
        return NPCTalkDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCTalkCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of NPCTalkCategoryDat.</returns>
    public ReadOnlyCollection<NPCTalkCategoryDat> GetNPCTalkCategoryDat()
    {
        return NPCTalkCategoryDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCTalkConsoleQuickActionsDat data.
    /// </summary>
    /// <returns>readonly collection of NPCTalkConsoleQuickActionsDat.</returns>
    public ReadOnlyCollection<NPCTalkConsoleQuickActionsDat> GetNPCTalkConsoleQuickActionsDat()
    {
        return NPCTalkConsoleQuickActionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets NPCTextAudioDat data.
    /// </summary>
    /// <returns>readonly collection of NPCTextAudioDat.</returns>
    public ReadOnlyCollection<NPCTextAudioDat> GetNPCTextAudioDat()
    {
        return NPCTextAudioDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets OnKillAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of OnKillAchievementsDat.</returns>
    public ReadOnlyCollection<OnKillAchievementsDat> GetOnKillAchievementsDat()
    {
        return OnKillAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PackFormationDat data.
    /// </summary>
    /// <returns>readonly collection of PackFormationDat.</returns>
    public ReadOnlyCollection<PackFormationDat> GetPackFormationDat()
    {
        return PackFormationDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveJewelRadiiDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveJewelRadiiDat.</returns>
    public ReadOnlyCollection<PassiveJewelRadiiDat> GetPassiveJewelRadiiDat()
    {
        return PassiveJewelRadiiDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveJewelSlotsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveJewelSlotsDat.</returns>
    public ReadOnlyCollection<PassiveJewelSlotsDat> GetPassiveJewelSlotsDat()
    {
        return PassiveJewelSlotsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillFilterCatagoriesDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillFilterCatagoriesDat.</returns>
    public ReadOnlyCollection<PassiveSkillFilterCatagoriesDat> GetPassiveSkillFilterCatagoriesDat()
    {
        return PassiveSkillFilterCatagoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillFilterOptionsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillFilterOptionsDat.</returns>
    public ReadOnlyCollection<PassiveSkillFilterOptionsDat> GetPassiveSkillFilterOptionsDat()
    {
        return PassiveSkillFilterOptionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillMasteryGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillMasteryGroupsDat.</returns>
    public ReadOnlyCollection<PassiveSkillMasteryGroupsDat> GetPassiveSkillMasteryGroupsDat()
    {
        return PassiveSkillMasteryGroupsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillMasteryEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillMasteryEffectsDat.</returns>
    public ReadOnlyCollection<PassiveSkillMasteryEffectsDat> GetPassiveSkillMasteryEffectsDat()
    {
        return PassiveSkillMasteryEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillsDat.</returns>
    public ReadOnlyCollection<PassiveSkillsDat> GetPassiveSkillsDat()
    {
        return PassiveSkillsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillStatCategoriesDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillStatCategoriesDat.</returns>
    public ReadOnlyCollection<PassiveSkillStatCategoriesDat> GetPassiveSkillStatCategoriesDat()
    {
        return PassiveSkillStatCategoriesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillTreesDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillTreesDat.</returns>
    public ReadOnlyCollection<PassiveSkillTreesDat> GetPassiveSkillTreesDat()
    {
        return PassiveSkillTreesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillTreeTutorialDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillTreeTutorialDat.</returns>
    public ReadOnlyCollection<PassiveSkillTreeTutorialDat> GetPassiveSkillTreeTutorialDat()
    {
        return PassiveSkillTreeTutorialDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveSkillTreeUIArtDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveSkillTreeUIArtDat.</returns>
    public ReadOnlyCollection<PassiveSkillTreeUIArtDat> GetPassiveSkillTreeUIArtDat()
    {
        return PassiveSkillTreeUIArtDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveTreeExpansionJewelsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveTreeExpansionJewelsDat.</returns>
    public ReadOnlyCollection<PassiveTreeExpansionJewelsDat> GetPassiveTreeExpansionJewelsDat()
    {
        return PassiveTreeExpansionJewelsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveTreeExpansionJewelSizesDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveTreeExpansionJewelSizesDat.</returns>
    public ReadOnlyCollection<PassiveTreeExpansionJewelSizesDat> GetPassiveTreeExpansionJewelSizesDat()
    {
        return PassiveTreeExpansionJewelSizesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveTreeExpansionSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveTreeExpansionSkillsDat.</returns>
    public ReadOnlyCollection<PassiveTreeExpansionSkillsDat> GetPassiveTreeExpansionSkillsDat()
    {
        return PassiveTreeExpansionSkillsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PassiveTreeExpansionSpecialSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of PassiveTreeExpansionSpecialSkillsDat.</returns>
    public ReadOnlyCollection<PassiveTreeExpansionSpecialSkillsDat> GetPassiveTreeExpansionSpecialSkillsDat()
    {
        return PassiveTreeExpansionSpecialSkillsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PCBangRewardMicrosDat data.
    /// </summary>
    /// <returns>readonly collection of PCBangRewardMicrosDat.</returns>
    public ReadOnlyCollection<PCBangRewardMicrosDat> GetPCBangRewardMicrosDat()
    {
        return PCBangRewardMicrosDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PetDat data.
    /// </summary>
    /// <returns>readonly collection of PetDat.</returns>
    public ReadOnlyCollection<PetDat> GetPetDat()
    {
        return PetDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PlayerConditionsDat data.
    /// </summary>
    /// <returns>readonly collection of PlayerConditionsDat.</returns>
    public ReadOnlyCollection<PlayerConditionsDat> GetPlayerConditionsDat()
    {
        return PlayerConditionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PlayerTradeWhisperFormatsDat data.
    /// </summary>
    /// <returns>readonly collection of PlayerTradeWhisperFormatsDat.</returns>
    public ReadOnlyCollection<PlayerTradeWhisperFormatsDat> GetPlayerTradeWhisperFormatsDat()
    {
        return PlayerTradeWhisperFormatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PreloadGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of PreloadGroupsDat.</returns>
    public ReadOnlyCollection<PreloadGroupsDat> GetPreloadGroupsDat()
    {
        return PreloadGroupsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ProjectilesDat data.
    /// </summary>
    /// <returns>readonly collection of ProjectilesDat.</returns>
    public ReadOnlyCollection<ProjectilesDat> GetProjectilesDat()
    {
        return ProjectilesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ProjectilesArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of ProjectilesArtVariationsDat.</returns>
    public ReadOnlyCollection<ProjectilesArtVariationsDat> GetProjectilesArtVariationsDat()
    {
        return ProjectilesArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ProjectileVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of ProjectileVariationsDat.</returns>
    public ReadOnlyCollection<ProjectileVariationsDat> GetProjectileVariationsDat()
    {
        return ProjectileVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets PVPTypesDat data.
    /// </summary>
    /// <returns>readonly collection of PVPTypesDat.</returns>
    public ReadOnlyCollection<PVPTypesDat> GetPVPTypesDat()
    {
        return PVPTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestDat data.
    /// </summary>
    /// <returns>readonly collection of QuestDat.</returns>
    public ReadOnlyCollection<QuestDat> GetQuestDat()
    {
        return QuestDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestAchievementsDat data.
    /// </summary>
    /// <returns>readonly collection of QuestAchievementsDat.</returns>
    public ReadOnlyCollection<QuestAchievementsDat> GetQuestAchievementsDat()
    {
        return QuestAchievementsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestFlagsDat data.
    /// </summary>
    /// <returns>readonly collection of QuestFlagsDat.</returns>
    public ReadOnlyCollection<QuestFlagsDat> GetQuestFlagsDat()
    {
        return QuestFlagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestItemsDat data.
    /// </summary>
    /// <returns>readonly collection of QuestItemsDat.</returns>
    public ReadOnlyCollection<QuestItemsDat> GetQuestItemsDat()
    {
        return QuestItemsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestRewardOffersDat data.
    /// </summary>
    /// <returns>readonly collection of QuestRewardOffersDat.</returns>
    public ReadOnlyCollection<QuestRewardOffersDat> GetQuestRewardOffersDat()
    {
        return QuestRewardOffersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of QuestRewardsDat.</returns>
    public ReadOnlyCollection<QuestRewardsDat> GetQuestRewardsDat()
    {
        return QuestRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestStatesDat data.
    /// </summary>
    /// <returns>readonly collection of QuestStatesDat.</returns>
    public ReadOnlyCollection<QuestStatesDat> GetQuestStatesDat()
    {
        return QuestStatesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestStaticRewardsDat data.
    /// </summary>
    /// <returns>readonly collection of QuestStaticRewardsDat.</returns>
    public ReadOnlyCollection<QuestStaticRewardsDat> GetQuestStaticRewardsDat()
    {
        return QuestStaticRewardsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestTrackerGroupDat data.
    /// </summary>
    /// <returns>readonly collection of QuestTrackerGroupDat.</returns>
    public ReadOnlyCollection<QuestTrackerGroupDat> GetQuestTrackerGroupDat()
    {
        return QuestTrackerGroupDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets QuestTypeDat data.
    /// </summary>
    /// <returns>readonly collection of QuestTypeDat.</returns>
    public ReadOnlyCollection<QuestTypeDat> GetQuestTypeDat()
    {
        return QuestTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RacesDat data.
    /// </summary>
    /// <returns>readonly collection of RacesDat.</returns>
    public ReadOnlyCollection<RacesDat> GetRacesDat()
    {
        return RacesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RaceTimesDat data.
    /// </summary>
    /// <returns>readonly collection of RaceTimesDat.</returns>
    public ReadOnlyCollection<RaceTimesDat> GetRaceTimesDat()
    {
        return RaceTimesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RareMonsterLifeScalingPerLevelDat data.
    /// </summary>
    /// <returns>readonly collection of RareMonsterLifeScalingPerLevelDat.</returns>
    public ReadOnlyCollection<RareMonsterLifeScalingPerLevelDat> GetRareMonsterLifeScalingPerLevelDat()
    {
        return RareMonsterLifeScalingPerLevelDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RarityDat data.
    /// </summary>
    /// <returns>readonly collection of RarityDat.</returns>
    public ReadOnlyCollection<RarityDat> GetRarityDat()
    {
        return RarityDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RealmsDat data.
    /// </summary>
    /// <returns>readonly collection of RealmsDat.</returns>
    public ReadOnlyCollection<RealmsDat> GetRealmsDat()
    {
        return RealmsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RecipeUnlockDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of RecipeUnlockDisplayDat.</returns>
    public ReadOnlyCollection<RecipeUnlockDisplayDat> GetRecipeUnlockDisplayDat()
    {
        return RecipeUnlockDisplayDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RecipeUnlockObjectsDat data.
    /// </summary>
    /// <returns>readonly collection of RecipeUnlockObjectsDat.</returns>
    public ReadOnlyCollection<RecipeUnlockObjectsDat> GetRecipeUnlockObjectsDat()
    {
        return RecipeUnlockObjectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ReminderTextDat data.
    /// </summary>
    /// <returns>readonly collection of ReminderTextDat.</returns>
    public ReadOnlyCollection<ReminderTextDat> GetReminderTextDat()
    {
        return ReminderTextDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RulesetsDat data.
    /// </summary>
    /// <returns>readonly collection of RulesetsDat.</returns>
    public ReadOnlyCollection<RulesetsDat> GetRulesetsDat()
    {
        return RulesetsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets RunicCirclesDat data.
    /// </summary>
    /// <returns>readonly collection of RunicCirclesDat.</returns>
    public ReadOnlyCollection<RunicCirclesDat> GetRunicCirclesDat()
    {
        return RunicCirclesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SalvageBoxesDat data.
    /// </summary>
    /// <returns>readonly collection of SalvageBoxesDat.</returns>
    public ReadOnlyCollection<SalvageBoxesDat> GetSalvageBoxesDat()
    {
        return SalvageBoxesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SessionQuestFlagsDat data.
    /// </summary>
    /// <returns>readonly collection of SessionQuestFlagsDat.</returns>
    public ReadOnlyCollection<SessionQuestFlagsDat> GetSessionQuestFlagsDat()
    {
        return SessionQuestFlagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShieldTypesDat data.
    /// </summary>
    /// <returns>readonly collection of ShieldTypesDat.</returns>
    public ReadOnlyCollection<ShieldTypesDat> GetShieldTypesDat()
    {
        return ShieldTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShopCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of ShopCategoryDat.</returns>
    public ReadOnlyCollection<ShopCategoryDat> GetShopCategoryDat()
    {
        return ShopCategoryDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShopCountryDat data.
    /// </summary>
    /// <returns>readonly collection of ShopCountryDat.</returns>
    public ReadOnlyCollection<ShopCountryDat> GetShopCountryDat()
    {
        return ShopCountryDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShopCurrencyDat data.
    /// </summary>
    /// <returns>readonly collection of ShopCurrencyDat.</returns>
    public ReadOnlyCollection<ShopCurrencyDat> GetShopCurrencyDat()
    {
        return ShopCurrencyDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShopPaymentPackageDat data.
    /// </summary>
    /// <returns>readonly collection of ShopPaymentPackageDat.</returns>
    public ReadOnlyCollection<ShopPaymentPackageDat> GetShopPaymentPackageDat()
    {
        return ShopPaymentPackageDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShopPaymentPackagePriceDat data.
    /// </summary>
    /// <returns>readonly collection of ShopPaymentPackagePriceDat.</returns>
    public ReadOnlyCollection<ShopPaymentPackagePriceDat> GetShopPaymentPackagePriceDat()
    {
        return ShopPaymentPackagePriceDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShopRegionDat data.
    /// </summary>
    /// <returns>readonly collection of ShopRegionDat.</returns>
    public ReadOnlyCollection<ShopRegionDat> GetShopRegionDat()
    {
        return ShopRegionDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShopTagDat data.
    /// </summary>
    /// <returns>readonly collection of ShopTagDat.</returns>
    public ReadOnlyCollection<ShopTagDat> GetShopTagDat()
    {
        return ShopTagDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ShopTokenDat data.
    /// </summary>
    /// <returns>readonly collection of ShopTokenDat.</returns>
    public ReadOnlyCollection<ShopTokenDat> GetShopTokenDat()
    {
        return ShopTokenDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SigilDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of SigilDisplayDat.</returns>
    public ReadOnlyCollection<SigilDisplayDat> GetSigilDisplayDat()
    {
        return SigilDisplayDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SingleGroundLaserDat data.
    /// </summary>
    /// <returns>readonly collection of SingleGroundLaserDat.</returns>
    public ReadOnlyCollection<SingleGroundLaserDat> GetSingleGroundLaserDat()
    {
        return SingleGroundLaserDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SkillArtVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillArtVariationsDat.</returns>
    public ReadOnlyCollection<SkillArtVariationsDat> GetSkillArtVariationsDat()
    {
        return SkillArtVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SkillGemInfoDat data.
    /// </summary>
    /// <returns>readonly collection of SkillGemInfoDat.</returns>
    public ReadOnlyCollection<SkillGemInfoDat> GetSkillGemInfoDat()
    {
        return SkillGemInfoDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SkillGemsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillGemsDat.</returns>
    public ReadOnlyCollection<SkillGemsDat> GetSkillGemsDat()
    {
        return SkillGemsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SkillMineVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillMineVariationsDat.</returns>
    public ReadOnlyCollection<SkillMineVariationsDat> GetSkillMineVariationsDat()
    {
        return SkillMineVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SkillMorphDisplayDat data.
    /// </summary>
    /// <returns>readonly collection of SkillMorphDisplayDat.</returns>
    public ReadOnlyCollection<SkillMorphDisplayDat> GetSkillMorphDisplayDat()
    {
        return SkillMorphDisplayDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SkillSurgeEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillSurgeEffectsDat.</returns>
    public ReadOnlyCollection<SkillSurgeEffectsDat> GetSkillSurgeEffectsDat()
    {
        return SkillSurgeEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SkillTotemVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillTotemVariationsDat.</returns>
    public ReadOnlyCollection<SkillTotemVariationsDat> GetSkillTotemVariationsDat()
    {
        return SkillTotemVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SkillTrapVariationsDat data.
    /// </summary>
    /// <returns>readonly collection of SkillTrapVariationsDat.</returns>
    public ReadOnlyCollection<SkillTrapVariationsDat> GetSkillTrapVariationsDat()
    {
        return SkillTrapVariationsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SocketNotchesDat data.
    /// </summary>
    /// <returns>readonly collection of SocketNotchesDat.</returns>
    public ReadOnlyCollection<SocketNotchesDat> GetSocketNotchesDat()
    {
        return SocketNotchesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SoundEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of SoundEffectsDat.</returns>
    public ReadOnlyCollection<SoundEffectsDat> GetSoundEffectsDat()
    {
        return SoundEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SpawnAdditionalChestsOrClustersDat data.
    /// </summary>
    /// <returns>readonly collection of SpawnAdditionalChestsOrClustersDat.</returns>
    public ReadOnlyCollection<SpawnAdditionalChestsOrClustersDat> GetSpawnAdditionalChestsOrClustersDat()
    {
        return SpawnAdditionalChestsOrClustersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SpawnObjectDat data.
    /// </summary>
    /// <returns>readonly collection of SpawnObjectDat.</returns>
    public ReadOnlyCollection<SpawnObjectDat> GetSpawnObjectDat()
    {
        return SpawnObjectDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SpecialRoomsDat data.
    /// </summary>
    /// <returns>readonly collection of SpecialRoomsDat.</returns>
    public ReadOnlyCollection<SpecialRoomsDat> GetSpecialRoomsDat()
    {
        return SpecialRoomsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SpecialTilesDat data.
    /// </summary>
    /// <returns>readonly collection of SpecialTilesDat.</returns>
    public ReadOnlyCollection<SpecialTilesDat> GetSpecialTilesDat()
    {
        return SpecialTilesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SpectreOverridesDat data.
    /// </summary>
    /// <returns>readonly collection of SpectreOverridesDat.</returns>
    public ReadOnlyCollection<SpectreOverridesDat> GetSpectreOverridesDat()
    {
        return SpectreOverridesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StartingPassiveSkillsDat data.
    /// </summary>
    /// <returns>readonly collection of StartingPassiveSkillsDat.</returns>
    public ReadOnlyCollection<StartingPassiveSkillsDat> GetStartingPassiveSkillsDat()
    {
        return StartingPassiveSkillsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StashTabAffinitiesDat data.
    /// </summary>
    /// <returns>readonly collection of StashTabAffinitiesDat.</returns>
    public ReadOnlyCollection<StashTabAffinitiesDat> GetStashTabAffinitiesDat()
    {
        return StashTabAffinitiesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StashTypeDat data.
    /// </summary>
    /// <returns>readonly collection of StashTypeDat.</returns>
    public ReadOnlyCollection<StashTypeDat> GetStashTypeDat()
    {
        return StashTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StatDescriptionFunctionsDat data.
    /// </summary>
    /// <returns>readonly collection of StatDescriptionFunctionsDat.</returns>
    public ReadOnlyCollection<StatDescriptionFunctionsDat> GetStatDescriptionFunctionsDat()
    {
        return StatDescriptionFunctionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StatsAffectingGenerationDat data.
    /// </summary>
    /// <returns>readonly collection of StatsAffectingGenerationDat.</returns>
    public ReadOnlyCollection<StatsAffectingGenerationDat> GetStatsAffectingGenerationDat()
    {
        return StatsAffectingGenerationDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StatsDat data.
    /// </summary>
    /// <returns>readonly collection of StatsDat.</returns>
    public ReadOnlyCollection<StatsDat> GetStatsDat()
    {
        return StatsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StrDexIntMissionExtraRequirementDat data.
    /// </summary>
    /// <returns>readonly collection of StrDexIntMissionExtraRequirementDat.</returns>
    public ReadOnlyCollection<StrDexIntMissionExtraRequirementDat> GetStrDexIntMissionExtraRequirementDat()
    {
        return StrDexIntMissionExtraRequirementDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets StrDexIntMissionsDat data.
    /// </summary>
    /// <returns>readonly collection of StrDexIntMissionsDat.</returns>
    public ReadOnlyCollection<StrDexIntMissionsDat> GetStrDexIntMissionsDat()
    {
        return StrDexIntMissionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SuicideExplosionDat data.
    /// </summary>
    /// <returns>readonly collection of SuicideExplosionDat.</returns>
    public ReadOnlyCollection<SuicideExplosionDat> GetSuicideExplosionDat()
    {
        return SuicideExplosionDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SummonedSpecificBarrelsDat data.
    /// </summary>
    /// <returns>readonly collection of SummonedSpecificBarrelsDat.</returns>
    public ReadOnlyCollection<SummonedSpecificBarrelsDat> GetSummonedSpecificBarrelsDat()
    {
        return SummonedSpecificBarrelsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SummonedSpecificMonstersDat data.
    /// </summary>
    /// <returns>readonly collection of SummonedSpecificMonstersDat.</returns>
    public ReadOnlyCollection<SummonedSpecificMonstersDat> GetSummonedSpecificMonstersDat()
    {
        return SummonedSpecificMonstersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SummonedSpecificMonstersOnDeathDat data.
    /// </summary>
    /// <returns>readonly collection of SummonedSpecificMonstersOnDeathDat.</returns>
    public ReadOnlyCollection<SummonedSpecificMonstersOnDeathDat> GetSummonedSpecificMonstersOnDeathDat()
    {
        return SummonedSpecificMonstersOnDeathDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SupporterPackSetsDat data.
    /// </summary>
    /// <returns>readonly collection of SupporterPackSetsDat.</returns>
    public ReadOnlyCollection<SupporterPackSetsDat> GetSupporterPackSetsDat()
    {
        return SupporterPackSetsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SurgeEffectsDat data.
    /// </summary>
    /// <returns>readonly collection of SurgeEffectsDat.</returns>
    public ReadOnlyCollection<SurgeEffectsDat> GetSurgeEffectsDat()
    {
        return SurgeEffectsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets SurgeTypesDat data.
    /// </summary>
    /// <returns>readonly collection of SurgeTypesDat.</returns>
    public ReadOnlyCollection<SurgeTypesDat> GetSurgeTypesDat()
    {
        return SurgeTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TableChargeDat data.
    /// </summary>
    /// <returns>readonly collection of TableChargeDat.</returns>
    public ReadOnlyCollection<TableChargeDat> GetTableChargeDat()
    {
        return TableChargeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TableMonsterSpawnersDat data.
    /// </summary>
    /// <returns>readonly collection of TableMonsterSpawnersDat.</returns>
    public ReadOnlyCollection<TableMonsterSpawnersDat> GetTableMonsterSpawnersDat()
    {
        return TableMonsterSpawnersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TagsDat data.
    /// </summary>
    /// <returns>readonly collection of TagsDat.</returns>
    public ReadOnlyCollection<TagsDat> GetTagsDat()
    {
        return TagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TalkingPetAudioEventsDat data.
    /// </summary>
    /// <returns>readonly collection of TalkingPetAudioEventsDat.</returns>
    public ReadOnlyCollection<TalkingPetAudioEventsDat> GetTalkingPetAudioEventsDat()
    {
        return TalkingPetAudioEventsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TalkingPetNPCAudioDat data.
    /// </summary>
    /// <returns>readonly collection of TalkingPetNPCAudioDat.</returns>
    public ReadOnlyCollection<TalkingPetNPCAudioDat> GetTalkingPetNPCAudioDat()
    {
        return TalkingPetNPCAudioDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TalkingPetsDat data.
    /// </summary>
    /// <returns>readonly collection of TalkingPetsDat.</returns>
    public ReadOnlyCollection<TalkingPetsDat> GetTalkingPetsDat()
    {
        return TalkingPetsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TencentAutoLootPetCurrenciesDat data.
    /// </summary>
    /// <returns>readonly collection of TencentAutoLootPetCurrenciesDat.</returns>
    public ReadOnlyCollection<TencentAutoLootPetCurrenciesDat> GetTencentAutoLootPetCurrenciesDat()
    {
        return TencentAutoLootPetCurrenciesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TencentAutoLootPetCurrenciesExcludableDat data.
    /// </summary>
    /// <returns>readonly collection of TencentAutoLootPetCurrenciesExcludableDat.</returns>
    public ReadOnlyCollection<TencentAutoLootPetCurrenciesExcludableDat> GetTencentAutoLootPetCurrenciesExcludableDat()
    {
        return TencentAutoLootPetCurrenciesExcludableDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TerrainPluginsDat data.
    /// </summary>
    /// <returns>readonly collection of TerrainPluginsDat.</returns>
    public ReadOnlyCollection<TerrainPluginsDat> GetTerrainPluginsDat()
    {
        return TerrainPluginsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TipsDat data.
    /// </summary>
    /// <returns>readonly collection of TipsDat.</returns>
    public ReadOnlyCollection<TipsDat> GetTipsDat()
    {
        return TipsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TopologiesDat data.
    /// </summary>
    /// <returns>readonly collection of TopologiesDat.</returns>
    public ReadOnlyCollection<TopologiesDat> GetTopologiesDat()
    {
        return TopologiesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TradeMarketCategoryDat data.
    /// </summary>
    /// <returns>readonly collection of TradeMarketCategoryDat.</returns>
    public ReadOnlyCollection<TradeMarketCategoryDat> GetTradeMarketCategoryDat()
    {
        return TradeMarketCategoryDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TradeMarketCategoryGroupsDat data.
    /// </summary>
    /// <returns>readonly collection of TradeMarketCategoryGroupsDat.</returns>
    public ReadOnlyCollection<TradeMarketCategoryGroupsDat> GetTradeMarketCategoryGroupsDat()
    {
        return TradeMarketCategoryGroupsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TradeMarketCategoryListAllClassDat data.
    /// </summary>
    /// <returns>readonly collection of TradeMarketCategoryListAllClassDat.</returns>
    public ReadOnlyCollection<TradeMarketCategoryListAllClassDat> GetTradeMarketCategoryListAllClassDat()
    {
        return TradeMarketCategoryListAllClassDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TradeMarketIndexItemAsDat data.
    /// </summary>
    /// <returns>readonly collection of TradeMarketIndexItemAsDat.</returns>
    public ReadOnlyCollection<TradeMarketIndexItemAsDat> GetTradeMarketIndexItemAsDat()
    {
        return TradeMarketIndexItemAsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TreasureHunterMissionsDat data.
    /// </summary>
    /// <returns>readonly collection of TreasureHunterMissionsDat.</returns>
    public ReadOnlyCollection<TreasureHunterMissionsDat> GetTreasureHunterMissionsDat()
    {
        return TreasureHunterMissionsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TriggerBeamDat data.
    /// </summary>
    /// <returns>readonly collection of TriggerBeamDat.</returns>
    public ReadOnlyCollection<TriggerBeamDat> GetTriggerBeamDat()
    {
        return TriggerBeamDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TriggerSpawnersDat data.
    /// </summary>
    /// <returns>readonly collection of TriggerSpawnersDat.</returns>
    public ReadOnlyCollection<TriggerSpawnersDat> GetTriggerSpawnersDat()
    {
        return TriggerSpawnersDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets TutorialDat data.
    /// </summary>
    /// <returns>readonly collection of TutorialDat.</returns>
    public ReadOnlyCollection<TutorialDat> GetTutorialDat()
    {
        return TutorialDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UITalkTextDat data.
    /// </summary>
    /// <returns>readonly collection of UITalkTextDat.</returns>
    public ReadOnlyCollection<UITalkTextDat> GetUITalkTextDat()
    {
        return UITalkTextDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UniqueChestsDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueChestsDat.</returns>
    public ReadOnlyCollection<UniqueChestsDat> GetUniqueChestsDat()
    {
        return UniqueChestsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UniqueJewelLimitsDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueJewelLimitsDat.</returns>
    public ReadOnlyCollection<UniqueJewelLimitsDat> GetUniqueJewelLimitsDat()
    {
        return UniqueJewelLimitsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UniqueMapInfoDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueMapInfoDat.</returns>
    public ReadOnlyCollection<UniqueMapInfoDat> GetUniqueMapInfoDat()
    {
        return UniqueMapInfoDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UniqueMapsDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueMapsDat.</returns>
    public ReadOnlyCollection<UniqueMapsDat> GetUniqueMapsDat()
    {
        return UniqueMapsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UniqueStashLayoutDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueStashLayoutDat.</returns>
    public ReadOnlyCollection<UniqueStashLayoutDat> GetUniqueStashLayoutDat()
    {
        return UniqueStashLayoutDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets UniqueStashTypesDat data.
    /// </summary>
    /// <returns>readonly collection of UniqueStashTypesDat.</returns>
    public ReadOnlyCollection<UniqueStashTypesDat> GetUniqueStashTypesDat()
    {
        return UniqueStashTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets VirtualStatContextFlagsDat data.
    /// </summary>
    /// <returns>readonly collection of VirtualStatContextFlagsDat.</returns>
    public ReadOnlyCollection<VirtualStatContextFlagsDat> GetVirtualStatContextFlagsDat()
    {
        return VirtualStatContextFlagsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets VoteStateDat data.
    /// </summary>
    /// <returns>readonly collection of VoteStateDat.</returns>
    public ReadOnlyCollection<VoteStateDat> GetVoteStateDat()
    {
        return VoteStateDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets VoteTypeDat data.
    /// </summary>
    /// <returns>readonly collection of VoteTypeDat.</returns>
    public ReadOnlyCollection<VoteTypeDat> GetVoteTypeDat()
    {
        return VoteTypeDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WeaponClassesDat data.
    /// </summary>
    /// <returns>readonly collection of WeaponClassesDat.</returns>
    public ReadOnlyCollection<WeaponClassesDat> GetWeaponClassesDat()
    {
        return WeaponClassesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WeaponImpactSoundDataDat data.
    /// </summary>
    /// <returns>readonly collection of WeaponImpactSoundDataDat.</returns>
    public ReadOnlyCollection<WeaponImpactSoundDataDat> GetWeaponImpactSoundDataDat()
    {
        return WeaponImpactSoundDataDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WeaponTypesDat data.
    /// </summary>
    /// <returns>readonly collection of WeaponTypesDat.</returns>
    public ReadOnlyCollection<WeaponTypesDat> GetWeaponTypesDat()
    {
        return WeaponTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WindowCursorsDat data.
    /// </summary>
    /// <returns>readonly collection of WindowCursorsDat.</returns>
    public ReadOnlyCollection<WindowCursorsDat> GetWindowCursorsDat()
    {
        return WindowCursorsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WordsDat data.
    /// </summary>
    /// <returns>readonly collection of WordsDat.</returns>
    public ReadOnlyCollection<WordsDat> GetWordsDat()
    {
        return WordsDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WorldAreasDat data.
    /// </summary>
    /// <returns>readonly collection of WorldAreasDat.</returns>
    public ReadOnlyCollection<WorldAreasDat> GetWorldAreasDat()
    {
        return WorldAreasDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WorldAreaLeagueChancesDat data.
    /// </summary>
    /// <returns>readonly collection of WorldAreaLeagueChancesDat.</returns>
    public ReadOnlyCollection<WorldAreaLeagueChancesDat> GetWorldAreaLeagueChancesDat()
    {
        return WorldAreaLeagueChancesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets WorldPopupIconTypesDat data.
    /// </summary>
    /// <returns>readonly collection of WorldPopupIconTypesDat.</returns>
    public ReadOnlyCollection<WorldPopupIconTypesDat> GetWorldPopupIconTypesDat()
    {
        return WorldPopupIconTypesDat.Load(dataLoader).AsReadOnly();
    }

    /// <summary>
    /// Gets ZanaLevelsDat data.
    /// </summary>
    /// <returns>readonly collection of ZanaLevelsDat.</returns>
    public ReadOnlyCollection<ZanaLevelsDat> GetZanaLevelsDat()
    {
        return ZanaLevelsDat.Load(dataLoader).AsReadOnly();
    }
}
