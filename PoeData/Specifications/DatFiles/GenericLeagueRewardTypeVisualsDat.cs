// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing GenericLeagueRewardTypeVisuals.dat data.
/// </summary>
public sealed partial class GenericLeagueRewardTypeVisualsDat
{
    /// <summary> Gets Type.</summary>
    /// <remarks> references <see cref="GenericLeagueRewardTypesDat"/> on <see cref="Specification.LoadGenericLeagueRewardTypesDat"/> index.</remarks>
    public required int? Type { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int? Unknown16 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int? Unknown32 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required float Unknown48 { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }
}
