// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SentinelPassiveTypes.dat data.
/// </summary>
public sealed partial class SentinelPassiveTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets DefaultIcon.</summary>
    public required string DefaultIcon { get; init; }

    /// <summary> Gets ActiveIcon.</summary>
    public required string ActiveIcon { get; init; }

    /// <summary> Gets DroneType.</summary>
    /// <remarks> references <see cref="DroneTypesDat"/> on <see cref="Specification.LoadDroneTypesDat"/> index.</remarks>
    public required int? DroneType { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }
}
