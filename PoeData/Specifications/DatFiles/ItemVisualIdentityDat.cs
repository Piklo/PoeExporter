// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ItemVisualIdentity.dat data.
/// </summary>
public sealed partial class ItemVisualIdentityDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets DDSFile.</summary>
    public required string DDSFile { get; init; }

    /// <summary> Gets AOFile.</summary>
    public required string AOFile { get; init; }

    /// <summary> Gets InventorySoundEffect.</summary>
    /// <remarks> references <see cref="SoundEffectsDat"/> on <see cref="Specification.LoadSoundEffectsDat"/> index.</remarks>
    public required int? InventorySoundEffect { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets AOFile2.</summary>
    public required string AOFile2 { get; init; }

    /// <summary> Gets MarauderSMFiles.</summary>
    public required ReadOnlyCollection<string> MarauderSMFiles { get; init; }

    /// <summary> Gets RangerSMFiles.</summary>
    public required ReadOnlyCollection<string> RangerSMFiles { get; init; }

    /// <summary> Gets WitchSMFiles.</summary>
    public required ReadOnlyCollection<string> WitchSMFiles { get; init; }

    /// <summary> Gets DuelistDexSMFiles.</summary>
    public required ReadOnlyCollection<string> DuelistDexSMFiles { get; init; }

    /// <summary> Gets TemplarSMFiles.</summary>
    public required ReadOnlyCollection<string> TemplarSMFiles { get; init; }

    /// <summary> Gets ShadowSMFiles.</summary>
    public required ReadOnlyCollection<string> ShadowSMFiles { get; init; }

    /// <summary> Gets ScionSMFiles.</summary>
    public required ReadOnlyCollection<string> ScionSMFiles { get; init; }

    /// <summary> Gets MarauderShape.</summary>
    public required string MarauderShape { get; init; }

    /// <summary> Gets RangerShape.</summary>
    public required string RangerShape { get; init; }

    /// <summary> Gets WitchShape.</summary>
    public required string WitchShape { get; init; }

    /// <summary> Gets DuelistShape.</summary>
    public required string DuelistShape { get; init; }

    /// <summary> Gets TemplarShape.</summary>
    public required string TemplarShape { get; init; }

    /// <summary> Gets ShadowShape.</summary>
    public required string ShadowShape { get; init; }

    /// <summary> Gets ScionShape.</summary>
    public required string ScionShape { get; init; }

    /// <summary> Gets Unknown220.</summary>
    public required int Unknown220 { get; init; }

    /// <summary> Gets Unknown224.</summary>
    public required int Unknown224 { get; init; }

    /// <summary> Gets Pickup_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Pickup_AchievementItemsKeys { get; init; }

    /// <summary> Gets SMFiles.</summary>
    public required ReadOnlyCollection<string> SMFiles { get; init; }

    /// <summary> Gets Identify_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Identify_AchievementItemsKeys { get; init; }

    /// <summary> Gets EPKFile.</summary>
    public required string EPKFile { get; init; }

    /// <summary> Gets Corrupt_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Corrupt_AchievementItemsKeys { get; init; }

    /// <summary> Gets a value indicating whether IsAlternateArt is set.</summary>
    public required bool IsAlternateArt { get; init; }

    /// <summary> Gets a value indicating whether Unknown301 is set.</summary>
    public required bool Unknown301 { get; init; }

    /// <summary> Gets CreateCorruptedJewelAchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? CreateCorruptedJewelAchievementItemsKey { get; init; }

    /// <summary> Gets AnimationLocation.</summary>
    public required string AnimationLocation { get; init; }

    /// <summary> Gets Unknown326.</summary>
    public required string Unknown326 { get; init; }

    /// <summary> Gets Unknown334.</summary>
    public required string Unknown334 { get; init; }

    /// <summary> Gets Unknown342.</summary>
    public required string Unknown342 { get; init; }

    /// <summary> Gets Unknown350.</summary>
    public required string Unknown350 { get; init; }

    /// <summary> Gets Unknown358.</summary>
    public required string Unknown358 { get; init; }

    /// <summary> Gets Unknown366.</summary>
    public required string Unknown366 { get; init; }

    /// <summary> Gets Unknown374.</summary>
    public required string Unknown374 { get; init; }

    /// <summary> Gets Unknown382.</summary>
    public required string Unknown382 { get; init; }

    /// <summary> Gets Unknown390.</summary>
    public required string Unknown390 { get; init; }

    /// <summary> Gets Unknown398.</summary>
    public required string Unknown398 { get; init; }

    /// <summary> Gets Unknown406.</summary>
    public required string Unknown406 { get; init; }

    /// <summary> Gets Unknown414.</summary>
    public required string Unknown414 { get; init; }

    /// <summary> Gets a value indicating whether IsAtlasOfWorldsMapIcon is set.</summary>
    public required bool IsAtlasOfWorldsMapIcon { get; init; }

    /// <summary> Gets a value indicating whether IsTier16Icon is set.</summary>
    public required bool IsTier16Icon { get; init; }

    /// <summary> Gets Unknown424.</summary>
    public required ReadOnlyCollection<int> Unknown424 { get; init; }

    /// <summary> Gets a value indicating whether Unknown440 is set.</summary>
    public required bool Unknown440 { get; init; }

    /// <summary> Gets Unknown441.</summary>
    public required ReadOnlyCollection<int> Unknown441 { get; init; }

    /// <summary> Gets Unknown457.</summary>
    public required string Unknown457 { get; init; }

    /// <summary> Gets Unknown465.</summary>
    public required int Unknown465 { get; init; }

    /// <summary> Gets Unknown469.</summary>
    public required int? Unknown469 { get; init; }

    /// <summary> Gets Unknown485.</summary>
    public required int? Unknown485 { get; init; }

    /// <summary> Gets Unknown501.</summary>
    public required int? Unknown501 { get; init; }

    /// <summary> Gets Unknown517.</summary>
    public required int? Unknown517 { get; init; }
}
