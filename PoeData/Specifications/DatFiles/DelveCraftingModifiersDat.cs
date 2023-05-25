// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DelveCraftingModifiers.dat data.
/// </summary>
public sealed partial class DelveCraftingModifiersDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets AddedModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AddedModsKeys { get; init; }

    /// <summary> Gets NegativeWeight_TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> NegativeWeight_TagsKeys { get; init; }

    /// <summary> Gets NegativeWeight_Values.</summary>
    public required ReadOnlyCollection<int> NegativeWeight_Values { get; init; }

    /// <summary> Gets ForcedAddModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ForcedAddModsKeys { get; init; }

    /// <summary> Gets ForbiddenDelveCraftingTagsKeys.</summary>
    /// <remarks> references <see cref="DelveCraftingTagsDat"/> on <see cref="Specification.LoadDelveCraftingTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ForbiddenDelveCraftingTagsKeys { get; init; }

    /// <summary> Gets AllowedDelveCraftingTagsKeys.</summary>
    /// <remarks> references <see cref="DelveCraftingTagsDat"/> on <see cref="Specification.LoadDelveCraftingTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AllowedDelveCraftingTagsKeys { get; init; }

    /// <summary> Gets a value indicating whether CanMirrorItem is set.</summary>
    public required bool CanMirrorItem { get; init; }

    /// <summary> Gets CorruptedEssenceChance.</summary>
    public required int CorruptedEssenceChance { get; init; }

    /// <summary> Gets a value indicating whether CanImproveQuality is set.</summary>
    public required bool CanImproveQuality { get; init; }

    /// <summary> Gets a value indicating whether CanRollEnchant is set.</summary>
    public required bool CanRollEnchant { get; init; }

    /// <summary> Gets a value indicating whether HasLuckyRolls is set.</summary>
    public required bool HasLuckyRolls { get; init; }

    /// <summary> Gets SellPrice_ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SellPrice_ModsKeys { get; init; }

    /// <summary> Gets a value indicating whether CanRollWhiteSockets is set.</summary>
    public required bool CanRollWhiteSockets { get; init; }

    /// <summary> Gets Weight_TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Weight_TagsKeys { get; init; }

    /// <summary> Gets Weight_Values.</summary>
    public required ReadOnlyCollection<int> Weight_Values { get; init; }

    /// <summary> Gets DelveCraftingModifierDescriptionsKeys.</summary>
    /// <remarks> references <see cref="DelveCraftingModifierDescriptionsDat"/> on <see cref="Specification.LoadDelveCraftingModifierDescriptionsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> DelveCraftingModifierDescriptionsKeys { get; init; }

    /// <summary> Gets BlockedDelveCraftingModifierDescriptionsKeys.</summary>
    /// <remarks> references <see cref="DelveCraftingModifierDescriptionsDat"/> on <see cref="Specification.LoadDelveCraftingModifierDescriptionsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BlockedDelveCraftingModifierDescriptionsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown201 is set.</summary>
    public required bool Unknown201 { get; init; }

    /// <summary> Gets a value indicating whether Unknown202 is set.</summary>
    public required bool Unknown202 { get; init; }

    /// <summary> Gets Unknown203.</summary>
    public required ReadOnlyCollection<int> Unknown203 { get; init; }

    /// <summary> Gets Unknown219.</summary>
    public required ReadOnlyCollection<int> Unknown219 { get; init; }
}
