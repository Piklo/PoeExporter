// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistChests.dat data.
/// </summary>
public sealed partial class HeistChestsDat
{
    /// <summary> Gets ChestsKey.</summary>
    /// <remarks> references <see cref="ChestsDat"/> on <see cref="Specification.LoadChestsDat"/> index.</remarks>
    public required int? ChestsKey { get; init; }

    /// <summary> Gets Weight.</summary>
    public required int Weight { get; init; }

    /// <summary> Gets HeistAreasKey.</summary>
    /// <remarks> references <see cref="HeistAreasDat"/> on <see cref="Specification.LoadHeistAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> HeistAreasKey { get; init; }

    /// <summary> Gets HeistChestTypesKey.</summary>
    /// <remarks> references <see cref="HeistChestTypesDat"/> on <see cref="Specification.LoadHeistChestTypesDat"/> index.</remarks>
    public required int HeistChestTypesKey { get; init; }

    /// <summary>
    /// Gets HeistChestsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HeistChestsDat.</returns>
    internal static HeistChestsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HeistChests.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistChestsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ChestsKey
            (var chestskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Weight
            (var weightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HeistAreasKey
            (var tempheistareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var heistareaskeyLoading = tempheistareaskeyLoading.AsReadOnly();

            // loading HeistChestTypesKey
            (var heistchesttypeskeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistChestsDat()
            {
                ChestsKey = chestskeyLoading,
                Weight = weightLoading,
                HeistAreasKey = heistareaskeyLoading,
                HeistChestTypesKey = heistchesttypeskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
