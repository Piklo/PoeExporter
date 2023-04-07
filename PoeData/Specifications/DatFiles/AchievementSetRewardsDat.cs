// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AchievementSetRewards.dat data.
/// </summary>
public sealed partial class AchievementSetRewardsDat
{
    /// <summary> Gets SetId.</summary>
    /// <remarks> references <see cref="AchievementSetsDisplayDat"/> on <see cref="AchievementSetsDisplayDat.Id"/>.</remarks>
    public required int SetId { get; init; }

    /// <summary> Gets AchievementsRequired.</summary>
    public required int AchievementsRequired { get; init; }

    /// <summary> Gets Rewards.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Rewards { get; init; }

    /// <summary> Gets TotemPieceEveryNAchievements.</summary>
    public required int TotemPieceEveryNAchievements { get; init; }

    /// <summary> Gets Message.</summary>
    public required string Message { get; init; }

    /// <summary> Gets NotificationIcon.</summary>
    public required string NotificationIcon { get; init; }

    /// <summary> Gets HideoutName.</summary>
    public required string HideoutName { get; init; }

    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary>
    /// Gets AchievementSetRewardsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AchievementSetRewardsDat.</returns>
    internal static AchievementSetRewardsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/AchievementSetRewards.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AchievementSetRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading SetId
            (var setidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AchievementsRequired
            (var achievementsrequiredLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Rewards
            (var temprewardsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var rewardsLoading = temprewardsLoading.AsReadOnly();

            // loading TotemPieceEveryNAchievements
            (var totempieceeverynachievementsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Message
            (var messageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading NotificationIcon
            (var notificationiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HideoutName
            (var hideoutnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AchievementSetRewardsDat()
            {
                SetId = setidLoading,
                AchievementsRequired = achievementsrequiredLoading,
                Rewards = rewardsLoading,
                TotemPieceEveryNAchievements = totempieceeverynachievementsLoading,
                Message = messageLoading,
                NotificationIcon = notificationiconLoading,
                HideoutName = hideoutnameLoading,
                Id = idLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
