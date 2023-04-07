// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing IncursionRooms.dat data.
/// </summary>
public sealed partial class IncursionRoomsDat : IDat<IncursionRoomsDat>
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
    /// <remarks> references <see cref="IncursionRoomsDat"/> on <see cref="Specification.GetIncursionRoomsDat"/> index.</remarks>
    public required int? RoomUpgrade_IncursionRoomsKey { get; init; }

    /// <summary> Gets Mods.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Mods { get; init; }

    /// <summary> Gets PresentARMFile.</summary>
    public required string PresentARMFile { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets IncursionArchitectKey.</summary>
    /// <remarks> references <see cref="IncursionArchitectDat"/> on <see cref="Specification.GetIncursionArchitectDat"/> index.</remarks>
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
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKeys { get; init; }

    /// <summary> Gets Unknown132.</summary>
    public required int Unknown132 { get; init; }

    /// <summary> Gets Unknown136.</summary>
    public required int Unknown136 { get; init; }

    /// <summary> Gets RoomUpgradeFrom_IncursionRoomsKey.</summary>
    /// <remarks> references <see cref="IncursionRoomsDat"/> on <see cref="Specification.GetIncursionRoomsDat"/> index.</remarks>
    public required int? RoomUpgradeFrom_IncursionRoomsKey { get; init; }

    /// <summary> Gets ItemisedFlavourText.</summary>
    /// <remarks> references <see cref="FlavourTextDat"/> on <see cref="Specification.GetFlavourTextDat"/> index.</remarks>
    public required int? ItemisedFlavourText { get; init; }

    /// <summary> Gets Unknown164.</summary>
    public required string Unknown164 { get; init; }

    /// <summary> Gets Unknown172.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown172 { get; init; }

    /// <inheritdoc/>
    public static IncursionRoomsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/IncursionRooms.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new IncursionRoomsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RoomUpgrade_IncursionRoomsKey
            (var roomupgrade_incursionroomskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Mods
            (var tempmodsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modsLoading = tempmodsLoading.AsReadOnly();

            // loading PresentARMFile
            (var presentarmfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IncursionArchitectKey
            (var incursionarchitectkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading PastARMFile
            (var pastarmfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TSIFile
            (var tsifileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading UIIcon
            (var uiiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FlavourText
            (var flavourtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading Unknown132
            (var unknown132Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown136
            (var unknown136Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RoomUpgradeFrom_IncursionRoomsKey
            (var roomupgradefrom_incursionroomskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading ItemisedFlavourText
            (var itemisedflavourtextLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown164
            (var unknown164Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown172
            (var tempunknown172Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown172Loading = tempunknown172Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new IncursionRoomsDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Tier = tierLoading,
                MinLevel = minlevelLoading,
                RoomUpgrade_IncursionRoomsKey = roomupgrade_incursionroomskeyLoading,
                Mods = modsLoading,
                PresentARMFile = presentarmfileLoading,
                HASH16 = hash16Loading,
                IncursionArchitectKey = incursionarchitectkeyLoading,
                PastARMFile = pastarmfileLoading,
                TSIFile = tsifileLoading,
                UIIcon = uiiconLoading,
                FlavourText = flavourtextLoading,
                Description = descriptionLoading,
                AchievementItemsKeys = achievementitemskeysLoading,
                Unknown132 = unknown132Loading,
                Unknown136 = unknown136Loading,
                RoomUpgradeFrom_IncursionRoomsKey = roomupgradefrom_incursionroomskeyLoading,
                ItemisedFlavourText = itemisedflavourtextLoading,
                Unknown164 = unknown164Loading,
                Unknown172 = unknown172Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
