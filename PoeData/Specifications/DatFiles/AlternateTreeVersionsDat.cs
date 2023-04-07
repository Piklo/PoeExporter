// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AlternateTreeVersions.dat data.
/// </summary>
public sealed partial class AlternateTreeVersionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets a value indicating whether Unknown8 is set.</summary>
    public required bool Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether Unknown9 is set.</summary>
    public required bool Unknown9 { get; init; }

    /// <summary> Gets Unknown10.</summary>
    public required int Unknown10 { get; init; }

    /// <summary> Gets Unknown14.</summary>
    public required int Unknown14 { get; init; }

    /// <summary> Gets Unknown18.</summary>
    public required int Unknown18 { get; init; }

    /// <summary> Gets Unknown22.</summary>
    public required int Unknown22 { get; init; }

    /// <summary> Gets Unknown26.</summary>
    public required int Unknown26 { get; init; }

    /// <summary> Gets Unknown30.</summary>
    public required int Unknown30 { get; init; }

    /// <summary> Gets Unknown34.</summary>
    public required int Unknown34 { get; init; }

    /// <summary>
    /// Gets AlternateTreeVersionsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AlternateTreeVersionsDat.</returns>
    internal static AlternateTreeVersionsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/AlternateTreeVersions.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AlternateTreeVersionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown9
            (var unknown9Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown10
            (var unknown10Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown14
            (var unknown14Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown18
            (var unknown18Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown22
            (var unknown22Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown26
            (var unknown26Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown30
            (var unknown30Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown34
            (var unknown34Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AlternateTreeVersionsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown9 = unknown9Loading,
                Unknown10 = unknown10Loading,
                Unknown14 = unknown14Loading,
                Unknown18 = unknown18Loading,
                Unknown22 = unknown22Loading,
                Unknown26 = unknown26Loading,
                Unknown30 = unknown30Loading,
                Unknown34 = unknown34Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
