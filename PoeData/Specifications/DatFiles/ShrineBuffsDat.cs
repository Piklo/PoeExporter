// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ShrineBuffs.dat data.
/// </summary>
public sealed partial class ShrineBuffsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BuffStatValues.</summary>
    public required ReadOnlyCollection<int> BuffStatValues { get; init; }

    /// <summary> Gets BuffDefinitionsKey.</summary>
    /// <remarks> references <see cref="BuffDefinitionsDat"/> on <see cref="Specification.LoadBuffDefinitionsDat"/> index.</remarks>
    public required int? BuffDefinitionsKey { get; init; }

    /// <summary> Gets BuffVisual.</summary>
    /// <remarks> references <see cref="BuffVisualsDat"/> on <see cref="Specification.LoadBuffVisualsDat"/> index.</remarks>
    public required int? BuffVisual { get; init; }
}
