// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MonsterConditions.dat data.
/// </summary>
public sealed partial class MonsterConditionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int? Unknown8 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int? Unknown40 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required ReadOnlyCollection<int> Unknown56 { get; init; }

    /// <summary> Gets a value indicating whether Unknown72 is set.</summary>
    public required bool Unknown72 { get; init; }

    /// <summary> Gets a value indicating whether Unknown73 is set.</summary>
    public required bool Unknown73 { get; init; }

    /// <summary> Gets Unknown74.</summary>
    public required ReadOnlyCollection<int> Unknown74 { get; init; }

    /// <summary> Gets Unknown90.</summary>
    public required ReadOnlyCollection<int> Unknown90 { get; init; }

    /// <summary> Gets Unknown106.</summary>
    public required int Unknown106 { get; init; }

    /// <summary> Gets Unknown110.</summary>
    public required int Unknown110 { get; init; }

    /// <summary> Gets Unknown114.</summary>
    public required int Unknown114 { get; init; }

    /// <summary> Gets Unknown118.</summary>
    public required int Unknown118 { get; init; }

    /// <summary>
    /// Gets MonsterConditionsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MonsterConditionsDat.</returns>
    internal static MonsterConditionsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MonsterConditions.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterConditionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var tempunknown56Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown56Loading = tempunknown56Loading.AsReadOnly();

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown73
            (var unknown73Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown74
            (var tempunknown74Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown74Loading = tempunknown74Loading.AsReadOnly();

            // loading Unknown90
            (var tempunknown90Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown90Loading = tempunknown90Loading.AsReadOnly();

            // loading Unknown106
            (var unknown106Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown110
            (var unknown110Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown114
            (var unknown114Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown118
            (var unknown118Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterConditionsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown24 = unknown24Loading,
                Unknown40 = unknown40Loading,
                Unknown56 = unknown56Loading,
                Unknown72 = unknown72Loading,
                Unknown73 = unknown73Loading,
                Unknown74 = unknown74Loading,
                Unknown90 = unknown90Loading,
                Unknown106 = unknown106Loading,
                Unknown110 = unknown110Loading,
                Unknown114 = unknown114Loading,
                Unknown118 = unknown118Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
