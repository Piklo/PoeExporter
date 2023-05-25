// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing IncursionChestRewards.dat data.
/// </summary>
public sealed partial class IncursionChestRewardsDat
{
    /// <summary> Gets IncursionRoomsKey.</summary>
    /// <remarks> references <see cref="IncursionRoomsDat"/> on <see cref="Specification.LoadIncursionRoomsDat"/> index.</remarks>
    public required int? IncursionRoomsKey { get; init; }

    /// <summary> Gets IncursionChestsKeys.</summary>
    /// <remarks> references <see cref="IncursionChestsDat"/> on <see cref="Specification.LoadIncursionChestsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> IncursionChestsKeys { get; init; }

    /// <summary> Gets ChestMarkerMetadata.</summary>
    public required string ChestMarkerMetadata { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }
}
