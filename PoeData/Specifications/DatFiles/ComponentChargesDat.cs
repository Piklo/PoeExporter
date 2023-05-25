// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ComponentCharges.dat data.
/// </summary>
public sealed partial class ComponentChargesDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="BaseItemTypesDat.Id"/>.</remarks>
    public required string BaseItemTypesKey { get; init; }

    /// <summary> Gets MaxCharges.</summary>
    public required int MaxCharges { get; init; }

    /// <summary> Gets PerCharge.</summary>
    public required int PerCharge { get; init; }

    /// <summary> Gets MaxCharges2.</summary>
    public required int MaxCharges2 { get; init; }

    /// <summary> Gets PerCharge2.</summary>
    public required int PerCharge2 { get; init; }
}
