// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ClientLakeDifficulty.dat data.
/// </summary>
public sealed partial class ClientLakeDifficultyDat
{
    /// <summary> Gets Id.</summary>
    public required int Id { get; init; }

    /// <summary> Gets Scaling.</summary>
    public required float Scaling { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required int Unknown12 { get; init; }

    /// <summary>
    /// Gets ClientLakeDifficultyDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ClientLakeDifficultyDat.</returns>
    internal static ClientLakeDifficultyDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ClientLakeDifficulty.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ClientLakeDifficultyDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Scaling
            (var scalingLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ClientLakeDifficultyDat()
            {
                Id = idLoading,
                Scaling = scalingLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
