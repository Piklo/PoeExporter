// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing BetrayalTraitorRewards.dat data.
/// </summary>
public sealed partial class BetrayalTraitorRewardsDat : IDat<BetrayalTraitorRewardsDat>
{
    /// <summary> Gets BetrayalJobsKey.</summary>
    /// <remarks> references <see cref="BetrayalJobsDat"/> on <see cref="Specification.GetBetrayalJobsDat"/> index.</remarks>
    public required int? BetrayalJobsKey { get; init; }

    /// <summary> Gets BetrayalTargetsKey.</summary>
    /// <remarks> references <see cref="BetrayalTargetsDat"/> on <see cref="Specification.GetBetrayalTargetsDat"/> index.</remarks>
    public required int? BetrayalTargetsKey { get; init; }

    /// <summary> Gets BetrayalRanksKey.</summary>
    /// <remarks> references <see cref="BetrayalRanksDat"/> on <see cref="Specification.GetBetrayalRanksDat"/> index.</remarks>
    public required int? BetrayalRanksKey { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <inheritdoc/>
    public static BetrayalTraitorRewardsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/BetrayalTraitorRewards.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BetrayalTraitorRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BetrayalJobsKey
            (var betrayaljobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BetrayalTargetsKey
            (var betrayaltargetskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BetrayalRanksKey
            (var betrayalrankskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BetrayalTraitorRewardsDat()
            {
                BetrayalJobsKey = betrayaljobskeyLoading,
                BetrayalTargetsKey = betrayaltargetskeyLoading,
                BetrayalRanksKey = betrayalrankskeyLoading,
                Description = descriptionLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
