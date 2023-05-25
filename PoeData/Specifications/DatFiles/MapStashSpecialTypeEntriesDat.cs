// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MapStashSpecialTypeEntries.dat data.
/// </summary>
public sealed partial class MapStashSpecialTypeEntriesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets MapItem.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? MapItem { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary> Gets Icon2.</summary>
    public required string Icon2 { get; init; }

    /// <summary> Gets a value indicating whether IsShaperGuardian is set.</summary>
    public required bool IsShaperGuardian { get; init; }

    /// <summary> Gets a value indicating whether IsElderGuardian is set.</summary>
    public required bool IsElderGuardian { get; init; }
}
