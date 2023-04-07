// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MonsterMapBossDifficulty.dat data.
/// </summary>
public sealed partial class MonsterMapBossDifficultyDat
{
    /// <summary> Gets MapLevel.</summary>
    public required int MapLevel { get; init; }

    /// <summary> Gets Stat1Value.</summary>
    public required int Stat1Value { get; init; }

    /// <summary> Gets Stat2Value.</summary>
    public required int Stat2Value { get; init; }

    /// <summary> Gets StatsKey1.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? StatsKey1 { get; init; }

    /// <summary> Gets StatsKey2.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? StatsKey2 { get; init; }

    /// <summary> Gets StatsKey3.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? StatsKey3 { get; init; }

    /// <summary> Gets Stat3Value.</summary>
    public required int Stat3Value { get; init; }

    /// <summary> Gets StatsKey4.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? StatsKey4 { get; init; }

    /// <summary> Gets Stat4Value.</summary>
    public required int Stat4Value { get; init; }

    /// <summary> Gets StatsKey5.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? StatsKey5 { get; init; }

    /// <summary> Gets Stat5Value.</summary>
    public required int Stat5Value { get; init; }

    /// <summary>
    /// Gets MonsterMapBossDifficultyDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MonsterMapBossDifficultyDat.</returns>
    internal static MonsterMapBossDifficultyDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MonsterMapBossDifficulty.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterMapBossDifficultyDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MapLevel
            (var maplevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat1Value
            (var stat1valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat2Value
            (var stat2valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKey1
            (var statskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading StatsKey2
            (var statskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading StatsKey3
            (var statskey3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Stat3Value
            (var stat3valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKey4
            (var statskey4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Stat4Value
            (var stat4valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading StatsKey5
            (var statskey5Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Stat5Value
            (var stat5valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterMapBossDifficultyDat()
            {
                MapLevel = maplevelLoading,
                Stat1Value = stat1valueLoading,
                Stat2Value = stat2valueLoading,
                StatsKey1 = statskey1Loading,
                StatsKey2 = statskey2Loading,
                StatsKey3 = statskey3Loading,
                Stat3Value = stat3valueLoading,
                StatsKey4 = statskey4Loading,
                Stat4Value = stat4valueLoading,
                StatsKey5 = statskey5Loading,
                Stat5Value = stat5valueLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
