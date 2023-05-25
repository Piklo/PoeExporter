// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MetamorphosisMetaSkills.dat data.
/// </summary>
public sealed partial class MetamorphosisMetaSkillsDat
{
    /// <summary> Gets Monster.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? Monster { get; init; }

    /// <summary> Gets SkillType.</summary>
    /// <remarks> references <see cref="MetamorphosisMetaSkillTypesDat"/> on <see cref="Specification.LoadMetamorphosisMetaSkillTypesDat"/> index.</remarks>
    public required int? SkillType { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int? Unknown32 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required ReadOnlyCollection<int> Unknown48 { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required ReadOnlyCollection<int> Unknown64 { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required int? Unknown80 { get; init; }

    /// <summary> Gets Animation.</summary>
    /// <remarks> references <see cref="AnimationDat"/> on <see cref="Specification.LoadAnimationDat"/> index.</remarks>
    public required int? Animation { get; init; }

    /// <summary> Gets Stats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Stats { get; init; }

    /// <summary> Gets StatsValues.</summary>
    public required ReadOnlyCollection<int> StatsValues { get; init; }

    /// <summary> Gets Unknown144.</summary>
    public required int Unknown144 { get; init; }

    /// <summary> Gets Unknown148.</summary>
    public required int? Unknown148 { get; init; }

    /// <summary> Gets GrantedEffects.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.LoadGrantedEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> GrantedEffects { get; init; }

    /// <summary> Gets Unknown180.</summary>
    public required int Unknown180 { get; init; }

    /// <summary> Gets Unknown184.</summary>
    public required int? Unknown184 { get; init; }

    /// <summary> Gets Script1.</summary>
    public required string Script1 { get; init; }

    /// <summary> Gets Script2.</summary>
    public required string Script2 { get; init; }

    /// <summary> Gets Mods.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Mods { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Unknown240.</summary>
    public required int Unknown240 { get; init; }

    /// <summary> Gets Unknown244.</summary>
    public required ReadOnlyCollection<int> Unknown244 { get; init; }

    /// <summary> Gets Unknown260.</summary>
    public required int Unknown260 { get; init; }

    /// <summary> Gets Unknown264.</summary>
    public required int Unknown264 { get; init; }

    /// <summary> Gets Unknown268.</summary>
    public required ReadOnlyCollection<int> Unknown268 { get; init; }

    /// <summary> Gets Unknown284.</summary>
    public required ReadOnlyCollection<int> Unknown284 { get; init; }

    /// <summary> Gets Unknown300.</summary>
    public required ReadOnlyCollection<int> Unknown300 { get; init; }

    /// <summary> Gets Unknown316.</summary>
    public required ReadOnlyCollection<int> Unknown316 { get; init; }

    /// <summary> Gets MiscAnimations.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MiscAnimations { get; init; }

    /// <summary> Gets a value indicating whether Unknown348 is set.</summary>
    public required bool Unknown348 { get; init; }
}
