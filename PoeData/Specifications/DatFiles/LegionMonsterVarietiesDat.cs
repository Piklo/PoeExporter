// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LegionMonsterVarieties.dat data.
/// </summary>
public sealed partial class LegionMonsterVarietiesDat
{
    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets LegionFactionsKey.</summary>
    /// <remarks> references <see cref="LegionFactionsDat"/> on <see cref="Specification.LoadLegionFactionsDat"/> index.</remarks>
    public required int? LegionFactionsKey { get; init; }

    /// <summary> Gets LegionRanksKey.</summary>
    /// <remarks> references <see cref="LegionRanksDat"/> on <see cref="Specification.LoadLegionRanksDat"/> index.</remarks>
    public required int? LegionRanksKey { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets MiscAnimatedKey.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MiscAnimatedKey { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int Unknown68 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required ReadOnlyCollection<int> Unknown76 { get; init; }

    /// <summary> Gets Unknown92.</summary>
    public required ReadOnlyCollection<int> Unknown92 { get; init; }

    /// <summary> Gets Unknown108.</summary>
    public required ReadOnlyCollection<int> Unknown108 { get; init; }

    /// <summary> Gets Unknown124.</summary>
    public required ReadOnlyCollection<int> Unknown124 { get; init; }

    /// <summary> Gets Unknown140.</summary>
    public required ReadOnlyCollection<int> Unknown140 { get; init; }

    /// <summary> Gets Unknown156.</summary>
    public required ReadOnlyCollection<int> Unknown156 { get; init; }

    /// <summary> Gets Unknown172.</summary>
    public required int Unknown172 { get; init; }

    /// <summary> Gets Unknown176.</summary>
    public required int Unknown176 { get; init; }

    /// <summary> Gets Unknown180.</summary>
    public required ReadOnlyCollection<int> Unknown180 { get; init; }

    /// <summary> Gets MonsterVarietiesKey2.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey2 { get; init; }
}
