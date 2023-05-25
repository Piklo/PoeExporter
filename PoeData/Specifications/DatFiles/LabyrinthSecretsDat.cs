// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LabyrinthSecrets.dat data.
/// </summary>
public sealed partial class LabyrinthSecretsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Id2.</summary>
    public required string Id2 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required ReadOnlyCollection<int> Unknown16 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets LabyrinthSecretEffectsKeys0.</summary>
    /// <remarks> references <see cref="LabyrinthSecretEffectsDat"/> on <see cref="Specification.LoadLabyrinthSecretEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> LabyrinthSecretEffectsKeys0 { get; init; }

    /// <summary> Gets LabyrinthSecretEffectsKeys1.</summary>
    /// <remarks> references <see cref="LabyrinthSecretEffectsDat"/> on <see cref="Specification.LoadLabyrinthSecretEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> LabyrinthSecretEffectsKeys1 { get; init; }

    /// <summary> Gets LabyrinthSecretEffectsKeys2.</summary>
    /// <remarks> references <see cref="LabyrinthSecretEffectsDat"/> on <see cref="Specification.LoadLabyrinthSecretEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> LabyrinthSecretEffectsKeys2 { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int Unknown88 { get; init; }

    /// <summary> Gets LabyrinthSecretEffectsKeys3.</summary>
    /// <remarks> references <see cref="LabyrinthSecretEffectsDat"/> on <see cref="Specification.LoadLabyrinthSecretEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> LabyrinthSecretEffectsKeys3 { get; init; }

    /// <summary> Gets a value indicating whether Unknown108 is set.</summary>
    public required bool Unknown108 { get; init; }

    /// <summary> Gets a value indicating whether Unknown109 is set.</summary>
    public required bool Unknown109 { get; init; }

    /// <summary> Gets Unknown110.</summary>
    public required int Unknown110 { get; init; }

    /// <summary> Gets a value indicating whether Unknown114 is set.</summary>
    public required bool Unknown114 { get; init; }

    /// <summary> Gets a value indicating whether Unknown115 is set.</summary>
    public required bool Unknown115 { get; init; }

    /// <summary> Gets a value indicating whether Unknown116 is set.</summary>
    public required bool Unknown116 { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? AchievementItemsKey { get; init; }

    /// <summary> Gets LabyrinthTierMinimum.</summary>
    public required int LabyrinthTierMinimum { get; init; }

    /// <summary> Gets LabyrinthTierMaximum.</summary>
    public required int LabyrinthTierMaximum { get; init; }

    /// <summary> Gets a value indicating whether Unknown149 is set.</summary>
    public required bool Unknown149 { get; init; }
}
