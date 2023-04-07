// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DaemonSpawningData.dat data.
/// </summary>
public sealed partial class DaemonSpawningDataDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MonsterVarieties.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MonsterVarieties { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets a value indicating whether Unknown28 is set.</summary>
    public required bool Unknown28 { get; init; }

    /// <summary> Gets Unknown29.</summary>
    public required int Unknown29 { get; init; }

    /// <summary> Gets Unknown33.</summary>
    public required int Unknown33 { get; init; }

    /// <summary> Gets a value indicating whether Unknown37 is set.</summary>
    public required bool Unknown37 { get; init; }

    /// <summary> Gets a value indicating whether Unknown38 is set.</summary>
    public required bool Unknown38 { get; init; }

    /// <summary> Gets a value indicating whether Unknown39 is set.</summary>
    public required bool Unknown39 { get; init; }

    /// <summary>
    /// Gets DaemonSpawningDataDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of DaemonSpawningDataDat.</returns>
    internal static DaemonSpawningDataDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/DaemonSpawningData.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DaemonSpawningDataDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterVarieties
            (var tempmonstervarietiesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monstervarietiesLoading = tempmonstervarietiesLoading.AsReadOnly();

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown29
            (var unknown29Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown33
            (var unknown33Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown37
            (var unknown37Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown38
            (var unknown38Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown39
            (var unknown39Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DaemonSpawningDataDat()
            {
                Id = idLoading,
                MonsterVarieties = monstervarietiesLoading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Unknown29 = unknown29Loading,
                Unknown33 = unknown33Loading,
                Unknown37 = unknown37Loading,
                Unknown38 = unknown38Loading,
                Unknown39 = unknown39Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
