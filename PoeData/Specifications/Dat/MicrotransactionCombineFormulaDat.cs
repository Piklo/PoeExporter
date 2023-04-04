﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MicrotransactionCombineFormula.dat data.
/// </summary>
public sealed partial class MicrotransactionCombineFormulaDat : ISpecificationFile<MicrotransactionCombineFormulaDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Result_BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? Result_BaseItemTypesKey { get; init; }

    /// <summary> Gets Ingredients_BaseItemTypesKeys.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Ingredients_BaseItemTypesKeys { get; init; }

    /// <summary> Gets BK2File.</summary>
    public required string BK2File { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required ReadOnlyCollection<int> Unknown48 { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets a value indicating whether Unknown68 is set.</summary>
    public required bool Unknown68 { get; init; }

    /// <inheritdoc/>
    public static MicrotransactionCombineFormulaDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MicrotransactionCombineFormula.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MicrotransactionCombineFormulaDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Result_BaseItemTypesKey
            (var result_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Ingredients_BaseItemTypesKeys
            (var tempingredients_baseitemtypeskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var ingredients_baseitemtypeskeysLoading = tempingredients_baseitemtypeskeysLoading.AsReadOnly();

            // loading BK2File
            (var bk2fileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown48
            (var tempunknown48Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown48Loading = tempunknown48Loading.AsReadOnly();

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MicrotransactionCombineFormulaDat()
            {
                Id = idLoading,
                Result_BaseItemTypesKey = result_baseitemtypeskeyLoading,
                Ingredients_BaseItemTypesKeys = ingredients_baseitemtypeskeysLoading,
                BK2File = bk2fileLoading,
                Unknown48 = unknown48Loading,
                Unknown64 = unknown64Loading,
                Unknown68 = unknown68Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}