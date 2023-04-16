// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing WarbandsGraph.dat data.
/// </summary>
public sealed partial class WarbandsGraphDat
{
    /// <summary> Gets WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets Connections.</summary>
    public required ReadOnlyCollection<int> Connections { get; init; }

    /// <summary>
    /// Gets WarbandsGraphDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of WarbandsGraphDat.</returns>
    internal static WarbandsGraphDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/WarbandsGraph.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new WarbandsGraphDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Connections
            (var tempconnectionsLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var connectionsLoading = tempconnectionsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new WarbandsGraphDat()
            {
                WorldAreasKey = worldareaskeyLoading,
                Connections = connectionsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
