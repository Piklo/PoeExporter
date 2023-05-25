// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MetamorphosisRewardTypeItemsClient.dat data.
/// </summary>
public sealed partial class MetamorphosisRewardTypeItemsClientDat
{
    /// <summary> Gets MetamorphosisRewardTypesKey.</summary>
    /// <remarks> references <see cref="MetamorphosisRewardTypesDat"/> on <see cref="Specification.LoadMetamorphosisRewardTypesDat"/> index.</remarks>
    public required int? MetamorphosisRewardTypesKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }
}
