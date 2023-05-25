// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistChests.dat data.
/// </summary>
public sealed partial class HeistChestsDat
{
    /// <summary> Gets ChestsKey.</summary>
    /// <remarks> references <see cref="ChestsDat"/> on <see cref="Specification.LoadChestsDat"/> index.</remarks>
    public required int? ChestsKey { get; init; }

    /// <summary> Gets Weight.</summary>
    public required int Weight { get; init; }

    /// <summary> Gets HeistAreasKey.</summary>
    /// <remarks> references <see cref="HeistAreasDat"/> on <see cref="Specification.LoadHeistAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> HeistAreasKey { get; init; }

    /// <summary> Gets HeistChestTypesKey.</summary>
    /// <remarks> references <see cref="HeistChestTypesDat"/> on <see cref="Specification.LoadHeistChestTypesDat"/> index.</remarks>
    public required int HeistChestTypesKey { get; init; }
}
