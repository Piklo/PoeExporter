// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BestiaryRecipes.dat data.
/// </summary>
public sealed partial class BestiaryRecipesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets BestiaryRecipeComponentKeys.</summary>
    /// <remarks> references <see cref="BestiaryRecipeComponentDat"/> on <see cref="Specification.LoadBestiaryRecipeComponentDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BestiaryRecipeComponentKeys { get; init; }

    /// <summary> Gets Notes.</summary>
    public required string Notes { get; init; }

    /// <summary> Gets Category.</summary>
    /// <remarks> references <see cref="BestiaryRecipeCategoriesDat"/> on <see cref="Specification.LoadBestiaryRecipeCategoriesDat"/> index.</remarks>
    public required int? Category { get; init; }

    /// <summary> Gets a value indicating whether Unknown56 is set.</summary>
    public required bool Unknown56 { get; init; }

    /// <summary> Gets Achievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievements { get; init; }

    /// <summary> Gets a value indicating whether Unknown73 is set.</summary>
    public required bool Unknown73 { get; init; }

    /// <summary> Gets Unknown74.</summary>
    public required int Unknown74 { get; init; }

    /// <summary> Gets Unknown78.</summary>
    public required int Unknown78 { get; init; }

    /// <summary> Gets Unknown82.</summary>
    public required int Unknown82 { get; init; }

    /// <summary> Gets FlaskMod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? FlaskMod { get; init; }
}
