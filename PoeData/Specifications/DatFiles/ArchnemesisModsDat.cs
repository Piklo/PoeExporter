// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ArchnemesisMods.dat data.
/// </summary>
public sealed partial class ArchnemesisModsDat
{
    /// <summary> Gets Mod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? Mod { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Visual.</summary>
    /// <remarks> references <see cref="ArchnemesisModVisualsDat"/> on <see cref="Specification.LoadArchnemesisModVisualsDat"/> index.</remarks>
    public required int? Visual { get; init; }

    /// <summary> Gets TextStyles.</summary>
    public required ReadOnlyCollection<string> TextStyles { get; init; }

    /// <summary> Gets a value indicating whether Unknown56 is set.</summary>
    public required bool Unknown56 { get; init; }

    /// <summary> Gets a value indicating whether Unknown57 is set.</summary>
    public required bool Unknown57 { get; init; }
}
