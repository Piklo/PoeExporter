// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing NPCShops.dat data.
/// </summary>
public sealed partial class NPCShopsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets Shop.</summary>
    /// <remarks> references <see cref="NPCShopDat"/> on <see cref="Specification.LoadNPCShopDat"/> index.</remarks>
    public required int? Shop { get; init; }

    /// <summary> Gets ShopHardmode.</summary>
    /// <remarks> references <see cref="NPCShopDat"/> on <see cref="Specification.LoadNPCShopDat"/> index.</remarks>
    public required int? ShopHardmode { get; init; }
}
