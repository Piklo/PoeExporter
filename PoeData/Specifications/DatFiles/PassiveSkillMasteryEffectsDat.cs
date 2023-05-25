// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PassiveSkillMasteryEffects.dat data.
/// </summary>
public sealed partial class PassiveSkillMasteryEffectsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets Stats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Stats { get; init; }

    /// <summary> Gets Stat1Value.</summary>
    public required int Stat1Value { get; init; }

    /// <summary> Gets Stat2Value.</summary>
    public required int Stat2Value { get; init; }

    /// <summary> Gets Stat3Value.</summary>
    public required int Stat3Value { get; init; }

    /// <summary> Gets AchievementItem.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? AchievementItem { get; init; }
}
