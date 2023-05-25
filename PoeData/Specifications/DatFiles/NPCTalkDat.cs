// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing NPCTalk.dat data.
/// </summary>
public sealed partial class NPCTalkDat
{
    /// <summary> Gets NPCKey.</summary>
    /// <remarks> references <see cref="NPCsDat"/> on <see cref="Specification.LoadNPCsDat"/> index.</remarks>
    public required int? NPCKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets DialogueOption.</summary>
    public required string DialogueOption { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required ReadOnlyCollection<int> Unknown28 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required ReadOnlyCollection<int> Unknown44 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required ReadOnlyCollection<int> Unknown60 { get; init; }

    /// <summary> Gets Script.</summary>
    public required string Script { get; init; }

    /// <summary> Gets TextAudio.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? TextAudio { get; init; }

    /// <summary> Gets Category.</summary>
    /// <remarks> references <see cref="NPCTalkCategoryDat"/> on <see cref="Specification.LoadNPCTalkCategoryDat"/> index.</remarks>
    public required int? Category { get; init; }

    /// <summary> Gets QuestRewardOffersKey.</summary>
    /// <remarks> references <see cref="QuestRewardOffersDat"/> on <see cref="Specification.LoadQuestRewardOffersDat"/> index.</remarks>
    public required int? QuestRewardOffersKey { get; init; }

    /// <summary> Gets QuestFlag.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.LoadQuestFlagsDat"/> index.</remarks>
    public required int? QuestFlag { get; init; }

    /// <summary> Gets NPCTextAudioKeys.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required ReadOnlyCollection<int> NPCTextAudioKeys { get; init; }

    /// <summary> Gets Script2.</summary>
    public required string Script2 { get; init; }

    /// <summary> Gets a value indicating whether Unknown172 is set.</summary>
    public required bool Unknown172 { get; init; }

    /// <summary> Gets a value indicating whether Unknown173 is set.</summary>
    public required bool Unknown173 { get; init; }

    /// <summary> Gets Unknown174.</summary>
    public required ReadOnlyCollection<int> Unknown174 { get; init; }

    /// <summary> Gets Unknown190.</summary>
    public required ReadOnlyCollection<int> Unknown190 { get; init; }

    /// <summary> Gets Unknown206.</summary>
    public required int Unknown206 { get; init; }

    /// <summary> Gets Unknown210.</summary>
    public required ReadOnlyCollection<int> Unknown210 { get; init; }

    /// <summary> Gets Unknown226.</summary>
    public required int Unknown226 { get; init; }

    /// <summary> Gets a value indicating whether Unknown230 is set.</summary>
    public required bool Unknown230 { get; init; }

    /// <summary> Gets Unknown231.</summary>
    public required int? Unknown231 { get; init; }

    /// <summary> Gets a value indicating whether Unknown247 is set.</summary>
    public required bool Unknown247 { get; init; }

    /// <summary> Gets a value indicating whether Unknown248 is set.</summary>
    public required bool Unknown248 { get; init; }

    /// <summary> Gets DialogueOption2.</summary>
    public required string DialogueOption2 { get; init; }

    /// <summary> Gets Unknown257.</summary>
    public required int? Unknown257 { get; init; }

    /// <summary> Gets Unknown273.</summary>
    public required int? Unknown273 { get; init; }

    /// <summary> Gets Unknown289.</summary>
    public required int Unknown289 { get; init; }

    /// <summary> Gets Unknown293.</summary>
    public required ReadOnlyCollection<int> Unknown293 { get; init; }

    /// <summary> Gets Unknown309.</summary>
    public required int Unknown309 { get; init; }
}
