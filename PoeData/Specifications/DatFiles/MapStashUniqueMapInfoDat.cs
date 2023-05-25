// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MapStashUniqueMapInfo.dat data.
/// </summary>
public sealed partial class MapStashUniqueMapInfoDat
{
    /// <summary> Gets UniqueMap.</summary>
    /// <remarks> references <see cref="UniqueMapsDat"/> on <see cref="Specification.LoadUniqueMapsDat"/> index.</remarks>
    public required int? UniqueMap { get; init; }

    /// <summary> Gets BaseItem.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItem { get; init; }
}
