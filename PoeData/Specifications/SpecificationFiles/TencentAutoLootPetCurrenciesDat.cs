﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing TencentAutoLootPetCurrencies.dat data.
/// </summary>
public sealed partial class TencentAutoLootPetCurrenciesDat : ISpecificationFile<TencentAutoLootPetCurrenciesDat>
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown16 is set.</summary>
    public required bool Unknown16 { get; init; }

    /// <inheritdoc/>
    public static TencentAutoLootPetCurrenciesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/TencentAutoLootPetCurrencies.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TencentAutoLootPetCurrenciesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetBaseItemTypesDat();

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TencentAutoLootPetCurrenciesDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Unknown16 = unknown16Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}