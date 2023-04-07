// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing IncursionUniqueUpgradeComponents.dat data.
/// </summary>
public sealed partial class IncursionUniqueUpgradeComponentsDat
{
    /// <summary> Gets Unknown0.</summary>
    public required int? Unknown0 { get; init; }

    /// <summary> Gets BaseItemType.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemType { get; init; }

    /// <summary>
    /// Gets IncursionUniqueUpgradeComponentsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of IncursionUniqueUpgradeComponentsDat.</returns>
    internal static IncursionUniqueUpgradeComponentsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/IncursionUniqueUpgradeComponents.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new IncursionUniqueUpgradeComponentsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BaseItemType
            (var baseitemtypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new IncursionUniqueUpgradeComponentsDat()
            {
                Unknown0 = unknown0Loading,
                BaseItemType = baseitemtypeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
