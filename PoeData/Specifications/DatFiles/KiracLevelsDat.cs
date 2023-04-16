// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing KiracLevels.dat data.
/// </summary>
public sealed partial class KiracLevelsDat
{
    /// <summary> Gets AreaLevel.</summary>
    public required int AreaLevel { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required int Unknown4 { get; init; }

    /// <summary>
    /// Gets KiracLevelsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of KiracLevelsDat.</returns>
    internal static KiracLevelsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/KiracLevels.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new KiracLevelsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading AreaLevel
            (var arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new KiracLevelsDat()
            {
                AreaLevel = arealevelLoading,
                Unknown4 = unknown4Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
