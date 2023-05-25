// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing RecipeUnlockDisplay.dat data.
/// </summary>
public sealed partial class RecipeUnlockDisplayDat
{
    /// <summary> Gets RecipeId.</summary>
    public required int RecipeId { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets CraftingItemClassCategoriesKeys.</summary>
    /// <remarks> references <see cref="CraftingItemClassCategoriesDat"/> on <see cref="Specification.LoadCraftingItemClassCategoriesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> CraftingItemClassCategoriesKeys { get; init; }

    /// <summary> Gets UnlockDescription.</summary>
    public required string UnlockDescription { get; init; }

    /// <summary> Gets Rank.</summary>
    public required int Rank { get; init; }

    /// <summary> Gets UnlockArea.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? UnlockArea { get; init; }
}
