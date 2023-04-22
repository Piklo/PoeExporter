// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DelveMonsterSpawners.dat data.
/// </summary>
public sealed partial class DelveMonsterSpawnersDat
{
    /// <summary> Gets BaseMetadata.</summary>
    public required string BaseMetadata { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required ReadOnlyCollection<int> Unknown12 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int Unknown52 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <summary> Gets a value indicating whether Unknown60 is set.</summary>
    public required bool Unknown60 { get; init; }

    /// <summary> Gets a value indicating whether Unknown61 is set.</summary>
    public required bool Unknown61 { get; init; }

    /// <summary> Gets a value indicating whether Unknown62 is set.</summary>
    public required bool Unknown62 { get; init; }

    /// <summary> Gets a value indicating whether Unknown63 is set.</summary>
    public required bool Unknown63 { get; init; }

    /// <summary> Gets a value indicating whether Unknown64 is set.</summary>
    public required bool Unknown64 { get; init; }

    /// <summary> Gets a value indicating whether Unknown65 is set.</summary>
    public required bool Unknown65 { get; init; }

    /// <summary> Gets a value indicating whether Unknown66 is set.</summary>
    public required bool Unknown66 { get; init; }

    /// <summary> Gets a value indicating whether Unknown67 is set.</summary>
    public required bool Unknown67 { get; init; }

    /// <summary> Gets a value indicating whether Unknown68 is set.</summary>
    public required bool Unknown68 { get; init; }

    /// <summary> Gets a value indicating whether Unknown69 is set.</summary>
    public required bool Unknown69 { get; init; }

    /// <summary> Gets Unknown70.</summary>
    public required int Unknown70 { get; init; }

    /// <summary> Gets Unknown74.</summary>
    public required int Unknown74 { get; init; }

    /// <summary> Gets Unknown78.</summary>
    public required int Unknown78 { get; init; }

    /// <summary> Gets Unknown82.</summary>
    public required int Unknown82 { get; init; }

    /// <summary> Gets Unknown86.</summary>
    public required int? Unknown86 { get; init; }

    /// <summary> Gets a value indicating whether Unknown102 is set.</summary>
    public required bool Unknown102 { get; init; }

    /// <summary> Gets a value indicating whether Unknown103 is set.</summary>
    public required bool Unknown103 { get; init; }

    /// <summary> Gets Unknown104.</summary>
    public required int Unknown104 { get; init; }

    /// <summary> Gets Script.</summary>
    public required string Script { get; init; }

    /// <summary> Gets a value indicating whether Unknown116 is set.</summary>
    public required bool Unknown116 { get; init; }

    /// <summary>
    /// Gets DelveMonsterSpawnersDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of DelveMonsterSpawnersDat.</returns>
    internal static DelveMonsterSpawnersDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/DelveMonsterSpawners.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveMonsterSpawnersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseMetadata
            (var basemetadataLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var tempunknown12Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown12Loading = tempunknown12Loading.AsReadOnly();

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown62
            (var unknown62Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown63
            (var unknown63Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown65
            (var unknown65Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown67
            (var unknown67Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown69
            (var unknown69Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown70
            (var unknown70Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown74
            (var unknown74Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown78
            (var unknown78Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown82
            (var unknown82Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown86
            (var unknown86Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown102
            (var unknown102Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown103
            (var unknown103Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown104
            (var unknown104Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Script
            (var scriptLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveMonsterSpawnersDat()
            {
                BaseMetadata = basemetadataLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Unknown28 = unknown28Loading,
                Unknown32 = unknown32Loading,
                Unknown36 = unknown36Loading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                Unknown56 = unknown56Loading,
                Unknown60 = unknown60Loading,
                Unknown61 = unknown61Loading,
                Unknown62 = unknown62Loading,
                Unknown63 = unknown63Loading,
                Unknown64 = unknown64Loading,
                Unknown65 = unknown65Loading,
                Unknown66 = unknown66Loading,
                Unknown67 = unknown67Loading,
                Unknown68 = unknown68Loading,
                Unknown69 = unknown69Loading,
                Unknown70 = unknown70Loading,
                Unknown74 = unknown74Loading,
                Unknown78 = unknown78Loading,
                Unknown82 = unknown82Loading,
                Unknown86 = unknown86Loading,
                Unknown102 = unknown102Loading,
                Unknown103 = unknown103Loading,
                Unknown104 = unknown104Loading,
                Script = scriptLoading,
                Unknown116 = unknown116Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
