// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MonsterHeightBrackets.dat data.
/// </summary>
public sealed partial class MonsterHeightBracketsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets BuffVisuals1.</summary>
    /// <remarks> references <see cref="BuffVisualsDat"/> on <see cref="Specification.LoadBuffVisualsDat"/> index.</remarks>
    public required int? BuffVisuals1 { get; init; }

    /// <summary> Gets BuffVisuals2.</summary>
    /// <remarks> references <see cref="BuffVisualsDat"/> on <see cref="Specification.LoadBuffVisualsDat"/> index.</remarks>
    public required int? BuffVisuals2 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required float Unknown48 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required float Unknown52 { get; init; }

    /// <summary> Gets Tag.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required int? Tag { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required float Unknown76 { get; init; }
}
