// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MonsterDeathAchievements.dat data.
/// </summary>
public sealed partial class MonsterDeathAchievementsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MonsterVarietiesKeys.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MonsterVarietiesKeys { get; init; }

    /// <summary> Gets AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown40 is set.</summary>
    public required bool Unknown40 { get; init; }

    /// <summary> Gets PlayerConditionsKeys.</summary>
    /// <remarks> references <see cref="PlayerConditionsDat"/> on <see cref="Specification.LoadPlayerConditionsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> PlayerConditionsKeys { get; init; }

    /// <summary> Gets MonsterDeathConditionsKeys.</summary>
    /// <remarks> references <see cref="MonsterDeathConditionsDat"/> on <see cref="Specification.LoadMonsterDeathConditionsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MonsterDeathConditionsKeys { get; init; }

    /// <summary> Gets Unknown73.</summary>
    public required ReadOnlyCollection<int> Unknown73 { get; init; }

    /// <summary> Gets Unknown89.</summary>
    public required int Unknown89 { get; init; }

    /// <summary> Gets Unknown93.</summary>
    public required int Unknown93 { get; init; }

    /// <summary> Gets a value indicating whether Unknown97 is set.</summary>
    public required bool Unknown97 { get; init; }

    /// <summary> Gets a value indicating whether Unknown98 is set.</summary>
    public required bool Unknown98 { get; init; }

    /// <summary> Gets Unknown99.</summary>
    public required int? Unknown99 { get; init; }

    /// <summary> Gets Unknown115.</summary>
    public required ReadOnlyCollection<int> Unknown115 { get; init; }

    /// <summary> Gets Unknown131.</summary>
    public required ReadOnlyCollection<int> Unknown131 { get; init; }

    /// <summary> Gets Unknown147.</summary>
    public required ReadOnlyCollection<int> Unknown147 { get; init; }

    /// <summary> Gets Unknown163.</summary>
    public required int? Unknown163 { get; init; }

    /// <summary> Gets Unknown179.</summary>
    public required int Unknown179 { get; init; }

    /// <summary> Gets NearbyMonsterConditionsKeys.</summary>
    /// <remarks> references <see cref="NearbyMonsterConditionsDat"/> on <see cref="Specification.LoadNearbyMonsterConditionsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> NearbyMonsterConditionsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown199 is set.</summary>
    public required bool Unknown199 { get; init; }

    /// <summary> Gets MultiPartAchievementConditionsKeys.</summary>
    /// <remarks> references <see cref="MultiPartAchievementConditionsDat"/> on <see cref="Specification.LoadMultiPartAchievementConditionsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MultiPartAchievementConditionsKeys { get; init; }

    /// <summary> Gets Unknown216.</summary>
    public required int Unknown216 { get; init; }

    /// <summary> Gets a value indicating whether Unknown220 is set.</summary>
    public required bool Unknown220 { get; init; }
}
