// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HideoutNPCs.dat data.
/// </summary>
public sealed partial class HideoutNPCsDat
{
    /// <summary> Gets Hideout_NPCsKey.</summary>
    /// <remarks> references <see cref="NPCsDat"/> on <see cref="Specification.LoadNPCsDat"/> index.</remarks>
    public required int? Hideout_NPCsKey { get; init; }

    /// <summary> Gets Regular_NPCsKeys.</summary>
    /// <remarks> references <see cref="NPCsDat"/> on <see cref="Specification.LoadNPCsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Regular_NPCsKeys { get; init; }

    /// <summary> Gets HideoutDoodadsKey.</summary>
    /// <remarks> references <see cref="HideoutDoodadsDat"/> on <see cref="Specification.LoadHideoutDoodadsDat"/> index.</remarks>
    public required int? HideoutDoodadsKey { get; init; }

    /// <summary> Gets NPCMasterKey.</summary>
    public required int NPCMasterKey { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int? Unknown52 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int? Unknown68 { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required int Unknown84 { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int? Unknown88 { get; init; }

    /// <summary> Gets Unknown104.</summary>
    public required int Unknown104 { get; init; }

    /// <summary> Gets a value indicating whether Unknown108 is set.</summary>
    public required bool Unknown108 { get; init; }

    /// <summary> Gets Unknown109.</summary>
    public required int? Unknown109 { get; init; }
}
