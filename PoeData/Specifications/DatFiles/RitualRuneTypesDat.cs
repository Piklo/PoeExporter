// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing RitualRuneTypes.dat data.
/// </summary>
public sealed partial class RitualRuneTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MiscAnimatedKey1.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimatedKey1 { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets LevelMin.</summary>
    public required int LevelMin { get; init; }

    /// <summary> Gets LevelMax.</summary>
    public required int LevelMax { get; init; }

    /// <summary> Gets BuffDefinitionsKey.</summary>
    /// <remarks> references <see cref="BuffDefinitionsDat"/> on <see cref="Specification.LoadBuffDefinitionsDat"/> index.</remarks>
    public required int? BuffDefinitionsKey { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required ReadOnlyCollection<int> Unknown52 { get; init; }

    /// <summary> Gets SpawnPatterns.</summary>
    /// <remarks> references <see cref="RitualSpawnPatternsDat"/> on <see cref="Specification.LoadRitualSpawnPatternsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SpawnPatterns { get; init; }

    /// <summary> Gets ModsKey.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKey { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required ReadOnlyCollection<int> Unknown100 { get; init; }

    /// <summary> Gets Unknown116.</summary>
    public required ReadOnlyCollection<int> Unknown116 { get; init; }

    /// <summary> Gets MiscAnimatedKey2.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimatedKey2 { get; init; }

    /// <summary> Gets EnvironmentsKey.</summary>
    /// <remarks> references <see cref="EnvironmentsDat"/> on <see cref="Specification.LoadEnvironmentsDat"/> index.</remarks>
    public required int? EnvironmentsKey { get; init; }

    /// <summary> Gets Unknown164.</summary>
    public required int Unknown164 { get; init; }

    /// <summary> Gets Achievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievements { get; init; }

    /// <summary> Gets Type.</summary>
    public required string Type { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets DaemonSpawningDataKey.</summary>
    /// <remarks> references <see cref="DaemonSpawningDataDat"/> on <see cref="Specification.LoadDaemonSpawningDataDat"/> index.</remarks>
    public required int? DaemonSpawningDataKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown216 is set.</summary>
    public required bool Unknown216 { get; init; }
}
