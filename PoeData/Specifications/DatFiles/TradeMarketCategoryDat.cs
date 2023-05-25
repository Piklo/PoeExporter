// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing TradeMarketCategory.dat data.
/// </summary>
public sealed partial class TradeMarketCategoryDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets StyleFlag.</summary>
    /// <remarks> references <see cref="TradeMarketCategoryStyleFlagDat"/> on <see cref="Specification.LoadTradeMarketCategoryStyleFlagDat"/> index.</remarks>
    public required int StyleFlag { get; init; }

    /// <summary> Gets Group.</summary>
    /// <remarks> references <see cref="TradeMarketCategoryGroupsDat"/> on <see cref="Specification.LoadTradeMarketCategoryGroupsDat"/> index.</remarks>
    public required int? Group { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required ReadOnlyCollection<int> Unknown36 { get; init; }

    /// <summary> Gets a value indicating whether Unknown52 is set.</summary>
    public required bool Unknown52 { get; init; }

    /// <summary> Gets a value indicating whether IsDisabled is set.</summary>
    public required bool IsDisabled { get; init; }
}
