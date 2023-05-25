// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AtlasAwakeningStats.dat data.
/// </summary>
public sealed partial class AtlasAwakeningStatsDat
{
    /// <summary> Gets AwakeningLevel.</summary>
    public required int AwakeningLevel { get; init; }

    /// <summary> Gets Stats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Stats { get; init; }

    /// <summary> Gets Values.</summary>
    public required ReadOnlyCollection<int> Values { get; init; }
}
