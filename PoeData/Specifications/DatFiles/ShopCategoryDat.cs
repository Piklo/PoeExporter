﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ShopCategory.dat data.
/// </summary>
public sealed partial class ShopCategoryDat
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
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? AppliedTo_BaseItemTypesKey { get; init; }

    /// <summary>
    /// Gets ShopCategoryDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ShopCategoryDat.</returns>
    internal static ShopCategoryDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ShopCategory.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

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
            (var appliedto_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
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
