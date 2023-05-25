// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ArchnemesisRecipes.dat data.
/// </summary>
public sealed partial class ArchnemesisRecipesDat
{
    /// <summary> Gets Result.</summary>
    /// <remarks> references <see cref="ArchnemesisModsDat"/> on <see cref="Specification.LoadArchnemesisModsDat"/> index.</remarks>
    public required int? Result { get; init; }

    /// <summary> Gets Recipe.</summary>
    /// <remarks> references <see cref="ArchnemesisModsDat"/> on <see cref="Specification.LoadArchnemesisModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Recipe { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }
}
