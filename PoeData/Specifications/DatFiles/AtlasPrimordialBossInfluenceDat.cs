// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AtlasPrimordialBossInfluence.dat data.
/// </summary>
public sealed partial class AtlasPrimordialBossInfluenceDat
{
    /// <summary> Gets Boss.</summary>
    /// <remarks> references <see cref="AtlasPrimordialBossesDat"/> on <see cref="Specification.LoadAtlasPrimordialBossesDat"/> index.</remarks>
    public required int? Boss { get; init; }

    /// <summary> Gets Progress.</summary>
    public required int Progress { get; init; }

    /// <summary> Gets MinMapTier.</summary>
    public required int MinMapTier { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int? Unknown32 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required float Unknown48 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.LoadQuestFlagsDat"/> index.</remarks>
    public required int? Unknown52 { get; init; }
}
