// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing NPCConversations.dat data.
/// </summary>
public sealed partial class NPCConversationsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets DialogueEvent.</summary>
    /// <remarks> references <see cref="DialogueEventDat"/> on <see cref="Specification.LoadDialogueEventDat"/> index.</remarks>
    public required int? DialogueEvent { get; init; }

    /// <summary> Gets NPCTextAudioKeys.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required ReadOnlyCollection<int> NPCTextAudioKeys { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required ReadOnlyCollection<int> Unknown40 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }
}
