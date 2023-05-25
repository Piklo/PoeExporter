// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LabyrinthIzaroChests.dat data.
/// </summary>
public sealed partial class LabyrinthIzaroChestsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ChestsKey.</summary>
    /// <remarks> references <see cref="ChestsDat"/> on <see cref="Specification.LoadChestsDat"/> index.</remarks>
    public required int? ChestsKey { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets MinLabyrinthTier.</summary>
    public required int MinLabyrinthTier { get; init; }

    /// <summary> Gets MaxLabyrinthTier.</summary>
    public required int MaxLabyrinthTier { get; init; }
}
