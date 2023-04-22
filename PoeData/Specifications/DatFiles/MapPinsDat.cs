// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MapPins.dat data.
/// </summary>
public sealed partial class MapPinsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets PositionX.</summary>
    public required int PositionX { get; init; }

    /// <summary> Gets PositionY.</summary>
    public required int PositionY { get; init; }

    /// <summary> Gets Waypoint_WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? Waypoint_WorldAreasKey { get; init; }

    /// <summary> Gets WorldAreasKeys.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> WorldAreasKeys { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets FlavourText.</summary>
    public required string FlavourText { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required ReadOnlyCollection<int> Unknown64 { get; init; }

    /// <summary> Gets Act.</summary>
    public required int Act { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required string Unknown84 { get; init; }

    /// <summary> Gets Unknown92.</summary>
    public required ReadOnlyCollection<int> Unknown92 { get; init; }

    /// <summary> Gets Unknown108.</summary>
    public required int Unknown108 { get; init; }

    /// <summary>
    /// Gets MapPinsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MapPinsDat.</returns>
    internal static MapPinsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MapPins.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapPinsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PositionX
            (var positionxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PositionY
            (var positionyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Waypoint_WorldAreasKey
            (var waypoint_worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading WorldAreasKeys
            (var tempworldareaskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var worldareaskeysLoading = tempworldareaskeysLoading.AsReadOnly();

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FlavourText
            (var flavourtextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var tempunknown64Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown64Loading = tempunknown64Loading.AsReadOnly();

            // loading Act
            (var actLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown92
            (var tempunknown92Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown92Loading = tempunknown92Loading.AsReadOnly();

            // loading Unknown108
            (var unknown108Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapPinsDat()
            {
                Id = idLoading,
                PositionX = positionxLoading,
                PositionY = positionyLoading,
                Waypoint_WorldAreasKey = waypoint_worldareaskeyLoading,
                WorldAreasKeys = worldareaskeysLoading,
                Name = nameLoading,
                FlavourText = flavourtextLoading,
                Unknown64 = unknown64Loading,
                Act = actLoading,
                Unknown84 = unknown84Loading,
                Unknown92 = unknown92Loading,
                Unknown108 = unknown108Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
