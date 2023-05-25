// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing EssenceStashTabLayout.dat data.
/// </summary>
public sealed partial class EssenceStashTabLayoutDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets X.</summary>
    public required int X { get; init; }

    /// <summary> Gets Y.</summary>
    public required int Y { get; init; }

    /// <summary> Gets IntId.</summary>
    public required int IntId { get; init; }

    /// <summary> Gets SlotWidth.</summary>
    public required int SlotWidth { get; init; }

    /// <summary> Gets SlotHeight.</summary>
    public required int SlotHeight { get; init; }

    /// <summary> Gets a value indicating whether IsUpgradableEssenceSlot is set.</summary>
    public required bool IsUpgradableEssenceSlot { get; init; }
}
