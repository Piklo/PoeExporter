// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HideoutDoodads.dat data.
/// </summary>
public sealed partial class HideoutDoodadsDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets Variation_AOFiles.</summary>
    public required ReadOnlyCollection<string> Variation_AOFiles { get; init; }

    /// <summary> Gets a value indicating whether IsNonMasterDoodad is set.</summary>
    public required bool IsNonMasterDoodad { get; init; }

    /// <summary> Gets InheritsFrom.</summary>
    public required string InheritsFrom { get; init; }

    /// <summary> Gets a value indicating whether Unknown41 is set.</summary>
    public required bool Unknown41 { get; init; }

    /// <summary> Gets a value indicating whether IsCraftingBench is set.</summary>
    public required bool IsCraftingBench { get; init; }

    /// <summary> Gets Tags.</summary>
    /// <remarks> references <see cref="HideoutDoodadTagsDat"/> on <see cref="Specification.LoadHideoutDoodadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Tags { get; init; }

    /// <summary> Gets a value indicating whether Unknown59 is set.</summary>
    public required bool Unknown59 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int? Unknown60 { get; init; }

    /// <summary> Gets Category.</summary>
    /// <remarks> references <see cref="HideoutDoodadCategoryDat"/> on <see cref="Specification.LoadHideoutDoodadCategoryDat"/> index.</remarks>
    public required int? Category { get; init; }

    /// <summary> Gets Unknown92.</summary>
    public required int Unknown92 { get; init; }

    /// <summary> Gets a value indicating whether Unknown96 is set.</summary>
    public required bool Unknown96 { get; init; }

    /// <summary> Gets Unknown97.</summary>
    public required int? Unknown97 { get; init; }

    /// <summary> Gets a value indicating whether Unknown113 is set.</summary>
    public required bool Unknown113 { get; init; }

    /// <summary> Gets Unknown114.</summary>
    public required int? Unknown114 { get; init; }
}
