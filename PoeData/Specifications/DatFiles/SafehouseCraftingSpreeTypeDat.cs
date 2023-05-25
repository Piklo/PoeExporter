// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SafehouseCraftingSpreeType.dat data.
/// </summary>
public sealed partial class SafehouseCraftingSpreeTypeDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Currencies.</summary>
    /// <remarks> references <see cref="SafehouseCraftingSpreeCurrenciesDat"/> on <see cref="Specification.LoadSafehouseCraftingSpreeCurrenciesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Currencies { get; init; }

    /// <summary> Gets CurrencyCount.</summary>
    public required ReadOnlyCollection<int> CurrencyCount { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required ReadOnlyCollection<int> Unknown40 { get; init; }

    /// <summary> Gets a value indicating whether Disabled is set.</summary>
    public required bool Disabled { get; init; }

    /// <summary> Gets ItemClassText.</summary>
    public required string ItemClassText { get; init; }
}
