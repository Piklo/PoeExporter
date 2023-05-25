// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing StrDexIntMissionExtraRequirement.dat data.
/// </summary>
public sealed partial class StrDexIntMissionExtraRequirementDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets TimeLimit.</summary>
    public required int TimeLimit { get; init; }

    /// <summary> Gets a value indicating whether HasTimeBonusPerKill is set.</summary>
    public required bool HasTimeBonusPerKill { get; init; }

    /// <summary> Gets a value indicating whether HasTimeBonusPerObjectTagged is set.</summary>
    public required bool HasTimeBonusPerObjectTagged { get; init; }

    /// <summary> Gets a value indicating whether HasLimitedPortals is set.</summary>
    public required bool HasLimitedPortals { get; init; }

    /// <summary> Gets NPCTalkKey.</summary>
    /// <remarks> references <see cref="NPCTalkDat"/> on <see cref="Specification.LoadNPCTalkDat"/> index.</remarks>
    public required int? NPCTalkKey { get; init; }

    /// <summary> Gets TimeLimitBonusFromObjective.</summary>
    public required int TimeLimitBonusFromObjective { get; init; }

    /// <summary> Gets ObjectCount.</summary>
    public required int ObjectCount { get; init; }

    /// <summary> Gets Unknown51.</summary>
    public required ReadOnlyCollection<int> Unknown51 { get; init; }

    /// <summary> Gets a value indicating whether Unknown67 is set.</summary>
    public required bool Unknown67 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required ReadOnlyCollection<int> Unknown68 { get; init; }
}
