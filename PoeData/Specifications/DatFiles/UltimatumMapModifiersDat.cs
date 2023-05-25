// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing UltimatumMapModifiers.dat data.
/// </summary>
public sealed partial class UltimatumMapModifiersDat
{
    /// <summary> Gets Stat.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Stat { get; init; }

    /// <summary> Gets Mods.</summary>
    /// <remarks> references <see cref="UltimatumModifiersDat"/> on <see cref="Specification.LoadUltimatumModifiersDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Mods { get; init; }
}
