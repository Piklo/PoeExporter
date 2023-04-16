// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing WindowCursors.dat data.
/// </summary>
public sealed partial class WindowCursorsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets CursorDefault.</summary>
    public required string CursorDefault { get; init; }

    /// <summary> Gets CursorClick.</summary>
    public required string CursorClick { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets CursorHover.</summary>
    public required string CursorHover { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets a value indicating whether IsEnabled is set.</summary>
    public required bool IsEnabled { get; init; }

    /// <summary> Gets Unknown49.</summary>
    public required int Unknown49 { get; init; }

    /// <summary>
    /// Gets WindowCursorsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of WindowCursorsDat.</returns>
    internal static WindowCursorsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/WindowCursors.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WindowCursorsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CursorDefault
            (var cursordefaultLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CursorClick
            (var cursorclickLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CursorHover
            (var cursorhoverLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsEnabled
            (var isenabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown49
            (var unknown49Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WindowCursorsDat()
            {
                Id = idLoading,
                CursorDefault = cursordefaultLoading,
                CursorClick = cursorclickLoading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                CursorHover = cursorhoverLoading,
                Description = descriptionLoading,
                IsEnabled = isenabledLoading,
                Unknown49 = unknown49Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
