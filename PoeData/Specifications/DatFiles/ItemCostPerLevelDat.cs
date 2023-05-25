// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ItemCostPerLevel.dat data.
/// </summary>
public sealed partial class ItemCostPerLevelDat
{
    /// <summary> Gets Contract_BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? Contract_BaseItemTypesKey { get; init; }

    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets Cost.</summary>
    /// <remarks> references <see cref="ItemCostsDat"/> on <see cref="Specification.LoadItemCostsDat"/> index.</remarks>
    public required int? Cost { get; init; }

    /// <summary> Gets CostHardmode.</summary>
    /// <remarks> references <see cref="ItemCostsDat"/> on <see cref="Specification.LoadItemCostsDat"/> index.</remarks>
    public required int? CostHardmode { get; init; }
}
