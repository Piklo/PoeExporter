// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistRevealingNPCs.dat data.
/// </summary>
public sealed partial class HeistRevealingNPCsDat
{
    /// <summary> Gets NPCsKey.</summary>
    /// <remarks> references <see cref="NPCsDat"/> on <see cref="Specification.LoadNPCsDat"/> index.</remarks>
    public required int? NPCsKey { get; init; }

    /// <summary> Gets PortraitFile.</summary>
    public required string PortraitFile { get; init; }

    /// <summary> Gets NPCAudioKey.</summary>
    /// <remarks> references <see cref="NPCAudioDat"/> on <see cref="Specification.LoadNPCAudioDat"/> index.</remarks>
    public required ReadOnlyCollection<int> NPCAudioKey { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }
}
