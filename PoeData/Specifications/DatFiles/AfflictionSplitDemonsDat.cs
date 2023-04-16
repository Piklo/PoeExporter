﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AfflictionSplitDemons.dat data.
/// </summary>
public sealed partial class AfflictionSplitDemonsDat
{
    /// <summary> Gets Unknown0.</summary>
    public required int Unknown0 { get; init; }

    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets AfflictionRandomModCategoriesKey.</summary>
    /// <remarks> references <see cref="AfflictionRandomModCategoriesDat"/> on <see cref="Specification.GetAfflictionRandomModCategoriesDat"/> index.</remarks>
    public required int? AfflictionRandomModCategoriesKey { get; init; }

    /// <summary>
    /// Gets AfflictionSplitDemonsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AfflictionSplitDemonsDat.</returns>
    internal static AfflictionSplitDemonsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/AfflictionSplitDemons.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AfflictionSplitDemonsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading AfflictionRandomModCategoriesKey
            (var afflictionrandommodcategorieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AfflictionSplitDemonsDat()
            {
                Unknown0 = unknown0Loading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
                AfflictionRandomModCategoriesKey = afflictionrandommodcategorieskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}