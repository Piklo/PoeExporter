// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing HeistChests.dat data.
/// </summary>
public sealed partial class HeistChestsDat : ISpecificationFile<HeistChestsDat>
{
    /// <summary> Gets ChestsKey.</summary>
    public required int? ChestsKey { get; init; }

    /// <summary> Gets Weight.</summary>
    public required int Weight { get; init; }

    /// <summary> Gets HeistAreasKey.</summary>
    public required ReadOnlyCollection<int> HeistAreasKey { get; init; }

    /// <summary> Gets HeistChestTypesKey.</summary>
    public required int HeistChestTypesKey { get; init; }

    /// <inheritdoc/>
    public static HeistChestsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/HeistChests.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

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

            // loading referenced tables if any
            // specification.GetChestsDat();
            // specification.GetHeistAreasDat();

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
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
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
