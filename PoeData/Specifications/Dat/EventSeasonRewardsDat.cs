// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing EventSeasonRewards.dat data.
/// </summary>
public sealed partial class EventSeasonRewardsDat : ISpecificationFile<EventSeasonRewardsDat>
{
    /// <summary> Gets EventSeasonKey.</summary>
    public required int? EventSeasonKey { get; init; }

    /// <summary> Gets Point.</summary>
    public required int Point { get; init; }

    /// <summary> Gets RewardCommand.</summary>
    public required string RewardCommand { get; init; }

    /// <inheritdoc/>
    public static EventSeasonRewardsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/EventSeasonRewards.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EventSeasonRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetEventSeasonDat();

            // loading EventSeasonKey
            (var eventseasonkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Point
            (var pointLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RewardCommand
            (var rewardcommandLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EventSeasonRewardsDat()
            {
                EventSeasonKey = eventseasonkeyLoading,
                Point = pointLoading,
                RewardCommand = rewardcommandLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
