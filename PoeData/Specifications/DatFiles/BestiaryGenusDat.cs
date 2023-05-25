// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BestiaryGenus.dat data.
/// </summary>
public sealed partial class BestiaryGenusDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets BestiaryGroupsKey.</summary>
    /// <remarks> references <see cref="BestiaryGroupsDat"/> on <see cref="Specification.LoadBestiaryGroupsDat"/> index.</remarks>
    public required int? BestiaryGroupsKey { get; init; }

    /// <summary> Gets Name2.</summary>
    public required string Name2 { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }
}
