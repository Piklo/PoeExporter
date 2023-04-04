// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing SpawnAdditionalChestsOrClusters.dat data.
/// </summary>
public sealed partial class SpawnAdditionalChestsOrClustersDat : IDat<SpawnAdditionalChestsOrClustersDat>
{
    /// <summary> Gets StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? StatsKey { get; init; }

    /// <summary> Gets ChestsKey.</summary>
    /// <remarks> references <see cref="ChestsDat"/> on <see cref="Specification.GetChestsDat"/> index.</remarks>
    public required int? ChestsKey { get; init; }

    /// <summary> Gets ChestClustersKey.</summary>
    /// <remarks> references <see cref="ChestClustersDat"/> on <see cref="Specification.GetChestClustersDat"/> index.</remarks>
    public required int? ChestClustersKey { get; init; }

    /// <inheritdoc/>
    public static SpawnAdditionalChestsOrClustersDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/SpawnAdditionalChestsOrClusters.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SpawnAdditionalChestsOrClustersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ChestsKey
            (var chestskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ChestClustersKey
            (var chestclusterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SpawnAdditionalChestsOrClustersDat()
            {
                StatsKey = statskeyLoading,
                ChestsKey = chestskeyLoading,
                ChestClustersKey = chestclusterskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
