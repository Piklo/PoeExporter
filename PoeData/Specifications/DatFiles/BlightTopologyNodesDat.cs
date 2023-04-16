// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BlightTopologyNodes.dat data.
/// </summary>
public sealed partial class BlightTopologyNodesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required ReadOnlyCollection<int> Unknown8 { get; init; }

    /// <summary> Gets Size.</summary>
    public required int Size { get; init; }

    /// <summary> Gets Angle.</summary>
    public required int Angle { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required ReadOnlyCollection<int> Unknown32 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required ReadOnlyCollection<int> Unknown48 { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required ReadOnlyCollection<int> Unknown64 { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required ReadOnlyCollection<int> Unknown80 { get; init; }

    /// <summary> Gets Unknown96.</summary>
    public required ReadOnlyCollection<int> Unknown96 { get; init; }

    /// <summary> Gets Unknown112.</summary>
    public required ReadOnlyCollection<int> Unknown112 { get; init; }

    /// <summary> Gets Unknown128.</summary>
    public required int Unknown128 { get; init; }

    /// <summary>
    /// Gets BlightTopologyNodesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of BlightTopologyNodesDat.</returns>
    internal static BlightTopologyNodesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/BlightTopologyNodes.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BlightTopologyNodesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var tempunknown8Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown8Loading = tempunknown8Loading.AsReadOnly();

            // loading Size
            (var sizeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Angle
            (var angleLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var tempunknown32Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown32Loading = tempunknown32Loading.AsReadOnly();

            // loading Unknown48
            (var tempunknown48Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown48Loading = tempunknown48Loading.AsReadOnly();

            // loading Unknown64
            (var tempunknown64Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown64Loading = tempunknown64Loading.AsReadOnly();

            // loading Unknown80
            (var tempunknown80Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown80Loading = tempunknown80Loading.AsReadOnly();

            // loading Unknown96
            (var tempunknown96Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown96Loading = tempunknown96Loading.AsReadOnly();

            // loading Unknown112
            (var tempunknown112Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown112Loading = tempunknown112Loading.AsReadOnly();

            // loading Unknown128
            (var unknown128Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BlightTopologyNodesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Size = sizeLoading,
                Angle = angleLoading,
                Unknown32 = unknown32Loading,
                Unknown48 = unknown48Loading,
                Unknown64 = unknown64Loading,
                Unknown80 = unknown80Loading,
                Unknown96 = unknown96Loading,
                Unknown112 = unknown112Loading,
                Unknown128 = unknown128Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
