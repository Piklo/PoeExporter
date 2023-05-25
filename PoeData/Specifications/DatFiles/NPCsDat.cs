// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing NPCs.dat data.
/// </summary>
public sealed partial class NPCsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Metadata.</summary>
    public required string Metadata { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets NPCMasterKey.</summary>
    /// <remarks> references <see cref="NPCMasterDat"/> on <see cref="Specification.LoadNPCMasterDat"/> index.</remarks>
    public required int? NPCMasterKey { get; init; }

    /// <summary> Gets ShortName.</summary>
    public required string ShortName { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets NPCAudios1.</summary>
    /// <remarks> references <see cref="NPCAudioDat"/> on <see cref="Specification.LoadNPCAudioDat"/> index.</remarks>
    public required ReadOnlyCollection<int> NPCAudios1 { get; init; }

    /// <summary> Gets NPCAudios2.</summary>
    /// <remarks> references <see cref="NPCAudioDat"/> on <see cref="Specification.LoadNPCAudioDat"/> index.</remarks>
    public required ReadOnlyCollection<int> NPCAudios2 { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required int Unknown100 { get; init; }

    /// <summary> Gets Unknown104.</summary>
    /// <remarks> references <see cref="NPCsDat"/> on <see cref="Specification.LoadNPCsDat"/> index.</remarks>
    public required int? Unknown104 { get; init; }

    /// <summary> Gets Portrait.</summary>
    /// <remarks> references <see cref="NPCPortraitsDat"/> on <see cref="Specification.LoadNPCPortraitsDat"/> index.</remarks>
    public required int? Portrait { get; init; }

    /// <summary> Gets DialogueStyle.</summary>
    /// <remarks> references <see cref="NPCDialogueStylesDat"/> on <see cref="Specification.LoadNPCDialogueStylesDat"/> index.</remarks>
    public required int? DialogueStyle { get; init; }

    /// <summary> Gets a value indicating whether Unknown144 is set.</summary>
    public required bool Unknown144 { get; init; }

    /// <summary> Gets Unknown145.</summary>
    public required int? Unknown145 { get; init; }

    /// <summary> Gets Gender.</summary>
    public required string Gender { get; init; }
}
