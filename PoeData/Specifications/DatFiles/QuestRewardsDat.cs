// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing QuestRewards.dat data.
/// </summary>
public sealed partial class QuestRewardsDat
{
    /// <summary> Gets RewardOffer.</summary>
    /// <remarks> references <see cref="QuestRewardOffersDat"/> on <see cref="Specification.LoadQuestRewardOffersDat"/> index.</remarks>
    public required int? RewardOffer { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Characters.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.LoadCharactersDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Characters { get; init; }

    /// <summary> Gets Reward.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? Reward { get; init; }

    /// <summary> Gets RewardLevel.</summary>
    public required int RewardLevel { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int? Unknown56 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required string Unknown76 { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required ReadOnlyCollection<int> Unknown84 { get; init; }

    /// <summary> Gets RewardStack.</summary>
    public required int RewardStack { get; init; }

    /// <summary> Gets a value indicating whether Unknown104 is set.</summary>
    public required bool Unknown104 { get; init; }

    /// <summary> Gets a value indicating whether Unknown105 is set.</summary>
    public required bool Unknown105 { get; init; }

    /// <summary> Gets Unknown106.</summary>
    public required ReadOnlyCollection<int> Unknown106 { get; init; }

    /// <summary> Gets Unknown122.</summary>
    public required int Unknown122 { get; init; }

    /// <summary> Gets Unknown126.</summary>
    public required int Unknown126 { get; init; }

    /// <summary> Gets Unknown130.</summary>
    public required ReadOnlyCollection<int> Unknown130 { get; init; }

    /// <summary> Gets Unknown146.</summary>
    public required int Unknown146 { get; init; }

    /// <summary> Gets Unknown150.</summary>
    public required int? Unknown150 { get; init; }

    /// <summary> Gets Unknown166.</summary>
    public required int Unknown166 { get; init; }
}
