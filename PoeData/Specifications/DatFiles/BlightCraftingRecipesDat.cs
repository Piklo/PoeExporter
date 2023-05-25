// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BlightCraftingRecipes.dat data.
/// </summary>
public sealed partial class BlightCraftingRecipesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BlightCraftingItemsKeys.</summary>
    /// <remarks> references <see cref="BlightCraftingItemsDat"/> on <see cref="Specification.LoadBlightCraftingItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BlightCraftingItemsKeys { get; init; }

    /// <summary> Gets BlightCraftingResultsKey.</summary>
    /// <remarks> references <see cref="BlightCraftingResultsDat"/> on <see cref="Specification.LoadBlightCraftingResultsDat"/> index.</remarks>
    public required int? BlightCraftingResultsKey { get; init; }

    /// <summary> Gets BlightCraftingTypesKey.</summary>
    /// <remarks> references <see cref="BlightCraftingTypesDat"/> on <see cref="Specification.LoadBlightCraftingTypesDat"/> index.</remarks>
    public required int? BlightCraftingTypesKey { get; init; }
}
