// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing UltimatumModifiers.dat data.
/// </summary>
public sealed partial class UltimatumModifiersDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Types.</summary>
    /// <remarks> references <see cref="UltimatumModifierTypesDat"/> on <see cref="Specification.LoadUltimatumModifierTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Types { get; init; }

    /// <summary> Gets ExtraMods.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ExtraMods { get; init; }

    /// <summary> Gets TypesFiltered.</summary>
    /// <remarks> references <see cref="UltimatumModifierTypesDat"/> on <see cref="Specification.LoadUltimatumModifierTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TypesFiltered { get; init; }

    /// <summary> Gets DaemonSpawningData.</summary>
    /// <remarks> references <see cref="DaemonSpawningDataDat"/> on <see cref="Specification.LoadDaemonSpawningDataDat"/> index.</remarks>
    public required int? DaemonSpawningData { get; init; }

    /// <summary> Gets PreviousTiers.</summary>
    /// <remarks> references <see cref="UltimatumModifiersDat"/> on <see cref="Specification.LoadUltimatumModifiersDat"/> index.</remarks>
    public required ReadOnlyCollection<int> PreviousTiers { get; init; }

    /// <summary> Gets a value indicating whether Unknown88 is set.</summary>
    public required bool Unknown88 { get; init; }

    /// <summary> Gets Bosses.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Bosses { get; init; }

    /// <summary> Gets Radius.</summary>
    public required int Radius { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets TypesExtra.</summary>
    /// <remarks> references <see cref="UltimatumModifierTypesDat"/> on <see cref="Specification.LoadUltimatumModifierTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TypesExtra { get; init; }

    /// <summary> Gets MonsterTypesApplyingRuin.</summary>
    public required int MonsterTypesApplyingRuin { get; init; }

    /// <summary> Gets MiscAnimated.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimated { get; init; }

    /// <summary> Gets BuffTemplates.</summary>
    /// <remarks> references <see cref="BuffTemplatesDat"/> on <see cref="Specification.LoadBuffTemplatesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BuffTemplates { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets Unknown185.</summary>
    public required int Unknown185 { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets MonsterSpawners.</summary>
    public required ReadOnlyCollection<string> MonsterSpawners { get; init; }

    /// <summary> Gets a value indicating whether Unknown213 is set.</summary>
    public required bool Unknown213 { get; init; }

    /// <summary> Gets Achievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievements { get; init; }

    /// <summary> Gets TextAudio.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? TextAudio { get; init; }

    /// <summary> Gets UniqueMapMod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? UniqueMapMod { get; init; }
}
