// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing IndexableSupportGems.dat data.
/// </summary>
public sealed partial class IndexableSupportGemsDat
{
    /// <summary> Gets Index.</summary>
    public required int Index { get; init; }

    /// <summary> Gets SupportGem.</summary>
    /// <remarks> references <see cref="SkillGemsDat"/> on <see cref="Specification.GetSkillGemsDat"/> index.</remarks>
    public required int? SupportGem { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets IndexableSupportGemsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of IndexableSupportGemsDat.</returns>
    internal static IndexableSupportGemsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/IndexableSupportGems.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new IndexableSupportGemsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Index
            (var indexLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SupportGem
            (var supportgemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new IndexableSupportGemsDat()
            {
                Index = indexLoading,
                SupportGem = supportgemLoading,
                Name = nameLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
