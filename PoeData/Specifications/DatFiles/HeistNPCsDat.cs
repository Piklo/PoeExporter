// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistNPCs.dat data.
/// </summary>
public sealed partial class HeistNPCsDat
{
    /// <summary> Gets NPCsKey.</summary>
    /// <remarks> references <see cref="NPCsDat"/> on <see cref="Specification.LoadNPCsDat"/> index.</remarks>
    public required int? NPCsKey { get; init; }

    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets SkillLevel_HeistJobsKeys.</summary>
    /// <remarks> references <see cref="HeistJobsDat"/> on <see cref="Specification.LoadHeistJobsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SkillLevel_HeistJobsKeys { get; init; }

    /// <summary> Gets PortraitFile.</summary>
    public required string PortraitFile { get; init; }

    /// <summary> Gets HeistNPCStatsKeys.</summary>
    /// <remarks> references <see cref="HeistNPCStatsDat"/> on <see cref="Specification.LoadHeistNPCStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> HeistNPCStatsKeys { get; init; }

    /// <summary> Gets StatValues.</summary>
    public required ReadOnlyCollection<float> StatValues { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required float Unknown88 { get; init; }

    /// <summary> Gets SkillLevel_Values.</summary>
    public required ReadOnlyCollection<int> SkillLevel_Values { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets SilhouetteFile.</summary>
    public required string SilhouetteFile { get; init; }

    /// <summary> Gets Unknown124.</summary>
    public required int Unknown124 { get; init; }

    /// <summary> Gets Unknown128.</summary>
    public required int Unknown128 { get; init; }

    /// <summary> Gets HeistNPCsKey.</summary>
    /// <remarks> references <see cref="HeistNPCsDat"/> on <see cref="Specification.LoadHeistNPCsDat"/> index.</remarks>
    public required int? HeistNPCsKey { get; init; }

    /// <summary> Gets StatValues2.</summary>
    public required ReadOnlyCollection<float> StatValues2 { get; init; }

    /// <summary> Gets Ally_NPCsKey.</summary>
    /// <remarks> references <see cref="NPCsDat"/> on <see cref="Specification.LoadNPCsDat"/> index.</remarks>
    public required int? Ally_NPCsKey { get; init; }

    /// <summary> Gets ActiveNPCIcon.</summary>
    public required string ActiveNPCIcon { get; init; }

    /// <summary> Gets HeistJobsKey.</summary>
    /// <remarks> references <see cref="HeistJobsDat"/> on <see cref="Specification.LoadHeistJobsDat"/> index.</remarks>
    public required int? HeistJobsKey { get; init; }

    /// <summary> Gets Equip_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Equip_AchievementItemsKeys { get; init; }

    /// <summary> Gets AOFile.</summary>
    public required string AOFile { get; init; }

    /// <summary> Gets Unknown220.</summary>
    public required int? Unknown220 { get; init; }
}
