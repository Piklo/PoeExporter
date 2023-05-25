// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ItemisedVisualEffect.dat data.
/// </summary>
public sealed partial class ItemisedVisualEffectDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets ItemVisualEffectKey.</summary>
    /// <remarks> references <see cref="ItemVisualEffectDat"/> on <see cref="Specification.LoadItemVisualEffectDat"/> index.</remarks>
    public required int? ItemVisualEffectKey { get; init; }

    /// <summary> Gets ItemVisualIdentityKey1.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required int? ItemVisualIdentityKey1 { get; init; }

    /// <summary> Gets ItemVisualIdentityKey2.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required int? ItemVisualIdentityKey2 { get; init; }

    /// <summary> Gets Stats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Stats { get; init; }

    /// <summary> Gets ItemClasses.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.LoadItemClassesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ItemClasses { get; init; }

    /// <summary> Gets Unknown96.</summary>
    public required ReadOnlyCollection<int> Unknown96 { get; init; }

    /// <summary> Gets a value indicating whether Unknown112 is set.</summary>
    public required bool Unknown112 { get; init; }

    /// <summary> Gets Unknown113.</summary>
    public required ReadOnlyCollection<int> Unknown113 { get; init; }

    /// <summary> Gets Unknown129.</summary>
    public required ReadOnlyCollection<int> Unknown129 { get; init; }

    /// <summary> Gets a value indicating whether Unknown145 is set.</summary>
    public required bool Unknown145 { get; init; }

    /// <summary> Gets Unknown146.</summary>
    public required ReadOnlyCollection<int> Unknown146 { get; init; }

    /// <summary> Gets a value indicating whether Unknown162 is set.</summary>
    public required bool Unknown162 { get; init; }
}
