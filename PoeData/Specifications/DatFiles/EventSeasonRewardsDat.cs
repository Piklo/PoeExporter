// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing EventSeasonRewards.dat data.
/// </summary>
public sealed partial class EventSeasonRewardsDat
{
    /// <summary> Gets EventSeasonKey.</summary>
    /// <remarks> references <see cref="EventSeasonDat"/> on <see cref="Specification.GetEventSeasonDat"/> index.</remarks>
    public required int? EventSeasonKey { get; init; }

    /// <summary> Gets Point.</summary>
    public required int Point { get; init; }

    /// <summary> Gets RewardCommand.</summary>
    public required string RewardCommand { get; init; }

    /// <summary>
    /// Gets EventSeasonRewardsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of EventSeasonRewardsDat.</returns>
    internal static EventSeasonRewardsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/EventSeasonRewards.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

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

            // loading EventSeasonKey
            (var eventseasonkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Point
            (var pointLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RewardCommand
            (var rewardcommandLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
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
