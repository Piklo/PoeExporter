// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DroneBaseTypes.dat data.
/// </summary>
public sealed partial class DroneBaseTypesDat
{
    /// <summary> Gets BaseType.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseType { get; init; }

    /// <summary> Gets Type.</summary>
    /// <remarks> references <see cref="DroneTypesDat"/> on <see cref="Specification.LoadDroneTypesDat"/> index.</remarks>
    public required int? Type { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets Visual.</summary>
    /// <remarks> references <see cref="BuffVisualsDat"/> on <see cref="Specification.LoadBuffVisualsDat"/> index.</remarks>
    public required int? Visual { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int Unknown60 { get; init; }

    /// <summary> Gets UseAchievement.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? UseAchievement { get; init; }

    /// <summary> Gets a value indicating whether Unknown80 is set.</summary>
    public required bool Unknown80 { get; init; }
}
