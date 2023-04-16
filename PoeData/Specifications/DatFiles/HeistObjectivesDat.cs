// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistObjectives.dat data.
/// </summary>
public sealed partial class HeistObjectivesDat
{
    /// <summary> Gets BaseItemType.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemType { get; init; }

    /// <summary> Gets Scaling.</summary>
    public required float Scaling { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets HeistObjectivesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HeistObjectivesDat.</returns>
    internal static HeistObjectivesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HeistObjectives.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistObjectivesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemType
            (var baseitemtypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Scaling
            (var scalingLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistObjectivesDat()
            {
                BaseItemType = baseitemtypeLoading,
                Scaling = scalingLoading,
                Name = nameLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
