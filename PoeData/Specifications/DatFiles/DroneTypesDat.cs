// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DroneTypes.dat data.
/// </summary>
public sealed partial class DroneTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.LoadQuestFlagsDat"/> index.</remarks>
    public required int? Unknown8 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets DeployText.</summary>
    public required string DeployText { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required string Unknown48 { get; init; }

    /// <summary> Gets UnlockedStat.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? UnlockedStat { get; init; }

    /// <summary> Gets SocketableText.</summary>
    public required string SocketableText { get; init; }

    /// <summary> Gets NotPoweredText.</summary>
    public required string NotPoweredText { get; init; }
}
