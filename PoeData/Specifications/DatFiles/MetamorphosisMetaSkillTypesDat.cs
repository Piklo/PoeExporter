// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MetamorphosisMetaSkillTypes.dat data.
/// </summary>
public sealed partial class MetamorphosisMetaSkillTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets UnavailableArt.</summary>
    public required string UnavailableArt { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required string Unknown32 { get; init; }

    /// <summary> Gets AvailableArt.</summary>
    public required string AvailableArt { get; init; }

    /// <summary> Gets ItemisedSample.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? ItemisedSample { get; init; }

    /// <summary> Gets BodypartName.</summary>
    public required string BodypartName { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }

    /// <summary> Gets AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKeys { get; init; }

    /// <summary> Gets BodypartNamePlural.</summary>
    public required string BodypartNamePlural { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required int Unknown100 { get; init; }
}
