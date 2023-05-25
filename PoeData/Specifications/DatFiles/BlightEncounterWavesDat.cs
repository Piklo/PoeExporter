// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BlightEncounterWaves.dat data.
/// </summary>
public sealed partial class BlightEncounterWavesDat
{
    /// <summary> Gets MonsterSpawnerId.</summary>
    public required string MonsterSpawnerId { get; init; }

    /// <summary> Gets EncounterType.</summary>
    /// <remarks> references <see cref="BlightEncounterTypesDat"/> on <see cref="Specification.LoadBlightEncounterTypesDat"/> index.</remarks>
    public required int? EncounterType { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary> Gets Wave.</summary>
    public required int Wave { get; init; }
}
