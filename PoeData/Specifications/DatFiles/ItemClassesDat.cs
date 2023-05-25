// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ItemClasses.dat data.
/// </summary>
public sealed partial class ItemClassesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets TradeMarketCategory.</summary>
    /// <remarks> references <see cref="TradeMarketCategoryDat"/> on <see cref="Specification.LoadTradeMarketCategoryDat"/> index.</remarks>
    public required int? TradeMarketCategory { get; init; }

    /// <summary> Gets ItemClassCategory.</summary>
    /// <remarks> references <see cref="ItemClassCategoriesDat"/> on <see cref="Specification.LoadItemClassCategoriesDat"/> index.</remarks>
    public required int? ItemClassCategory { get; init; }

    /// <summary> Gets a value indicating whether RemovedIfLeavesArea is set.</summary>
    public required bool RemovedIfLeavesArea { get; init; }

    /// <summary> Gets Unknown49.</summary>
    public required ReadOnlyCollection<int> Unknown49 { get; init; }

    /// <summary> Gets IdentifyAchievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> IdentifyAchievements { get; init; }

    /// <summary> Gets a value indicating whether AllocateToMapOwner is set.</summary>
    public required bool AllocateToMapOwner { get; init; }

    /// <summary> Gets a value indicating whether AlwaysAllocate is set.</summary>
    public required bool AlwaysAllocate { get; init; }

    /// <summary> Gets a value indicating whether CanHaveVeiledMods is set.</summary>
    public required bool CanHaveVeiledMods { get; init; }

    /// <summary> Gets PickedUpQuest.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.LoadQuestFlagsDat"/> index.</remarks>
    public required int? PickedUpQuest { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required int Unknown100 { get; init; }

    /// <summary> Gets a value indicating whether AlwaysShow is set.</summary>
    public required bool AlwaysShow { get; init; }

    /// <summary> Gets a value indicating whether CanBeCorrupted is set.</summary>
    public required bool CanBeCorrupted { get; init; }

    /// <summary> Gets a value indicating whether CanHaveIncubators is set.</summary>
    public required bool CanHaveIncubators { get; init; }

    /// <summary> Gets a value indicating whether CanHaveInfluence is set.</summary>
    public required bool CanHaveInfluence { get; init; }

    /// <summary> Gets a value indicating whether CanBeDoubleCorrupted is set.</summary>
    public required bool CanBeDoubleCorrupted { get; init; }

    /// <summary> Gets a value indicating whether CanHaveAspects is set.</summary>
    public required bool CanHaveAspects { get; init; }

    /// <summary> Gets a value indicating whether CanTransferSkin is set.</summary>
    public required bool CanTransferSkin { get; init; }

    /// <summary> Gets ItemStance.</summary>
    /// <remarks> references <see cref="ItemStancesDat"/> on <see cref="Specification.LoadItemStancesDat"/> index.</remarks>
    public required int? ItemStance { get; init; }

    /// <summary> Gets a value indicating whether CanScourge is set.</summary>
    public required bool CanScourge { get; init; }

    /// <summary> Gets a value indicating whether CanUpgradeRarity is set.</summary>
    public required bool CanUpgradeRarity { get; init; }

    /// <summary> Gets a value indicating whether Unknown129 is set.</summary>
    public required bool Unknown129 { get; init; }

    /// <summary> Gets a value indicating whether Unknown130 is set.</summary>
    public required bool Unknown130 { get; init; }

    /// <summary> Gets MaxInventoryDimensions.</summary>
    public required ReadOnlyCollection<int> MaxInventoryDimensions { get; init; }

    /// <summary> Gets Flags.</summary>
    /// <remarks> references <see cref="ItemClassFlagsDat"/> on <see cref="Specification.LoadItemClassFlagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Flags { get; init; }

    /// <summary> Gets a value indicating whether IsUnmodifiable is set.</summary>
    public required bool IsUnmodifiable { get; init; }

    /// <summary> Gets a value indicating whether CanBeFractured is set.</summary>
    public required bool CanBeFractured { get; init; }

    /// <summary> Gets EquipAchievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? EquipAchievements { get; init; }

    /// <summary> Gets a value indicating whether Unknown181 is set.</summary>
    public required bool Unknown181 { get; init; }
}
