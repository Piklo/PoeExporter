// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Hideouts.dat data.
/// </summary>
public sealed partial class HideoutsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets HideoutArea.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? HideoutArea { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets HideoutFile.</summary>
    public required string HideoutFile { get; init; }

    /// <summary> Gets SpawnAreas.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SpawnAreas { get; init; }

    /// <summary> Gets ClaimSideArea.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? ClaimSideArea { get; init; }

    /// <summary> Gets HideoutImage.</summary>
    public required string HideoutImage { get; init; }

    /// <summary> Gets a value indicating whether IsEnabled is set.</summary>
    public required bool IsEnabled { get; init; }

    /// <summary> Gets Weight.</summary>
    public required int Weight { get; init; }

    /// <summary> Gets Rarity.</summary>
    /// <remarks> references <see cref="HideoutRarityDat"/> on <see cref="Specification.LoadHideoutRarityDat"/> index.</remarks>
    public required int? Rarity { get; init; }

    /// <summary> Gets a value indicating whether NotActsArea is set.</summary>
    public required bool NotActsArea { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Unknown106.</summary>
    public required ReadOnlyCollection<int> Unknown106 { get; init; }

    /// <summary> Gets a value indicating whether Unknown122 is set.</summary>
    public required bool Unknown122 { get; init; }

    /// <summary> Gets a value indicating whether Unknown123 is set.</summary>
    public required bool Unknown123 { get; init; }

    /// <summary> Gets a value indicating whether Unknown124 is set.</summary>
    public required bool Unknown124 { get; init; }

    /// <summary> Gets a value indicating whether Unknown125 is set.</summary>
    public required bool Unknown125 { get; init; }
}
