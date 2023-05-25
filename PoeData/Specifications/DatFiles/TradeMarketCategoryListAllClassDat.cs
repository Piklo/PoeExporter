// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing TradeMarketCategoryListAllClass.dat data.
/// </summary>
public sealed partial class TradeMarketCategoryListAllClassDat
{
    /// <summary> Gets TradeCategory.</summary>
    /// <remarks> references <see cref="TradeMarketCategoryDat"/> on <see cref="Specification.LoadTradeMarketCategoryDat"/> index.</remarks>
    public required int? TradeCategory { get; init; }

    /// <summary> Gets ItemClass.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.LoadItemClassesDat"/> index.</remarks>
    public required int? ItemClass { get; init; }
}
