// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ModType.dat data.
/// </summary>
public sealed partial class ModTypeDat
{
    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets ModSellPriceTypesKeys.</summary>
    /// <remarks> references <see cref="ModSellPriceTypesDat"/> on <see cref="Specification.LoadModSellPriceTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModSellPriceTypesKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown24 is set.</summary>
    public required bool Unknown24 { get; init; }

    /// <summary>
    /// Gets ModTypeDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ModTypeDat.</returns>
    internal static ModTypeDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ModType.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ModTypeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ModSellPriceTypesKeys
            (var tempmodsellpricetypeskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modsellpricetypeskeysLoading = tempmodsellpricetypeskeysLoading.AsReadOnly();

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ModTypeDat()
            {
                Name = nameLoading,
                ModSellPriceTypesKeys = modsellpricetypeskeysLoading,
                Unknown24 = unknown24Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
