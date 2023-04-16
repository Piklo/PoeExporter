// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AdditionalLifeScaling.dat data.
/// </summary>
public sealed partial class AdditionalLifeScalingDat
{
    /// <summary> Gets IntId.</summary>
    public required int IntId { get; init; }

    /// <summary> Gets ID.</summary>
    public required string ID { get; init; }

    /// <summary> Gets DatFile.</summary>
    public required string DatFile { get; init; }

    /// <summary>
    /// Gets AdditionalLifeScalingDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AdditionalLifeScalingDat.</returns>
    internal static AdditionalLifeScalingDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/AdditionalLifeScaling.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AdditionalLifeScalingDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading IntId
            (var intidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ID
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DatFile
            (var datfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AdditionalLifeScalingDat()
            {
                IntId = intidLoading,
                ID = idLoading,
                DatFile = datfileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
