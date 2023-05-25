// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Environments.dat data.
/// </summary>
public sealed partial class EnvironmentsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Base_ENVFile.</summary>
    public required string Base_ENVFile { get; init; }

    /// <summary> Gets Corrupted_ENVFile.</summary>
    public required string Corrupted_ENVFile { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int? Unknown40 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required ReadOnlyCollection<int> Unknown56 { get; init; }

    /// <summary> Gets EnvironmentTransitionsKey.</summary>
    /// <remarks> references <see cref="EnvironmentTransitionsDat"/> on <see cref="Specification.LoadEnvironmentTransitionsDat"/> index.</remarks>
    public required int? EnvironmentTransitionsKey { get; init; }

    /// <summary> Gets PreloadGroup.</summary>
    /// <remarks> references <see cref="PreloadGroupsDat"/> on <see cref="Specification.LoadPreloadGroupsDat"/> index.</remarks>
    public required int? PreloadGroup { get; init; }
}
