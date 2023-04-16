// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing TriggerSpawners.dat data.
/// </summary>
public sealed partial class TriggerSpawnersDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required ReadOnlyCollection<int> Unknown8 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required ReadOnlyCollection<int> Unknown28 { get; init; }

    /// <summary> Gets a value indicating whether Unknown44 is set.</summary>
    public required bool Unknown44 { get; init; }

    /// <summary>
    /// Gets TriggerSpawnersDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of TriggerSpawnersDat.</returns>
    internal static TriggerSpawnersDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/TriggerSpawners.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TriggerSpawnersDat[tableRows];
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
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var tempunknown28Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown28Loading = tempunknown28Loading.AsReadOnly();

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TriggerSpawnersDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Unknown44 = unknown44Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
