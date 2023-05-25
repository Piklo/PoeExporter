// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing StashTabAffinities.dat data.
/// </summary>
public sealed partial class StashTabAffinitiesDat
{
    /// <summary> Gets SpecializedStash.</summary>
    /// <remarks> references <see cref="StashIdDat"/> on <see cref="Specification.LoadStashIdDat"/> index.</remarks>
    public required int SpecializedStash { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets ShowInStashes.</summary>
    /// <remarks> references <see cref="StashIdDat"/> on <see cref="Specification.LoadStashIdDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ShowInStashes { get; init; }
}
