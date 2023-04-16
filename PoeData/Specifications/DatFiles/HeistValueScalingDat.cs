// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistValueScaling.dat data.
/// </summary>
public sealed partial class HeistValueScalingDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required float Unknown8 { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required float Unknown12 { get; init; }

    /// <summary>
    /// Gets HeistValueScalingDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HeistValueScalingDat.</returns>
    internal static HeistValueScalingDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HeistValueScaling.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistValueScalingDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistValueScalingDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
