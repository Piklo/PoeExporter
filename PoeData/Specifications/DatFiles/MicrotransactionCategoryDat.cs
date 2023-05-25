// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MicrotransactionCategory.dat data.
/// </summary>
public sealed partial class MicrotransactionCategoryDat
{
    /// <summary> Gets Id.</summary>
    /// <remarks> references <see cref="MicrotransactionCategoryIdDat"/> on <see cref="Specification.LoadMicrotransactionCategoryIdDat"/> index.</remarks>
    public required int Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }
}
