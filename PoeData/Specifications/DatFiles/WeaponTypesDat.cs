// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing WeaponTypes.dat data.
/// </summary>
public sealed partial class WeaponTypesDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets Critical.</summary>
    public required int Critical { get; init; }

    /// <summary> Gets Speed.</summary>
    public required int Speed { get; init; }

    /// <summary> Gets DamageMin.</summary>
    public required int DamageMin { get; init; }

    /// <summary> Gets DamageMax.</summary>
    public required int DamageMax { get; init; }

    /// <summary> Gets RangeMax.</summary>
    public required int RangeMax { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }
}
