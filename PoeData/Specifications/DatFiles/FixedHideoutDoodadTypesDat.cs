// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing FixedHideoutDoodadTypes.dat data.
/// </summary>
public sealed partial class FixedHideoutDoodadTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets HideoutDoodadsKeys.</summary>
    /// <remarks> references <see cref="HideoutDoodadsDat"/> on <see cref="Specification.LoadHideoutDoodadsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> HideoutDoodadsKeys { get; init; }

    /// <summary> Gets BaseTypeHideoutDoodadsKey.</summary>
    /// <remarks> references <see cref="HideoutDoodadsDat"/> on <see cref="Specification.LoadHideoutDoodadsDat"/> index.</remarks>
    public required int? BaseTypeHideoutDoodadsKey { get; init; }
}
