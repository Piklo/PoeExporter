// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MasterHideoutLevels.dat data.
/// </summary>
public sealed partial class MasterHideoutLevelsDat
{
    /// <summary> Gets NPCMasterKey.</summary>
    /// <remarks> references <see cref="NPCMasterDat"/> on <see cref="Specification.LoadNPCMasterDat"/> index.</remarks>
    public required int? NPCMasterKey { get; init; }

    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets MissionsRequired.</summary>
    public required int MissionsRequired { get; init; }
}
