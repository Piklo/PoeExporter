// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PassiveTreeExpansionJewels.dat data.
/// </summary>
public sealed partial class PassiveTreeExpansionJewelsDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets PassiveTreeExpansionJewelSizesKey.</summary>
    /// <remarks> references <see cref="PassiveTreeExpansionJewelSizesDat"/> on <see cref="Specification.LoadPassiveTreeExpansionJewelSizesDat"/> index.</remarks>
    public required int? PassiveTreeExpansionJewelSizesKey { get; init; }

    /// <summary> Gets MinNodes.</summary>
    public required int MinNodes { get; init; }

    /// <summary> Gets MaxNodes.</summary>
    public required int MaxNodes { get; init; }

    /// <summary> Gets SmallIndices.</summary>
    public required ReadOnlyCollection<int> SmallIndices { get; init; }

    /// <summary> Gets NotableIndices.</summary>
    public required ReadOnlyCollection<int> NotableIndices { get; init; }

    /// <summary> Gets SocketIndices.</summary>
    public required ReadOnlyCollection<int> SocketIndices { get; init; }

    /// <summary> Gets TotalIndices.</summary>
    public required int TotalIndices { get; init; }
}
