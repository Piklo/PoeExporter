// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ArchnemesisMetaRewards.dat data.
/// </summary>
public sealed partial class ArchnemesisMetaRewardsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets RewardText.</summary>
    public required string RewardText { get; init; }

    /// <summary> Gets RewardGroup.</summary>
    public required int RewardGroup { get; init; }

    /// <summary> Gets ScriptArgument.</summary>
    public required string ScriptArgument { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary>
    /// Gets ArchnemesisMetaRewardsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ArchnemesisMetaRewardsDat.</returns>
    internal static ArchnemesisMetaRewardsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ArchnemesisMetaRewards.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ArchnemesisMetaRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RewardText
            (var rewardtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RewardGroup
            (var rewardgroupLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ScriptArgument
            (var scriptargumentLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ArchnemesisMetaRewardsDat()
            {
                Id = idLoading,
                RewardText = rewardtextLoading,
                RewardGroup = rewardgroupLoading,
                ScriptArgument = scriptargumentLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
