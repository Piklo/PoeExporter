// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing ShopCategory.dat data.
/// </summary>
public sealed partial class ShopCategoryDat : ISpecificationFile<ShopCategoryDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ClientText.</summary>
    public required string ClientText { get; init; }

    /// <summary> Gets ClientJPGFile.</summary>
    public required string ClientJPGFile { get; init; }

    /// <summary> Gets WebsiteText.</summary>
    public required string WebsiteText { get; init; }

    /// <summary> Gets WebsiteJPGFile.</summary>
    public required string WebsiteJPGFile { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets AppliedTo_BaseItemTypesKey.</summary>
    public required int? AppliedTo_BaseItemTypesKey { get; init; }

    /// <inheritdoc/>
    public static ShopCategoryDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/ShopCategory.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShopCategoryDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetBaseItemTypesDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ClientText
            (var clienttextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ClientJPGFile
            (var clientjpgfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WebsiteText
            (var websitetextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WebsiteJPGFile
            (var websitejpgfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AppliedTo_BaseItemTypesKey
            (var appliedto_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShopCategoryDat()
            {
                Id = idLoading,
                ClientText = clienttextLoading,
                ClientJPGFile = clientjpgfileLoading,
                WebsiteText = websitetextLoading,
                WebsiteJPGFile = websitejpgfileLoading,
                Unknown40 = unknown40Loading,
                AppliedTo_BaseItemTypesKey = appliedto_baseitemtypeskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
