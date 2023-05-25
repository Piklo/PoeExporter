// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DropPool.dat data.
/// </summary>
public sealed partial class DropPoolDat
{
    /// <summary> Gets Group.</summary>
    public required string Group { get; init; }

    /// <summary> Gets Weight.</summary>
    public required int Weight { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required ReadOnlyCollection<int> Unknown12 { get; init; }

    /// <summary> Gets WeightHardmode.</summary>
    public required int WeightHardmode { get; init; }
}
