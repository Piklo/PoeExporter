// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BaseItemTypes.dat data.
/// </summary>
public sealed partial class BaseItemTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ItemClassesKey.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.LoadItemClassesDat"/> index.</remarks>
    public required int? ItemClassesKey { get; init; }

    /// <summary> Gets Width.</summary>
    public required int Width { get; init; }

    /// <summary> Gets Height.</summary>
    public required int Height { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets InheritsFrom.</summary>
    public required string InheritsFrom { get; init; }

    /// <summary> Gets DropLevel.</summary>
    public required int DropLevel { get; init; }

    /// <summary> Gets FlavourTextKey.</summary>
    /// <remarks> references <see cref="FlavourTextDat"/> on <see cref="Specification.LoadFlavourTextDat"/> index.</remarks>
    public required int? FlavourTextKey { get; init; }

    /// <summary> Gets Implicit_ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Implicit_ModsKeys { get; init; }

    /// <summary> Gets SizeOnGround.</summary>
    public required int SizeOnGround { get; init; }

    /// <summary> Gets SoundEffect.</summary>
    /// <remarks> references <see cref="SoundEffectsDat"/> on <see cref="Specification.LoadSoundEffectsDat"/> index.</remarks>
    public required int? SoundEffect { get; init; }

    /// <summary> Gets TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TagsKeys { get; init; }

    /// <summary> Gets ModDomain.</summary>
    /// <remarks> references <see cref="ModDomainsDat"/> on <see cref="Specification.LoadModDomainsDat"/> index.</remarks>
    public required int ModDomain { get; init; }

    /// <summary> Gets SiteVisibility.</summary>
    /// <remarks> references <see cref="BaseItemTypeVisibilityDat"/> on <see cref="Specification.LoadBaseItemTypeVisibilityDat"/> index.</remarks>
    public required int SiteVisibility { get; init; }

    /// <summary> Gets ItemVisualIdentity.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required int? ItemVisualIdentity { get; init; }

    /// <summary> Gets HASH32.</summary>
    public required int HASH32 { get; init; }

    /// <summary> Gets VendorRecipe_AchievementItems.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> VendorRecipe_AchievementItems { get; init; }

    /// <summary> Gets Inflection.</summary>
    public required string Inflection { get; init; }

    /// <summary> Gets Equip_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? Equip_AchievementItemsKey { get; init; }

    /// <summary> Gets a value indicating whether IsCorrupted is set.</summary>
    public required bool IsCorrupted { get; init; }

    /// <summary> Gets Identify_AchievementItems.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Identify_AchievementItems { get; init; }

    /// <summary> Gets IdentifyMagic_AchievementItems.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> IdentifyMagic_AchievementItems { get; init; }

    /// <summary> Gets FragmentBaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? FragmentBaseItemTypesKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown229 is set.</summary>
    public required bool Unknown229 { get; init; }

    /// <summary> Gets Unknown230.</summary>
    public required int? Unknown230 { get; init; }

    /// <summary> Gets Unknown246.</summary>
    public required int? Unknown246 { get; init; }

    /// <summary> Gets a value indicating whether Unknown262 is set.</summary>
    public required bool Unknown262 { get; init; }

    /// <summary> Gets TradeMarketCategory.</summary>
    /// <remarks> references <see cref="TradeMarketCategoryDat"/> on <see cref="Specification.LoadTradeMarketCategoryDat"/> index.</remarks>
    public required int? TradeMarketCategory { get; init; }

    /// <summary> Gets a value indicating whether Unknown279 is set.</summary>
    public required bool Unknown279 { get; init; }

    /// <summary> Gets Achievement.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievement { get; init; }
}
