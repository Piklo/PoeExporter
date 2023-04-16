// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HarvestDurability.dat data.
/// </summary>
public sealed partial class HarvestDurabilityDat
{
    /// <summary> Gets HarvestObjectsKey.</summary>
    /// <remarks> references <see cref="HarvestObjectsDat"/> on <see cref="Specification.LoadHarvestObjectsDat"/> index.</remarks>
    public required int? HarvestObjectsKey { get; init; }

    /// <summary> Gets Durability.</summary>
    public required int Durability { get; init; }

    /// <summary>
    /// Gets HarvestDurabilityDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HarvestDurabilityDat.</returns>
    internal static HarvestDurabilityDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HarvestDurability.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestDurabilityDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading HarvestObjectsKey
            (var harvestobjectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Durability
            (var durabilityLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestDurabilityDat()
            {
                HarvestObjectsKey = harvestobjectskeyLoading,
                Durability = durabilityLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
