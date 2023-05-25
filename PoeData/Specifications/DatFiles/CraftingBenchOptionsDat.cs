// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing CraftingBenchOptions.dat data.
/// </summary>
public sealed partial class CraftingBenchOptionsDat
{
    /// <summary> Gets HideoutNPCsKey.</summary>
    /// <remarks> references <see cref="HideoutNPCsDat"/> on <see cref="Specification.LoadHideoutNPCsDat"/> index.</remarks>
    public required int? HideoutNPCsKey { get; init; }

    /// <summary> Gets Order.</summary>
    public required int Order { get; init; }

    /// <summary> Gets AddMod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? AddMod { get; init; }

    /// <summary> Gets Cost_BaseItemTypes.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Cost_BaseItemTypes { get; init; }

    /// <summary> Gets Cost_Values.</summary>
    public required ReadOnlyCollection<int> Cost_Values { get; init; }

    /// <summary> Gets RequiredLevel.</summary>
    public required int RequiredLevel { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets CraftingBenchCustomAction.</summary>
    /// <remarks> references <see cref="CraftingBenchCustomActionsDat"/> on <see cref="Specification.LoadCraftingBenchCustomActionsDat"/> index.</remarks>
    public required int CraftingBenchCustomAction { get; init; }

    /// <summary> Gets ItemClasses.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.LoadItemClassesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ItemClasses { get; init; }

    /// <summary> Gets Links.</summary>
    public required int Links { get; init; }

    /// <summary> Gets SocketColours.</summary>
    public required string SocketColours { get; init; }

    /// <summary> Gets Sockets.</summary>
    public required int Sockets { get; init; }

    /// <summary> Gets ItemQuantity.</summary>
    public required int ItemQuantity { get; init; }

    /// <summary> Gets Unknown120.</summary>
    public required ReadOnlyCollection<int> Unknown120 { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets a value indicating whether IsDisabled is set.</summary>
    public required bool IsDisabled { get; init; }

    /// <summary> Gets a value indicating whether IsAreaOption is set.</summary>
    public required bool IsAreaOption { get; init; }

    /// <summary> Gets RecipeIds.</summary>
    /// <remarks> references <see cref="RecipeUnlockDisplayDat"/> on <see cref="RecipeUnlockDisplayDat.RecipeId"/>.</remarks>
    public required ReadOnlyCollection<int> RecipeIds { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets CraftingItemClassCategories.</summary>
    /// <remarks> references <see cref="CraftingItemClassCategoriesDat"/> on <see cref="Specification.LoadCraftingItemClassCategoriesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> CraftingItemClassCategories { get; init; }

    /// <summary> Gets Unknown182.</summary>
    public required int Unknown182 { get; init; }

    /// <summary> Gets UnlockCategory.</summary>
    /// <remarks> references <see cref="CraftingBenchUnlockCategoriesDat"/> on <see cref="Specification.LoadCraftingBenchUnlockCategoriesDat"/> index.</remarks>
    public required int? UnlockCategory { get; init; }

    /// <summary> Gets UnveilsRequired.</summary>
    public required int UnveilsRequired { get; init; }

    /// <summary> Gets UnveilsRequired2.</summary>
    public required int UnveilsRequired2 { get; init; }

    /// <summary> Gets Unknown210.</summary>
    public required ReadOnlyCollection<int> Unknown210 { get; init; }

    /// <summary> Gets Unknown226.</summary>
    public required ReadOnlyCollection<int> Unknown226 { get; init; }

    /// <summary> Gets Unknown242.</summary>
    public required int Unknown242 { get; init; }

    /// <summary> Gets Unknown246.</summary>
    public required int Unknown246 { get; init; }

    /// <summary> Gets Unknown250.</summary>
    public required int? Unknown250 { get; init; }

    /// <summary> Gets AddEnchantment.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? AddEnchantment { get; init; }

    /// <summary> Gets SortCategory.</summary>
    /// <remarks> references <see cref="CraftingBenchSortCategoriesDat"/> on <see cref="Specification.LoadCraftingBenchSortCategoriesDat"/> index.</remarks>
    public required int? SortCategory { get; init; }

    /// <summary> Gets Unknown298.</summary>
    public required int? Unknown298 { get; init; }

    /// <summary> Gets a value indicating whether Unknown314 is set.</summary>
    public required bool Unknown314 { get; init; }

    /// <summary> Gets Unknown315.</summary>
    public required int Unknown315 { get; init; }

    /// <summary> Gets Unknown319.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Unknown319 { get; init; }

    /// <summary> Gets Unknown335.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Unknown335 { get; init; }

    /// <summary> Gets Unknown351.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Unknown351 { get; init; }
}
