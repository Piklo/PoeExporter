// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing CraftingBenchSortCategories.dat data.
/// </summary>
public sealed partial class CraftingBenchSortCategoriesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required int? Name { get; init; }

    /// <summary> Gets a value indicating whether IsVisible is set.</summary>
    public required bool IsVisible { get; init; }
}
