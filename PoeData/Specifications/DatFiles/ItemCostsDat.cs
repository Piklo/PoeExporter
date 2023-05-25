// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ItemCosts.dat data.
/// </summary>
public sealed partial class ItemCostsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Cost1Currencies.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Cost1Currencies { get; init; }

    /// <summary> Gets Cost1Values.</summary>
    public required ReadOnlyCollection<int> Cost1Values { get; init; }

    /// <summary> Gets Cost2Currencies.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Cost2Currencies { get; init; }

    /// <summary> Gets Cost2Values.</summary>
    public required ReadOnlyCollection<int> Cost2Values { get; init; }

    /// <summary> Gets Cost3Currencies.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Cost3Currencies { get; init; }

    /// <summary> Gets Cost3Values.</summary>
    public required ReadOnlyCollection<int> Cost3Values { get; init; }

    /// <summary> Gets Cost4Currencies.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Cost4Currencies { get; init; }

    /// <summary> Gets Cost4Values.</summary>
    public required ReadOnlyCollection<int> Cost4Values { get; init; }
}
