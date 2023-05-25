// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing EnvironmentTransitions.dat data.
/// </summary>
public sealed partial class EnvironmentTransitionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets OTFiles.</summary>
    public required ReadOnlyCollection<string> OTFiles { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required ReadOnlyCollection<string> Unknown24 { get; init; }
}
