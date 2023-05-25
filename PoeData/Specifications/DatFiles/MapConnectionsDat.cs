// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MapConnections.dat data.
/// </summary>
public sealed partial class MapConnectionsDat
{
    /// <summary> Gets MapPinsKey0.</summary>
    /// <remarks> references <see cref="MapPinsDat"/> on <see cref="Specification.LoadMapPinsDat"/> index.</remarks>
    public required int? MapPinsKey0 { get; init; }

    /// <summary> Gets MapPinsKey1.</summary>
    /// <remarks> references <see cref="MapPinsDat"/> on <see cref="Specification.LoadMapPinsDat"/> index.</remarks>
    public required int? MapPinsKey1 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int? Unknown32 { get; init; }

    /// <summary> Gets RestrictedAreaText.</summary>
    public required string RestrictedAreaText { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int? Unknown56 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int? Unknown72 { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int? Unknown88 { get; init; }

    /// <summary> Gets Unknown104.</summary>
    public required ReadOnlyCollection<int> Unknown104 { get; init; }
}
