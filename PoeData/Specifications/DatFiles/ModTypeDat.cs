// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ModType.dat data.
/// </summary>
public sealed partial class ModTypeDat
{
    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets ModSellPriceTypesKeys.</summary>
    /// <remarks> references <see cref="ModSellPriceTypesDat"/> on <see cref="Specification.LoadModSellPriceTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModSellPriceTypesKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown24 is set.</summary>
    public required bool Unknown24 { get; init; }
}
