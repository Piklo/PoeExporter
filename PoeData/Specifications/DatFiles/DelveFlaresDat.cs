// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DelveFlares.dat data.
/// </summary>
public sealed partial class DelveFlaresDat
{
    /// <summary> Gets Unknown0.</summary>
    public required int Unknown0 { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required int? Unknown4 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int? Unknown20 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int? Unknown40 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int Unknown60 { get; init; }

    /// <summary>
    /// Gets DelveFlaresDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of DelveFlaresDat.</returns>
    internal static DelveFlaresDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/DelveFlares.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveFlaresDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveFlaresDat()
            {
                Unknown0 = unknown0Loading,
                Unknown4 = unknown4Loading,
                Unknown20 = unknown20Loading,
                Unknown36 = unknown36Loading,
                Unknown40 = unknown40Loading,
                Unknown56 = unknown56Loading,
                Unknown60 = unknown60Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
