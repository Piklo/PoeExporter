// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LabyrinthTrinkets.dat data.
/// </summary>
public sealed partial class LabyrinthTrinketsDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets LabyrinthSecretsKey.</summary>
    /// <remarks> references <see cref="LabyrinthSecretsDat"/> on <see cref="Specification.LoadLabyrinthSecretsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> LabyrinthSecretsKey { get; init; }

    /// <summary> Gets Buff_BuffDefinitionsKey.</summary>
    /// <remarks> references <see cref="BuffDefinitionsDat"/> on <see cref="Specification.LoadBuffDefinitionsDat"/> index.</remarks>
    public required int? Buff_BuffDefinitionsKey { get; init; }

    /// <summary> Gets Buff_StatValues.</summary>
    public required ReadOnlyCollection<int> Buff_StatValues { get; init; }
}
