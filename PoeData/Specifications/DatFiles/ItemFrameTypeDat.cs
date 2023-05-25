// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ItemFrameType.dat data.
/// </summary>
public sealed partial class ItemFrameTypeDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets a value indicating whether Unknown8 is set.</summary>
    public required bool Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether DoubleLine is set.</summary>
    public required bool DoubleLine { get; init; }

    /// <summary> Gets HeaderSingle.</summary>
    public required string HeaderSingle { get; init; }

    /// <summary> Gets HeaderDouble.</summary>
    public required string HeaderDouble { get; init; }

    /// <summary> Gets HardmodeHeaderSingle.</summary>
    public required string HardmodeHeaderSingle { get; init; }

    /// <summary> Gets HardmodeHeaderDouble.</summary>
    public required string HardmodeHeaderDouble { get; init; }

    /// <summary> Gets Color.</summary>
    public required ReadOnlyCollection<int> Color { get; init; }

    /// <summary> Gets Separator.</summary>
    public required string Separator { get; init; }

    /// <summary> Gets a value indicating whether Unknown66 is set.</summary>
    public required bool Unknown66 { get; init; }

    /// <summary> Gets Rarity.</summary>
    /// <remarks> references <see cref="RarityDat"/> on <see cref="Specification.LoadRarityDat"/> index.</remarks>
    public required int? Rarity { get; init; }

    /// <summary> Gets DisplayString.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required int? DisplayString { get; init; }

    /// <summary> Gets ColorMarkup.</summary>
    public required string ColorMarkup { get; init; }
}
