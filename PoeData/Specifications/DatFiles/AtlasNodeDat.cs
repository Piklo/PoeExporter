// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AtlasNode.dat data.
/// </summary>
public sealed partial class AtlasNodeDat
{
    /// <summary> Gets WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets ItemVisualIdentityKey.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required int? ItemVisualIdentityKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown32 is set.</summary>
    public required bool Unknown32 { get; init; }

    /// <summary> Gets MapsKey.</summary>
    /// <remarks> references <see cref="MapsDat"/> on <see cref="Specification.LoadMapsDat"/> index.</remarks>
    public required int? MapsKey { get; init; }

    /// <summary> Gets FlavourTextKey.</summary>
    /// <remarks> references <see cref="FlavourTextDat"/> on <see cref="Specification.LoadFlavourTextDat"/> index.</remarks>
    public required int? FlavourTextKey { get; init; }

    /// <summary> Gets AtlasNodeKeys.</summary>
    /// <remarks> references <see cref="AtlasNodeDat"/> on <see cref="Specification.LoadAtlasNodeDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AtlasNodeKeys { get; init; }

    /// <summary> Gets Tier0.</summary>
    public required int Tier0 { get; init; }

    /// <summary> Gets Tier1.</summary>
    public required int Tier1 { get; init; }

    /// <summary> Gets Tier2.</summary>
    public required int Tier2 { get; init; }

    /// <summary> Gets Tier3.</summary>
    public required int Tier3 { get; init; }

    /// <summary> Gets Tier4.</summary>
    public required int Tier4 { get; init; }

    /// <summary> Gets Unknown101.</summary>
    public required float Unknown101 { get; init; }

    /// <summary> Gets Unknown105.</summary>
    public required float Unknown105 { get; init; }

    /// <summary> Gets Unknown109.</summary>
    public required float Unknown109 { get; init; }

    /// <summary> Gets Unknown113.</summary>
    public required float Unknown113 { get; init; }

    /// <summary> Gets Unknown117.</summary>
    public required float Unknown117 { get; init; }

    /// <summary> Gets DDSFile.</summary>
    public required string DDSFile { get; init; }

    /// <summary> Gets a value indicating whether Unknown129 is set.</summary>
    public required bool Unknown129 { get; init; }

    /// <summary> Gets a value indicating whether NotOnAtlas is set.</summary>
    public required bool NotOnAtlas { get; init; }
}
