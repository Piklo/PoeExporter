// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing QuestItems.dat data.
/// </summary>
public sealed partial class QuestItemsDat
{
    /// <summary> Gets Item.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? Item { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int? Unknown16 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int? Unknown32 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int? Unknown52 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required ReadOnlyCollection<int> Unknown68 { get; init; }

    /// <summary> Gets a value indicating whether Unknown84 is set.</summary>
    public required bool Unknown84 { get; init; }

    /// <summary> Gets a value indicating whether Unknown85 is set.</summary>
    public required bool Unknown85 { get; init; }

    /// <summary> Gets Unknown86.</summary>
    public required int? Unknown86 { get; init; }

    /// <summary> Gets Unknown102.</summary>
    public required int Unknown102 { get; init; }

    /// <summary> Gets Unknown106.</summary>
    public required int? Unknown106 { get; init; }

    /// <summary> Gets Script.</summary>
    public required string Script { get; init; }

    /// <summary> Gets Unknown130.</summary>
    public required int? Unknown130 { get; init; }
}
