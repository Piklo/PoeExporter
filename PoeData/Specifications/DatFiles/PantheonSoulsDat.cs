// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PantheonSouls.dat data.
/// </summary>
public sealed partial class PantheonSoulsDat
{
    /// <summary> Gets WorldArea.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? WorldArea { get; init; }

    /// <summary> Gets CapturedVessel.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? CapturedVessel { get; init; }

    /// <summary> Gets QuestFlag.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.LoadQuestFlagsDat"/> index.</remarks>
    public required int? QuestFlag { get; init; }

    /// <summary> Gets CapturedMonster.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? CapturedMonster { get; init; }

    /// <summary> Gets PanelLayout.</summary>
    /// <remarks> references <see cref="PantheonPanelLayoutDat"/> on <see cref="Specification.LoadPantheonPanelLayoutDat"/> index.</remarks>
    public required int? PanelLayout { get; init; }
}
