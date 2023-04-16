// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MetamorphosisRewardTypeItemsClient.dat data.
/// </summary>
public sealed partial class MetamorphosisRewardTypeItemsClientDat
{
    /// <summary> Gets MetamorphosisRewardTypesKey.</summary>
    /// <remarks> references <see cref="MetamorphosisRewardTypesDat"/> on <see cref="Specification.GetMetamorphosisRewardTypesDat"/> index.</remarks>
    public required int? MetamorphosisRewardTypesKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary>
    /// Gets MetamorphosisRewardTypeItemsClientDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MetamorphosisRewardTypeItemsClientDat.</returns>
    internal static MetamorphosisRewardTypeItemsClientDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MetamorphosisRewardTypeItemsClient.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MetamorphosisRewardTypeItemsClientDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MetamorphosisRewardTypesKey
            (var metamorphosisrewardtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MetamorphosisRewardTypeItemsClientDat()
            {
                MetamorphosisRewardTypesKey = metamorphosisrewardtypeskeyLoading,
                Unknown16 = unknown16Loading,
                Description = descriptionLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
