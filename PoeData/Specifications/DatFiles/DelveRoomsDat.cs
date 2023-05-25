// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DelveRooms.dat data.
/// </summary>
public sealed partial class DelveRoomsDat
{
    /// <summary> Gets DelveBiomesKey.</summary>
    /// <remarks> references <see cref="DelveBiomesDat"/> on <see cref="Specification.LoadDelveBiomesDat"/> index.</remarks>
    public required int? DelveBiomesKey { get; init; }

    /// <summary> Gets DelveFeaturesKey.</summary>
    /// <remarks> references <see cref="DelveFeaturesDat"/> on <see cref="Specification.LoadDelveFeaturesDat"/> index.</remarks>
    public required int? DelveFeaturesKey { get; init; }

    /// <summary> Gets ARMFile.</summary>
    public required string ARMFile { get; init; }
}
