// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MapCreationInformation.dat data.
/// </summary>
public sealed partial class MapCreationInformationDat
{
    /// <summary> Gets MapsKey.</summary>
    /// <remarks> references <see cref="MapsDat"/> on <see cref="Specification.LoadMapsDat"/> index.</remarks>
    public required int? MapsKey { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }
}
