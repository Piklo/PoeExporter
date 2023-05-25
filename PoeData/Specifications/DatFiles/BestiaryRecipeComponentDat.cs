// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BestiaryRecipeComponent.dat data.
/// </summary>
public sealed partial class BestiaryRecipeComponentDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets BestiaryFamiliesKey.</summary>
    /// <remarks> references <see cref="BestiaryFamiliesDat"/> on <see cref="Specification.LoadBestiaryFamiliesDat"/> index.</remarks>
    public required int? BestiaryFamiliesKey { get; init; }

    /// <summary> Gets BestiaryGroupsKey.</summary>
    /// <remarks> references <see cref="BestiaryGroupsDat"/> on <see cref="Specification.LoadBestiaryGroupsDat"/> index.</remarks>
    public required int? BestiaryGroupsKey { get; init; }

    /// <summary> Gets ModsKey.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? ModsKey { get; init; }

    /// <summary> Gets BestiaryCapturableMonstersKey.</summary>
    /// <remarks> references <see cref="BestiaryCapturableMonstersDat"/> on <see cref="Specification.LoadBestiaryCapturableMonstersDat"/> index.</remarks>
    public required int? BestiaryCapturableMonstersKey { get; init; }

    /// <summary> Gets BeastRarity.</summary>
    /// <remarks> references <see cref="RarityDat"/> on <see cref="Specification.LoadRarityDat"/> index.</remarks>
    public required int? BeastRarity { get; init; }

    /// <summary> Gets BestiaryGenusKey.</summary>
    /// <remarks> references <see cref="BestiaryGenusDat"/> on <see cref="Specification.LoadBestiaryGenusDat"/> index.</remarks>
    public required int? BestiaryGenusKey { get; init; }
}
