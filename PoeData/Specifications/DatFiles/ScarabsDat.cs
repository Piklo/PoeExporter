// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Scarabs.dat data.
/// </summary>
public sealed partial class ScarabsDat
{
    /// <summary> Gets ScarabType.</summary>
    public required int ScarabType { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary>
    /// Gets ScarabsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ScarabsDat.</returns>
    internal static ScarabsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Scarabs.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ScarabsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ScarabType
            (var scarabtypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ScarabsDat()
            {
                ScarabType = scarabtypeLoading,
                Tier = tierLoading,
                BaseItemTypesKey = baseitemtypeskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
