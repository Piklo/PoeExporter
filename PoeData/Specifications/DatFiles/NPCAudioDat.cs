// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing NPCAudio.dat data.
/// </summary>
public sealed partial class NPCAudioDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required ReadOnlyCollection<int> Unknown8 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required ReadOnlyCollection<int> Unknown24 { get; init; }

    /// <summary> Gets VolumePercentage.</summary>
    public required int VolumePercentage { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int Unknown52 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int? Unknown60 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required int Unknown76 { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required int Unknown80 { get; init; }

    /// <summary>
    /// Gets NPCAudioDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of NPCAudioDat.</returns>
    internal static NPCAudioDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/NPCAudio.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCAudioDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var tempunknown8Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown8Loading = tempunknown8Loading.AsReadOnly();

            // loading Unknown24
            (var tempunknown24Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown24Loading = tempunknown24Loading.AsReadOnly();

            // loading VolumePercentage
            (var volumepercentageLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCAudioDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown24 = unknown24Loading,
                VolumePercentage = volumepercentageLoading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
                Unknown60 = unknown60Loading,
                Unknown76 = unknown76Loading,
                Unknown80 = unknown80Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
