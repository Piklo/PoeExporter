// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing UniqueJewelLimits.dat data.
/// </summary>
public sealed partial class UniqueJewelLimitsDat : ISpecificationFile<UniqueJewelLimitsDat>
{
    /// <summary> Gets JewelName.</summary>
    /// <remarks> references <see cref="WordsDat"/> on <see cref="Specification.GetWordsDat"/> index.</remarks>
    public required int? JewelName { get; init; }

    /// <summary> Gets Limit.</summary>
    public required int Limit { get; init; }

    /// <inheritdoc/>
    public static UniqueJewelLimitsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/UniqueJewelLimits.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UniqueJewelLimitsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading JewelName
            (var jewelnameLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Limit
            (var limitLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UniqueJewelLimitsDat()
            {
                JewelName = jewelnameLoading,
                Limit = limitLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
