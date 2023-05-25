// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SentinelDroneInventoryLayout.dat data.
/// </summary>
public sealed partial class SentinelDroneInventoryLayoutDat
{
    /// <summary> Gets DroneType.</summary>
    /// <remarks> references <see cref="DroneTypesDat"/> on <see cref="Specification.LoadDroneTypesDat"/> index.</remarks>
    public required int? DroneType { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int Unknown20 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }
}
