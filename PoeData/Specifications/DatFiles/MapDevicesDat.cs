// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MapDevices.dat data.
/// </summary>
public sealed partial class MapDevicesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MiscObject.</summary>
    /// <remarks> references <see cref="MiscObjectsDat"/> on <see cref="Specification.LoadMiscObjectsDat"/> index.</remarks>
    public required int? MiscObject { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required string Unknown28 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required string Unknown36 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required ReadOnlyCollection<int> Unknown44 { get; init; }

    /// <summary> Gets a value indicating whether Unknown60 is set.</summary>
    public required bool Unknown60 { get; init; }

    /// <summary> Gets Unknown61.</summary>
    public required int Unknown61 { get; init; }

    /// <summary> Gets a value indicating whether Unknown65 is set.</summary>
    public required bool Unknown65 { get; init; }

    /// <summary>
    /// Gets MapDevicesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MapDevicesDat.</returns>
    internal static MapDevicesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MapDevices.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapDevicesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MiscObject
            (var miscobjectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown44
            (var tempunknown44Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown44Loading = tempunknown44Loading.AsReadOnly();

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown61
            (var unknown61Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown65
            (var unknown65Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapDevicesDat()
            {
                Id = idLoading,
                MiscObject = miscobjectLoading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Unknown36 = unknown36Loading,
                Unknown44 = unknown44Loading,
                Unknown60 = unknown60Loading,
                Unknown61 = unknown61Loading,
                Unknown65 = unknown65Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
