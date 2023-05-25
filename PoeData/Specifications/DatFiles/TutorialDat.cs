// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Tutorial.dat data.
/// </summary>
public sealed partial class TutorialDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets UIFile.</summary>
    public required string UIFile { get; init; }

    /// <summary> Gets ClientString.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required int? ClientString { get; init; }

    /// <summary> Gets a value indicating whether IsEnabled is set.</summary>
    public required bool IsEnabled { get; init; }

    /// <summary> Gets Unknown33.</summary>
    public required int Unknown33 { get; init; }

    /// <summary> Gets Unknown37.</summary>
    public required ReadOnlyCollection<int> Unknown37 { get; init; }

    /// <summary> Gets Unknown53.</summary>
    public required int? Unknown53 { get; init; }

    /// <summary> Gets Unknown69.</summary>
    public required int Unknown69 { get; init; }

    /// <summary> Gets Unknown73.</summary>
    public required ReadOnlyCollection<int> Unknown73 { get; init; }

    /// <summary> Gets a value indicating whether Unknown89 is set.</summary>
    public required bool Unknown89 { get; init; }

    /// <summary> Gets a value indicating whether Unknown90 is set.</summary>
    public required bool Unknown90 { get; init; }

    /// <summary> Gets Unknown91.</summary>
    public required int Unknown91 { get; init; }
}
