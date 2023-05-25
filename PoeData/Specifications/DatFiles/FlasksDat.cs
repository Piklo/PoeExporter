// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Flasks.dat data.
/// </summary>
public sealed partial class FlasksDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Group.</summary>
    public required int Group { get; init; }

    /// <summary> Gets LifePerUse.</summary>
    public required int LifePerUse { get; init; }

    /// <summary> Gets ManaPerUse.</summary>
    public required int ManaPerUse { get; init; }

    /// <summary> Gets RecoveryTime.</summary>
    public required int RecoveryTime { get; init; }

    /// <summary> Gets BuffDefinitionsKey.</summary>
    /// <remarks> references <see cref="BuffDefinitionsDat"/> on <see cref="Specification.LoadBuffDefinitionsDat"/> index.</remarks>
    public required int? BuffDefinitionsKey { get; init; }

    /// <summary> Gets BuffStatValues.</summary>
    public required ReadOnlyCollection<int> BuffStatValues { get; init; }

    /// <summary> Gets RecoveryTime2.</summary>
    public required int RecoveryTime2 { get; init; }

    /// <summary> Gets BuffStatValues2.</summary>
    public required ReadOnlyCollection<int> BuffStatValues2 { get; init; }
}
