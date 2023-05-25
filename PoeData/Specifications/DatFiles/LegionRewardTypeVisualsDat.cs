// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LegionRewardTypeVisuals.dat data.
/// </summary>
public sealed partial class LegionRewardTypeVisualsDat
{
    /// <summary> Gets IntId.</summary>
    public required int IntId { get; init; }

    /// <summary> Gets MinimapIconsKey.</summary>
    /// <remarks> references <see cref="MinimapIconsDat"/> on <see cref="Specification.LoadMinimapIconsDat"/> index.</remarks>
    public required int? MinimapIconsKey { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required string Unknown20 { get; init; }

    /// <summary> Gets MiscAnimatedKey.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimatedKey { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required float Unknown44 { get; init; }

    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }
}
