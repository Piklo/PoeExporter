// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistNPCDialogue.dat data.
/// </summary>
public sealed partial class HeistNPCDialogueDat
{
    /// <summary> Gets DialogueEventKey.</summary>
    /// <remarks> references <see cref="DialogueEventDat"/> on <see cref="Specification.LoadDialogueEventDat"/> index.</remarks>
    public required int? DialogueEventKey { get; init; }

    /// <summary> Gets HeistNPCsKey.</summary>
    /// <remarks> references <see cref="HeistNPCsDat"/> on <see cref="Specification.LoadHeistNPCsDat"/> index.</remarks>
    public required int? HeistNPCsKey { get; init; }

    /// <summary> Gets AudioNormal.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AudioNormal { get; init; }

    /// <summary> Gets AudioLoud.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AudioLoud { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }
}
