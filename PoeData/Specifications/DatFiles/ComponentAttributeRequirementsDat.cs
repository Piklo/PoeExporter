// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ComponentAttributeRequirements.dat data.
/// </summary>
public sealed partial class ComponentAttributeRequirementsDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="BaseItemTypesDat.Id"/>.</remarks>
    public required string BaseItemTypesKey { get; init; }

    /// <summary> Gets ReqStr.</summary>
    public required int ReqStr { get; init; }

    /// <summary> Gets ReqDex.</summary>
    public required int ReqDex { get; init; }

    /// <summary> Gets ReqInt.</summary>
    public required int ReqInt { get; init; }
}
