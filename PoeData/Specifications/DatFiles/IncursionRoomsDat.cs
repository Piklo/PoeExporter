// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing IncursionRooms.dat data.
/// </summary>
public sealed partial class IncursionRoomsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets RoomUpgrade_IncursionRoomsKey.</summary>
    /// <remarks> references <see cref="IncursionRoomsDat"/> on <see cref="Specification.LoadIncursionRoomsDat"/> index.</remarks>
    public required int? RoomUpgrade_IncursionRoomsKey { get; init; }

    /// <summary> Gets Mods.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Mods { get; init; }

    /// <summary> Gets PresentARMFile.</summary>
    public required string PresentARMFile { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets IncursionArchitectKey.</summary>
    /// <remarks> references <see cref="IncursionArchitectDat"/> on <see cref="Specification.LoadIncursionArchitectDat"/> index.</remarks>
    public required int? IncursionArchitectKey { get; init; }

    /// <summary> Gets PastARMFile.</summary>
    public required string PastARMFile { get; init; }

    /// <summary> Gets TSIFile.</summary>
    public required string TSIFile { get; init; }

    /// <summary> Gets UIIcon.</summary>
    public required string UIIcon { get; init; }

    /// <summary> Gets FlavourText.</summary>
    public required string FlavourText { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKeys { get; init; }

    /// <summary> Gets Unknown132.</summary>
    public required int Unknown132 { get; init; }

    /// <summary> Gets Unknown136.</summary>
    public required int Unknown136 { get; init; }

    /// <summary> Gets RoomUpgradeFrom_IncursionRoomsKey.</summary>
    /// <remarks> references <see cref="IncursionRoomsDat"/> on <see cref="Specification.LoadIncursionRoomsDat"/> index.</remarks>
    public required int? RoomUpgradeFrom_IncursionRoomsKey { get; init; }

    /// <summary> Gets ItemisedFlavourText.</summary>
    /// <remarks> references <see cref="FlavourTextDat"/> on <see cref="Specification.LoadFlavourTextDat"/> index.</remarks>
    public required int? ItemisedFlavourText { get; init; }

    /// <summary> Gets Unknown164.</summary>
    public required string Unknown164 { get; init; }

    /// <summary> Gets Unknown172.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown172 { get; init; }
}
