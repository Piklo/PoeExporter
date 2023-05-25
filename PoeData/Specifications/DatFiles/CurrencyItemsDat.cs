// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing CurrencyItems.dat data.
/// </summary>
public sealed partial class CurrencyItemsDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets Stacks.</summary>
    public required int Stacks { get; init; }

    /// <summary> Gets CurrencyUseType.</summary>
    public required int CurrencyUseType { get; init; }

    /// <summary> Gets Action.</summary>
    public required string Action { get; init; }

    /// <summary> Gets Directions.</summary>
    public required string Directions { get; init; }

    /// <summary> Gets FullStack_BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? FullStack_BaseItemTypesKey { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets Usage_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Usage_AchievementItemsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown80 is set.</summary>
    public required bool Unknown80 { get; init; }

    /// <summary> Gets CosmeticTypeName.</summary>
    public required string CosmeticTypeName { get; init; }

    /// <summary> Gets Possession_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? Possession_AchievementItemsKey { get; init; }

    /// <summary> Gets Unknown105.</summary>
    public required ReadOnlyCollection<int> Unknown105 { get; init; }

    /// <summary> Gets Unknown121.</summary>
    public required ReadOnlyCollection<int> Unknown121 { get; init; }

    /// <summary> Gets CurrencyTab_StackSize.</summary>
    public required int CurrencyTab_StackSize { get; init; }

    /// <summary> Gets XBoxDirections.</summary>
    public required string XBoxDirections { get; init; }

    /// <summary> Gets Unknown149.</summary>
    public required int Unknown149 { get; init; }

    /// <summary> Gets Unknown153.</summary>
    public required int? Unknown153 { get; init; }

    /// <summary> Gets Unknown169.</summary>
    public required int? Unknown169 { get; init; }

    /// <summary> Gets ModifyMapsAchievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModifyMapsAchievements { get; init; }

    /// <summary> Gets ModifyContractsAchievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModifyContractsAchievements { get; init; }

    /// <summary> Gets CombineAchievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> CombineAchievements { get; init; }

    /// <summary> Gets Unknown233.</summary>
    public required int Unknown233 { get; init; }

    /// <summary> Gets Unknown237.</summary>
    public required ReadOnlyCollection<int> Unknown237 { get; init; }

    /// <summary> Gets ShopTag.</summary>
    /// <remarks> references <see cref="ShopTagDat"/> on <see cref="Specification.LoadShopTagDat"/> index.</remarks>
    public required int? ShopTag { get; init; }

    /// <summary> Gets a value indicating whether IsHardmode is set.</summary>
    public required bool IsHardmode { get; init; }

    /// <summary> Gets DescriptionHardmode.</summary>
    public required string DescriptionHardmode { get; init; }

    /// <summary> Gets a value indicating whether IsGold is set.</summary>
    public required bool IsGold { get; init; }
}
