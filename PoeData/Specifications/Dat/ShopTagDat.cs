// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing ShopTag.dat data.
/// </summary>
public sealed partial class ShopTagDat : IDat<ShopTagDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets a value indicating whether IsCategory is set.</summary>
    public required bool IsCategory { get; init; }

    /// <summary> Gets Category.</summary>
    /// <remarks> references <see cref="ShopTagDat"/> on <see cref="Specification.GetShopTagDat"/> index.</remarks>
    public required int? Category { get; init; }

    /// <summary> Gets SkillGem.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SkillGem { get; init; }

    /// <inheritdoc/>
    public static ShopTagDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/ShopTag.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShopTagDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsCategory
            (var iscategoryLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Category
            (var categoryLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading SkillGem
            (var tempskillgemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var skillgemLoading = tempskillgemLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShopTagDat()
            {
                Id = idLoading,
                Name = nameLoading,
                IsCategory = iscategoryLoading,
                Category = categoryLoading,
                SkillGem = skillgemLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
