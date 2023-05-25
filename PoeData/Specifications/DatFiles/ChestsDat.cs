// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Chests.dat data.
/// </summary>
public sealed partial class ChestsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets a value indicating whether Unknown8 is set.</summary>
    public required bool Unknown8 { get; init; }

    /// <summary> Gets Unknown9.</summary>
    public required int Unknown9 { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets AOFiles.</summary>
    public required ReadOnlyCollection<string> AOFiles { get; init; }

    /// <summary> Gets a value indicating whether Unknown37 is set.</summary>
    public required bool Unknown37 { get; init; }

    /// <summary> Gets a value indicating whether Unknown38 is set.</summary>
    public required bool Unknown38 { get; init; }

    /// <summary> Gets Unknown39.</summary>
    public required int Unknown39 { get; init; }

    /// <summary> Gets a value indicating whether Unknown43 is set.</summary>
    public required bool Unknown43 { get; init; }

    /// <summary> Gets a value indicating whether Unknown44 is set.</summary>
    public required bool Unknown44 { get; init; }

    /// <summary> Gets Unknown45.</summary>
    public required int Unknown45 { get; init; }

    /// <summary> Gets Unknown49.</summary>
    public required ReadOnlyCollection<int> Unknown49 { get; init; }

    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown81 is set.</summary>
    public required bool Unknown81 { get; init; }

    /// <summary> Gets ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKeys { get; init; }

    /// <summary> Gets TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TagsKeys { get; init; }

    /// <summary> Gets ChestEffectsKey.</summary>
    /// <remarks> references <see cref="ChestEffectsDat"/> on <see cref="Specification.LoadChestEffectsDat"/> index.</remarks>
    public required int? ChestEffectsKey { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets Unknown134.</summary>
    public required string Unknown134 { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets Corrupt_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? Corrupt_AchievementItemsKey { get; init; }

    /// <summary> Gets CurrencyUse_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? CurrencyUse_AchievementItemsKey { get; init; }

    /// <summary> Gets Encounter_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Encounter_AchievementItemsKeys { get; init; }

    /// <summary> Gets Unknown194.</summary>
    public required int? Unknown194 { get; init; }

    /// <summary> Gets InheritsFrom.</summary>
    public required string InheritsFrom { get; init; }

    /// <summary> Gets a value indicating whether Unknown218 is set.</summary>
    public required bool Unknown218 { get; init; }

    /// <summary> Gets Unknown219.</summary>
    public required int? Unknown219 { get; init; }

    /// <summary> Gets Unknown235.</summary>
    public required ReadOnlyCollection<int> Unknown235 { get; init; }

    /// <summary> Gets Unknown251.</summary>
    public required string Unknown251 { get; init; }

    /// <summary> Gets Unknown259.</summary>
    public required int Unknown259 { get; init; }

    /// <summary> Gets Unknown263.</summary>
    public required int Unknown263 { get; init; }

    /// <summary> Gets a value indicating whether Unknown267 is set.</summary>
    public required bool Unknown267 { get; init; }

    /// <summary> Gets Unknown268.</summary>
    public required int? Unknown268 { get; init; }

    /// <summary> Gets Unknown284.</summary>
    public required int? Unknown284 { get; init; }

    /// <summary> Gets a value indicating whether Unknown300 is set.</summary>
    public required bool Unknown300 { get; init; }

    /// <summary> Gets a value indicating whether Unknown301 is set.</summary>
    public required bool Unknown301 { get; init; }

    /// <summary> Gets Unknown302.</summary>
    public required ReadOnlyCollection<int> Unknown302 { get; init; }

    /// <summary> Gets a value indicating whether IsHardmode is set.</summary>
    public required bool IsHardmode { get; init; }

    /// <summary> Gets StatsHardmode.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsHardmode { get; init; }
}
