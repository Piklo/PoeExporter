// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DeliriumStashTabLayout.dat data.
/// </summary>
public sealed partial class DeliriumStashTabLayoutDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BaseItemType.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemType { get; init; }

    /// <summary> Gets X.</summary>
    public required int X { get; init; }

    /// <summary> Gets Y.</summary>
    public required int Y { get; init; }

    /// <summary> Gets IntId.</summary>
    public required int IntId { get; init; }

    /// <summary> Gets Width.</summary>
    public required int Width { get; init; }

    /// <summary> Gets Height.</summary>
    public required int Height { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets a value indicating whether Unknown48 is set.</summary>
    public required bool Unknown48 { get; init; }

    /// <summary>
    /// Gets DeliriumStashTabLayoutDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of DeliriumStashTabLayoutDat.</returns>
    internal static DeliriumStashTabLayoutDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/DeliriumStashTabLayout.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DeliriumStashTabLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseItemType
            (var baseitemtypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading X
            (var xLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Y
            (var yLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IntId
            (var intidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Width
            (var widthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Height
            (var heightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DeliriumStashTabLayoutDat()
            {
                Id = idLoading,
                BaseItemType = baseitemtypeLoading,
                X = xLoading,
                Y = yLoading,
                IntId = intidLoading,
                Width = widthLoading,
                Height = heightLoading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
