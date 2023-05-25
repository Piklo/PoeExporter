// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HarvestCraftOptions.dat data.
/// </summary>
public sealed partial class HarvestCraftOptionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int? Unknown16 { get; init; }

    /// <summary> Gets Command.</summary>
    public required string Command { get; init; }

    /// <summary> Gets Parameters.</summary>
    public required string Parameters { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required ReadOnlyCollection<int> Unknown48 { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets a value indicating whether Unknown76 is set.</summary>
    public required bool Unknown76 { get; init; }

    /// <summary> Gets LifeforceType.</summary>
    public required int LifeforceType { get; init; }

    /// <summary> Gets LifeforceCost.</summary>
    public required int LifeforceCost { get; init; }

    /// <summary> Gets SacredCost.</summary>
    public required int SacredCost { get; init; }

    /// <summary> Gets a value indicating whether Unknown89 is set.</summary>
    public required bool Unknown89 { get; init; }

    /// <summary> Gets Achievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievements { get; init; }

    /// <summary> Gets Unknown106.</summary>
    public required int Unknown106 { get; init; }
}
