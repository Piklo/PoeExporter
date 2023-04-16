// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MonsterPackCounts.dat data.
/// </summary>
public sealed partial class MonsterPackCountsDat
{
    /// <summary> Gets Unknown0.</summary>
    public required int? Unknown0 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required ReadOnlyCollection<int> Unknown20 { get; init; }

    /// <summary> Gets a value indicating whether Unknown36 is set.</summary>
    public required bool Unknown36 { get; init; }

    /// <summary> Gets Unknown37.</summary>
    public required ReadOnlyCollection<int> Unknown37 { get; init; }

    /// <summary>
    /// Gets MonsterPackCountsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MonsterPackCountsDat.</returns>
    internal static MonsterPackCountsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MonsterPackCounts.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterPackCountsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var tempunknown20Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown20Loading = tempunknown20Loading.AsReadOnly();

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown37
            (var tempunknown37Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown37Loading = tempunknown37Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterPackCountsDat()
            {
                Unknown0 = unknown0Loading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown36 = unknown36Loading,
                Unknown37 = unknown37Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
