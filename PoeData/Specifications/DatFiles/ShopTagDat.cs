// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ShopTag.dat data.
/// </summary>
public sealed partial class ShopTagDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets a value indicating whether IsCategory is set.</summary>
    public required bool IsCategory { get; init; }

    /// <summary> Gets Category.</summary>
    /// <remarks> references <see cref="ShopTagDat"/> on <see cref="Specification.LoadShopTagDat"/> index.</remarks>
    public required int? Category { get; init; }

    /// <summary> Gets SkillGem.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SkillGem { get; init; }
}
