// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HarvestSeedTypes.dat data.
/// </summary>
public sealed partial class HarvestSeedTypesDat
{
    /// <summary> Gets HarvestObjectsKey.</summary>
    /// <remarks> references <see cref="HarvestObjectsDat"/> on <see cref="Specification.LoadHarvestObjectsDat"/> index.</remarks>
    public required int? HarvestObjectsKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int? Unknown16 { get; init; }

    /// <summary> Gets GrowthCycles.</summary>
    public required int GrowthCycles { get; init; }

    /// <summary> Gets AOFiles.</summary>
    public required ReadOnlyCollection<string> AOFiles { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required ReadOnlyCollection<int> Unknown52 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int Unknown68 { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets RequiredNearbySeed_Tier.</summary>
    public required int RequiredNearbySeed_Tier { get; init; }

    /// <summary> Gets RequiredNearbySeed_Amount.</summary>
    public required int RequiredNearbySeed_Amount { get; init; }

    /// <summary> Gets WildLifeforceConsumedPercentage.</summary>
    public required int WildLifeforceConsumedPercentage { get; init; }

    /// <summary> Gets VividLifeforceConsumedPercentage.</summary>
    public required int VividLifeforceConsumedPercentage { get; init; }

    /// <summary> Gets PrimalLifeforceConsumedPercentage.</summary>
    public required int PrimalLifeforceConsumedPercentage { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets HarvestCraftOptionsKeys.</summary>
    /// <remarks> references <see cref="HarvestCraftOptionsDat"/> on <see cref="Specification.LoadHarvestCraftOptionsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> HarvestCraftOptionsKeys { get; init; }

    /// <summary> Gets Unknown120.</summary>
    public required int Unknown120 { get; init; }

    /// <summary> Gets Unknown124.</summary>
    public required ReadOnlyCollection<int> Unknown124 { get; init; }

    /// <summary> Gets AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKeys { get; init; }

    /// <summary> Gets OutcomeType.</summary>
    public required int OutcomeType { get; init; }
}
