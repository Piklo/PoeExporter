// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistChestRewardTypes.dat data.
/// </summary>
public sealed partial class HeistChestRewardTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Art.</summary>
    public required string Art { get; init; }

    /// <summary> Gets RewardTypeName.</summary>
    public required string RewardTypeName { get; init; }

    /// <summary> Gets Unknown24.</summary>
    /// <remarks> references <see cref="HeistChestRewardTypesDat"/> on <see cref="Specification.LoadHeistChestRewardTypesDat"/> index.</remarks>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets RewardRoomName.</summary>
    public required string RewardRoomName { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets Weight.</summary>
    public required int Weight { get; init; }

    /// <summary> Gets RewardRoomName2.</summary>
    public required string RewardRoomName2 { get; init; }

    /// <summary> Gets HeistJobsKey.</summary>
    /// <remarks> references <see cref="HeistJobsDat"/> on <see cref="Specification.LoadHeistJobsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> HeistJobsKey { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required int Unknown76 { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required int Unknown80 { get; init; }
}
