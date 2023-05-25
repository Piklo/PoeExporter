// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Footprints.dat data.
/// </summary>
public sealed partial class FootprintsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Active_AOFiles.</summary>
    public required ReadOnlyCollection<string> Active_AOFiles { get; init; }

    /// <summary> Gets Idle_AOFiles.</summary>
    public required ReadOnlyCollection<string> Idle_AOFiles { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required ReadOnlyCollection<string> Unknown40 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required ReadOnlyCollection<string> Unknown56 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }
}
