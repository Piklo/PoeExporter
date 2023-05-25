// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HellscapePassiveTree.dat data.
/// </summary>
public sealed partial class HellscapePassiveTreeDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets AllocationsRequired.</summary>
    public required int AllocationsRequired { get; init; }

    /// <summary> Gets Passives.</summary>
    /// <remarks> references <see cref="HellscapePassivesDat"/> on <see cref="Specification.LoadHellscapePassivesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Passives { get; init; }
}
