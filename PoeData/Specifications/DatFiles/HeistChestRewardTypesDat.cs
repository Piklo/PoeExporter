// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistChestRewardTypes.dat data.
/// </summary>
public sealed partial class HeistChestRewardTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Art.</summary>
    public required string Art { get; init; }

    /// <summary> Gets RewardTypeName.</summary>
    public required string RewardTypeName { get; init; }

    /// <summary> Gets Unknown24.</summary>
    /// <remarks> references <see cref="HeistChestRewardTypesDat"/> on <see cref="Specification.LoadHeistChestRewardTypesDat"/> index.</remarks>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets RewardRoomName.</summary>
    public required string RewardRoomName { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets Weight.</summary>
    public required int Weight { get; init; }

    /// <summary> Gets RewardRoomName2.</summary>
    public required string RewardRoomName2 { get; init; }

    /// <summary> Gets HeistJobsKey.</summary>
    /// <remarks> references <see cref="HeistJobsDat"/> on <see cref="Specification.LoadHeistJobsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> HeistJobsKey { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required int Unknown76 { get; init; }

    /// <summary>
    /// Gets HeistChestRewardTypesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HeistChestRewardTypesDat.</returns>
    internal static HeistChestRewardTypesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HeistChestRewardTypes.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistChestRewardTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Art
            (var artLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RewardTypeName
            (var rewardtypenameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading RewardRoomName
            (var rewardroomnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Weight
            (var weightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RewardRoomName2
            (var rewardroomname2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HeistJobsKey
            (var tempheistjobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var heistjobskeyLoading = tempheistjobskeyLoading.AsReadOnly();

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistChestRewardTypesDat()
            {
                Id = idLoading,
                Art = artLoading,
                RewardTypeName = rewardtypenameLoading,
                Unknown24 = unknown24Loading,
                RewardRoomName = rewardroomnameLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
                Weight = weightLoading,
                RewardRoomName2 = rewardroomname2Loading,
                HeistJobsKey = heistjobskeyLoading,
                Unknown76 = unknown76Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
