// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Achievements.dat data.
/// </summary>
public sealed partial class AchievementsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets SetId.</summary>
    /// <remarks> references <see cref="AchievementSetsDisplayDat"/> on <see cref="AchievementSetsDisplayDat.Id"/>.</remarks>
    public required int SetId { get; init; }

    /// <summary> Gets Objective.</summary>
    public required string Objective { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets a value indicating whether Unknown32 is set.</summary>
    public required bool Unknown32 { get; init; }

    /// <summary> Gets a value indicating whether HideAchievementItems is set.</summary>
    public required bool HideAchievementItems { get; init; }

    /// <summary> Gets a value indicating whether Unknown34 is set.</summary>
    public required bool Unknown34 { get; init; }

    /// <summary> Gets MinCompletedItems.</summary>
    public required int MinCompletedItems { get; init; }

    /// <summary> Gets a value indicating whether TwoColumnLayout is set.</summary>
    public required bool TwoColumnLayout { get; init; }

    /// <summary> Gets a value indicating whether ShowItemCompletionsAsOne is set.</summary>
    public required bool ShowItemCompletionsAsOne { get; init; }

    /// <summary> Gets Unknown41.</summary>
    public required string Unknown41 { get; init; }

    /// <summary> Gets a value indicating whether SoftcoreOnly is set.</summary>
    public required bool SoftcoreOnly { get; init; }

    /// <summary> Gets a value indicating whether HardcoreOnly is set.</summary>
    public required bool HardcoreOnly { get; init; }

    /// <summary> Gets a value indicating whether Unknown51 is set.</summary>
    public required bool Unknown51 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required string Unknown52 { get; init; }
}
