// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing InfluenceTags.dat data.
/// </summary>
public sealed partial class InfluenceTagsDat
{
    /// <summary> Gets ItemClass.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.LoadItemClassesDat"/> index.</remarks>
    public required int? ItemClass { get; init; }

    /// <summary> Gets Influence.</summary>
    /// <remarks> references <see cref="InfluenceTypesDat"/> on <see cref="Specification.LoadInfluenceTypesDat"/> index.</remarks>
    public required int Influence { get; init; }

    /// <summary> Gets Tag.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required int? Tag { get; init; }
}
