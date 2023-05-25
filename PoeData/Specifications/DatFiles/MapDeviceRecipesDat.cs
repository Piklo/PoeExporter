// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MapDeviceRecipes.dat data.
/// </summary>
public sealed partial class MapDeviceRecipesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets RecipeItems.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> RecipeItems { get; init; }

    /// <summary> Gets WorldArea.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? WorldArea { get; init; }

    /// <summary> Gets MicrotransactionPortalVariation.</summary>
    /// <remarks> references <see cref="MicrotransactionPortalVariationsDat"/> on <see cref="Specification.LoadMicrotransactionPortalVariationsDat"/> index.</remarks>
    public required int? MicrotransactionPortalVariation { get; init; }

    /// <summary> Gets AreaLevel.</summary>
    public required int AreaLevel { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int? Unknown60 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required int Unknown76 { get; init; }

    /// <summary> Gets a value indicating whether Unknown80 is set.</summary>
    public required bool Unknown80 { get; init; }

    /// <summary> Gets a value indicating whether Unknown81 is set.</summary>
    public required bool Unknown81 { get; init; }

    /// <summary> Gets a value indicating whether Unknown82 is set.</summary>
    public required bool Unknown82 { get; init; }

    /// <summary> Gets OpenAchievemnts.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> OpenAchievemnts { get; init; }
}
