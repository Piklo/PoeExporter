// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DelveCraftingTags.dat data.
/// </summary>
public sealed partial class DelveCraftingTagsDat
{
    /// <summary> Gets TagsKey.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required int? TagsKey { get; init; }

    /// <summary> Gets ItemClass.</summary>
    public required string ItemClass { get; init; }

    /// <summary>
    /// Gets DelveCraftingTagsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of DelveCraftingTagsDat.</returns>
    internal static DelveCraftingTagsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/DelveCraftingTags.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveCraftingTagsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading TagsKey
            (var tagskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ItemClass
            (var itemclassLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveCraftingTagsDat()
            {
                TagsKey = tagskeyLoading,
                ItemClass = itemclassLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
