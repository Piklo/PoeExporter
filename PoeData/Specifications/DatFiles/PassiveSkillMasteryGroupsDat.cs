// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PassiveSkillMasteryGroups.dat data.
/// </summary>
public sealed partial class PassiveSkillMasteryGroupsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MasteryEffects.</summary>
    /// <remarks> references <see cref="PassiveSkillMasteryEffectsDat"/> on <see cref="Specification.LoadPassiveSkillMasteryEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MasteryEffects { get; init; }

    /// <summary> Gets InactiveIcon.</summary>
    public required string InactiveIcon { get; init; }

    /// <summary> Gets ActiveIcon.</summary>
    public required string ActiveIcon { get; init; }

    /// <summary> Gets ActiveEffectImage.</summary>
    public required string ActiveEffectImage { get; init; }

    /// <summary> Gets a value indicating whether Unknown48 is set.</summary>
    public required bool Unknown48 { get; init; }

    /// <summary> Gets SoundEffect.</summary>
    /// <remarks> references <see cref="SoundEffectsDat"/> on <see cref="Specification.LoadSoundEffectsDat"/> index.</remarks>
    public required int? SoundEffect { get; init; }

    /// <summary> Gets MasteryCountStat.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? MasteryCountStat { get; init; }
}
