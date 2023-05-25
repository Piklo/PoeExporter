// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ItemNoteCode.dat data.
/// </summary>
public sealed partial class ItemNoteCodeDat
{
    /// <summary> Gets BaseItem.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItem { get; init; }

    /// <summary> Gets Code.</summary>
    public required string Code { get; init; }

    /// <summary> Gets Order1.</summary>
    public required int Order1 { get; init; }

    /// <summary> Gets a value indicating whether Show is set.</summary>
    public required bool Show { get; init; }

    /// <summary> Gets Order2.</summary>
    public required int Order2 { get; init; }
}
