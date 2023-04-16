// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing WeaponPassiveTreeBalancePerItemLevel.dat data.
/// </summary>
public sealed partial class WeaponPassiveTreeBalancePerItemLevelDat
{
    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets Bar1.</summary>
    public required int Bar1 { get; init; }

    /// <summary> Gets Bar2.</summary>
    public required int Bar2 { get; init; }

    /// <summary> Gets Bar3.</summary>
    public required int Bar3 { get; init; }

    /// <summary> Gets Bar4.</summary>
    public required int Bar4 { get; init; }

    /// <summary> Gets Bar5.</summary>
    public required int Bar5 { get; init; }

    /// <summary>
    /// Gets WeaponPassiveTreeBalancePerItemLevelDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of WeaponPassiveTreeBalancePerItemLevelDat.</returns>
    internal static WeaponPassiveTreeBalancePerItemLevelDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/WeaponPassiveTreeBalancePerItemLevel.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WeaponPassiveTreeBalancePerItemLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Bar1
            (var bar1Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Bar2
            (var bar2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Bar3
            (var bar3Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Bar4
            (var bar4Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Bar5
            (var bar5Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WeaponPassiveTreeBalancePerItemLevelDat()
            {
                Level = levelLoading,
                Bar1 = bar1Loading,
                Bar2 = bar2Loading,
                Bar3 = bar3Loading,
                Bar4 = bar4Loading,
                Bar5 = bar5Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
