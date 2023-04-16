// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing EndlessLedgeChests.dat data.
/// </summary>
public sealed partial class EndlessLedgeChestsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets BaseItemTypesKeys.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BaseItemTypesKeys { get; init; }

    /// <summary> Gets SocketColour.</summary>
    public required string SocketColour { get; init; }

    /// <summary>
    /// Gets EndlessLedgeChestsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of EndlessLedgeChestsDat.</returns>
    internal static EndlessLedgeChestsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/EndlessLedgeChests.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EndlessLedgeChestsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BaseItemTypesKeys
            (var tempbaseitemtypeskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var baseitemtypeskeysLoading = tempbaseitemtypeskeysLoading.AsReadOnly();

            // loading SocketColour
            (var socketcolourLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EndlessLedgeChestsDat()
            {
                Id = idLoading,
                WorldAreasKey = worldareaskeyLoading,
                BaseItemTypesKeys = baseitemtypeskeysLoading,
                SocketColour = socketcolourLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
