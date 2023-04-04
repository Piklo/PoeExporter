// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing IndexableSupportGems.dat data.
/// </summary>
public sealed partial class IndexableSupportGemsDat : ISpecificationFile<IndexableSupportGemsDat>
{
    /// <summary> Gets Index.</summary>
    public required int Index { get; init; }

    /// <summary> Gets SupportGem.</summary>
    /// <remarks> references <see cref="SkillGemsDat"/> on <see cref="Specification.GetSkillGemsDat"/> index.</remarks>
    public required int? SupportGem { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <inheritdoc/>
    public static IndexableSupportGemsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/IndexableSupportGems.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

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
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
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
