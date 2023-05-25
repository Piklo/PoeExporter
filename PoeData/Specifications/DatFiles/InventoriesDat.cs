// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Inventories.dat data.
/// </summary>
public sealed partial class InventoriesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets InventoryIdKey.</summary>
    /// <remarks> references <see cref="InventoryIdDat"/> on <see cref="Specification.LoadInventoryIdDat"/> index.</remarks>
    public required int InventoryIdKey { get; init; }

    /// <summary> Gets InventoryTypeKey.</summary>
    /// <remarks> references <see cref="InventoryTypeDat"/> on <see cref="Specification.LoadInventoryTypeDat"/> index.</remarks>
    public required int InventoryTypeKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets a value indicating whether Unknown20 is set.</summary>
    public required bool Unknown20 { get; init; }

    /// <summary> Gets a value indicating whether Unknown21 is set.</summary>
    public required bool Unknown21 { get; init; }

    /// <summary> Gets a value indicating whether Unknown22 is set.</summary>
    public required bool Unknown22 { get; init; }
}
