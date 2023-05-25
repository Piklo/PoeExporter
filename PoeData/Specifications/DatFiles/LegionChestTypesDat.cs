// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LegionChestTypes.dat data.
/// </summary>
public sealed partial class LegionChestTypesDat
{
    /// <summary> Gets Unknown0.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? Unknown0 { get; init; }

    /// <summary> Gets Chest.</summary>
    /// <remarks> references <see cref="ChestsDat"/> on <see cref="Specification.LoadChestsDat"/> index.</remarks>
    public required int? Chest { get; init; }

    /// <summary> Gets HardmodeChest.</summary>
    /// <remarks> references <see cref="ChestsDat"/> on <see cref="Specification.LoadChestsDat"/> index.</remarks>
    public required int? HardmodeChest { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets Faction.</summary>
    /// <remarks> references <see cref="LegionFactionsDat"/> on <see cref="Specification.LoadLegionFactionsDat"/> index.</remarks>
    public required int? Faction { get; init; }
}
