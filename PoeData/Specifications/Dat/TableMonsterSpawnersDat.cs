// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing TableMonsterSpawners.dat data.
/// </summary>
public sealed partial class TableMonsterSpawnersDat : ISpecificationFile<TableMonsterSpawnersDat>
{
    /// <summary> Gets Metadata.</summary>
    public required string Metadata { get; init; }

    /// <summary> Gets AreaLevel.</summary>
    public required int AreaLevel { get; init; }

    /// <summary> Gets SpawnsMonsters.</summary>
    public required ReadOnlyCollection<int> SpawnsMonsters { get; init; }

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

    /// <summary> Gets Unknown66.</summary>
    public required int Unknown66 { get; init; }

    /// <summary> Gets Unknown70.</summary>
    public required int Unknown70 { get; init; }

    /// <summary> Gets Unknown74.</summary>
    public required int Unknown74 { get; init; }

    /// <summary> Gets Unknown78.</summary>
    public required int Unknown78 { get; init; }

    /// <summary> Gets Unknown82.</summary>
    public required int? Unknown82 { get; init; }

    /// <summary> Gets a value indicating whether Unknown98 is set.</summary>
    public required bool Unknown98 { get; init; }

    /// <summary> Gets a value indicating whether Unknown99 is set.</summary>
    public required bool Unknown99 { get; init; }

    /// <summary> Gets Script1.</summary>
    public required string Script1 { get; init; }

    /// <summary> Gets a value indicating whether Unknown108 is set.</summary>
    public required bool Unknown108 { get; init; }

    /// <summary> Gets a value indicating whether Unknown109 is set.</summary>
    public required bool Unknown109 { get; init; }

    /// <summary> Gets Script2.</summary>
    public required string Script2 { get; init; }

    /// <summary> Gets Unknown118.</summary>
    public required ReadOnlyCollection<int> Unknown118 { get; init; }

    /// <summary> Gets Unknown134.</summary>
    public required int Unknown134 { get; init; }

    /// <summary> Gets Unknown138.</summary>
    public required int Unknown138 { get; init; }

    /// <summary> Gets Unknown142.</summary>
    public required int Unknown142 { get; init; }

    /// <summary> Gets Unknown146.</summary>
    public required int Unknown146 { get; init; }

    /// <summary> Gets Unknown150.</summary>
    public required int Unknown150 { get; init; }

    /// <summary> Gets Unknown154.</summary>
    public required int Unknown154 { get; init; }

    /// <summary> Gets Unknown158.</summary>
    public required int Unknown158 { get; init; }

    /// <summary> Gets Unknown162.</summary>
    public required int Unknown162 { get; init; }

    /// <inheritdoc/>
    public static TableMonsterSpawnersDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/TableMonsterSpawners.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TableMonsterSpawnersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetMonsterVarietiesDat();

            // loading Metadata
            (var metadataLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AreaLevel
            (var arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpawnsMonsters
            (var tempspawnsmonstersLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnsmonstersLoading = tempspawnsmonstersLoading.AsReadOnly();

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
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown70
            (var unknown70Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown74
            (var unknown74Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown78
            (var unknown78Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown82
            (var unknown82Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown98
            (var unknown98Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown99
            (var unknown99Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Script1
            (var script1Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown109
            (var unknown109Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Script2
            (var script2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown118
            (var tempunknown118Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown118Loading = tempunknown118Loading.AsReadOnly();

            // loading Unknown134
            (var unknown134Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown138
            (var unknown138Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown142
            (var unknown142Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown146
            (var unknown146Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown150
            (var unknown150Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown154
            (var unknown154Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown158
            (var unknown158Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown162
            (var unknown162Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TableMonsterSpawnersDat()
            {
                Metadata = metadataLoading,
                AreaLevel = arealevelLoading,
                SpawnsMonsters = spawnsmonstersLoading,
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
                Unknown70 = unknown70Loading,
                Unknown74 = unknown74Loading,
                Unknown78 = unknown78Loading,
                Unknown82 = unknown82Loading,
                Unknown98 = unknown98Loading,
                Unknown99 = unknown99Loading,
                Script1 = script1Loading,
                Unknown108 = unknown108Loading,
                Unknown109 = unknown109Loading,
                Script2 = script2Loading,
                Unknown118 = unknown118Loading,
                Unknown134 = unknown134Loading,
                Unknown138 = unknown138Loading,
                Unknown142 = unknown142Loading,
                Unknown146 = unknown146Loading,
                Unknown150 = unknown150Loading,
                Unknown154 = unknown154Loading,
                Unknown158 = unknown158Loading,
                Unknown162 = unknown162Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
